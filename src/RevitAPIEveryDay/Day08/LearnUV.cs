using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Library;

namespace LearnAPI.Day08
{
    [Transaction(TransactionMode.Manual)]
    public class LearnUV : RevitCommand
    {
        public override void Action()
        {
            //UV Coordinate: The Point in UV Parameter Space defined by U, V, and sometimes W.
            // Ex : rectangle with size  100x200
            // Get point center of rectangle : 
            UV uv = new UV(0.5, 0.5);


            BoundingBoxUV bbUv = new BoundingBoxUV(0, 10, 10, 0);
        }
    }
}
