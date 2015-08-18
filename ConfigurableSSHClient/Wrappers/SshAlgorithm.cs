using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurableSSHClient.Wrappers
{
    public class SshAlgorithm
    {
        public string Name { get; set; }

        public bool IsEnabled { get; set; }
    }
}
