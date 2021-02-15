using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Application = Autodesk.Revit.ApplicationServices.Application;

namespace Day06
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    public class RotateElement : IExternalCommand
    {
        /// <summary>
        /// Rotate Element Example
        /// </summary>
        /// <param name="commandData"></param>
        /// <param name="message"></param>
        /// <param name="elements"></param>
        /// <returns></returns>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            Element element = doc.GetElement(uidoc.Selection.PickObject(ObjectType.Element));

            using (Transaction tran = new Transaction(doc))
            {
                tran.Start("Rotate");
                if (element.Location is LocationPoint)
                {
                    var lc = element.Location as LocationPoint;
                    XYZ lcPoint = lc.Point;
                    double angle = 45;
                    Rotate(element, lcPoint, angle);
                }

                if (element.Location is LocationCurve)
                {
                    LocationCurve lc = element.Location as LocationCurve;
                    XYZ lcPoint = lc.Curve.GetEndPoint(1);
                    double angle = 45;
                    Rotate(element, lcPoint, angle);

                }

                tran.Commit();
            }
            return Result.Succeeded;
        }


        /// <summary>
        /// Rotate Element
        /// </summary>
        /// <param name="element">element rotate</param>
        /// <param name="lcPoint">location need rotate</param>
        /// <param name="angle">angle need rotate</param>
        void Rotate(Element element, XYZ lcPoint, double angle)
        {
            using (SubTransaction tran = new SubTransaction(element.Document))
            {
                tran.Start();
                Line line = Line.CreateBound(lcPoint, new XYZ(lcPoint.X, lcPoint.Y, lcPoint.Z + 10000));
                ElementTransformUtils.RotateElement(element.Document, element.Id, line, angle * Math.PI / 180);
                tran.Commit();
            }

        }
    }
}
