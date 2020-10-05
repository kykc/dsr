using System;
using System.Security.Cryptography.X509Certificates;

namespace dsr.Report.StateModel
{
	class ReportRequest
	{
		public ReportRequest(string subject, System.Diagnostics.Stopwatch timer, bool rawSizeFormat = false) => (Subject, Timer, RawSizeFormat) = (subject, timer, rawSizeFormat);

		public string Subject { get; set; }
		public System.Diagnostics.Stopwatch Timer { get; set; }
		public bool RawSizeFormat {get;set;}
	}
}
