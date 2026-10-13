using Newtonsoft.Json;
using RentVilla;
using System.ComponentModel;
using System.Text;
using System.Text.Json.Serialization;
using VillaWebApp.Models;
using VillaWebApp.Services.IServices;

namespace VillaWebApp.Services
{
    public class BaseService : IBaseService
    {
        public APIResponse responseModel { get; set; }
        public IHttpClientFactory httpClientFactory { get; set; }   //get Request,send to external api ,recive response 
        public BaseService(IHttpClientFactory httpClientFactory)
        {
            this.responseModel = new APIResponse();
            this.httpClientFactory = httpClientFactory;
        }


        public async Task<T> SendAsync<T>(APIRequest apirequest)
        {
            try
            {
                var client = httpClientFactory.CreateClient("MagicAPI");
                HttpRequestMessage massage = new HttpRequestMessage();
                massage.Headers.Add("Accept", "application/json");
                massage.RequestUri = new Uri(apirequest.Url);
                if (apirequest.Data != null)
                {
                    //  massage.Content = new StringContent(JsonConvert.SerializeObject(apirequest.Data));
                    massage.Content = new StringContent(
                        JsonConvert.SerializeObject(apirequest.Data),
                        Encoding.UTF8,
                        "application/json");
                }


                switch (apirequest.apiType)
                {
                    case SD.ApiType.POST:
                        massage.Method = HttpMethod.Post;
                        break;
                    case SD.ApiType.PUT:
                        massage.Method = HttpMethod.Put;
                        break;
                    case SD.ApiType.DELETE:
                        massage.Method = HttpMethod.Delete;
                        break;
                    case SD.ApiType.GET:
                        massage.Method = HttpMethod.Get;
                        break;

                }
                HttpResponseMessage apiresponse = null;
                apiresponse = await client.SendAsync(massage);
                var apicontent = await apiresponse.Content.ReadAsStringAsync();
                var APIResponse = JsonConvert.DeserializeObject<T>(apicontent);
                return APIResponse;
            }
            catch (Exception ex)
            {
                var dto = new APIResponse
                {
                     ErrorMassage = new List<string> { Convert.ToString(ex.Message) },
                     IsSuccess=false
                };
                var res = JsonConvert.SerializeObject(dto);
                var apires = JsonConvert.DeserializeObject<T>(res);
                return apires;
            }
        }

    }
}
