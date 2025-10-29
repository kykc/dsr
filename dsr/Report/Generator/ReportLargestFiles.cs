using System;
using dsr.Report.StateModel;
using System.IO;
using System.Linq;
using System.Collections.Concurrent;

namespace dsr.Report.Generator
{
	class ReportLargestFiles : IReportGenerator
	{
		public ReportLargestFiles(ReportRequest rq, uint limit)
		{
			_limit = limit;
			_rq = rq;
		}
		
		private uint _limit = 10;
		private ReportRequest _rq;
		private ConcurrentBag<FileInfo> _db = new();
		
		private ReportResponse _result = new ReportResponse();

		public void HandleFile(FileInfo f)
		{
			_db.Add(f);
		}
		
		public void HandleDirectory(DirectoryInfo d)
		{
		}
		
		public ReportResponse GetResult()
		{
			_result.Name = "Largest files";
			_result.Members = _db
				.OrderByDescending(x => x.Length)
				.Take((int)_limit)
				.Select(x => ReportResponseMember.Make(x, !_rq.RawSizeFormat))
				.ToList();
			
			return _result;
		}
	}	
}