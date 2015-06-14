using Xunit;
using Nancy;
using Nancy.Testing;

namespace VNextNancy.Tests
{
    public class HomeModuleTests
    {
        [Fact]
        public void HiRouteWorks()
        {
            var browser = new Browser(with => with.Module(new HomeModule()));
            
            var result = browser.Get("/hi", with => with.HttpRequest());
            
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal("Hello World", result.Body.AsString());            
        }
    }
}
