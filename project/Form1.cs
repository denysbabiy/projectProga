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
        Point cursor;
        int k = 0;
        Point[] points = new Point[4];
        public Form1()
        {
            InitializeComponent();
            g = this.CreateGraphics();
            p = new Pen(Color.Black, 3);

        }

       

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            cursor = this.PointToClient(Cursor.Position);
            toolStripStatusLabel1.Text = "X:" + cursor.X + " Y:" + cursor.Y;
        }

        

        private void Form1_Click(object sender, EventArgs e)
        {
            g.DrawEllipse(p, cursor.X-5, cursor.Y-5 , 5, 5);
            points[k++] = new Point(cursor.X, cursor.Y);
            
            listBox1.Items.Add("X:" + cursor.X + " Y:" + cursor.Y);
            if (k == 4)
            {
                g.DrawPolygon(p, points);
            }
            
        }
    }
}
