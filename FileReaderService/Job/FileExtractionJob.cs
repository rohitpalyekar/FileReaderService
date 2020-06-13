using FileReaderService.Helper;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FileReaderService.Job

{
    public class FileExtractionJob : IJob
    {
        private readonly ILogger<FileExtractionJob> logger;

        public FileExtractionJob(ILogger<FileExtractionJob> logger)
        {
            this.logger = logger;
        }

        public  Task Execute(IJobExecutionContext context)
        {

           return Task.CompletedTask;
        }
    }
}
