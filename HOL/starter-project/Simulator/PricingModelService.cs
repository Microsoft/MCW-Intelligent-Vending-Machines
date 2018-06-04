using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using IO.Swagger.Api;
using IO.Swagger.Client;
using IO.Swagger.Model;

namespace Simulator
{
    public class PricingModelService
    {
        static string _apiBasePath = System.Configuration.ConfigurationManager.AppSettings["rServiceBaseAddress"];
        static string _apiUsername = System.Configuration.ConfigurationManager.AppSettings["rServiceUsername"];
        static string _apiPassword = System.Configuration.ConfigurationManager.AppSettings["rServicePassword"];
        Dictionary<string, string> _authHeader = new Dictionary<string, string>();

        static Configuration config = new Configuration(new ApiClient(_apiBasePath));
        ApiPredictPurchasePriceApi api = new ApiPredictPurchasePriceApi(config);
        UserApi apiAuth = new UserApi(config);

        public async Task<double> GetSuggestedPrice(int age, string gender, string productSelected)
        {
            return InvokeRequestResponseService(age, gender, productSelected);
        }

        double InvokeRequestResponseService(int age, string gender, string productSelected)
        {
            double suggestedPrice = -1;

            var loginRequest = new LoginRequest(_apiUsername, _apiPassword);
            var loginResponse = apiAuth.Login(loginRequest);

            if (!string.IsNullOrEmpty(loginResponse.AccessToken))
            {
                _authHeader.Clear();
                _authHeader.Add("Authorization", $"{loginResponse.TokenType} {loginResponse.AccessToken}");
                config.DefaultHeader = _authHeader;
            }

            var webServiceParameters = new InputParameters(age, gender, productSelected);
            var response = api.ApiPredictPurchasePrice(webServiceParameters);

            Console.WriteLine(response.OutputParameters.PurchasePrice);

            if (response.Success.HasValue && response.Success.Value)
            {
                if (response.OutputParameters.PurchasePrice != null)
                {
                    suggestedPrice = (double)response.OutputParameters.PurchasePrice;
                    Console.WriteLine("Result: ", response.OutputParameters.PurchasePrice);
                }
                else
                {
                    Console.WriteLine($"The request failed with the following error: {response.ErrorMessage}");
                }
            }

            return suggestedPrice;
        }
    }
}