using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace DW.KafkaCLI
{
    public class KafkaCLIOptions
    {
        [Value(index: 0, Required = true, HelpText = "Имя исполняемого запроса")]
        public string ExecutionName { get; set; }

        [Value(index: 1, Required = true, HelpText = "Нужно указать действие, которое должен выполнить CLI")]
        public string Action { get; set; }

        [Value(index: 2, Required = true, HelpText = "Список брокеров кластера Kafka (через ,)")]
        public string BootstrapServers { get; set; }
    }
}
