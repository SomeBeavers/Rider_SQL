Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace Model
    Public Class Food
        <Key>
        <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
        Public Property Id As Integer
        Public Property Title As String
        Public Overridable Property Animal As Animal
        Public Property AnimalId As Integer?
        Public Overridable Property Drawbacks As ICollection(Of Drawback)

        Public Overrides Function ToString() As String
            Return $"Food : Id = {Id} Title = {Title}"
        End Function
    End Class

    Public Class NormalFood
        Inherits Food

        Public Property Taste As Taste

        Public Overrides Function ToString() As String
            Return _
                $"{MyBase.ToString()} NormalFood : Taste = {Taste}"
        End Function
    End Class

    Public Enum Taste
        Excellent
        VeryGood
        Good
        Normal
        Bad
        VeryBad
        Dirt
    End Enum

    Public Class VeganFood
        Inherits Food

        Public Property Calories As Integer

        Public Overrides Function ToString() As String
            Return _
                $"{MyBase.ToString()} VeganFood : Calories = {Calories}"
        End Function
    End Class
End Namespace
