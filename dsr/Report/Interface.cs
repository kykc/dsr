using System;
using System.IO;
using System.Collections.Generic;

namespace dsr.Report
{
	interface IReportGenerator
	{
		void HandleFile(FileInfo f);
		void HandleDirectory(DirectoryInfo d);
		
		StateModel.ReportResponse GetResult();
	}
	
	interface IReportFilter
	{
		bool FilterFile(FileInfo f);
		bool FilterDirectory(DirectoryInfo d);
	}
	
	interface ITrace
	{
		void Error(string error);
		void Warning(string warning);
		void Log(string info);
		
		IEnumerable<string> Warnings { get; }
	}
}
