using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using dsr.Report.StateModel;

namespace dsr.Report.Generator
{
	class ReportLargestDirectories : IReportGenerator
	{
		private uint _limit = 10;
		private ReportResponse _result = new ReportResponse();
		private ReportRequest _rq;
		private Dictionary<string, ulong> _hash = new Dictionary<string, ulong>();
		
		public ReportLargestDirectories(ReportRequest rq, uint limit)
		{
			_limit = limit;
			_rq = rq;
		}
		
		public void handleFile(FileInfo f)
		{
			var dir = f.Directory.FullName;
			
			while (dir != null && dir.Length >= _rq.Subject.Length)
			{
				if (!_hash.ContainsKey(dir))
				{
					_hash[dir] = 0;
				}
				
				_hash[dir] += (ulong)f.Length;
				
				dir = new DirectoryInfo(dir).Parent.FullName;
			}
		}
		
		public void handleDirectory(DirectoryInfo d)
		{
			
		}
		
		public ReportResponse getResult()
		{
			_result.Members = _hash
				.OrderByDescending(pair => pair.Value)
				.Skip(1)
				.Take((int)_limit)
				.Select(x => ReportResponseMember.make(new DirectoryInfo(x.Key), x.Value))
				.ToList();
			
			_result.Name = "Largest directories";
			
			return _result;
		}
	}
}