namespace AuroraFramework.Drawing
{
    /// <summary>
    /// 渐变色
    /// </summary>
    public class AuroraGradientColor
    {
        private System.Drawing.Color _FromColor = System.Drawing.Color.Empty;
        /// <summary>
        /// 初始颜色
        /// </summary>
        [System.ComponentModel.Description("初始颜色")]
        public System.Drawing.Color FromColor
        {
            get
            {
                return this._FromColor;
            }
            set
            {
                this. _FromColor = value;
            }
        }

        private System.Drawing.Color _ToColor = System.Drawing.Color.Empty;
        /// <summary>
        /// 结束颜色
        /// </summary>
        [System.ComponentModel.Description("结束颜色")]
        public System.Drawing.Color ToColor
        {
            get
            {
                return this._ToColor;
            }
            set
            {
                this._ToColor = value;
            }
        }

        private System.Drawing.Drawing2D.LinearGradientMode _GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
        /// <summary>
        /// 线性渐变模式
        /// </summary>
        [System.ComponentModel.Description("线性渐变模式")]
        public System.Drawing.Drawing2D.LinearGradientMode GradientMode
        {
            get
            {
                return this._GradientMode;
            }
            set
            {
                this._GradientMode = value;
            }
        }

        private float[] _Factors = null;
        /// <summary>
        /// 色彩渲染系数(0到1的浮点数值)
        /// </summary>
        [System.ComponentModel.Description("色彩渲染系数(0到1的浮点数值)"), System.ComponentModel.Browsable(false)]
        public float[] Factors
        {
            get
            {
                if (this._Factors == null)
                    this._Factors = new float[] { };
                return this._Factors;
            }
            set
            {
                if (value != null)
                    this._Factors = value;
            }
        }

        private float[] _Positions = null;
        /// <summary>
        /// 色彩渲染位置(0到1的浮点数值)
        /// </summary>
        [System.ComponentModel.Description("色彩渲染位置(0到1的浮点数值)"), System.ComponentModel.Browsable(false)]
        public float[] Positions
        {
            get
            {
                if (this._Positions == null)
                    this._Positions = new float[] { };
                return this._Positions;
            }
            set
            {
                if (value != null)
                    this._Positions = value;
            }
        }

        /// <summary>
        /// 初始化<see cref="AuroraFramework.Forms.Control.AuroraGradientColor"/>结构的新实例
        /// </summary>
        public AuroraGradientColor()
        {
            this.FromColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ToColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.Factors = null;
            this.Positions = null;
        }

        /// <summary>
        /// 初始化<see cref="AuroraFramework.Forms.Control.AuroraGradientColor"/>结构的新实例
        /// </summary>
        /// <param name="color">颜色</param>
        public AuroraGradientColor(System.Drawing.Color color)
        {
            this.FromColor = color;
            this.ToColor = color;
            this.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.Factors = null;
            this.Positions = null;
        }

        /// <summary>
        /// 初始化<see cref="AuroraFramework.Forms.Control.AuroraGradientColor"/>结构的新实例
        /// </summary>
        /// <param name="fColor">初始颜色</param>
        /// <param name="tColor">结束颜色</param>
        public AuroraGradientColor(System.Drawing.Color fColor, System.Drawing.Color tColor)
        {
            this.FromColor = fColor;
            this.ToColor = tColor;
            this.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.Factors = null;
            this.Positions = null;
        }

        /// <summary>
        /// 初始化<see cref="AuroraFramework.Forms.ControlAuroraGradientColor"/>结构的新实例
        /// </summary>
        /// <param name="fColor">初始颜色</param>
        /// <param name="tColor">结束颜色</param>
        /// <param name="gradientMode">线性渐变模式</param>
        public AuroraGradientColor(System.Drawing.Color fColor, System.Drawing.Color tColor, System.Drawing.Drawing2D.LinearGradientMode gradientMode)
        {
            this.FromColor = fColor;
            this.ToColor = tColor;
            this.GradientMode = gradientMode;
            this.Factors = null;
            this.Positions = null;
        }

        /// <summary>
        /// 初始化<see cref="AuroraFramework.Forms.Control.AuroraGradientColor"/>结构的新实例
        /// </summary>
        /// <param name="fColor">初始颜色</param>
        /// <param name="tColor">结束颜色</param>
        /// <param name="factors">色彩渲染系数(0到1的浮点数值)</param>
        /// <param name="positions">色彩渲染位置(0到1的浮点数值)</param>
        public AuroraGradientColor(System.Drawing.Color fColor, System.Drawing.Color tColor, float[] factors, float[] positions)
        {
            this.FromColor = fColor;
            this.ToColor = tColor;
            this.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.Factors = factors ?? (new float[] { });
            this.Positions = positions ?? (new float[] { });
        }

        /// <summary>
        /// 初始化<see cref="AuroraFramework.Forms.Control.AuroraGradientColor"/>结构的新实例
        /// </summary>
        /// <param name="fColor">初始颜色</param>
        /// <param name="tColor">结束颜色</param>
        /// <param name="gradientMode">线性渐变模式</param>
        /// <param name="blend"><see cref="LinearGradientBrush"/>对象的混合图案</param>
        public AuroraGradientColor(System.Drawing.Color fColor, System.Drawing.Color tColor, System.Drawing.Drawing2D.LinearGradientMode gradientMode, System.Drawing.Drawing2D.Blend blend)
        {
            this.FromColor = fColor;
            this.ToColor = tColor;
            this.GradientMode = gradientMode;
            this.Factors = blend == null ? new float[] { } : (blend.Factors ?? (new float[] { }));
            this.Positions = blend == null ? new float[] { } : (blend.Positions ?? (new float[] { }));
        }

        /// <summary>
        /// 初始化<see cref="AuroraFramework.Forms.Control.AuroraGradientColor"/>结构的新实例
        /// </summary>
        /// <param name="fColor">初始颜色</param>
        /// <param name="tColor">结束颜色</param>
        /// <param name="gradientMode">线性渐变模式</param>
        /// <param name="factors">色彩渲染系数(0到1的浮点数值)</param>
        /// <param name="positions">色彩渲染位置(0到1的浮点数值)</param>
        public AuroraGradientColor(System.Drawing.Color fColor, System.Drawing.Color tColor, System.Drawing.Drawing2D.LinearGradientMode gradientMode, float[] factors, float[] positions)
        {
            this.FromColor = fColor;
            this.ToColor = tColor;
            this.GradientMode = gradientMode;
            this.Factors = factors ?? (new float[] { });
            this.Positions = positions ?? (new float[] { });
        }
    }
}
