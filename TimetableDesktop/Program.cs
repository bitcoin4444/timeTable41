using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;
using TimetableDesktop.BusinesLogic;
using TimetableDesktop.Models.Interfaces;
using TimetableDesktop.Services;

namespace TimetableDesktop
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var services = new ServiceCollection();

            ConfigureServices(services);
            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                var form1 = serviceProvider.GetRequiredService<Form1>();
                Application.Run(form1);
            }
        }
        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddScoped<Form1>();
            services.AddScoped<ITimetableService, TimetableService>();
            services.AddScoped<IBusinessLogic, BusinesLogicClass>();
        }
    }
}
