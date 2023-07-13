using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using umvel.challenge.domain.Aggregates;
using umvel.challenge.domain.Contexts;
using umvel.challenge.domain.Entities.Customers.Rules;
using umvel.challenge.domain.Entities.Products.Rules;
using umvel.challenge.domain.Entities.Sales.Rule;

namespace umvel.challenge.domain
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddAggregates(this IServiceCollection services)
        {
            services.AddScoped<ICustomerAggregate, CustomerAggregate>();
            services.AddScoped<ISaleAggregate, SaleAggregate>();
            services.AddScoped<IProductAggregate, ProductAggregate>();
            return services;
        }

        public static IServiceCollection AddRules(this IServiceCollection services)
        {
            services.AddScoped<ICustomerIdMustExistRule, CustomerIdMustExistRule>();
            services.AddScoped<IProductIdMustExistRule, ProductIdMustExistRule>();
            services.AddScoped<ISaleIdMustExistRule, SaleIdMustExistRule>();
            return services;
        }
    }
}
