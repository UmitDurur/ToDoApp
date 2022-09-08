using Microsoft.EntityFrameworkCore;
using Volo.Abp.DependencyInjection;

namespace ToDoFrontEnd.Data;

public class ToDoFrontEndEFCoreDbSchemaMigrator : ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public ToDoFrontEndEFCoreDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the ToDoFrontEndDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<ToDoFrontEndDbContext>()
            .Database
            .MigrateAsync();
    }
}
