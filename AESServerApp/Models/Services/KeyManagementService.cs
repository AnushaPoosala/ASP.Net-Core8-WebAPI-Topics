using AESServerApp.DBContextInfo;
using Microsoft.EntityFrameworkCore;

namespace AESServerApp.Models.Services
{
    public class KeyManagementService
    {

        private readonly ApplicationDbContext _context;

        public KeyManagementService(ApplicationDbContext context)
        {
             _context = context;
        }


        public async Task<ClientKeyIV?> GetClientKeyIVAsync(string clientId)
        {
              return await _context.ClientkeyIVs.FirstOrDefaultAsync(x => x.ClientId.ToUpper() == clientId.ToUpper());
        }
    }
}
