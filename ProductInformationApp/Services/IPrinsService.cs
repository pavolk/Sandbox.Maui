using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductInformationApp.Services
{
    public interface IPrinsService
    {
        Task<Prins.Product> GetByIdAsync(int id);
    }
}
