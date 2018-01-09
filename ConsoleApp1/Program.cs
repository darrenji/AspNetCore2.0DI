using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;
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
            IServiceCollection services = new ServiceCollection();

            //条件2： 在容器中要允许IOptions的登场
            services.AddOptions();
            services.AddTransient<MyTaxCalculator>();

            //条件3：使用Configure方法动态设置实例
            services.Configure<MyTaxCalculatorOptions>(options =>
            {
                options.TaxRatio = 135;
            });
            var seriviceProvider = services.BuildServiceProvider();
            var calculator = seriviceProvider.GetService<MyTaxCalculator>();
            Console.WriteLine(calculator.Calculate(100));
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
