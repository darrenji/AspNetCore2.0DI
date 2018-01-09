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
            var instance = new MyInstance { Value = 88 };
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton(instance);

            //在IServiceCollection中每一个service的类型是ServiceDescriptor
            //遍历IServiceColleciton
            //在注册到DI容器之前得到一个对象实例
            foreach(ServiceDescriptor service in services)
            {
                //判断每个ServiceDescriptor类型
                if(service.ServiceType == typeof(MyInstance))
                {
                    //从ServiceDescriptor中拿到正在实现的类
                    var registeredInstance = (MyInstance)service.ImplementationInstance;
                    Console.WriteLine("registered instance: " + registeredInstance.Value);
                }
            }

            var serviceProvider = services.BuildServiceProvider();
            var myInstance = serviceProvider.GetService<MyInstance>();
            Console.WriteLine("registered service by instance registration: " + myInstance.Value);
        }

    }

    public class MyInstance
    {
        public int Value { get; set; }
    }

}
