using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.ExceptionHandlers
{
    public class AppBadRequestException : Exception
    {
        public AppBadRequestException(string message)
            : base(message)
        {
        }
    }

}
