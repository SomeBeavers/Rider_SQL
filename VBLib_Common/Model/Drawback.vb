
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace Model
    Public Class Drawback
        <Key>
        <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
        Public Property Id As Integer
        Public Property Title As String
        Public Overridable Property JobDrawbacks As ICollection(Of JobDrawback)
        Public Overridable Property Foods As ICollection(Of Food)
        Public Overridable Property Clubs As ICollection(Of Club)
        Public Overridable Property Consequence As Consequence

        Public Overrides Function ToString() As String
            Return $"Drawback : Id = {Id} Title = {Title}"
        End Function
    End Class
End Namespace
