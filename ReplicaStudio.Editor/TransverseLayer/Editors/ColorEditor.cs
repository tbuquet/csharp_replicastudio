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
    class ColorEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService svc = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            VO_ColorTransformation ct = value as VO_ColorTransformation;
            if (svc != null && ct != null)
            {
                using (ImageColorManager form = new ImageColorManager())
                {
                    form.OriginalColorTransformations = ct;
                    if (svc.ShowDialog(form) == DialogResult.OK)
                    {
                        ct = form.OriginalColorTransformations; // update object
                    }
                }
            }
            return value; // can also replace the wrapper object here
        }
    }
}
