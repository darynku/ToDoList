using ToDoList;
using ToDoList.Services;
using ToDoList.Services.Abstract;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
});
builder.Services.AddControllers();
builder.Services.AddScoped<INoteService, NoteService>();

builder.Services.AddScoped<ApplicationDbContext>(sp =>
    new ApplicationDbContext(builder.Configuration.GetConnectionString("Default")!));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Notes API v1");
    });
}
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
