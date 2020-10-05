using System;
using System.Linq;
using System.Collections.Generic;
using dsr.Report;

namespace dsr
{
	static internal class Program
	{			
		public static void Main(string[] args)
		{
			bool defaultPause = !Util.IsUnix;
			string? subject = null;
			uint limit = 10;
			bool pause = defaultPause;
			bool defaults = true;
			bool largestFiles = false;
			bool largestDirs = false;
			bool includeTotals = true;
			bool rawSize = false;
			bool fileCountReport = false;
			bool helpTokenFound = false;
			bool licenseTokenFound = false;
			
			var p = new NDesk.Options.OptionSet() {
				{"l|limit=", v => limit = InOut.parse(v, limit)},
				{"h|?|help|version", v => helpTokenFound = v != null},
				{"enable-pause", v => pause = v != null},
				{"disable-pause", v => pause = v == null},
				{"top-files", v => largestFiles = v != null},
				{"top-dirs", v => largestDirs = v != null},
				{"nodef", v => defaults = v == null},
				{"enable-totals", v => includeTotals = v != null},
				{"disable-totals", v => includeTotals = v == null},
				{"raw-file-length", v => rawSize = v != null},
				{"top-file-count", v => fileCountReport = v != null},
				{"license", v => licenseTokenFound = v != null},
			};
			
			List<string> extraArgs = p.Parse(args);
			
			subject = InOut.parseSubject(extraArgs);
			
			if (licenseTokenFound) {
				Console.WriteLine(InOut.getLicenseText());
			} else if (helpTokenFound || subject == null) {
				Func<string, string, KeyValuePair<string, string>> mk = (c, d) => new KeyValuePair<string, string>(c, d);
				
				var generalOptionsHelp = new List<KeyValuePair<string, string>> {
					mk("--limit=LIMIT, -l LIMIT", "number of lines per report (default: 10)"),
					mk("--enable-pause, --disable-pause", "press any key prompt (default: " + (defaultPause ? "enabled" : "disabled") + ")"),
					mk("--nodef", "disables default set of reports"),
					mk("--top-files", "enables largest files report (default: on)"),
					mk("--top-dirs", "enables largest directories report (default: on)"),
					mk("--top-file-count", "enables top file count report (default: off)"),
					mk("--enable-totals, --disable-totals", "toggles totals section (default: enabled)"),
					mk("--raw-file-length", "output file/directory size in bytes"),
					mk("--license", "shows license"),
					mk("--help, -h, --version, /?", "this help page")
				};
				
				Console.WriteLine("DSR: simple disk usage reporter");
				Console.WriteLine("Version: " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Map(x => x.ToString(), "Unknown"));
				Console.WriteLine("Usage: dsr [options] path");
				Console.WriteLine("");
				Console.WriteLine("general options:");
				
				generalOptionsHelp.ForEach(sdef => {
					int leftMargin = 2;
					int cmdWidth = generalOptionsHelp.Max(x => x.Key.Length);
					int descriptionWidth = 80 - leftMargin - cmdWidth - 2 - 1;
					
					cmdWidth *= -1;
					descriptionWidth *= -1;
					
					Console.WriteLine(String.Format("{0,2}{1," + cmdWidth + "}  {2," + descriptionWidth + "}", "", sdef.Key, sdef.Value));
				});
			} else {
				var rq = new Report.StateModel.ReportRequest(subject, new System.Diagnostics.Stopwatch(), rawSize);
				
				var trace = new Report.StateModel.SimpleTrace();
				
				var filters = new List<IReportFilter>{
					new Report.Filter.FilterRealFiles()
				};
				
				var reports = new List<IReportGenerator>{};
				
				Action<bool, Func<IReportGenerator>> _insrep = (b, r) => {if (b) reports.Add(r());};
				
				_insrep(defaults || largestFiles, () => new Report.Generator.ReportLargestFiles(rq, limit));
				_insrep(defaults || largestDirs, () => new Report.Generator.ReportLargestDirectories(rq, limit));
				_insrep(fileCountReport, () => new Report.Generator.ReportTopFileCount(rq, limit));
				_insrep(includeTotals, () => new Report.Generator.ReportTotals(rq));
				
				int fileSizeColumnWidth = 0;
				int totalCaptionColumnWidth = 0;
				
				Func<Report.StateModel.ReportResponse, IEnumerable<string>> genTotalLines = (resp) => {
					return resp.Totals.Select(x => String.Format("{0, -" + (totalCaptionColumnWidth).ToString() + "} {1}", x.Key, x.Value));
				};
				
				Action<Report.StateModel.ReportResponse> outputMembers = (resp) => {
					if (resp.Members.Count > 0) {
						InOut.outputStdSection(resp.Name, resp.Members.Select(x => {
						return String.Format("{1," + (fileSizeColumnWidth).ToString() + "} {2}", "", x.FormattedNumber, x.Path);
						}));
					}
				};
				
				var roller = MainRecursiveLoop.make();
				
				rq.Timer.Start();
				roller(subject, reports, filters, trace);
				var results = reports.Select(x => x.getResult()).ToList();
				
				fileSizeColumnWidth = results.Select(x => x.Members).Concat().Select(x => x.FormattedNumber.Length).DefaultIfEmpty(0).Max();
				totalCaptionColumnWidth = results.Select(x => x.Totals).Concat().Select(x => x.Key.Length).DefaultIfEmpty(0).Max();
				results.ForEach(outputMembers);
				
				if (includeTotals) {
					InOut.outputStdSection("", results.Select(x => genTotalLines(x)).Concat());
				}
				
				trace.Warning.ForEach(x => Console.WriteLine("WARNING: " + x));
			}
			
			if (pause) {
				Console.WriteLine("Press any key to continue . . .");
				Console.ReadKey(true);
			}
		}
	}
}