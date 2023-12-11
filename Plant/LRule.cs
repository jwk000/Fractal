using System.Numerics;
using System.Drawing;
using System.Data;
using System.Runtime.Intrinsics.X86;

namespace LSystem
{
    /*
  Character        Meaning
   F             Move forward by line length drawing a line
   f             Move forward by line length without drawing a line
   +             Turn left by turning angle
   -             Turn right by turning angle
   |             Reverse direction (ie: turn by 180 degrees)
   [             Push current drawing state onto stack
   ]             Pop current drawing state from the stack
   #             Increment the line width by line width increment
   !             Decrement the line width by line width increment
   @             Draw a dot with line width radius
   {             Open a polygon
   }             Close a polygon and fill it with fill colour
   >             Multiply the line length by the line length scale factor
   <             Divide the line length by the line length scale factor
   &             Swap the meaning of + and -
   (             Decrement turning angle by turning angle increment
   )             Increment turning angle by turning angle increment
     */
    public enum LRuleType
    {
        EdgeRule,
        NodeRule
    }
    public class LRule
    {
        LRuleType mRuleType = LRuleType.NodeRule;
        float mLen = 10;
        float mLenScala = 1.5f;
        float mAngle = MathF.PI / 2;
        float mDeltaAngle = MathF.PI / 6;
        float mWidth = 1;
        float mDeltaWidth = 1;

        Vector2 mDir = new Vector2(1, 0);
        Vector2 mPos = new Vector2(50, 50);
        Pen mPen = Pens.LawnGreen;

        public LRule(LRuleType rt, float len, float angle, float offsetX = 0, float offsetY = 0, float rotate=0)
        {
            mRuleType = rt;
            mLen = len;
            mAngle = angle / 180 * MathF.PI;
            mPos.X += offsetX;
            mPos.Y += offsetY;
            mPen = new Pen(Color.LawnGreen, mWidth);
            mDir = Vector2.Transform(mDir, Matrix3x2.CreateRotation(rotate / 180 * MathF.PI));

            mInitContext.Pos = mPos;
            mInitContext.Dir = mDir;
        }

        

        enum OpNodeType
        {

            Left, //+
            Right,//-
            Reverse, //|
            Move, //F
            Jump, //f
            Push, //[
            Pop, //]
            DecAngle,//(
            IncAngle,//)
            SwapLR,//&
            Dot,//@
            IncWidth,//#
            DecWidth,//!
            MulLength,//>
            DivLength,//<
            Node,//ABCD...
            MoveNode,//ABCD...
        }
        class OpNode
        {
            public OpNodeType nodeType;
            public char rule;
            public OpNode(OpNodeType t, char r)
            {
                nodeType = t;
                rule = r;
            }
        }

        class Context
        {
            public Vector2 Dir;
            public Vector2 Pos;
        }
        Context mInitContext = new Context();
        Stack<Context> mOpStack = new Stack<Context>();

        Dictionary<char, List<OpNode>> mRuleDict = new Dictionary<char, List<OpNode>>();
        
        List<OpNode> mOpNodes = new List<OpNode>();

        bool mIsSwapLR = false;
        public List<Vector2> Exec(string[] rules, char x, int depth)
        {
            Parse(rules,x,depth);
            return Run();
        }

        public int Parse(string[] rules, char x, int depth)
        {
            foreach (string rule in rules)
            {
                char r = rule[0];
                var list = mRuleDict[r] = new List<OpNode>();
                for (int i = 2; i < rule.Length; i++)
                {
                    if (rule[i] == '+')
                    {
                        if (mIsSwapLR)
                        {
                            list.Add(new OpNode(OpNodeType.Right, rule[i]));
                        }
                        else
                        {
                            list.Add(new OpNode(OpNodeType.Left, rule[i]));
                        }
                    }
                    else if (rule[i] == '-')
                    {
                        if (mIsSwapLR)
                        {
                            list.Add(new OpNode(OpNodeType.Left, rule[i]));
                        }
                        else
                        {
                            list.Add(new OpNode(OpNodeType.Right, rule[i]));
                        }
                    }
                    else if (rule[i] == '[')
                    {
                        list.Add(new OpNode(OpNodeType.Push, rule[i]));
                    }
                    else if (rule[i] == ']')
                    {
                        list.Add(new OpNode(OpNodeType.Pop, rule[i]));
                    }
                    else if (rule[i] == '|')
                    {
                        list.Add(new OpNode(OpNodeType.Reverse, rule[i]));
                    }
                    else if (rule[i] == '#')
                    {
                        list.Add(new OpNode(OpNodeType.IncWidth, rule[i]));
                    }
                    else if (rule[i] == '!')
                    {
                        list.Add(new OpNode(OpNodeType.DecWidth, rule[i]));
                    }
                    else if (rule[i] == '@')
                    {
                        list.Add(new OpNode(OpNodeType.Dot, rule[i]));
                    }
                    else if (rule[i] == '(')
                    {
                        list.Add(new OpNode(OpNodeType.DecAngle, rule[i]));
                    }
                    else if (rule[i] == ')')
                    {
                        list.Add(new OpNode(OpNodeType.IncAngle, rule[i]));
                    }
                    else if (rule[i] == '<')
                    {
                        list.Add(new OpNode(OpNodeType.DivLength, rule[i]));
                    }
                    else if (rule[i] == '>')
                    {
                        list.Add(new OpNode(OpNodeType.MulLength, rule[i]));
                    }
                    else if (rule[i] == '&')
                    {
                        mIsSwapLR = !mIsSwapLR;
                    }
                    else//node
                    {
                        if(mRuleType == LRuleType.EdgeRule)
                        {
                            list.Add(new OpNode(OpNodeType.MoveNode, rule[i]));
                        }
                        else
                        {
                            if (rule[i] == 'F')
                            {
                                list.Add(new OpNode(OpNodeType.Move, rule[i]));
                            }
                            else if (rule[i] == 'f')
                            {
                                list.Add(new OpNode(OpNodeType.Jump, rule[i]));
                            }
                            else
                            {
                                list.Add(new OpNode(OpNodeType.Node, rule[i]));
                            }
                        }
                    }
                }
            }

            mOpNodes = Iterate(x, depth);

            return mOpNodes.Count;
        }

