# DbCodeFirstDemo

## Les diff�rentes �tapes ##
### 1- Installation des outils EF Core Migration ###

```csharp
dotnet tool install --global dotnet-ef
```

### 2- Cr�ation du mod�le de BDD ###

- Cr�ation du mod�le avec utilisation de 
[DataAnnotation](https://learn.microsoft.com/en-us/ef/core/modeling/#use-data-annotations-to-configure-a-model)

- Premi�re cr�ation du sch�ma � partir du mod�le :
` dotnet ef migrations add InitialCreate `

- Mise en place de l'application du mod�le � notre base de donn�es :
```csharp
//Ajout du contexte au builder de l'application pour DI
builder.Services.AddDbContext<DbCodeFirstDemoDbContext>();
[...]
using var serviceScope = app.Services.CreateScope();
//obtention du DbContext ant�rieurement inject�
using var dbCodeFirstDemoDbContext = serviceScope.ServiceProvider
.GetService<DbCodeFirstDemoDbContext>();
//V�rification s'il y a des mises � jour � effectuer
if (dbCodeFirstDemoDbContext?.Database.GetPendingMigrations().Count() > 0)
    dbCodeFirstDemoDbContext.Database.Migrate(); 
```

- Injection de donn�es dans la base lors de sa cr�ation 
[DataSeeding](https://learn.microsoft.com/en-us/ef/core/modeling/data-seeding)

- Suppression du mod�le pour recr�ation :

` dotnet ef migrations remove `

<details><detail>
Si la migration que l'on souhaite supprimer a d�j� �t� appliqu�e en base on doit forcer la suppression :

 ```
 dotnet ef migrations remove -f 
 ```
</detail></details>

- Recr�ation du sch�ma via InitialCreate
- Modification du sch�ma, reg�n�ration du code de migration de base

` dotnet ef migrations add InitialCreate `

` dotnet ef migrations add AlterDefaultValue `

