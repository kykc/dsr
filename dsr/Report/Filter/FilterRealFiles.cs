using System;

namespace dsr.Report.Filter
{
	class FilterRealFiles : IReportFilter
	{
		public bool FilterFile(System.IO.FileInfo f)
		{
			return !System.IO.File.GetAttributes(f.FullName).HasFlag(System.IO.FileAttributes.ReparsePoint);
		}
		
		public bool FilterDirectory(System.IO.DirectoryInfo d)
		{
			return !System.IO.File.GetAttributes(d.FullName).HasFlag(System.IO.FileAttributes.ReparsePoint);
		}
	}
}