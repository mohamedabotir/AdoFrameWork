using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdoFrameWork.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AdoFrameWork.Core
{
    public static class RegisterService
    {
        private static IServiceProvider _serviceProvider;

        public static void RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<InputReader, InputReaderImpl>();
            _serviceProvider = services.BuildServiceProvider(true);
        }
        public static void DisposeServices()
        {
            if (_serviceProvider == null)
            {
                return;
            }
            if (_serviceProvider is IDisposable)
            {
                ((IDisposable)_serviceProvider).Dispose();
            }
        }
    }
}