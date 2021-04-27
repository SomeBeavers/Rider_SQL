Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace Model
    Public Class Club
        <Key>
        <DatabaseGenerated(DatabaseGeneratedOption.Identity)>
        Public Property Id As Integer
        Public Property Title As String
        Public Property LocalizedText As NotMappedText
        Public Overridable Property Animals As ICollection(Of Animal)
        Public Overridable Property Locations As ICollection(Of Location)
        Public Overridable Property Grades As ICollection(Of Grade)
        Public Overridable Property Drawbacks As ICollection(Of Drawback)

        Public Overrides Function ToString() As String
            Return $"Club: Id = {Id} Title = {Title}"
        End Function
    End Class

    <NotMapped>
    Public Class NotMappedText
        Public Property LocalizedText As String
    End Class
End Namespace
