using System.Reflection;
using Domain.Commands;
using Domain.Commands.Contexts;
using Infrastructure.Commands;
using Infrastructure.TypedFactory;

namespace ConsoleApp
{
    using App;
    using Autofac;
    using Domain.Entities;
    using Domain.Entities.Deposits;
    using Domain.Repositories;
    using Domain.Services;
    using System;
    using System.Linq;
    using Infrastructure.Queries;
    public class Program
    {
        public static void Main(string[] args)
        {
            var containerBuilder = new ContainerBuilder();

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

            containerBuilder.RegisterType<App>();

            IContainer container = containerBuilder.Build();



            App app = container.Resolve<App>();

            Employee myEmployee = app.CreateEmployee("Nya", "Nyan", "Nyanyan");
            RentPoint rp=app.AddRentPoint("Компрос",myEmployee);

            Employee otherEmployee = app.CreateEmployee("otherNya", "otherNyan", "otherNyanyan");
            app.AddRentPoint("Ленина",otherEmployee);

            Client client = app.CreateClient("Keke", "Ke", "Kekekeke");
            Client clientWhoWantTakeReservedBike = app.CreateClient("aaa", "a", "aaaaa");

            Deposit deposit = new MoneyDeposit(5000);

            app.AddBike("Кама", 50, 4500, rp);
            app.AddBike("Кама", 100,4500, rp);

            //Bike iChooseThisBike = app.GetBikes().FirstOrDefault(x => x.Name == "Кама");

            //app.ReserveBike(client, iChooseThisBike, DateTime.UtcNow.AddDays(1));

            //app.GetBikeInRent(client, iChooseThisBike, deposit);

            //bool iBrokeBike = true;
            //app.GetBikeInRent(client, iChooseThisBike, deposit);
            //app.ReturnBike(iChooseThisBike, otherRentPoint, iBrokeBike);
            container.Dispose();
        }
    }
}
