using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Draw.src.Model
{
    [Serializable]
    internal class triangle:Shape
    {
        public triangle(RectangleF rect) : base(rect)
        {
        }

        public triangle(RectangleShape rectangle) : base(rectangle)
        {
        }
        public override bool Contains(PointF point)
        {
            if (base.Contains(point))
              
                return true;
            else
                return false;
        }
        public override void DrawSelf(Graphics grfx)
        {
            base.DrawSelf(grfx);

         
            float offsetX = Rectangle.X;
            float offsetY = Rectangle.Y;

          
            PointF topLeft = new PointF(0 + offsetX, 0 + offsetY);       
            PointF topRight = new PointF(102 + offsetX, 0 + offsetY);    
            PointF bottom = new PointF(52 + offsetX, 102 + offsetY);    
            PointF center = new PointF(52 + offsetX, 42 + offsetY);    

           
            PointF[] face1 = { topLeft, topRight, center };
            PointF[] face2 = { topLeft, center, bottom };
            PointF[] face3 = { topRight, center, bottom };

            Brush faceBrush = Brushes.Gold;
            Pen borderPen = Pens.Black;

        
            grfx.FillPolygon(faceBrush, face1);
            grfx.DrawPolygon(borderPen, face1);

            grfx.FillPolygon(faceBrush, face2);
            grfx.DrawPolygon(borderPen, face2);

            grfx.FillPolygon(faceBrush, face3);
            grfx.DrawPolygon(borderPen, face3);

         
        }
    }
}
