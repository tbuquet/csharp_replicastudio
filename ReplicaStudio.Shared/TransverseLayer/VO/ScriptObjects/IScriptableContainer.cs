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
    public interface IScriptableContainer
    {
        List<VO_Line> GetLines(string code);
    }
}
