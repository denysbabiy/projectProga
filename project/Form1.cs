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
            //g = pictureBox1.CreateGraphics();
            p = new Pen(Color.Black, 3);
            gr = new Pen(Color.Gray, 0.5F);
            numOfCells = pictureBox1.Width;
            

        }
        
       

        

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
            g.DrawEllipse(p, cursor.X - 5, cursor.Y - 5, 5, 5);
            points[k++] = new Point(cursor.X, cursor.Y);
           
            listBox1.Items.Add("X:" + cursor.X + " Y:" + cursor.Y);
            if (k == 4)
            {
                drawPoligon(points);
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
            Console.WriteLine( intersect_check(points[0], points[1], points[2], points[3]));
            Console.WriteLine(intersect_check(points[0], points[3], points[1], points[2]));
            checkAndSwap(points);
            g.DrawPolygon(p, points);
            Array.Clear(points, 0, 4);
            k = 0;
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

        

        

        private void pictureBox1_Resize(object sender, EventArgs e)
        {
            
            g = pictureBox1.CreateGraphics();
            listBox1.Items.Clear();
            Array.Clear(points, 0, 4);
            k = 0;
            checkBoxGrid.Checked = false;
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {


            
            
            
        }

        private void checkBoxGrid_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxGrid.Checked == true)
            {
                drawGrid(numOfCells, cellSize);
            }
            else
            {
                g.Clear(Color.White);
            }
        }
    }
}
