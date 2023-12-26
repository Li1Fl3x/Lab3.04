using Lab3;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = AuthOptions.ISSUER,
        ValidateAudience = true,
        ValidAudience = AuthOptions.AUDIENCE,
        ValidateLifetime = true,
        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
        ValidateIssuerSigningKey = true
    };
});

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
IServiceCollection serviceCollection = builder.Services.AddDbContext<ModelDB>(options => options.UseSqlServer(connection));
var app = builder.Build();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapPost("/login",async(User loginData,ModelDB db) =>
{
    User? person = await db.Users!.FirstOrDefaultAsync(p => p.EMail == loginData.EMail &&
p.Password == loginData.Password);
    if (person is null) return Results.Unauthorized();
    var claims = new List<Claim> { new Claim(ClaimTypes.Email, person.EMail!) };
    var jwt = new JwtSecurityToken(issuer: AuthOptions.ISSUER,
        audience: AuthOptions.AUDIENCE,
        claims: claims,
        expires: DateTime.Now.Add(TimeSpan.FromMinutes(2)),
        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
        );
    var encoderJWT = new JwtSecurityTokenHandler().WriteToken(jwt);
    var response = new
    {
        access_token = encoderJWT,
        username = person.EMail
    };
    return Results.Json(response);
});

app.MapGet("/api/PriceLists", [Authorize] async (ModelDB db) => await db.PriceLists!.ToListAsync());
app.MapGet("/api/PriceList/{id:int}", [Authorize] async (ModelDB db, int id) => await db.PriceLists!.Where(g => g.Id == id).FirstOrDefaultAsync());
app.MapGet("/api/Registrations", [Authorize] async (ModelDB db) => await db.Registrations!.ToListAsync());
app.MapGet("/api/PriceList/{name}", [Authorize] async (ModelDB db,string name) => await db.PriceLists!.Where(u=>u.NameBroadcast==name).FirstOrDefaultAsync());
app.MapPost("/api/PriceList", [Authorize] async (PriceList PriceList, ModelDB db) =>
{
    await db.PriceLists!.AddAsync(PriceList);
    await db.SaveChangesAsync();
    return PriceList;
});
app.MapPost("/api/Registration", [Authorize] async (Registration Registration, ModelDB db) =>
{
    await db.Registrations!.AddAsync(Registration);
    await db.SaveChangesAsync();
    return Registration;
});
app.MapDelete("/api/PriceList/{id:int}", [Authorize] async (int id, ModelDB db) =>
{
    PriceList? PriceList = await db.PriceLists!.FirstOrDefaultAsync(u => u.Id == id);
    if (PriceList == null) return Results.NotFound(new { message = "Прайс лист не найден" });
    db.PriceLists!.Remove(PriceList);
    await db.SaveChangesAsync();
    return Results.Json(PriceList);
});
app.MapDelete("/api/Registration/{id:int}", [Authorize] async (int id, ModelDB db) =>
{
    Registration? Registration = await db.Registrations!.FirstOrDefaultAsync(u => u.Id == id);
    if (Registration == null) return Results.NotFound(new { message = "Регистрация не найдена" });
    db.Registrations!.Remove(Registration);
    await db.SaveChangesAsync();
    return Results.Json(Registration);
});
app.MapPut("/api/PriceList", [Authorize] async (PriceList PriceList, ModelDB db) =>
{
    PriceList? g = await db.PriceLists!.FirstOrDefaultAsync(u => u.Id == PriceList.Id);
    if (g == null) return Results.NotFound(new { message = "Прайс лист не найден" });
    g.CodeBroadcast = PriceList.CodeBroadcast;
    g.NameBroadcast = PriceList.NameBroadcast;
    g.PricePerMinute = PriceList.PricePerMinute;
    await db.SaveChangesAsync();
    return Results.Json(g);
});
app.MapPut("/api/Registration", [Authorize] async (Registration Registration, ModelDB db) =>
{
    Registration? st = await db.Registrations!.FirstOrDefaultAsync(u => u.Id == Registration.Id);
    if (st == null) return Results.NotFound(new { message = "Прайс лист не найден" });
    st.DateBroadcast = Registration.DateBroadcast;
    st.CodeBroadcast = Registration.CodeBroadcast;   
    st.NameBroadcast = Registration.NameBroadcast;
    st.Regularity = Registration.Regularity;
    st.TimeOnBroadcast = Registration.TimeOnBroadcast;
    st.CostBroadcast = Registration.CostBroadcast;
    st.PriceListId = Registration.PriceListId;
    await db.SaveChangesAsync();
    return Results.Json(st);
});
app.Run();
