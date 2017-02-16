using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Yumiko.LinqToHtml.Helper
{
    public class Selector
    {
        public static void Print()
        {

            var o = from n in Assembly.GetExecutingAssembly().GetTypes()
                    where n.GetInterfaces().Any(x => x == typeof(Interfaces.ITag)) & n.IsSubclassOf(typeof(Tags.Infrastructure.Tag)) & !n.IsAbstract
                    select n;

            foreach (var item in o)
            {
                Console.WriteLine(item);
            }

        }
    }
}
