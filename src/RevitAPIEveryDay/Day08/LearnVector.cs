using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Library;

namespace LearnAPI.Day08
{

    [Transaction(TransactionMode.Manual)]
    public class LearnVector : RevitCommand
    {
        public override void Action()
        {
            XYZ u = XYZ.BasisX;
            XYZ v = XYZ.BasisY;
            XYZ w = XYZ.BasisZ;

            //dot product : Angle Between Vector
            double dotProduct = u.DotProduct(v); // cos90 = 0
            MessageBox.Show($"Angle : {dotProduct}");

            //cross product : matrix
            //https://en.wikipedia.org/wiki/Cross_product

            XYZ crossProduct = u.CrossProduct(v); //Cross Same Direction with Z
            crossProduct.ShowMessageBox(nameof(crossProduct));

            //This article is about ternary operations on vectors. For the identity in number theory, see Jacobi triple product.
            // https://en.wikipedia.org/wiki/Triple_product
            //(u,v,w)=u⋅(v×w)=v⋅(w×u)=w⋅(u×v)
            //(u,v,w)=(w,u,v)=(v,w,u)=−(v,u,w)=−(w,v,u)=−(u,w,v)
            // ku⋅(v×w)=k(u,v,w)
            //(u,v,w)=u⋅(v×w)
            double tripleProduct = u.TripleProduct(v, w);
            System.Windows.Forms.MessageBox.Show($"{nameof(tripleProduct)}: {tripleProduct}");

            // Write By Step By Step 
            //Dot Product
            DotProduct(u, v).ShowMessageBox();
            //Cross Product
            CrossProduct(u, v).ShowMessageBox();
        }

        double DotProduct(XYZ u, XYZ v)
        {
            return u.X * v.X + u.Y * v.Y + u.Z * v.Z;
        }
        XYZ CrossProduct(XYZ u, XYZ v)
        {
            double a = u.Y * v.Z - u.Z * v.Y;
            double b = u.Z * v.X - u.X * v.Z;
            double c = u.X * v.Y - u.Y * v.X;
            return new XYZ(a, b, c);
        }
    }
}
