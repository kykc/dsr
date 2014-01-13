using System;
using System.IO;
using System.Collections.Generic;

namespace dsr.Report
{
	interface IReportGenerator
	{
		void handleFile(FileInfo f);
		void handleDirectory(DirectoryInfo d);
		
		StateModel.ReportResponse getResult();
	}
	
	interface IReportFilter
	{
		bool filterFile(FileInfo f);
		bool filterDirectory(DirectoryInfo d);
	}
	
	interface ITrace
	{
		void pushError(string error);
		void pushWarning(string warning);
		void pushInfo(string info);
		
		List<string> Warning {get;}
	}
}
