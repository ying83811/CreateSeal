using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormTest
{
    public class CreateParty : System.ComponentModel.Component
    {
        private Color _color = Color.Black;
        private Color _borderColor = Color.Transparent;
        private float _borderWidth = 1f;

        private GraphicsPath _graphicsPath = null;

        protected GraphicsPath cCP = new GraphicsPath(
         new PointF[] {
        new PointF(365F, 6F),
        new PointF(531F, 54F),
        new PointF(596F, 133F),
        new PointF(622F, 250F),
        new PointF(637F, 336F),
        new PointF(627F, 412F),
        new PointF(573F, 486F),
        new PointF(323F, 234F),
        new PointF(416F, 140F),
        new PointF(376F, 100F),
        new PointF(358F, 101F),
        new PointF(343F, 118F),
        new PointF(258F, 118F),
        new PointF(88F, 288F),
        new PointF(183F, 384F),
        new PointF(248F, 320F),
        new PointF(490F, 563F),
        new PointF(408F, 629F),
        new PointF(317F, 629F),
        new PointF(210F, 583F),
        new PointF(165F, 560F),
        new PointF(134F, 537F),
        new PointF(93F, 484F),
        new PointF(37F, 539F),
        new PointF(76F, 578F),
        new PointF(67F, 591F),
        new PointF(26F, 585F),
        new PointF(-9F, 620F),
        new PointF(11F, 676F),
        new PointF(27F, 704F),
        new PointF(42F, 718F),
        new PointF(81F, 713F),
        new PointF(105F, 709F),
        new PointF(125F, 676F),
        new PointF(126F, 640F),
        new PointF(137F, 631F),
        new PointF(199F, 685F),
        new PointF(246F, 713F),
        new PointF(342F, 720F),
        new PointF(431F, 724F),
        new PointF(492F, 711F),
        new PointF(576F, 651F),
        new PointF(649F, 725F),
        new PointF(731F, 640F),
        new PointF(655F, 566F),
        new PointF(703F, 491F),
        new PointF(718F, 451F),
        new PointF(719F, 354F),
        new PointF(720F, 243F),
        new PointF(635F, 22F),
        new PointF(379F, 6F)
       },
         new System.Byte[] {
          0,
          3,
          3,
          3,
          3,
          3,
          3,
          1,
          1,
          1,
          1,
          1,
          1,
          1,
          1,
          1,
          1,
          3,
          3,
          3,
          3,
          3,
          3,
          1,
          1,
          1,
          3,
          3,
          3,
          3,
          3,
          3,
          3,
          3,
          3,
          1,
          3,
          3,
          3,
          3,
          3,
          3,
          1,
          1,
          1,
          3,
          3,
          3,
          3,
          3,
          131});

        private float _width = 100f;
        private float _height = 100f;
        private PointF _location = new PointF(0, 0);

        public float Width
        {
            get { return this._width; }
            set { this._width = value; }
        }

        public System.Drawing.PointF Location
        {
            get { return this._location; }
            set { this._location = value; }
        }

        public float Height
        {
            get { return this._height; }
            set { this._height = value; }
        }

        public System.Drawing.Drawing2D.GraphicsPath GraphicsPath
        {
            get
            {
                this._graphicsPath = this.RetrieveGraphicsPath();
                return this._graphicsPath;
            }
            set { this._graphicsPath = value; }
        }

        public System.Drawing.Color Color
        {
            get { return this._color; }
            set { this._color = value; }
        }

        public float BorderWidth
        {
            get { return this._borderWidth; }
            set { this._borderWidth = value; }
        }

        public System.Drawing.Color BorderColor
        {
            get { return this._borderColor; }
            set { this._borderColor = value; }
        }

        private GraphicsPath RetrieveGraphicsPath()
        {
            GraphicsPath gp = new GraphicsPath();
            gp.FillMode = FillMode.Alternate;
            gp.AddPath(this.cCP, false);

            LogoHelper lh = new LogoHelper();
            gp
            lh.DestRectF = new RectangleF(this._location, new SizeF(this._width, this._height));
            lh.SrcGP = gp;
            GraphicsPath gpResult = lh.RetrievePath();
            gp.Dispose();

            return gpResult;
        }

        public CreateParty()
        {
            this.InitializeGraphics();
        }

        private void InitializeGraphics()
        {
        }

        public virtual void RenderGraphics(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            g.FillPath(new SolidBrush(this._color), this.GraphicsPath);
            g.DrawPath(new Pen(this._borderColor, this._borderWidth), this.GraphicsPath);
        }

        // Required to dispose of created resources
        private void DisposeGraphics()
        {
            this.cCP.Dispose();
            if (this._graphicsPath != null) this._graphicsPath.Dispose();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.DisposeGraphics();
            }
           }


        //调用：
        private void button1_Click(object sender, System.EventArgs e)
        {
            int width = 400;
            int height = 400;
            m_Bitmap = new Bitmap(width, height);
            g = Graphics.FromImage(m_Bitmap);
            g.Clear(Color.White);
            PartyLogoA partyLogoA = new PartyLogoA();
            partyLogoA.Width = width;
            partyLogoA.Height = height;
            partyLogoA.Color = Color.Red;
            partyLogoA.RenderGraphics(g);
            this.Invalidate();
        }
    }
}