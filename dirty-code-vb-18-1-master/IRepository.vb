Public Interface IRepository

    Function SaveSpeaker(ByVal speaker As Speaker) As Integer

    Function GetRegistrationFee(ByVal yearsExperience As Integer?) As Integer

End Interface
