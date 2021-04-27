Namespace Model
    Public Class AnimalLocation
        Public Property Name As String
        Public Property Address As String

        Public Overrides Function ToString() As String
            Return $"AnimalLocation : Name = {Name} Address = {Address}"
        End Function
    End Class
End Namespace
