using Microsoft.AspNet.Builder;
using Nancy;
using Nancy.Owin;
using Microsoft.Framework.Runtime;

namespace VNextNancy
{
    public class Startup
    {
        internal static IApplicationEnvironment Environment { get; private set; }

        public Startup(IApplicationEnvironment env)
        {
            Environment = env;
        }
        
        public void Configure(IApplicationBuilder app)
        {
            app.UseOwin(x => x.UseNancy());
        }
    }
    
    public class vNextRootPathProvider : IRootPathProvider
    {
        private string BasePath = Startup.Environment.ApplicationBasePath;
    
        public string GetRootPath()
        {
            return BasePath;
        }
    }
    
    public class CustomBootstrapper : DefaultNancyBootstrapper
    {
        protected override IRootPathProvider RootPathProvider
        {
            get { return new vNextRootPathProvider(); }
        }
    }
}
