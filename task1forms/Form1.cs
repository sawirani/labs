using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace task1forms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Line line = new Line(new Point(0, 0), new Point(100, 100));
            IDrawable drawable = new VisualLine(e.Graphics, line);

            drawable.Draw();

            Bezier bezier = new Bezier(new Point(100, 100), new Point(250, 10), new Point(25, 15), new Point(300, 400));
            IDrawable drawable2 = new VisualBezier(e.Graphics,bezier);

            drawable2.Draw();
        }
    }
}
