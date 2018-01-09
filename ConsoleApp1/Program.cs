using Microsoft.Extensions.DependencyInjection;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo();
        }

        private static void Demo() 
        {
            IServiceCollection services = new ServiceCollection();
            services.AddTransient<IMyService, MyService>();
            services.AddTransient(provider => new MyUtility(provider.GetService<IMyService>()));
            var serviceProvider = services.BuildServiceProvider();
            var instance = serviceProvider.GetService<MyUtility>();
            instance.DoSth();
        }

    }

    public interface IMyService
    {
        void Do();
    }

    public class MyService : IMyService
    {
        public void Do()
        {
            Console.WriteLine("i am doing sth");
        }
    }

    public class MyUtility
    {
        private readonly IMyService _myService;

        public MyUtility(IMyService myService)
        {
            this._myService = myService;
        }

        public void DoSth()
        {
            _myService.Do();
        }
    }

}
