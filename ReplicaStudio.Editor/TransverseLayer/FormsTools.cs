using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using ReplicaStudio.Editor.TransverseLayer.Constants;
using ReplicaStudio.Shared.TransverseLayer.Constants;
using System.Drawing.Imaging;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Shared.DatasLayer;
using ReplicaStudio.TransverseLayer;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.ComponentModel;
using System.Reflection;
using System.IO;
using ReplicaStudio.Editor.TransverseLayer.Managers;
using System.Collections;

namespace ReplicaStudio.Editor.TransverseLayer
{
    /// <summary>
    /// Outils pour l'éditeur
    /// </summary>
    public static class FormsTools
    {
        #region Gestion d'images
        /// <summary>
        /// Récupère les informations nécessaires au placement au milieu d'une surface dans une autre, réduite si besoin en conservant les proportions.
        /// </summary>
        /// <param name="pContainer">Rectangle conteneur</param>
        /// <param name="pSurface">Rectangle de l'objet</param>
        /// <returns>Objet rectangle</returns>
        static public Rectangle GetSurfacePositionCenterInSurface(Rectangle container, Rectangle surface)
        {
            Rectangle output = new Rectangle();

            if (surface.Width > container.Width || surface.Height > container.Height)
            {
                float nPercent = 0;
                float nPercentW = 0;
                float nPercentH = 0;

                nPercentW = ((float)container.Width / (float)surface.Width);
                nPercentH = ((float)container.Height / (float)surface.Height);

                if (nPercentH < nPercentW)
                    nPercent = nPercentH;
                else
                    nPercent = nPercentW;

                int destWidth = (int)(surface.Width * nPercent);
                int destHeight = (int)(surface.Height * nPercent);

                output.Width = destWidth;
                output.Height = destHeight;
            }
            else
            {
                output.Width = surface.Width;
                output.Height = surface.Height;
            }
            output.X = (container.Width - output.Width) / 2;
            output.Y = (container.Height - output.Height) / 2;

            return output;
        }

        /// <summary>
        /// Place la Surface Source dans un Surface de taille pSize.
        /// </summary>
        /// <param name="pSource">Surface à centrer</param>
        /// <param name="pBackground">Surface de background</param>
        /// <param name="pSize">Taille du conteneur</param>
        /// <param name="pSize">Taille du contenu</param>
        /// <returns>Surface transformée</returns>
        static public Image GetImageReducedAndCentered(Image source, Image background, Rectangle size, Rectangle sizeSource, bool full)
        {
            Rectangle finalSize = FormsTools.GetSurfacePositionCenterInSurface(size, sizeSource);
            Image finalSurface = new Bitmap(background);
            using (Graphics graphics = Graphics.FromImage(finalSurface))
            {
                if (full)
                    graphics.DrawImageUnscaled(source, finalSize);
                else
                    graphics.DrawImage(source, finalSize);
            }
            return finalSurface;
        }

        /// <summary>
        /// Converti un VO_Color en Color GDI
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color GetGDIColorFromVOColor(VO_Color color)
        {
            Color output = Color.FromArgb(color.A, color.R, color.G, color.B);

            return output;
        }

        /// <summary>
        /// Converti un Color GDI en VO_Color
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static VO_Color GetVOColorFromGDIColor(Color color)
        {
            VO_Color output = new VO_Color();
            output.A = color.A;
            output.G = color.G;
            output.R = color.R;
            output.B = color.B;

            return output;
        }
        #endregion

        #region Gestion de listes
        /// <summary>
        /// Récupère une liste de directions pour les characters.
        /// </summary>
        /// <returns></returns>
        public static List<VO_ListItem> GetMovementsList()
        {
            List<VO_ListItem> list = new List<VO_ListItem>();

            foreach (Enums.Movement mov in Enum.GetValues(typeof(Enums.Movement)))
            {
                if (GameCore.Instance.Game.Project.MovementDirections == 8 || (int)mov < 4)
                {
                    list.Add(new VO_ListItem((int)mov, mov.GetDescription()));
                }
            }

            return list;
        }

