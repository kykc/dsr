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
		
		private ReportResponse _result = new();

		public void HandleFile(FileInfo f)
		{
			uint largerThanMe = 0;

			foreach (var x in _db)
			{
				if (x.Length > f.Length)
				{
					largerThanMe++;
				}

				if (largerThanMe >= _limit)
				{
					break;
				}
			}

			if (largerThanMe < _limit)
			{
				_db.Add(f);
			}
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