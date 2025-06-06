using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw.src.Model
{
    [Serializable]
    internal class LineS:Shape
    {
        public Point Start { get; set; }
        public Point End { get; set; }

        public LineS(Point start, Point end)
        {
            Start = start;
            End = end;
        }

        public override void DrawSelf(Graphics grfx)
        {
            grfx.DrawLine(Pens.Blue, Start, End);
        }
    }
}
