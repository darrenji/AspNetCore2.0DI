using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            MultipleImplementation();
        }

        private static void MultipleImplementation()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddTransient<IHasValue, MyClassWithValue>();
            services.AddTransient<IHasValue, MyClassWithValue2>();
            var serviceProvider = services.BuildServiceProvider();

            //从容器从拿到所有服务
            var myServices = serviceProvider.GetServices<IHasValue>().ToList();
            var myService = serviceProvider.GetService<IHasValue>();

            Console.WriteLine("来看看所有的服务");
            foreach(var service in myServices)
            {
                Console.WriteLine(service.Value);
            }

            Console.WriteLine("来看默认的服务");
            Console.WriteLine(myService.Value);
        }

    }

    public interface IHasValue
    {
        object Value { get; set; }

    }

    public class MyClassWithValue : IHasValue
    {
        public object Value { get; set; }

        public MyClassWithValue()
        {
            Value = 42;
        }
    }

    public class MyClassWithValue2 : IHasValue
    {
        public object Value { get; set; }

        public MyClassWithValue2()
        {
            Value = 43;
        }
    }

}
