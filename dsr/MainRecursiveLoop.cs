using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using dsr.Report;

namespace dsr
{
	static class MainRecursiveLoop
	{
		public static Action<string, List<IReportGenerator>, List<IReportFilter>, ITrace> Make(bool parallelProcessing)
		{
			Action<string, List<IReportGenerator>, List<IReportFilter>, ITrace> self = null!;
			
			self = (subject, reports, filters, trace) =>
			{
				try
				{
					Util.ForEach(parallelProcessing, Directory.GetFiles(subject), (path) =>
					{
						try
						{
							var file = new FileInfo(path);

							if (Util.Filter(file, filters, (x, y) => x.FilterFile(y)))
							{
								reports.ForEach(x => x.HandleFile(file));
							}
						}
						catch (Exception ex)
						{
							trace.Warning("Cannot get file info of [" + path + "] with error [" + ex.GetType().Name +
							              " - " + ex.Message + "]");
						}
					});
				}
				catch
				{
					trace.Warning("Cannot list files of [" + subject + "]");
				}
				
				try
				{
					Util.ForEach(parallelProcessing, Directory.GetDirectories(subject), (path) =>
					{
						try
						{
							var dir = new DirectoryInfo(path);

							if (Util.Filter(dir, filters, (x, y) => x.FilterDirectory(y)))
							{
								reports.ForEach(x => x.HandleDirectory(dir));

								self(dir.FullName, reports, filters, trace);
							}
						}
						catch (Exception ex)
						{
							trace.Warning("Cannot get directory info of [" + path + "] with error [" +
							              ex.GetType().Name + " - " + ex.Message + "]");
						}
					});
				}
				catch
				{
					trace.Warning("Cannot list subdirectories of [" + subject + "]");
				}
			};
			
			return self;
		}
	}
}