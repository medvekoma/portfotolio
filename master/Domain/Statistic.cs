using System.Collections.Generic;

namespace Portfotolio.Domain
{
    public class Statistic
    {
        public string Name { get; private set; }
        public List<KeyValuePair<string, int>> Stats { get; private set; } 

        public Statistic(string name, List<KeyValuePair<string,int>> stats)
        {
            Name = name;
            Stats = stats;
        }
    }
}
