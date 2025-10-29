using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using dsr.Report.StateModel;

namespace dsr.Report.Generator
{
	class ReportTotals : IReportGenerator
	{
		private ReportRequest _rq;
		private ulong _fileCount = 0;
		private ulong _directoryCount = 0;
		private ulong _totalSize = 0;
		
		public ReportTotals(ReportRequest rq)
		{
			_rq = rq;
		}
		
		public void HandleFile(FileInfo f)
		{
			Interlocked.Increment(ref _fileCount);
			Interlocked.Add(ref _totalSize, (ulong)f.Length);
		}
		
		public void HandleDirectory(DirectoryInfo d)
		{
			Interlocked.Increment(ref _directoryCount);
		}
		
		public ReportResponse GetResult()
		{
			var resp = new ReportResponse();
			resp.Totals.Add(new KeyValuePair<string, string>("Total file count", _fileCount.ToString()));
			resp.Totals.Add(new KeyValuePair<string, string>("Total directory count", _directoryCount.ToString()));
			resp.Totals.Add(new KeyValuePair<string, string>("Total size", InOut.HumanizeFilesize(_totalSize, !_rq.RawSizeFormat)));
			resp.Totals.Add(new KeyValuePair<string, string>("Total running time", InOut.HumanizeSeconds(_rq.Timer.Elapsed)));
			resp.Totals.Add(new KeyValuePair<string, string>("Peak memory usage", InOut.HumanizeFilesize((ulong)System.Diagnostics.Process.GetCurrentProcess().PeakWorkingSet64, !_rq.RawSizeFormat)));
			
			return resp;
		}
	}
}