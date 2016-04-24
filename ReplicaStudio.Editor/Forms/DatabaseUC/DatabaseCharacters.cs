using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Editor.ServiceLayer;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using ReplicaStudio.Editor.TransverseLayer;

namespace ReplicaStudio.Editor.Forms.DatabaseUC
{
    /// <summary>
    /// Formulaire Characters de la database
    /// </summary>
    public partial class DatabaseCharacters : UserControl
    {
        #region Members
        /// <summary>
        /// Référence au service
        /// </summary>
        CharacterService _Service;
        #endregion

        #region Properties
        /// <summary>
        /// Character courant
        /// </summary>
        public VO_Character CurrentCharacter { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur principal
        /// </summary>
        public DatabaseCharacters()
        {
            InitializeComponent();
            _Service = new CharacterService();

            ddpSpeed.Minimum = GlobalConstants.CHARACTERS_MIN_SPEED;
            ddpSpeed.Maximum = GlobalConstants.CHARACTERS_MAX_SPEED;
            ddpSpeed.Value = GlobalConstants.CHARACTERS_NORMAL_SPEED;
            ListCharacters.Title = Culture.Language.DatabaseRessources.DatabaseCharacters;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Méthode qui charge l'animation courante
        /// </summary>
        public void LoadCharacter(Guid guid)
        {
            Cursor.Current = Cursors.WaitCursor;

            //Suppression des eventhandler
            this.txtName.LostFocus -= new System.EventHandler(this.txtName_TextChanged);
            this.ddpSpeed.ValueChanged -= new System.EventHandler(this.ddpSpeed_ValueChanged);

            //Code de chargement
            CurrentCharacter = GameCore.Instance.GetCharacterById(guid);
            txtName.Text = CurrentCharacter.Title;
            ddpSpeed.Value = CurrentCharacter.Speed;
            AnimCharacter.ParentCharacter = CurrentCharacter.Id;
            ColorDialog.Color = FormsTools.GetGDIColorFromVOColor(CurrentCharacter.DialogColor);
            if (CurrentCharacter.Face != new Guid())
            {
                AnimFace.LoadAnimation(CurrentCharacter.Face);
                AnimFace.Start();
            }
            else
                AnimFace.LoadAnimation(new Guid());
            if (CurrentCharacter.TalkingFace != new Guid())
            {
                TalkingFace.LoadAnimation(CurrentCharacter.TalkingFace);
                TalkingFace.Start();
            }
            else
                TalkingFace.LoadAnimation(new Guid());

            LoadLists();
            if (CurrentCharacter.StandingAnim != new Guid())
            {
                AnimCharacter.UseCustomRow = true;
                AnimCharacter.Row = 2;
                AnimCharacter.LoadAnimation(CurrentCharacter.StandingAnim);
                AnimCharacter.Start();
            }
            else
                AnimCharacter.LoadAnimation(new Guid());

            this.txtName.LostFocus += new System.EventHandler(this.txtName_TextChanged);
            this.ddpSpeed.ValueChanged += new System.EventHandler(this.ddpSpeed_ValueChanged);

            grpAnimations.Visible = true;
            grpInformations.Visible = true;
            grpDialogs.Visible = true;

            Cursor.Current = DefaultCursor;
        }
       
        /// <summary>
        /// Charger les listes propres à CharacterDB
        /// </summary>
        private void LoadLists()
        {
            this.ddpStandingAnim.SelectedIndexChanged -= new System.EventHandler(this.ddpStandingAnim_SelectedIndexChanged);
            this.ddpWalkingAnim.SelectedIndexChanged -= new System.EventHandler(this.ddpWalkingAnim_SelectedIndexChanged);
            this.ddpTalkingAnim.SelectedIndexChanged -= new EventHandler(ddpTalkingAnim_SelectedIndexChanged);

            List<VO_Base> list = _Service.GetCharacterAnimationList(CurrentCharacter.Id);
            ddpStandingAnim.DataSource = list;
            ddpStandingAnim.DisplayMember = "Title";
            ddpStandingAnim.ValueMember = "Id";
            ddpStandingAnim.SelectedValue = CurrentCharacter.StandingAnim;
            List<VO_Base> list2 = _Service.GetCharacterAnimationList(CurrentCharacter.Id);
            ddpWalkingAnim.DataSource = list2;
            ddpWalkingAnim.DisplayMember = "Title";
            ddpWalkingAnim.ValueMember = "Id";
            ddpWalkingAnim.SelectedValue = CurrentCharacter.WalkingAnim;
            List<VO_Base> list3 = _Service.GetCharacterAnimationList(CurrentCharacter.Id);
            ddpTalkingAnim.DataSource = list3;
            ddpTalkingAnim.DisplayMember = "Title";
            ddpTalkingAnim.ValueMember = "Id";
            ddpTalkingAnim.SelectedValue = CurrentCharacter.TalkingAnim;

            this.ddpStandingAnim.SelectedIndexChanged += new System.EventHandler(this.ddpStandingAnim_SelectedIndexChanged);
            this.ddpWalkingAnim.SelectedIndexChanged += new System.EventHandler(this.ddpWalkingAnim_SelectedIndexChanged);
            this.ddpTalkingAnim.SelectedIndexChanged += new EventHandler(ddpTalkingAnim_SelectedIndexChanged);
        }

        /// <summary>
        /// Charge la liste de characters
        /// </summary>
        private void ProvisionList()
        {
            ListCharacters.DataSource = _Service.ProvisionList();
            ListCharacters.LoadList();
        }

        /// <summary>
        /// Survient lorsque le formulaire devient visible
        /// </summary>
        public void InitializeDBCharacters()
        {
            CurrentCharacter = null;
            ProvisionList();
            if (ListCharacters.DataSource.Count > 0)
            {
                Guid firstAnimation = ListCharacters.DataSource[0].Id;
                ListCharacters.SelectItem(firstAnimation);
                LoadCharacter(firstAnimation);
            }
            else
                ListCharacters_ListIsEmpty(this, new EventArgs());
        }
        #endregion

        #region EventHandlers
        /// <summary>
        /// Bouton qui affiche la ColorDialog pour le choix de couleur de dialogue.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChoose_Click(object sender, EventArgs e)
        {
            ColorDialog.ShowDialog();
            CurrentCharacter.DialogColor = FormsTools.GetVOColorFromGDIColor(ColorDialog.Color);
        }

        /// <summary>
        /// Masque tous les contrôles lorsqu'il n'y a pas d'item dans la liste
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListCharacters_ListIsEmpty(object sender, EventArgs e)
        {
            grpAnimations.Visible = false;
            grpInformations.Visible = false;
            grpDialogs.Visible = false;
        }

        /// <summary>
        /// Charge un nouveau character en fonction de l'item choisi.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListCharacters_ItemChosen(object sender, EventArgs e)
        {
            LoadCharacter(ListCharacters.ItemSelectedValue);
        }

        /// <summary>
        /// Code ajouté lors de la création d'un item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListCharacters_ItemToCreate(object sender, EventArgs e)
        {
            VO_Character newChar = _Service.CreateCharacter();
            ListCharacters.AddItem(newChar.Id, newChar.Title);
            LoadCharacter(newChar.Id);
        }

        /// <summary>
        /// Code ajouté lors de la suppression d'un item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListCharacters_ItemToDelete(object sender, EventArgs e)
        {
            CurrentCharacter.Delete();
            CurrentCharacter = null;
        }

        /// <summary>
        /// Synchronise le label d'un item avec la liste d'items et le titre dans le formulaire
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (ListCharacters.ChangeItemName(CurrentCharacter.Id, txtName.Text))
            {
                CurrentCharacter.Title = txtName.Text;
            }
            else
            {
                txtName.Text = CurrentCharacter.Title;
                MessageBox.Show(Errors.ERROR_UNIQUE_TITLE, Errors.ERROR_BOX_TITLE);
            }
        }

        /// <summary>
        /// Change la vitesse de déplacement du character
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ddpSpeed_ValueChanged(object sender, EventArgs e)
        {
            CurrentCharacter.Speed = Convert.ToInt32(ddpSpeed.Value);
        }

        /// <summary>
        /// Change l'id de l'animation de face
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnimFace_AnimationLoading(object sender, EventArgs e)
        {
            CurrentCharacter.Face = AnimFace.Animation;
        }

        /// <summary>
        /// Change l'id de l'animation de face qui parle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TalkingFace_AnimationLoading(object sender, EventArgs e)
        {
            CurrentCharacter.TalkingFace = TalkingFace.Animation;
        }

        /// <summary>
        /// Change l'id de l'animation du character
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnimCharacter_AnimationLoading(object sender, EventArgs e)
        {
            LoadLists();
        }

        /// <summary>
        /// Changement de l'animation d'arrêt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ddpStandingAnim_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentCharacter.StandingAnim = ((VO_Base)ddpStandingAnim.SelectedItem).Id;
            this.LoadCharacter(CurrentCharacter.Id);
        }

        /// <summary>
        /// Changement de l'animation de déplacement
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ddpWalkingAnim_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentCharacter.WalkingAnim = ((VO_Base)ddpWalkingAnim.SelectedItem).Id;
        }

        /// <summary>
        /// Changement de l'animation de parole
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ddpTalkingAnim_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentCharacter.TalkingAnim = ((VO_Base)ddpTalkingAnim.SelectedItem).Id;
        }
        #endregion 
    }
}
