using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Entities;
using System.Threading.Tasks;

namespace Data.Contexts
{
    public static class DbContext
    {
        static DbContext()
        {
            Groups = new List<Group>();
        }
        public static List<Group> Groups { get; set; }
    }
}
