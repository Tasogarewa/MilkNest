using Castle.Components.DictionaryAdapter;
using Castle.Core.Configuration;
using Microsoft.Extensions.Configuration;
using MilkNest.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;


namespace MilkNest.Persistence.Services
{
    public class SmsService : ISmsService
    {
        private readonly IConfiguration _configuration;
        private readonly TwilioRestClient _client;

        public SmsService(IConfiguration configuration)
        {
            
            _configuration = configuration;
            TwilioClient.Init(_configuration["Twilio:AccountSID"], _configuration["Twilio:AuthToken"]);
            _client = new TwilioRestClient(_configuration["Twilio:AccountSID"], _configuration["Twilio:AuthToken"]);
        }

        public async Task SendSmsAsync(string number, string message)
        {
         
            var messageOptions = new CreateMessageOptions(new PhoneNumber(number))
            {
                From = new PhoneNumber(_configuration["Twilio:PhoneNumber"]),
                Body = message
            };

            await MessageResource.CreateAsync(messageOptions);
        }
    }
}
