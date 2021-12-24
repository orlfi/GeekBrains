using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScannerLibrary;
using ScannerLibrary.Interfaces;

namespace ScannerLibrary
{
    public class ScannerContextBuilder
    {
        private readonly IScannerDevice _device;
        IPicturePipeline? _imageProcessorPipeline;
        private readonly string _outputDirectory;

        private ScannerContextBuilder(IScannerDevice device, string outputDirectory) => (_device, _outputDirectory) = (device, outputDirectory);

        public static ScannerContextBuilder Create(IScannerDevice device, string outputDirectory)
        {
            return new ScannerContextBuilder(device, outputDirectory);
        }

        public ScannerContextBuilder WithImageProcessorPipeline(IPicturePipeline imageProcessorPipeline)
        {
            _imageProcessorPipeline = imageProcessorPipeline;
            return this;
        }

        public IScannerContext Build()
        {
            var context = new ScannerContext(_device, _outputDirectory);

            if (_imageProcessorPipeline is not null)
                context.ConfigureImageProcessorPipeline(_imageProcessorPipeline);

            return context;
        }
    }
}