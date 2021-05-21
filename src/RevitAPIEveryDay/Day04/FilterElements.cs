using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Application = Autodesk.Revit.ApplicationServices.Application;

namespace LearnAPI.Day04
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    public class FilterElements : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;


            //Filter Wall
            IList<Element> elems = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Walls).WhereElementIsNotElementType().ToElements();

            MessageBox.Show($"Count Walls: {elems.Count}");

            //Filter Rooms
            List<Room> rooms = new FilteredElementCollector(doc)
                .OfClass(typeof(SpatialElement)).Cast<Room>().ToList();
            MessageBox.Show($"Count Rooms: {rooms.Count}");

            //Filter Schedule
            List<ViewSchedule> schedules = new FilteredElementCollector(doc)
                .OfClass(typeof(ViewSchedule)).OfType<ViewSchedule>()
                .Where(x => !x.IsTitleblockRevisionSchedule && !x.IsTemplate && x.Definition.ShowHeaders)
                .OrderBy(x => x.Name)
                .ToList();
            MessageBox.Show($"Count Schedule: {schedules.Count}");

            return Result.Succeeded;

        }
    }
}