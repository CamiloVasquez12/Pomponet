using Microsoft.EntityFrameworkCore;
using Pomponet.Context;
using Pomponet.Repositories;
using Pomponet.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var conString = builder.Configuration.GetConnectionString("connection");
builder.Services.AddDbContext<CropsDbContext>(options => options.UseSqlServer(conString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPeopleService, PeopleService>();
builder.Services.AddScoped<IPeopleRepository, PeopleRepository>();

builder.Services.AddScoped<ICropsService, CropsService>();
builder.Services.AddScoped<ICropsRepository, CropsRepository>();

builder.Services.AddScoped<IAchievementsService, AchievementsService>();
builder.Services.AddScoped<IAchievementsRepository, AchievementsRepository>();

builder.Services.AddScoped<IAplicationToolsService, AplicationToolsService>();
builder.Services.AddScoped<IAplicationToolsRepository, AplicationToolsRepository>();

builder.Services.AddScoped<IEppsService, EppsService>();
builder.Services.AddScoped<IEppsRepository, EppsRepository>();

builder.Services.AddScoped<IFungicide_X_Pompon_PartService, Fungicide_X_Pompon_PartService>();
builder.Services.AddScoped<IFungicide_X_Pompon_PartRepository, Fungicide_X_Pompon_PartRepository>();

builder.Services.AddScoped<IFungicidesService, FungicidesService>();
builder.Services.AddScoped<IFungicidesRepository, FungicidesRepository>();

builder.Services.AddScoped<IInventoriesService, InventoriesService>();
builder.Services.AddScoped<IInventoriesRepository, InventoriesRepository>();

builder.Services.AddScoped<IMoneyService, MoneyService>();
builder.Services.AddScoped<IMoneyRepository, MoneyRepository>();

builder.Services.AddScoped<IPest_X_FungicideService, Pest_X_FungicideService>();
builder.Services.AddScoped<IPest_X_FungicideRepository, Pest_X_FungicideRepository>();

builder.Services.AddScoped<IPestsService, PestsService>();
builder.Services.AddScoped<IPestsRepository, PestsRepository>();

builder.Services.AddScoped<IPlayer_AchievementsService, Player_AchievementsService>();
builder.Services.AddScoped<IPlayer_AchievementsRepository, Player_AchievementsRepository>();

builder.Services.AddScoped<IPlayersService, PlayersService>();
builder.Services.AddScoped<IPlayersRepository, PlayersRepository>();

builder.Services.AddScoped<IPompon_PartsService, Pompon_PartsService>();
builder.Services.AddScoped<IPompo_PartsnRepository, Pompon_PartsRepository>();

builder.Services.AddScoped<ISensorsService, SensorsService>();
builder.Services.AddScoped<ISensorsRepository, SensorsRepository>();

builder.Services.AddScoped<ITypes_FungicidesService, Types_FungicidesService>();
builder.Services.AddScoped<ITypes_FungicidesRepository, Types_FungicidesRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();    
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
