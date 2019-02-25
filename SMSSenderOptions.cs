using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace SMSSender
{
    public class SMSSenderOptions
    {
        public string AppUrl { get; set; }

        /// <summary>
        /// default false use get method,when true use post method.
        /// </summary>
        public bool UsePostMethod { get; set; }

        /// <summary>
        /// input name that api send to destination.
        /// </summary>
        public string Destination { get; set; }

        /// <summary>
        /// input name that api send message conent.
        /// </summary>
        public string MessageContent { get; set; }

        public readonly IDictionary<string, string> Parameters = new Dictionary<string, string>();

        public string RequestUri(string destination, string message)
        {
            if (string.IsNullOrWhiteSpace(Destination)) throw new Exception("not found the name of Destination");
            if (string.IsNullOrWhiteSpace(MessageContent)) throw new Exception("not found the name of MessageContent");
            return Parameters.Count < 1 ? $"{AppUrl}?{Destination}={destination}&{MessageContent}={message}" : $"{AppUrl}?{string.Join("&", Parameters.Select(m => $"{m.Key}={m.Value}"))}&{Destination}={destination}&{MessageContent}={message}";
        }

        public StringContent HttpContent(string destination, string message)
        {
            return new StringContent($"{{{string.Join(",", Parameters.Select(m => $"\"{m.Key}\":\"{m.Value}\""))},\"{Destination}\":\"{destination}\",\"{MessageContent}\":\"{message}\"}}", System.Text.Encoding.UTF8, "application/json");
        }
    }


}
