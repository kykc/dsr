using System;
using System.Collections.Generic;

namespace dsr.Report.StateModel
{
	class SimpleTrace : ITrace
	{
		private readonly List<string> _error = new();
		private readonly List<string> _warning = new();
		private readonly List<string> _info = new();
		
		public void Error(string error)
		{
			_error.Add(error);
		}
		
		public void Warning(string warning)
		{
			_warning.Add(warning);
		}
		
		public void Log(string info)
		{
			_info.Add(info);
		}
		
		public IEnumerable<string> Warnings => _warning;
	}
}