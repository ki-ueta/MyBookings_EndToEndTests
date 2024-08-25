using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using Microsoft.Testing.Platform.Configurations;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyBooking_EndToEndTests.Helpers
{
    static class CheckEmailHelper
    {
        // psuedo code
        public static async Task<Message> CheckUnreadEmailsWithSubjectAsync(string subject, IConfiguration configuration)
        {
            var credentials = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets { ClientId = configuration["EmailService:ClientId"], ClientSecret = configuration["EmailService:ClientSecret"] },
                new[] { GmailService.Scope.GmailReadonly },
                "Gmail API",
                CancellationToken.None
            );

            var service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credentials
            });

            // Filter for unread emails in the inbox
            // POtential improvement: use api to return email that match the subject only.
            var messages = await service.Users.Messages.List("me")
            .Q("is:unread label:inbox") 
            .ExecuteAsync();

            foreach (var message in messages.Messages)
            {
                var messageDetails = await service.Users.Messages.Get("me", message.Id).ExecuteAsync();


                if (messageDetails.Subject.Contains(subject))
                {
                    return messageDetails;
                }
            }

            return null;
        }
    }
}
