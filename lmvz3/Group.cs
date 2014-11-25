using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lmvz3
{
    public class Group
    {
        public String Title { get; set; }
        public long GroupId { get; set; }
        public long FacultiesId { get; set; }

        public Group()
        {

        }

        public Group(String title, long facultiesId)
        {
            Title = title;
            FacultiesId = facultiesId;
        }

        public Group(long id, String title, long facultiesId)
        {
            Title = title;
            FacultiesId = facultiesId;
            GroupId = id;
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
