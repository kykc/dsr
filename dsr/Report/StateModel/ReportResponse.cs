using System;
using System.Collections.Generic;

namespace dsr.Report.StateModel
{
	class ReportResponse
	{
		public ReportResponse()
		{
			Members = new List<ReportResponseMember>();
			Name = "";
			Totals = new List<KeyValuePair<string, string>>();
		}
		
		public List<ReportResponseMember> Members {get;set;}
		public string Name {get;set;}
		public List<KeyValuePair<string, string>> Totals {get;set;}
	}
}