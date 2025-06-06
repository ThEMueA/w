using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw.src.Model
{
    [Serializable]
    internal class Star:Shape
    {
        public Star(RectangleF rect) : base(rect)
        {
        }

        public Star(RectangleShape rectangle) : base(rectangle)
        {
        }
        public override bool Contains(PointF point)
        {
            if (base.Contains(point))
                // Проверка дали е в обекта само, ако точката е в обхващащия правоъгълник.
                // В случая на правоъгълник - директно връщаме true
                return true;
            else
                // Ако не е в обхващащия правоъгълник, то неможе да е в обекта и => false
                return false;
        }
        public override void DrawSelf(Graphics grfx)
        {
            base.DrawSelf(grfx);


            Point[] Points = new Point[] { new Point(10, 201), new Point(51, 201), new Point(75, 170), new Point(100, 200), new Point(141, 200), new Point(110, 230), new Point(119, 260), new Point(75, 230), new Point(30, 260), new Point(40, 230) };
            for (int i = 0; i < Points.Length; i++)
            {
                Points[i].X += 200;
                Points[i].Y += 100;
            }

            grfx.DrawPolygon(Pens.Gold, Points);
            grfx.FillPolygon(new SolidBrush(Color.Gold), Points);
        }

    }
}
