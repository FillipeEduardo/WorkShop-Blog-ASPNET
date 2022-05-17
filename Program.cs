using Blog.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().ConfigureApiBehaviorOptions(o => o.SuppressModelStateInvalidFilter = true);
builder.Services.AddDbContext<BlogDataContext>();

var app = builder.Build();

app.MapControllers();

app.Run();


