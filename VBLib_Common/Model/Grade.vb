Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace Model
    Public Class Grade
        <Key>
        <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
        Public Property Id As Integer
        Public Property TheGrade As Double?
        Public Overridable Property Club As Club
        Public Overridable Property Animal As Animal

        Public Overrides Function ToString() As String
            Return $"Grade : Id = {Id} Grade = {TheGrade}"
        End Function
    End Class
End Namespace
