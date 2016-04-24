using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using System.Windows.Forms;

namespace ReplicaStudio.Shared.TransverseLayer.VO
{
    [Serializable]
    public class VO_Terminology
    {
        #region Properties
        public string NewGame { get; set; }
        public string LoadGame { get; set; }
        public string Options { get; set; }
        public string LeaveGame { get; set; }
        public string SaveGame { get; set; }
        public string ReturnTitle { get; set; }
        public string SaveState { get; set; }
        public string ChoiceNext { get; set; }
        public string ChoicePrevious { get; set; }
        #endregion

        #region Constructors

        public VO_Terminology()
        {
            //Valeurs par défaut
        }
        #endregion

        #region Methods
        public void Update()
        {
            try
            {
                GameCore.Instance.Game.Terminology = (VO_Terminology)this.MemberwiseClone();
            }
            catch (Exception e)
            {
                MessageBox.Show(Errors.ERROR_UPDATE_VO + "Terminology :" + e.Message, Errors.ERROR_BOX_TITLE);
            }
        }

        public VO_Terminology Clone()
        {
            return (VO_Terminology)this.MemberwiseClone();
        }
        #endregion
    }
}
