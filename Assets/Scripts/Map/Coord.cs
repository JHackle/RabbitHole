namespace Hackle.Map
{
    [System.Serializable]
    public struct Coord
    {
        public int X;
        public int Y;

        public Coord(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public static bool operator ==(Coord c1, Coord c2f)
        {
            return c1.X == c2f.X && c1.Y == c2f.Y;
        }

        public static bool operator !=(Coord c1, Coord c2f)
        {
            return !(c1 == c2f);
        }

        public override bool Equals(object obj)
        {
            if (obj is Coord)
            {
                Coord other = (Coord)obj;
                if ((other.X == X) && (other.Y == Y))
                {
                    return true;
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}