        /// <summary>
        /// Récupère une liste de durées de messages (secondes)
        /// </summary>
        /// <returns></returns>
        public static List<VO_ListItem> GetMessageDurationList()
        {
            List<VO_ListItem> list = new List<VO_ListItem>();

            for (int i = GlobalConstants.DIALOG_MIN_DURATION; i <= GlobalConstants.DIALOG_MAX_DURATION; i++)
            {
                list.Add(new VO_ListItem(i, i.ToString()));
            }

            return list;
        }

        /// <summary>
        /// Récupère un liste de tailles de polices
        /// </summary>
        /// <returns></returns>
        public static List<VO_ListItem> GetMessageFontSizeList()
        {
            List<VO_ListItem> list = new List<VO_ListItem>();

            list.Add(new VO_ListItem(8, 8.ToString()));
            list.Add(new VO_ListItem(9, 9.ToString()));
            list.Add(new VO_ListItem(10, 10.ToString()));
            list.Add(new VO_ListItem(11, 11.ToString()));
            list.Add(new VO_ListItem(12, 12.ToString()));
            list.Add(new VO_ListItem(14, 14.ToString()));
            list.Add(new VO_ListItem(16, 16.ToString()));
            list.Add(new VO_ListItem(18, 18.ToString()));
            list.Add(new VO_ListItem(20, 20.ToString()));
            list.Add(new VO_ListItem(24, 24.ToString()));
            list.Add(new VO_ListItem(28, 28.ToString()));
            list.Add(new VO_ListItem(32, 32.ToString()));
            list.Add(new VO_ListItem(36, 36.ToString()));
            list.Add(new VO_ListItem(42, 42.ToString()));

            return list;
        }

        /// <summary>
        /// Récupère un liste de tailles de polices
        /// </summary>
        /// <returns></returns>
        public static List<VO_ListItem> GetMovementsSpeedList()
        {
            List<VO_ListItem> list = new List<VO_ListItem>();

            for (int i = GlobalConstants.CHARACTERS_MIN_SPEED; i <= GlobalConstants.CHARACTERS_MAX_SPEED; i++)
            {
                list.Add(new VO_ListItem(i, i.ToString()));
            }

            return list;
        }

        /// <summary>
        /// Récupère un liste de tailles de polices
        /// </summary>
        /// <returns></returns>
        public static List<VO_ListItem> GetAnimationFrequencyList()
        {
            List<VO_ListItem> list = new List<VO_ListItem>();

            for (int i = GlobalConstants.ANIMATION_MIN_FREQUENCY; i <= GlobalConstants.ANIMATION_MAX_FREQUENCY; i++)
            {
                list.Add(new VO_ListItem(i, i.ToString()));
            }

            return list;
        }

        /// <summary>
        /// Récupérer un tableau de masques de couleurs
        /// </summary>
        /// <returns></returns>
        public static Pen[] GetMasksColors()
        {
            Pen[] pens = new Pen[10];

            pens[0] = CreatePen(Color.Green);
            pens[1] = CreatePen(Color.Red);
            pens[2] = CreatePen(Color.Blue);
            pens[3] = CreatePen(Color.Yellow);
            pens[4] = CreatePen(Color.Violet);
            pens[5] = CreatePen(Color.Olive);
            pens[6] = CreatePen(Color.Navy);
            pens[7] = CreatePen(Color.LightPink);
            pens[8] = CreatePen(Color.Indigo);
            pens[9] = CreatePen(Color.MediumOrchid);

            return pens;
        }

        /// <summary>
        /// Récupérer un tableau de masques de brushes
        /// </summary>
        /// <returns></returns>
        public static Brush[] GetMasksFillingColors()
        {
            Brush[] brushes = new Brush[10];

            brushes[0] = CreateBrush(Color.Green);
            brushes[1] = CreateBrush(Color.Red);
            brushes[2] = CreateBrush(Color.Blue);
            brushes[3] = CreateBrush(Color.Yellow);
            brushes[4] = CreateBrush(Color.Violet);
            brushes[5] = CreateBrush(Color.Olive);
            brushes[6] = CreateBrush(Color.Navy);
            brushes[7] = CreateBrush(Color.LightPink);
            brushes[8] = CreateBrush(Color.Indigo);
            brushes[9] = CreateBrush(Color.MediumOrchid);

            return brushes;
        }

