using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WPF_POS.Model;

namespace WPF_POS
{
    public class Util
    {
        public static void GenerateSampleCategory()
        {
            List<Category> Categories = new List<Category>();

            Categories.Add(new Category(1, "Drink"));
            Categories.Add(new Category(2, "Noodle"));
            Categories.Add(new Category(3, "Bakery"));

            new IOManager().Write("Category", Categories);
        }
    }
}