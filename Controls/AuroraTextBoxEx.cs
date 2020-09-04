using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AuroraFramework.Controls
{
    [ToolboxBitmap(typeof(TextBox))]
    public class AuroraTextBoxEx : Control
    {
        /// <summary>
        /// 文本框
        /// </summary>
        private AuroraWaterMarkTextBox _BaseTextBox;

        /// <summary>
        /// 错误提示器
        /// </summary>
        private ErrorProvider _BaseErrorProvider;

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

        /// <summary>
        /// 水印文本
        /// </summary>
        [Category("Aurora Style"), Description("水印文本")]
        public string WaterMarkText
        {
            get { return this._BaseTextBox.WaterMarkText; }
            set { this._BaseTextBox.WaterMarkText = value; }
        }

        /// <summary>
        /// 水印文本颜色
        /// </summary>
        [Category("Aurora Style"), Description("水印文本颜色")]
        public Color WaterMarkColor
        {
            get { return this._BaseTextBox.WaterMarkColor; }
            set { this._BaseTextBox.WaterMarkColor = value; }
        }

        /// <summary>
        /// 水印文本字体
        /// </summary>
        [Category("Aurora Style"),Description("水印文本字体")]
        public Font WaterMarkFont
        {
            get { return this._BaseTextBox.WaterMarkFont; }
            set { this._BaseTextBox.WaterMarkFont = value; }
        }

        private bool _UseErrorProvider = false;
        /// <summary>
        /// 使用错误提示器
        /// </summary>
        [Category("Aurora Style"), Description("使用错误提示器")]
        public bool UseErrorProvider
        {
            get { return this._UseErrorProvider; }
            set { this._UseErrorProvider = value; }
        }

        private Icon _Icon = null;
        /// <summary>
        /// 错误提示图标
        /// </summary>
        [Category("Aurora Style"), Description("错误提示图标")]
        public Icon Icon
        {
            get { return this._Icon; }
            set { this._Icon = value; this.Refresh(); }
        }

        private FormatCheckCollection _FormatCheckCollection = new FormatCheckCollection();
        /// <summary>
        /// 格式校验集合
        /// </summary>
        [Category("Aurora Style"), Description("格式校验集合")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public FormatCheckCollection FormatCheckCollection
        {
            get { return this._FormatCheckCollection; }
            set { this._FormatCheckCollection = value; this.Refresh(); }
        }

        private Image _Image = null;
        /// <summary>
        /// 图片
        /// </summary>
        [Category("Aurora Style"), Description("图片")]
        public Image Image
        {
            get { return this._Image; }
            set { this._Image = value; this.Refresh(); }
        }

        private ToolStripItemAlignment _ImageAlignment = ToolStripItemAlignment.Left;
        /// <summary>
        /// 图片放置的位置
        /// </summary>
        [Category("Aurora Style"), Description("图片放置的位置")]
        public ToolStripItemAlignment ImageAlignment
        {
            get { return this._ImageAlignment; }
            set
            {
                this._ImageAlignment = value;
                this.Refresh();
            }
        }

        /// <summary>
        /// 图片尺寸
        /// </summary>
        [Category("Aurora Style"), Description("图片尺寸")]
        protected Size ImageSize
        {
            get
            {
                if (this.Image == null)
                    return new Size(-1, -1);
                int height = this.Image.Height > this.ClientRectangle.Height ? this.ClientRectangle.Height : this.Image.Height;
                Size size = this.Image.Size;
                double imageHeight = (double)height / size.Height;
                Point point = new Point(1, 1);
                return new Size((int)(size.Width * imageHeight), (int)(size.Height * imageHeight));
            }
        }

        /// <summary>
        /// 控件是否在字符键入时修改其大小写格式
        /// </summary>
        [Category("Aurora Style"), Description("控件是否在字符键入时修改其大小写格式")]
        [DefaultValue(CharacterCasing.Normal)]
        public CharacterCasing CharacterCasing
        {
            get
            {
                return this._BaseTextBox.CharacterCasing;
            }
            set
            {
                this._BaseTextBox.CharacterCasing = value;
            }
        }

        /// <summary>
        /// 与控件关联的快捷菜单
        /// </summary>
        [Category("Aurora Style"), Description("与控件关联的快捷菜单")]
        public override ContextMenu ContextMenu
        {
            get { return this._BaseTextBox.ContextMenu; }
            set
            {
                this.ContextMenu = value;
                this._BaseTextBox.ContextMenu = value;
            }
        }

        /// <summary>
        /// 与此控件关联的 System.Windows.Forms.ContextMenuStrip
        /// </summary>
        [Category("Aurora Style"), Description("与此控件关联的 System.Windows.Forms.ContextMenuStrip")]
        public override ContextMenuStrip ContextMenuStrip
        {
            get { return this._BaseTextBox.ContextMenuStrip; }
            set
            {
                this.ContextMenuStrip = value;
                this._BaseTextBox.ContextMenuStrip = value;
            }
        }

        /// <summary>
        /// 此控件是否为多行文本框控件
        /// </summary>
        [Category("Aurora Style"), Description("此控件是否为多行文本框控件")]
        [DefaultValue(false)]
        public bool Multiline
        {
            get { return this._BaseTextBox.Multiline; }
            set { this._BaseTextBox.Multiline = value; }
        }

        /// <summary>
        /// 文本框中的当前文本
        /// </summary>
        [Category("Aurora Style"), Description("文本框中的当前文本")]
        public override string Text
        {
            get { return this._BaseTextBox.Text; }
            set { this._BaseTextBox.Text = value; }
        }

        /// <summary>
        /// 文本框控件中的文本的字符串数组
        /// </summary>
        [Category("Aurora Style"), Description("文本框控件中的文本的字符串数组")]
        public string[] Lines
        {
            get { return this._BaseTextBox.Lines; }
            set { this._BaseTextBox.Lines = value; }
        }

        /// <summary>
        /// 文本框中的文本是否为只读
        /// </summary>
        [Category("Aurora Style"), Description("文本框中的文本是否为只读")]
        [DefaultValue(false)]
        public bool ReadOnly
        {
            get { return this._BaseTextBox.ReadOnly; }
            set { this._BaseTextBox.ReadOnly = value; }
        }

        /// <summary>
        /// 用于屏蔽单行文本框控件中的密码字符
        /// </summary>
        [Category("Aurora Style"), Description("用于屏蔽单行文本框控件中的密码字符")]
        public char PasswordChar
        {
            get { return this._BaseTextBox.PasswordChar; }
            set { this._BaseTextBox.PasswordChar = value; }
        }

        /// <summary>
        /// 文本框控件中的文本是否应该以默认的密码字符显示
        /// </summary>
        [Category("Aurora Style"), Description("文本框控件中的文本是否应该以默认的密码字符显示")]
        [DefaultValue(false)]
        public bool UseSystemPasswordChar
        {
            get { return this._BaseTextBox.UseSystemPasswordChar; }
            set { this._BaseTextBox.UseSystemPasswordChar = value; }
        }

        /// <summary>
        /// 文本的对齐方式
        /// </summary>
        [Category("Aurora Style"), Description("文本的对齐方式")]
        [DefaultValue(HorizontalAlignment.Left)]
        public HorizontalAlignment TextAlign
        {
            get { return this._BaseTextBox.TextAlign; }
            set { this._BaseTextBox.TextAlign = value; }
        }

        /// <summary>
        /// 用户能否使用 Tab 键将焦点放到该控件上
        /// </summary>
        [Category("Aurora Style"), Description("用户能否使用 Tab 键将焦点放到该控件上")]
        [DefaultValue(true)]
        public new bool TabStop
        {
            get { return this._BaseTextBox.TabStop; }
            set { this._BaseTextBox.TabStop = value; }
        }

        /// <summary>
        /// 用户可在文本框控件中键入或粘贴的最大字符数
        /// </summary>
        [Category("Aurora Style"), Description("用户可在文本框控件中键入或粘贴的最大字符数")]
        public int MaxLength
        {
            get { return this._BaseTextBox.MaxLength; }
            set { this._BaseTextBox.MaxLength = value; }
        }

        /// <summary>
        /// 获取或设置哪些滚动条应出现在多行文本框控件中
        /// </summary>
        [Category("Aurora Style"), Description("获取或设置哪些滚动条应出现在多行文本框控件中")]
        public ScrollBars ScrollBars
        {
            get { return this._BaseTextBox.ScrollBars; }
            set { this._BaseTextBox.ScrollBars = value; }
        }

        private bool _WithError = false;
        /// <summary>
        /// 文本框文本格式是否错误
        /// </summary>
        [Category("Aurora Style"), Description("文本框文本格式是否错误")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool WithError
        {
            get { return this._WithError; }
            set
            {
                this._WithError = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// 文本框中选定的文本起始点
        /// </summary>
        [Browsable(false)]
        [Category("Aurora Style"), Description("文本框中选定的文本起始点")]
        public int SelectionStart
        {
            get { return this._BaseTextBox.SelectionStart; }
            set { this._BaseTextBox.SelectionStart = value; }
        }

        /// <summary>
        /// 文本框中选定的字符数
        /// </summary>
        [Browsable(false)]
        [Category("Aurora Style"), Description("文本框中选定的字符数")]
        public int SelectionLength
        {
            get { return this._BaseTextBox.SelectionLength; }
            set { this._BaseTextBox.SelectionLength = value; }
        }

        /// <summary>
        /// 控件中当前选定的文本
        /// </summary>
        [Browsable(false)]
        [Category("Aurora Style"), Description("控件中当前选定的文本")]
        public string SelectedText
        {
            get { return this._BaseTextBox.SelectedText; }
            set { this._BaseTextBox.Text = value; }
        }

        public AuroraTextBoxEx()
        {
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer, true);
            this.GotFocus += new EventHandler(this.SkinTextBoxEx_GotFocus);
            this.BackColor = SystemColors.Window;
            base.TabStop = false;
            this.CreateBaseTextBox();
            this.UpdateBaseTextBox();
            this.AddEventHandler();
        }

        #region Event Handler
        public event EventHandler AcceptsTabChanged;

        /// <summary>
        /// 添加事件处理程序
        /// </summary>
        private void AddEventHandler()
        {
            this._BaseTextBox.AcceptsTabChanged += new EventHandler(this.BaseTextBoxAcceptsTabChanged);
            this._BaseTextBox.CausesValidationChanged += new EventHandler(this.BaseTextBoxCausesValidationChanged);
            this._BaseTextBox.ChangeUICues += new UICuesEventHandler(this.BaseTextBoxChangeUiCues);
            this._BaseTextBox.Click += new EventHandler(this.BaseTextBoxClick);
            this._BaseTextBox.ClientSizeChanged += new EventHandler(this.BaseTextBoxClientSizeChanged);
            this._BaseTextBox.ContextMenuChanged += new EventHandler(this.BaseTextBoxContextMenuChanged);
            this._BaseTextBox.ContextMenuStripChanged += new EventHandler(this.BaseTextBoxContextMenuStripChanged);
            this._BaseTextBox.CursorChanged += new EventHandler(this.BaseTextBoxCursorChanged);
            this._BaseTextBox.KeyDown += new KeyEventHandler(this.BaseTextBoxKeyDown);
            this._BaseTextBox.KeyPress += new KeyPressEventHandler(this.BaseTextBoxKeyPress);
            this._BaseTextBox.KeyUp += new KeyEventHandler(this.BaseTextBoxKeyUp);
            this._BaseTextBox.SizeChanged += new EventHandler(this.BaseTextBoxSizeChanged);
            this._BaseTextBox.TextChanged += new EventHandler(this.BaseTextBoxTextChanged);
            this._BaseTextBox.GotFocus += new EventHandler(this.BaseTextBox_GotFocus);
            this._BaseTextBox.LostFocus += new EventHandler(this.BaseTextBox_LostFocus);
        }

        private void BaseTextBoxAcceptsTabChanged(object sender, EventArgs e)
        {
            if (this.AcceptsTabChanged == null)
                return;
            this.AcceptsTabChanged((object)this, e);
        }

        private void BaseTextBoxCausesValidationChanged(object sender, EventArgs e)
        {
            this.OnCausesValidationChanged(e);
        }

        private void BaseTextBoxChangeUiCues(object sender, UICuesEventArgs e)
        {
            this.OnChangeUICues(e);
        }

        private void BaseTextBoxClick(object sender, EventArgs e)
        {
            this.OnClick(e);
        }

        private void BaseTextBoxClientSizeChanged(object sender, EventArgs e)
        {
            this.OnClientSizeChanged(e);
        }

        private void BaseTextBoxContextMenuChanged(object sender, EventArgs e)
        {
            this.OnContextMenuChanged(e);
        }

        private void BaseTextBoxContextMenuStripChanged(object sender, EventArgs e)
        {
            this.OnContextMenuStripChanged(e);
        }

        private void BaseTextBoxCursorChanged(object sender, EventArgs e)
        {
            this.OnCursorChanged(e);
        }

        private void BaseTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            this.OnKeyDown(e);
        }

        private void BaseTextBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            this.OnKeyPress(e);
        }

        private void BaseTextBoxKeyUp(object sender, KeyEventArgs e)
        {
            this.OnKeyUp(e);
        }

        private void BaseTextBoxSizeChanged(object sender, EventArgs e)
        {
            this.OnSizeChanged(e);
        }

        private void BaseTextBoxTextChanged(object sender, EventArgs e)
        {
            this.OnTextChanged(e);
            this.Invalidate();
        }

        private void BaseTextBox_GotFocus(object sender, EventArgs e)
        {
            this.Invalidate();
            this.InvokeGotFocus(this, e);
        }

        private void BaseTextBox_LostFocus(object sender, EventArgs e)
        {
            CheckTextBoxText();
            this.Invalidate();
            this.InvokeLostFocus(this, e);
        }
        #endregion

        #region TextBox Method
        public void Select(int start, int length)
        {
            this._BaseTextBox.Select(start, length);
        }

        public void SelectAll()
        {
            this._BaseTextBox.SelectAll();
        }

        public void Clear()
        {
            this._BaseTextBox.Clear();
        }

        private void SkinTextBoxEx_GotFocus(object sender, EventArgs e)
        {
            this._BaseTextBox.Focus();
        }

        public void AppendText(string text)
        {
            this._BaseTextBox.AppendText(text);
        }
        #endregion     

        #region Override
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            try
            {
                this._BaseTextBox.BackColor = this.BackColor;

                if (this.BackColor.A == byte.MaxValue)
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
            this._BaseTextBox.ForeColor = this.ForeColor;

            if (this.WithError && this.UseErrorProvider)
            {
                if (this.BorderColor == Color.Red)
                    this.BorderColor = Color.Orange;
                else
                    this.BorderColor = Color.Red;
            }

            using (Pen pen = new Pen(this.BorderColor))
            {
                e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, this.Width - 2, this.Height - 1));
            }

            this.DrawIcon(e.Graphics);
        }

        public override void Refresh()
        {
            base.Refresh();
            this.UpdateBaseTextBox();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.UpdateBaseTextBox();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
        }
        #endregion

        private void CreateBaseTextBox()
        {
            if (this._BaseTextBox != null)
                return;
            this._BaseTextBox = new AuroraWaterMarkTextBox
            {
                BorderStyle = BorderStyle.None,
                Font = this.Font,
                Location = new Point(3, 3),
                Size = new Size(this.Width - 6, this.Height - 6)
            };
            this.Size = new Size(this._BaseTextBox.Width + 6, this._BaseTextBox.Height + 6);
            this._BaseTextBox.TabStop = true;
            this.Controls.Add(_BaseTextBox);

            if (this.UseErrorProvider)
            {
                if (this._BaseErrorProvider == null)
                {
                    this._BaseErrorProvider = new ErrorProvider(); 
                }

                if (this.Icon != null)
                    this._BaseErrorProvider.Icon = this.Icon;
            }
        }

        private void UpdateBaseTextBox()
        {
            if (this._BaseTextBox == null)
                return;
            this._BaseTextBox.Font = this.Font;
            if (this.Image != null)
            {
                Point point = new Point(this.ImageSize.Width + 10, 5);
                if (this.ImageAlignment == ToolStripItemAlignment.Right)
                    point = new Point(3, 3);
                this._BaseTextBox.Location = point;
                this._BaseTextBox.Size = new Size(this.Width - (20) - this.ImageSize.Width, this.Height - 6);
            }
            else
            {
                this._BaseTextBox.Location = new Point(3, 3);
                this._BaseTextBox.Size = new Size(this.Width - (6), this.Height - 6);
            }

            if (this.UseErrorProvider)
            {
                if (this._BaseErrorProvider == null)
                {
                    this._BaseErrorProvider = new ErrorProvider();
                }

                if (this.Icon != null)
                    this._BaseErrorProvider.Icon = this.Icon;
            }
        }

        private void DrawIcon(Graphics g)
        {
            if (this.Image != null)
            {
                Point location = new Point(this.ClientRectangle.X, this.ClientRectangle.Y);
                if (this.ImageAlignment == ToolStripItemAlignment.Right)
                    location = new Point(this.ClientRectangle.Width - this.ImageSize.Width - 1, 1);
                g.DrawImage(this.Image, new Rectangle(location, this.ImageSize));
                this.UpdateBaseTextBox();
            }
        }

        /// <summary>
        /// 校验文本框文本
        /// </summary>
        private void CheckTextBoxText()
        {
            this.WithError = false;
            if (this.UseErrorProvider)
            {
                foreach (FormatCheck error in this.FormatCheckCollection)
                {
                    Regex regex = new Regex(error.RegularExpression);
                    if (!regex.IsMatch(this.Text))
                    {
                        this.WithError = true;

                        if (this._BaseErrorProvider == null)
                        {
                            this._BaseErrorProvider = new ErrorProvider();
                        }

                        if (this.Icon != null)
                            this._BaseErrorProvider.Icon = this.Icon;

                        this._BaseErrorProvider.SetError(this, error.PromptText);
                        break;
                    }
                }
            }
        }
    }

    /// <summary>
    /// 错误提示
    /// </summary>
    public class FormatCheck
    {
        [Category("Skin ErrorPrompt Properties")]
        [Description("正则表达式")]
        public string RegularExpression { get; set; }

        [Category("Skin ErrorPrompt Properties")]
        [Description("提示文本")]
        public string PromptText { get; set; }

        [Category("Skin ErrorPrompt Properties")]
        [Description("描述")]
        public string Description { get; set; }
    }

    /// <summary>
    /// 错误提示集合
    /// </summary>
    public class FormatCheckCollection : CollectionBase
    {
        public FormatCheckCollection()
        {
            //List.Clear();

            //List.Add(new FormatCheck()
            //{
            //    RegularExpression = @".*[^\s]",
            //    PromptText = "必填项不能为空！",
            //    Description = "非空项验证"
            //});

            //List.Add(new FormatCheck()
            //{
            //    RegularExpression = @"^[0-9]*$",
            //    PromptText = "数值不为正整数或零！",
            //    Description = "正整数和零验证"
            //});

            //List.Add(new FormatCheck()
            //{
            //    RegularExpression = @"^[\u4e00-\u9fa5]{0,}$",
            //    PromptText = "包含非汉字文本！",
            //    Description = "汉字验证"
            //});

            //List.Add(new FormatCheck()
            //{
            //    RegularExpression = @"^[A-Za-z0-9]+$",
            //    PromptText = "不为英文或数字！",
            //    Description = "英文和数字验证"
            //});

            //List.Add(new FormatCheck()
            //{
            //    RegularExpression = @"^(13[0-9]|14[5|7]|15[0|1|2|3|4|5|6|7|8|9]|18[0|1|2|3|5|6|7|8|9])\d{8}$",
            //    PromptText = "手机号码格式错误！",
            //    Description = "手机号码验证"
            //});
        }

        public FormatCheck this[int index]
        {
            get { return (FormatCheck)List[index]; }
        }

        /// <summary>
        /// 向集合中添加项
        /// </summary>
        /// <param name="value">要添加到列表中的对象</param>
        public int Add(FormatCheck value)
        {
            return List.Add(value);
        }

        /// <summary>
        /// 在列表中的指定索引处插入项
        /// </summary>
        /// <param name="index">从零开始的索引，应在该位置插入 value</param>
        /// <param name="value">要插入到 列表中的对象</param>
        public void Insert(int index, FormatCheck value)
        {
            List.Insert(index, value);
        }

        /// <summary>
        /// 从列表中移除特定对象的第一个匹配项
        /// </summary>
        /// <param name="value">要从列表中移除的对象</param>
        public void Remove(FormatCheck value)
        {
            List.Remove(value);
        }

        /// <summary>
        /// 确定列表中特定项的索引
        /// </summary>
        /// <param name="value">要在列表中查找的对象</param>
        /// <returns>如果在列表中找到 value，则为该项的索引；否则为 -1</returns>
        public int IndexOf(FormatCheck value)
        {
            return List.IndexOf(value);
        }
    }

}
