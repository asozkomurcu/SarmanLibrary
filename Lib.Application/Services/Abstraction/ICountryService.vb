Imports [Lib].Application.Models.DTOs
Imports [Lib].Application.Wrapper

Namespace Services.Abstraction
    Public Interface ICountryService
        Function GetAllCountries() As Task(Of Result(Of List(Of CountryDTO)))

    End Interface
End Namespace