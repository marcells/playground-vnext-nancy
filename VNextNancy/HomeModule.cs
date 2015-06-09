using Nancy;
using System.Threading.Tasks;

namespace VNextNancy
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ => View["Index"];
            
            Get["/hi"] = _ => "Hello World";
            
            Get["/hi/{name}"] = parameters => $"Hello {parameters.name}!";
            
            Get["/products/{id:int}"] = parameters => $"Product with id {parameters.id}!";
            
            Get["/products/between/{left:int}/and/{right:int}"] = parameters => $"Products between {parameters.left} and {parameters.right}!";
            
            Get["/async", runAsync: true] = async (_, token) => {
                await Task.Delay(3000);
                return "Hello World delayed";
            };
        }
    }
}
