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
	}
}

