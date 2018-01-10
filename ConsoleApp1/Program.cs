using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Linq;

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
            var configuration = new ConfigurationBuilder()
                 .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"))
                 .Build();

            IServiceCollection services = new ServiceCollection();
            services.AddOptions();
            services.AddScoped<MyTaxCalculator>();
            services.Configure<MyTaxCalculatorOptions>(configuration.GetSection("TaxOptions"));
            var serivceProvider = services.BuildServiceProvider();
            var calculator = serivceProvider.GetRequiredService<MyTaxCalculator>();
            Console.WriteLine(calculator.Calculate(100));
            Console.WriteLine(configuration["TaxOptions:TaxRatio"]);
        }
    }

    public class MyTaxCalculator
    {
        private readonly MyTaxCalculatorOptions _options;

        //条件1：IOptions<T>的出现，是告诉我们T的赋值再DI容器中赋值
        public MyTaxCalculator(IOptions<MyTaxCalculatorOptions> options)
        {
            _options = options.Value;
        }

        public int Calculate(int amount)
        {
            return amount * _options.TaxRatio / 100;
        }
    }

    public class MyTaxCalculatorOptions
    {
        public int TaxRatio { get; set; }

        public MyTaxCalculatorOptions()
        {
            TaxRatio = 118;
        }
    }

}
