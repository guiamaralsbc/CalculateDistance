using GAB.DistanceCalculation.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;

namespace GAB.DistanceCalculation.API.Controllers
{
    [Route("api/[controller]")]
    public class CalculateController : Controller
    {
        Calculate calc;

        [HttpGet("[action]/{user}/{password}")]
        public string Login(string user, string password)
        {
            Authenticate auth = new Authenticate();
            if (auth.Login(user, password))
            {
                return TokenManager.CreateToken(user);
            }
            else
            {
                throw new Exception("Error 401 - Unauthorized");
            }
        }

        [HttpGet("[action]/{friendName}")]
        public string ListCloser(string friendName)
        {
            try
            {
                string list = String.Empty;
                string token = Request.Headers["Token"].ToString();

                if (TokenManager.ValidateToken(token))
                {
                    calc = new Calculate();
                    var friends = calc.ListCloser(friendName);
                    if (friends != null)
                    {
                        list = JsonConvert.SerializeObject(friends);
                    }
                    else
                    {
                        throw new Exception("Friend Not Found");
                    }
                }

                return list;
            }
            catch (Exception)
            {
                throw new Exception("Error 401 - Unauthorized");
            }
        }
        
        private string GetSettings(string settingName)
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile($"appsettings.json");

            var config = builder.Build();

            return config.GetSection(settingName).Value;
        }
    }
}
