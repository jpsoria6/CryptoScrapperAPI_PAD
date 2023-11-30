using CryptoScrapperAPI_PAD.DTOs;
using MediatR;
using Newtonsoft.Json;
using System.Web;

namespace CryptoScrapperAPI_PAD.Features.Coins
{
    public class GetCoinQuery
    {
        public class GetCoinsQuery : IRequest<GetCoinQueryResponse>
        {

        }

        public class GetCoinQueryResponse
        {
            public List<CryptoCurrencyDTO> CryptoCurrencies { get; set; }
        }

        public class ResponseDTO
        {
            public dynamic status { get; set; }
            public List<CryptoCurrencyDTO> data { get; set; }

        }

        public class Handler : IRequestHandler<GetCoinsQuery, GetCoinQueryResponse>
        {
            private readonly IConfiguration _configuracion;
            public Handler(IConfiguration configuration)
            {
                _configuracion = configuration;
            }

            public async Task<GetCoinQueryResponse> Handle(GetCoinsQuery request, CancellationToken cancellationToken)
            {
                bool success;
                try
                {

                    var apiUrl = new UriBuilder(_configuracion.GetSection("CoinMarketCapAPI-URL").Value+ "cryptocurrency/listings/latest");

                    using (HttpClient client = new HttpClient())
                    {
                        try
                        {
                            client.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", _configuracion.GetSection("CoinMarketCapAPI-ApiKey").Value);
                            client.DefaultRequestHeaders.Add("Accepts", "application/json");

                            var queryString = HttpUtility.ParseQueryString(string.Empty);
                            queryString["start"] = "1";
                            queryString["limit"] = "500";
                            queryString["convert"] = "USD";

                            apiUrl.Query = queryString.ToString();

                            HttpResponseMessage response = await client.GetAsync(apiUrl.ToString());

                            if (response.IsSuccessStatusCode)
                            {
                                // Read and display the content as a string 
                                string content = await response.Content.ReadAsStringAsync();
                                Console.WriteLine($"Response Content: {content}");
                                var dataDeserialized = JsonConvert.DeserializeObject<ResponseDTO>(content);
                                return new GetCoinQueryResponse()
                                {
                                    CryptoCurrencies = dataDeserialized.data
                                };
                            }
                            else
                            {
                                Console.WriteLine($"Failed with status code: {response.StatusCode}");
                            }
                        }
                        catch (HttpRequestException ex)
                        {
                            Console.WriteLine($"Request error: {ex.Message}");
                            success = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR Getting Coins {ex.Message}\n StackTrace: {ex.StackTrace}");
                    success = false;
                }
                return new GetCoinQueryResponse();
            }
        }
    }
}
