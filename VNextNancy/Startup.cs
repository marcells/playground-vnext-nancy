using Microsoft.AspNet.Builder;
using System.Collections.Generic;
using System;
using Nancy;
using Nancy.Owin;
using Nancy.ViewEngines.Razor;
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
            
        protected override IEnumerable<Type> ViewEngines
        {
            get 
            { 
                return new[] 
                { 
                    typeof(Nancy.ViewEngines.SuperSimpleViewEngine.SuperSimpleViewEngineWrapper),
                    typeof(Nancy.ViewEngines.Razor.RazorViewEngine)
                };
            }
        }
    }
    
    public class RazorConfig : IRazorConfiguration
    {
        public IEnumerable<string> GetAssemblyNames()
        {
            yield return "VNextNancy";
        }
    
        public IEnumerable<string> GetDefaultNamespaces()
        {
            yield return "VNextNancy";
        }
    
        public bool AutoIncludeModelNamespace
        {
            get { return true; }
        }
    }
}
