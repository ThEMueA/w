﻿using System;
using System.Collections.Generic;
using System.Drawing;

namespace Draw
{
	/// <summary>
	/// Базовия клас на примитивите, който съдържа общите характеристики на примитивите.
	/// </summary>
   [Serializable]
	public abstract class Shape
	{
        #region Constructors
       
        public Shape()
		{
		}
		
		public Shape(RectangleF rect)
		{
			rectangle = rect;
		}

        

        public Shape(Shape shape)
		{
			this.Height = shape.Height;
			this.Width = shape.Width;
			this.Location = shape.Location;
			this.rectangle = shape.rectangle;
			
			this.FillColor =  shape.FillColor;
			Selected = false;
		}
		#endregion
		
		#region Properties
		
		/// <summary>
		/// Обхващащ правоъгълник на елемента.
		/// </summary>
		private RectangleF rectangle;		
		public virtual RectangleF Rectangle {
			get { return rectangle; }
			set { rectangle = value; }
		}
		
		/// <summary>
		/// Широчина на елемента.
		/// </summary>
		public virtual float Width {
			get { return Rectangle.Width; }
			set { rectangle.Width = value; }
		}
		
		/// <summary>
		/// Височина на елемента.
		/// </summary>
		public virtual float Height {
			get { return Rectangle.Height; }
			set { rectangle.Height = value; }
		}
		
		/// <summary>
		/// Горен ляв ъгъл на елемента.
		/// </summary>
		public virtual PointF Location {
			get { return Rectangle.Location; }
			set { rectangle.Location = value; }
		}
		
		/// <summary>
		/// Цвят на елемента.
		/// </summary>
		private Color fillColor;		
		public virtual Color FillColor {
			get { return fillColor; }
			set { fillColor = value; }
		}
        private Color borderColor;
        public virtual Color BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; }
        }
        private int borderSize;
        public virtual int BorderSize
        {
            get { return borderSize; }
            set { borderSize= value; }
        }

        private bool selected;
		public virtual bool Selected
        {
            get { return selected; }
            set { selected = value; }
        }


        #endregion


        /// <summary>
        /// Проверка дали точка point принадлежи на елемента.
        /// </summary>
        /// <param name="point">Точка</param>
        /// <returns>Връща true, ако точката принадлежи на елемента и
        /// false, ако не пренадлежи</returns>
        public virtual bool Contains(PointF point)
		{
			return Rectangle.Contains(point.X, point.Y);
		}

        public static PointF RotatePoint(PointF point, PointF center, float angleDegrees)
        {
            double angleRad = angleDegrees * Math.PI / 180.0;

            float dx = point.X - center.X;
            float dy = point.Y - center.Y;
            float xNew = (float)(Math.Cos(angleRad) * dx - Math.Sin(angleRad) * dy + center.X);
            float yNew = (float)(Math.Sin(angleRad) * dx + Math.Cos(angleRad) * dy + center.Y);
            return new PointF(xNew, yNew);
        }

        /// <summary>
        /// Визуализира елемента.
        /// </summary>
        /// <param name="grfx">Къде да бъде визуализиран елемента.</param>
        public virtual void DrawSelf(Graphics grfx)
		{
			//shape.Rectangle.Inflate(shape.BorderWidth, shape.BorderWidth);
		}
        public virtual void Rotate(Graphics grfx, float g)
        {
            //shape.Rectangle.Inflate(shape.BorderWidth, shape.BorderWidth);
        }

    }
}
