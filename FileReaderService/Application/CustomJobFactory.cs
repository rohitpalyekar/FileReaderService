using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FileReaderService.Application
{
    public class CustomJobFactory : IJobFactory
    {
        private readonly IServiceProvider serviceProvider;

        public CustomJobFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            var jobDetail = bundle.JobDetail;
            return (IJob)serviceProvider.GetService(jobDetail.JobType);
        }

        public void ReturnJob(IJob job)
        {
           
        }
    }
}
