using System;

namespace dsr.Report.StateModel
{
	class ReportRequest
	{
		public string Subject {get;set;}
		public System.Diagnostics.Stopwatch Timer {get;set;}
		public bool RawSizeFormat {get;set;}
	}
}
