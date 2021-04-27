Imports Microsoft.EntityFrameworkCore
Imports Microsoft.Extensions.Logging
Imports VBLib_Common.Model


Public Class AnimalContext
    Inherits DbContext

    Public Property Persons As DbSet(Of Person)
    Public Property Animals As DbSet(Of Animal)
    Public Property Beavers As DbSet(Of Beaver)
    Public Property Crows As DbSet(Of Crow)
    Public Property Deers As DbSet(Of Deer)
    Public Property Clubs As DbSet(Of Club)
    Public Property Grades As DbSet(Of Grade) 
    Public Property Jobs As DbSet(Of Job)
    Public Property Drawbacks As DbSet(Of Drawback)
    Public Property JobDrawbacks As DbSet(Of JobDrawback)
    Public Property Food As DbSet(Of Food)
    Public Property NormalFood As DbSet(Of NormalFood)
    Public Property VeganFood As DbSet(Of VeganFood)
    Public Property MapToQuery As DbSet(Of MapToQuery)

    Public Function GetAnimalLocation(animalId As Integer) As IQueryable(Of AnimalLocation)
        Return FromExpression(Function() GetAnimalLocation(animalId))
    End Function

    Public Sub New()
    End Sub

    Public Sub New(options As DbContextOptions(Of AnimalContext))
        MyBase.New(options)
    End Sub

    Protected Overrides Sub OnConfiguring(optionsBuilder As DbContextOptionsBuilder)
        If Not optionsBuilder.IsConfigured Then
            optionsBuilder.UseSqlServer(
                "Server=unit-1019\sqlexpress;Database=BeaversLife;Trusted_Connection=True;" &
                "MultipleActiveResultSets=True")
            optionsBuilder.LogTo(AddressOf Console.WriteLine, LogLevel.Information)
            optionsBuilder.AddInterceptors(New MySaveChangesInterceptor())
            optionsBuilder.UseLazyLoadingProxies(False)
        End If
    End Sub

    Protected Overrides Sub OnModelCreating(modelBuilder As ModelBuilder)
        modelBuilder.Entity (Of Animal)().ToTable("Animals")
        modelBuilder.Entity (Of Beaver)().ToTable("Beavers")
        modelBuilder.Entity (Of Crow)().ToTable("Crows")
        modelBuilder.Entity (Of Deer)().ToTable("Deers")
        modelBuilder.Entity (Of Animal)().HasMany(Function(a) a.Clubs).WithMany(Function(c) c.Animals).UsingEntity _
            (Of AnimalClub)(
                Function(c) _
                               c.HasOne(Function(animalClub) animalClub.Club).WithMany().HasForeignKey(
                                   Function(animalClub) animalClub.ClubId),
                Function(a) _
                               a.HasOne(Function(animalClub) animalClub.Animal).WithMany().HasForeignKey(
                                   Function(animalClub) animalClub.AnimalId), Function(j)
                                       j.[Property](Function(pt) pt.PublicationDate).HasDefaultValueSql(
                                           "CURRENT_TIMESTAMP")
                                       j.HasKey(Function(t) New With {t.AnimalId, t.ClubId
                                                   })
                                   End Function)
        modelBuilder.Entity (Of Grade)(Function(entity)
            entity.[Property](Function(e) e.TheGrade).HasColumnType("decimal(3, 2)").HasAnnotation(
                "Relational:ColumnType", "decimal(3, 2)")
        End Function)
        modelBuilder.Entity (Of JobDrawback)(Function(entity)
            entity.HasKey(Function(j) New With {j.JobId, j.DrawbackId
                             })
            entity.HasOne(Function(jd) jd.Job).WithMany(Function(j) j.JobDrawbacks).HasForeignKey(Function(x) x.JobId).
                                                OnDelete(DeleteBehavior.ClientSetNull)
            entity.HasOne(Function(jd) jd.Drawback).WithMany(Function(d) d.JobDrawbacks).HasForeignKey(
                Function(x) x.DrawbackId).OnDelete(DeleteBehavior.ClientSetNull)
        End Function)
        modelBuilder.Entity (Of MapToQuery)().ToSqlQuery(
            "    select b.Id, b.Fluffiness, b.Size, ac.ClubId as ClubId from Beavers b
                              inner join AnimalClub ac on b.Id = ac.AnimalId
                              Union all
                              select cr.Id, 2, 1, ac.ClubId as ClubId  from Crows cr
                              inner join AnimalClub ac on cr.Id = ac.AnimalId
                    ")
        modelBuilder.SharedTypeEntity (Of Dictionary(Of String, Object))("Category", Function(category)
            category.IndexerProperty (Of Integer)("Id")
            category.IndexerProperty (Of String)("Name").IsRequired()
            category.IndexerProperty (Of Integer?)("FoodId")
            category.HasOne(GetType(Food)).WithOne()
        End Function)
        modelBuilder.SharedTypeEntity (Of Dictionary(Of String, Object))("Product", Function(product)
            product.IndexerProperty (Of Integer)("Id")
            product.IndexerProperty (Of String)("Name").IsRequired()
            product.IndexerProperty (Of Integer?)("CategoryId")
            product.HasOne("Category", Nothing).WithMany()
        End Function)
        modelBuilder.Entity (Of Drawback)(Function(drawback)
            drawback.OwnsOne(Function(d) d.Consequence, Function(cons)
                cons.[Property](Function(c) c.Name).IsRequired()
            End Function)
            drawback.Navigation(Function(d) d.Consequence).IsRequired()
        End Function)
        modelBuilder.Entity(GetType(AnimalLocation)).HasNoKey()
        modelBuilder.HasDbFunction(Function() GetAnimalLocation("default"))
    End Sub
End Class