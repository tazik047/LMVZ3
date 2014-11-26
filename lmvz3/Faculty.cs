using System;
using System.Collections;
using System.Collections.Generic;

namespace lmvz3
{
    [Serializable]
	public class Faculty
	{
        public String Title { get; set; }
        public List<Group> Groups { get; set; }
        public long FacultiesId { get; set; }

		public Faculty(){	
		}

		public Faculty(long id, String title, List<Group> groups){
            FacultiesId = id;
            Groups = groups;
            Title = title;
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

            return Title.Equals(((Faculty)obj).Title);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return Title.GetHashCode();
        }
	}
}

