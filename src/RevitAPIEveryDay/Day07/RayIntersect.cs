using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Library;
using Application = Autodesk.Revit.ApplicationServices.Application;

namespace Day07
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    public class RayIntersect : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            Element element = doc.GetElement(uidoc.Selection.PickObject(ObjectType.Element));
            List<string> Categories = new List<string>();
            Categories.Add("Walls");

            element.RayIntersect(Categories,new XYZ(0,0,1));
            return Result.Succeeded;
        }
    }
}
