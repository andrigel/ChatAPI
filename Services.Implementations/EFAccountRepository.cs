using DataLayer;
using DataLayer.Entityes;
using Microsoft.AspNetCore.Identity;
using Services.Interfaces;
using System;
using System.Threading.Tasks;
using ViewModels;

namespace Services.Implementations
{
    public class EFAccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public EFAccountRepository(EFDBContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task Register(RegisterViewModel model)
        {

            await _userManager.CreateAsync(new ApplicationUser { Email = model.Email, UserName = model.UserName }, model.Password);
        }
    }
}
