﻿using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace Testovoe.Application
{
    public static class MediatrRegistration
    {
        public static IServiceCollection AddMediatRServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg
            .RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            return services;
        }
    }
}
