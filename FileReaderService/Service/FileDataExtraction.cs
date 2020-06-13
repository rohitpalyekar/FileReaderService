using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileReaderService.Service
{
    public class FileDataExtraction : IFileDataExtraction
    {
        private readonly ILogger _logger;
        public FileDataExtraction(ILogger<FileDataExtraction> logger)
        {
            this._logger = logger;
        }

        public FileDataExtraction()
        {
           
        }

        public void checklog()
        {
            _logger.LogInformation("Hello");
        }
    }
}
