using System;
using dsr.Report.StateModel;
using System.IO;
using System.Linq;

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
		#pragma warning disable 0414
		private ReportRequest _rq;
		#pragma warning restore 0414
		
		private ReportResponse _result = new ReportResponse();

		public void handleFile(FileInfo f)
		{
			var file = ReportResponseMember.make(f);

			_result.Members.Add(file);
			
			_result.Members = _result.Members
				.OrderByDescending(x => x.Size)
				.Take((int)_limit)
				.ToList();
		}
		
		public void handleDirectory(DirectoryInfo d)
		{
		}
		
		public ReportResponse getResult()
		{			
			_result.Name = "Largest files";
			
			return _result;
		}
	}	
}