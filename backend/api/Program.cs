using api.Services;
using System;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddScoped<PgExecutor>(sp =>
	new(builder.Environment.IsDevelopment()
		? builder.Configuration["ConnectionStrings:LocalhostConnection"]!
		: builder.Configuration["ConnectionStrings:DatabaseConnection"]!,
		builder.Environment.IsDevelopment()
		? builder.Configuration["MasterConnectionStrings:LocalhostConnection"]!
		: builder.Configuration["MasterConnectionStrings:DatabaseConnection"]!)
);


builder.Services.AddControllers();
var app = builder.Build();



app.MapControllers();
app.Run();
