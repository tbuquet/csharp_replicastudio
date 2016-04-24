using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReplicaStudio.Shared.ServiceLayer;
using ReplicaStudio.Viewer.BusinessLayer;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Viewer.TransverseLayer.Constants;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Viewer.TransverseLayer.VO;
using ReplicaStudio.Viewer.TransverseLayer.Algorithms;
using Microsoft.Xna.Framework;

namespace ReplicaStudio.Viewer.ServiceLayer
{
    /// <summary>
    /// Couche service de stage
    /// </summary>
    public class StageService : BaseService
    {
        #region Members
        /// <summary>
        /// Référence au business
        /// </summary>
        StageBusiness _Business;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur
        /// </summary>
        public StageService()
        {
            _Business = new StageBusiness();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Charge une scène
        /// </summary>
        /// <param name="id">Id de la scène</param>
        /// <returns>VO_Stage</returns>
        public VO_Stage GetStageData(Guid id)
        {
            VO_Stage stage = null;

            RunServiceTask(delegate
            {
                stage = _Business.GetStageData(id);
            }, ViewerErrors.STAGE_LOAD_MENU, id.ToString());

            return stage;
        }

        /// <summary>
        /// Charge les informations lourdes du stage
        /// </summary>
        public void PreLoadStage(VO_Stage stage, int matrixPrecision)
        {
            RunServiceTask(delegate
            {
                _Business.PreLoadStage(stage, matrixPrecision);
            }, ViewerErrors.STAGE_LOAD_MENU, stage.Title, matrixPrecision.ToString());
        }

        /// <summary>
        /// Retourne une animation
        /// </summary>
        /// <param name="id">Id de l'animation</param>
        /// <param name="type">Type de l'animation</param>
        /// <returns></returns>
        public VO_AnimatedSprite GetAnimatedSprite(Guid id, Enums.StageObjectType type)
        {
            VO_AnimatedSprite anim = null;

            RunServiceTask(delegate
            {
                anim = _Business.GetAnimatedSprite(id, type);
            }, ViewerErrors.STAGE_LOAD_MENU, false, id.ToString(), type.ToString());

            return anim;
        }

        /// <summary>
        /// Execute les scripts d'animation
        /// </summary>
        /// <param name="animStage"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool ExecuteAnimationScript(VO_StageAnimation animStage, Enums.TriggerExecutionType type)
        {
            bool anim = false;

            RunServiceTask(delegate
            {
                anim = _Business.ExecuteAnimationScript(animStage, type);
            }, ViewerErrors.STAGE_LOAD_MENU, false, animStage.ToString(), type.ToString());

            return anim;
        }

        /// <summary>
        /// Récupère le ratio actuel.
        /// </summary>
        /// /// <param name="point">Point à tester</param>
        /// <returns>Ratio</returns>
        public float GetRatioFromMatrix(Point point, int matrixPrecision)
        {
            float ratio = 1;

            RunServiceTask(delegate
            {
                ratio = _Business.GetRatioFromMatrix(point, matrixPrecision);
            }, ViewerErrors.STAGE_LOAD_MENU, false, point.ToString(), matrixPrecision.ToString());

            return ratio;
        }

        /// <summary>
        /// Récupère et traite l'event
        /// </summary>
        /// <param name="point"></param>
        /// <param name="matrixPrecision"></param>
        public bool GetEventFromMatrix(Point point, int matrixPrecision)
        {
            bool eventSpot = false;

            RunServiceTask(delegate
            {
                eventSpot = _Business.GetEventFromMatrix(point, matrixPrecision);
            }, ViewerErrors.STAGE_LOAD_MENU, false, point.ToString(), matrixPrecision.ToString());

            return eventSpot;
        }

        /// <summary>
        /// Click sur Event
        /// </summary>
        /// <param name="point"></param>
        /// <param name="matrixPrecision"></param>
        public bool ExecuteClickedEvent(Point point, int matrixPrecision)
        {
            bool output = false;

            RunServiceTask(delegate
            {
                output = _Business.ExecuteClickedEvent(point, matrixPrecision);
            }, ViewerErrors.STAGE_LOAD_MENU, point.ToString(), matrixPrecision.ToString());

            return output;
        }

        /// <summary>
        /// Check les evenements s'executant automatiquement.
        /// </summary>
        public void CheckParallelProcessAndAutomaticAndContactScripts(int matrixPrecision)
        {
            RunServiceTask(delegate
            {
                _Business.CheckParallelProcessAndAutomaticAndContactScripts(matrixPrecision);
            }, ViewerErrors.STAGE_LOAD_MENU, false, matrixPrecision.ToString());
        }

        /// <summary>
        /// Récupère le CharacterSprite en fonction d'un characterId
        /// </summary>
        /// <param name="characterId"></param>
        /// <returns></returns>
        public VO_CharacterSprite GetCharacterSprite(Guid characterId)
        {
            VO_CharacterSprite character = null;

            RunServiceTask(delegate
            {
                character = _Business.GetCharacterSprite(characterId);
            }, ViewerErrors.STAGE_LOAD_MENU, characterId.ToString());

            return character;
        }

        /// <summary>
        /// Récupère l'index du calque en fonction de la position d'un personnage
        /// </summary>
        /// <param name="coords"></param>
        /// <returns></returns>
        public int GetLayerIndexFromCharacterLocation(Point coords)
        {
            int output = 0;

            RunServiceTask(delegate
            {
                output = _Business.GetLayerIndexFromCharacterLocation(coords);
            }, ViewerErrors.STAGE_LOAD_MENU, false, coords.ToString());

            return output;
        }

        /// <summary>
        /// Prépare un perso à être dessiné.
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public VO_CharacterSprite DrawCharacter(VO_StageCharacter character)
        {
            VO_CharacterSprite characterSprite = null;

            RunServiceTask(delegate
            {
                characterSprite = _Business.DrawCharacter(character);
            }, ViewerErrors.STAGE_LOAD_MENU, false, character.ToString());

            return characterSprite;
        }

        /// <summary>
        /// Prépare une animation à être dessinée
        /// </summary>
        /// <param name="animation"></param>
        /// <returns></returns>
        public VO_AnimatedSprite DrawAnimated(VO_StageAnimation animation)
        {
            VO_AnimatedSprite animSprite = null;

            RunServiceTask(delegate
            {
                animSprite = _Business.DrawAnimated(animation);
            }, ViewerErrors.STAGE_LOAD_MENU, false, animation.ToString());

            return animSprite;
        }

        /// <summary>
        /// Format Text
        /// </summary>
        /// <param name="message">VO_message du message</param>
        /// <returns>Sprite de texte</returns>
        public List<VO_String2D> FormatText(VO_Message message, VO_Size container, Point camera)
        {
            List<VO_String2D> sprite = null;

            RunServiceTask(delegate
            {
                sprite = _Business.FormatText(message, container, camera);
            }, ViewerErrors.STAGE_LOAD_MENU, false, message.ToString(), container.ToString(), camera.ToString());

            return sprite;
        }

        /// <summary>
        /// Fixer la caméra à un personnage
        /// </summary>
        /// <param name="character">Personnage</param>
        /// <returns>Position</returns>
        public Vector2 GetCameraCoords(Point location)
        {
            Vector2 rect = new Vector2();

            RunServiceTask(delegate
            {
                rect = _Business.GetCameraCoords(location);
            }, ViewerErrors.STAGE_LOAD_MENU, false, location.ToString());

            return rect;
        }

        /// <summary>
        /// Récupère les faces à afficher du dialogue en cours
        /// </summary>
        /// <param name="dialog">Dialog</param>
        /// <returns>Listes des faces</returns>
        public List<VO_AnimatedSprite> GetAnimatedFaces(VO_Dialog dialog)
        {
            List<VO_AnimatedSprite> list = null;

            RunServiceTask(delegate
            {
                list = _Business.GetAnimatedFaces(dialog);
            }, ViewerErrors.STAGE_LOAD_MENU, false, dialog.ToString());

            return list;
        }

        /// <summary>
        /// Récupère le stage courant
        /// </summary>
        /// <returns></returns>
        public VO_Stage GetCurrentStage()
        {
            VO_Stage stage = null;

            RunServiceTask(delegate
            {
                stage = _Business.GetCurrentStage();
            }, ViewerErrors.STAGE_LOAD_MENU, false);

            return stage;
        }
        #endregion
    }
}