        List<OpNode> Iterate(char rule, int depth)
        {
            List<OpNode> ans = new List<OpNode>();
            var nodes = mRuleDict[rule];
            foreach (var node in nodes)
            {
                if (node.nodeType == OpNodeType.Node)
                {
                    if (depth > 0)
                    {
                        ans.AddRange(Iterate(node.rule, depth - 1));
                    }
                }
                else if (node.nodeType == OpNodeType.MoveNode)
                {
                    if (depth == 0)
                    {
                        ans.Add(node);
                    }
                    else
                    {
                        ans.AddRange(Iterate(node.rule, depth - 1));
                    }
                }
                else
                {
                    ans.Add(node);
                }
            }
            return ans;
        }

        List<Vector2> Run()
        {
            List<Vector2> Vertexes = new List<Vector2>();

            Vertexes.Add(mPos);
            foreach (var node in mOpNodes)
            {
                switch (node.nodeType)
                {
                    case OpNodeType.Left:
                        TurnL();
                        break;
                    case OpNodeType.Right:
                        TurnR();
                        break;
                    case OpNodeType.Reverse:
                        TurnB();
                        break;
                    case OpNodeType.Move:
                        Forward();
                        Vertexes.Add(mPos);
                        break;
                    case OpNodeType.MoveNode:
                        Forward();
                        Vertexes.Add(mPos);
                        break;
                    case OpNodeType.DecAngle:
                        DecAngle();
                        break;
                    case OpNodeType.IncAngle:
                        IncAngle();
                        break;
                    case OpNodeType.MulLength:
                        mLen *= mLenScala;
                        break;
                    case OpNodeType.DivLength:
                        mLen /= mLenScala;
                        break;
                    case OpNodeType.Push:
                        mOpStack.Push(new Context() { Dir = mDir, Pos = mPos });
                        break;
                    case OpNodeType.Pop:
                        {
                            Context ctx = mOpStack.Pop();
                            mDir = ctx.Dir;
                            mPos = ctx.Pos;
                        }
                        break;
                    default:
                        break;
                }
            }
            return Vertexes;
        }

        void DrawNode(Graphics g, OpNode node)
        {
            switch (node.nodeType)
            {
                case OpNodeType.Left:
                    TurnL();
                    break;
                case OpNodeType.Right:
                    TurnR();
                    break;
                case OpNodeType.Reverse:
                    TurnB();
                    break;
                case OpNodeType.Move:
                    {
                        PointF a = new PointF(mPos.X, mPos.Y);
                        Forward();
                        PointF b = new PointF(mPos.X, mPos.Y);
                        g.DrawLine(mPen, a, b);
                    }
                    break;
                case OpNodeType.MoveNode:
                    {
                        PointF a = new PointF(mPos.X, mPos.Y);
                        Forward();
                        PointF b = new PointF(mPos.X, mPos.Y);
                        g.DrawLine(mPen, a, b);
                    }
                    break;
                case OpNodeType.Jump:
                    Forward();
                    break;
                case OpNodeType.DecAngle:
                    DecAngle();
                    break;
                case OpNodeType.IncAngle:
                    IncAngle();
                    break;
                case OpNodeType.IncWidth:
                    mWidth += mDeltaWidth; mPen.Width = mWidth;
                    break;
                case OpNodeType.DecWidth:
                    mWidth -= mDeltaWidth; mPen.Width = mWidth;
                    break;
                case OpNodeType.Dot:
                    g.DrawEllipse(mPen, mPos.X, mPos.Y, mWidth, mWidth);
                    break;
                case OpNodeType.MulLength:
                    mLen *= mLenScala;
                    break;
                case OpNodeType.DivLength:
                    mLen /= mLenScala;
                    break;
                case OpNodeType.Push:
                    Push();
                    break;
                case OpNodeType.Pop:
                    Pop();
                    break;
                default:
                    break;
            }
        }

        void InitContext()
        {
            mPos = mInitContext.Pos;
            mDir = mInitContext.Dir;
        }

        public void DrawStep(Graphics g ,int step)
        {
            InitContext();
            for (int i = 0; i < step; i++)
            {
                if (i < mOpNodes.Count)
                {
                    DrawNode(g, mOpNodes[i]);
                }
            }
        }
            
        public void Draw(Graphics g)
        {
            InitContext();
            foreach (var node in mOpNodes)
            {
                DrawNode(g, node);
            }
        }
        void TurnB() { mDir = -mDir; }
        void TurnL() { mDir = Vector2.Transform(mDir, Matrix3x2.CreateRotation(mAngle)); }
        void TurnR() { mDir = Vector2.Transform(mDir, Matrix3x2.CreateRotation(-mAngle)); }
        void DecAngle() { mAngle -= mDeltaAngle; }
        void IncAngle() { mAngle += mDeltaAngle; }
        void Forward() { mPos = mPos + mDir * mLen; }
        void Push() { mOpStack.Push(new Context() { Dir = mDir, Pos = mPos }); }
        void Pop()
        {
            Context ctx = mOpStack.Pop();
            mDir = ctx.Dir;
            mPos = ctx.Pos;
        }
    }
}