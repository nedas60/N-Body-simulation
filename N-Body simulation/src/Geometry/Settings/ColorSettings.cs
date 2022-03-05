﻿using GlmSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Body_simulation.src.Geometry.Settings
{
    internal class ColorSettings
    {
        List<Mark> Colors;

        public ColorSettings()
        {
            Colors = new List<Mark>();
            Colors.Add(new Mark(0f, new Color(84, 134, 219)));
            Colors.Add(new Mark(0.04f, new Color(164, 174, 91)));
            Colors.Add(new Mark(0.10f, new Color(112, 183, 19)));
            Colors.Add(new Mark(0.57f, new Color(145, 90, 42)));
            //Colors.Add(new Mark(0.78f, new Color(125, 81, 45)));
            Colors.Add(new Mark(0.78f, new Color(255, 255, 255)));
        }

        public void Add(Mark mark)
        {
            Colors.Add(mark);
            Colors.Sort();
        }
        public Color GetColorAt(float pos)
        {
            if (Colors.Count == 0) return new Color(0, 0, 0);
            Mark First = Colors[0], Last = Colors[0];
            for (int i = 0; i < Colors.Count; i++)
            {
                if (Colors[i].Position <= pos) continue;
                Last = Colors[i];
                if (i == 0) return First.Color;
                else First = Colors[i-1];
                break;
            }
            if (pos <= Colors[0].Position) return Colors[0].Color;
            if (pos >= Colors[Colors.Count - 1].Position) return Colors[Colors.Count - 1].Color;

            float weight = (pos - First.Position) / (Last.Position - First.Position);
            float R = First.Color.RNorm * (1 - weight) + Last.Color.RNorm * weight;
            float G = First.Color.GNorm * (1 - weight) + Last.Color.GNorm * weight;
            float B = First.Color.BNorm * (1 - weight) + Last.Color.BNorm * weight;

            return new Color(R, G, B);
        }
    }
    public struct Mark
    {
        public float Position = 0f;
        public Color Color;
        public Mark(float pos, Color col)
        {
            Position = pos;
            Color = col;
        }
        public static bool operator >(Mark lhs, Mark rhs)
        {
            return lhs.Position > rhs.Position;
        }
        public static bool operator <(Mark lhs, Mark rhs)
        {
            return lhs.Position < rhs.Position;
        }
    }
    public struct Color {
        public int R, G, B;
        public float RNorm, GNorm, BNorm;
        public Color(int R, int G, int B)
        {
            this.R = R;
            this.G = G;
            this.B = B;
            RNorm = R / 255f;
            GNorm = G / 255f;
            BNorm = B / 255f;
        }
        public Color(float RNorm, float GNorm, float BNorm)
        {
            this.RNorm = RNorm;
            this.GNorm = GNorm;
            this.BNorm = BNorm;
            R = (int)(RNorm * 255f);
            G = (int)(GNorm * 255f);
            B = (int)(BNorm * 255f);
        }

        public float[] ToArray()
        {
            return new float[3]{ RNorm, GNorm, BNorm};
        }
    }
}