Namespace Model
    Public Class Beaver
        Inherits Animal

        Public Property Fluffiness As FluffinessEnum
        Public Property Size As Integer

        Public Overrides Function ToString() As String
            Return _
                $"{MyBase.ToString()} Beaver: Fluffiness = {Me.Fluffiness} Size = {Me.Size}"
        End Function
    End Class

    Public Enum FluffinessEnum
        NotFluffy
        Fluffy
        VeryFluffy
    End Enum
End Namespace
