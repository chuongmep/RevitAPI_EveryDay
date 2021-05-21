using Autodesk.Revit.Attributes;
using Library;

namespace LearnAPI
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    public class _Template : RevitCommand
    {
        public override void Action()
        {
            //do some thing
        }
    }
}
