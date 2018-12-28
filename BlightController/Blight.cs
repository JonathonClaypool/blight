using System;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Extensions;

namespace BlightController
{
    public class Blight
    {

        private RestResponseCookie authCookie;
        private readonly string username;
        private readonly string password;

        private readonly RestClient client;
        

        public Blight(string username, string password)
        {
            this.username = username;
            this.password = password;
            
            this.client = new RestClient("https://blight.ironhelmet.com/");
        }
        
        private void login(string username, string password)
        {
            
            IRestRequest request = new RestRequest("arequest/login",Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("undefined", $"type=login" +
                                              $"&alias={username}" +
                                              $"&password={password}", 
                ParameterType.RequestBody
                );
            IRestResponse response = client.Execute(request);

            if (!response.IsSuccessful)
            {
                Console.WriteLine($"failure to {nameof(login)}! with error {response.ErrorMessage} {response.Content} {response.StatusCode}");
            }
            
            foreach (RestResponseCookie restResponseCookie in response.Cookies) // 
            {
                if (restResponseCookie.Name == "auth")
                {
                    authCookie = restResponseCookie;    
                }
            }

            
        }

        public dynamic togglePause(string gameNumber) // returns a gamestate object - I'm too lazy to parse at the moment.
        {
            if (authCookie == null || authCookie.Expired)
            {
                login(this.username, this.password);
            }
            IRestRequest request = new RestRequest("grequest/order",Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("undefined", $"type=order" +
                                              $"&order=toggle_pause_game&age=238840" +
                                              $"&game_number={gameNumber}&build_number=1052", 
                ParameterType.RequestBody
                );
            request.AddCookie(authCookie.Name, authCookie.Value);
            IRestResponse response = client.Execute(request);

            return response.Content;
        }
        
        
        
    }
}