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
using Application = Autodesk.Revit.ApplicationServices.Application;

namespace RevitAPIEveryDay
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    public class SelectElementInLinked : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;
            Reference link_ref = uidoc.Selection.PickObject(ObjectType.Element, "Select a link instance first");
            IList<Reference> el_ref = uidoc.Selection.PickObjects(ObjectType.LinkedElement,
                "Select Element In Linked");

            RevitLinkInstance link1 = doc.GetElement(link_ref.ElementId) as RevitLinkInstance;
            Document linkDoc = link1.GetLinkDocument();
            List<ElementId> elementIds = new List<ElementId>();
            StringBuilder sb = new StringBuilder();
            foreach (var r1 in el_ref)
            {
                ElementId elementId = r1.LinkedElementId;
                elementIds.Add(elementId);
            }

            foreach (ElementId id in elementIds)
            {
                Element element = linkDoc.GetElement(id);
                sb.AppendLine($"{element.Name}-{element.Id}");
            }
            MessageBox.Show($"{sb}","Information",MessageBoxButtons.OK);
            return Result.Succeeded;
        }
    }
}