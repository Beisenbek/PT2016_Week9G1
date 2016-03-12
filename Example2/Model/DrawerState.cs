using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Example2.Model
{
    public enum Shape
    {
        Line,
        Circle,
        Rectangle,
        Pencil,
        Eraser
    }
    public enum DrawTool
    {
        Pen,
        Brush
    }

    public class DrawerState
    {
        public Pen pen = new Pen(Color.Red);
        public Bitmap bmp;
        Graphics g;
        GraphicsPath path;
        private PictureBox pictureBox1;

        public Point prevPoint;

        public DrawTool DrawTool { get; set; }
        public Shape Shape { get; set; }

        public void FixPath()
        {
            if (path != null)
            {
                g.DrawPath(pen, path);
                path = null;
            }
        }

        public DrawerState(PictureBox pictureBox1)
        {
            this.pictureBox1 = pictureBox1;
            
            Load("");

            DrawTool = DrawTool.Pen;
            Shape = Shape.Pencil;

            pictureBox1.Paint += PictureBox1_Paint;
        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (path != null)
            {
                e.Graphics.DrawPath(pen, path);
            }
        }

        public void Draw(Point currentPoint)
        {
            switch (Shape)
            {
                case Shape.Line:
                    path = new GraphicsPath();
                    path.AddLine(prevPoint, currentPoint);
                    break;
                case Shape.Circle:
                    break;
                case Shape.Rectangle:
                    break;
                case Shape.Pencil:
                    g.DrawLine(pen, prevPoint, currentPoint);
                    prevPoint = currentPoint;
                    break;
                case Shape.Eraser:
                    g.DrawLine(new Pen(Color.White,pen.Width), prevPoint, currentPoint);
                    break;
                default:
                    break;
            }

            pictureBox1.Refresh();
        }

        public void Save(string fileName)
        {
            bmp.Save(fileName);
        }

        public void Load(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                bmp = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            }
            else {
                bmp = new Bitmap(fileName);
            }

            g = Graphics.FromImage(bmp);
            pictureBox1.Image = bmp;
        }
    }
}
