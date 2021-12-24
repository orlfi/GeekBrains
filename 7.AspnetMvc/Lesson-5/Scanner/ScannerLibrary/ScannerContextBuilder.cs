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
        private IScanSaver? _saver;
        private ILogger? _logger;

        private ScannerContextBuilder(IScannerDevice device) => _device = device;

        public static ScannerContextBuilder Create(IScannerDevice device)
        {
            return new ScannerContextBuilder(device);
        }

        public ScannerContextBuilder WithLogger(ILogger logger)
        {
            _logger = logger;
            return this;
        }

        public ScannerContextBuilder WithSaver(IScanSaver saver)
        {
            _saver = saver;
            return this;
        }

        public IScannerContext Build()
        {
            var context = new ScannerContext(_device, _logger);

            if (_saver is not null)
                context.ConfigureProcessor(_saver);

            return context;
        }
    }
}