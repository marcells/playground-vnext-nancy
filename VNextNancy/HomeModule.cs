using System;
using Nancy;
using Nancy.ModelBinding;
using System.Threading.Tasks;
using static System.Console;

namespace VNextNancy
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Before += ctx =>
            {
                WriteLine($"Process Route: {ctx.ResolvedRoute.Description.Path}");
                return null;
            };
            
            After += ctx =>
            {
                WriteLine($"Finished Route: {ctx.ResolvedRoute.Description.Path}");
            };
            
            OnError += (ctx, ex) =>
            {
                WriteLine($"Error in Route: {ctx.ResolvedRoute.Description.Path}");
                return null;
            };

            Get["/"] = _ => View["Index"];

            Get["/hi"] = _ => "Hello World";

            Get["/hi/{name}"] = parameters => $"Hello {parameters.name}!";

            Get["/products/{id:int}"] = parameters => $"Product with id {parameters.id}!";

            Get["/products/between/{left:int}/and/{right:int}"] = parameters => $"Products between {parameters.left} and {parameters.right}!";

            Get["/async", runAsync: true] = async (_, token) =>
            {
                await Task.Delay(3000);
                return "Hello World delayed";
            };
            
            Get["/error"] = _ => { throw new Exception("Ooops, something went wrong!"); };
            
            Post["/some/model"] = _ => 
            {
                var myModel = this.Bind<MyModel>();
              
                var tags = string.Join(",", myModel.Tags);
                var ints = string.Join(", ", myModel.Ints);
                
                WriteLine($"RememberMe: {myModel.RememberMe}");
                WriteLine($"Ints: {ints}");
                WriteLine($"Tags: {tags}");
                
                return View["Index", myModel];
            };
        }
    }
    
    public class MyModel
    {
        public bool RememberMe { get; set; }
        public string[] Tags { get; set; }
        public int[] Ints { get; set; }
    }
}
