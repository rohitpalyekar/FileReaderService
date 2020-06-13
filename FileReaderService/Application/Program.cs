using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileReaderService.Application;
using FileReaderService.Application.SettingConfigModel;
using FileReaderService.Job;
using FileReaderService.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace FileReaderService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseWindowsService()
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole(options => options.IncludeScopes = true);
                logging.AddDebug();
            }).ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<IJobFactory, CustomJobFactory>();
                    services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
                    services.AddSingleton<FileExtractionJob>();
                    services.AddSingleton(jobConfigurations());

                    services.AddTransient<IFileDataExtraction, FileDataExtraction>();
                    services.AddHostedService<CustomHostedService>();
                });

        private static List<JobConfiguration> jobConfigurations()
        {
            var jobList = new List<JobConfiguration>();

            jobList.Add(new JobConfiguration(Guid.NewGuid(), typeof(FileExtractionJob), "FileExtraction Job", "0 0/1 * * * ?"));

            return jobList;


        }
    }

}
