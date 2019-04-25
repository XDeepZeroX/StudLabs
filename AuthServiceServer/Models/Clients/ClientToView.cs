using IdentityServer4.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServiceServer.Models.Clients
{
    public class ClientToView
    {

        [Display(Name = "Id клиента")]
        public string ClientId;

        [Display(Name = "Название клиента")]
        public string ClientName;

        //public string IpAddress = "";
        //public new List<ClientScope> AllowedScopes;
        public ClientToView(Client client)
        {
            //ClientId = client.ClientId;
            ClientId = client.ClientId;
            ClientName = client.ClientName;
            //ClientSecrets = client.ClientSecrets;
            //AllowedScopes =  client.AllowedScopes;

        }
    }

}
