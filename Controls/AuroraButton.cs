using AuroraFramework.Drawing;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AuroraFramework.Controls
{
    [ToolboxBitmap(typeof(Button))]
    public class AuroraButton : Button
    {
        #region Properties
        /// <summary>
        /// 控件状态
        /// </summary>
        private AuroraControlStatus _ControlStatus = AuroraControlStatus.Default;

        private int _Radius = 0;
        /// <summary>
        /// 圆角弧度大小
        /// </summary>
        [Category("Aurora Style"), Description("圆角弧度大小")]
        public int Radius
        {
            get { return this._Radius; }
            set { this._Radius = value; }
        }

        private int _ContentMargin = 3;
        /// <summary>
        /// 内容与图片的边距
        /// </summary>
        [Category("Aurora Style"), Description("内容与图片的边距")]
        public int ContentMargin
        {
            get { return this._ContentMargin; }
            set { this._ContentMargin = value; }
        }

        /// <summary>
        /// 在按钮控件上的图像
        /// </summary>
        [Category("Aurora Style"), Description("在按钮控件上的图像")]
        public new Image Image
        {
            get { return base.Image; }
            set { base.Image = value; }
        }

        private Size _ImageSize = Size.Empty;
        /// <summary>
        /// 图片尺寸
        /// </summary>
        [Category("Aurora Style"), Description("图片尺寸")]
        public Size ImageSize
        {
            get
            {
                if (this._ImageSize == Size.Empty)
                    this._ImageSize = new Size(16, 16);
                return this._ImageSize;
            }
            set { this._ImageSize = value; }
        }

        /// <summary>
        /// 文本和图像相互之间的相对位置
        /// </summary>
        [Category("Aurora Style"), Description("文本和图像相互之间的相对位置")]
        public new TextImageRelation TextImageRelation
        {
            get { return base.TextImageRelation; }
            set { base.TextImageRelation = value; }
        }

        /// <summary>
        /// 是否将控件的元素对齐以支持使用从右向左的字体的区域设置
        /// </summary>
        [Category("Aurora Style"), Description("是否将控件的元素对齐以支持使用从右向左的字体的区域设置")]
        public new RightToLeft RightToLeft
        {
            get { return base.RightToLeft; }
            set { base.RightToLeft = value; }
        }

        private Color _DefaultControlColor = Color.Empty;
        /// <summary>
        /// 默认控件颜色
        /// </summary>
        [Category("Aurora Style"), Description("默认控件颜色")]
        public Color DefaultControlColor
        {
            get
            {
                if (this._DefaultControlColor == Color.Empty)
                    this._DefaultControlColor = Color.FromArgb(246, 247, 250);
                return this._DefaultControlColor;
            }
            set { this._DefaultControlColor = value; }
        }

        private Color _HoverControlColor = Color.Empty;
        /// <summary>
        /// 悬停控件颜色
        /// </summary>
        [Category("Aurora Style"), Description("悬停控件颜色")]
        public Color HoverControlColor
        {
            get
            {
                if (this._HoverControlColor == Color.Empty)
                    this._HoverControlColor = Color.FromArgb(67, 165, 220);
                return this._HoverControlColor;
            }
            set { this._HoverControlColor = value; }
        }

        private Color _PressedControlColor = Color.Empty;
        /// <summary>
        /// 按下控件颜色
        /// </summary>
        [Category("Aurora Style"), Description("按下控件颜色")]
        public Color PressedControlColor
        {
            get
            {
                if (this._PressedControlColor == Color.Empty)
                    this._PressedControlColor = Color.FromArgb(39, 88, 142);
                return this._PressedControlColor;
            }
            set { this._PressedControlColor = value; }
        }

        private Color _BorderColor = Color.Empty;
        /// <summary>
        /// 控件边框颜色
        /// </summary>
        [Category("Aurora Style"), Description("控件边框颜色")]
        public Color BorderColor
        {
            get
            {
                if (this._BorderColor == Color.Empty)
                    this._BorderColor = Color.FromArgb(182, 168, 192);
                return this._BorderColor;
            }
            set { this._BorderColor = value; }
        }

        [Browsable(false)]
        public new Color BackColor
        {
            get { return base.BackColor; }
        }

        [Browsable(false)]
        public new ContentAlignment TextAlign
        {
            get { return base.TextAlign; }
        }

        [Browsable(false)]
        public new ContentAlignment ImageAlign
        {
            get { return base.ImageAlign; }
        }
        #endregion

        /// <summary>
        /// 初始化<see cref ="AuroraButton" />类的新实例。
        /// </summary>
        public AuroraButton()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            this._ControlStatus = AuroraControlStatus.Default;
            this.Cursor = Cursors.Hand;
            base.TextImageRelation = TextImageRelation.ImageBeforeText;
            this.Size = new Size(100, 28);
            this.ResetRegion();
        }

        #region Override
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this._ControlStatus = AuroraControlStatus.Hover;
            this.Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                this._ControlStatus = AuroraControlStatus.Pressed;
                this.Invalidate();
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this._ControlStatus = AuroraControlStatus.Default;
            this.Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Left)
            {
                this._ControlStatus = AuroraControlStatus.Hover;
                this.Invalidate();
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Space)
            {
                this._ControlStatus = AuroraControlStatus.Pressed;
                this.Invalidate();
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            if (e.KeyCode == Keys.Space)
            {
                this._ControlStatus = AuroraControlStatus.Default;
                this.Invalidate();
                this.OnClick(e);
            }
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            this._ControlStatus = AuroraControlStatus.Hover;
            this.Invalidate();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            this._ControlStatus = AuroraControlStatus.Default;
            this.Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.ResetRegion();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.ResetRegion();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            base.OnPaintBackground(e);
            this.ResetRegion();
            this.DrawBackGround(e.Graphics);
            this.DrawContent(e.Graphics);
        }

        #endregion

        /// <summary>
        /// 绘制背景和边框
        /// </summary>
        private void DrawBackGround(Graphics graphics)
        {
            AuroraGraphics.InitializeGraphics(graphics);
            Rectangle rect = new Rectangle(0, 0, this.Size.Width - 1, this.Size.Height - 1);

            switch (this._ControlStatus)
            {
                case AuroraControlStatus.Default:
                    if (this.FlatStyle != FlatStyle.Flat)
                    {
                        AuroraGraphics.FillRectangle(graphics, rect, this.DefaultControlColor, this.Radius);
                    }
                    break;
                case AuroraControlStatus.Hover:
                    AuroraGraphics.FillRectangle(graphics, rect, this.HoverControlColor, this.Radius);
                    break;
                case AuroraControlStatus.Pressed:
                    AuroraGraphics.FillRectangle(graphics, rect, this.PressedControlColor, this.Radius);
                    break;
            }

            //Draw Border
            rect.Width -= 1;
            rect.Height -= 1;
            AuroraGraphics.DrawPathBorder(graphics, rect, this.Radius, this.BorderColor, 1);
        }

        /// <summary>
        /// 绘制按钮的内容：图片和文字
        /// </summary>
        private void DrawContent(Graphics g)
        {
            this.CalculateRect(out Rectangle imageRect, out Rectangle textRect);
            if (this.Image != null)
            {
                AuroraGraphics.DrawImage(g, imageRect, this.Image,this.ImageSize);
            }

            Color forceColor = this.Enabled ? this.ForeColor : Color.FromArgb(159, 159, 159);
            TextRenderer.DrawText(g, this.Text, this.Font, textRect, forceColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        /// <summary>
        /// 计算图片和文字的区域
        /// </summary>
        /// <param name="imageRect">图片矩形区域</param>
        /// <param name="textRect">文本矩形区域</param>
        private void CalculateRect(out Rectangle imageRect, out Rectangle textRect)
        {
            imageRect = Rectangle.Empty;
            textRect = Rectangle.Empty;
            if (this.Image == null)
            {
                textRect = new Rectangle(0, 0, this.Width, this.Height);
                return;
            }

            Size textSize = TextRenderer.MeasureText(this.Text, this.Font);
            int textMaxWidth = this.Width - this.ImageSize.Width - this.ContentMargin - 1;
            int textWidth = textSize.Width >= textMaxWidth ? textMaxWidth : textSize.Width;
            int contentWidth = this.ContentMargin + this.ImageSize.Width + textWidth;
            switch (this.TextImageRelation)
            {
                case TextImageRelation.Overlay:
                    imageRect = new Rectangle((this.Width - this.ImageSize.Width) / 2, (this.Height - this.ImageSize.Height) / 2, this.ImageSize.Width, this.ImageSize.Height);
                    textRect = new Rectangle(1, 1, this.Width - this.ContentMargin - 1, this.Height);
                    break;
                case TextImageRelation.ImageAboveText:
                    imageRect = new Rectangle((this.Width - this.ImageSize.Width) / 2, (this.Height-this.ImageSize.Height-textSize.Height)/2, this.ImageSize.Width, this.ImageSize.Height);
                    textRect = new Rectangle(1, imageRect.Bottom, this.Width - this.ContentMargin - 1, this.Height - imageRect.Bottom);
                    break;
                case TextImageRelation.ImageBeforeText:
                    imageRect = new Rectangle((this.Width - contentWidth) / 2, (this.Height - this.ImageSize.Height) / 2, this.ImageSize.Width, this.ImageSize.Height);
                    textRect = new Rectangle(imageRect.Right + this.ContentMargin, 1, textWidth, this.Height);
                    break;
                case TextImageRelation.TextAboveImage:
                    imageRect = new Rectangle((this.Width - this.ImageSize.Width) / 2, (this.Height - textSize.Height) / 2, this.ImageSize.Width, this.ImageSize.Height);
                    textRect = new Rectangle(1, (this.Height - this.ImageSize.Height - textSize.Height) / 2 - textSize.Height, this.Width - this.ContentMargin - 1, this.Height - imageRect.Y);   
                    break;
                case TextImageRelation.TextBeforeImage:
                    imageRect = new Rectangle((this.Width + contentWidth) / 2 - this.ImageSize.Width, (this.Height - this.ImageSize.Height) / 2, this.ImageSize.Width, this.ImageSize.Height);
                    textRect = new Rectangle((this.Width - contentWidth) / 2, 1, textWidth, this.Height);
                    break;
            }

            if (this.RightToLeft == RightToLeft.Yes)
            {
                imageRect.X = this.Width - imageRect.Right;
                textRect.X = this.Width - textRect.Right;
            }
        }

        /// <summary>
        /// 设置与控件关联的窗口区域
        /// </summary>
        private void ResetRegion()
        {
            if (this.Radius > 0)
            {
                Rectangle rect = new Rectangle(Point.Empty, this.Size);

                if (this.Region != null)
                {
                    this.Region.Dispose();
                }

                System.Drawing.Drawing2D.GraphicsPath graphicsPath = AuroraGraphics.GetGraphicsBezierPath(rect, this.Radius);

                this.Region = new Region(graphicsPath);
            }
        }
    }
}
