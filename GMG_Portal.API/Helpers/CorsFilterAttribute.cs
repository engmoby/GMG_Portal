using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Cors;
using System.Web.Http.Cors;

namespace GMG_Portal.API.Helpers
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    class CorsFilterAttribute : Attribute, ICorsPolicyProvider
    {
        public CorsFilterAttribute()
        {

        }       

        Task<CorsPolicy> ICorsPolicyProvider.GetCorsPolicyAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var originsVal = System.Configuration.ConfigurationManager.AppSettings.Get("CorsOrigins") ?? string.Empty;
            var methodsVal = System.Configuration.ConfigurationManager.AppSettings.Get("CorsMethods") ?? string.Empty;
            var headersVal = System.Configuration.ConfigurationManager.AppSettings.Get("CorsHeaders") ?? string.Empty;
            var policy = new CorsPolicy
            {
                AllowAnyMethod = methodsVal == "*",
                AllowAnyHeader = headersVal == "*",
                AllowAnyOrigin = originsVal == "*"
            };
            object controller = string.Empty;
            request.GetRouteData().Values.TryGetValue("controller", out controller);
            var origins = (originsVal).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var methods = (methodsVal).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var headers = (headersVal).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var orgn in origins)
            {
                policy.Origins.Add(orgn);
            }
            foreach (var hd in headers)
            {
                policy.ExposedHeaders.Add(hd);
            }
            policy.ExposedHeaders.Add("X-MiniProfiler-Ids");
            return Task.FromResult(policy);
        }
    }
    public class CorsPolicyFactory : ICorsPolicyProviderFactory
    {
        ICorsPolicyProvider _provider = new CorsFilterAttribute();

        public ICorsPolicyProvider GetCorsPolicyProvider(HttpRequestMessage request)
        {

            return _provider;
        }
    }
}
