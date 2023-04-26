using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace LetsGoPark.WebSite
{
    //Program is the main class is the main class for the wbesite.
    public class Program
    {
        //This is the main driver for the program.
        //It intakes a string list of arguments and uses them to run the application.
        public static void Main(string[] args)
        {
            //This line creates a new host and starts it. It runs the application and listens for incoming requests.
            CreateHostBuilder(args).Build().Run();
        }

        //This method returns an instance of the IHostBuilder interface, which is used to configure and build a new host.
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                //This function uses Startup.cs to Defaults for the web host. 
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //Uses the Startup class defined in Startup.cs to declare start up preferences.
                    webBuilder.UseStartup<Startup>();
                });
    }
}
