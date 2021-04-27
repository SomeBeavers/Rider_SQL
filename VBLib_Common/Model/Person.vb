
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations.Schema

Namespace Model
    Public Class Person
        <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
        Public Property Id As Integer
        Public Property Name As String
        <InverseProperty("LovedBy")>
        Public Overridable Property AnimalsLoved As List(Of Animal)
        <InverseProperty("HatedBy")>
        Public Overridable Property AnimalsHated As List(Of Animal)

        Public Overrides Function ToString() As String
            Return $"Person : Id = {Id} Name = {Name}"
        End Function
    End Class
End Namespace
