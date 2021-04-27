Namespace Model
    Public Class Consequence
        Public Property Name As String

        Public Overrides Function ToString() As String
            Return $"Consequence : Name = {Name}"
        End Function
    End Class
End Namespace
