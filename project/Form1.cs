using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace project
{
    public partial class Form1 : Form
    {
        Graphics g;
        Bitmap bmp = new Bitmap(100,100);
        Pen p;
        Pen gr;
        Point cursor;
        int k = 0;
        

        Point[] points = new Point[4];
        int numOfCells = 0;
        int cellSize = 20;
        

        public Form1()
        {
            InitializeComponent();
            //g = Graphics.FromImage(bmp);
            SetSize();
            p = new Pen(Color.Black, 3);
            gr = new Pen(Color.Gray, 0.5F);
            numOfCells = pictureBox1.Width;
            

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
            drawGrid(numOfCells, cellSize);
            pictureBox1.Image = bmp;
            toolTipClearButton.SetToolTip(this.clearButton, "Clear to continue drawing");


        }
        private void SetSize()
        {
            Rectangle rectangle = Screen.PrimaryScreen.Bounds;
            bmp = new Bitmap(rectangle.Width, rectangle.Height);
            g = Graphics.FromImage(bmp);
        }





        private void pictureBox1_Click(object sender, EventArgs e)
        {
            g = Graphics.FromImage(bmp);
            g.DrawEllipse(p, cursor.X - 5, cursor.Y - 5, 5, 5);
            points[k++] = new Point(cursor.X, cursor.Y);
            pictureBox1.Image = bmp;
            listBox1.Items.Add("X:" + cursor.X + " Y:" + cursor.Y);

            if (k == 4)
            {
                perimetrfunc(points);
                square(points);
                drawPoligon(points);
                pictureBox1.Enabled = false;
                clearButton.BackColor = Color.Green;
                
                //pictureBox1.Image = bmp;
                
            }
            
            

        }
        public int area(Point a,Point b,Point c)
        {
            return (b.X - a.X) * (c.Y - a.Y) - (b.Y - a.Y) * (c.X - a.X);
        }
        public bool intersect_1(int a, int b, int c, int d)
        {
            if (a > b)
            {
                var temp = a;
                a = b;
                b = temp;
                
            }

            if (c > d) 
            {
                var temp2 = c;
                c = d;
                d = temp2;
            }
            return Math.Max(a, c) <= Math.Min(b, d);
        }
        public bool intersect_check(Point a, Point b, Point c, Point d)
        {
            return intersect_1(a.X, b.X, c.X, d.X)
                   && intersect_1(a.Y, b.Y, c.Y, d.Y)
                   && area(a, b, c) * area(a, b, d) <= 0
                   && area(c, d, a) * area(c, d, b) <= 0;
        }
        public void checkAndSwap(Point[] points)
        {
            if (intersect_check(points[0], points[1], points[2], points[3]))
            {

                var buffer = points[1];
                points[1] = points[3];
                points[3] = buffer;
            } 
            if(intersect_check(points[0], points[3], points[1], points[2]))
            {

                var buffer = points[2];
                points[2] = points[3];
                points[3] = buffer;
            }

        }
        public void drawPoligon(Point[] points)
        {
            g = Graphics.FromImage(bmp);
            Console.WriteLine( intersect_check(points[0], points[1], points[2], points[3]));
            Console.WriteLine(intersect_check(points[0], points[3], points[1], points[2]));
            
            checkAndSwap(points);
            g.DrawPolygon(p, points);
            pictureBox1.Image = bmp;
            //Dispose();
            Array.Clear(points, 0, 4);
            k = 0;
            
        }
        public double sideLenght(Point a,Point b)
        {
            double len;
            Point ab = new Point(b.X - a.X, b.Y - a.Y);
            
            len = Math.Sqrt(Math.Pow(ab.X, 2) + Math.Pow(ab.Y, 2));
            
            return len;
        }
        double perimetr;
        public void perimetrfunc(Point[] points)
        {
            perimetr = sideLenght(points[0], points[1]) + sideLenght(points[1], points[2]) +
                sideLenght(points[2], points[3]) + sideLenght(points[3], points[0]);
            
            labelPerimetr.Text = Convert.ToString(Math.Round(perimetr, 4))+ " pixels";
            
            
        }
        public void square(Point[] points)
        {

            double square = Math.Sqrt((perimetr / 2 - sideLenght(points[0], points[1]))*
                (perimetr / 2 - sideLenght(points[1], points[2]))*(perimetr / 2 - sideLenght(points[2], points[3]))*
                (perimetr / 2 - sideLenght(points[3], points[0])));
            labelSquare.Text = Convert.ToString(Math.Round(square, 4)) + " pixels^2";
        }
        public void drawGrid(int numOfsels,int cellSize)
        {
            
            for (int y = 0; y < numOfCells; ++y)
            {
                g.DrawLine(gr, 0, y * cellSize, numOfCells * cellSize, y * cellSize);
            }

            for (int x = 0; x < numOfCells; ++x)
            {
                g.DrawLine(gr, x * cellSize, 0, x * cellSize, numOfCells * cellSize);
            }
           
            
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            cursor = this.PointToClient(Cursor.Position);
            toolStripStatusLabel1.Text = "X:" + cursor.X + " Y:" + cursor.Y;

        }

        private void clearButton_Click(object sender, EventArgs e)
        {

            g.Clear(Color.White);
            drawGrid(numOfCells, cellSize);
            pictureBox1.Image = bmp;
            Array.Clear(points, 0, 4);
            k = 0;
            listBox1.Items.Clear();
            labelPerimetr.Text = "";
            labelSquare.Text = "";
            pictureBox1.Enabled = true;
            clearButton.BackColor = SystemColors.Control;



        }
    }
}
