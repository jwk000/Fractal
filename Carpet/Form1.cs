namespace Carpet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            BackColor = Color.Black;
        }

        float TopX = 100;
        float TopY = 100;
        float width = 500;
        float height = 400;
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawSierpinskiCarpet(e.Graphics, TopX, TopY, width, height);
        }

        //绘制 谢尔宾斯基地毯
        void DrawSierpinskiCarpet(Graphics g, float x, float y, float width, float height)
        {
            if (width < 10 || height < 10) return;

            float subwidth = width / 3;
            float subheight = height / 3;
            float subx = x + subwidth;
            float suby = y + subheight;

            g.FillRectangle(Brushes.CadetBlue, subx, suby, subwidth, subheight);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (i == 1 && j == 1) continue;
                    DrawSierpinskiCarpet(g, x + i * subwidth, y + j * subheight, subwidth, subheight);
                }
            }
        }
    }
}