using RealTimeStockExchange.DAL.IRepositories;
using RealTimeStockExchange.DAL.Repositories;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews()
    .AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper();
builder.Services.AddSignalR();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddRepositories();
builder.Services.AddBusinessServcies();


// Use Cros
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Map JWTSetting to JWT in appSetting File
builder.Services.Configure<JWTSetting>(builder.Configuration.GetSection("JWT"));

// Add JWT Configuraion Services
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

})
    .AddJwtBearer(o =>
    {
        o.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var accessToken = context.Request.Query["access_token"];
                // If the request is for our hub...
                PathString path = context.HttpContext.Request.Path;

                if (!string.IsNullOrEmpty(accessToken) &&
                   (path.StartsWithSegments("/signalr")))
                {
                    // Read the token out of the query string
                    context.Token = accessToken;
                }

                return Task.CompletedTask;
            }
        };
        o.RequireHttpsMetadata = false;
        o.SaveToken = false;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
            ClockSkew = TimeSpan.Zero
        };

    });

builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseWebSockets();

if (app.Environment.IsDevelopment())
{
    Task.Run(() =>
    {
        using (var handler = new HttpClientHandler() { Credentials = CredentialCache.DefaultCredentials })
        {
            using (HttpClient httpClient = new HttpClient(handler))
            {
                string SourceDocumentAbsoluteUrl = builder.Configuration["SwaggerToTypeScriptClientGeneratorSettings:SourceDocumentAbsoluteUrl"];
                string OutputDocumentRelativePath = builder.Configuration["SwaggerToTypeScriptClientGeneratorSettings:OutputDocumentRelativePath"];
                using (Stream contentStream = httpClient.GetStreamAsync(SourceDocumentAbsoluteUrl).Result)
                using (StreamReader streamReader = new StreamReader(contentStream))
                {
                    string json = streamReader.ReadToEnd();
                    //NSwag.OpenApiDocument document = NSwag.OpenApiDocument.FromJsonAsync(json).Result;
                    //NSwag.CodeGeneration.TypeScript.TypeScriptClientGeneratorSettings settings = new NSwag.CodeGeneration.TypeScript.TypeScriptClientGeneratorSettings
                    var document = OpenApiDocument.FromJsonAsync(json).Result;
                    var settings = new NSwag.CodeGeneration.TypeScript.TypeScriptClientGeneratorSettings()
                    {
                        OperationNameGenerator = new SwaggerClientOperationNameGenerator(),
                        ClassName = "SwaggerClient",
                        Template = TypeScriptTemplate.Angular,
                        RxJsVersion = 7.0M,
                        HttpClass = HttpClass.HttpClient,
                        InjectionTokenType = InjectionTokenType.InjectionToken,
                        BaseUrlTokenName = "API_BASE_URL",
                        WrapDtoExceptions = true,
                        TypeScriptGeneratorSettings = { TypeScriptVersion = 4.3M }
                    };

                    //TypeScriptClientGenerator generator = new TypeScriptClientGenerator(document, settings);
                    var generator = new NSwag.CodeGeneration.TypeScript.TypeScriptClientGenerator(document, settings);
                    string code = generator.GenerateFile();
                    new FileInfo(OutputDocumentRelativePath).Directory.Create();
                    try
                    {
                        File.WriteAllText(OutputDocumentRelativePath, code);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }
    });
}

app.UseSession();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("corsapp");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.UseEndpoints(endpoint =>
{
    endpoint.MapHub<SignalRHub>("/signalr");
});

app.Run();
