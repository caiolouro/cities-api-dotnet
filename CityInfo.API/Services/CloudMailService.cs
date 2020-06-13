using System;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CityInfo.API.Services
{
    public class CloudMailService : IMailService
    {
        private readonly ILogger<CloudMailService> _logger;
        private readonly IConfiguration _configuration;

        public CloudMailService(ILogger<CloudMailService> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public void Send(string subject, string message)
        {
            var debugMsg = $"Sending mail from {_configuration["mailSettings:mailFrom"]} to {_configuration["mailSettings:mailTo"]} using CloudMailService...";
            _logger.LogInformation(debugMsg);
            Debug.WriteLine(debugMsg);
            Debug.WriteLine($"Subject: {subject}");
            Debug.WriteLine($"Message: {message}");
        }
    }
}
