using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quiz_Application.Services.Repository;


namespace Quiz_Application.Services
{
   public static class ServiceCollectionExtension
    {        
       public static IServiceCollection AddServices(this IServiceCollection services)
       {
          return services.AddScoped<ICandidate<Entities.Candidate>, Candidate<Entities.Candidate>>();
       }
    }
}
