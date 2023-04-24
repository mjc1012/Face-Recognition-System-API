using Face_Recognition_System_Data;
using Face_Recognition_System_Data.Contracts;
using Face_Recognition_System_Data.Repositories;
using Face_Recognition_System_Domain;
using Face_Recognition_System_Domain.Contracts;
using Face_Recognition_System_Domain.Handlers;
using Face_Recognition_System_Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using static Face_Recognition_System_Data.Constants;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(MappingProfiles));
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IFaceExpressionRepository, FaceExpressionRepository>();
builder.Services.AddScoped<IFaceToTrainRepository, FaceToTrainRepository>();
builder.Services.AddScoped<IAugmentedFaceRepository, AugmentedFaceRepository>();
builder.Services.AddScoped<IFaceToRecognizeRepository, FaceToRecognizeRepository>();
builder.Services.AddScoped<IFaceRecognitionStatusRepository, FaceRecognitionStatusRepository>();
builder.Services.AddScoped<IAugmentedFaceService, AugmentedFaceService>();
builder.Services.AddScoped<IFaceExpressionService, FaceExpressionService>();
builder.Services.AddScoped<IFaceRecognitionService, FaceRecognitionService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IImageAugmentationService, ImageAugmentationService>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IFaceToRecognizeService, FaceToRecognizeService>();
builder.Services.AddScoped<IFaceRecognitionStatusService, FaceRecognitionStatusService>();
builder.Services.AddScoped<IFaceToTrainService, FaceToTrainService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPersonHandler, PersonHandler>();
builder.Services.AddScoped<IFaceToTrainHandler, FaceToTrainHandler>();
builder.Services.AddScoped<IFaceToRecognizeHandler, FaceToRecognizeHandler>();
builder.Services.AddScoped<IFaceRecognitionStatusHandler, FaceRecognitionStatusHandler>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Face Recognition System API"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider( PathConstants.FacesToRecognizePath),
    RequestPath = "/FacesToRecognize"
});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(PathConstants.FaceDatasetPath),
    RequestPath = "/FaceDataset"
});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(PathConstants.FaceExpressionPath),
    RequestPath = "/FaceExpression"
});

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(PathConstants.AugmentedFacesPath),
    RequestPath = "/AugmentedFaces"
});

app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
