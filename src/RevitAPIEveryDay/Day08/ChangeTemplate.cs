using System.Windows.Forms;
using Autodesk.Revit.Attributes;
using Library;

namespace LearnAPI.Day08
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    public class ChangeTemplate : RevitCommand
    {
        public override void Action()
        {
            MessageBox.Show(Doc.Title);
        }
    }
}
