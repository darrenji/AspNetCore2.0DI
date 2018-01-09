﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
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
            services.TryAddTransient<IHasValue, MyClassWithValue2>();
            var serviceProvider = services.BuildServiceProvider();
            var myServices = serviceProvider.GetServices<IHasValue>().ToList();

            Console.WriteLine("来看所有的服务");
            foreach(var service in myServices)
            {
                Console.WriteLine(service.Value);
            }
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
