using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ASP.Net_Core_API_Sauki.viewModels;
using ASP.Net_Core_API_Sauki.Models;

namespace ASP.Net_Core_API_Sauki.Controllers.backend.question
{
    [Route("backend/[controller]")]
    [ApiController]
    public class questionController : ControllerBase
    {
        string content = string.Empty;

        [HttpGet("one")]
        public IEnumerable<vBackendQuestionOneResponse> Getone()
        {
            var URL = "https://screening.moduit.id/backend/question/one";
            var webrequest = (HttpWebRequest)System.Net.WebRequest.Create(URL);
            webrequest.Method = WebRequestMethods.Http.Get;
            webrequest.ContentType = "application/json";

            try
            {
                HttpWebResponse response = (HttpWebResponse)webrequest.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        content = sr.ReadToEnd();
                    }
                }

            }
            catch (Exception er)
            {
                Console.WriteLine(er.ToString());
            }

            var deserializedFQJVModel = JsonConvert.DeserializeObject<vBackendQuestionOneResponse>(content);
            var IobOne = new vBackendQuestionOneResponse()
            {
                id = deserializedFQJVModel.id,
                title = deserializedFQJVModel.title,
                description = deserializedFQJVModel.description,
                footer = deserializedFQJVModel.footer,
                createdAt = deserializedFQJVModel.createdAt,
            };
            yield return IobOne;
        }

        [HttpGet("two")]
        public IEnumerable<BackendQuestionOneResponse> Gettwo()
        {
            var URL = "https://screening.moduit.id/backend/question/two";
            var webrequest = (HttpWebRequest)System.Net.WebRequest.Create(URL);
            webrequest.Timeout = System.Threading.Timeout.Infinite;
            webrequest.Method = WebRequestMethods.Http.Get;
            webrequest.ContentType = "application/json";

            try
            {
                HttpWebResponse response = (HttpWebResponse)webrequest.GetResponse();//WithTimeout();

                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        content = sr.ReadToEnd();
                    }
                }

            }
            catch (Exception er)
            {
                Console.WriteLine(er.ToString());
            }

            var deserializedFQJVModel = JsonConvert.DeserializeObject<List<BackendQuestionOneResponse>>(content);
            var ress = deserializedFQJVModel.Where(x => (x.description.Contains("Ergonomic") || x.title.Contains("Ergonomic")) && x.tags != null && x.tags.Contains("Sports")).OrderByDescending(x => x.id).TakeLast(3);

            foreach (var f in ress)
            {
                var IobTwo = new BackendQuestionOneResponse()
                {
                    id = f.id,
                    category = f.category,
                    title = f.title,
                    description = f.description,
                    footer = f.footer,
                    tags = f.tags,
                    createdAt = f.createdAt,
                };
                yield return IobTwo;
            }
        }

        [HttpGet("three")]
        public IEnumerable<vBackendQuestionThreeResponse> Getthree()
        {
            var URL = "https://screening.moduit.id/backend/question/three";
            var webrequest = (HttpWebRequest)System.Net.WebRequest.Create(URL);
            webrequest.Timeout = System.Threading.Timeout.Infinite;
            webrequest.Method = WebRequestMethods.Http.Get;
            webrequest.ContentType = "application/json";

            try
            {
                HttpWebResponse response = (HttpWebResponse)webrequest.GetResponse();//WithTimeout();

                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        content = sr.ReadToEnd();
                    }
                }

            }
            catch (Exception er)
            {
                Console.WriteLine(er.ToString());
            }

            List<BackendQuestionThreeResponse> deserializedFQJVModel = JsonConvert.DeserializeObject<List<BackendQuestionThreeResponse>>(content);

            string title = string.Empty;
            string desc = string.Empty;
            string footer = string.Empty;

            foreach (var f in deserializedFQJVModel)
            {
                if (f.items != null)
                {
                    foreach (var i in f.items)
                    {
                        title = i.title;
                        desc = i.description;
                        footer = i.footer;

                        var d = new BackendQuestionThreeItem();
                        d.description = desc;
                        d.footer = footer;
                        var IobTwo = new vBackendQuestionThreeResponse()
                        {
                            id = f.id,
                            category = f.category,
                            title = title,
                            description = d.description,
                            footer = d.footer,
                            createdAt = f.createdAt,
                        };
                        yield return IobTwo;
                    }
                }
            }
        }



    }
}
