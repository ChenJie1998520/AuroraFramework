using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AuroraFramework.Controls
{
    [ToolboxBitmap(typeof(TextBox))]
    public class AuroraWaterMarkTextBox: TextBox
    {
        #region Properties
        /// <summary>
        /// 绘制水印文本
        /// </summary>
        private bool _IsDrawWaterMark;

        private string _WaterMarkText = string.Empty;
        /// <summary>
        /// 水印文本
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Aurora Style"), Description("水印文本")] 
        public string WaterMarkText
        {
            get { return this._WaterMarkText; }
            set
            {
                this._WaterMarkText = value.Trim();
                this.Invalidate();
            }
        }

        private Color _WaterMarkColor = Color.Empty;
        /// <summary>
        /// 水印文本颜色
        /// </summary>
        [Browsable(true)]
        [Category("Aurora Style"), Description("水印文本颜色")]
        public Color WaterMarkColor
        {
            get
            {
                if (this._WaterMarkColor == Color.Empty)
                    this._WaterMarkColor = Color.FromArgb(159, 159, 159);
                return this._WaterMarkColor;
            }
            set
            {
                this._WaterMarkColor = value;
                this.Invalidate();
            }
        }

        private Font _WaterMarkFont = SystemFonts.DefaultFont;
        /// <summary>
        /// 水印文本字体
        /// </summary>
        [Browsable(true)]
        [Category("Aurora Style"), Description("水印文本字体")]
        public Font WaterMarkFont
        {
            get { return this._WaterMarkFont; }
            set { this._WaterMarkFont = value; }
        }
        #endregion

        public AuroraWaterMarkTextBox()
        {
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer, true);
            this._IsDrawWaterMark = this.Text.Trim().Length == 0;
        }

        #region Override
        private const int OCM_COMMAND = 8465;
        private const int WM_PAINT = 15;
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg != WM_PAINT && m.Msg != OCM_COMMAND || (!this._IsDrawWaterMark || this.GetStyle(ControlStyles.UserPaint)))
                return;

            this.DrawWaterMark();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
        }

        protected override void OnTextAlignChanged(EventArgs e)
        {
            base.OnTextAlignChanged(e);
            this.Invalidate();
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            this._IsDrawWaterMark = this.Text.Trim().Length == 0;
            this.Invalidate();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (!this._IsDrawWaterMark)
                return;

            this.DrawWaterMark(e.Graphics);
        }
        #endregion

        /// <summary>
        /// 绘制水印
        /// </summary>
        private void DrawWaterMark()
        {
            using (Graphics graphics = this.CreateGraphics())
            {
                this.DrawWaterMark(graphics);
            }
        }

        /// <summary>
        /// 绘制水印
        /// </summary>
        private void DrawWaterMark(Graphics g)
        {
            TextFormatFlags textFormatFlags = TextFormatFlags.EndEllipsis | TextFormatFlags.NoPadding;
            Rectangle clientRectangle = this.ClientRectangle;
            switch (this.TextAlign)
            {
                case HorizontalAlignment.Left:
                    clientRectangle.Offset(1, 0);
                    break;
                case HorizontalAlignment.Right:
                    textFormatFlags |= TextFormatFlags.Right;
                    clientRectangle.Offset(-2, 0);
                    break;
                case HorizontalAlignment.Center:
                    textFormatFlags |= TextFormatFlags.HorizontalCenter;
                    clientRectangle.Offset(1, 0);
                    break;
            }
            SolidBrush solidBrush = new SolidBrush(this.WaterMarkColor);
            TextRenderer.DrawText(g, this.WaterMarkText, this.WaterMarkFont, clientRectangle, this.WaterMarkColor, this.BackColor, textFormatFlags);
        }
    }
}
