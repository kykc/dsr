using System;
using dsr.Report.StateModel;
using System.IO;
using System.Linq;
using System.Collections.Generic;

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
		private List<FileInfo> _db = new List<FileInfo>();
		
		private ReportResponse _result = new ReportResponse();

		public void handleFile(FileInfo f)
		{
			_db.Add(f);
			
			_db = _db
				.OrderByDescending(x => x.Length)
				.Take((int)_limit)
				.ToList();
		}
		
		public void handleDirectory(DirectoryInfo d)
		{
		}
		
		public ReportResponse getResult()
		{			
			_result.Name = "Largest files";
			_result.Members = _db
				.Select(x => ReportResponseMember.make(x, !_rq.RawSizeFormat))
				.ToList();
			
			return _result;
		}
	}	
}