using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace AuroraFramework.Forms.MessageBox
{
    public enum MessageMode
    {
        Info,
        Question,
        Warning,
        Error
    }

    public partial class AuroraMessageBoxForm : AuroraForm
    {
        #region Properties
        /// <summary>
        /// 窗体最大宽度
        /// </summary>
        private readonly int MaxWidth = 600;

        /// <summary>
        /// 窗体最大高度
        /// </summary>
        private readonly int MaxHeight = 400;

        /// <summary>
        /// 标题文本
        /// </summary>
        private string captionText;
        [Category("Aurora Style"), Description("标题文本")]
        public string CaptionText { get => this.captionText; set => this.captionText = value; }

        /// <summary>
        /// 消息文本
        /// </summary>
        private string message;
        [Category("Aurora Style"), Description("消息文本")]
        public string Message { get => this.message; set => this.message = value; }

        /// <summary>
        /// 消息模式
        /// </summary>
        private MessageMode messageMode;
        [Category("Aurora Style"), Description("消息模式")]
        public MessageMode MessageMode { get => this.messageMode; set => this.messageMode = value; }

        /// <summary>
        /// 消息模式
        /// </summary>
        private bool playSound;
        [Category("Aurora Style"), Description("播放声音")]
        public bool PlaySound { get => this.playSound; set => this.playSound = value; }
        #endregion

        public AuroraMessageBoxForm()
        {
            InitializeComponent();

            this.CaptionText = string.Empty;
            this.Message = string.Empty;
            this.MessageMode = MessageMode.Info;
            this.PlaySound = false;

            this.MaximumSize = new Size(this.MaxWidth, this.MaxHeight);
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width / 2 - this.Width / 2, Screen.PrimaryScreen.WorkingArea.Height / 2 - this.Height / 2);
        }

        public AuroraMessageBoxForm(string message) : this()
        {
            this.CaptionText = string.Empty;
            this.Message = string.IsNullOrWhiteSpace(message) ? string.Empty : message;
            this.ResetSize();
        }

        public AuroraMessageBoxForm(string captionText, string message) : this()
        {
            this.CaptionText = string.IsNullOrWhiteSpace(captionText) ? string.Empty : captionText;
            this.Message = string.IsNullOrWhiteSpace(message) ? string.Empty : message;
            this.ResetSize();
        }

        public AuroraMessageBoxForm(string captionText, string message, bool playSound) : this()
        {
            this.CaptionText = string.IsNullOrWhiteSpace(captionText) ? string.Empty : captionText;
            this.Message = string.IsNullOrWhiteSpace(message) ? string.Empty : message;
            this.PlaySound = playSound;
            this.ResetSize();
        }

        public AuroraMessageBoxForm(string message, MessageMode mode) : this()
        {
            this.CaptionText = string.Empty;
            this.Message = string.IsNullOrWhiteSpace(message) ? string.Empty : message;
            this.MessageMode = mode;
            this.ResetSize();
        }

        public AuroraMessageBoxForm(string message, MessageMode mode, bool playSound) : this()
        {
            this.CaptionText = string.Empty;
            this.Message = string.IsNullOrWhiteSpace(message) ? string.Empty : message;
            this.MessageMode = mode;
            this.PlaySound = playSound;
            this.ResetSize();
        }

        public AuroraMessageBoxForm(string captionText, string message, MessageMode mode) : this()
        {
            this.CaptionText = string.IsNullOrWhiteSpace(captionText) ? string.Empty : captionText;
            this.Message = string.IsNullOrWhiteSpace(message) ? string.Empty : message;
            this.MessageMode = mode;
            this.ResetSize();
        }

        public AuroraMessageBoxForm(string captionText, string message, MessageMode mode, bool playSound) : this()
        {
            this.CaptionText = string.IsNullOrWhiteSpace(captionText) ? string.Empty : captionText;
            this.Message = string.IsNullOrWhiteSpace(message) ? string.Empty : message;
            this.MessageMode = mode;
            this.PlaySound = playSound;
            this.ResetSize();
        }

        private void AuroraMessageBoxForm_Load(object sender, EventArgs e)
        {
            this.Text = this.CaptionText;
            this.lblMessage.Text = this.Message;
            switch (this.MessageMode)
            {
                case MessageMode.Info:
                    this.picImage.Image = Properties.Resources.Info;
                    btnCancel.Visible = false;
                    btnOK.Location = new Point((this.Width - btnOK.Width) / 2, btnOK.Location.Y);
                    this.PlaySoundEffects(Properties.Resources.InfoSound);
                    break;
                case MessageMode.Question:
                    this.picImage.Image = Properties.Resources.Question;
                    this.PlaySoundEffects(Properties.Resources.QuestionSound);
                    break;
                case MessageMode.Warning:
                    this.picImage.Image = Properties.Resources.Warning;
                    btnCancel.Visible = false;
                    btnOK.Location = new Point((this.Width - btnOK.Width) / 2, btnOK.Location.Y);
                    this.PlaySoundEffects(Properties.Resources.WarningSound);
                    break;
                case MessageMode.Error:
                    this.picImage.Image = Properties.Resources.Error;
                    btnCancel.Visible = false;
                    btnOK.Location = new Point((this.Width - btnOK.Width) / 2, btnOK.Location.Y);
                    this.PlaySoundEffects(Properties.Resources.ErrorSound);
                    break;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!e.Cancel)
            {
                base.OnFormClosing(e);
            }
        }

        /// <summary>
        /// 确定
        /// </summary>
        private void BtnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// 取消
        /// </summary>
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// 重置消息控件尺寸
        /// </summary>
        private void ResetSize()
        {
            Size msgSize = this.lblMessage.Size;
            Size minSize = TextRenderer.MeasureText("陈", this.Font);
            int maxLineCount = msgSize.Height / minSize.Height;
            int maxWordCount = (msgSize.Width * msgSize.Height) / (minSize.Width * minSize.Width);
            using (Graphics g = this.lblMessage.CreateGraphics())
            {
                g.MeasureString(this.Message, this.Font, new SizeF(msgSize.Width, this.MaxHeight), StringFormat.GenericDefault, out int wordCount, out int lineCount);

                if (lineCount > maxLineCount)
                {
                    this.Size = new SizeF(this.Size.Width, this.Size.Height + minSize.Height * (lineCount - maxLineCount)).ToSize();
                }

                if (wordCount > maxWordCount)
                {
                    float rate = this.Message.Length / maxWordCount;
                    rate = 1 + (rate - 1) / 8;
                    this.Size = new SizeF(this.Size.Width * rate, this.Size.Height * rate).ToSize();
                }
            }
        }

        /// <summary>
        /// 播放音效
        /// </summary>
        /// <param name="fileStream">文件流</param>
        private void PlaySoundEffects(Stream fileStream)
        {
            if (this.PlaySound)
            {
                using (SoundPlayer sp = new SoundPlayer())
                {
                    sp.Stream = fileStream;
                    sp.Play();
                }
            }
        }
    }
}
