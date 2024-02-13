Imports [Lib].Application.Models.DTOs
Imports [Lib].Application.Models.RequestModels.AuthorVMs
Imports [Lib].Application.Services.Abstraction
Imports Microsoft.AspNetCore.Mvc

Namespace Controllers
    <Route("year")>
    <ApiController>
    Public Class YearController
        Inherits ControllerBase

        Private ReadOnly _yearService As IYearService

        Public Sub New(yearService As IYearService)
            _yearService = yearService
        End Sub

        <HttpGet("all")>
        Public Async Function GetAllYears() As Task(Of ActionResult(Of List(Of YearDTO)))
            Dim years = Await _yearService.GetAllYear()
            Return Ok(years)
        End Function

        <HttpPost("allYY")>
        Public Async Function GetAllYY(yearModel As YearVM) As Task(Of ActionResult(Of List(Of YearDTO)))
            Dim years = Await _yearService.GetAllYY(yearModel)
            Return Ok(years)
        End Function
    End Class
End Namespace