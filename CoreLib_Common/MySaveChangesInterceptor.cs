using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CoreLib_Common
{
    public class MySaveChangesInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(
            DbContextEventData eventData,
            InterceptionResult<int> result)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Saving changes for {eventData.Context.Database.GetConnectionString()}");
            Console.ForegroundColor = ConsoleColor.White;
            return result;
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = new CancellationToken())
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Saving changes for {eventData.Context.Database.GetConnectionString()}");
            Console.ForegroundColor = ConsoleColor.White;
            return new ValueTask<InterceptionResult<int>>(result);
        }
    }
}