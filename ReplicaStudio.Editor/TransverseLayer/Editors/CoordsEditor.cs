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
    class CoordsEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService svc = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            VO_Coords ct = value as VO_Coords;
            if (svc != null && ct != null)
            {
                using (CoordsManager form = new CoordsManager())
                {
                    form.SourceObject = new System.Drawing.Rectangle(ct.Location, new System.Drawing.Size(0, 0));
                    form.UseStageBackground = true;
                    if (svc.ShowDialog(form) == DialogResult.OK)
                    {
                        ct = form.DestinationObject; // update object
                        value = form.DestinationObject;
                    }
                }
            }
            return value; // can also replace the wrapper object here
        }
    }
}
