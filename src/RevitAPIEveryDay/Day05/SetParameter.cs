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

namespace Day05
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    public class SetParameter : IExternalCommand
    {
        /// <summary>
        /// Set Parameter Value Element
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

            try
            {
                Reference r = uidoc.Selection.PickObject(ObjectType.Element);
                Element element = doc.GetElement(r);
                using (Transaction tran = new Transaction(doc))
                {
                    tran.Start("Set Value");
                    Parameter parameter = element.get_Parameter(BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS);
                    parameter.Set("Data");
                    tran.Commit();
                }
            }
            catch(Autodesk.Revit.Exceptions.OperationCanceledException){}
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return Result.Succeeded;
        }
    }
}
