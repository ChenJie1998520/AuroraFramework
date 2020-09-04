namespace AuroraFramework.Drawing.Design
{
    /// <summary>
    /// 不提供任何交互用户界面 (UI) 组件编辑器
    /// </summary>
    public class AuroraNoneEditStyleEditor : System.Drawing.Design.UITypeEditor
    {
        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            if (context != null && context.Instance != null)
            {
                return System.Drawing.Design.UITypeEditorEditStyle.None;
            }

            return base.GetEditStyle(context);
        }
    }
}
