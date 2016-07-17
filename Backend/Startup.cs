using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Backend
{
    public sealed class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc(SetupRoutes);
        }

        private static void SetupRoutes(IRouteBuilder routes){
            routes.MapRoute(
                name: "location", 
                template: "location/{id}", 
                defaults: new { controller = "Location" });

            routes.MapRoute(
                name: "locations", 
                template: "locations", 
                defaults: new { controller = "Location",  action = "Get"});
        }
    }
}