namespace AuroraFramework.Drawing
{
    /// <summary>
    /// 标题栏
    /// </summary>
    public class AuroraTitleBar
    {
        private AuroraGradientColor _BackgroundColor = null;
        /// <summary>
        /// 标题栏背景颜色
        /// </summary>
        [System.ComponentModel.Description("标题栏背景颜色")]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content)]
        [System.ComponentModel.TypeConverter(typeof(AuroraGradientColorConverter))]
        [System.ComponentModel.Editor(typeof(AuroraFramework.Drawing.Design.AuroraNoneEditStyleEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public AuroraGradientColor BackgroundColor
        {
            get
            {
                if (this._BackgroundColor == null)
                    this._BackgroundColor = new AuroraGradientColor(System.Drawing.SystemColors.GradientInactiveCaption);
                return this._BackgroundColor;
            }
            set { this._BackgroundColor = value; }
        }

        private int _Height = 32;
        /// <summary>
        /// 标题栏高度
        /// </summary>
        [System.ComponentModel.Description("标题栏高度")]
        public int Height
        {
            get { return this._Height; }
            set
            {
                if (value >= 0)
                    this._Height = value;
            }
        }

        private string _Title = string.Empty;
        /// <summary>
        /// 标题栏文本
        /// </summary>
        [System.ComponentModel.Description("标题栏文本")]
        public string Title
        {
            get { return this._Title; }
            set { this._Title = value; }
        }


        private AuroraFramework.Forms.AuroraTitleBarTextAlignment _TextAlign = Forms.AuroraTitleBarTextAlignment.Center;
        /// <summary>
        /// 标题栏文本对齐方式
        /// </summary>
        [System.ComponentModel.Description("标题栏文本对齐方式")]
        public AuroraFramework.Forms.AuroraTitleBarTextAlignment TextAlign
        {
            get { return this._TextAlign; }
            set { this._TextAlign = value; }
        }

        private System.Drawing.Font _Font = null;
        /// <summary>
        /// 标题栏文本字体
        /// </summary>
        [System.ComponentModel.Description("标题栏文本字体")]
        public System.Drawing.Font Font
        {
            get
            {
                if (this._Font == null)
                    this._Font = System.Drawing.SystemFonts.CaptionFont;
                return this._Font;
            }
            set { this._Font = value; }
        }

        private System.Drawing.Color _ForeColor = System.Drawing.Color.Empty;
        /// <summary>
        /// 标题栏文本前景色
        /// </summary>
        [System.ComponentModel.Description("标题栏文本前景色")]
        public System.Drawing.Color ForeColor
        {
            get
            {
                if (this._ForeColor.IsEmpty)
                    this._ForeColor = System.Drawing.SystemColors.ControlText;
                return this._ForeColor;
            }
            set { this._ForeColor = value; }
        }

        private bool _ShowCrystalLogo = false;
        /// <summary>
        /// 标题栏水晶Logo可见性
        /// </summary>
        [System.ComponentModel.Description("标题栏水晶Logo可见性")]
        public bool ShowCrystalLogo
        {
            get { return this._ShowCrystalLogo; }
            set { this._ShowCrystalLogo = value; }
        }

        private int _OffsetX = 0;
        /// <summary>
        /// 标题栏文本X轴偏移量，仅当 TextAlign = Left 时生效
        /// </summary>
        [System.ComponentModel.Description("标题栏文本X轴偏移量，仅当 TextAlign = Left 时生效")]
        public int OffsetX
        {
            get { return this._OffsetX; }
            set
            {
                if (value >= 0)
                    this._OffsetX = value;
            }
        }

        private System.Drawing.Size _LogoSize = System.Drawing.Size.Empty;
        /// <summary>
        /// Logo尺寸
        /// </summary>
        [System.ComponentModel.Description("Logo尺寸")]
        public System.Drawing.Size LogoSize
        {
            get { return this._LogoSize; }
            set
            {
                if (value.Width > 0 && value.Height > 0)
                    this._LogoSize = value;
            }
        }

        /// <summary>
        /// 初始化<see cref="AuroraFramework.Drawing.AuroraTitleBar"/>结构的新实例
        /// </summary>
        public AuroraTitleBar()
        {
            this.BackgroundColor = new AuroraGradientColor(System.Drawing.Color.White);
            this.Height = 32;
            this.TextAlign = AuroraFramework.Forms.AuroraTitleBarTextAlignment.Center;
            this.Font = System.Drawing.SystemFonts.CaptionFont;
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ShowCrystalLogo = false;
            this.OffsetX = 0;
            this.LogoSize = new System.Drawing.Size(28, 28);
        }
    }
}
