using Draw.src.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Text.Json;

namespace Draw
{
	/// <summary>
	/// Върху главната форма е поставен потребителски контрол,
	/// в който се осъществява визуализацията
	/// </summary>
	/// 


	public partial class MainForm : Form
	{
		/// <summary>
		/// Агрегирания диалогов процесор във формата улеснява манипулацията на модела.
		/// </summary>
		private DialogProcessor dialogProcessor = new DialogProcessor();
        private List<Shape> ShapeLists = new List<Shape>();
		private byte risuva = 0;
		private List<Shape> buf =  DialogProcessor.selectionlist;

     

        public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
            this.KeyPreview = true; // This is important!
            this.KeyDown += new KeyEventHandler(Form1_Key);

            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
        }

        /// <summary>
        /// Изход от програмата. Затваря главната форма, а с това и програмата.
        /// </summary>
        /// 
		
        private void Form1_Key(object sender, KeyEventArgs e)
        {
			
            if (e.Control && e.KeyCode == Keys.A)
            {
                MessageBox.Show("You pressed Ctrl + A");
                e.Handled = true; 
				var ShapeList = dialogProcessor.ShapeList;

				for (int i = ShapeList.Count - 1; i >= 0; i--)
				{

					if (ShapeList[i].Selected == false)
					{
						ShapeList[i].FillColor = Color.Red;
						ShapeList[i].Selected = true;

						dialogProcessor.Selection = ShapeList[i];
                    }

                }
                viewPort.Invalidate();


            }
			
            if (e.Control && e.KeyCode == Keys.C)
            {
                MessageBox.Show("You pressed Ctrl + C");
                e.Handled = true;  
                buf = DialogProcessor.selectionlist;
            }
            if (e.Control && e.KeyCode == Keys.V)
            {
                MessageBox.Show("You pressed Ctrl + V");
                e.Handled = true; 
				Shape v;
				for (int i = 0; i < buf.Count; i++)
				{
				
					
				}
                viewPort.Invalidate();

            }


            if (e.Control && e.KeyCode == Keys.D)
            {
                MessageBox.Show("You pressed Ctrl + D");
                e.Handled = true;  
                buf = DialogProcessor.selectionlist;
                for (int i = 0; i < buf.Count; i++)
                {
					if(buf[i] != null)
					dialogProcessor.ShapeList.Remove(buf[i]);

                }
                viewPort.Invalidate();
            }

        }



        void ExitToolStripMenuItemClick(object sender, EventArgs e)
		{
			Close();
		}

		/// <summary>
		/// Събитието, което се прихваща, за да се превизуализира при изменение на модела.
		/// </summary>
		void ViewPortPaint(object sender, PaintEventArgs e)
		{
			dialogProcessor.ReDraw(sender, e);
		}

		/// <summary>
		/// Бутон, който поставя на произволно място правоъгълник със зададените размери.
		/// Променя се лентата със състоянието и се инвалидира контрола, в който визуализираме.
		/// </summary>
		void DrawRectangleSpeedButtonClick(object sender, EventArgs e)
		{
			dialogProcessor.AddRandomRectangle();

			statusBar.Items[0].Text = "Последно действие: Рисуване на правоъгълник";

			viewPort.Invalidate();
		}


