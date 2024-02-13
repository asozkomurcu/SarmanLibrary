Imports [Lib].Application.Models.DTOs
Imports [Lib].Application.Models.RequestModels
Imports [Lib].Application.Models.RequestModels.AuthorVMs
Imports [Lib].Application.Wrapper

Namespace Services.Abstraction
    Public Interface IAuthorService
        Function GetAllAuthors() As Task(Of Result(Of List(Of AuthorDTO)))
        Function GetSearchAuthor(search As SearchVM) As Task(Of Result(Of List(Of AuthorDTO)))
        Function GetAuthorById(authorId As Long) As Task(Of Result(Of AuthorDTO))
        Function AddAuthor(addAuthorVM As AddAuthorVM) As Result(Of Integer)
        Function UpdateAuthor(updateAuthorVM As UpdateVM) As Result(Of Integer)
        Function DeleteAuthor(aurhorId As Long) As Result(Of Boolean)
    End Interface
End Namespace