        /// <summary>
        /// Créer un brush
        /// </summary>
        /// <param name="color">Couleur</param>
        /// <returns>brush</returns>
        private static Brush CreateBrush(Color color)
        {
            return new SolidBrush(Color.FromArgb(GeneralSettingsConstants.DEFAULT_MASKS_OPACITY, color));
        }

        /// <summary>
        /// Créer un pinceau
        /// </summary>
        /// <param name="color">Couleur</param>
        /// <returns>pinceau</returns>
        private static Pen CreatePen(Color color)
        {
            return new Pen(Color.FromArgb(GeneralSettingsConstants.DEFAULT_MASKS_OPACITY, color));
        }
        #endregion

        #region Gestion cursors
        /// <summary>
        /// Méthode "qui marche" pour charger un curseur.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static Cursor LoadCursor(Stream reader)
        {
            string file = Path.GetTempPath() + "/cursor_pcs.cur";
            // create a write stream
            FileStream writeStream = new FileStream(file, FileMode.Create, FileAccess.Write);
            // write to the stream
            FormsTools.ReadWriteStream(reader, writeStream);

            Cursor cursor = FormsTools.LoadCustomCursor(file);

            writeStream.Close();
            File.Delete(file);

            return cursor;
        }

        /// <summary>
        /// readStream is the stream you need to read
        /// writeStream is the stream you want to write to
        /// </summary>
        /// <param name="readStream"></param>
        /// <param name="writeStream"></param>
        public static void ReadWriteStream(Stream readStream, Stream writeStream)
        {
            int Length = 256;
            Byte[] buffer = new Byte[Length];
            int bytesRead = readStream.Read(buffer, 0, Length);
            // write the required bytes
            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = readStream.Read(buffer, 0, Length);
            }
            readStream.Close();
            writeStream.Close();
        }

        public static Cursor LoadCustomCursor(string path)
        {
            IntPtr hCurs = LoadCursorFromFile(path);
            if (hCurs == IntPtr.Zero) throw new Win32Exception();
            var curs = new Cursor(hCurs);
            // Note: force the cursor to own the handle so it gets released properly
            var fi = typeof(Cursor).GetField("ownHandle", BindingFlags.NonPublic | BindingFlags.Instance);
            fi.SetValue(curs, true);
            return curs;
        }
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr LoadCursorFromFile(string path);

        #endregion

        #region Export Jeu

        public static bool IsFolderEmpty(string Folder)
        {
            if (Directory.GetDirectories(Folder).Count() > 0)
                return false;
            if (Directory.GetFiles(Folder).Count() > 0)
                return false;
            return true;
        }

        #endregion

        #region Redraw Suspend/Resume
        [DllImport("user32.dll", EntryPoint = "SendMessageA", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        private const int WM_SETREDRAW = 0xB;

        public static void SuspendDrawing(this Control target)
        {
            SendMessage(target.Handle, WM_SETREDRAW, 0, 0);
        }

        public static void ResumeDrawing(this Control target) { ResumeDrawing(target, true); }
        public static void ResumeDrawing(this Control target, bool redraw)
        {
            SendMessage(target.Handle, WM_SETREDRAW, 1, 0);

            if (redraw)
            {
                target.Refresh();
            }
        }
        #endregion

        /// <summary>
        /// Test si un point est dans un polygone
        /// </summary>
        /// <param name="p"></param>
        /// <param name="poly"></param>
        /// <returns></returns>
        public static bool PointInPolygon(Point p, Point[] poly)
        {
            Point p1, p2;
            bool inside = false;

            if (poly.Length < 3)
            {
                return inside;
            }

            Point oldPoint = new Point(poly[poly.Length - 1].X, poly[poly.Length - 1].Y);

            for (int i = 0; i < poly.Length; i++)
            {
                Point newPoint = new Point(poly[i].X, poly[i].Y);

                if (newPoint.X > oldPoint.X)
                {
                    p1 = oldPoint;
                    p2 = newPoint;
                }
                else
                {
                    p1 = newPoint;
                    p2 = oldPoint;
                }

                if ((newPoint.X < p.X) == (p.X <= oldPoint.X)
                    && ((int)p.Y - (int)p1.Y) * (int)(p2.X - p1.X)
                     < ((int)p2.Y - (int)p1.Y) * (int)(p.X - p1.X))
                {
                    inside = !inside;
                }
                oldPoint = newPoint;
            }
            return inside;

        }

    }
}
