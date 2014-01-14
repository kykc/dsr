using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using dsr.Report;

namespace dsr.Report.Generator
{
	internal sealed class ReportTopFileCount : IReportGenerator
	{
		private uint _limit = 10;
		private Dictionary<string, uint> _hash = new Dictionary<string, uint>();
		StateModel.ReportRequest _rq;
		
		public ReportTopFileCount(StateModel.ReportRequest rq, uint limit)
		{
			_limit = limit;
			_rq = rq;
		}
		
		public void handleFile(System.IO.FileInfo f)
		{
			string parent = f.Directory.FullName;
			
			if (_hash.ContainsKey(parent))
			{
				_hash[parent] += 1;
			}
			else
			{
				_hash[parent] = 1;
			}
		}
		
		public void handleDirectory(System.IO.DirectoryInfo d)
		{
			
		}
		
		public dsr.Report.StateModel.ReportResponse getResult()
		{
			var resp = new StateModel.ReportResponse();
			resp.Name = "Top file count";
			
			resp.Members = _hash
				.AsEnumerable()
				.OrderByDescending(x => x.Value)
				.Take((int)_limit)
				.Select(x => new StateModel.ReportResponseMember(x.Key, InOut.humanizeCount(x.Value, !_rq.RawSizeFormat)))
				.ToList();
			
			return resp;
		}
	}
}