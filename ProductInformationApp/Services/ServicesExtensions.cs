using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductInformationApp.Services
{
    public static class ServicesExtensions 
    { 
        public static void AddPrinsService(this IServiceCollection services)
        {
            services.AddSingleton<IPrinsService, PrinsService>();
        }
    }
}
