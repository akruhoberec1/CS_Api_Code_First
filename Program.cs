using API_CSharp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

async Task<List<VehicleMake>> GetAllMakes(DataContext context)  =>
    await context.VehicleMakes.ToListAsync();   

app.MapGet("/vmakes", async (DataContext context) =>
    await context.VehicleMakes.ToListAsync());

app.MapGet("/vmakes/{id}", async (DataContext context, int id) =>
    await context.VehicleMakes.FindAsync(id) is VehicleMake make ? 
    Results.Ok(make) :
    Results.NotFound("Sorry, no make found."));

app.MapPost("/vmakes", async (DataContext context, VehicleMake make) =>
{
    context.VehicleMakes.Add(make);
    await context.SaveChangesAsync();
    return Results.Ok(await GetAllMakes(context));
});

app.MapPut("/vmakes/{id}", async (DataContext context, VehicleMake make, int id) =>
{
    var dbMake = await context.VehicleMakes.FindAsync(id);
    if (dbMake == null) return Results.NotFound("No make found");

    dbMake.Name = make.Name;
    dbMake.Abrv = make.Abrv;
    await context.SaveChangesAsync();

    return Results.Ok(await GetAllMakes(context));
});

app.MapDelete("/vmakes/{id}", async (DataContext context, int id) =>
{
    var dbMake = await context.VehicleMakes.FindAsync(id);
    if (dbMake == null) return Results.NotFound("Koja je to marka");

    context.VehicleMakes.Remove(dbMake);
    await context.SaveChangesAsync();

        return Results.Ok(await GetAllMakes(context));
});

app.Run();
