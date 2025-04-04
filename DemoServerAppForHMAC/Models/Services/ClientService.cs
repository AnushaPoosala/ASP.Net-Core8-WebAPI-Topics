using DemoServerAppForHMAC.Context;
using Microsoft.EntityFrameworkCore;

namespace DemoServerAppForHMAC.Models.Services
{
    public class ClientService
    {
        private readonly ServerAppDbContext _context;
        public ClientService(ServerAppDbContext context) 
        {
            _context = context;
        }
        
        public async Task<string> GetClientSecrectKeyInfoAsync(string clientId)
        {
            var client = _context.ClientInfos
            .AsNoTracking()
            .FirstOrDefault(client => client.ClientId == clientId);

            return client?.ClientSecretKey;
        }
    }
}
