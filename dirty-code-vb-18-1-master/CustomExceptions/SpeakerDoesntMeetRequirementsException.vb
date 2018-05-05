Public Class SpeakerDoesntMeetRequirementsException
    Inherits Exception

    Public Sub New(ByVal message As String)
    End Sub

    Public Sub New(ByVal format As String, ParamArray args As Object())
    End Sub
End Class
