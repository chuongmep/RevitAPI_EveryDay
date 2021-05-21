using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace Library
{
    
    public abstract class RevitCommand : IExternalCommand
    {
        public abstract void Action();
        public UIDocument UIDoc { get; set; }
        public Document Doc { get; set; }
        public UIApplication UIApp { get; set; }
        public Application App { get; set; }

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            this.UIApp = commandData.Application;
            this.UIDoc = UIApp.ActiveUIDocument;
            this.App = UIApp.Application;
            this.Doc = UIDoc.Document;
            try
            {
                Action();
            }
            catch(Autodesk.Revit.Exceptions.OperationCanceledException){}
            catch (Exception e)
            {
                Debug.Print(e.ToString());
                return Result.Cancelled;
            }
            return Result.Succeeded;
        }
        
    }
}
