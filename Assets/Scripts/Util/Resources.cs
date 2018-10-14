using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hackle.Util
{
    public class Resources
    {
        public int Wood { get; set; }
        public int Food { get; set; }
        public int Gold { get; set; }

        public Resources(int w, int f, int g)
        {
            Wood = w;
            Food = f;
            Gold = g;
        }

        public override string ToString()
        {
            return "Wood:" + Wood + " Food:" + Food + " Gold:" + Gold;
        }
    }
}
