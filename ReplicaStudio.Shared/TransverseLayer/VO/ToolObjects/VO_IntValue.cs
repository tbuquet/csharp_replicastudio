using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.DatasLayer;

namespace ReplicaStudio.Shared.TransverseLayer.VO
{
    [Serializable]
    public class VO_IntValue
    {
        #region Properties
        public Guid VariableValue { get; set; }
        public int IntValue { get; set; }
        #endregion

        #region Constructors
        public VO_IntValue()
        {
        }

        public VO_IntValue(int intValue)
        {
            IntValue = intValue;
            VariableValue = Guid.Empty;
        }

        public VO_IntValue(Guid variableValue)
        {
            VariableValue = variableValue;
            IntValue = 0;
        }
        #endregion
    }
}

