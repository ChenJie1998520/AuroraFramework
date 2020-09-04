namespace AuroraFramework.Drawing
{
    /// <summary>
    /// 自定义控制按钮
    /// </summary>
    public class AuroraCustomControlBox
    {
        private bool _Visibale = true;
        /// <summary>
        /// 控件可见性
        /// </summary>
        [System.ComponentModel.Description("控件可见性")]
        public bool Visibale
        {
            get { return this._Visibale; }
            set { this._Visibale = value; }
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
                    this._HoverBackgroundColor = new AuroraGradientColor(System.Drawing.SystemColors.GradientInactiveCaption);
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
                    this._PressedBackgroundColor = new AuroraGradientColor(System.Drawing.SystemColors.GradientInactiveCaption);
                return this._PressedBackgroundColor;
            }
            set { this._PressedBackgroundColor = value; }
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

        private System.Drawing.Bitmap _Image = null;
        /// <summary>
        /// 控件图片
        /// </summary>
        [System.ComponentModel.Description("控件图片")]
        public System.Drawing.Bitmap Image
        {
            get { return this._Image; }
            set { this._Image = value; }
        }

        private System.Drawing.Size _ImageSize = System.Drawing.Size.Empty;
        /// <summary>
        /// 图片尺寸
        /// </summary>
        [System.ComponentModel.Description("图片尺寸")]
        public System.Drawing.Size ImageSize
        {
            get { return this._ImageSize; }
            set
            {
                if (value.Width > 0 && value.Height > 0)
                    this._ImageSize = value;
            }
        }

        private System.Windows.Forms.ContextMenuStrip _ContextMenuStrip = null;
        /// <summary>
        /// 控件快捷菜单
        /// </summary>
        [System.ComponentModel.Description("控件快捷菜单")]
        public System.Windows.Forms.ContextMenuStrip ContextMenuStrip
        {
            get { return this._ContextMenuStrip; }
            set { this._ContextMenuStrip = value; }
        }

        private AuroraFramework.Forms.AuroraControlBoxStatus _ControlBoxStatus = AuroraFramework.Forms.AuroraControlBoxStatus.Default;
        /// <summary>
        /// 控件状态
        /// </summary>
        [System.ComponentModel.Description("控件状态")]
        internal AuroraFramework.Forms.AuroraControlBoxStatus ControlBoxStatus
        {
            get { return this._ControlBoxStatus; }
            set { this._ControlBoxStatus = value; }
        }

        //声明一个委托
        public delegate void ClickEventHandler(object sender, System.Windows.Forms.MouseEventArgs e);

        [System.ComponentModel.Description("当鼠标指针移过组件时发生"), System.ComponentModel.Category("Aurora Custom Event")]
        public System.Windows.Forms.MouseEventHandler CustomMouseMove;
        public virtual void OnCustomMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.CustomMouseMove?.Invoke(sender, e);
        }

        [System.ComponentModel.Description("当鼠标在控件内保持静止达一段时间时发生"), System.ComponentModel.Category("Aurora Custom Event")]
        public System.EventHandler CustomMouseHover;
        public virtual void OnCustomMouseHover(object sender, System.EventArgs e)
        {
            this.CustomMouseHover?.Invoke(sender, e);
        }

        [System.ComponentModel.Description("当鼠标指针在组件上方并按下鼠标按钮时发生")]
        public System.Windows.Forms.MouseEventHandler CustomMouseDown;
        public virtual void OnCustomMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.CustomMouseDown?.Invoke(sender, e);
        }

        [System.ComponentModel.Description("在鼠标指针在组件上方释放鼠标按钮时发生")]
        public System.Windows.Forms.MouseEventHandler CustomMouseUp;
        public virtual void OnCustomMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.CustomMouseUp?.Invoke(sender, e);
        }

        [System.ComponentModel.Description("单击组件时发生")]
        public System.EventHandler CustomClick;
        public virtual void OnCustomClick(object sender, System.EventArgs e)
        {
            this.CustomClick?.Invoke(sender, e);
        }

        /// <summary>
        /// 初始化<see cref="AuroraCustomControlBox"/>结构的新实例
        /// </summary>
        public AuroraCustomControlBox()
        {
            this.Visibale = true;
            this.DefaultFlagColor = System.Drawing.Color.FromArgb(0, 0, 0);
            this.HoverFlagColor = System.Drawing.Color.FromArgb(255, 255, 255);

            //string name = System.Reflection.Assembly.GetCallingAssembly().GetName().Name + ".Images.Image.png";
            //System.IO.Stream manifestResourceStream = System.Reflection.Assembly.GetCallingAssembly().GetManifestResourceStream(name);
            //if (manifestResourceStream != null)
            //{
            //    this.Image = (System.Drawing.Bitmap)System.Drawing.Image.FromStream(manifestResourceStream);
            //    manifestResourceStream.Close();
            //}
            this.Image = AuroraFramework.Properties.Resources.Image;
            this.ImageSize = new System.Drawing.Size(14, 14);
            this.ContextMenuStrip = null;
        }
    }
}
