using Microsoft.EntityFrameworkCore;
using Testovoe.Infrastructure.AppContext;
using MediatR;
using System.Reflection;
using Testovoe.Application.Commands;
using Testovoe.Application.Request;
using Testovoe.Application.Response;
using Testovoe.Application;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        string connection = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

        builder.Services.AddMediatRServices();

        //builder.Services
        //    .AddTransient<IRequestHandler<AddDoctorRequest, AddDoctorResponse>, AddDoctorCommand>();
        //builder.Services
        //    .AddTransient<IRequestHandler<DeleteDoctorRequest, Unit>, DeleteDoctorCommand>();
        //builder.Services
        //    .AddTransient<IRequestHandler<DoctorsListRequest,List<DoctorsListResponse>>, DoctorsListCommand>();
        //builder.Services
        //   .AddTransient<IRequestHandler<DoctorRedactRequest, Unit>, DoctorRedactCommand>();
        //builder.Services
        //   .AddTransient<IRequestHandler<TakeReadactRowRequest, TakeRedactRowResponse>, TakeRedactRowCommand>();

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.MapControllers();

        app.Run();
    }
}
