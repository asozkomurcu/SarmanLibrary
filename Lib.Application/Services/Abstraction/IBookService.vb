Imports [Lib].Application.Models.DTOs
Imports [Lib].Application.Models.RequestModels
Imports [Lib].Application.Models.RequestModels.BookVMs
Imports [Lib].Application.Wrapper

Namespace Services.Abstraction
    Public Interface IBookService
        Function GetAllBooks() As Task(Of Result(Of List(Of BookDTO)))
        Function GetSearchBooks(search As SearchVM) As Task(Of Result(Of List(Of BookDTO)))
        Function GetBookById(bookId As Long) As Task(Of Result(Of BookDTO))
        Function AddBook(addBookVM As AddBookVM) As Result(Of Integer)
        Function UpdateBook(updateBookVM As UpdateVM) As Result(Of Integer)
        Function DeleteBook(bookId As Long) As Result(Of Boolean)
        Function GetBooksByCategory(categoryId As Long) As Task(Of Result(Of List(Of BookDTO)))
        Function GetBooksByAuthor(authorId As Long) As Task(Of Result(Of List(Of BookDTO)))
        Function GetBooksByPublisher(publisherId As Long) As Task(Of Result(Of List(Of BookDTO)))

    End Interface
End Namespace