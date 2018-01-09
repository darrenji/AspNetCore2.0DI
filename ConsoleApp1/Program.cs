using Microsoft.Extensions.DependencyInjection;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();

            //Transient:service每次被请求就创建一个实例
            //Scoped:为每个请求创建一个service实例
            //Singleton:所有的请求用一个service实例
            services.AddTransient<MyService>();
            var serviceProvider = services.BuildServiceProvider();

            //这里的IServiceProvider看做是DI容器
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
