using System;
using System.Collections.Generic;
using System.Linq;
using AiApplication.Application.Abstractions.AI;
using AiApplication.Domain.AI;

namespace AiApplication.Infrastructure.AI
{
    public sealed class AiClientProvider : IAiClientProvider
        {


            private readonly
                IEnumerable<IAiClient> _clients;



            public AiClientProvider(
                IEnumerable<IAiClient> clients)
            {

                _clients = clients;

            }



            public IAiClient GetClient(
                AiProvider provider)
            {


                var client =
                    _clients.FirstOrDefault(
                        x => x.Provider == provider);



                if(client is null)
                {
                    throw new Exception(
                        $"未找到AI Provider:{provider}");
                }



                return client;

            }

        } 
}
