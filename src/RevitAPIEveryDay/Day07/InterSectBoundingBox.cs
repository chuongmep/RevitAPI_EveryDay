using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Application = Autodesk.Revit.ApplicationServices.Application;

namespace LearnAPI.Day07
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    public class InterSectBoundingBox : IExternalCommand
    {
        /// <summary>
        /// Check Element Intersect With Element Selected By BoundingBox
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

            ExclusionFilter excludedFilter = new ExclusionFilter(new List<ElementId>(){ element.Id});
            BoundingBoxXYZ bbxyz = element.get_BoundingBox(doc.ActiveView);
            Outline outline = new Outline(bbxyz.Min,bbxyz.Max);
            BoundingBoxIntersectsFilter intersectFilter = new BoundingBoxIntersectsFilter(outline);
            IList<Element> eleIntersect = new FilteredElementCollector(doc)
                .WhereElementIsNotElementType()
                .WherePasses(excludedFilter)
                .WherePasses(intersectFilter).ToElements()
                .Where(x=>x.Category.Name!="Cameras")
                .Where(x => !string.IsNullOrEmpty(x.Name)).ToList();
            StringBuilder sb = new StringBuilder();
            if (eleIntersect.Any())
            {
                foreach (Element e in eleIntersect)
                {
                    sb.AppendLine($"Name:{e.Name}-Category:{e.Category.Name}-Id:{e.Id}");
                }
            }
            MessageBox.Show($"Element Intersect : \n{sb}");
            return Result.Succeeded;
        }

    }
}
