using System.Numerics;

namespace Julia
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            BackColor = Color.Black;
            ClientSize = new Size(1024, 768);
            DoubleBuffered = true;

            //ShowRectangle(-2, 2, -2, 2, -0.8, 0.2);
            ShowRectangle(-2, 2, -2, 2, 0.285, 0.01);
            //ShowRectangle(-2, 2, -2, 2, 0.285, 0);

            //this.timer1.Tick += Tick;
            //this.timer1.Start();
        }

        private void Tick(object? sender, EventArgs e)
        {
            mReal -= 0.001;
            mImag -= 0.001;

            ShowRectangle();
        }

        int mWidth = 1024;
        int mHeight = 768;
        double mMinX = -1.7;
        double mMaxX = 1.5;
        double mMinY = -1.3;
        double mMaxY = 1.2;
        double mReal = -0.7;
        double mImag = 0.27;
        double mThrehold = 0xff;
        Bitmap mBitmap;
        void ShowRectangle(double a, double b, double c, double d, double real, double imag)
        {
            mReal = real;
            mImag = imag;
            ShowRectangle();
        }

        void ShowRectangle()
        {
            mBitmap = new Bitmap(mWidth, mHeight);
            Complex c = new Complex(mReal, mImag);
            double deltaY = (mMaxY - mMinY) / mHeight;
            double deltaX = (mMaxX - mMinX) / mWidth;
            double delta = Math.Max(deltaX, deltaY);
            for (int x = 0; x < mWidth; x++)
            {
                for (int y = 0; y < mHeight; y++)
                {
                    double real = mMinX + delta * x;
                    double imag = mMinY + delta * y;
                    Complex z = new Complex(real, imag);
                    Color color = Julia(z, c);
                    mBitmap.SetPixel(x, y, color);
                }
            }
            Invalidate();
        }

        //一个点z=(i,j)经过depth迭代z=z^2+c后发散到无穷则点z属于julia集合
        //集合里的点标记为黑色，否则根据发散速度染色
        Color Julia(Complex z, Complex c)
        {
            for (int i = 0; i < 256; i++)
            {
                if (z.Magnitude > mThrehold)
                {
                    return Color.FromArgb(20, i, i);
                }
                z = z * z + c;
            }
            return Color.FromArgb(20, 255, 255);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (mBitmap == null)
            {
                return;
            }
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
                mMinX = -1.7;
                mMaxX = 1.5;
                mMinY = -1.3;
                mMaxY = 1.2;

                ShowRectangle();
            }
        }

        int mSelectState = 0;//0未选择 1选择中 
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