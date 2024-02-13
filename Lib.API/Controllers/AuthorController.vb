Imports [Lib].Application.Models.DTOs
Imports [Lib].Application.Models.RequestModels
Imports [Lib].Application.Models.RequestModels.AuthorVMs
Imports [Lib].Application.Services.Abstraction
Imports Microsoft.AspNetCore.Mvc

Namespace Controllers
    <Route("author")>
    <ApiController>
    Public Class AuthorController
        Inherits ControllerBase

        Private ReadOnly _authorService As IAuthorService

        Public Sub New(authorService As IAuthorService)
            _authorService = authorService
        End Sub

        <HttpGet("all")>
        Public Async Function GetAllAuthors() As Task(Of ActionResult(Of List(Of AuthorDTO)))
            Dim authors = Await _authorService.GetAllAuthors()
            Return Ok(authors)
        End Function

        <HttpGet("{authorId}")>
        Public Async Function GetAuthorById(authorId As Long) As Task(Of ActionResult(Of AuthorDTO))
            Dim author = Await _authorService.GetAuthorById(authorId)
            If author IsNot Nothing Then
                Return Ok(author)
            End If
            Return NotFound()
        End Function

        <HttpPost("searchAuthor")>
        Public Async Function GetAuthor(search As SearchVM) As Task(Of ActionResult(Of List(Of AuthorDTO)))
            Dim author = Await _authorService.GetSearchAuthor(search)
            Return Ok(author)
        End Function

        <HttpPost("add")>
        Public Function AddAuthor(addAuthorVM As AddAuthorVM)
            Dim authors = _authorService.AddAuthor(addAuthorVM)
            Return Ok(authors)
        End Function

        <HttpPut("update")>
        Public Function UpdateAuthor(updateVM As UpdateVM)
            Dim authors = _authorService.UpdateAuthor(updateVM)
            Return Ok(authors)
        End Function

        <HttpDelete("delete/{authorId}")>
        Public Function DeleteAuthor(authorId As Long)
            Dim authors = _authorService.DeleteAuthor(authorId)
            Return Ok(authors)
        End Function
    End Class
End Namespace