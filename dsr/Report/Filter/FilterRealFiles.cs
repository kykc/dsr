using System;

namespace dsr.Report.Filter
{
	class FilterRealFiles : IReportFilter
	{
		public bool filterFile(System.IO.FileInfo f)
		{
			return !System.IO.File.GetAttributes(f.FullName).HasFlag(System.IO.FileAttributes.ReparsePoint);
		}
		
		public bool filterDirectory(System.IO.DirectoryInfo d)
		{
			return !System.IO.File.GetAttributes(d.FullName).HasFlag(System.IO.FileAttributes.ReparsePoint);
		}
	}
}