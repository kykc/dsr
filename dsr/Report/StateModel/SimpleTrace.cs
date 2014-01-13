using System;
using System.Collections.Generic;

namespace dsr.Report.StateModel
{
	class SimpleTrace : ITrace
	{
		private List<string> _error = new List<string>();
		private List<string> _warning = new List<string>();
		private List<string> _info = new List<string>();
		
		public void pushError(string error)
		{
			_error.Add(error);
		}
		
		public void pushWarning(string warning)
		{
			_warning.Add(warning);
		}
		
		public void pushInfo(string info)
		{
			_info.Add(info);
		}
		
		public List<string> Warning{get {return _warning;}}
	}
}