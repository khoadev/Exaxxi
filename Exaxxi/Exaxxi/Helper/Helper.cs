using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Exaxxi.Helper
{
    public class DepartmentsAPI
    {
        public HttpClient Initial()
        {
            var Client = new HttpClient();

            //Uri - Escanor: <!--http://localhost:51340 -->
            Client.BaseAddress = new Uri("http://localhost:51340");
            return Client;
        }
    }
}
