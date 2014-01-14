using System;
using System.IO;

namespace dsr.Report.StateModel
{
	class ReportResponseMember
	{
		public string Path {get;set;}
		public string FormattedNumber {get;set;}
		
		public ReportResponseMember(string path, string formattedNumber)
		{
			Path = path;
			FormattedNumber = formattedNumber;
		}
		
		public static ReportResponseMember make(FileInfo f, bool humanize = true)
		{
			return new ReportResponseMember(f.FullName, InOut.humanizeFilesize((ulong)f.Length, humanize));
		}
	}
}
