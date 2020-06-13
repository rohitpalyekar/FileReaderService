using FileReaderService.Application.SettingConfigModel;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileReaderService.Application
{
    public class CustomHostedService : IHostedService
    {
        private readonly ISchedulerFactory schedulerFactory;
        private readonly IJobFactory jobFactory;
        private readonly List<JobConfiguration> jobList;
        public CustomHostedService(ISchedulerFactory
            schedulerFactory,
            List<JobConfiguration> jobList,
            IJobFactory jobFactory)
        {
            this.schedulerFactory = schedulerFactory;
            this.jobList = jobList;
            this.jobFactory = jobFactory;
        }
        public IScheduler Scheduler { get; set; }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            foreach (JobConfiguration configuration in jobList)
            {
                Scheduler = await schedulerFactory.GetScheduler();
                Scheduler.JobFactory = jobFactory;
                var job = CreateJob(configuration);
                var trigger = CreateTrigger(configuration);
                await Scheduler.ScheduleJob(job, trigger, cancellationToken);
                await Scheduler.Start(cancellationToken);
            }
        }
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Scheduler?.Shutdown(cancellationToken);
        }
        private ITrigger CreateTrigger(JobConfiguration jobConfiguration)
        {
            return TriggerBuilder.Create()
            .WithIdentity(jobConfiguration.JobId.ToString())
            .WithCronSchedule(jobConfiguration.CronExpression)
            .WithDescription($"{jobConfiguration.JobName}")
            .Build();
        }
        private IJobDetail CreateJob(JobConfiguration jobConfiguration)
        {
            return JobBuilder
            .Create(jobConfiguration.JobType)
            .WithIdentity(jobConfiguration.JobId.ToString())
            .WithDescription($"{jobConfiguration.JobName}")
            .Build();
        }
    }
}
