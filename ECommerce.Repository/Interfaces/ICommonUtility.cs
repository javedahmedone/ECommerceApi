using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Repository.Interfaces
{
    public interface ICommonUtility
    {
        Task<string?> UserRoleById(int roleId);
    }
}
