
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Net
Imports Microsoft.EntityFrameworkCore

Namespace Model
    Public Class Animal
        Private _food As Food
        <Key>
        <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
        Public Property Id As Integer
        <MaxLength(128)>
        Public Property Name As String
        Public Property Age As Integer
        Public Overridable Property Clubs As List(Of Club)
        Public Overridable Property Grades As ICollection(Of Grade)
        Public Overridable Property Job As Job
        Public Property JobId As Integer?
        Public Overridable Property LovedBy As Person
        Public Overridable Property HatedBy As Person

        <BackingField(NameOf(_food))>
        Public Overridable Property Food As Food
            Get
                Return _food
            End Get
            Set(ByVal value As Food)
                _food = value
            End Set
        End Property

        Public Property IpAddress As IPAddress

        Public Overrides Function ToString() As String
            Return $"Animal : Id = {Id} Name = {Name} IpAddress = {IpAddress}"
        End Function
    End Class
End Namespace
