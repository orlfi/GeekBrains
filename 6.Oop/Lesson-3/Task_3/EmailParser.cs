using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.CompilerServices;

namespace Task_3
{
    public class EmailParser
    {
        private static string regTemplate = @"([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})";
        private readonly string _fileName;
        public EmailParser(string fileName) => _fileName = fileName;



        public async IAsyncEnumerable<string> EnumEmailsAsync([EnumeratorCancellation] CancellationToken Cancel = default)
        {
            using StreamReader sr = new(_fileName);
            while (!sr.EndOfStream)
            {
                var line = await sr.ReadLineAsync().ConfigureAwait(false);
                SearchMail(ref line);
                if (line is { Length: > 0 })
                {
                    yield return line;
                }
            }
        }

        public static void SearchMail(ref string s)
        {
            Regex reg = new Regex(regTemplate, RegexOptions.IgnoreCase);
            var firstMatch = reg.Matches(s).FirstOrDefault();
            s = firstMatch?.Value ?? "";
        }
    }
}