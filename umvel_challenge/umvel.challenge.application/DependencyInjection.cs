using System;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using umvel.challenge.domain;

namespace umvel.challenge.application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddMediatR(Assembly.GetExecutingAssembly());

			return services;
		}

		public static IServiceCollection AddDomainDependencies(this IServiceCollection services)
		{
			services.AddAggregates();
			services.AddRules();

			return services;
		}
	}
}

