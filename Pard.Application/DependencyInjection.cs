﻿using System.Reflection;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Pard.Application.Auth;
using Pard.Application.Common.Behaviors;
using Pard.Application.Common.Interfaces;
using Pard.Application.Repositories.Records;
using Pard.Application.Services;

namespace Pard.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            services.AddSingleton<IJwtFactory, JwtFactory>();
            services.AddTransient<IRecordsRepository, SqlServerRecordsRepository>();
            services.AddTransient<ILocationsRepository, SqlServerLocationsRepository>();
            services.AddTransient<IRecordsService, RecordsService>();
            services.AddTransient<ILocationsService, LocationsService>();
            services.AddTransient<IArchiveService, ArchiveService>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            return services;
        }
    }
}