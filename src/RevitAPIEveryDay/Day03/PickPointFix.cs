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

namespace Day03
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    public class PickPointFix : IExternalCommand
    {
        /// <summary>
        /// PickPoints Support Everything In 3D View And Section
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
                using (Transaction tran = new Transaction(doc))
                {
                    tran.Start("run");
                    Plane plane = Plane.CreateByNormalAndOrigin(uidoc.ActiveView.ViewDirection,uidoc.ActiveView.Origin);
                    SketchPlane sp = SketchPlane.Create(doc, plane);
                    doc.ActiveView.SketchPlane = sp;
                    doc.ActiveView.ShowActiveWorkPlane();
                    XYZ point = uidoc.Selection.PickPoint(ObjectSnapTypes.Intersections);
                    doc.ActiveView.HideActiveWorkPlane();
                    sp.Dispose();
                    MessageBox.Show(point.ToString());
                    tran.Commit();
                }
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