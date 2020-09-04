using AuroraFramework.Drawing;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AuroraFramework.Controls
{
    [ToolboxBitmap(typeof(System.Windows.Forms.Panel))]
    public class AuroraPanel : Panel
    {
        #region Properties
        /// <summary>
        /// 圆角弧度大小
        /// </summary>
        private int _Radius;
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

        /// <summary>
        /// 是否无边框
        /// </summary>
        private AuroraRoundStyle _RoundeStyle;
        [Category("Aurora Style"), Description("圆角的样式"), DefaultValue(true)]
        public AuroraRoundStyle RoundeStyle
        {
            get { return this._RoundeStyle; }
            set { this._RoundeStyle = value; base.Invalidate(); }
        }

        /// <summary>
        /// 边框颜色
        /// </summary>
        private Color _BorderColor;
        [Category("Aurora Style"), Description("边框颜色")]
        public Color BorderColor
        {
            get { return this._BorderColor; }
            set { this._BorderColor = value; base.Invalidate(); }
        }
        #endregion

        public AuroraPanel() : base()
        {
            SetStyle(ControlStyles.UserPaint | 
                ControlStyles.AllPaintingInWmPaint | 
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.DoubleBuffer |
                ControlStyles.ResizeRedraw | 
                ControlStyles.SupportsTransparentBackColor, true);

            this.Radius = 10;
            this.RoundeStyle = AuroraRoundStyle.All;
            this.BorderColor = Color.LightGray;
        }
        
        #region Override
        protected override void OnPaint(PaintEventArgs e)
        {
            int width = base.Width - base.Margin.Left - base.Margin.Right;
            int height = base.Height - base.Margin.Top - base.Margin.Bottom;
            Rectangle rec = new Rectangle(base.Margin.Left, base.Margin.Top, width, height);
            AuroraGraphics.DrawBorder(e.Graphics, rec, this.RoundeStyle, this.Radius,this.BorderColor);
        }

        protected override void OnResize(EventArgs e)
        {
            base.Refresh();
        }
        #endregion
    }
}
