# DbCodeFirstDemo

## Les différentes étapes ##
### 1- Installation des outils EF Core Migration ###

```csharp
dotnet tool install --global dotnet-ef
```

### 2- Création du modèle de BDD ###

- Création du modèle avec utilisation de 
[DataAnnotation](https://learn.microsoft.com/en-us/ef/core/modeling/#use-data-annotations-to-configure-a-model)

- Première création du schéma à partir du modèle :
` dotnet ef migrations add InitialCreate `

- Mise en place de l'application du modèle à notre base de données :
```csharp
//Ajout du contexte au builder de l'application pour DI
builder.Services.AddDbContext<DbCodeFirstDemoDbContext>();
[...]
using var serviceScope = app.Services.CreateScope();
//obtention du DbContext antérieurement injecté
using var dbCodeFirstDemoDbContext = serviceScope.ServiceProvider
.GetService<DbCodeFirstDemoDbContext>();
//Vérification s'il y a des mises à jour à effectuer
if (dbCodeFirstDemoDbContext?.Database.GetPendingMigrations().Count() > 0)
    dbCodeFirstDemoDbContext.Database.Migrate(); 
```

- Injection de données dans la base lors de sa création 
[DataSeeding](https://learn.microsoft.com/en-us/ef/core/modeling/data-seeding)

- Suppression du modèle pour recréation :

` dotnet ef migrations remove `

<details><detail>
Si la migration que l'on souhaite supprimer a déjà été appliquée en base on doit forcer la suppression :

 ```
 dotnet ef migrations remove -f 
 ```
</detail></details>

- Recréation du schéma via InitialCreate
- Modification du schéma, regénération du code de migration de base

` dotnet ef migrations add InitialCreate `

` dotnet ef migrations add AlterDefaultValue `

