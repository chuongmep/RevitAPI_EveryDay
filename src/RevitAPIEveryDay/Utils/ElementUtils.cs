using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;

namespace Library
{
    public static class ElementUtils
    {

        /// <summary>
        /// Filter Element By Categories
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="categoryNames"></param>
        /// <returns></returns>
        public static ElementFilter FiltersElementByCategory(this Document doc, IEnumerable<string> categoryNames)
        {

            ElementFilter categoriesFilter = null;
            if (categoryNames != null && categoryNames.Any())
            {
                var categoryIds = new List<ElementId>();
                foreach (var categoryName in categoryNames)
                {
                    var category = doc.Settings.Categories.get_Item(categoryName);
                    if (category != null)
                    {
                        categoryIds.Add(category.Id);
                    }
                }

                var categoryFilters = new List<ElementFilter>();
                if (categoryIds.Count > 0)
                {
                    var categoryRule = new FilterCategoryRule(categoryIds);
                    var categoryFilter = new ElementParameterFilter(categoryRule);
                    categoryFilters.Add(categoryFilter);
                }
                if (categoryFilters.Count > 0)
                {
                    categoriesFilter = new LogicalOrFilter(categoryFilters);
                }
            }

            return categoriesFilter;
        }

        /// <summary>
        /// Find Elements Intersect By Ray 
        /// </summary>
        /// <param name="element"></param>
        /// <param name="categories"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static Autodesk.Revit.DB.XYZ RayIntersect(this Autodesk.Revit.DB.Element element, List<string> categories, XYZ direction)
        {
            // Find a 3D view to use for the ReferenceIntersector constructor

            FilteredElementCollector collector = new FilteredElementCollector(element.Document);
            Func<View3D, bool> isNotTemplate = v3 => !(v3.IsTemplate);
            View3D view3D = collector.OfClass(typeof(View3D)).Cast<View3D>().First<View3D>(isNotTemplate);

            // Use the center of the skylight bounding box as the start point.
            BoundingBoxXYZ box = element.get_BoundingBox(view3D);
            XYZ center = box.Min.Add(box.Max).Multiply(0.5);
            // Project in the negative Z direction down to the ceiling.
            ElementFilter elementFilter = element.Document.FiltersElementByCategory(categories);
            ReferenceIntersector refIntersector = new ReferenceIntersector(elementFilter, FindReferenceTarget.Face, view3D);
            refIntersector.FindReferencesInRevitLinks = true;
            ReferenceWithContext referenceWithContext = refIntersector.FindNearest(center, direction);
            Reference reference = referenceWithContext?.GetReference();
            return reference?.GlobalPoint;
        }
    }
}
