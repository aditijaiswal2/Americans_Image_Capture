using DinkToPdf;
using DinkToPdf.Contracts;
using Americans_Image_Capture.Server.Services;
using Microsoft.EntityFrameworkCore;
using Americans_Image_Capture.Server.Contracts;
using Americans_Image_Capture.Server.Models;


namespace Americans_Image_Capture.Server.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<OutlookEmailService>();

            services.AddTransient<IDocumentService, DocumentService>();

           
            return services;
        }
    }
}