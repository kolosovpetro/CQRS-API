﻿using System;
using CqrsApi.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CqrsApi.Core;

public static class DatabaseMigrator
{
    public static void MigrateDatabase(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
            .CreateScope();

        using var context = serviceScope.ServiceProvider.GetService<MoviesContext>();

        if (context == null)
        {
            throw new InvalidOperationException("Database context is NULL at Migrator service.");
        }

        try
        {
            context.Database.Migrate();
            LoggerToFile.PrintToLog("Migration successful.");
        }
        catch (Exception e)
        {
            LoggerToFile.PrintExceptionToFile(e);
            Console.WriteLine(e);
            throw;
        }
    }
}