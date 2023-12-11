namespace Tree
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
            DrawFratalTree(e.Graphics);
        }

        //��򵥵ķ�����
        float pecent = 0.618f;//���ɺ���֦�ĳ��ȱ���
        float firstTall = 500;//��һ�����ɵĳ���
        double rotate = Math.PI / 3;//��ת60��
        void Init()
        {
            firstTall = (ClientSize.Height - 100) * 0.4f;
            Invalidate();
        }
        Pen linePen = new Pen(Color.YellowGreen, 2);
        void DrawFratalTree(Graphics g)
        {
            PointF a = new PointF(ClientSize.Width * 0.5f, ClientSize.Height - 50);
            PointF b = new PointF(a.X, a.Y - firstTall);
            g.DrawLine(linePen, a, b);
            RealDrawFratalTree(g, a, b, firstTall);
        }

        //����AB����baselen
        void RealDrawFratalTree(Graphics g, PointF A, PointF B, float baselen)
        {
            if (baselen < 1) return;
            float sublen = baselen * pecent;
            PointF C = CalcPointOnlineByOffset(A, B, sublen);
            PointF p1 = PointRotate(B, C, rotate);
            PointF p2 = PointRotate(B, C, -rotate);

            g.DrawLine(linePen, B, p1);
            g.DrawLine(linePen, B, p2);

            RealDrawFratalTree(g, B, p1, sublen);
            RealDrawFratalTree(g, B, p2, sublen);
        }

        //���C��λ��AB�ӳ��߷���
        private PointF CalcPointOnlineByOffset(PointF A, PointF B, float len)
        {
            float Cx, Cy;

            //���Ʒ�
            Cx = B.X + (B.X - A.X) * 0.618f;
            Cy = B.Y + (B.Y - A.Y) * 0.618f;

            return new PointF(Cx, Cy);
        }

        /// <summary>  
        /// �����ĵ���תAngle�Ƕ�  
        /// </summary>  
        /// <param name="center">���ĵ�</param>  
        /// <param name="p1">����ת�ĵ�</param>  
        /// <param name="angle">��ת�Ƕȣ����ȣ�</param>  
        private PointF PointRotate(PointF center, PointF p1, double angle)
        {
            double x1 = (p1.X - center.X) * Math.Cos(angle) + (p1.Y - center.Y) * Math.Sin(angle) + center.X;
            double y1 = -(p1.X - center.X) * Math.Sin(angle) + (p1.Y - center.Y) * Math.Cos(angle) + center.Y;
            return new PointF((float)x1, (float)y1);
        }
    }
}