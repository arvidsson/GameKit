using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityLib.Spatial
{
    /// <summary>
    /// Represents a hex coordinate.
    /// </summary>
    [System.Serializable]
    public struct Hex
    {   
        public readonly int q;
        public readonly int r;
        public readonly int s;

        public Hex(int q, int r, int s)
        {
            this.q = q;
            this.r = r;
            this.s = s;

            if (q + r + s != 0)
            {
                throw new ArgumentException("Hex: (q + r + s) must be 0");
            }
        }

        public Hex(int q, int r)
        {
            this.q = q;
            this.r = r;
            this.s = -q - r;

            if (q + r + s != 0)
            {
                throw new ArgumentException("Hex: (q + r + s) must be 0");
            }
        }

        // overridden so we can use Hex as a key in a Dictionary
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Hex o = (Hex)obj;
            if ((System.Object)o == null)
            {
                return false;
            }

            return ((q == o.q) && (r == o.r) && (s == o.s));
        }

        // overridden so we can use Hex as a key in a Dictionary
        public override int GetHashCode()
        {
            return (q.GetHashCode() ^ (r.GetHashCode() + (int)(Mathf.Pow(2, 32) / (1 + Mathf.Sqrt(5)) / 2) + (q.GetHashCode() << 6) + (q.GetHashCode() >> 2)));
        }

        /// <summary>
        /// Returns a string representation of the Hex coordinates.
        /// </summary>
        public override string ToString()
        {
            return string.Format("[" + q + "," + r + "," + s + "]");
        }

        /// <summary>
        /// Returns a new Hex object created from a string.
        /// </summary>
        public static Hex FromString(string hexString)
        {
            // remove the surrounding []
            var str = hexString.Substring(1, hexString.Length - 2);
            var values = str.Split(',');

            int q = int.Parse(values[0]);
            int r = int.Parse(values[1]);
            int s = int.Parse(values[2]);

            Hex h = new Hex(q, r, s);
            return h;
        }

        public static Hex Add(Hex a, Hex b)
        {
            return new Hex(a.q + b.q, a.r + b.r, a.s + b.s);
        }

        public static Hex operator +(Hex a, Hex b)
        {
            return Add(a, b);
        }

        public static Hex Subtract(Hex a, Hex b)
        {
            return new Hex(a.q - b.q, a.r - b.r, a.s - b.s);
        }

        public static Hex operator -(Hex a, Hex b)
        {
            return Subtract(a, b);
        }

        public static Hex Scale(Hex h, int k)
        {
            return new Hex(h.q * k, h.r * k, h.s * k);
        }

        // flat: N, NE, SE, S, SW, NW
        public static List<Hex> directions = new List<Hex> { new Hex(0, 1, -1), new Hex(1, 0, -1), new Hex(1, -1, 0), new Hex(0, -1, 1), new Hex(-1, 0, +1), new Hex(-1, 1, 0) };

        public static Hex Direction(int direction)
        {
            return directions[direction];
        }

        public static Hex Neighbour(Hex h, int direction)
        {
            return Add(h, Direction(direction));
        }

        public static List<Hex> Neighbours(Hex h)
        {
            List<Hex> neighbours = new List<Hex>();

            for (int i = 0; i < 6; i++)
            {
                var neighbour = Neighbour(h, i);
                neighbours.Add(neighbour);
            }

            return neighbours;
        }

        public static List<Hex> diagonals = new List<Hex> { new Hex(2, -1, -1), new Hex(1, -2, 1), new Hex(-1, -1, 2), new Hex(-2, 1, 1), new Hex(-1, 2, -1), new Hex(1, 1, -2) };

        public static Hex DiagonalNeighbor(Hex h, int direction)
        {
            return Add(h, diagonals[direction]);
        }

        public static int Length(Hex h)
        {
            return (int)((Math.Abs(h.q) + Math.Abs(h.r) + Math.Abs(h.s)) / 2);
        }

        public static int Distance(Hex a, Hex b)
        {
            return Length(Subtract(a, b));
        }
    }
}