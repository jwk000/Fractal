using System.Collections.Generic;
using System.Numerics;
using LSystem;

namespace Plant
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            BackColor = Color.Black;
            DoubleBuffered = true;
            WindowState = FormWindowState.Maximized;
            this.timer1.Interval = 10;
            this.timer1.Tick += Tick;
            this.timer1.Start();
        }


        private void Tick(object? sender, EventArgs e)
        {
            if (mStep == mStepCount)
            {
                return;
            }
            mStep++;
            Invalidate();
        }

        int mStep = 0;
        int mStepCount = 0;
        LRule mRule = null;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (mRule == null) return;

            mRule.DrawStep(e.Graphics, mStep);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Space)
            {
                mStep = mStepCount;
                Invalidate();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                mStep = 0;
            }
        }

        private void plant1_Click(object sender, EventArgs e)
        {
            mRule = new LRule(LRuleType.EdgeRule, 5, 25, 600, 900, -90);
            string[] rules = new string[]
            {
                "X:F+[[X]-X]-F[-FX]+X",
                "F:FF"
            };
            mStepCount = mRule.Parse(rules, 'X', 5);
            mStep = 0;
            Invalidate();
        }

        private void plant2_Click(object sender, EventArgs e)
        {
            mRule = new LRule(LRuleType.EdgeRule, 5, 22.5f, 600, 900, -90);
            string[] rules = new string[]
            {
                "F:FF-[XY]+[XY]",
                "X:+FY",
                "Y:-FX"
            };
            mStepCount = mRule.Parse(rules, 'F', 5);
            mStep = 0;
            Invalidate();
        }



        private void plant3_Click(object sender, EventArgs e)
        {
            mRule = new LRule(LRuleType.EdgeRule, 16, 25f, 600, 900, -90);
            string[] rules = new string[]
            {
                "F:F[+F][-F[-F]F]F[+F][-F]",
            };
            mStepCount = mRule.Parse(rules, 'F', 4);
            mStep = 0;
            Invalidate();
        }

        private void plant4_Click(object sender, EventArgs e)
        {
            mRule = new LRule(LRuleType.EdgeRule, 5, 25.7f, 600, 900, -90);
            string[] rules = new string[]
            {
                "F:F[+F]F[-F]F",
            };
            mStepCount = mRule.Parse(rules, 'F', 5);
            mStep = 0;
            Invalidate();
        }

        private void plant5_Click(object sender, EventArgs e)
        {
            mRule = new LRule(LRuleType.EdgeRule, 10, 20, 600, 900, -90);
            string[] rules = new string[]
            {
                "F:F[+F]F[-F][F]",
            };
            mStepCount = mRule.Parse(rules, 'F', 4);
            mStep = 0;
            Invalidate();
        }

        private void plant6_Click(object sender, EventArgs e)
        {
            mRule = new LRule(LRuleType.EdgeRule, 10, 22.5f, 600, 900, -90);
            string[] rules = new string[]
            {
                "F:FF-[-F+F+F]+[+F-F-F]",
            };
            mStepCount = mRule.Parse(rules, 'F', 4);
            mStep = 0;
            Invalidate();
        }

        private void plant7_Click(object sender, EventArgs e)
        {
            mRule = new LRule(LRuleType.EdgeRule, 5, 25.7f, 600, 900, -90);
            string[] rules = new string[]
            {
                "F:FF",
                "X:F[+X][-X]FX"
            };
            mStepCount = mRule.Parse(rules, 'X', 5);
            mStep = 0;
            Invalidate();
        }

        private void plant8_Click(object sender, EventArgs e)
        {
            mRule = new LRule(LRuleType.EdgeRule, 5, 22.5f, 600, 900, -90);
            string[] rules = new string[]
            {
                "F:FF",
                "X:F-[[X]+X]+F[+FX]-X"
            };
            mStepCount = mRule.Parse(rules, 'X', 5);
            mStep = 0;
            Invalidate();
        }
    }
}