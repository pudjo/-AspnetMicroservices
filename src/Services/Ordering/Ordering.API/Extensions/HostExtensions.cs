using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Ordering.Infrastructure.Persistence;
using SendGrid.Helpers.Mail;
using System.Reflection.Emit;

namespace Ordering.API.Extensions;

public static class HostExtensions
{
    public static WebApplication MigrateDatabase(this WebApplication webApp, int? retry = 0)
    {
        int retryForAvailability = retry.Value;
        using (var scope = webApp.Services.CreateScope())
        {
            using (var appContext = scope.ServiceProvider.GetRequiredService<OrderContext>())
            {
                try
                {
                    appContext.Database.Migrate();
                    
                }
                catch (Exception ex)
                {

                    if (retryForAvailability < 50)
                    {
                        retryForAvailability++;
                        System.Threading.Thread.Sleep(2000);
                        MigrateDatabase(webApp,  retryForAvailability);
                    }
                    throw;
                }
            }
        }
        return webApp;
    }
    

}
