using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Library;

namespace RevitAPIEveryDay.Day06
{
    [Transaction(TransactionMode.Manual)]
    class ShowElementInLink : RevitCommand
    {
        /// <summary>
        /// https://forum.dynamobim.com/t/zoom-to-selected-element-in-a-linked-document/61279/2
        /// Show Element Linked ViewPlan
        /// </summary>
        public override void Action()
        {
            Reference r = UIDoc.Selection.PickObject(ObjectType.LinkedElement, "Select LinkElement");
            RevitLinkInstance lnkiinst = Doc.GetElement(r) as RevitLinkInstance;
            Transform trlnk = lnkiinst.GetTotalTransform();
            Document linkDocument = lnkiinst.GetLinkDocument();
            Element EleInLink = linkDocument.GetElement(r.LinkedElementId);
            BoundingBoxXYZ bb = EleInLink.get_BoundingBox(null);
            XYZ P1 = new XYZ(bb.Min.X, bb.Min.Y, 0);
            XYZ P2 = new XYZ(bb.Max.X, bb.Max.Y, 0);
            IList<UIView> uiViews = UIDoc.GetOpenUIViews();
            UIView viewui = null;
            foreach (UIView uiView in uiViews)
            {
                if (uiView.ViewId==Doc.ActiveView.Id)
                {
                    viewui = uiView;
                }
            }
            viewui.ZoomAndCenterRectangle(P1,P2);
        }
    }
}
