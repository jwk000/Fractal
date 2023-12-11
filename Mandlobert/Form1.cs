using System.Numerics;

namespace Mandlobert
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            BackColor = Color.Black;
            DoubleBuffered = true;
            ClientSize = new Size(1024, 768);
            //ShowRectangle();
            //ShowRectangle(0.275, 0.28, 0.006, 0.01);
            ShowRectangle(-0.09, -0.086, 0.654, 0.657);
        }
        int mWidth = 1024;
        int mHeight = 768;
        double mMinX = -2.5;
        double mMinY = -1.5;
        double mMaxX = 1;
        double mMaxY = 1.5;

        Bitmap mBitmap;
        void ShowRectangle(double a, double b, double c, double d)
        {
            mMinX = a;
            mMaxX = b;
            mMinY = c;
            mMaxY = d;
            ShowRectangle();
        }
        void ShowRectangle()
        {
            mBitmap = new Bitmap(mWidth, mHeight);
            double deltaX = (mMaxX - mMinX) / mWidth;
            double deltaY = (mMaxY - mMinY) / mHeight;
            double delta = Math.Max(deltaX, deltaY);
            for (int x = 0; x < mWidth; x++)
            {
                for (int y = 0; y < mHeight; y++)
                {
                    double real = mMinX + delta * x;
                    double imag = mMinY + delta * y;
                    Complex c = new Complex(real, imag);
                    Color color = Mandelbrot(c);
                    mBitmap.SetPixel(x, y, color);
                }
            }
            Invalidate();
        }

        //һ����c=(i,j)����depth����c=c^2+c��ɢ���������c���ڼ���
        //������ĵ���Ϊ��ɫ��������ݷ�ɢ�ٶ�Ⱦɫ
        Color Mandelbrot(Complex c)
        {
            Complex x = c;
            for (int i = 0; i < 256; i++)
            {
                if (x.Magnitude > 0xff)
                {
                    return Color.FromArgb(Math.Min((int)(i * 1.5), 255), i, 0);
                }
                x = x * x + c;
            }
            return Color.FromArgb(255, 255, 0);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawImage(mBitmap, 0, 0);
            if (mSelectState == 1)
            {
                e.Graphics.DrawRectangle(Pens.White, mSelectRect);
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Escape)
            {
                mMinX = -2.5;
                mMinY = -1.5;
                mMaxX = 1;
                mMaxY = 1.5;
                ShowRectangle();
            }
        }

        int mSelectState = 0;//0δѡ�� 1ѡ���� 
        Rectangle mSelectRect = new Rectangle();
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            mSelectState = 1;
            mSelectRect.X = e.X;
            mSelectRect.Y = e.Y;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (mSelectState == 1)
            {
                mSelectRect.Width = e.X - mSelectRect.X;
                mSelectRect.Height = e.Y - mSelectRect.Y;
                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (mSelectState == 1)
            {
                mSelectState = 0;
                mSelectRect.Width = e.X - mSelectRect.X;
                mSelectRect.Height = e.Y - mSelectRect.Y;

                double left = mSelectRect.Left * 1.0 / mWidth * (mMaxX - mMinX) + mMinX;
                double right = mSelectRect.Right * 1.0 / mWidth * (mMaxX - mMinX) + mMinX;
                double top = mSelectRect.Top * 1.0 / mHeight * (mMaxY - mMinY) + mMinY;
                double bottom = mSelectRect.Bottom * 1.0 / mHeight * (mMaxY - mMinY) + mMinY;
                mMinX = left;
                mMaxX = right;
                mMinY = top;
                mMaxY = bottom;
                ShowRectangle();
            }
        }
    }


}