using System.Numerics;

namespace cohe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Init();
            BackColor = Color.Black;
            DoubleBuffered = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += Tick;
            this.timer1.Start();
        }

        int depth = 0;
        int delta = 1;
        private void Tick(object? sender, EventArgs e)
        {
            Step(depth);
            depth += delta;

            if (depth == 6)
            {
                delta = -1;
            }
            else if (depth == 0)
            {
                delta = 1;
            }
        }

        Vector2 O;
        Vector2 A;
        Vector2 B;
        Vector2 C;
        float L = 600;

        List<Vector2> Vertexes = new List<Vector2>();
        public void Init()
        {
            O = new Vector2(L, L/2);
            A = O - new Vector2(L / 2, 0);
            B = O + new Vector2(L / 2, 0);
            Vector2 ab = B - A;
            Vector2 n = Vector2.Transform(ab, Matrix3x2.CreateRotation(MathF.PI / 2));
            C = O + n * MathF.Cos(MathF.PI / 6);

        }

        void Step(int depth)
        {
            Vertexes.Clear();
            Vertexes.Add(A);
            Vertexes.AddRange(Cohe(A, B, depth));
            Vertexes.Add(B);
            Vertexes.AddRange(Cohe(B, C, depth));
            Vertexes.Add(C);
            Vertexes.AddRange(Cohe(C, A, depth));
            Vertexes.Add(A);
            Invalidate();
        }

        List<Vector2> Cohe(Vector2 a,Vector2 b, int depth)
        {
            List<Vector2> ans = new List<Vector2>();
            if (depth == 0)
            {
                return ans;
            }
            Vector2 ab = b - a;
            if (ab.LengthSquared() < 5)
            {
                return ans;
            }
            Vector2 c = a + ab / 3;
            ans.AddRange(Cohe(a, c,depth-1));
            ans.Add(c);
            Vector2 d = a + ab * 2 / 3;
            Vector2 m = a + ab / 2;
            Vector2 cd = d - c;
            Vector2 n = Vector2.Transform(cd, Matrix3x2.CreateRotation(MathF.PI / 2));
            Vector2 e = m - n * MathF.Cos(MathF.PI / 6);
            ans.AddRange(Cohe(c, e,depth-1));
            ans.Add(e);
            ans.AddRange(Cohe(e, d,depth-1));
            ans.Add(d);
            ans.AddRange(Cohe(d, b,depth-1));
            return ans;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (Vertexes.Count == 0)
            {
                return;
            }
            var pts = Vertexes.Select(v => new PointF(v.X, v.Y)).ToArray();
            e.Graphics.DrawLines(Pens.WhiteSmoke, pts);
        }


    }
}