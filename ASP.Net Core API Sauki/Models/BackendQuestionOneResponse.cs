using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.Net_Core_API_Sauki.Models
{
    public class BackendQuestionOneResponse
    {
        public int id { get; set; }
        public int category { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string footer { get; set; }
        public string[] tags { get; set; }
        public DateTime createdAt { get; set; }

    }
}
