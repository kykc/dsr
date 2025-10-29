using System;
using System.Collections.Concurrent;
using System.Linq;
using System.IO;
using dsr.Report.StateModel;

namespace dsr.Report.Generator
{
	class ReportLargestDirectories : IReportGenerator
	{
		private readonly uint _limit;
		private readonly ReportResponse _result = new ReportResponse();
		private readonly ReportRequest _rq;
		private readonly ConcurrentDictionary<string, ulong> _hash = new();
		
		public ReportLargestDirectories(ReportRequest rq, uint limit = 10)
		{
			_limit = limit;
			_rq = rq;
		}
		
		public void HandleFile(FileInfo f)
		{
			var dir = f.Directory?.FullName;
			
			while (dir != null && dir.Length >= _rq.Subject.Length)
			{
				_hash.TryAdd(dir, 0);
				
				_hash[dir] += (ulong)f.Length;
				
				dir = new DirectoryInfo(dir).Parent?.FullName;
			}
		}
		
		public void HandleDirectory(DirectoryInfo d)
		{
			
		}
		
		public ReportResponse GetResult()
		{
			_result.Members = _hash
				.OrderByDescending(pair => pair.Value)
				.Skip(1)
				.Take((int)_limit)
				.Select(x => new ReportResponseMember(x.Key, InOut.HumanizeFilesize(x.Value, !_rq.RawSizeFormat)))
				.ToList();
			
			_result.Name = "Largest directories";
			
			return _result;
		}
	}
}