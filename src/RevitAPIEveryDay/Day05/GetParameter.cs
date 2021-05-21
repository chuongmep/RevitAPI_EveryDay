using System;
using System.Text;
using System.Windows.Forms;
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
    public class GetParameter : IExternalCommand
    {
        /// <summary>
        /// Get Information Parameter Value
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
                Element elementType = doc.GetElement(element.GetTypeId());
                Parameter parameter = element.get_Parameter(BuiltInParameter.ALL_MODEL_MARK);
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"Element Name:{element.Name}-ID:{element.Id}");
                sb.AppendLine($"Total Parameter Instance: {element.Parameters.Size.ToString()}");
                sb.AppendLine($"Total Parameter Type: {elementType.Parameters.Size.ToString()}");
                sb.AppendLine($"Parameter Name: {parameter.Definition.Name}");
                sb.AppendLine($"Parameter Group: {parameter.Definition.ParameterGroup}");
                sb.AppendLine($"Parameter Type: {parameter.Definition.ParameterType}");
                sb.AppendLine($"Parameter StorageType: {parameter.StorageType}");
                sb.AppendLine($"Parameter UnitType: {parameter.Definition.UnitType}");
                sb.AppendLine($"Parameter Id: {parameter.Id}");
                sb.AppendLine($"Parameter IsShared: {parameter.IsShared}");
                MessageBox.Show(sb.ToString());
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
