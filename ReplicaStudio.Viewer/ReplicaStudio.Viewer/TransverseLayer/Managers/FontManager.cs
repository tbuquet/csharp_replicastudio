using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Viewer.TransverseLayer.VO;
using ReplicaStudio.Viewer.TransverseLayer.Constants;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace ReplicaStudio.Viewer.TransverseLayer.Managers
{
    /// <summary>
    /// Classe de gestion des fonts
    /// </summary>
    public class FontManager
    {
        #region Members
        /// <summary>
        /// Liste des sprites des actions
        /// </summary>
        private static Dictionary<string, Dictionary<int, SpriteFont>> _Fonts;
        #endregion

        #region Properties
        /// <summary>
        /// SpriteFont Debug
        /// </summary>
        public static SpriteFont Debug
        {
            get;
            set;
        }

        /// <summary>
        /// SpriteFont Debug
        /// </summary>
        public static SpriteFont LogDebug
        {
            get;
            set;
        }
        #endregion

        #region Methods
        public static SpriteFont GetSpriteFont(string name, int size)
        {
            if(_Fonts != null)
                 if (_Fonts.ContainsKey(name) && _Fonts[name].ContainsKey(size))
                     return _Fonts[name][size];
            return null;
        }

        public static void AddSpriteFont(SpriteFont spriteFont, string name, int size)
        {
            if (_Fonts == null)
                _Fonts = new Dictionary<string, Dictionary<int, SpriteFont>>();
            if (!_Fonts.ContainsKey(name))
                _Fonts.Add(name, new Dictionary<int, SpriteFont>());
            if (!_Fonts[name].ContainsKey(size))
                _Fonts[name].Add(size, spriteFont);
        }

        public static void LoadFonts(ContentManager contentManager, List<string> debug)
        {
            FontManager.AddSpriteFont(contentManager.Load<SpriteFont>(@"Fonts\Arial\Arial8"), "Arial", 8);
            FontManager.AddSpriteFont(contentManager.Load<SpriteFont>(@"Fonts\Arial\Arial9"), "Arial", 9);
            FontManager.AddSpriteFont(contentManager.Load<SpriteFont>(@"Fonts\Arial\Arial10"), "Arial", 10);
            FontManager.AddSpriteFont(contentManager.Load<SpriteFont>(@"Fonts\Arial\Arial11"), "Arial", 11);
            FontManager.AddSpriteFont(contentManager.Load<SpriteFont>(@"Fonts\Arial\Arial12"), "Arial", 12);
            FontManager.AddSpriteFont(contentManager.Load<SpriteFont>(@"Fonts\Arial\Arial14"), "Arial", 14);
            FontManager.AddSpriteFont(contentManager.Load<SpriteFont>(@"Fonts\Arial\Arial16"), "Arial", 16);
            FontManager.AddSpriteFont(contentManager.Load<SpriteFont>(@"Fonts\Arial\Arial20"), "Arial", 20);
            FontManager.AddSpriteFont(contentManager.Load<SpriteFont>(@"Fonts\Arial\Arial24"), "Arial", 24);
            FontManager.AddSpriteFont(contentManager.Load<SpriteFont>(@"Fonts\Arial\Arial28"), "Arial", 28);
            FontManager.AddSpriteFont(contentManager.Load<SpriteFont>(@"Fonts\Arial\Arial32"), "Arial", 32);
            FontManager.AddSpriteFont(contentManager.Load<SpriteFont>(@"Fonts\Arial\Arial36"), "Arial", 36);
            FontManager.AddSpriteFont(contentManager.Load<SpriteFont>(@"Fonts\Arial\Arial42"), "Arial", 42);

            /*FontManager.AddSpriteFont(contentManager.Load<SpriteFont>(@"Arial8"), "Arial", 8);
            FontManager.AddSpriteFont(contentManager.Load<SpriteFont>(@"Arial9"), "Arial", 9);
            FontManager.AddSpriteFont(contentManager.Load<SpriteFont>(@"Arial10"), "Arial", 10);
            FontManager.AddSpriteFont(contentManager.Load<SpriteFont>(@"Arial11"), "Arial", 11);
            FontManager.AddSpriteFont(contentManager.Load<SpriteFont>(@"Arial12"), "Arial", 12);
            FontManager.AddSpriteFont(contentManager.Load<SpriteFont>(@"Arial14"), "Arial", 14);
            FontManager.AddSpriteFont(contentManager.Load<SpriteFont>(@"Arial16"), "Arial", 16);
            FontManager.AddSpriteFont(contentManager.Load<SpriteFont>(@"Arial20"), "Arial", 20);
            FontManager.AddSpriteFont(contentManager.Load<SpriteFont>(@"Arial24"), "Arial", 24);
            FontManager.AddSpriteFont(contentManager.Load<SpriteFont>(@"Arial28"), "Arial", 28);
            FontManager.AddSpriteFont(contentManager.Load<SpriteFont>(@"Arial32"), "Arial", 32);
            FontManager.AddSpriteFont(contentManager.Load<SpriteFont>(@"Arial36"), "Arial", 36);
            FontManager.AddSpriteFont(contentManager.Load<SpriteFont>(@"Arial42"), "Arial", 42);*/

            FontManager.Debug = contentManager.Load<SpriteFont>(@"Fonts\Debug\Debug");
            FontManager.LogDebug = contentManager.Load<SpriteFont>(@"Fonts\Debug\Debug2");
        }
        #endregion
    }
}
