Imports System

Namespace Model
    Public Class AnimalClub
        Public Property AnimalId As Integer
        Public Property ClubId As Integer
        Public Overridable Property Animal As Animal
        Public Overridable Property Club As Club
        Public Property PublicationDate As DateTime
    End Class
End Namespace
