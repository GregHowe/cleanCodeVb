Imports BusinessLayer

Public Class SqlServerCompactRepository
    Implements IRepository

    Private Function IRepository_SaveSpeaker(speaker As Speaker) As Integer Implements IRepository.SaveSpeaker
        Return 1
    End Function

    Public Function GetRegistrationFee(yearsExperience As Integer?) As Integer Implements IRepository.GetRegistrationFee

        Dim feeFakeData As List(Of FeeTableDriven) = New List(Of FeeTableDriven) From {
            New FeeTableDriven With {.MaxYearExperience = 1, .MinYearExperience = 0, .Fee = 500},
            New FeeTableDriven With {.MinYearExperience = 2, .MaxYearExperience = 3, .Fee = 250},
            New FeeTableDriven With {.MinYearExperience = 4, .MaxYearExperience = 5, .Fee = 100},
            New FeeTableDriven With {.MinYearExperience = 6, .MaxYearExperience = 9, .Fee = 50}}

        Dim feedResult = feeFakeData.FirstOrDefault(Function(x) x.MinYearExperience >= yearsExperience AndAlso x.MaxYearExperience <= yearsExperience)
        Return If(feedResult?.Fee, 0)

    End Function

End Class
