using System;
using System.Collections.Generic;
using System.Text;

namespace FileReaderService.Application.SettingConfigModel
{
    public class JobConfiguration
    {
        public Guid JobId { get; set; }
        public Type JobType { get; }
        public string JobName { get; }
        public string CronExpression { get; }

       // https://www.freeformatter.com/cron-expression-generator-quartz.html
        public JobConfiguration(Guid jobId, Type jobType, string jobName, string cronExpression)
        {
            JobId = jobId;
            JobType = jobType;
            JobName = jobName;
            CronExpression = cronExpression;
        }
    }
}
