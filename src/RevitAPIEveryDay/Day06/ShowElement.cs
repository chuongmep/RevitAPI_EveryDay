using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI.Selection;
using Library;

namespace LearnAPI.Day06
{
    [Transaction(TransactionMode.Manual)]
    class ShowElement : RevitCommand
    {
        /// <summary>
        /// Show Element In Document
        /// https://www.revitapidocs.com/2020/6c40c35b-1b2b-1741-dafa-5ab6b1744634.htm
        /// </summary>
        public override void Action()
        {
            Autodesk.Revit.DB.Reference pickObject = UIDoc.Selection.PickObject(ObjectType.Element);
            Autodesk.Revit.DB.Element element = Doc.GetElement(pickObject);
            UIDoc.ShowElements(element);
        }
    }
}
