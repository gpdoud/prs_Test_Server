using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prs_Test_Server {

    /// <summary>
    /// Args - parses the command line
    ///
    /// Parameters start with double dash (i.e. --p1, --a)
    /// Optionaly values for parameters are separated by a space (i.e. --a 123 --b 456 --c --d)
    ///
    /// The dictionary returned will have the parameter with the dashes as the key
    /// and the value or null as the data
    /// </summary>
    internal class Args {

        static Dictionary<string, string> parms = new Dictionary<string, string>();

        internal static IDictionary<string, string> Parse(string[] args) {
            var arguments = new List<string>(args);
            arguments.Add("--");
            // 1st arg is the command
            parms.Add("cmd", arguments[0]);
            arguments.RemoveAt(0);
            for(var idx = 0; idx < arguments.Count - 1; idx++) {
                var key = arguments[idx];
                if (!key.StartsWith("--"))
                    continue;
                var val = arguments[idx + 1].StartsWith("--") ? null : arguments[idx + 1];
                parms.Add(key, val);
            }

            return parms;
        }

    }
}
