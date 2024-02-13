Imports [Lib].Application.Models.DTOs
Imports [Lib].Application.Services.Abstraction
Imports [Lib].Application.Wrapper
Imports [Lib].Domain

Public Class CountryService
    Implements ICountryService

    Private ReadOnly _unitOfWork As IUnitofWork
    Public Sub New(unitOfWork As IUnitofWork)
        _unitOfWork = unitOfWork
    End Sub
    Public Async Function GetAllCountries() As Task(Of Result(Of List(Of CountryDTO))) Implements ICountryService.GetAllCountries
        Dim countryRepository = _unitOfWork.GetRepository(Of Country)()
        Dim result As New Result(Of List(Of CountryDTO))

        Try
            Dim countries = Await countryRepository.GetAllAsync()

            result.Data = countries.Select(Function(country) New CountryDTO With {
                .Id = country.Id,
                .Name = country.Name
            }).ToList()

            result.Success = True
        Catch ex As Exception
            result.Success = False
            result.Errors.Add("Listeleme sırasında bir hata oluştu.")
        End Try

        Return result
    End Function
End Class
