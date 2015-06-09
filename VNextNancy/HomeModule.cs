using Nancy;
using System.Threading.Tasks;

namespace VNextNancy
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ => "Hello World";
            
            Get["/hi/{name}"] = parameters => $"Hello {parameters.name}!";
            
            Get["/async", runAsync: true] = async (_, token) => {
                await Task.Delay(5000);
                return "Hello World delayed";
            };
        }
    }
}
