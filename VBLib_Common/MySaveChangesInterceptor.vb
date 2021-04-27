Imports System
Imports System.Threading
Imports System.Threading.Tasks
Imports Microsoft.EntityFrameworkCore
Imports Microsoft.EntityFrameworkCore.Diagnostics


Public Class MySaveChangesInterceptor
    Inherits SaveChangesInterceptor

    Public Overrides Function SavingChanges(ByVal eventData As DbContextEventData, ByVal result As InterceptionResult(Of Integer)) As InterceptionResult(Of Integer)
        Console.ForegroundColor = ConsoleColor.Red
        Console.WriteLine($"Saving changes for {eventData.Context.Database.GetConnectionString()}")
        Console.ForegroundColor = ConsoleColor.White
        Return result
    End Function
End Class