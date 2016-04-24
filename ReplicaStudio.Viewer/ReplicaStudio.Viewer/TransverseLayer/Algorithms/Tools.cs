using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ReplicaStudio.Shared.TransverseLayer.VO;
using ReplicaStudio.Viewer.DataLayer;

namespace ReplicaStudio.Viewer.TransverseLayer.Algorithms
{
    /// <summary>
    /// Divers tools
    /// </summary>
    public static class Tools
    {
        public static int GetVariableValue(VO_IntValue variable)
        {
            VO_Variable vari = GameState.State.Variables.Find(p => p.Id == variable.VariableValue);
            if (vari != null)
            {
                return vari.Value;
            }
            else
            {
                return variable.IntValue;
            }
        }

        /// <summary>
        /// Test si un point est dans un polygone
        /// </summary>
        /// <param name="p"></param>
        /// <param name="poly"></param>
        /// <returns></returns>
        public static bool PointInPolygon(Point p, System.Drawing.Point[] poly)
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

        /// <summary>
        /// Rend les points d'une ligne
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static List<Point> RenderLine(Point point1, Point point2)
        {
            List<Point> points = new List<Point>();

            // Get Points From Line(s)
            float curDist = 0;
            float distance = 0;
            float deltaX = point2.X - point1.X;
            float deltaY = point2.Y - point1.Y;
            curDist = 0;
            distance = (float)Math.Sqrt(Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2));
            while (curDist < distance)
            {
                curDist++;
                int offsetX = (int)((double)curDist / (double)distance * (double)deltaX);
                int offsetY = (int)((double)curDist / (double)distance * (double)deltaY);
                points.Add(new Point(point1.X + offsetX, point1.Y + offsetY));
            }
            return points;
        }

        /// <summary>
        /// Récupère une direction en fonction de l'angle
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <param name="nbrMovements"></param>
        /// <returns></returns>
        public static Double GetAngle(Point point1, Point point2, int nbrMovements)
        {
            double point0X = point1.X;
            double point0Y = point1.Y - Math.Sqrt(Math.Abs(point2.X - point1.X) * Math.Abs(point2.X - point1.X) + Math.Abs(point2.Y - point1.Y) * Math.Abs(point2.Y - point1.Y));
            double final = Math.Round((Math.Atan2((double)point2.Y - point0Y, (double)point2.X - point0X)) * nbrMovements / Math.PI);
            if (final == nbrMovements)
                return 0;
            return final;
        }

        /// <summary>
        /// Converti un Color GDI en Color SFML
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Color GetXNAColorFromVOColor(VO_Color color)
        {
            return new Color(color.R, color.G, color.B, color.A);
        }
    }
}
