using Draw.src.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Security;

namespace Draw
{
    /// <summary>
    /// Класът, който ще бъде използван при управляване на диалога.
    /// </summary>
   [Serializable]
    public class DialogProcessor : DisplayProcessor
	{
		#region Constructor
		
		public DialogProcessor()
		{
		}

        #endregion

        #region Properties

        /// <summary>
        /// Избран елемент.
        /// </summary>
        /// 

        public  static List<Shape> selectionlist = new List<Shape>();
  


        private Shape selection;
		public Shape Selection {
			get { return selection;  }
			set { selection = value;
				if(selectionlist.Contains(selection)) 
				selectionlist.Remove(selection);
				else 
			    selectionlist.Add(selection);
				//broi = " "+selectionlist.Count;
			}
		}
		
		/// <summary>
		/// Дали в момента диалога е в състояние на "влачене" на избрания елемент.
		/// </summary>
		private bool isDragging;
	//	public string broi = "haha";
		public bool IsDragging {
			get { return isDragging; }
			set { isDragging = value; }
		}
		
		/// <summary>
		/// Последна позиция на мишката при "влачене".
		/// Използва се за определяне на вектора на транслация.
		/// </summary>
		private PointF lastLocation;
		public PointF LastLocation {
			get { return lastLocation; }
			set { lastLocation = value; }
		}
		
		#endregion
		
		/// <summary>
		/// Добавя примитив - правоъгълник на произволно място върху клиентската област.
		/// </summary>
		public void AddRandomRectangle()
		{
			Random rnd = new Random();
			int x = rnd.Next(100,1000);
			int y = rnd.Next(100,600);
			
			RectangleShape rect = new RectangleShape(new Rectangle(x,y,100,200));
			rect.FillColor = Color.White;
            rect.BorderColor = Color.Magenta;
			rect.BorderSize = 10;
            ShapeList.Add(rect);
		}

        public void AddStar()
        {


			Star rect = new Star(new Rectangle(210,300,100,90));
            ShapeList.Add(rect);
        }
        public void AddTriangle()
        {


            triangle rect = new triangle(new Rectangle(210, 300, 100, 90));
            ShapeList.Add(rect);
        }



        public void AddRandomElpisa()
        {
            Random rnd = new Random();
            int x = rnd.Next(100, 1000);
            int y = rnd.Next(100, 600);

            Elipsa rect = new Elipsa(new Rectangle(x, y, 100, 200));
            rect.FillColor = Color.White;
            rect.FillColor = Color.White;
            rect.BorderColor = Color.Magenta;
            rect.BorderSize = 10;
            ShapeList.Add(rect);
        }

        

        public void AddRandomPoint()
        {
            Random rnd = new Random();
            int x = rnd.Next(100, 1000);
            int y = rnd.Next(100, 600);

            PointS rect = new PointS(new Point (x, y));
            rect.FillColor = Color.White;
            rect.FillColor = Color.White;
            rect.BorderColor = Color.Magenta;
            rect.BorderSize = 30;
            ShapeList.Add(rect);
        }
        public void AddPoints(Point A)
        {
           
            int x = A.X;
			int y = A.Y;

            PointS rect = new PointS(new Point(x, y));
            rect.FillColor = Color.White;
            rect.FillColor = Color.White;
            rect.BorderColor = Color.Magenta;
            rect.BorderSize = 30;
            ShapeList.Add(rect);
        }

        public void AddRandomLine()
        {
            Random rnd = new Random();
            int x = rnd.Next(100, 1000);
            int y = rnd.Next(100, 600);

            LineS rect = new LineS(new Point(x, y), new Point(x+100, y+7));
            rect.FillColor = Color.White;
            rect.FillColor = Color.White;
            rect.BorderColor = Color.Magenta;
            rect.BorderSize = 50;
            ShapeList.Add(rect);
        }

        /// <summary>
        /// Проверява дали дадена точка е в елемента.
        /// Обхожда в ред обратен на визуализацията с цел намиране на
        /// "най-горния" елемент т.е. този който виждаме под мишката.
        /// </summary>
        /// <param name="point">Указана точка</param>
        /// <returns>Елемента на изображението, на който принадлежи дадената точка.</returns>

        public Shape ContainsPoint(PointF point)
		{
			for(int i = ShapeList.Count - 1; i >= 0; i--){
				if (ShapeList[i].Contains(point)){

					if (ShapeList[i].Selected==false)
					{
						ShapeList[i].FillColor = Color.Red;
						ShapeList[i].Selected = true;

                    }
					else {
						
						ShapeList[i].FillColor = Color.White;
						ShapeList[i].Selected = false;
                    }

                    return ShapeList[i];
				}	
			}
			return null;
		}
		
		/// <summary>
		/// Транслация на избраният елемент на вектор определен от <paramref name="p>p</paramref>
		/// </summary>
		/// <param name="p">Вектор на транслация.</param>
		public void TranslateTo(PointF p)
		{


			if (selection != null) {


	        //  selection.Location = new PointF(selection.Location.X + p.X - lastLocation.X, selection.Location.Y + p.Y - lastLocation.Y);

					int c = selectionlist.Count();
				if (c > 0) {
					for (int i = 0; i < c; i++)
					{
                        if (selectionlist[i] != null)
                            selectionlist[i].Location= new PointF(selectionlist[i].Location.X + p.X - lastLocation.X, selectionlist[i].Location.Y + p.Y - lastLocation.Y);
                        
                    }
				}
			
		        lastLocation = p;
			

			}
		}
	}
}
