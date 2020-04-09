using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace task1forms
{

    interface IPoint {
        double X { get; set; }
        double Y { get; set; }
    }

    interface ICurve {
        void GetPoint(double t, out IPoint p);
    }

    class Point:IPoint{
        double x;
        double y;

        public Point(double x, double y) {
            this.x = x;
            this.y = y;
        }

        public double X { get => x; set => x = value; }
        public double Y { get => y; set => y = value; }
    }

    abstract class ACurve : ICurve
    {
        protected IPoint a;
        protected IPoint b;

        public abstract void GetPoint(double t, out IPoint p);
        protected ACurve(IPoint a, IPoint b) {
            this.a = a;
            this.b = b;
        }

    }

    class Line : ACurve {

        public Line(IPoint a,IPoint b) :base(a,b) {}
  
        public override void GetPoint(double t, out IPoint p)
        {
            Point tmp = new Point(0, 0);
            tmp.X = (1 - t) * a.X + t * b.X;
            tmp.Y = (1 - t) * a.Y + t * b.Y;
            p = tmp;
        }
    }

    class Bezier : ACurve
    {
        private IPoint c;
        private IPoint d;

        public Bezier(IPoint a, IPoint b, IPoint c, IPoint d) : base(a, b) {
            this.c = c;
            this.d = d;
        }

        public override void GetPoint(double t, out IPoint p)
        {
            Point tmp = new Point(0, 0);
            tmp.X = Math.Pow((1 - t), 3) * a.X + 3 * t * Math.Pow((1 - t), 2) * c.X + 3 * Math.Pow(t, 2) * (1 - t) * d.X + Math.Pow(t, 3) * b.X;
            tmp.Y = Math.Pow((1 - t), 3) * a.Y + 3 * t * Math.Pow((1 - t), 2) * c.Y + 3 * Math.Pow(t, 2) * (1 - t) * d.Y + Math.Pow(t, 3) * b.Y;
            p = tmp;
        }
    }

    interface IDrawable {
        void Draw();
    }

    abstract class VisualCurve: IDrawable {
        protected Graphics g;
        protected ICurve curve;

        public VisualCurve(Graphics g, ICurve curve) {
            this.curve = curve;
            this.g = g;
        }

        public abstract void Draw();
    }

    class VisualLine: VisualCurve{

        public VisualLine(Graphics g, ICurve curve) : base(g, curve) { }

        public override void Draw()
        {
            Pen pen = new Pen(Color.Black, 3);
            IPoint FirstPoint;
            IPoint SecondPoint;
            curve.GetPoint(0, out FirstPoint);
            curve.GetPoint(1, out SecondPoint);
            PointF point1= new PointF(Convert.ToSingle(FirstPoint.X), Convert.ToSingle(FirstPoint.Y));
            PointF point2 = new PointF(Convert.ToSingle(SecondPoint.X), Convert.ToSingle(SecondPoint.Y));
            g.DrawLine(pen, point1, point2);
        }
    }

    class VisualBezier : VisualCurve
    {
        public VisualBezier(Graphics g, ICurve curve) : base(g, curve) { }

        public override void Draw()
        {
            Pen pen = new Pen(Color.Red, 5);
            curve.GetPoint(0, out IPoint FirstPoint);
            curve.GetPoint(0.33, out IPoint SecondPoint);
            curve.GetPoint(0.66, out IPoint ThirdPoint);
            curve.GetPoint(1, out IPoint FourthPoint);
            PointF point1 = new PointF(Convert.ToSingle(FirstPoint.X), Convert.ToSingle(FirstPoint.Y));
            PointF point2 = new PointF(Convert.ToSingle(SecondPoint.X), Convert.ToSingle(SecondPoint.Y));
            PointF point3 = new PointF(Convert.ToSingle(ThirdPoint.X), Convert.ToSingle(ThirdPoint.Y));
            PointF point4 = new PointF(Convert.ToSingle(FourthPoint.X), Convert.ToSingle(FourthPoint.Y));
            g.DrawBezier(pen, point1, point2, point3, point4);
        }
    }

    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
