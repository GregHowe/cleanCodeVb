﻿Imports System.Text
Imports DataAccessLayer
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()> Public Class SpeakerTests

    Private ReadOnly _repository As SqlServerCompactRepository = New SqlServerCompactRepository()

    <TestMethod()>
    Public Sub Register_EmptyFirstName_ThrowsArgumentNullException()

        Dim speaker = GetSpeakerThatWouldBeApproved()

        speaker.FirstName = ""

        Dim exception = ExceptionAssert.Throws(Of ArgumentNullException)(Function() speaker.Register(_repository))

        Assert.AreEqual(exception.[GetType](), GetType(ArgumentNullException))

    End Sub

    <TestMethod()>
    Public Sub Register_EmptyLastName_ThrowsArgumentNullException()

        Dim speaker = GetSpeakerThatWouldBeApproved()

        speaker.LastName = ""

        Dim exception = ExceptionAssert.Throws(Of ArgumentNullException)(Function() speaker.Register(_repository))

        Assert.AreEqual(exception.[GetType](), GetType(ArgumentNullException))

    End Sub

    <TestMethod()>
    Public Sub Register_EmptyEmail_ThrowsArgumentNullException()

        Dim speaker = GetSpeakerThatWouldBeApproved()

        speaker.Email = ""

        Dim exception = ExceptionAssert.Throws(Of ArgumentNullException)(Function() speaker.Register(_repository))

        Assert.AreEqual(exception.[GetType](), GetType(ArgumentNullException))

    End Sub

    <TestMethod()>
    Public Sub Register_HasBlogButHasRedFlags_ReturnsSpeakerId()

        Dim speaker = GetSpeakerWithRedFlags()

        Dim speakerId As Integer? = speaker.Register(New SqlServerCompactRepository())

        Assert.IsFalse(speakerId Is Nothing)

    End Sub

    <TestMethod()>
    Public Sub Register_HasCertificationsButHasRedFlags_ReturnsSpeakerId()

        Dim speaker = GetSpeakerWithRedFlags()

        speaker.Certifications = New List(Of String)() From {"cert1", "cert2", "cert3", "cert4"}

        Dim speakerId As Integer? = speaker.Register(New SqlServerCompactRepository())

        Assert.IsFalse(speakerId Is Nothing)

    End Sub

    <TestMethod()>
    Public Sub Register_SingleSessionThatsOnOldTech_ThrowsNoSessionsApprovedException()

        Dim speaker = GetSpeakerThatWouldBeApproved()

        speaker.Sessions = New List(Of Session)() From {New Session("Cobol for dummies", "Intro to Cobol")}

        Dim exception = ExceptionAssert.Throws(Of NoSessionsApprovedException)(Function() speaker.Register(_repository))

        Assert.AreEqual(exception.[GetType](), GetType(NoSessionsApprovedException))

    End Sub

    <TestMethod()>
    Public Sub Register_NoSessionsPassed_ThrowsArgumentException()

        Dim speaker = GetSpeakerThatWouldBeApproved()

        speaker.Sessions = New List(Of Session)()

        Dim exception = ExceptionAssert.Throws(Of ArgumentException)(Function() speaker.Register(_repository))

        Assert.AreEqual(exception.[GetType](), GetType(ArgumentException))

    End Sub

    <TestMethod()>
    Public Sub Register_DoesntAppearExceptionalAndUsingOldBrowser_ThrowsNoSessionsApprovedException()

        Dim speakerThatDoesntAppearExceptional = GetSpeakerThatWouldBeApproved()

        speakerThatDoesntAppearExceptional.HasBlog = False

        speakerThatDoesntAppearExceptional.Browser = New WebBrowser("IE", 6)

        Dim exception = ExceptionAssert.Throws(Of SpeakerDoesntMeetRequirementsException)(Function() speakerThatDoesntAppearExceptional.Register(_repository))

        Assert.AreEqual(exception.[GetType](), GetType(SpeakerDoesntMeetRequirementsException))

    End Sub

    <TestMethod()>
    Public Sub Register_DoesntAppearExceptionalAndHasAncientEmail_ThrowsNoSessionsApprovedException()

        Dim speakerThatDoesntAppearExceptional = GetSpeakerThatWouldBeApproved()

        speakerThatDoesntAppearExceptional.HasBlog = False

        speakerThatDoesntAppearExceptional.Email = "name@aol.com"

        Dim exception = ExceptionAssert.Throws(Of SpeakerDoesntMeetRequirementsException)(Function() speakerThatDoesntAppearExceptional.Register(_repository))

        Assert.AreEqual(exception.[GetType](), GetType(SpeakerDoesntMeetRequirementsException))

    End Sub

    Private Function GetSpeakerThatWouldBeApproved() As Speaker

        Return New Speaker() With {.FirstName = "First", .LastName = "Last", .Email = "example@domain.com", .Employer = "Example Employer", .HasBlog = True, .Browser = New WebBrowser("test", 1), .YearsExperiencie = 1, .Certifications = New List(Of String)(), .BlogUrl = "", .Sessions = New List(Of Session)() From {New Session("test title", "test description")}}

    End Function

    Private Function GetSpeakerWithRedFlags() As Speaker

        Dim speaker = GetSpeakerThatWouldBeApproved()

        speaker.Email = "tom@aol.com"

        speaker.Browser = New WebBrowser("IE", 6)

        Return speaker

    End Function

End Class