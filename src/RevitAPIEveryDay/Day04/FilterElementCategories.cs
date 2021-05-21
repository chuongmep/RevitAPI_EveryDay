using System.Collections.Generic;
using System.Windows.Forms;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Application = Autodesk.Revit.ApplicationServices.Application;

namespace LearnAPI.Day04
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    public class FilterElementCategories : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            ElementCategoryFilter filter = new ElementCategoryFilter(BuiltInCategory.OST_Walls);

            IList<Element> elems = new FilteredElementCollector(doc).WherePasses(filter).WhereElementIsNotElementType().ToElements();
            MessageBox.Show($"{elems.Count}");
            return Result.Succeeded;
        }
    }
}