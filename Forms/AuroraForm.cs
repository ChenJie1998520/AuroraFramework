using AuroraFramework.Drawing;
using AuroraFramework.Native;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AuroraFramework.Forms
{
    public class AuroraForm : System.Windows.Forms.Form
    {
        #region Properties

        #region Aurora Style
        private int _Radius = 0;
        /// <summary>
        /// 窗体圆角半径
        /// </summary>
        [Category("Aurora Style"), Description("窗体圆角半径")]
        public int Radius
        {
            get { return this._Radius; }
            set
            {
                if (value >= 0)
                    this._Radius = value;
                this.SetFormRoundRectRgn(this, this.Radius);
                this.Invalidate();
            }
        }

        private AuroraTitleBar _TitleBarStyle = null;
        /// <summary>
        /// 标题栏
        /// </summary>
        [Category("Aurora Style"), Description("标题栏")]
        [TypeConverter(typeof(AuroraTitleBarConverter))]
        [Editor(typeof(AuroraFramework.Drawing.Design.AuroraNoneEditStyleEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public AuroraTitleBar TitleBarStyle
        {
            get
            {
                if (this._TitleBarStyle == null)
                {
                    this._TitleBarStyle = new AuroraTitleBar();
                }
                return this._TitleBarStyle;
            }
            set
            {
                this._TitleBarStyle = value;
                base.Text = value.Title;
                SetFormPadding();
            }
        }

        private AuroraControlBox _ControlBoxStyle = null;
        /// <summary>
        /// 控制按钮
        /// </summary>
        [Category("Aurora Style"), Description("控制按钮")]
        [TypeConverter(typeof(AuroraControlBoxConverter))]
        [Editor(typeof(AuroraFramework.Drawing.Design.AuroraNoneEditStyleEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public AuroraControlBox ControlBoxStyle
        {
            get
            {
                if (this._ControlBoxStyle == null)
                    this._ControlBoxStyle = new AuroraControlBox();
                return this._ControlBoxStyle;
            }
            set
            {
                this._ControlBoxStyle = value;
                this.Invalidate(this.TitleBarRect);
            }
        }

        private List<AuroraCustomControlBox> _CustomControlBoxes = null;
        /// <summary>
        /// 自定义控制按钮集合
        /// </summary>
        [Category("Aurora Style"), Description("自定义控制按钮集合")]
        [TypeConverter(typeof(System.ComponentModel.CollectionConverter))]//指定类型装换器
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<AuroraCustomControlBox> CustomControlBoxes
        {
            get
            {
                if (this._CustomControlBoxes == null)
                    this._CustomControlBoxes = new List<AuroraCustomControlBox>();
                return this._CustomControlBoxes;
            }
            set
            {
                this._CustomControlBoxes = value;
                this.Invalidate(this.TitleBarRect);
            }
        }

        private AuroraGradientColor _BackgroundColor = null;
        /// <summary>
        /// 窗体背景颜色
        /// </summary>
        [Category("Aurora Style"), Description("窗体背景颜色")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [TypeConverter(typeof(AuroraGradientColorConverter))]
        [Editor(typeof(AuroraFramework.Drawing.Design.AuroraNoneEditStyleEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public AuroraGradientColor BackgroundColor
        {
            get
            {
                if (this._BackgroundColor == null)
                    this._BackgroundColor = new AuroraGradientColor();
                return this._BackgroundColor;
            }
            set { this._BackgroundColor = value; }
        }

        private AuroraFormBorderStyle _BorderStyle = AuroraFormBorderStyle.All;
        /// <summary>
        /// 窗体边框样式
        /// </summary>
        [Category("Aurora Style"), Description("窗体边框样式")]
        public AuroraFormBorderStyle BorderStyle
        {
            get { return this._BorderStyle; }
            set
            {
                this._BorderStyle = value; this.Invalidate();
            }
        }

        private Color _BorderColor = Color.Empty;
        /// <summary>
        /// 窗体边框颜色
        /// </summary>
        [Category("Aurora Style"), Description("窗体边框颜色")]
        public Color BorderColor
        {
            get
            {
                if (this._BorderColor.IsEmpty)
                    this._BorderColor = Color.LightGray;
                return this._BorderColor;
            }
            set
            {
                this._BorderColor = value;
                this.Invalidate();
            }
        }

        private bool _CanMove = true;
        /// <summary>
        /// 可移动窗体
        /// </summary>
        [Category("Aurora Style"), Description("可移动窗体")]
        public bool CanMove
        {
            get { return this._CanMove; }
            set { this._CanMove = value; }
        }

        private bool _CanResize = true;
        /// <summary>
        /// 可调整窗体尺寸
        /// </summary>
        [Category("Aurora Style"), Description("可调整窗体尺寸")]
        public bool CanResize
        {
            get { return this._CanResize; }
            set { this._CanResize = value; }
        }

        private bool _CanCoverTitle = true;
        /// <summary>
        /// 背景图片可覆盖标题栏，即标题栏不会绘制背景颜色
        /// </summary>
        [Category("Aurora Style"), Description("背景图片可覆盖标题栏，即标题栏不会绘制背景颜色")]
        public bool CanCoverTitle
        {
            get { return this._CanCoverTitle; }
            set { this._CanCoverTitle = value; }
        }
        #endregion

        #region Hidden Form Properties
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected new string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = this.Text;
                if (this.TitleBarStyle != null)
                    this.TitleBarStyle.Title = base.Text;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected new FormBorderStyle FormBorderStyle
        {
            get { return base.FormBorderStyle; }
            set { base.FormBorderStyle = FormBorderStyle.None; }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected new Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = this.BackColor; }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected new ImageLayout BackgroundImageLayout
        {
            get { return base.BackgroundImageLayout; }
            set { base.BackgroundImageLayout = this.BackgroundImageLayout; }
        }
        #endregion

        #region Rectangle
        /// <summary>
        /// 标题栏区域
        /// </summary>
        protected Rectangle TitleBarRect;

        /// <summary>
        /// 水晶Logo矩形区域
        /// </summary>
        protected Rectangle CrystalLogoRect
        {
            get
            {
                Rectangle rect = Rectangle.Empty;
                if (this.ShowIcon && this.Icon != null && this.TitleBarStyle.ShowCrystalLogo)
                {
                    int w = this.TitleBarStyle.Height - 1;
                    int h = this.TitleBarStyle.Height - 1;
                    rect = new Rectangle(this.GetFormBorderWidth() + this.TitleBarStyle.OffsetX, (this.TitleBarStyle.Height - h) / 2, w, h);
                }
                return rect;
            }
        }

        /// <summary>
        /// 图标的矩形区域
        /// </summary>
        protected Rectangle LogoRect
        {
            get
            {
                Rectangle rect = Rectangle.Empty;
                if (this.ShowIcon && this.Icon != null)
                {
                    if (this.TitleBarStyle.ShowCrystalLogo)
                    {
                        int x = this.CrystalLogoRect.Right;
                        int y = (this.TitleBarStyle.Height - this.TitleBarStyle.LogoSize.Height) / 2;
                        rect = new Rectangle(x, y, this.TitleBarStyle.LogoSize.Width, this.TitleBarStyle.LogoSize.Height);
                    }
                    else
                    {
                        int w = this.TitleBarStyle.LogoSize.Width;
                        int h = this.TitleBarStyle.LogoSize.Height;
                        rect = new Rectangle(this.GetFormBorderWidth() + this.TitleBarStyle.OffsetX, (this.TitleBarStyle.Height - h) / 2, w, h);
                    }
                }
                return rect;
            }
        }

        /// <summary>
        /// 关闭按钮的矩形区域
        /// </summary>
        protected Rectangle CloseBoxRect
        {
            get
            {
                if (this.ControlBox)
                {
                    return new Rectangle(
                        base.Width - this.ControlBoxStyle.Size.Width - this.GetFormBorderWidth(),
                        0,
                        this.ControlBoxStyle.Size.Width,
                        this.ControlBoxStyle.Size.Height);
                }

                return Rectangle.Empty;
            }
        }

        /// <summary>
        /// 最大化按钮的矩形区域
        /// </summary>
        protected Rectangle MaximizeBoxRect
        {
            get
            {
                if (this.ControlBox && this.MaximizeBox)
                {
                    return new Rectangle(
                        base.Width - this.ControlBoxStyle.Size.Width - this.CloseBoxRect.Width - this.GetFormBorderWidth(),
                        0,
                        this.ControlBoxStyle.Size.Width,
                        this.ControlBoxStyle.Size.Height);
                }

                return Rectangle.Empty;
            }
        }

        /// <summary>
        /// 最小化按钮的矩形区域
        /// </summary>
        protected Rectangle MinimizeBoxRect
        {
            get
            {
                if (this.ControlBox && this.MinimizeBox)
                {
                    return new Rectangle(
                        base.Width - this.ControlBoxStyle.Size.Width - this.CloseBoxRect.Width - this.MaximizeBoxRect.Width - this.GetFormBorderWidth(),
                        0,
                        this.ControlBoxStyle.Size.Width,
                        this.ControlBoxStyle.Size.Height);
                }

                return Rectangle.Empty;
            }
        }
        #endregion

        #region ControlBox Status
        /// <summary>
        /// 关闭按钮状态
        /// </summary>
        private AuroraControlBoxStatus CloseBoxState;

        /// <summary>
        /// 最大化按钮状态
        /// </summary>
        private AuroraControlBoxStatus MaximizeBoxState;

        /// <summary>
        /// 最小化按钮状态
        /// </summary>
        private AuroraControlBoxStatus MinimizeBoxState;
        #endregion

        #endregion

        public AuroraForm()
        {
            base.FormBorderStyle = FormBorderStyle.None;
            this.ControlBoxStyle.DefaultFlagColor = Color.Black;

            base.SetStyle(
             ControlStyles.UserPaint |
             ControlStyles.AllPaintingInWmPaint |
             ControlStyles.OptimizedDoubleBuffer |
             ControlStyles.ResizeRedraw |
             ControlStyles.SupportsTransparentBackColor |
             ControlStyles.DoubleBuffer, true);
            base.UpdateStyles();
            base.AutoScaleMode = AutoScaleMode.None;
        }


        #region Aurora Override
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            Rectangle rect = this.ClientRectangle;

            if (this.BackgroundImage != null)
            {
                if (this.CanCoverTitle)
                    AuroraGraphics.DrawImage(e.Graphics, rect, this.BackgroundImage);
                else
                {
                    Rectangle clientRect = new Rectangle(rect.X, this.TitleBarRect.Height, rect.Width, rect.Height - this.TitleBarRect.Height);
                    AuroraGraphics.DrawImage(e.Graphics, clientRect, this.BackgroundImage);
                }
            }
            else
            {
                AuroraGraphics.FillRectangle(e.Graphics, rect, this.BackgroundColor);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            this.DrawCaption(e.Graphics);

            this.DrawControlBox(e.Graphics);

            #region Draw Border
            if (this.WindowState == FormWindowState.Maximized)
                return;
            Color borderColor = this.BorderColor.IsEmpty ? Color.LightGray : this.BorderColor;

            if(this.BorderStyle == AuroraFormBorderStyle.None)
            {
                //无边框
                ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, borderColor, ButtonBorderStyle.None);
            }
            else
            {
                if (this.Radius > 0)
                {
                    //圆角边框
                    Rectangle rect = this.ClientRectangle;
                    using (GraphicsPath path = AuroraGraphics.CreateGraphicsPath(this.ClientRectangle, this.Radius, AuroraFramework.Controls.AuroraRoundStyle.All, true))
                    {
                        using (Pen pen = new Pen(borderColor))
                        {
                            e.Graphics.DrawPath(pen, path);
                        }
                    }
                }
                else
                {
                    //矩形边框
                    ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, borderColor, ButtonBorderStyle.Solid);
                }
            }
            #endregion
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (DesignMode) return;

            base.OnMouseMove(e);

            #region ControlBox process
            this.CloseBoxState = AuroraControlBoxStatus.Default;
            this.MaximizeBoxState = AuroraControlBoxStatus.Default;
            this.MinimizeBoxState = AuroraControlBoxStatus.Default;

            if (this.CloseBoxRect.Contains(e.Location))
                this.CloseBoxState = AuroraControlBoxStatus.Hover;

            if (this.MaximizeBoxRect.Contains(e.Location))
                this.MaximizeBoxState = AuroraControlBoxStatus.Hover;

            if (this.MinimizeBoxRect.Contains(e.Location))
                this.MinimizeBoxState = AuroraControlBoxStatus.Hover;

            foreach (AuroraCustomControlBox controlBox in this.CustomControlBoxes)
            {
                if (!controlBox.Visibale)
                    continue;

                controlBox.ControlBoxStatus = AuroraControlBoxStatus.Default;

                Rectangle controlBoxRect = this.GetAuroraCustomControlBoxRectangle(controlBox);
                if (controlBoxRect.Contains(e.Location))
                {
                    controlBox.ControlBoxStatus = AuroraControlBoxStatus.Hover;
                    if (controlBox.CustomMouseMove != null)
                        controlBox.OnCustomMouseMove(null, e);
                }
            }
            #endregion

            this.Invalidate();
        }

        protected override void OnMouseHover(EventArgs e)
        {
            if (DesignMode) return;

            base.OnMouseHover(e);

            #region ControlBox process
            this.CloseBoxState = AuroraControlBoxStatus.Default;
            this.MaximizeBoxState = AuroraControlBoxStatus.Default;
            this.MinimizeBoxState = AuroraControlBoxStatus.Default;

            Point point = this.PointToClient(MousePosition);

            if (this.CloseBoxRect.Contains(point))
                this.CloseBoxState = AuroraControlBoxStatus.Hover;

            if (this.MaximizeBoxRect.Contains(point))
                this.MaximizeBoxState = AuroraControlBoxStatus.Hover;

            if (this.MinimizeBoxRect.Contains(point))
                this.MinimizeBoxState = AuroraControlBoxStatus.Hover;

            foreach (AuroraCustomControlBox controlBox in this.CustomControlBoxes)
            {
                if (!controlBox.Visibale)
                    continue;

                controlBox.ControlBoxStatus = AuroraControlBoxStatus.Default;

                Rectangle controlBoxRect = this.GetAuroraCustomControlBoxRectangle(controlBox);
                if (controlBoxRect.Contains(point))
                {
                    controlBox.ControlBoxStatus = AuroraControlBoxStatus.Hover;
                    if (controlBox.CustomMouseHover != null)
                        controlBox.OnCustomMouseHover(null, e);
                }
            }
            #endregion

            this.Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (DesignMode) return;

            base.OnMouseDown(e);

            #region ControlBox process
            this.CloseBoxState = AuroraControlBoxStatus.Default;
            this.MaximizeBoxState = AuroraControlBoxStatus.Default;
            this.MinimizeBoxState = AuroraControlBoxStatus.Default;

            int top = this.TitleBarStyle.Height <= 5 ? 5 : this.TitleBarStyle.Height;

            if (e.X > 5 && e.X < ClientSize.Width - 5 && e.Y > top && e.Y <= ClientSize.Height - 5)
            {
                //鼠标在客户区域
                if (this.CanMove && this.WindowState != FormWindowState.Maximized)
                {
                    //释放鼠标焦点捕获
                    Win32.ReleaseCapture();

                    //向当前窗体发送拖动消息
                    Win32.SendMessage(this.Handle, 0x0112, 0xF011, 0);
                    OnMouseUp(e);
                }
            }
            else if (!this.CloseBoxRect.Contains(e.Location) && !this.MaximizeBoxRect.Contains(e.Location)
                            && !this.MinimizeBoxRect.Contains(e.Location) && this.TitleBarRect.Contains(e.Location))
            {
                //鼠标在标题栏区域(不包含控制按钮区域)

                //判断是否在自定义控制按钮区域中
                bool isContain = false;
                foreach (AuroraCustomControlBox controlBox in this.CustomControlBoxes)
                {
                    if (!controlBox.Visibale) continue;

                    Rectangle controlBoxRect = this.GetAuroraCustomControlBoxRectangle(controlBox);
                    if (controlBoxRect.Contains(e.Location))
                    {
                        isContain = true;
                        break;
                    }
                }

                if (!isContain)
                {
                    //鼠标不在自定义控制按钮区域中
                    if (this.CanMove)
                    {
                        //释放鼠标焦点捕获
                        Win32.ReleaseCapture();

                        //向当前窗体发送拖动消息
                        Win32.SendMessage(this.Handle, 0x0112, 0xF011, 0);
                        OnMouseUp(e);
                    }
                }
            }
            else
            {
                if (!this.CloseBoxRect.IsEmpty && this.CloseBoxRect.Contains(e.Location))
                    this.CloseBoxState = AuroraControlBoxStatus.Pressed;

                if (!this.MaximizeBoxRect.IsEmpty && this.MaximizeBoxRect.Contains(e.Location))
                    this.MaximizeBoxState = AuroraControlBoxStatus.Pressed;

                if (!this.MinimizeBoxRect.IsEmpty && this.MinimizeBoxRect.Contains(e.Location))
                    this.MinimizeBoxState = AuroraControlBoxStatus.Pressed;

                foreach (AuroraCustomControlBox controlBox in this.CustomControlBoxes)
                {
                    if (!controlBox.Visibale)
                        continue;

                    controlBox.ControlBoxStatus = AuroraControlBoxStatus.Default;

                    Rectangle controlBoxRect = this.GetAuroraCustomControlBoxRectangle(controlBox);
                    if (!controlBoxRect.IsEmpty && controlBoxRect.Contains(e.Location))
                    {
                        controlBox.ControlBoxStatus = AuroraControlBoxStatus.Pressed;
                        if (controlBox.CustomMouseDown != null)
                            controlBox.OnCustomMouseDown(null, e);
                    }
                }
            }
            #endregion

            this.Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (DesignMode) return;

            base.OnMouseUp(e);

            #region ControlBox process
            this.CloseBoxState = AuroraControlBoxStatus.Default;
            this.MaximizeBoxState = AuroraControlBoxStatus.Default;
            this.MinimizeBoxState = AuroraControlBoxStatus.Default;

            if (!this.CloseBoxRect.IsEmpty && this.CloseBoxRect.Contains(e.Location) && e.Button == MouseButtons.Left)
            {
                base.Close();
                this.CloseBoxState = AuroraControlBoxStatus.Default;
            }

            if (!this.MaximizeBoxRect.IsEmpty && this.MaximizeBoxRect.Contains(e.Location) && e.Button == MouseButtons.Left)
            {
                this.MaxNormalSwitch();
                this.MaximizeBoxState = AuroraControlBoxStatus.Default;
            }

            if (!this.MinimizeBoxRect.IsEmpty && this.MinimizeBoxRect.Contains(e.Location) && e.Button == MouseButtons.Left)
            {
                this.WindowState = FormWindowState.Minimized;
                this.MinimizeBoxState = AuroraControlBoxStatus.Default;
            }

            foreach (AuroraCustomControlBox controlBox in this.CustomControlBoxes)
            {
                if (!controlBox.Visibale)
                    continue;

                Rectangle controlBoxRect = this.GetAuroraCustomControlBoxRectangle(controlBox);
                if (!controlBoxRect.IsEmpty && controlBoxRect.Contains(e.Location) && e.Button == MouseButtons.Left)
                {
                    controlBox.ControlBoxStatus = AuroraControlBoxStatus.Default;
                    if (controlBox.CustomMouseUp != null)
                        controlBox.OnCustomMouseUp(null, e);
                }
            }
            #endregion

            this.Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (DesignMode) return;

            base.OnMouseLeave(e);

            #region ControlBox process
            if (!this.CloseBoxRect.IsEmpty)
                this.CloseBoxState = AuroraControlBoxStatus.Default;

            if (!this.MinimizeBoxRect.IsEmpty)
                this.MaximizeBoxState = AuroraControlBoxStatus.Default;

            if (!this.MinimizeBoxRect.IsEmpty)
                this.MinimizeBoxState = AuroraControlBoxStatus.Default;

            foreach (AuroraCustomControlBox controlBox in this.CustomControlBoxes)
            {
                if (!controlBox.Visibale)
                    continue;

                Rectangle controlBoxRect = this.GetAuroraCustomControlBoxRectangle(controlBox);
                if (!controlBoxRect.IsEmpty)
                {
                    controlBox.ControlBoxStatus = AuroraControlBoxStatus.Default;
                }
            }
            #endregion

            this.Invalidate();
        }

        protected override void OnClick(EventArgs e)
        {
            if (DesignMode) return;

            base.OnClick(e);

            #region Custom controlBox process
            Point point = this.PointToClient(MousePosition);
            foreach (AuroraCustomControlBox controlBox in this.CustomControlBoxes)
            {
                if (!controlBox.Visibale) continue;

                Rectangle controlBoxRect = this.GetAuroraCustomControlBoxRectangle(controlBox);
                if ((!controlBoxRect.IsEmpty && controlBoxRect.Contains(point)))
                {
                    if (controlBox.CustomClick != null)
                        controlBox.OnCustomClick(null, e);
                }
            }
            #endregion

            this.Invalidate();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            if(this.WindowState == FormWindowState.Maximized)
                this.SetFormRoundRectRgn(this, 0);
            else
                this.SetFormRoundRectRgn(this, this.Radius);

            this.SetTitleBarRect();
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);

            this.Invalidate();
        }

        #endregion

        #region Title Bar Method
        /// <summary>
        /// 绘制标题栏
        /// </summary>
        private void DrawCaption(Graphics graphics)
        {
            if (this.TitleBarStyle.Height > 0)
            {
                //绘制标题栏背景颜色
                Rectangle newRect = new Rectangle(this.TitleBarRect.Left, this.TitleBarRect.Bottom, this.TitleBarRect.Width, 1);
                graphics.SetClip(newRect, CombineMode.Exclude);

                if ((this.BackgroundImage != null && !this.CanCoverTitle) || this.BackgroundImage == null)
                {
                    AuroraGraphics.FillRectangle(graphics, this.TitleBarRect, this.TitleBarStyle.BackgroundColor);
                }

                //恢复Clip为无限区域，重新绘制
                graphics.ResetClip();

                if (this.TitleBarStyle.ShowCrystalLogo)
                {
                    this.DrawCrystalLogo(graphics);
                }
                else
                {
                    //绘制标题栏Logo
                    if (this.ShowIcon && this.Icon != null)
                    {
                        int x = this.TitleBarStyle.OffsetX + this.GetFormBorderWidth();
                        int y = (this.TitleBarStyle.Height - this.TitleBarStyle.LogoSize.Height) / 2;
                        int w = this.TitleBarStyle.LogoSize.Width;
                        int h = this.TitleBarStyle.LogoSize.Height;
                        var rect = new Rectangle(x, y, w, h);
                        AuroraGraphics.DrawImage(graphics, rect, this.Icon.ToBitmap());
                    }
                }

                //绘制标题栏标题文本
                var textOffsetX = this.TitleBarStyle.OffsetX + this.GetFormBorderWidth();

                Font font = this.TitleBarStyle.Font ?? this.Font;
                Size fontSize = Size.Ceiling(graphics.MeasureString(this.TitleBarStyle.Title, font));

                if (this.TitleBarStyle.TextAlign == AuroraTitleBarTextAlignment.Center)
                {
                    //标题栏文本居中对齐
                    textOffsetX = (this.Width - fontSize.Width) / 2;
                }
                else if (this.TitleBarStyle.TextAlign == AuroraTitleBarTextAlignment.Left)
                {
                    if (this.ShowIcon && this.Icon != null && this.TitleBarStyle.ShowCrystalLogo)
                    {
                        //显示水晶Logo
                        textOffsetX = this.CrystalLogoRect.Right + 5;
                    }
                    else if (this.ShowIcon && this.Icon != null)
                    {
                        //显示Logo
                        textOffsetX = this.LogoRect.Right + 5;
                    }
                }

                Color foreColor = this.TitleBarStyle.ForeColor.IsEmpty ? this.ForeColor : this.TitleBarStyle.ForeColor;
                using (var brush = new SolidBrush(foreColor))
                {
                    graphics.DrawString(this.TitleBarStyle.Title, font, brush, textOffsetX, (this.TitleBarStyle.Height - fontSize.Height) / 2 + this.GetFormBorderWidth());
                }
            }
        }

        /// <summary>
        /// 绘制水晶Logo
        /// </summary>
        private void DrawCrystalLogo(Graphics graphics)
        {
            if (this.ShowIcon && this.Icon != null && this.TitleBarStyle.ShowCrystalLogo)
            {
                Rectangle rectangle = new Rectangle(
                    this.TitleBarStyle.OffsetX + this.GetFormBorderWidth(),
                    this.TitleBarStyle.Height,
                    this.Width - this.GetFormBorderWidth() * 2,
                    this.Height - this.TitleBarStyle.Height);
                graphics.SetClip(rectangle, CombineMode.Exclude);

                AuroraGraphics.InitializeGraphics(graphics);

                Rectangle crystalRect = this.CrystalLogoRect;
                crystalRect.Inflate(-1, -1);
                AuroraGraphics.FillEllipse(graphics, crystalRect, Color.White);
                Color c1 = Color.Empty, c2 = Color.Empty, c3 = Color.FromArgb(232, 246, 250);
                Blend blend = new Blend
                {
                    Positions = new float[] { 0f, 0.3f, 0.5f, 0.8f, 1f },
                    Factors = new float[] { 0.15f, 0.55f, 0.7f, 0.8f, 0.95f }
                };

                AuroraGraphics.DrawCrystalLogo(graphics, crystalRect, c1, c2, c3, blend);
                Color borderColor = Color.FromArgb(65, 177, 199);
                AuroraGraphics.DrawEllipseBorder(graphics, crystalRect, borderColor, 1);

                Size imgSize = new Size(this.TitleBarStyle.Height - 12, this.TitleBarStyle.Height - 12);
                if (base.ShowIcon && this.Icon != null)
                {
                    Image img = Image.FromHbitmap(this.Icon.ToBitmap().GetHbitmap());
                    if (img.Width <= this.TitleBarStyle.Height - 12 && img.Height <= this.TitleBarStyle.Height - 12)
                    {
                        imgSize = new Size(img.Width, img.Height);
                    }
                    else if (img.Width < this.TitleBarStyle.Height - 12 && img.Height > this.TitleBarStyle.Height - 12)
                    {
                        imgSize = new Size(img.Width, img.Width);
                    }
                    else if (img.Width > this.TitleBarStyle.Height - 12 && img.Height < this.TitleBarStyle.Height - 12)
                    {
                        imgSize = new Size(img.Height, img.Height);
                    }

                    AuroraGraphics.DrawImage(graphics, crystalRect, img, imgSize);
                }
                graphics.ResetClip();
            }
        }

        /// <summary>
        /// 设置标题栏区域
        /// </summary>
        private void SetTitleBarRect()
        {
            if (!this.BorderColor.IsEmpty && this.GetFormBorderWidth() > 0)
            {
                this.TitleBarRect = new Rectangle(0, 0, this.Width - this.GetFormBorderWidth() * 2, this.TitleBarStyle.Height);
            }
            else
            {
                this.TitleBarRect = new Rectangle(0, 0, this.Width, this.TitleBarStyle.Height);
            }

            int width = this.CloseBoxRect.Width + this.MaximizeBoxRect.Width + this.MinimizeBoxRect.Width + (this.TitleBarStyle.ShowCrystalLogo ? this.CrystalLogoRect.Width : this.LogoRect.Width);
            this.MinimumSize = new Size(width, this.TitleBarRect.Height);
        }
        #endregion

        #region ControlBox Method
        /// <summary>
        /// 绘制控制按钮
        /// </summary>
        private void DrawControlBox(Graphics graphics)
        {
            if (this.ControlBox)
            {
                AuroraGradientColor hoverBackColor = this.ControlBoxStyle.HoverBackgroundColor;
                AuroraGradientColor pressedBackColor = this.ControlBoxStyle.PressedBackgroundColor;
                AuroraGradientColor defaultBackColor = this.ControlBoxStyle.DefaultBackgroundColor;

                if (!this.CloseBoxRect.IsEmpty)
                {
                    Color flagColor = this.ControlBoxStyle.DefaultFlagColor.IsEmpty ? Color.FromArgb(0, 0, 0) : this.ControlBoxStyle.DefaultFlagColor;
                    switch (this.CloseBoxState)
                    {
                        case AuroraControlBoxStatus.Hover:
                            AuroraGraphics.FillRectangle(graphics, this.CloseBoxRect, this.ControlBoxStyle.ClosedHoverBackgroundColor);
                            flagColor = this.ControlBoxStyle.HoverFlagColor.IsEmpty ? flagColor : this.ControlBoxStyle.HoverFlagColor;
                            break;
                        case AuroraControlBoxStatus.Pressed:
                            AuroraGraphics.FillRectangle(graphics, this.CloseBoxRect, this.ControlBoxStyle.ClosedPressedBackgroundColor);
                            flagColor = this.ControlBoxStyle.HoverFlagColor.IsEmpty ? flagColor : this.ControlBoxStyle.HoverFlagColor;
                            break;
                        default:
                            AuroraGraphics.FillRectangle(graphics, this.CloseBoxRect, defaultBackColor);
                            break;
                    }

                    using (Brush brush = new SolidBrush(flagColor))
                    {
                        graphics.FillPath(brush, AuroraGraphics.CreateCloseFlag(this.CloseBoxRect));
                    }
                    //using (Pen pen = new Pen(flagColor, 2))
                    //{
                    //    PointF centerPoint = new PointF(this.CloseBoxRect.X + this.CloseBoxRect.Width / 2.0f, this.CloseBoxRect.Y + this.CloseBoxRect.Height / 2.0f);
                    //    graphics.DrawLine(pen, centerPoint.X - 5, centerPoint.Y - 4, centerPoint.X + 3, centerPoint.Y + 4);
                    //    graphics.DrawLine(pen, centerPoint.X - 5, centerPoint.Y + 4, centerPoint.X + 3, centerPoint.Y - 4);
                    //}

                }

                if (!this.MaximizeBoxRect.IsEmpty && this.MaximizeBox)
                {
                    Color flagColor = this.ControlBoxStyle.DefaultFlagColor.IsEmpty ? Color.FromArgb(0, 0, 0) : this.ControlBoxStyle.DefaultFlagColor;
                    switch (this.MaximizeBoxState)
                    {
                        case AuroraControlBoxStatus.Hover:
                            AuroraGraphics.FillRectangle(graphics, this.MaximizeBoxRect, hoverBackColor);
                            flagColor = this.ControlBoxStyle.HoverFlagColor.IsEmpty ? flagColor : this.ControlBoxStyle.HoverFlagColor;
                            break;
                        case AuroraControlBoxStatus.Pressed:
                            AuroraGraphics.FillRectangle(graphics, this.MaximizeBoxRect, pressedBackColor);
                            flagColor = this.ControlBoxStyle.HoverFlagColor.IsEmpty ? flagColor : this.ControlBoxStyle.HoverFlagColor;
                            break;
                        default:
                            AuroraGraphics.FillRectangle(graphics, this.MaximizeBoxRect, defaultBackColor);
                            break;
                    }

                    using (Brush brush = new SolidBrush(flagColor))
                    {
                        graphics.FillPath(brush, AuroraGraphics.CreateMaximizeFlag(this.MaximizeBoxRect, this.WindowState == FormWindowState.Maximized ? true : false));
                    }
                }

                if (!this.MinimizeBoxRect.IsEmpty && this.MinimizeBox)
                {
                    Color flagColor = this.ControlBoxStyle.DefaultFlagColor.IsEmpty ? Color.FromArgb(0, 0, 0) : this.ControlBoxStyle.DefaultFlagColor;
                    switch (this.MinimizeBoxState)
                    {
                        case AuroraControlBoxStatus.Hover:
                            AuroraGraphics.FillRectangle(graphics, this.MinimizeBoxRect, hoverBackColor);
                            flagColor = this.ControlBoxStyle.HoverFlagColor.IsEmpty ? flagColor : this.ControlBoxStyle.HoverFlagColor;
                            break;
                        case AuroraControlBoxStatus.Pressed:
                            AuroraGraphics.FillRectangle(graphics, this.MinimizeBoxRect, pressedBackColor);
                            flagColor = this.ControlBoxStyle.HoverFlagColor.IsEmpty ? flagColor : this.ControlBoxStyle.HoverFlagColor;
                            break;
                        default:
                            AuroraGraphics.FillRectangle(graphics, this.MinimizeBoxRect, defaultBackColor);
                            break;
                    }

                    using (Brush brush = new SolidBrush(flagColor))
                    {
                        graphics.FillPath(brush, AuroraGraphics.CreateMinimizeFlag(this.MinimizeBoxRect));
                    }
                }

                foreach (AuroraCustomControlBox controlBox in this.CustomControlBoxes)
                {
                    if (!controlBox.Visibale) continue;

                    //获取控制按钮矩形区域
                    Rectangle controlBoxRect = this.GetAuroraCustomControlBoxRectangle(controlBox);
                    Color flagColor = controlBox.DefaultFlagColor.IsEmpty ? Color.FromArgb(255, 255, 255) : controlBox.DefaultFlagColor;
                    if (!controlBoxRect.IsEmpty)
                    {
                        switch (controlBox.ControlBoxStatus)
                        {
                            case AuroraControlBoxStatus.Hover:
                                AuroraGraphics.FillRectangle(graphics, controlBoxRect, controlBox.HoverBackgroundColor ?? hoverBackColor);
                                flagColor = controlBox.HoverFlagColor.IsEmpty ? flagColor : controlBox.HoverFlagColor;
                                break;
                            case AuroraControlBoxStatus.Pressed:
                                AuroraGraphics.FillRectangle(graphics, controlBoxRect, controlBox.PressedBackgroundColor ?? pressedBackColor);
                                flagColor = controlBox.HoverFlagColor.IsEmpty ? flagColor : controlBox.HoverFlagColor;
                                break;
                            default:
                                AuroraGraphics.FillRectangle(graphics, controlBoxRect, controlBox.DefaultBackgroundColor ?? defaultBackColor);
                                break;
                        }

                        Size size = (controlBox.ImageSize == null || controlBox.ImageSize.IsEmpty) ? new Size(14, 14) : controlBox.ImageSize;

                        Bitmap bitmap = controlBox.Image ?? AuroraFramework.Properties.Resources.Image;

                        Bitmap image = AuroraGraphics.TransformBitmapFlagColor(bitmap, flagColor);

                        AuroraGraphics.DrawImage(graphics, controlBoxRect, image, size);
                    }
                }
            }
        }

        /// <summary>
        /// 获取自定义控制按钮矩形区域
        /// </summary>
        /// <param name="control">自定义控制按钮</param>
        /// <returns></returns>
        private Rectangle GetAuroraCustomControlBoxRectangle(AuroraCustomControlBox control)
        {
            int index = this.CustomControlBoxes.IndexOf(control);

            if (index != -1)
            {
                //获取系统控制按钮矩形区域
                return new Rectangle(
                        base.Width - (index + 1) * this.ControlBoxStyle.Size.Width - this.CloseBoxRect.Width - this.MaximizeBoxRect.Width - this.MinimizeBoxRect.Width - this.GetFormBorderWidth(),
                        0,
                        this.ControlBoxStyle.Size.Width,
                        this.ControlBoxStyle.Size.Height);
            }

            return Rectangle.Empty;
        }
        #endregion

        #region Form Method
        /// <summary>
        /// 设置窗体的圆角矩形
        /// </summary>
        /// <param name="form">需要设置的窗体</param>
        /// <param name="rgnRadius">圆角矩形的半径</param>
        private void SetFormRoundRectRgn(Form form, int rgnRadius)
        {
            int hRgn = 0;
            hRgn = Win32.CreateRoundRectRgn(0, 0, form.Width + 1, form.Height + 1, rgnRadius, rgnRadius);
            Win32.SetWindowRgn(form.Handle, hRgn, true);
            Win32.DeleteObject(hRgn);
        }

        /// <summary>
        /// 获取窗体边框宽度
        /// </summary>
        /// <returns></returns>
        private int GetFormBorderWidth()
        {
            if (this.WindowState == FormWindowState.Maximized || !this.BorderColor.IsEmpty || this.BorderStyle == AuroraFormBorderStyle.None)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        /// <summary>
        /// 设置窗体内边距
        /// </summary>
        private void SetFormPadding()
        {
            base.Padding = new Padding(
                this.GetFormBorderWidth(),
                this.TitleBarStyle.Height,
                this.GetFormBorderWidth(),
                this.GetFormBorderWidth()
                );

            this.SetTitleBarRect();
            this.Invalidate();
        }

        /// <summary>
        /// 最大化和正常状态窗口切换
        /// </summary>
        private void MaxNormalSwitch()
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                //如果当前状态是最大化状态 则窗体需要恢复默认大小
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                //防止遮挡任务栏
                this.MaximumSize = new Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height);
                this.WindowState = FormWindowState.Maximized;
            }
        }
        #endregion

        #region WndProc
        /// <summary>
        /// 消息响应处理
        /// </summary>
        const int WM_NCHITTEST = 0x0084;
        /// <summary>
        /// 左边界
        /// </summary>
        const int HTLEFT = 10;
        /// <summary>
        /// 右边界
        /// </summary>
        const int HTRIGHT = 11;
        /// <summary>
        /// 上边界
        /// </summary>
        const int HTTOP = 12;
        /// <summary>
        /// 左上角
        /// </summary>
        const int HTTOPLEFT = 13;
        /// <summary>
        /// 右上角
        /// </summary>
        const int HTTOPRIGHT = 14;
        /// <summary>
        /// 下边界
        /// </summary>
        const int HTBOTTOM = 15;
        /// <summary>
        /// 左下角
        /// </summary>
        const int HTBOTTOMLEFT = 0x10;
        /// <summary>
        /// 右下角
        /// </summary>
        const int HTBOTTOMRIGHT = 17;

        protected override void WndProc(ref Message m)
        {
            if (this.DesignMode)
            {
                base.WndProc(ref m);
                return;
            }

            if (this.CloseBoxState != AuroraControlBoxStatus.Default || this.MinimizeBoxState != AuroraControlBoxStatus.Default || this.MaximizeBoxState != AuroraControlBoxStatus.Default)
            {
                base.WndProc(ref m);
                return;
            }

            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    base.WndProc(ref m);
                    if (this.CanResize && WindowState != FormWindowState.Maximized)
                    {
                        Point mouseLocation = new Point((int)m.LParam & 0xFFFF, (int)m.LParam >> 16 & 0xFFFF);
                        mouseLocation = PointToClient(mouseLocation);
                        if (mouseLocation.X <= 5)
                        {
                            if (mouseLocation.Y <= 5)
                                m.Result = (IntPtr)HTTOPLEFT;
                            else if (mouseLocation.Y >= ClientSize.Height - 5)
                                m.Result = (IntPtr)HTBOTTOMLEFT;
                            else
                                m.Result = (IntPtr)HTLEFT;
                        }
                        else if (mouseLocation.X >= ClientSize.Width - 5)
                        {
                            if (mouseLocation.Y <= 5)
                                m.Result = (IntPtr)HTTOPRIGHT;
                            else if (mouseLocation.Y >= ClientSize.Height - 5)
                                m.Result = (IntPtr)HTBOTTOMRIGHT;
                            else
                                m.Result = (IntPtr)HTRIGHT;
                        }
                        else if (mouseLocation.Y <= 5)
                        {
                            m.Result = (IntPtr)HTTOP;
                        }
                        else if (mouseLocation.Y >= ClientSize.Height - 5)
                        {
                            m.Result = (IntPtr)HTBOTTOM;
                        }
                    }
                    break;
                    {
                        //case 0x0201: //鼠标左键按下的消息
                        //    Point cPoint = new Point((int)m.LParam & 0xFFFF, (int)m.LParam >> 16 & 0xFFFF);
                        //    Rectangle captionRect = new Rectangle(0, 0, this.Width, this.AuroraTitleBar.Height);
                        //    if (!this.CloseBoxRect.Contains(cPoint) && !this.MaximizeBoxRect.Contains(cPoint)
                        //        && !this.MinimizeBoxRect.Contains(cPoint) && !this.CrystalLogoRect.Contains(cPoint)
                        //        && !this.LogoRect.Contains(cPoint) && captionRect.Contains(cPoint))
                        //    {
                        //        bool flag = false;
                        //        //foreach (CustomSysControlBox controlBoxButton in this.SysControlBoxes)
                        //        //{
                        //        //    if (!controlBoxButton.Visibale) continue;

                        //        //    Rectangle rect = this.GetControlBoxButtonRectangle(controlBoxButton);
                        //        //    if (rect.Contains(cPoint))
                        //        //    {
                        //        //        flag = true;
                        //        //        break;
                        //        //    }
                        //        //}

                        //        if (!flag)
                        //        {
                        //            m.Msg = 0x00A1;         //更改消息为非客户区按下鼠标   
                        //            m.LParam = IntPtr.Zero; //默认值   
                        //            m.WParam = new IntPtr(2);//鼠标放在标题栏内   
                        //            base.WndProc(ref m);
                        //        }
                        //    }
                        //    else
                        //    {
                        //        m.Msg = 0x00A1;         //更改消息为非客户区按下鼠标   
                        //        m.LParam = IntPtr.Zero; //默认值   
                        //        m.WParam = new IntPtr(2);//鼠标放在标题栏内   
                        //        base.WndProc(ref m);
                        //    }
                        //    break;
                    }
                case 0x0203://双击左键
                    base.WndProc(ref m);
                    if (this.CanResize && this.MaximizeBox)
                    {
                        Point point = new Point((int)m.LParam & 0xFFFF, (int)m.LParam >> 16 & 0xFFFF);
                        Rectangle titleRect = new Rectangle(0, 0, this.Width, this.TitleBarStyle.Height);
                        if (!this.CloseBoxRect.Contains(point) && !this.MaximizeBoxRect.Contains(point)
                            && !this.MinimizeBoxRect.Contains(point) && titleRect.Contains(point))
                        {
                            bool isContain = false;
                            foreach (AuroraCustomControlBox controlBox in this.CustomControlBoxes)
                            {
                                if (!controlBox.Visibale) continue;

                                Rectangle controlBoxRect = this.GetAuroraCustomControlBoxRectangle(controlBox);
                                if (controlBoxRect.Contains(point))
                                {
                                    isContain = true;
                                    break;
                                }
                            }
                            if (!isContain && WindowState != FormWindowState.Minimized)
                            {
                                this.MaxNormalSwitch();
                            }
                        }
                    }
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        #endregion
    }
}
