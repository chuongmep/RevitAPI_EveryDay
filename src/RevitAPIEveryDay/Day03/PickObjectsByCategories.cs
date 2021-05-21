using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Application = Autodesk.Revit.ApplicationServices.Application;

namespace LearnAPI.Day03
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    public class PickObjectsByCategories : IExternalCommand
    {
        /// <summary>
        /// PickObject Element Multiply Categories
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
                CategoryFilter categoryFilter = new CategoryFilter { catname = "Walls" };
                IList<Reference> references = uidoc.Selection.PickObjects(ObjectType.Element, categoryFilter,
                    "Select Element Filter By Categories ");
                StringBuilder sb = new StringBuilder();
                foreach (Reference r in references)
                {
                    Autodesk.Revit.DB.Element e = doc.GetElement(r);
                    sb.AppendLine($"{e.Name}-{e.Id}");
                }
                MessageBox.Show($"{sb}");
            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException) {}
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            return Result.Succeeded;
        }

        public class CategoryFilter : ISelectionFilter
        {
            public string catname { get; set; }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="elem"></param>
            /// <returns></returns>
            public bool AllowElement(Autodesk.Revit.DB.Element elem)
            {
                List<bool> result = new List<bool>();
                if (catname == elem.Category.Name)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="reference"></param>
            /// <param name="position"></param>
            /// <returns></returns>
            public bool AllowReference(Reference reference, XYZ position)
            {
                return false;
            }
        }
    }
}