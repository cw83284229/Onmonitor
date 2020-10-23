using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Users;


namespace OnMonitor.Controllers
{

    [Route("MyAccount")]
    public class MyAccountController : AbpController
    {
        private readonly UserManager<IUser> _userManager;

        public ICurrentUser _currentUser;

        public MyAccountController(ICurrentUser currentUser)
        {
            _currentUser = currentUser;
        
        }
        public string Gettest()
        {

         var data=  _currentUser.Id;

        
            
          return data.ToString();
        }
    }
}
