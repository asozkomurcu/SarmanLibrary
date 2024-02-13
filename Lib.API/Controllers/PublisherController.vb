Imports [Lib].Application.Models.DTOs
Imports [Lib].Application.Models.RequestModels
Imports [Lib].Application.Models.RequestModels.PublisherVMs
Imports [Lib].Application.Services.Abstraction
Imports Microsoft.AspNetCore.Mvc

Namespace Controllers
    <Route("publisher")>
    <ApiController>
    Public Class PublisherController
        Inherits ControllerBase

        Private ReadOnly _publisherService As IPublisherService

        Public Sub New(publisherService As IPublisherService)
            _publisherService = publisherService
        End Sub

        <HttpGet("all")>
        Public Async Function GetAllPublishers() As Task(Of ActionResult(Of List(Of PublisherDTO)))
            Dim publishers = Await _publisherService.GetAllPublishers()
            Return Ok(publishers)
        End Function

        <HttpGet("{publisherId}")>
        Public Async Function GetPublisherById(getPublisherByIdVM As Long) As Task(Of ActionResult(Of PublisherDTO))
            Dim publisher = Await _publisherService.GetPublisherById(getPublisherByIdVM)
            If publisher IsNot Nothing Then
                Return Ok(publisher)
            End If
            Return NotFound()
        End Function

        <HttpPost("searchPublisher")>
        Public Async Function GetPublisher(search As SearchVM) As Task(Of ActionResult(Of List(Of PublisherDTO)))
            Dim publisher = Await _publisherService.GetSearchPublisher(search)
            Return Ok(publisher)
        End Function

        <HttpPost("add")>
        Public Function AddPublisher(addPublisherVM As AddPublisherVM)
            Dim publishers = _publisherService.AddPublisher(addPublisherVM)
            Return Ok(publishers)
        End Function

        <HttpPut("update")>
        Public Function UpdatePublisher(updateVM As UpdateVM)
            Dim publishers = _publisherService.UpdatePublisher(updateVM)
            Return Ok(publishers)
        End Function

        '<<<<<<< Updated upstream
        '    <HttpDelete("{publisherId}")>
        '    Public Sub DeletePublisher(publisherId As Long)
        '        _publisherService.DeletePublisher(publisherId)
        '    End Sub
        '=======
        <HttpDelete("delete/{publisherId}")>
        Public Function DeletePublisher(publisherId As Long)
            Dim publishers = _publisherService.DeletePublisher(publisherId)
            Return Ok(publishers)
        End Function

        '>>>>>>> Stashed changes
    End Class
End Namespace