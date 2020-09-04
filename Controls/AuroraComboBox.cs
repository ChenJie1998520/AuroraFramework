using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AuroraFramework.Controls
{
    [ToolboxBitmap(typeof(ComboBox))]
    public class AuroraComboBox : System.Windows.Forms.ComboBox
    {
        #region Properties
        /// <summary>
        /// 控件状态
        /// </summary>
        private AuroraControlStatus _ControlStatus = AuroraControlStatus.Default;

        /// <summary>
        /// 是否绘制提示文本
        /// </summary>
        private bool _DrawPrompt;

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

        private Color _SelectedItemBackColor = Color.Empty;
        /// <summary>
        /// 选中项背景颜色
        /// </summary>
        [Category("Aurora Style"), Description("选中项背景颜色")]
        public Color SelectedItemBackColor
        {
            get
            {
                if (this._SelectedItemBackColor == Color.Empty)
                    this._SelectedItemBackColor = SystemColors.Control;
                return this._SelectedItemBackColor;
            }
            set { this._SelectedItemBackColor = value; }
        }

        private Color _SelectedItemForeColor = Color.Empty;
        /// <summary>
        /// 选中项字体颜色
        /// </summary>
        [Category("Aurora Style"), Description("选中项字体颜色")]
        public Color SelectedItemForeColor
        {
            get
            {
                if (this._SelectedItemForeColor == Color.Empty)
                    this._SelectedItemForeColor = SystemColors.WindowText;
                return this._SelectedItemForeColor;
            }
            set { this._SelectedItemForeColor = value; }
        }

        private string _PromptText = string.Empty;
        /// <summary>
        /// 提示文本
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Aurora Style"), Description("提示文本")]
        public string PromptText
        {
            get { return this._PromptText; }
            set { this._PromptText = value.Trim(); this.Invalidate(); }
        }

        /// <summary>
        /// 控件显示的文字的字体
        /// </summary>
        [Category("Aurora Style"), Description("控件显示的文字的字体")]
        public override Font Font
        {
            get { return base.Font; }
            set { base.Font = value; }
        }

        [Browsable(false)]
        [DefaultValue(DrawMode.OwnerDrawFixed)]
        public new DrawMode DrawMode
        {
            get { return DrawMode.OwnerDrawFixed; }
            set { base.DrawMode = DrawMode.OwnerDrawFixed; }
        }

        [Browsable(false)]
        [DefaultValue(ComboBoxStyle.DropDownList)]
        public new ComboBoxStyle DropDownStyle
        {
            get { return ComboBoxStyle.DropDownList; }
            set { base.DropDownStyle = ComboBoxStyle.DropDownList; }
        }
        #endregion

        public AuroraComboBox()
        {
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer, true);
            base.DrawMode = DrawMode.OwnerDrawFixed;
            base.DropDownStyle = ComboBoxStyle.DropDownList;
            this._DrawPrompt = this.SelectedIndex == -1;
        }

        #region Override
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            try
            {
                if (this.BackColor.A == byte.MaxValue && this.BackgroundImage == null)
                {
                    e.Graphics.Clear(this.BackColor);
                }
                else
                {
                    base.OnPaintBackground(e);
                }
            }
            catch
            {
                this.Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                if (this.GetStyle(ControlStyles.AllPaintingInWmPaint))
                    this.OnPaintBackground(e);

                this.OnPaintForeground(e);
            }
            catch
            {
                this.Invalidate();
            }
        }

        protected virtual void OnPaintForeground(PaintEventArgs e)
        {
            this.ItemHeight = this.GetPreferredSize(Size.Empty).Height;

            Color foreColor = this.Enabled ? this.ForeColor : Color.FromArgb(159, 159, 159);

            using (Pen pen = new Pen(this.BorderColor))
            {
                Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
                e.Graphics.DrawRectangle(pen, rect);
            }

            using (SolidBrush solidBrush = new SolidBrush(foreColor))
            {
                e.Graphics.FillPolygon(solidBrush, new Point[3]
                {
                    new Point(this.Width - 20, this.Height / 2 - 2),
                    new Point(this.Width - 9, this.Height / 2 - 2),
                    new Point(this.Width - 15, this.Height / 2 + 4)
                });
            }

            Rectangle bounds = new Rectangle(2, 2, this.Width - 20, this.Height - 4);
            TextRenderer.DrawText(e.Graphics, this.Text, this.Font, bounds, foreColor, TextFormatFlags.VerticalCenter);

            if (!this._DrawPrompt)
                return;
            this.DrawTextPrompt(e.Graphics);
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                Color foreColor;
                if (e.State == (DrawItemState.NoAccelerator | DrawItemState.NoFocusRect) || e.State == DrawItemState.None)
                {
                    using (SolidBrush solidBrush = new SolidBrush(this.BackColor))
                    {
                        e.Graphics.FillRectangle(solidBrush, new Rectangle(e.Bounds.Left, e.Bounds.Top, e.Bounds.Width, e.Bounds.Height));
                    }
                    foreColor = this.ForeColor;
                }
                else
                {
                    using (SolidBrush solidBrush = new SolidBrush(this.SelectedItemBackColor))
                    {
                        e.Graphics.FillRectangle(solidBrush, new Rectangle(e.Bounds.Left, e.Bounds.Top, e.Bounds.Width, e.Bounds.Height));
                    }
                    foreColor = this.SelectedItemForeColor;
                }
                Rectangle bounds = new Rectangle(0, e.Bounds.Top, e.Bounds.Width, e.Bounds.Height);

                TextRenderer.DrawText(e.Graphics, this.GetItemText(this.Items[e.Index]), this.Font, bounds, foreColor, TextFormatFlags.VerticalCenter);
            }
            else
                base.OnDrawItem(e);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            this._ControlStatus = AuroraControlStatus.Hover;
            this.Invalidate();
            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            this._ControlStatus = AuroraControlStatus.Default;
            this.Invalidate();
            base.OnLostFocus(e);
        }

        protected override void OnEnter(EventArgs e)
        {
            this._ControlStatus = AuroraControlStatus.Hover;
            this.Invalidate();
            base.OnEnter(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            this._ControlStatus = AuroraControlStatus.Default;
            this.Invalidate();
            base.OnLeave(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                this._ControlStatus = AuroraControlStatus.Hover;
                this.Invalidate();
            }
            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            this.Invalidate();
            base.OnKeyUp(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            this._ControlStatus = AuroraControlStatus.Hover;
            this.Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this._ControlStatus = AuroraControlStatus.Pressed;
                this.Invalidate();
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            this._ControlStatus = AuroraControlStatus.Default;
            this.Invalidate();
            base.OnMouseUp(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            this._ControlStatus = AuroraControlStatus.Default;
            this.Invalidate();
            base.OnMouseLeave(e);
        }

        public override Size GetPreferredSize(Size proposedSize)
        {
            base.GetPreferredSize(proposedSize);
            Size size;
            using (Graphics graphics = this.CreateGraphics())
            {
                string text = this.Text.Length > 0 ? this.Text : "MeasureText";
                proposedSize = new Size(int.MaxValue, int.MaxValue);
                size = TextRenderer.MeasureText(graphics, text, this.Font, proposedSize, TextFormatFlags.VerticalCenter | TextFormatFlags.LeftAndRightPadding);
                size.Height += 4;
            }
            return size;
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);
            this._DrawPrompt = this.SelectedIndex == -1;
            this.Invalidate();
        }

        private const int OCM_COMMAND = 8465;
        private const int WM_PAINT = 15;
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg != WM_PAINT && m.Msg != OCM_COMMAND || !this._DrawPrompt)
                return;
            this.DrawTextPrompt();
        }
        #endregion

        /// <summary>
        /// 绘制文本提示
        /// </summary>
        private void DrawTextPrompt()
        {
            using (Graphics graphics = this.CreateGraphics())
            {
                this.DrawTextPrompt(graphics);
            }
        }

        /// <summary>
        /// 绘制文本提示
        /// </summary>
        private void DrawTextPrompt(Graphics g)
        {
            Rectangle bounds = new Rectangle(2, 2, this.Width - 20, this.Height - 4);

            TextRenderer.DrawText(g, this.PromptText, this.Font, bounds, SystemColors.GrayText, this.BackColor, TextFormatFlags.EndEllipsis | TextFormatFlags.VerticalCenter);
        }

    }
}
