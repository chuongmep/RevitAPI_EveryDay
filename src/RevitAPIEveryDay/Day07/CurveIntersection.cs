using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Library;
using Application = Autodesk.Revit.ApplicationServices.Application;

namespace Day07
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    public class CurveIntersection : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            XYZ Intersect = new XYZ(10,15,0);
            XYZ Point = new XYZ(10,50,0);
            Curve curve1 = Line.CreateBound(XYZ.Zero, Intersect) as Curve;
            Curve curve2 = Line.CreateBound(Intersect, Point) as Curve;
            XYZ intersection = Intersection(curve1, curve2);
            MessageBox.Show($"Point Intersect : {intersection}");
            return Result.Succeeded;
        }

        public static XYZ Intersection(Curve _curve, Curve _another)
        {
            IntersectionResultArray resultArray = new IntersectionResultArray();
            SetComparisonResult result = _curve.Intersect(_another, out resultArray);

            if (result != SetComparisonResult.Overlap)
            {
                bool Flag2 = _curve.GetEndPoint(0).IsEqual(_another.GetEndPoint(0)) || _curve.GetEndPoint(0).IsEqual(_another.GetEndPoint(1));

                bool Flag3 = _curve.GetEndPoint(1).IsEqual(_another.GetEndPoint(0)) || _curve.GetEndPoint(1).IsEqual(_another.GetEndPoint(1));

                if (Flag2) return _curve.GetEndPoint(0);
                if (Flag3) return _another.GetEndPoint(1);

                return null;
            }
            if (resultArray.Size != 1)
            {
                throw new ArithmeticException("Having more one intersection.");
            }

            return resultArray.get_Item(0).XYZPoint;
        }
    }
}
