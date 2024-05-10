
using Application.Common.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Security.Claims;
using System.Text;


namespace Infrastructure.Services
{
    public class HubService
    {
        private readonly ICurrentUserService _user;
    
        public  HubConnection _HubConnection => CreateHubConnection();


        public HubService(ICurrentUserService user )
        {
            _user = user;
        
        }

        public  HubConnection CreateHubConnection()
        {
        
          var  accessToken = _user.Token;
            var endpoint = "https://localhost:7211/SignalrHub?access_token=" + accessToken.Result;

            var connection =     new HubConnectionBuilder()
                            .WithUrl(endpoint).WithAutomaticReconnect()
                            .Build();

          
                connection.StartAsync().Wait();
            
          




            return connection;

    
        }
       
    }
}
