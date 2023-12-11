using System.Collections.Generic;
using System.Numerics;

namespace LSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            BackColor = Color.Black;
            DoubleBuffered = true;
            this.timer1.Interval = 10;
            this.timer1.Tick += Tick;
            this.timer1.Start();
        }


        private void Tick(object? sender, EventArgs e)
        {
            if (mStep == mVertexes.Count + 1)
            {
                return;
            }
            mStep++;
            Invalidate();
        }

        int mStep = 2;
        List<PointF> mVertexes = new List<PointF>();


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (mVertexes.Count < 2)
            {
                return;
            }
            var pts = mVertexes.Take(mStep).ToArray();
            e.Graphics.FillRectangle(Brushes.Yellow, new RectangleF(pts.First(), new SizeF(3, 3)));
            e.Graphics.DrawLines(Pens.WhiteSmoke, pts);
            e.Graphics.FillRectangle(Brushes.Red, new RectangleF(pts.Last(), new SizeF(3, 3)));
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Space)
            {
                mStep = mVertexes.Count;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                mStep = 2;
            }
        }

        private void curve1_Click(object sender, EventArgs e)
        {
            mStep = 2;
            LRule er = new LRule(LRuleType.EdgeRule,5, 90);
            string[] rules = new string[]
            {
                "L:+R-LL-R+",
                "R:-L+RR+L-"
            };
            mVertexes = er.Exec(rules, 'L', 6).Select(v => new PointF(v.X, v.Y)).ToList();
            Invalidate();
        }

        private void curve2_Click(object sender, EventArgs e)
        {
            mStep = 2;
            LRule er = new LRule(LRuleType.NodeRule,5, 90);
            string[] rules = new string[]
            {
                "A:+X",
                "X:XFYFX-F-YFXFY+F+XFYFX",
                "Y:YFXFY+F+XFYFX-F-YFXFY"
            };
            mVertexes = er.Exec(rules, 'A', 5).Select(v => new PointF(v.X, v.Y)).ToList();
            Invalidate();
        }

        private void curve3_Click(object sender, EventArgs e)
        {
            mStep = 2;
            LRule er = new LRule(LRuleType.NodeRule,5, 90);
            string[] rules = new string[]
            {
                "X:XFXF+YFYFY+F+YF-XFX-FYF-XFXFXFX-FYFYFY+",
                "Y:-XFXFXF+YFYFYFY+FXF+YFY+FX-F-XFXFX-FYFY"
            };
            mVertexes = er.Exec(rules, 'X', 3).Select(v => new PointF(v.X, v.Y)).ToList();
            Invalidate();
        }

        private void curve4_Click(object sender, EventArgs e)
        {
            mStep = 2;
            LRule er = new LRule(LRuleType.NodeRule,5, 90);
            string[] rules = new string[]
            {
                "X:+YFYFYF-XFX-FYFYFY+F+YFYFYF-XFX-FYFYFY+",
                "Y:-XFXFXF+YFY+FXFXFX-F-XFXFXF+YFY+FXFXFX-"
            };
            mVertexes = er.Exec(rules, 'X', 3).Select(v => new PointF(v.X, v.Y)).ToList();
            Invalidate();
        }

        private void curve5_Click(object sender, EventArgs e)
        {
            mStep = 2;
            LRule er = new LRule(LRuleType.EdgeRule,5, 90);
            string[] rules = new string[]
            {
                "L:LL+R+R-L-L+R+RL-R-LLR+L-R-LL-R+LR+R+L-L-RR+",
                "R:-LL+R+R-L-LR-L+RR+L+R-LRR+L+RL-L-R+R+L-L-RR"
            };
            mVertexes = er.Exec(rules, 'L', 2).Select(v => new PointF(v.X, v.Y)).ToList();
            Invalidate();
        }

        private void curve6_Click(object sender, EventArgs e)
        {
            mStep = 2;
            LRule er = new LRule(LRuleType.EdgeRule,5, 90, 400, 600);
            string[] rules = new string[]
            {
                "X:F-F-F-F",
                "F:F-F+F+FF-F-F+F",
            };
            mVertexes = er.Exec(rules, 'X', 3).Select(v => new PointF(v.X, v.Y)).ToList();
            Invalidate();
        }

        private void curve7_Click(object sender, EventArgs e)
        {
            mStep = 2;
            LRule er = new LRule(LRuleType.EdgeRule,5, 90, 0, 500);
            string[] rules = new string[]
            {
                "X:F-F-F-F",
                "F:FF-F-F-F-FF"
            };
            mVertexes = er.Exec(rules, 'X', 4).Select(v => new PointF(v.X, v.Y)).ToList();
            Invalidate();
        }

        private void curve8_Click(object sender, EventArgs e)
        {
            mStep = 2;
            LRule er = new LRule(LRuleType.EdgeRule,5, 90, 500, 500);
            string[] rules = new string[]
            {
                "X:F-F-F-F",
                "F:FF-F+F-F-FF"
            };
            mVertexes = er.Exec(rules, 'X', 4).Select(v => new PointF(v.X, v.Y)).ToList();
            Invalidate();
        }

        private void curve9ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mStep = 2;
            LRule er = new LRule(LRuleType.EdgeRule,5, -90, 500, 0);
            string[] rules = new string[]
            {
                "X:F-F-F-F",
                "F:FF-F--F-F"
            };
            mVertexes = er.Exec(rules, 'X', 4).Select(v => new PointF(v.X, v.Y)).ToList();
            Invalidate();
        }

        private void curve10ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mStep = 2;
            LRule er = new LRule(LRuleType.EdgeRule,5, 60, 300, 200);
            string[] rules = new string[]
            {
                "L:L+R++R-L--LL-R+",
                "R:-L+RR++R+L--L-R"
            };
            mVertexes = er.Exec(rules, 'L', 3).Select(v => new PointF(v.X, v.Y)).ToList();
            Invalidate();
        }
        private void dragonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mStep = 2;
            LRule er = new LRule(LRuleType.EdgeRule,5, 90, 800, 200);
            string[] rules = new string[]
            {
                "F:F+G",
                "G:F-G"
            };
            mVertexes = er.Exec(rules, 'G', 12).Select(v => new PointF(v.X, v.Y)).ToList();
            Invalidate();
        }

        private void triangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mStep = 2;
            LRule er = new LRule(LRuleType.EdgeRule,10, 60, 0, 600);
            string[] rules = new string[]
            {
                "A:B-A-B",
                "B:A+B+A"
            };
            mVertexes = er.Exec(rules, 'B', 5).Select(v => new PointF(v.X, v.Y)).ToList();
            Invalidate();
        }

        private void triangle2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mStep = 2;
            LRule er = new LRule(LRuleType.EdgeRule,20, 120, 0, 600);
            string[] rules = new string[]
            {
                "A:F-F-F",
                "F:F-G+F+G-F",
                "G:GG"
            };
            mVertexes = er.Exec(rules, 'A', 5).Select(v => new PointF(v.X, v.Y)).ToList();
            Invalidate();
        }

    }
}