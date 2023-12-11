using System.Numerics;

namespace Hilbert
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Init();
            BackColor = Color.Black;
            DoubleBuffered = true;
            this.timer1.Interval = 10;
            this.timer1.Tick += Tick;
            this.timer1.Start();
        }


        private void Tick(object? sender, EventArgs e)
        {
            mStep++;
            if (mStep == Vertexes.Count)
            {
                //HilbertX(5);
                //Hilbert();
                mStep = 2;
            }
            Invalidate();
        }

        int mStep = 2;
        float mLen = 10;
        int mDir = 1;//right
        Vector2 mPos = new Vector2(100, 100);
        List<int> mOp = new List<int>();//0左转 1右转 2移动
        List<Vector2> Vertexes = new List<Vector2>();
        public void Init()
        {
            HilbertX(6);
            Hilbert();
        }
        
        /*
         * 用L系统的节点置换，得到Hilbert曲线代码：
         * 角度：δ=90°
         * 初始元：X
         * 生成元1：X→+YF-XFX-FY+
         * 生成元2：Y→-XF+YFY+FX-
         */
        void Hilbert()
        {
            Vertexes.Add(mPos);
            foreach (int op in mOp)
            {
                if (op == 0)
                {
                    TurnL();
                }
                else if (op == 1)
                {
                    TurnR();
                }
                else
                {
                    Move();
                    Vertexes.Add(mPos);
                }
            }
        }

        void HilbertX( int depth)
        {
            if (depth == 0)
            {
                return ;
            }

            mOp.Add(0);
            HilbertY(depth - 1);
            mOp.Add(2);
            mOp.Add(1);
            HilbertX(depth - 1);
            mOp.Add(2);
            HilbertX(depth - 1);
            mOp.Add(1);
            mOp.Add(2);
            HilbertY(depth - 1);
            mOp.Add(0);
        }
        void HilbertY(int depth)
        {
            if (depth == 0)
            {
                return;
            }
            mOp.Add(1);
            HilbertX(depth - 1);
            mOp.Add(2);
            mOp.Add(0);
            HilbertY(depth - 1);
            mOp.Add(2);
            HilbertY(depth - 1);
            mOp.Add(0);
            mOp.Add(2);
            HilbertX(depth - 1);
            mOp.Add(1);
        }
        //0 up 1 right 2 down 3 left
        int Turn(int dir, bool leftRotate)
        {
            if (leftRotate)
            {
                dir = dir - 1;
                if (dir == -1) { dir = 3; }
            }
            else
            {
                dir = dir + 1;
                if (dir == 4) { dir = 0; }
            }
            return dir;
        }
        void TurnL() { mDir = Turn(mDir, true); }
        void TurnR() { mDir = Turn(mDir, false); }
        void Move() { mPos = Go(mPos, mDir, mLen); }
        Vector2 Go(Vector2 from,int dir,float len)
        {
            if (dir == 0)
            {
                return new Vector2(from.X, from.Y + len);
            }
            if (dir == 1)
            {
                return new Vector2(from.X + len, from.Y);
            }
            if (dir == 2)
            {
                return new Vector2(from.X, from.Y - len);
            }
            if (dir == 3)
            {
                return new Vector2(from.X - len, from.Y); 
            }
            return Vector2.Zero;
        }



        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (Vertexes.Count == 0)
            {
                return;
            }
            var pts = Vertexes.Take(mStep).Select(v => new PointF(v.X, v.Y)).ToArray();
            e.Graphics.DrawLines(Pens.WhiteSmoke, pts);
            e.Graphics.FillRectangle(Brushes.Red, new RectangleF(pts.Last(), new SizeF(3, 3)));
        }


    }
}