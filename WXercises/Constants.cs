namespace WXercises
{
    public class Constants
    {
        public static class CircuitBreaker
        {
            public const string ExceptionAllowedBeforeBreaking = "circuitBreaker:exceptionAllowedBeforeBreaking";
            public const string DurationOfBreakInMinutes = "circuitBreaker:durationOfBreakInMinutes";
        }

        public static class ProxyPrefix
        {
            public const string ApiResourceProxy = "api.resource.proxy";
        }
    }
}
