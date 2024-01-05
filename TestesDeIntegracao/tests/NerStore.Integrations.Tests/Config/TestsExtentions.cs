using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerStore.Integrations.Tests.Config
{
    public static class TestsExtentions
    {
        public static decimal OnlyNumebrs(this string value)
        {
            return Convert.ToDecimal(new string(value.Where(char.IsDigit).ToArray()));
        }
    }
}
