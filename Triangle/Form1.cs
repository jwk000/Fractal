using System.Drawing.Drawing2D;

namespace Triangle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Init();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawSierpinskiTriangle(e.Graphics, A, B, C);
        }

        PointF A;
        PointF B;
        PointF C;
        //绘制 谢尔宾斯三角形
        void Init()
        {
            float triHeight = ClientSize.Height - 100;
            float siderLen = (float)(triHeight / Math.Sin(Math.PI / 3));
            A = new PointF(ClientSize.Width / 2, 50);
            B = new PointF(A.X - siderLen / 2, A.Y + triHeight);
            C = new PointF(B.X + siderLen, B.Y);

            Invalidate();
        }
        void DrawSierpinskiTriangle(Graphics g, PointF A, PointF B, PointF C)
        {
            if (A.X - B.X < 10) return;

            PointF a = new PointF((A.X + B.X) / 2, (A.Y + B.Y) / 2);
            PointF b = new PointF((A.X + C.X) / 2, (A.Y + C.Y) / 2);
            PointF c = new PointF((B.X + C.X) / 2, (B.Y + C.Y) / 2);

            GraphicsPath path = new GraphicsPath();
            path.AddLine(a, b);
            path.AddLine(b, c);
            path.AddLine(c, a);
            path.CloseAllFigures();
            g.FillRegion(Brushes.GreenYellow, new Region(path));

            DrawSierpinskiTriangle(g, A, a, b);
            DrawSierpinskiTriangle(g, a, B, c);
            DrawSierpinskiTriangle(g, b, c, C);
        }
    }
}