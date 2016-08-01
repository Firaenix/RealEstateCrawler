using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RealEstateCrawler.Service.Crawlers;
using RealEstateCrawler.Service.Interfaces;

namespace RealEstateCrawler
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddTransient<ICrawler, DomainCrawler>();
        }

        public void Configure (
            IApplicationBuilder app,
            ILoggerFactory loggerFactory )
        {
            loggerFactory.WithFilter(new FilterLoggerSettings
                {
                    { "Microsoft", LogLevel.Warning },
                    { "System", LogLevel.Warning }
                })
                .AddConsole();

            // add Trace Source logging
            var testSwitch = new SourceSwitch("sourceSwitch", "Logging Sample");
            testSwitch.Level = SourceLevels.Warning;
            loggerFactory.AddTraceSource(testSwitch, new TextWriterTraceListener(writer: Console.Out));
        }
    }
}