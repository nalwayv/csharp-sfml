using Serilog;
using System;

namespace csharp_sfml
{
    public class Logger
    {   
        // --- LOGGER
        // https://github.com/serilog/serilog-sinks-file
        Serilog.Core.Logger logger;

        private static readonly Lazy<Logger> instance = new Lazy<Logger>(() => new Logger());
        public static Logger Instance { get => instance.Value; }

        private Logger()
        {
            logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("Log.txt", fileSizeLimitBytes: 10000000)
                .CreateLogger();
        }

        public Serilog.Core.Logger Log { get => logger; }
    }
}
