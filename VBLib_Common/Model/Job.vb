Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace Model
    Public Class Job
        <Key>
        <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
        Public Property Id As Integer
        Public Property Title As String
        Public Property Salary As Integer
        Public Overridable Property Animals As ICollection(Of Animal)
        Public Overridable Property JobDrawbacks As ICollection(Of JobDrawback)

        Public Overrides Function ToString() As String
            Return $"Job : Id = {Id} Title = {Title}"
        End Function
    End Class
End Namespace
