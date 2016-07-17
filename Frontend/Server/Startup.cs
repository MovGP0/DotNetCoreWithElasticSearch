using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace Frontend
{
    public sealed class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), @"..\wwwroot");
            var fileProvider = new PhysicalFileProvider(path);

            var defaultFileOptions = new DefaultFilesOptions
            {
                FileProvider = fileProvider
            };
            defaultFileOptions.DefaultFileNames.Clear();
            defaultFileOptions.DefaultFileNames.Add("index.html");
            app.UseDefaultFiles(defaultFileOptions);
            
            var staticFileOptions = new StaticFileOptions 
            {
                FileProvider =  fileProvider
            };
            app.UseStaticFiles(staticFileOptions);
        }
    }
}