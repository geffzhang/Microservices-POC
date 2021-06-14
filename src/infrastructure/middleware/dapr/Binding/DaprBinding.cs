﻿using Binding.Abstractions;
using Dapr.Client;
using System;
using System.Threading.Tasks;

namespace Binding
{
    public class DaprBinding : IBinding
    {
        private readonly string DAPR_NOTIFICATION_BINDING_NAME = "disx-poc-notification-binding";
        private readonly string DAPR_KINESIS_BINDING_NAME = "disx-kinesis-binding";
        // private readonly string DAPR_KINESIS2_BINDING_NAME = "disx-kinesis2-binding";
        private readonly string DAPR_EMAIL_BINDING_NAME = "disx-poc-email-binding";
        private readonly string DAPR_BINDING_OPERATION = "create";
        public async Task SendNotificationAsync<Notification>(Notification notification)
        {
            using var client = new DaprClientBuilder().Build();
            await client.InvokeBindingAsync<Notification>(DAPR_NOTIFICATION_BINDING_NAME, DAPR_BINDING_OPERATION, notification);
            //var secret = await client.GetSecretAsync("awssecretmanager", "disx-db-secret");
            //var secretValue = secret["disx-db-secret"];
        }

        public async Task SendMailAsync<Mail>(Mail mail)
        {
            using var client = new DaprClientBuilder().Build();
            await client.InvokeBindingAsync<Mail>(DAPR_EMAIL_BINDING_NAME, DAPR_BINDING_OPERATION, mail);
        }

        public async Task CreateBindingAsync<BindingEvent>(BindingEvent bindingEvent)
        {
            try
            {
                using var client = new DaprClientBuilder().Build();
                await client.InvokeBindingAsync<BindingEvent>(DAPR_KINESIS_BINDING_NAME, DAPR_BINDING_OPERATION, bindingEvent);
                // await client.InvokeBindingAsync<BindingEvent>(DAPR_KINESIS2_BINDING_NAME, DAPR_BINDING_OPERATION, bindingEvent);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
