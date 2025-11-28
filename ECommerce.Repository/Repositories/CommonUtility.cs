using ECommerce.Repository.Interfaces;
using ECommerce.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Repository.Repositories
{
    internal class CommonUtility : ICommonUtility
    {
        private readonly AppDbContext _ctx;

        public CommonUtility(AppDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<string?> UserRoleById(int roleId)
        {
            return  await _ctx.UserRoles.Where(x => x.Id == roleId).Select(x=> x.Role).FirstOrDefaultAsync();
        }
    }
}
