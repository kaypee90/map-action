using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace mapaction
{
    public class Startup
    {
        public record Greeting(string Message);
        
        [HttpGet("/about")]
        private JsonResult About() => new (new {about = "me"});

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                [HttpGet("/")]
                JsonResult Index() => new (new Greeting("Hello World"));

                endpoints.MapAction((Func<JsonResult>)Index);
                endpoints.MapAction((Func<JsonResult>) About);
            });
        }
    }

    public static class Program
    {
        public static void Main(string[] args) =>
            CreateHostBuilder(args).Build().Run();

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                    webBuilder.UseStartup<Startup>()
                    );
    }
}
