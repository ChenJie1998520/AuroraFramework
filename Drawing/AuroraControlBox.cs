namespace AuroraFramework.Drawing
{
    /// <summary>
    /// 控制按钮
    /// </summary>
    public class AuroraControlBox
    {
        private System.Drawing.Size _Size = System.Drawing.Size.Empty;
        /// <summary>
        /// 控制按钮尺寸
        /// </summary>
        [System.ComponentModel.Description("控制按钮尺寸")]
        public System.Drawing.Size Size
        {
            get { return this._Size; }
            set { this._Size = value; }
        }

        private System.Drawing.Color _DefaultFlagColor = System.Drawing.Color.Empty;
        /// <summary>
        /// 默认图案标志颜色
        /// </summary>
        [System.ComponentModel.Description("默认图案标志颜色")]
        public System.Drawing.Color DefaultFlagColor
        {
            get { return this._DefaultFlagColor; }
            set { this._DefaultFlagColor = value; }
        }

        private System.Drawing.Color _HoverFlagColor = System.Drawing.Color.Empty;
        /// <summary>
        /// 悬停图案标志颜色
        /// </summary>
        [System.ComponentModel.Description("悬停图案标志颜色")]
        public System.Drawing.Color HoverFlagColor
        {
            get { return this._HoverFlagColor; }
            set { this._HoverFlagColor = value; }
        }

        private AuroraGradientColor _DefaultBackgroundColor = null;
        /// <summary>
        /// 默认背景颜色
        /// </summary>
        [System.ComponentModel.Description("默认背景颜色")]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content)]
        [System.ComponentModel.TypeConverter(typeof(AuroraGradientColorConverter))]
        [System.ComponentModel.Editor(typeof(AuroraFramework.Drawing.Design.AuroraNoneEditStyleEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public AuroraGradientColor DefaultBackgroundColor
        {
            get
            {
                if (this._DefaultBackgroundColor == null)
                    this._DefaultBackgroundColor = new AuroraGradientColor(System.Drawing.Color.Transparent);
                return this._DefaultBackgroundColor;
            }
            set { this._DefaultBackgroundColor = value; }
        }

        private AuroraGradientColor _HoverBackgroundColor = null;
        /// <summary>
        /// 悬停背景颜色
        /// </summary>
        [System.ComponentModel.Description("悬停背景颜色")]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content)]
        [System.ComponentModel.TypeConverter(typeof(AuroraGradientColorConverter))]
        [System.ComponentModel.Editor(typeof(AuroraFramework.Drawing.Design.AuroraNoneEditStyleEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public AuroraGradientColor HoverBackgroundColor
        {
            get
            {
                if (this._HoverBackgroundColor == null)
                    this._HoverBackgroundColor = new AuroraGradientColor(System.Drawing.SystemColors.GradientActiveCaption);
                return this._HoverBackgroundColor;
            }
            set { this._HoverBackgroundColor = value; }
        }

        private AuroraGradientColor _PressedBackgroundColor = null;
        /// <summary>
        /// 按下背景颜色
        /// </summary>
        [System.ComponentModel.Description("按下背景颜色")]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content)]
        [System.ComponentModel.TypeConverter(typeof(AuroraGradientColorConverter))]
        [System.ComponentModel.Editor(typeof(AuroraFramework.Drawing.Design.AuroraNoneEditStyleEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public AuroraGradientColor PressedBackgroundColor
        {
            get
            {
                if (this._PressedBackgroundColor == null)
                    this._PressedBackgroundColor = new AuroraGradientColor(System.Drawing.SystemColors.GradientActiveCaption);
                return this._PressedBackgroundColor;
            }
            set { this._PressedBackgroundColor = value; }
        }

        private AuroraGradientColor _ClosedHoverBackgroundColor = null;
        /// <summary>
        /// 关闭按钮悬停背景颜色
        /// </summary>
        [System.ComponentModel.Description("关闭按钮悬停背景颜色")]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content)]
        [System.ComponentModel.TypeConverter(typeof(AuroraGradientColorConverter))]
        [System.ComponentModel.Editor(typeof(AuroraFramework.Drawing.Design.AuroraNoneEditStyleEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public AuroraGradientColor ClosedHoverBackgroundColor
        {
            get
            {
                if (this._ClosedHoverBackgroundColor == null)
                    this._ClosedHoverBackgroundColor = new AuroraGradientColor(System.Drawing.Color.Red);
                return this._ClosedHoverBackgroundColor;
            }
            set { this._ClosedHoverBackgroundColor = value; }
        }

        private AuroraGradientColor _ClosedPressedBackgroundColor = null;
        /// <summary>
        /// 关闭按钮按下背景颜色
        /// </summary>
        [System.ComponentModel.Description("关闭按钮按下背景颜色")]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content)]
        [System.ComponentModel.TypeConverter(typeof(AuroraGradientColorConverter))]
        [System.ComponentModel.Editor(typeof(AuroraFramework.Drawing.Design.AuroraNoneEditStyleEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public AuroraGradientColor ClosedPressedBackgroundColor
        {
            get
            {
                if (this._ClosedPressedBackgroundColor == null)
                    this._ClosedPressedBackgroundColor = new AuroraGradientColor(System.Drawing.Color.Red);
                return this._ClosedPressedBackgroundColor;
            }
            set { this._ClosedPressedBackgroundColor = value; }
        }

        public AuroraControlBox()
        {
            this.Size = new System.Drawing.Size(40, 32);
            this.DefaultFlagColor = System.Drawing.Color.FromArgb(0, 0, 0);
            this.HoverFlagColor = System.Drawing.Color.FromArgb(255, 255, 255);
        }
    }
}
