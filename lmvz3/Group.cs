using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lmvz3
{
    [Serializable]
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

        // override object.Equals
        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return Title.Equals(((Group)obj).Title);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return Title.GetHashCode();
        }
    }
}
