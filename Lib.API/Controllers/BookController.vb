Imports [Lib].Application.Models.DTOs
Imports [Lib].Application.Models.RequestModels
Imports [Lib].Application.Models.RequestModels.BookVMs
Imports [Lib].Application.Services.Abstraction
Imports Microsoft.AspNetCore.Mvc

Namespace Controllers
    <Route("book")>
    <ApiController>
    Public Class BookController
        Inherits ControllerBase

        Private ReadOnly _bookService As IBookService

        Public Sub New(bookService As IBookService)
            _bookService = bookService
        End Sub

        <HttpGet("all")>
        Public Async Function GetAllBooks() As Task(Of ActionResult(Of List(Of BookDTO)))
            Dim books = Await _bookService.GetAllBooks()
            Return Ok(books)
        End Function


        <HttpPost("searchBook")>
        Public Async Function GetBooks(search As SearchVM) As Task(Of ActionResult(Of List(Of BookDTO)))
            Dim books = Await _bookService.GetSearchBooks(search)
            Return Ok(books)
        End Function

        <HttpGet("{bookId}")>
        Public Async Function GetBookById(bookId As Long) As Task(Of ActionResult(Of BookDTO))
            Dim book = Await _bookService.GetBookById(bookId)
            If book IsNot Nothing Then
                Return Ok(book)
            End If
            Return NotFound()
        End Function

        <HttpGet("category/{categoryId}")>
        Public Async Function GetBooksByCategory(categoryId As Long) As Task(Of ActionResult(Of List(Of BookDTO)))
            Dim books = Await _bookService.GetBooksByCategory(categoryId)
            Return Ok(books)
        End Function

        <HttpGet("author/{authorId}")>
        Public Async Function GetBooksByAuthor(authorId As Long) As Task(Of ActionResult(Of List(Of BookDTO)))
            Dim books = Await _bookService.GetBooksByAuthor(authorId)
            Return Ok(books)
        End Function

        <HttpGet("publisher/{publisherId}")>
        Public Async Function GetBooksByPublisher(publisherId As Long) As Task(Of ActionResult(Of List(Of BookDTO)))
            Dim books = Await _bookService.GetBooksByPublisher(publisherId)
            Return Ok(books)
        End Function

        <HttpPost("add")>
        Public Function AddBook(addBookVM As AddBookVM)
            Dim books = _bookService.AddBook(addBookVM)
            Return Ok(books)
        End Function

        <HttpPut("update")>
        Public Function UpdateBook(updateBookVM As UpdateVM)
            Dim books = _bookService.UpdateBook(updateBookVM)
            Return Ok(books)
        End Function

        <HttpDelete("delete/{bookId}")>
        Public Function DeleteBook(bookId As Long)
            Dim books = _bookService.DeleteBook(bookId)
            Return Ok(books)
        End Function
    End Class
End Namespace