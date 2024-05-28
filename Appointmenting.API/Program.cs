using Appointmenting.API.Application.RepositoryContracts;
using Appointmenting.API.Application.ServiceContracts;
using Appointmenting.API.Domain.Entities;
using Appointmenting.API.Infrastructure.Database;
using Appointmenting.API.Infrastructure.Extensions;
using Appointmenting.API.Infrastructure.Repositories;
using Appointmenting.API.Infrastructure.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication().AddCookie(IdentityConstants.ApplicationScheme);

builder.Services.AddIdentityCore<User>()
    .AddEntityFrameworkStores<AppUserDbContext>()
    .AddApiEndpoints();

builder.Services.AddDbContext<AppUserDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Users")));
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Appointments")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//  ***** TimeSlot Validators
builder.Services.AddScoped<CreateTimeSlotValidator>();
builder.Services.AddScoped<UpdateTimeslotValidator>();
builder.Services.AddScoped<DeleteTimeslotByIdValidator>();
builder.Services.AddScoped<DeleteTimeslotsByDateValidator>();
builder.Services.AddScoped<DeleteTimeslotsBeforeDateValidator>();

//  ***** Appointment Validators

//  ***** Common Validators
builder.Services.AddScoped<DateTimeValidator>();
builder.Services.AddScoped<AppointmentDtoValidator>();
builder.Services.AddScoped<AppointmentConfirmationDtoValidator>();

builder.Services.AddScoped<ITimeslotRepo, TimeSlotRepository>();
builder.Services.AddScoped<IAppointmentRepo, AppointmentRepository>();
builder.Services.AddScoped<IEmployeeRepo, EmployeeRepository>();
builder.Services.AddScoped<IUserRepo, UserRepository>();

builder.Services.AddMediatR(config => {
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    //app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.MapIdentityApi<User>();

app.UseAuthorization();

app.MapControllers();

app.Run();
