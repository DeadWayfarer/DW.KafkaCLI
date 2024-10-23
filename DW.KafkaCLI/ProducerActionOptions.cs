using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DW.KafkaCLI
{
    internal class ProducerActionOptions : KafkaCLIOptions
    {
        [Option(shortName: 't', longName: "topic", Required = false, HelpText = "Топик")]
        public string Topic { get; set; }
        [Option(shortName: 'k', longName: "key", Required = false, HelpText = "Ключ сообщения")]
        public string Key { get; set; }
        [Option(shortName: 'v', longName: "value", Required = false, HelpText = "Значение")]
        public string Value { get; set; }
    }
}
