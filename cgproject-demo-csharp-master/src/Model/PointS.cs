using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw.src.Model
{
    [Serializable]
    internal class PointS:Shape
    {

        public Point Position { get; set; }

        public PointS(Point p)
        {
            Position = p;
        }

        public override void DrawSelf(Graphics grfx)
        {
            grfx.FillEllipse(Brushes.Red, Position.X - 3, Position.Y - 3, 6, 6);
        }

       
    }
}
