using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using dsr.Report;

namespace dsr
{
	static class MainRecursiveLoop
	{
		public static Action<string, List<IReportGenerator>, List<IReportFilter>, ITrace> make()
		{
			Action<string, List<IReportGenerator>, List<IReportFilter>, ITrace> self = null!;
			
			self = (subject, reports, filters, trace) =>
			{
				try
				{
					foreach (var path in Directory.GetFiles(subject))
					{
						try
						{
							var file = new FileInfo(path);
							
							if (Util.filter(file, filters, (x, y) => x.filterFile(y)))
							{
								reports.ForEach(x => x.handleFile(file));
							}
						}
						catch (Exception ex)
						{
							trace.pushWarning("Cannot get file info of [" + path + "] with error [" + ex.GetType().Name + " - " + ex.Message + "]");
						}
					}
				}
				catch
				{
					trace.pushWarning("Cannot list files of [" + subject + "]");
				}
				
				try
				{
					foreach (var path in Directory.GetDirectories(subject))
					{
						try
						{
							var dir = new DirectoryInfo(path);
							
							if (Util.filter(dir, filters, (x, y) => x.filterDirectory(y)))
							{
								reports.ForEach(x => x.handleDirectory(dir));
								
								self(dir.FullName, reports, filters, trace);
							}
						}
						catch (Exception ex)
						{
							trace.pushWarning("Cannot get directory info of [" + path + "] with error [" + ex.GetType().Name + " - " + ex.Message + "]");
						}
					}
				}
				catch
				{
					trace.pushWarning("Cannot list subdirectories of [" + subject + "]");
				}
			};
			
			return self;
		}
	}
}