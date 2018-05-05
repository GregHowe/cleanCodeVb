Public Class Speaker

    Public Property FirstName As String

    Public Property LastName As String

    Public Property Email As String

    Public Property YearsExperiencie As Integer?

    Public Property HasBlog As Boolean

    Public Property BlogUrl As String

    Public Property Browser As WebBrowser

    Public Property Certifications As List(Of String)

    Public Property Employer As String

    Public Property RegistrationFee As Integer

    Public Property Sessions As List(Of Session)

    Public Function Register(ByVal repository As IRepository) As Integer?

        ValidateInformation()

        ValidateFeatures()

        ValidateSessions()

        RegistrationFee = repository.GetRegistrationFee(YearsExperiencie)

        Return repository.SaveSpeaker(Me)

    End Function

    Private Sub ValidateInformation()
        If String.IsNullOrWhiteSpace(FirstName) Then
            Throw New ArgumentNullException("First Name is required")
        End If

        If String.IsNullOrWhiteSpace(LastName) Then
            Throw New ArgumentNullException("Last name is required.")
        End If

        If String.IsNullOrWhiteSpace(Email) Then
            Throw New ArgumentNullException("Email is required.")
        End If
    End Sub

    Private Sub ValidateFeatures()

        Dim isGood As Boolean
        Dim isNavigatorDomain As Boolean
        Dim isAcepted As Boolean

        isGood = IsGoodWorker()
        isNavigatorDomain = IsNavigatorDomainValidate()

        isAcepted = isGood OrElse Not isNavigatorDomain

        If Not isAcepted Then
            Throw New SpeakerDoesntMeetRequirementsException("Speaker doesn't meet our abitrary and capricious standards.")
        End If
    End Sub

    Private Function IsGoodWorker() As Boolean

        Const numberCertifications As Integer = 3
        Const maxNumberYearsExperience As Integer = 10

        Dim employersList As List(Of String) = New List(Of String) From {"Microsoft", "Google", "Fog Creek Software", "37Signals"}

        Return YearsExperiencie > maxNumberYearsExperience OrElse HasBlog OrElse Certifications.Count > numberCertifications OrElse employersList.Contains(Employer)

    End Function

    Private Function IsNavigatorDomainValidate() As Boolean

        Const validBrowserVersion As Integer = 9

        Dim domains As List(Of String) = New List(Of String) From {"aol.com", "hotmail.com", "prodigy.com", "CompuServe.com"}

        Dim emailDomain As String = Email.Split("@"c).Last()

        Return domains.Contains(emailDomain) OrElse (Browser.Name = BrowserName.InternetExplorer AndAlso Browser.MajorVersion < validBrowserVersion)

    End Function

    Private Sub ValidateSessions()

        If (Sessions.Count = 0) Then Throw New ArgumentException("Can't register speaker with no sessions to present.")

        For Each session As Session In Sessions

            session.Approved = Not IsSessionAboutOldTechnologies(session)

        Next

        If Not Sessions.Any(Function(x) x.Approved) Then

            Throw New NoSessionsApprovedException("No sessions approved.")

        End If
    End Sub

    Private Function IsSessionAboutOldTechnologies(ByVal session As Session) As Boolean

        Dim oldTechnologies As List(Of String) = New List(Of String) From {"Cobol", "Punch Cards", "Commodore", "VBScript"}

        Return oldTechnologies.Any(Function(x) session.Title.Contains(x) OrElse session.Description.Contains(x))

    End Function
End Class
