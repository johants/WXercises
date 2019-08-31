using System;

namespace WXercises.Helpers
{
    interface IApiEndpointConfiguration
    {
        Uri BaseAddress { get; }

        string Token { get; }

        int ExceptionAllowedBeforeBreaking { get; }

        double DurationOfBreakInMinutes { get; }

        TimeSpan Timeout { get; }
    }
}