		/// <summary>
		/// Прихващане на координатите при натискането на бутон на мишката и проверка (в обратен ред) дали не е
		/// щракнато върху елемент. Ако е така то той се отбелязва като селектиран и започва процес на "влачене".
		/// Промяна на статуса и инвалидиране на контрола, в който визуализираме.
		/// Реализацията се диалогът с потребителя, при който се избира "най-горния" елемент от екрана.
		/// </summary>
		void ViewPortMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (pickUpSpeedButton.Checked)
			{

				if (risuva == 1) risuva = 2;


				dialogProcessor.Selection = (dialogProcessor.ContainsPoint(e.Location));
				//	label1.Text = dialogProcessor.broi;
				if (dialogProcessor.Selection != null)
				{
					statusBar.Items[0].Text = "Последно действие: Селекция на примитив";
					dialogProcessor.IsDragging = true;
					dialogProcessor.LastLocation = e.Location;
					viewPort.Invalidate();
				}
			}
		}

		/// <summary>
		/// Прихващане на преместването на мишката.
		/// Ако сме в режм на "влачене", то избрания елемент се транслира.
		/// </summary>
		void ViewPortMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{

			if (risuva == 2)
			{
				dialogProcessor.AddPoints(e.Location);
				viewPort.Invalidate();

			}

			if (dialogProcessor.IsDragging)
			{

				if (dialogProcessor.Selection != null) statusBar.Items[0].Text = "Последно действие: Влачене";
				dialogProcessor.TranslateTo(e.Location);
				viewPort.Invalidate();

			}



		}

		/// <summary>
		/// Прихващане на отпускането на бутона на мишката.
		/// Излизаме от режим "влачене".
		/// </summary>
		void ViewPortMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			dialogProcessor.IsDragging = false;

			if (risuva == 2) risuva = 1;


		}

		private void MainForm_Load(object sender, EventArgs e)
		{

		}

		private void viewPort_Load(object sender, EventArgs e)
		{

		}

		private void pickUpSpeedButton_Click(object sender, EventArgs e)
		{
			dialogProcessor.AddRandomElpisa();

			statusBar.Items[0].Text = "Последно действие: Рисуване на елипса";

			viewPort.Invalidate();
		}

		private void trackBar1_Scroll(object sender, EventArgs e)
		{
			for (int i = 0; i < DialogProcessor.selectionlist.Count; i++)
			{
                if (DialogProcessor.selectionlist[i] != null)
                    DialogProcessor.selectionlist[i].BorderSize = trackBar1.Value + 5;

				viewPort.Invalidate();


			}

		}

		private void toolStripButton2_Click(object sender, EventArgs e)
		{

		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "Binary files (*.bin)|*.bin|All files (*.*)|*.*";
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				ModelToFile(saveFileDialog.FileName);
			}
		}

		public void ModelToFile(string filename)
		{
			BinaryFormatter bin = new BinaryFormatter();

			using (FileStream fs = new FileStream(filename, FileMode.Create))
			{
				// Тук сериализираме
				bin.Serialize(fs, dialogProcessor.ShapeList);
			}
		}



		public void LoadModel(string filename)
		{
			BinaryFormatter formatter = new BinaryFormatter();

			using (FileStream fs = new FileStream(filename, FileMode.Open))
			{


				ShapeLists = (List<Shape>)formatter.Deserialize(fs);
			}
		}




		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Binary files (*.bin)|*.bin|All files (*.*)|*.*";
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				LoadModel(openFileDialog.FileName);
				dialogProcessor.ShapeList = ShapeLists;
				viewPort.Invalidate();
			}
		}

		private void toolStripButton3_Click(object sender, EventArgs e)
		{
			if (risuva == 0) { risuva = 1; statusBar.Items[0].Text = "Последно действие: Рисуване ON"; }
			else
			{
				risuva = 0;

				statusBar.Items[0].Text = "Последно действие: Рисуване OFF";
			}

		}

		private void toolStripButton4_Click(object sender, EventArgs e)
		{
			dialogProcessor.AddRandomLine();

			statusBar.Items[0].Text = "Последно действие: Рисуване на отсечка";

			viewPort.Invalidate();
		}

		private void label2_Click(object sender, EventArgs e)
		{

		}

		private void button1_Click(object sender, EventArgs e)
		{
			int g = int.Parse(textBox1.Text);



            for (int i = 0; i < DialogProcessor.selectionlist.Count; i++)
            {
				if (DialogProcessor.selectionlist[i] != null)
				{
					Graphics gr = viewPort.CreateGraphics();
					dialogProcessor.RotateShape(gr, DialogProcessor.selectionlist[i], g);
				}


            }

			

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            dialogProcessor.AddStar();

            statusBar.Items[0].Text = "Последно действие: Рисуване на Звезда";

            viewPort.Invalidate();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
			string c = textBox2.Text.Trim();
			try
			{

				// Try to convert input to a known color or HTML code
				Color color = ColorTranslator.FromHtml(c);


				buf = DialogProcessor.selectionlist;

				for (int i = 0; i < buf.Count; i++)
				{
					if (buf[i] != null)
						buf[i].FillColor = color;

					
                }

				for (int i = 0; i < buf.Count; i++)
				{
					dialogProcessor.ShapeList[i].Selected = false;
				}
                DialogProcessor.selectionlist.Clear();

                viewPort.Invalidate();


            }
            catch
            {
                MessageBox.Show("Грешно въведен цвят!");
            }

        }

        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            dialogProcessor.AddTriangle();

            statusBar.Items[0].Text = "Последно действие: Рисуване на триъгълник";

            viewPort.Invalidate();
        }
    }
}