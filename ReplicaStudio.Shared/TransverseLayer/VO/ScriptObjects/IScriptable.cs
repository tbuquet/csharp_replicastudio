using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ReplicaStudio.Shared.TransverseLayer.VO
{
    /// <summary>
    /// Interface permettant de gérer les rendus
    /// </summary>
    public interface IScriptable
    {
        #region Properties

        bool Valid { get; set; }

        #endregion

        #region Methods

        List<TreeNode> RenderInScriptManager(string code);
        bool IsScriptValid();
        IScriptable Clone();

        #endregion
    }
}
