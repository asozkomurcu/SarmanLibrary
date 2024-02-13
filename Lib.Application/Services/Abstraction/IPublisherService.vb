Imports [Lib].Application.Models.DTOs
Imports [Lib].Application.Models.RequestModels
Imports [Lib].Application.Models.RequestModels.PublisherVMs
Imports [Lib].Application.Wrapper

Namespace Services.Abstraction
    Public Interface IPublisherService
        Function GetAllPublishers() As Task(Of Result(Of List(Of PublisherDTO)))
        Function GetSearchPublisher(search As SearchVM) As Task(Of Result(Of List(Of PublisherDTO)))
        Function GetPublisherById(publisherId As Long) As Task(Of Result(Of PublisherDTO))
        Function AddPublisher(addPublisherVM As AddPublisherVM) As Result(Of Integer)
        Function UpdatePublisher(updatePublisherVM As UpdateVM) As Result(Of Integer)
        Function DeletePublisher(publisherId As Long) As Result(Of Boolean)

    End Interface
End Namespace