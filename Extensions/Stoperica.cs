using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    public static class Stoperica {
        public static string ToTime(uint sec) {
            return DateTime.Today.AddSeconds(sec).ToString("HH:mm:ss");
        }
    }
}
