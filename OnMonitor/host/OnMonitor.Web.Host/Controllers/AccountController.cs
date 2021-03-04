using Volo.Abp.AspNetCore.Mvc.Authentication;
using Volo.Abp.Caching;

namespace OnMonitor.Controllers
{
    public class AccountController : ChallengeAccountController
    {


        IDistributedCache<string> cache;

        public AccountController(IDistributedCache<string> cache)
        { 
        
        
        
        
        }
    }
}
