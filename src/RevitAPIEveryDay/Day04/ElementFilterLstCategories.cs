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
    public class ElementFilterLstCategories : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            List<string> categories = new List<string>();
            categories.Add("Floors");
            categories.Add("Ceilings");
            ElementFilter filter = Library.ElementUtils.FiltersElementByCategory(doc, categories);
            IList<Element> elems = new FilteredElementCollector(doc).WhereElementIsNotElementType().WherePasses(filter)
                .ToElements();
            MessageBox.Show($"Total Ceilings-Floors:{elems.Count}");
            return Result.Succeeded;
        }
    }
}