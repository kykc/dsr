using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace dsr
{
	internal static class InOut
	{
		static InOut()
	    {
			Units = new List<string>(){
				"B", "KB", "MB", "GB", "TB"
			};
	    }
		
		private const int PRECISION = 2;
		
	    private static IList<string> Units;
		
		public static string humanizeFilesize(ulong size, bool human = true)
		{
			if (!human)
			{
				return size.ToString();
			}
			
			ulong bytes = size;
			
			double pow = Math.Floor((bytes>0 ? Math.Log(bytes) : 0) / Math.Log(1024));
        	pow = Math.Min(pow, Units.Count-1);
        	double value = (double)bytes / Math.Pow(1024, pow);
        	return value.ToString(pow==0 ? "F0" : "F" + PRECISION.ToString()) + "" + Units[(int)pow];
		}
		
		public static string humanizeCount(ulong count, bool human = true)
		{
			return count.ToString();
		}
		
		public static string humanizeSeconds(TimeSpan t)
		{
			string answer = string.Format("{0:D2}m:{1:D2}s:{2:D3}ms", 
    			t.Minutes, 
    			t.Seconds, 
    			t.Milliseconds);
			
			return answer;
		}
		
		public static Int32 parse(string s, Int32 def)
		{
			Int32 temp = 0;
			
			return Int32.TryParse(s, out temp) ? temp : def;
		}
		
		public static UInt32 parse(string s, UInt32 def)
		{
			UInt32 temp = 0;
			
			return UInt32.TryParse(s, out temp) ? temp : def;
		}
		
		public static string parseSubject(List<string> args)
		{
			string result = null;
			
			if (args.Count == 1)
			{
				string subject = Path.GetFullPath(args.First());
				
				if (Directory.Exists(subject))
				{
					result = subject;
				}
			}
			
			return result;
		}
		
		public static string indent(uint indent)
		{
			return new String(' ', (int)indent * 2);
		}
		
		public static void outputStdSection(string header, IEnumerable<string> list, uint ind = 0)
		{
			if (header.Length > 0)
			{
				Console.WriteLine(indent(ind++) + header + ":");
			}
			
			list.ForEach((str) => Console.WriteLine(indent(ind) + str));
			
			Console.WriteLine(indent(ind));
		}
		
		public static string getLicenseText()
		{
			return @"Copyright 2014 Aleksandr Prokopchuk <ya@tomatl.org>. 
All rights reserved.

Redistribution and use in source and binary forms, with or without modification, are
permitted provided that the following conditions are met:

   1. Redistributions of source code must retain the above copyright notice, this list of
      conditions and the following disclaimer.

   2. Redistributions in binary form must reproduce the above copyright notice, this list
      of conditions and the following disclaimer in the documentation and/or other materials
      provided with the distribution.

THIS SOFTWARE IS PROVIDED BY ITS COPYRIGHT HOLDER ``AS IS'' AND ANY EXPRESS OR IMPLIED
WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR
CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON
ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF
ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

The views and conclusions contained in the software and documentation are those of the
authors and should not be interpreted as representing official policies, either expressed
or implied, of the copyright holder.
";
		}
	}
}
