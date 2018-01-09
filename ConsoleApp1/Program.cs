using Microsoft.Extensions.DependencyInjection;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo2();
        }

        private static void Demo()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddTransient<TransientDateOperation>();
            services.AddScoped<ScopedDateOperation>();
            services.AddSingleton<SingletonDateOperation>();
            var serviceProvider = services.BuildServiceProvider();

            Console.WriteLine();
            Console.WriteLine("-----first request----");
            Console.WriteLine();
            var transientService = serviceProvider.GetService<TransientDateOperation>();
            var scopedServce = serviceProvider.GetService<ScopedDateOperation>();
            var singletonService = serviceProvider.GetService<SingletonDateOperation>();

            Console.WriteLine();
            Console.WriteLine("-----second request----");
            Console.WriteLine();
            var transientService2 = serviceProvider.GetService<TransientDateOperation>();
            var scopedService2 = serviceProvider.GetService<ScopedDateOperation>();
            var singletonService2 = serviceProvider.GetService<SingletonDateOperation>();

            Console.WriteLine();
            Console.WriteLine("---------");
            Console.WriteLine();

        }

        private static void Demo2()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddTransient<TransientDateOperation>();
            services.AddScoped<ScopedDateOperation>();
            services.AddSingleton<SingletonDateOperation>();
            var serviceProvider = services.BuildServiceProvider();

            Console.WriteLine();
            Console.WriteLine("-----first request----");
            Console.WriteLine();
            using(var scope = serviceProvider.CreateScope())
            {
                var transientService = scope.ServiceProvider.GetService<TransientDateOperation>();
                var scopedService = scope.ServiceProvider.GetService<ScopedDateOperation>();
                var singletonService = scope.ServiceProvider.GetService<SingletonDateOperation>();
            }

            Console.WriteLine();
            Console.WriteLine("-----second request----");
            Console.WriteLine();
            using (var scope = serviceProvider.CreateScope())
            {
                var transientService = scope.ServiceProvider.GetService<TransientDateOperation>();
                var scopedService = scope.ServiceProvider.GetService<ScopedDateOperation>();
                var singletonService = scope.ServiceProvider.GetService<SingletonDateOperation>();
            }

            Console.WriteLine();
            Console.WriteLine("---------");
            Console.WriteLine();
        }
    }

    public class MyService
    {
        public void DoSth()
        {
            Console.WriteLine("doing sth...");
        }
    }

    public class TransientDateOperation
    {
        public TransientDateOperation()
        {
            Console.WriteLine("Transient service is created");
        }
    }

    public class ScopedDateOperation
    {
        public ScopedDateOperation()
        {
            Console.WriteLine("Scoped service is created");
        }
    }

    public class SingletonDateOperation
    {
        public SingletonDateOperation()
        {
            Console.WriteLine("Singleton service is created");
        }
    }

   
}
