using System.Collections.Generic;
using System.Windows.Forms;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Application = Autodesk.Revit.ApplicationServices.Application;

namespace LearnAPI.Day03
{
    [Transaction(TransactionMode.Manual)]
    public class PickElementsByRectangle : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            IList<Element> eles = uidoc.Selection.PickElementsByRectangle(new SelectionFilter(), "Select Element By Drag Mouse Rectangle");
            MessageBox.Show(eles.Count.ToString());
            return Result.Succeeded;
        }
    }
    public class SelectionFilter : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {
            if (elem !=null) return true;
            return false;
           
        }
        public bool AllowReference(Reference refer, XYZ pos)
        {
            return false;
           
        }
    }
}
