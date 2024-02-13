Imports [Lib].Application
Imports [Lib].Application.Models.DTOs
Imports [Lib].Application.Services.Abstraction
Imports Microsoft.AspNetCore.Mvc

<Route("country")>
<ApiController>
Public Class CountryController
    Inherits ControllerBase

    Private ReadOnly _countryService As ICountryService

    Public Sub New(countryService As ICountryService)
        _countryService = countryService
    End Sub

    <HttpGet("all")>
    Public Async Function GetAllCountries() As Task(Of ActionResult(Of List(Of CountryDTO)))
        Dim country = Await _countryService.GetAllCountries()
        Return Ok(country)
    End Function

End Class