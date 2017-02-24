using System;
using System.Reflection;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Autofac.Extensions.DependencyInjection;
using Domain.Commands;
using Domain.Repositories;
using Domain.Services;
using Infrastructure.Commands;
using Infrastructure.Queries;
using Infrastructure.TypedFactory;
using Microsoft.AspNetCore.Routing;

namespace WebApp
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }
            Configuration = builder.Build();
        }
        public IContainer ApplicationContainer { get; private set; }

        public IConfigurationRoot Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddMvc();
            // Create the container builder.
            var containerBuilder = new ContainerBuilder();

            // Register dependencies, populate the services from
            // the collection, and build the container. If you want
            // to dispose of the container at the end of the app,
            // be sure to keep a reference to it as a property or field.
            containerBuilder
                .RegisterGeneric(typeof(Repository<>))
                .As(typeof(IRepository<>))
                .SingleInstance();

            containerBuilder
                .RegisterType<BikeNameVerifier>()
                .As<IBikeNameVerifier>();

            containerBuilder
                .RegisterType<BikeService>()
                .As<IBikeService>();

            containerBuilder
                .RegisterType<RentPointService>()
                .As<IRentPointService>();

            containerBuilder
                .RegisterType<EmployeeService>()
                .As<IEmployeeService>();

            containerBuilder
                .RegisterType<RentService>()
                .As<IRentService>();

            containerBuilder
                .RegisterType<DepositCalculator>()
                .As<IDepositCalculator>();

            containerBuilder
                .RegisterType<RentSumCalculate>()
                .As<IRentSumCalculate>();
            containerBuilder.RegisterTypedFactory<ICommandFactory>();
            containerBuilder.RegisterTypedFactory<IQueryFactory>();
            containerBuilder.RegisterTypedFactory<IQueryBuilder>();
            containerBuilder
                .RegisterGeneric(typeof(QueryFor<>))
                .As(typeof(IQueryFor<>));

            containerBuilder
                .RegisterAssemblyTypes(typeof(GetAllRentPointsQuery).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IQuery<,>));

            containerBuilder
                .RegisterType<CommandBuilder>()
                .As<ICommandBuilder>();
            containerBuilder
                .RegisterAssemblyTypes(typeof(AddBikeCommand).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(ICommand<>));
            containerBuilder.Populate(services);
            this.ApplicationContainer = containerBuilder.Build();

            // Create the IServiceProvider based on the container.
            return new AutofacServiceProvider(this.ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute("ParameterRequest", "{controller}/{action}/{name}");
                routes.MapRoute(
                name: "List",
                template: "{controller}s",
                defaults: new { action = "List" });
                routes.MapRoute("route", "{controller}/{action}");
                routes.MapRoute(
                name: "View",
                template: "{controller}/{name}",
                defaults: new { action = "View" });
                
                routes.MapRoute(
                name: "areaRoute",
                template: "{controller=Home}/{action=Index}");
            });
        }
    }
}
