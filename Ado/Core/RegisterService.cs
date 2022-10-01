using AdoFrameWork.Core.Service;
using AdoFrameWork.Services;
using Microsoft.Extensions.DependencyInjection;
using AdoFrameWork.Abstract.Services;
namespace AdoFrameWork.Core
{
    public static class RegisterService
    {

        public static IServiceCollection RegisterServices(this IServiceCollection service)
        {

            service.AddTransient<InputReader, InputReaderImpl>();
            service.AddSingleton<Connection, ConnectionImpl>();
            return service;
        }

    }
}