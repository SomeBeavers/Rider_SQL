Imports Microsoft.EntityFrameworkCore

Namespace Model
    <Owned>
    Public Class Location
        Public Property Address As String
        Public Overridable Property Club As Club

        Public Overrides Function ToString() As String
            Return $"Location: Address = {Address}"
        End Function
    End Class
End Namespace
