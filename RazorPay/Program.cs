using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DrakeUI.Framework;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;

namespace RazorPay
{
    internal static class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            var host = CreateHostBuilder().Build();
            var services = host.Services;

          //  Application.Run(services.GetRequiredService<MainForm>());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(services.GetRequiredService<Form1>());
            Logger.Info("Application started.");
            // Application.Run(new Form1());
        }
        static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<BarcodeService>();
                    services.AddSingleton<ExcelService>();
                    services.AddSingleton<WordDocumentService>();
                    services.AddSingleton<Form1>();
                });
        }
    }
}
