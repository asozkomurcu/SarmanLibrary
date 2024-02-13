Imports [Lib].Application.Models.DTOs
Imports [Lib].Application.Models.RequestModels.AuthorVMs
Imports [Lib].Application.Wrapper

Namespace Services.Abstraction
    Public Interface IYearService
        Function GetAllYear() As Task(Of Result(Of List(Of YearDTO)))
        Function GetAllYY(yearVM As YearVM) As Task(Of Result(Of List(Of YearDTO)))

    End Interface
End Namespace