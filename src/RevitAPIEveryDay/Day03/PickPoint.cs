using System.Text;
using System.Windows.Forms;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Application = Autodesk.Revit.ApplicationServices.Application;

namespace LearnAPI.Day03
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    public class PickPoint : IExternalCommand
    {
        /// <summary>
        /// PickPoints Of Element
        /// </summary>
        /// <param name="commandData"></param>
        /// <param name="message"></param>
        /// <param name="elements"></param>
        /// <returns></returns>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            #region Init

            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            #endregion

            try
            {
                XYZ point1 = uidoc.Selection.PickPoint();
                XYZ point2 = uidoc.Selection.PickPoint();
                XYZ point3 = uidoc.Selection.PickPoint();
                XYZ point4 = uidoc.Selection.PickPoint();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(point1.ToString());
                sb.AppendLine(point2.ToString());
                sb.AppendLine(point3.ToString());
                sb.AppendLine(point4.ToString());

                MessageBox.Show("You have selected points:\r\n" + sb.ToString());
            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException e)
            {
                MessageBox.Show($"You Has Press Esc\n{e}","Warning",MessageBoxButtons.OK);
                return Result.Cancelled;
                //Pressed Esc
            }

            return Result.Succeeded;
        }
    }
}