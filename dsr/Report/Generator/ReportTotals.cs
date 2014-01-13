using System;
using System.Collections.Generic;
using System.IO;
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
		
		public void handleFile(FileInfo f)
		{
			_fileCount++;
			_totalSize += (ulong)f.Length;
		}
		
		public void handleDirectory(DirectoryInfo d)
		{
			_directoryCount++;
		}
		
		public ReportResponse getResult()
		{
			var resp = new ReportResponse();
			resp.Totals.Add(new KeyValuePair<string, string>("Total file count", _fileCount.ToString()));
			resp.Totals.Add(new KeyValuePair<string, string>("Total directory count", _directoryCount.ToString()));
			resp.Totals.Add(new KeyValuePair<string, string>("Total size", InOut.humanizeFilesize(_totalSize, !_rq.RawSizeFormat)));
			resp.Totals.Add(new KeyValuePair<string, string>("Total running time", InOut.humanizeSeconds(_rq.Timer.Elapsed)));
			
			return resp;
		}
	}
}