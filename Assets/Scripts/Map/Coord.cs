namespace Hackle.Map
{
    [System.Serializable]
    public struct Coord
    {
        public int x;
        public int y;

        public Coord(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static bool operator ==(Coord c1, Coord c2f)
        {
            return c1.x == c2f.x && c1.y == c2f.y;
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
                if ((other.x == x) && (other.y == y))
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