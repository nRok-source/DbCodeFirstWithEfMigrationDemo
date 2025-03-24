using DbCodeFirstDemo.Models.DataBase;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Ajout du contexte au builder de l'application pour DI
builder.Services.AddDbContext<DbCodeFirstDemoDbContext>();
var app = builder.Build();

#region "Initialisation d'un context de BDD pour voir si l'on doit appliquer des migrations au démarrage de l'application"

using var serviceScope = app.Services.CreateScope();
//obtention du DbContext antérieurement injecté
using var dbCodeFirstDemoDbContext = serviceScope.ServiceProvider.GetService<DbCodeFirstDemoDbContext>();

//Vérification s'il y a des mises à jour à effectuer
if (dbCodeFirstDemoDbContext?.Database.GetPendingMigrations().Count() > 0)
    dbCodeFirstDemoDbContext.Database.Migrate();

//DataSeeding
if (dbCodeFirstDemoDbContext != null)
{
    bool exists = dbCodeFirstDemoDbContext.Database.SqlQuery<string>($"SELECT name AS Value FROM sys.tables where name = 'Blog'").FirstOrDefault() == "Blog";
    if (exists)
    {
        var testBlog = dbCodeFirstDemoDbContext.Blogs.FirstOrDefault();
        if (testBlog == null)
        {
            dbCodeFirstDemoDbContext.Blogs.AddRange(new Blog
            {
                Url = "http://blog1.com",
                Pages = new List<Page>() {
                    new Page { Title = "Article 1", Html="<p>Contenu de l'article 1...</p>",
                        Comments =new List<Comment>() {
                            new Comment{ Html = "<div class=\"comment\">Commentaire 1 sur l'article 1</div>",   },
                            new Comment{ Html = "<div class=\"comment\">Commentaire 2 sur l'article 1</div>",   },
                            new Comment{ Html = "<div class=\"comment\">Commentaire 3 sur l'article 1</div>",   },
                            new Comment{ Html = "<div class=\"comment\">Commentaire 4 sur l'article 1</div>",   },
                        }},
                    new Page { Title = "Article 2", Html="<p>Contenu de l'article 2...</p>",
                        Comments =new List<Comment>() {
                            new Comment{ Html = "<div class=\"comment\">Commentaire 1 sur l'article 2</div>",   },
                            new Comment{ Html = "<div class=\"comment\">Commentaire 2 sur l'article 2</div>",   },
                            new Comment{ Html = "<div class=\"comment\">Commentaire 3 sur l'article 2</div>",   },
                            new Comment{ Html = "<div class=\"comment\">Commentaire 4 sur l'article 2</div>",   },
                        }},
                    new Page { Title = "Article 3", Html="<p>Contenu de l'article 3...</p>",
                        Comments =new List<Comment>() {
                            new Comment{ Html = "<div class=\"comment\">Commentaire 1 sur l'article 3</div>",   },
                            new Comment{ Html = "<div class=\"comment\">Commentaire 2 sur l'article 3</div>",   },
                            new Comment{ Html = "<div class=\"comment\">Commentaire 3 sur l'article 3</div>",   },
                            new Comment{ Html = "<div class=\"comment\">Commentaire 4 sur l'article 3</div>",   },
                        }}},
            }, 
            new Blog
            {
                Url = "http://blog2.com",
                Pages = new List<Page>() {
                    new Page { Title = "Article 1", Html="<p>Contenu de l'article 1...</p>",
                        Comments =new List<Comment>() {
                            new Comment{ Html = "<div class=\"comment\">Commentaire 1 sur l'article 1</div>",   },
                            new Comment{ Html = "<div class=\"comment\">Commentaire 2 sur l'article 1</div>",   },
                            new Comment{ Html = "<div class=\"comment\">Commentaire 3 sur l'article 1</div>",   },
                            new Comment{ Html = "<div class=\"comment\">Commentaire 4 sur l'article 1</div>",   },
                        }},
                    new Page { Title = "Article 2", Html="<p>Contenu de l'article 2...</p>",
                        Comments =new List<Comment>() {
                            new Comment{ Html = "<div class=\"comment\">Commentaire 1 sur l'article 2</div>",   },
                            new Comment{ Html = "<div class=\"comment\">Commentaire 2 sur l'article 2</div>",   },
                            new Comment{ Html = "<div class=\"comment\">Commentaire 3 sur l'article 2</div>",   },
                            new Comment{ Html = "<div class=\"comment\">Commentaire 4 sur l'article 2</div>",   },
                        }},
                    new Page { Title = "Article 3", Html="<p>Contenu de l'article 3...</p>",
                        Comments =new List<Comment>() {
                            new Comment{ Html = "<div class=\"comment\">Commentaire 1 sur l'article 3</div>",   },
                            new Comment{ Html = "<div class=\"comment\">Commentaire 2 sur l'article 3</div>",   },
                            new Comment{ Html = "<div class=\"comment\">Commentaire 3 sur l'article 3</div>",   },
                            new Comment{ Html = "<div class=\"comment\">Commentaire 4 sur l'article 3</div>",   },
                        }}},
            });
            await dbCodeFirstDemoDbContext.SaveChangesAsync();
        }
    }
}

#endregion

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
