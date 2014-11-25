using System;
using System.Collections;
using System.Collections.Generic;

namespace lmvz3
{
	public class Faculty
	{
		private String title;
		private List<Group> groups;
		private long facultiesId;

		public Faculty(){	
		}

		public Faculty(long id, String title, List<Group> groups){
			setFacultiesId(id);
			setGroups(groups);
			setTitle(title);
		}

		public String getTitle() {
			return title;
		}
		public void setTitle(String title) {
			this.title = title;
		}
		public List<Group> getGroups() {
			return groups;
		}
		public void setGroups(List<Group> groups) {
			this.groups = groups;
		}
		public long getFacultiesId() {
			return facultiesId;
		}
		public void setFacultiesId(long facultiesId) {
			this.facultiesId = facultiesId;
		}
	}
	public class Group
	{
		private String title;
		private long groupId;
		private long facultiesId;

		public Group(){

		}

		public Group(String title, long facultiesId){
			setTitle(title);
			setFacultiesId(facultiesId);
		}

		public Group(long id, String title, long facultiesId){
			this.title = title;
			this.facultiesId = facultiesId;
			setGroupId(id);
		}

		public String getTitle() {
			return title;
		}

		public void setTitle(String title) {
			this.title = title;
		}

		public long getGroupId() {
			return groupId;
		}

		public void setGroupId(long groupId) {
			this.groupId = groupId;
		}

		public long getFacultiesId() {
			return facultiesId;
		}

		public void setFacultiesId(long facultiesId) {
			this.facultiesId = facultiesId;
		}
	}
}

