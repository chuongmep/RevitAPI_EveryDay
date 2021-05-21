using System;
using System.Text;
using System.Windows.Forms;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Application = Autodesk.Revit.ApplicationServices.Application;
using View = Autodesk.Revit.DB.View;

namespace LearnAPI.Day07
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    public class GetBoundingBox : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            Element element = doc.GetElement(uidoc.Selection.PickObject(ObjectType.Element,"Pick Object"));
            var view = doc.GetElement(element.OwnerViewId) as View;
            BoundingBoxXYZ bbXyz = null;
            if (view!=null)
            {
                bbXyz = element.get_BoundingBox(view);
            }
            else
            {
                bbXyz = element.get_BoundingBox(null);
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Max Point: {bbXyz.Max}");
            sb.AppendLine($"Min Point: {bbXyz.Min}");
            sb.AppendLine($"Centroid: {Center(bbXyz)}");
            sb.AppendLine($"Length Of BoundingBox: {Length(bbXyz)}");
            sb.AppendLine($"Width Of BoundingBox: {Width(bbXyz)}");
            sb.AppendLine($"Height Of BoundingBox: {Height(bbXyz)}");
            sb.AppendLine($"TranFrom Origin: {bbXyz.Transform.Origin}");
            MessageBox.Show(sb.ToString());
            return Result.Succeeded;
        }

        /// <summary>
        /// Get Length Of BoundingBox
        /// </summary>
        /// <param name="boundingBox"></param>
        /// <returns></returns>
        public static double Length(BoundingBoxXYZ boundingBox)
        {
            if (boundingBox is null)
                throw new ArgumentNullException(nameof(boundingBox));

            return boundingBox.Max.X - boundingBox.Min.X;
        }
        /// <summary>
        /// Return A Width Of BoundingBox
        /// </summary>
        /// <param name="boundingBox"></param>
        /// <returns></returns>
        public static double Width(BoundingBoxXYZ boundingBox)
        {
            if (boundingBox is null)
                throw new ArgumentNullException(nameof(boundingBox));

            return boundingBox.Max.Y - boundingBox.Min.Y;
        }

        /// <summary>
        /// Return Height Of BoundingBox
        /// </summary>
        /// <param name="boundingBox"></param>
        /// <returns></returns>
        public static double Height( BoundingBoxXYZ boundingBox)
        {
            if (boundingBox is null)
                throw new ArgumentNullException(nameof(boundingBox));

            return boundingBox.Max.Z - boundingBox.Min.Z;
        }

        #region Get Center By 2 Alternate

        /// <summary>
        /// Return Center Of Two Point
        /// </summary>
        /// <param name="Point1"></param>
        /// <param name="Point2"></param>
        /// <returns></returns>
        public static XYZ Center(XYZ Point1, XYZ Point2)
        {
            XYZ Between = new XYZ(0.5 * (Point1.X + Point2.X), 0.5 * (Point1.Y + Point2.Y), 0.5 * (Point1.Z + Point2.Z));

            return Between;
        }

        /// <summary>
        /// Return Center Of BoundingBox
        /// </summary>
        /// <param name="boundingBox"></param>
        /// <returns></returns>
        public static XYZ Center(BoundingBoxXYZ boundingBox)
        {
            XYZ center = (boundingBox.Max + boundingBox.Min) * 0.5;
            return center;
        }

        #endregion
    }
}
