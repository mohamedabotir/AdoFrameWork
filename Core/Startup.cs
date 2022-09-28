using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        public async void BuildScreen()
        {

            var provider = GetProvider();

            var input = provider.GetRequiredService<InputReader>();
            var result = input.StartScreen();
            await OperateSelected(result);
        }
        private IServiceProvider GetProvider()
        {
            var serviceScope = _serviceProvider.CreateScope();
            var provider = serviceScope.ServiceProvider;
            return provider;
        }
        public async Task OperateSelected(int option)
        {

            switch (option)
            {
                case 0:
                    await SetConnection();
                    break;
                case 1:
                    await SubmitQuery();
                    break;
                case 2:
                    await InsertQuery();
                    break;
                default:
                    break;
            }
            BuildScreen();
        }

        private async Task SetConnection()
        {
            Console.WriteLine("=======QueryString=====");
            var input = Console.ReadLine();
            var result = await GetProvider().GetRequiredService<Connection>().BuildConnection(input);
            if (result)
                Console.WriteLine("Connected");
        }

        private async Task SubmitQuery()
        {
            var sql = Console.ReadLine();
            var result = await GetProvider().GetRequiredService<Connection>().SubmitQueryScaler(sql);

        }
        private async Task InsertQuery()
        {
            await GetProvider().GetRequiredService<Connection>().Insert();
        }
    }
}