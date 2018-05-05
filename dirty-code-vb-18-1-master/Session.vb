Public Class Session

    Public Property Approved As Boolean

    Public Property Title As String

    Public Property Description As String

    Public Sub New(ByVal title As String, ByVal description As String)
        Me.Title = title
        Me.Description = description
    End Sub
End Class