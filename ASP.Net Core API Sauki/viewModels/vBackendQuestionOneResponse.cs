using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.Net_Core_API_Sauki.viewModels
{
    public class vBackendQuestionOneResponse
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string footer { get; set; }
        public DateTime createdAt { get; set; }
    }
}
