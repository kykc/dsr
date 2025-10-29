using System;
using System.Collections.Concurrent;
using System.Linq;
using System.IO;
using dsr.Report;

namespace dsr.Report.Generator
{
	internal sealed class ReportTopFileCount : IReportGenerator
	{
		private readonly uint _limit;
		private readonly ConcurrentDictionary<string, uint> _hash = new();
		private readonly StateModel.ReportRequest _rq;
		
		public ReportTopFileCount(StateModel.ReportRequest rq, uint limit = 10)
		{
			_limit = limit;
			_rq = rq;
		}
		
		public void HandleFile(System.IO.FileInfo f)
		{
			string? parent = f.Directory?.FullName;

			if (parent == null)
			{
				return;
			}

			if (!_hash.TryAdd(parent, 1))
			{
				_hash[parent] += 1;
			}
		}
		
		public void HandleDirectory(System.IO.DirectoryInfo d)
		{
			
		}
		
		public dsr.Report.StateModel.ReportResponse GetResult()
		{
			var resp = new StateModel.ReportResponse();
			resp.Name = "Top file count";
			
			resp.Members = _hash
				.AsEnumerable()
				.OrderByDescending(x => x.Value)
				.Take((int)_limit)
				.Select(x => new StateModel.ReportResponseMember(x.Key, InOut.HumanizeCount(x.Value, !_rq.RawSizeFormat)))
				.ToList();
			
			return resp;
		}
	}
}