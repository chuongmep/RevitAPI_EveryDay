using System.Text;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using Library;

namespace LearnAPI.Day08
{
    [Transaction(TransactionMode.Manual)]
    class LearnXYZ : RevitCommand
    {
        public override void Action()
        {
            StringBuilder st = new StringBuilder();
            //Create A Point 
            XYZ p1 = new XYZ(1, 1, 1);
            XYZ p2 = new XYZ(1, 1, 10);
            p1.ShowMessageBox();
            //Point Zero 
            XYZ zero1 = new XYZ(0, 0, 0);
            zero1.ShowMessageBox();
            XYZ zero2 = XYZ.Zero;
            zero2.ShowMessageBox();
            // Sum
            XYZ add = p1.Add(p2);
            add.ShowMessageBox();
            //Sub
            XYZ sub = p1.Subtract(p2);
            sub.ShowMessageBox();
            // mul
            XYZ multiply = p1.Multiply(5);
            multiply.ShowMessageBox();
            //div
            XYZ divide = p1.Divide(5);
            divide.ShowMessageBox();
            // 
            XYZ a = new XYZ(1, 2, 3);
            a.IsZeroLength().ShowMessageBox();
            a.ShowMessageBox();
            //Normal
            XYZ normalize = a.Normalize();
            normalize.ShowMessageBox("normalize");

        }
    }
}
