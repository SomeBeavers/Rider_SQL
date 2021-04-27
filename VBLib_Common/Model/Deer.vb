Namespace Model
    Public Class Deer
        Inherits Animal

        Public Property Horns As Boolean

        Public Overrides Function ToString() As String
            Return _
                $"{MyBase.ToString()} Deer : Horns = {Me.Horns}"
        End Function
    End Class
End Namespace
