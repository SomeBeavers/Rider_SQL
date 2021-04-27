Imports System.ComponentModel.DataAnnotations.Schema

Namespace Model
    Public Class Crow
        Inherits Animal

        Public Property Color As String
        <NotMapped>
        Public Property Size As Integer

        Public Overrides Function ToString() As String
            Return _
                $"{MyBase.ToString()} Crow : Color = {Me.Color} Size = {Me.Size} (cause [NotMapped])"
        End Function
    End Class
End Namespace
