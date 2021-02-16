using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Application = Autodesk.Revit.ApplicationServices.Application;

namespace Day02
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    public class RvtDocument : IExternalCommand
    {
        /// <summary>
        /// Document
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

            List<string> data = new List<string>();
            data.Add(doc.Title);
            data.Add(doc.ActiveView.Name);
            data.Add(doc.IsLinked.ToString());
            data.Add(doc.ProjectInformation.BuildingName);
            MessageBox.Show(string.Join(Environment.NewLine, data));
            return Result.Succeeded;
            
        }
    }
}
