using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Exaxxi.Common
{
    public class GoogleRecaptchaHelper
    {
        // A function that checks reCAPTCHA results
        public static bool IsReCaptchaPassedAsync(string gRecaptchaResponse, string secret)
        {
            HttpClient httpClient = new HttpClient();
            //var content = new FormUrlEncodedContent(new[]
            //{
            //    new KeyValuePair<string, string>("secret", secret),
            //    new KeyValuePair<string, string>("response", gRecaptchaResponse)
            //});
            var res = httpClient.GetAsync($"https://www.google.com/recaptcha/api/siteverify?secret={secret}&response={gRecaptchaResponse}").Result;
            if (res.StatusCode != HttpStatusCode.OK)
            {
                return false;
            }

            string JSONres = res.Content.ReadAsStringAsync().Result;
            dynamic JSONdata = JObject.Parse(JSONres);
            if (JSONdata.success != "true")
            {
                return false;
            }

            return true;
        }
    }
}
