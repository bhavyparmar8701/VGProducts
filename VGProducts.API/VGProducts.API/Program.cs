using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NLog;
using NLog.Web;
using System.Text;
using VGProducts.API.Extentions;
using VGProducts.Business.Implementation;
using VGProducts.Business.Interface;
using VGProducts.Entities.DTOs;
using VGProducts.Repository.DataAccess;
using VGProducts.Repository.Implementation;
using VGProducts.Repository.Interface;
using VGProducts.Repository.Mapping;
using VGProducts.Service.Implementation;
using VGProducts.Service.Interface;

var logger = LogManager.Setup().LoadConfigurationFromFile("nlog.config").GetCurrentClassLogger();
var builder = WebApplication.CreateBuilder(args);
try
{

    //logger 
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    //Add Distributed Memory Cache
    builder.Services.AddDistributedMemoryCache();

    //Add Session
    builder.Services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromDays(60);
        options.Cookie.Name = "VGProducts";
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    });
    builder.Services.AddHttpContextAccessor();
    

    // Add services to the container.
    builder.Services.AddControllers();
    // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
    builder.Services.AddOpenApi();

    //Database connection
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

    

    builder.Services.AddSwaggerGen();
    //Swagger
    builder.Services.AddEndpointsApiExplorer();


    builder.Services.AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            Description = "Enter : Bearer {Your JWT Token}\""
        });

        options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
        {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
        });
    });

    builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 6;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();



    // Dependency Injection
    builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
    builder.Services.AddScoped<ICategoryBusiness, CategoryBusiness>();
    builder.Services.AddScoped<ICategoryServices, CategoryServices>();

    builder.Services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
    builder.Services.AddScoped<ISubCategoryBusiness, SubCategoryBusiness>();
    builder.Services.AddScoped<ISubCategoryServices, SubCategoryServices>();

    builder.Services.AddScoped<IProductRepository, ProductRepository>();
    builder.Services.AddScoped<IProductBusiness, ProductBusiness>();
    builder.Services.AddScoped<IProductServices, ProductServices>();

    builder.Services.AddScoped<IFavouritesRepository, FavouritesRepository>();
    builder.Services.AddScoped<IFavouritesBusiness, FavouritesBusiness>();
    builder.Services.AddScoped<IFavouritesServices, FavouritesServices>();

    builder.Services.AddScoped<ICountryRepository, CountryRepository>();
    builder.Services.AddScoped<ICountryBusiness, CountryBusiness>();
    builder.Services.AddScoped<ICountryServices, CountryServices>();

    builder.Services.AddScoped<IAuthRepository, AuthRepository>();
    builder.Services.AddScoped<IAuthBusiness, AuthBusiness>();
    builder.Services.AddScoped<IAuthServices, AuthServices>();

    builder.Services.AddScoped<IJwtRepository, JwtRepository>();
    builder.Services.AddScoped<IJwtBusiness, JwtBusiness>();
    builder.Services.AddScoped<IJwtServices, JwtServices>();

    builder.Services.AddScoped<IEmailRepository, EmailRepository>();
    builder.Services.AddScoped<IEmailBusiness, EmailBusiness>();
    builder.Services.AddScoped<IEmailService, EmailService>();


    builder.Services.AddMemoryCache();
    builder.Services.AddStackExchangeRedisCache(options =>
    {
        options.Configuration = "localhost:6379";
        options.InstanceName = "VGProducts_";
    });
    builder.Services.AddSingleton<DapperContext>();

    builder.Services.AddAutoMapper(typeof(MappingProfile));
    builder.Services.AddScoped<ICategoryRepositoryDapper, CategoryRepositoryDapper>();
    builder.Services.AddScoped<ICategoryBusinessDapper, CategoryBusinessDapper>();
    builder.Services.AddScoped<ICategoryServicesDapper, CategoryServicesDapper>();

    builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();
    builder.Services.AddScoped<ICartItemBusiness, CartItemBusiness>();
    builder.Services.AddScoped<ICartItemServices, CartItemServices>();

    builder.Services.AddScoped<ICountryRepository, CountryRepository>();
    builder.Services.AddScoped<ICountryBusiness, CountryBusiness>();
    builder.Services.AddScoped<ICountryServices, CountryServices>();

    builder.Services.AddScoped<IStateRepository, StateRepository>();
    builder.Services.AddScoped<IStateBusiness, StateBusiness>();
    builder.Services.AddScoped<IStateServices, StateServices>();

    builder.Services.AddScoped<ICityRepository, CityRepository>();
    builder.Services.AddScoped<ICityBusiness, CityBusiness>();
    builder.Services.AddScoped<ICityServices, CityServices>();

    builder.Services.AddScoped<IAddressRepository, AddressRepository>();
    builder.Services.AddScoped<IAddressBusiness, AddressBusiness>();
    builder.Services.AddScoped<IAddressServices, AddressServices>();

    builder.Services.AddScoped<IOrderRepository, OrderRepository>(); 
    builder.Services.AddScoped<IOrderBusiness, OrderBusiness>();
    builder.Services.AddScoped<IOrderServices, OrderServices>();

    builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
    builder.Services.AddScoped<IReviewBusiness, ReviewBusiness>();
    builder.Services.AddScoped<IReviewServices, ReviewServices>();

    builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
    builder.Services.AddScoped<IInvoiceBusiness, InvoiceBusiness>();
    builder.Services.AddScoped<IInvoiceServices, InvoiceServices>();
    builder.Services.Configure<IdentityOptions>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 4;
    });

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = "Bearer";
        options.DefaultChallengeScheme = "Bearer";
    })
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"]))
        };
    });
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy(Permissions.CreateCategory, p => p.RequireClaim("Permission", Permissions.CreateCategory));
        options.AddPolicy(Permissions.UpdateCategory, p => p.RequireClaim("Permission", Permissions.UpdateCategory));
        options.AddPolicy(Permissions.DeleteCategory, p => p.RequireClaim("Permission", Permissions.DeleteCategory));
        options.AddPolicy(Permissions.ViewCategory, p => p.RequireClaim("Permission", Permissions.ViewCategory));

        options.AddPolicy(Permissions.CreateSubCategory, p => p.RequireClaim("Permission", Permissions.CreateSubCategory));
        options.AddPolicy(Permissions.UpdateSubCategory, p => p.RequireClaim("Permission", Permissions.UpdateSubCategory));
        options.AddPolicy(Permissions.DeleteSubCategory, p => p.RequireClaim("Permission", Permissions.DeleteSubCategory));
        options.AddPolicy(Permissions.ViewSubCategory, p => p.RequireClaim("Permission", Permissions.ViewSubCategory));

        options.AddPolicy(Permissions.CreateProduct, p => p.RequireClaim("Permission", Permissions.CreateProduct));
        options.AddPolicy(Permissions.UpdateProduct, p => p.RequireClaim("Permission", Permissions.UpdateProduct));
        options.AddPolicy(Permissions.DeleteProduct, p => p.RequireClaim("Permission", Permissions.DeleteProduct));
        options.AddPolicy(Permissions.ViewProduct, p => p.RequireClaim("Permission", Permissions.ViewProduct));
        options.AddPolicy(Permissions.GetProductById, p => p.RequireClaim("Permission", Permissions.GetProductById));

        options.AddPolicy(Permissions.CreateFavourites, p => p.RequireClaim("Permission", Permissions.CreateFavourites));
        options.AddPolicy(Permissions.DeleteFavourites, p => p.RequireClaim("Permission", Permissions.DeleteFavourites));
        options.AddPolicy(Permissions.ViewFavourites, p => p.RequireClaim("Permission", Permissions.ViewFavourites));

        options.AddPolicy(Permissions.CreateCartItem, p => p.RequireClaim("Permission", Permissions.CreateCartItem));
        options.AddPolicy(Permissions.ViewCartItem, p => p.RequireClaim("Permission", Permissions.ViewCartItem));
        options.AddPolicy(Permissions.DeleteCartItemById, p => p.RequireClaim("Permission", Permissions.DeleteCartItemById));
        options.AddPolicy(Permissions.DeleteAllCartItemAsync, p => p.RequireClaim("Permission", Permissions.DeleteAllCartItemAsync));
        options.AddPolicy(Permissions.AddByIdCartItemAsync, p => p.RequireClaim("Permission", Permissions.AddByIdCartItemAsync));

        options.AddPolicy(Permissions.CreateCountry,p => p.RequireClaim("Permission", Permissions.CreateCountry));
        options.AddPolicy(Permissions.GetCountry,p => p.RequireClaim("Permission", Permissions.GetCountry)); 
        options.AddPolicy(Permissions.GetCountryById,p => p.RequireClaim("Permission", Permissions.GetCountryById));
        options.AddPolicy(Permissions.DeleteCountry,p => p.RequireClaim("Permission", Permissions.DeleteCountry));

        options.AddPolicy(Permissions.CreateState, p => p.RequireClaim("Permission", Permissions.CreateState));
        options.AddPolicy(Permissions.GetState, p => p.RequireClaim("Permission", Permissions.GetState));
        options.AddPolicy(Permissions.GetStateById, p => p.RequireClaim("Permission", Permissions.GetStateById));
        options.AddPolicy(Permissions.DeleteState, p => p.RequireClaim("Permission", Permissions.DeleteState));

        options.AddPolicy(Permissions.CreateCity, p => p.RequireClaim("Permission", Permissions.CreateCity));
        options.AddPolicy(Permissions.GetCity, p => p.RequireClaim("Permission", Permissions.GetCity));
        options.AddPolicy(Permissions.GetCityById, p => p.RequireClaim("Permission", Permissions.GetCityById));
        options.AddPolicy(Permissions.DeleteCity, p => p.RequireClaim("Permission", Permissions.DeleteCity));

        options.AddPolicy(Permissions.CreateAddress, p => p.RequireClaim("Permission", Permissions.CreateAddress));
        options.AddPolicy(Permissions.GetAddress, p => p.RequireClaim("Permission", Permissions.GetAddress));
        options.AddPolicy(Permissions.GetAddressById, p => p.RequireClaim("Permission", Permissions.GetAddressById));
        options.AddPolicy(Permissions.DeleteAddress, p => p.RequireClaim("Permission", Permissions.DeleteAddress));
        options.AddPolicy(Permissions.UpdateAddress, p => p.RequireClaim("Permission", Permissions.UpdateAddress));

        options.AddPolicy(Permissions.CreateOrder, p => p.RequireClaim("Permission", Permissions.CreateOrder));
        options.AddPolicy(Permissions.GetOrder, p => p.RequireClaim("Permission", Permissions.GetOrder));
        options.AddPolicy(Permissions.DeleteOrder, p => p.RequireClaim("Permission", Permissions.DeleteOrder));
        options.AddPolicy(Permissions.SelectPaymentMethod, p => p.RequireClaim("Permission", Permissions.SelectPaymentMethod));
        options.AddPolicy(Permissions.GetPaymentQr, p => p.RequireClaim("Permission", Permissions.GetPaymentQr));

        options.AddPolicy(Permissions.GetAllUser, p => p.RequireClaim("Permission", Permissions.GetAllUser));
        options.AddPolicy(Permissions.UpdateUser, p => p.RequireClaim("Permission", Permissions.UpdateUser));
        options.AddPolicy(Permissions.ChangePassword, p => p.RequireClaim("Permission", Permissions.ChangePassword));  
        
        options.AddPolicy(Permissions.CreateOrUpdateReview, p => p.RequireClaim("Permission", Permissions.CreateOrUpdateReview));

        options.AddPolicy(Permissions.GetInvoice, p => p.RequireClaim("Permission", Permissions.GetInvoice));
    });

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowReact",
            policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
    });


    var app = builder.Build();


    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        await DbSeeder.SeedRoles(services);
        await DbSeeder.SeedRolesAndClaims(services);
    }


    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        //app.MapOpenApi();
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseCors("AllowReact");

    app.UseAuthentication();

    app.UseStaticFiles();

    app.UseAuthorization();

    app.UseSession();

    app.MapControllers();

    //Minimal API endpoints

    app.MapGroup("/api/")
        .MapCategoryRoute()
        .WithTags("Category");

    app.MapGroup("/api/")
        .MapSubCategoryRoute()
        .WithTags("SubCategory");

    app.MapGroup("/api/")
        .MapProductRoute()
        .WithTags("Product");

    app.MapGroup("/api/")
       .MapUserRoute()
       .WithTags("User");

    app.MapGroup("/api/")
       .MapFavouriesRoute()
       .WithTags("Favourites");

    app.MapGroup("/api/")
       .MapCategoryRouteDapper()
       .WithTags("CategoryDapper");

    app.MapGroup("/api/")
        .MapCartItemRoute()
        .WithTags("CartItem");

    app.MapGroup("/api/")
        .MapCountryRoute()
        .WithTags("Country");

    app.MapGroup("/api/")
        .MapStateRoute()
        .WithTags("State");

    app.MapGroup("/api/")
        .MapCityRoute()
        .WithTags("City");

    app.MapGroup("/api/")
        .MapAddressRoute()
        .WithTags("Address");

    app.MapGroup("/api/Order")
        .MapOrderRoute()
        .WithTags("Order");

    app.MapGroup("/api/")
        .MapReviewRoute()
        .WithTags("Review");

    app.MapGroup("/api/")
        .MapInvoiceRoute()
        .WithTags("Invoice");

    app.Run();


}
catch (Exception ex)
{

    logger.Error(ex, "Application Stopped Due To Exception");
}
finally
{
    LogManager.Shutdown();
}