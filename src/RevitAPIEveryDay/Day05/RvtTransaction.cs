using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Application = Autodesk.Revit.ApplicationServices.Application;

namespace LearnAPI.Day05
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    public class RvtTransaction : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            Element element = doc.GetElement(uidoc.Selection.PickObject(ObjectType.Element));
            using (TransactionGroup transg = new TransactionGroup(doc))
            {
                transg.Start("Transaction Group");
                //Exception(Out Side Transaction)
                //SetParameter(element,"Transaction Group");
                using (Transaction tran = new Transaction(doc))
                {
                    tran.Start("Transaction");
                    SetParameter(element, "Transaction");
                    using (SubTransaction subtrans = new SubTransaction(doc))
                    {
                        subtrans.Start();
                        SetParameter(element, "Sub Transaction");
                        subtrans.Commit();
                    }
                    tran.Commit();
                }
                transg.Commit();
            }

            return Result.Succeeded;
        }

        /// <summary>
        /// Set Parameter Value
        /// </summary>
        /// <param name="e">element</param>
        /// <param name="value">value</param>
        void SetParameter(Autodesk.Revit.DB.Element e, string value)
        {
            e.get_Parameter(BuiltInParameter.ALL_MODEL_INSTANCE_COMMENTS).Set(value);
        }
    }
}
