// -----------------------------------------------------------------------
// <copyright file="TreeViewColorTool.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace ReplicaStudio.Shared.TransverseLayer.Tools
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Runtime.Serialization;
    using System.Drawing;
    using System.IO;
    using System.Runtime.Serialization.Json;

    /// <summary>
    /// Class permettant de définir un couple Text/Couleur pour le rendu d'un TreeNode
    /// </summary>
    [DataContract]
    public class TreeViewColorTool
    {
        #region Properties

        [DataMember]
        public List<string[]> TextColorList = null;

        #endregion

        #region Constructor

        public TreeViewColorTool()
        {
            TextColorList = new List<string[]>();
        }

        #endregion

        #region Method

        public void AddNewColorAndText(string HexaColor, string Text)
        {
            string[] TextAndColor = new string[2];

            TextAndColor[0] = HexaColor;
            TextAndColor[1] = Text;

            TextColorList.Add(TextAndColor);
        }

        /// <summary>
        /// Conversion de la liste des couple Text/Couleur vers JSON (string)
        /// </summary>
        public string GetJsonisedObject()
        {
            MemoryStream stream1 = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(TreeViewColorTool));

            ser.WriteObject(stream1, this);
            stream1.Seek(0, SeekOrigin.Begin);

            StreamReader reader = new StreamReader(stream1);
            string contents = reader.ReadToEnd();

            return contents;
        }

        #endregion
    }
}
