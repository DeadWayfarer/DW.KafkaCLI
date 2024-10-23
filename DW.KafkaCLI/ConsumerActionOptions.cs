using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DW.KafkaCLI
{
    internal class ConsumerActionOptions : KafkaCLIOptions
    {
        [Option(shortName: 't', longName: "topic", Required = false, HelpText = "Топик")]
        public string Topic { get; set; }
        [Option(shortName: 'g', longName: "groupid", Required = false, HelpText = "Группа консьюмера, по умолчанию генерируется guid")]
        public string ConsumerGroupId { get; set; }
        [Option(shortName: 'c', longName: "count", Required = false, HelpText = "Кол-во сообщений для чтения")]
        public int Count { get; set; }
        [Option(shortName: 'd', longName: "delay", Required = false, HelpText = "Время чтения сообщений в секундах")]
        public int Delay { get; set; }
        [Option(shortName: 'r', longName: "regex", Required = false, HelpText = "Фильтр в формате регулярного выражения")]
        public string Regex { get; set; }
        [Option(shortName: 's', longName: "since", Required = false, HelpText = "Дата с которой будет выводится сообщения")]
        public DateTime Since { get; set; }
    }
}
