using Microsoft.Extensions.DependencyInjection;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            services.AddTransient<MyService>();
            var serviceProvider = services.BuildServiceProvider();
            var myService = serviceProvider.GetService<MyService>();
            myService.DoSth();
        }
    }

    public class MyService
    {
        public void DoSth()
        {
            Console.WriteLine("doing sth...");
        }
    }
}
