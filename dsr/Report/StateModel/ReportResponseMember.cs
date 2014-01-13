using System;
using System.IO;

namespace dsr.Report.StateModel
{
	class ReportResponseMember
	{
		public string Path {get;set;}
		public ulong Size {get;set;}
		public string Comment {get;set;}
		
		public static ReportResponseMember make(FileInfo f)
		{
			var obj = new ReportResponseMember();
			obj.Path = f.FullName;
			obj.Size = (ulong)f.Length;
			
			return obj;
		}
		
		public static ReportResponseMember make(DirectoryInfo d, ulong size)
		{
			var obj = new ReportResponseMember();
			obj.Path = d.FullName;
			obj.Size = size;
			
			return obj;
		}
	}
}
