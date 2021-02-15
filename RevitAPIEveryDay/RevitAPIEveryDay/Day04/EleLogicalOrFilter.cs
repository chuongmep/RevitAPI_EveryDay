using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Application = Autodesk.Revit.ApplicationServices.Application;

namespace Day04
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    public class EleLogicalOrFilter : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            ElementCategoryFilter filter1 = new ElementCategoryFilter(BuiltInCategory.OST_Windows);
            ElementCategoryFilter filter2 = new ElementCategoryFilter(BuiltInCategory.OST_Doors);
            LogicalOrFilter filters = new LogicalOrFilter(filter1, filter2);
            IList<Element> elems = new FilteredElementCollector(doc).WherePasses(filters).WhereElementIsNotElementType().ToElements();
            MessageBox.Show($"Count Element{elems.Count}");
            return Result.Succeeded;
        }
    }
}
