using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdoFrameWork.Abstract.Services;
using AdoFrameWork.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AdoFrameWork.Core
{
    public class Startup
    {
        public static IServiceProvider _serviceProvider;
        public Startup(IServiceProvider services)
        {
            _serviceProvider = services;
        }
        public void BuildScreen()
        {

            var provider = GetProvider();

            var input = provider.GetRequiredService<InputReader>();
            var result = input.StartScreen();
            OperateSelected(result);
        }
        private IServiceProvider GetProvider()
        {
            var serviceScope = _serviceProvider.CreateScope();
            var provider = serviceScope.ServiceProvider;
            return provider;
        }
        public void OperateSelected(int option)
        {

            switch (option)
            {
                case 0:
                    SetConnection();
                    break;
                case 1:
                    break;
                default:
                    break;
            }
        }

        private async void SetConnection()
        {
            Console.WriteLine("=======QueryString=====");
            var input = Console.ReadLine();
            var result = await GetProvider().GetRequiredService<Connection>().BuildConnection(input);
            if (result)
                Console.WriteLine("Connected");
        }
    }
}