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

namespace Day06
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    public class MoveElement : IExternalCommand
    {
        /// <summary>
        /// Move Element Example
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
            using (Transaction tran = new Transaction(doc))
            {
                tran.Start("Move Element");
                XYZ translation = new XYZ(10, 0, 0);
                element.Location.Move(translation);
                MessageBox.Show("Element Has Moved", "Information", MessageBoxButtons.OK);
                tran.Commit();
            }

            return Result.Succeeded;
        }
    }
}
