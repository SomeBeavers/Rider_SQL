Namespace Model
    Public Class MapToQuery
        Public Property Id As Integer
        Public Property Fluffiness As FluffinessEnum
        Public Property Size As Integer
        Public Overridable Property Club As Club
        Public Overridable Property ClubId As Integer

        Public Overrides Function ToString() As String
            Return _
                $"MapToQuery: Id = {Id}"
        End Function
    End Class
End Namespace
