using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using Library;

namespace LearnAPI.Day08
{
    [Transaction(TransactionMode.Manual)]
    public class DistanceAToB  :RevitCommand
    {
        public override void Action()
        {
            XYZ A = new XYZ(0, 0, 0);

            XYZ B = new XYZ(100, 0, 0);

            //Distance A To B = 100
            A.DistanceTo(B).ToString().ShowMessageBox("Distance A To B");

            // Change A To New Location With Y
            XYZ add = A.Add(new XYZ(0, 10, 0));
            add.ShowMessageBox("New A"); // XYZ(0,10,0)
            add.DistanceTo(B).ToString().ShowMessageBox("Distance A New To B");

            //Practical
            XYZ p1 = UIDoc.Selection.PickPoint(ObjectSnapTypes.None);
            XYZ p2 = UIDoc.Selection.PickPoint(ObjectSnapTypes.None);
            double distanceTo = p1.DistanceTo(p2);
            distanceTo.ToString().ShowMessageBox("Distance From P1 To P2");
            
            // Distance FamilyInstance A To Family Instance B
            Reference r = UIDoc.Selection.PickObject(ObjectType.Element,"Pick Family 1");
            FamilyInstance fam1 = Doc.GetElement(r) as FamilyInstance;
            LocationPoint lcp = fam1.Location as LocationPoint;
            XYZ P1Fam = lcp.Point;
            P1Fam.ShowMessageBox("Point Family 1");
            // TODO : Code Here

            //Distance Point To Curve(Center of Curve Circle)
            //TODO

            // Create Dim With Two Point ref
            //TODO : Code Here


            //Distance Perpendicular Pipe1 To Pipe2
            //TODO : Code Here

            //Find Number Nearest with n integer from List B

            double n = 10;
            List<double> listB = new List<double>();
            foreach (double d in listB)
            {
                //TODO
            }

            // Find Number Nearest with XYZ with list XYZ(List<XYZ> )
            //TODO



            //Distance Point To Plane 
            //TODO


            //Distance Point To Line 
            //TODO

            

        }
    }
}
