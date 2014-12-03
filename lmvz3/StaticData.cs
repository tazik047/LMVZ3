using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lmvz3
{
    static class StaticData
    {
        public static List<Student> students;
        public static List<Faculty> faculties;

        public static List<Group> groups
        {
            get
            {
                return StaticData.faculties.Aggregate(new List<Group>(), (s, f) =>
                {
                    s.AddRange(f.Groups);
                    return s;
                });
            }
        } 
    }
}
