using AuroraFramework.Drawing;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AuroraFramework.Controls
{
    [ToolboxBitmap(typeof(System.Windows.Forms.Panel))]
    public class AuroraPanel : Panel
    {
        #region Properties
        private int _Radius = 8;
        /// <summary>
        /// 圆角弧度大小
        /// </summary>
        [Category("Aurora Style"), Description("圆角弧度大小"), DefaultValue(10)]
        public int Radius
        {
            get { return this._Radius; }
            set
            {
                this._Radius = value > 0 ? value : 0;
                base.Invalidate();
            }
        }

        private int _BorderWidth = 1;
        /// <summary>
        /// 边框宽度
        /// </summary>
        [Category("Aurora Style"), Description("边框宽度"), DefaultValue(10)]
        public int BorderWidth
        {
            get { return this._BorderWidth; }
            set
            {
                this._BorderWidth = value > 0 ? value : 0;
                base.Invalidate();
            }
        }

        private Color _BorderColor =  Color.Empty;
        /// <summary>
        /// 边框颜色
        /// </summary>
        [Category("Aurora Style"), Description("边框颜色")]
        public Color BorderColor
        {
            get
            {
                if (this._BorderColor == Color.Empty)
                    this._BorderColor = Color.LightGray;
                return this._BorderColor;
            }
            set { this._BorderColor = value; base.Invalidate(); }
        }

        private Color _BackgroundColorFrom = Color.Empty;
        /// <summary>
        /// 起始背景颜色
        /// </summary>
        [Category("Aurora Style"), Description("起始背景颜色")]
        public Color BackgroundColorFrom
        {
            get
            {
                if (this._BackgroundColorFrom == Color.Empty)
                    this._BackgroundColorFrom = SystemColors.Window;
                return this._BackgroundColorFrom;
            }
            set
            {
                this._BackgroundColorFrom = value;
                base.Invalidate();
            }
        }

        private Color _BackgroundColorTo = Color.Empty;
        /// <summary>
        /// 结束背景颜色
        /// </summary>
        [Category("Aurora Style"), Description("结束背景颜色")]
        public Color BackgroundColorTo
        {
            get {
                if (this._BackgroundColorTo == Color.Empty)
                    this._BackgroundColorTo = SystemColors.Window;
                return this._BackgroundColorTo; }
            set
            {
                this._BackgroundColorTo = value;
                base.Invalidate();
            }
        }

        [Browsable(false)]
        public new BorderStyle BorderStyle { get; set; }


        #endregion

        public AuroraPanel() : base()
        {
            this.SetStyle(ControlStyles.ResizeRedraw
                | ControlStyles.SupportsTransparentBackColor
                | ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.UpdateStyles();

            base.BackColor = Color.Transparent;

            this.Radius = 8;
            this.BorderWidth = 1;
            this.BorderColor = Color.LightGray;
        }

        #region Override
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            AuroraGraphics.InitializeGraphics(e.Graphics);
            AuroraGradientColor gradientColor = new AuroraGradientColor(this.BackgroundColorFrom, this.BackgroundColorTo, null, null);
            Rectangle rect = new Rectangle(0, 0, this.Size.Width - 1, this.Size.Height - 1);
            AuroraGraphics.FillRectangle(e.Graphics, rect, gradientColor, this.Radius);
            if (this.BorderWidth > 0)
            {
                rect.X += this.BorderWidth - 1;
                rect.Y += this.BorderWidth - 1;
                rect.Width -= this.BorderWidth - 1;
                rect.Height -= this.BorderWidth - 1;
                AuroraGraphics.DrawPathBorder(e.Graphics, rect, this.Radius, this.BorderColor, this.BorderWidth);
            }
        }
        #endregion
    }
}
