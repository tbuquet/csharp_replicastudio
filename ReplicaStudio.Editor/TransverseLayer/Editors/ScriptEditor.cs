using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Design;
using System.ComponentModel;
using System.Windows.Forms.Design;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Editor.Forms;
using System.Windows.Forms;

namespace ReplicaStudio.Editor.TransverseLayer.Editors
{
    class ScriptEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService svc = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            VO_Script script = value as VO_Script;
            if (svc != null && script != null)
            {
                using (ScriptManagerContainer form = new ScriptManagerContainer())
                {
                    form.LoadScript(script);
                    if (svc.ShowDialog(form) == DialogResult.OK)
                    {
                        script = form.Script; // update object
                    }
                }
            }
            return value; // can also replace the wrapper object here
        }
    }
}
