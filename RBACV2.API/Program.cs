using Microsoft.OpenApi.Models;
using RBACV2.API.Settings;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);


var setup = new AppSetup(builder.Configuration);

setup.Configure(builder.Environment);

setup.RegisterServices(builder.Services);

var app = builder.Build();

setup.SetupMiddlewares(app);

if (app.Environment.IsDevelopment() && Debugger.IsAttached)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseSwagger(c =>
    {
        c.RouteTemplate = "swagger/{documentName}/swagger.json";
        c.PreSerializeFilters.Add((swaggerDoc, httpReq) => swaggerDoc.Servers = new List<OpenApiServer>
         {
                new OpenApiServer { Url = "https://unit-aks-dev.eastus.cloudapp.azure.com/dev/backend/" },
                new OpenApiServer { Url = "https://unit-aks-dev.eastus.cloudapp.azure.com/qa/backend/" }

            });
    });
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

