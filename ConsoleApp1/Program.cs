using Microsoft.Extensions.DependencyInjection;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
          
        }

        private static void Demo()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddTransient(typeof(IMyGeneric<Special>), typeof(MyGeneric<Special>));
            var serviceProvider = services.BuildServiceProvider();
            var service = serviceProvider.GetService<IMyGeneric<Special>>();
        }

    }

    public interface IMyGeneric<T> where T : class
    {

    }

    public class MyGeneric<T> : IMyGeneric<T> where T : class
    {

    }

    public class Special
    {

    }

}
