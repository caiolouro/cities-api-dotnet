using System;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace CityInfo.API.Services
{
    public class LocalMailService : IMailService
    {
        private readonly IConfiguration _configuration;

        public LocalMailService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public void Send(string subject, string message)
        {
            Debug.WriteLine($"Sending mail from {_configuration["mailSettings:mailFrom"]} to {_configuration["mailSettings:mailTo"]} using LocalMailService...");
            Debug.WriteLine($"Subject: {subject}");
            Debug.WriteLine($"Message: {message}");
        }
    }
}
