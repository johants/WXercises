using System;
using Microsoft.Extensions.Configuration;

namespace WXercises.Helpers
{
    public class ApiEndpointConfiguration : IApiEndpointConfiguration
    {
        private readonly string _configPrefix;
        private readonly string _keySeparator;
        private readonly IConfiguration _configuration;

        public ApiEndpointConfiguration(string configPrefix, IConfiguration configuration)
        {
            this._configPrefix = configPrefix;
            this._configuration = configuration;
            this._keySeparator = ":";
        }

        public Uri BaseAddress => new Uri(
            new Uri(
                this._configuration[
            $"{this._configPrefix}{this._keySeparator}hostAddress"]), this._configuration[
            $"{this._configPrefix}{this._keySeparator}basePath"]);


        public string Token =>
            this._configuration[
                $"{this._configPrefix}{this._keySeparator}token"];

        public int ExceptionAllowedBeforeBreaking =>
            int.Parse(_configuration[$"{_configPrefix}{this._keySeparator}{Constants.CircuitBreaker.ExceptionAllowedBeforeBreaking}"]);

        public double DurationOfBreakInMinutes => double.Parse(_configuration[$"{_configPrefix}{this._keySeparator}{Constants.CircuitBreaker.DurationOfBreakInMinutes}"]);

        public TimeSpan Timeout
        {
            get
            {
                double result;
                if (!double.TryParse(this._configuration[
                    $"{this._configPrefix}{this._keySeparator}timeoutSeconds"], out result))
                    return TimeSpan.FromSeconds(3);
                return TimeSpan.FromSeconds(result);
            }
        }
    }
}
