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
    public class PickObject : IExternalCommand
    {
        /// <summary>
        /// PickObject Element Single
        /// Show Information Element
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
                Reference reference = uidoc.Selection.PickObject(ObjectType.Element);
                Element element = doc.GetElement(reference);
                List<string> EleInfo = new List<string>();
                EleInfo.Add($"Element Name: {element.Name}");
                EleInfo.Add($"Element Id: {element.Name}");
                EleInfo.Add($"UniqueId: {element.UniqueId}");
                EleInfo.Add($"Category: {element.Category.Name}");
                MessageBox.Show(string.Join(Environment.NewLine, EleInfo),"Info",MessageBoxButtons.OK);
            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException e)
            {
                MessageBox.Show($"You Has Press Esc\n{e}","Warning",MessageBoxButtons.OK);
                //Pressed Esc
            }

            return Result.Succeeded;
        }
    }
}