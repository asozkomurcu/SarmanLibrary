Imports [Lib].Application.Models.DTOs
Imports [Lib].Application.Models.RequestModels
Imports [Lib].Application.Models.RequestModels.BookVMs
Imports [Lib].Application.Services.Abstraction
Imports [Lib].Application.Wrapper
Imports [Lib].Domain
Imports [Lib].Persistence.Context

Public Class BookService
    Implements IBookService

    Private ReadOnly _unitOfWork As IUnitofWork
    Private ReadOnly _context As LibContext



    Public Sub New(unitOfWork As IUnitofWork, context As LibContext)
        _unitOfWork = unitOfWork
        _context = context
    End Sub


    Public Async Function GetSearchBooks(search As SearchVM) As Task(Of Result(Of List(Of BookDTO))) Implements IBookService.GetSearchBooks
        Dim result = New Result(Of List(Of BookDTO))

        Try
            Dim bookEntities As IQueryable(Of Book)
            bookEntities = _context.Books.Where(Function(x) x.BookName.ToUpper.Trim.Contains(search.SearchText.ToUpper.Trim))


            result.Data = bookEntities.Select(Function(book) New BookDTO With {
                .Id = book.Id,
                .ReleaseDateId = book.YearId,
                .CategoryId = book.CategoryId,
                .PublisherId = book.PublisherId,
                .AuthorId = book.AuthorId,
                .CountryId = book.Author.CountryId,
                .BookName = book.BookName,
                .AuthorFirstName = book.Author.FirstName,
                .AuthorLastName = book.Author.LastName,
                .Page = book.Page,
                .Detail = book.Detail,
                .IsRead = book.IsRead,
                .CategoryName = book.Category.Name,
                .PublisherName = book.Publisher.Name,
                .IntDate = book.Year.IntDate
            }).ToList()

            result.Success = True
        Catch ex As Exception
            result.Success = False
            result.Errors.Add("Listeleme işlemi sırasında bir hata oluştu.")
        End Try
        _unitOfWork.Dispose()
        Return result
    End Function

    Public Async Function GetAllBooks() As Task(Of Result(Of List(Of BookDTO))) Implements IBookService.GetAllBooks
        Dim bookRepository = _unitOfWork.GetRepository(Of Book)()
        Dim result As New Result(Of List(Of BookDTO))

        Try
            Dim books = Await bookRepository.GetAllAsync()

            result.Data = books.Select(Function(book) New BookDTO With {
                .Id = book.Id,
                .ReleaseDateId = book.YearId,
                .CategoryId = book.CategoryId,
                .PublisherId = book.PublisherId,
                .AuthorId = book.AuthorId,
                .CountryId = book.Author.CountryId,
                .BookName = book.BookName,
                .AuthorFirstName = book.Author.FirstName,
                .AuthorLastName = book.Author.LastName,
                .Page = book.Page,
                .Detail = book.Detail,
                .IsRead = book.IsRead,
                .CategoryName = book.Category.Name,
                .PublisherName = book.Publisher.Name,
                .IntDate = book.Year.IntDate
            }).ToList()

            result.Success = True
        Catch ex As Exception
            result.Success = False
            result.Errors.Add("Listeleme işlemi sırasında bir hata oluştu.")
        End Try

        Return result
    End Function

    Public Function AddBook(addBookVM As AddBookVM) As Result(Of Integer) Implements IBookService.AddBook
        Dim bookRepository = _unitOfWork.GetRepository(Of Book)()
        Dim result As New Result(Of Integer)

        Try
            Dim newBook As New Book With {
                .BookName = addBookVM.BookName,
                .Page = addBookVM.Page,
                .YearId = addBookVM.YearId,
                .CategoryId = addBookVM.CategoryId,
                .Detail = addBookVM.Detail,
                .IsRead = addBookVM.IsRead,
                .AuthorId = addBookVM.AuthorId,
                .PublisherId = addBookVM.PublisherId
            }

            bookRepository.Add(newBook)
            _unitOfWork.CommitAsync().Wait()
            result.Success = True
        Catch ex As Exception
            result.Success = False
            result.Errors.Add("Ekleme işlemi sırasında bir hata oluştu.")
        End Try

        Return result
    End Function

    Public Function UpdateBook(updateBookVM As UpdateVM) As Result(Of Integer) Implements IBookService.UpdateBook
        Dim bookRepository = _unitOfWork.GetRepository(Of Book)()
        Dim yearRepository = _unitOfWork.GetRepository(Of Year)()
        Dim result As New Result(Of Integer)


        Try

            Dim existingBook = bookRepository.GetById(updateBookVM.Id).Result
            Dim intDateId = yearRepository.GetSingleByFilterAsync(Function(x) x.IntDate = updateBookVM.IntDate).Result

            If existingBook IsNot Nothing Then
                existingBook.BookName = updateBookVM.BookName
                existingBook.Page = updateBookVM.Page
                existingBook.Detail = updateBookVM.Detail
                existingBook.IsRead = updateBookVM.IsRead
                existingBook.PublisherId = updateBookVM.PublisherId
                existingBook.AuthorId = updateBookVM.AuthorId
                existingBook.CategoryId = updateBookVM.CategoryId
                existingBook.YearId = updateBookVM.ReleaseDateId

                bookRepository.Update(existingBook)
                _unitOfWork.CommitAsync().Wait()
                result.Success = True
            Else
                result.Errors.Add("Kitap bulunamadı.")
                result.Success = False
            End If
        Catch ex As Exception
            result.Success = False
            result.Errors.Add("Güncelleme işlemi sırasında bir hata oluştu.")
        End Try

        Return result
    End Function

    Public Function DeleteBook(bookId As Long) As Result(Of Boolean) Implements IBookService.DeleteBook
        Dim bookRepository = _unitOfWork.GetRepository(Of Book)()

        Dim result As New Result(Of Boolean)

        Try
            Dim bookToDelete = bookRepository.GetById(bookId).Result

            If bookToDelete IsNot Nothing Then
                bookRepository.Delete(bookToDelete)
                _unitOfWork.CommitAsync().Wait()
                result.Success = True
            Else
                result.Errors.Add("Kitap bulunamadı.")
                result.Success = False
            End If
        Catch ex As Exception
            result.Success = False
            result.Errors.Add("Silme işlemi sırasında bir hata oluştu.")
        End Try

        Return result
    End Function

    Public Async Function GetBookById(bookId As Long) As Task(Of Result(Of BookDTO)) Implements IBookService.GetBookById
        Dim bookRepository = _unitOfWork.GetRepository(Of Book)()
        Dim result As New Result(Of BookDTO)

        Try
            Dim book = Await bookRepository.GetSingleByFilterAsync(Function(e) e.Id = bookId, {"Author", "Category", "Year", "Publisher"})

            If book IsNot Nothing Then
                result.Data = New BookDTO With {
                    .Id = book.Id,
                    .ReleaseDateId = book.YearId,
                    .CategoryId = book.CategoryId,
                    .PublisherId = book.PublisherId,
                    .AuthorId = book.AuthorId,
                    .CountryId = book.Author.CountryId,
                    .BookName = book.BookName,
                    .AuthorFirstName = book.Author.FirstName,
                    .AuthorLastName = book.Author.LastName,
                    .Page = book.Page,
                    .Detail = book.Detail,
                    .IsRead = book.IsRead,
                    .CategoryName = book.Category.Name,
                    .PublisherName = book.Publisher.Name,
                    .IntDate = book.Year.IntDate
                }
                result.Success = True
            Else
                result.Errors.Add("Kitap bulunamadı")
                result.Success = False
            End If

        Catch ex As Exception
            result.Success = False
            result.Errors.Add("Arama işlemi sırasında bir hata oluştu.")
        End Try

        Return result
    End Function

    Public Async Function GetBooksByCategory(categoryId As Long) As Task(Of Result(Of List(Of BookDTO))) Implements IBookService.GetBooksByCategory
        Dim bookRepository = _unitOfWork.GetRepository(Of Book)()
        Dim result As New Result(Of List(Of BookDTO))

        Try
            Dim books = Await bookRepository.GetByFilterAsync(Function(book) book.Category.Id = categoryId, "Author", "Category", "Year", "Publisher")

            result.Data = books.Select(Function(book) New BookDTO With {
                .Id = book.Id,
                .ReleaseDateId = book.YearId,
                .CategoryId = book.CategoryId,
                .PublisherId = book.PublisherId,
                .AuthorId = book.AuthorId,
                .CountryId = book.Author.CountryId,
                .BookName = book.BookName,
                .AuthorFirstName = book.Author.FirstName,
                .AuthorLastName = book.Author.LastName,
                .Page = book.Page,
                .Detail = book.Detail,
                .IsRead = book.IsRead,
                .CategoryName = book.Category.Name,
                .PublisherName = book.Publisher.Name,
                .IntDate = book.Year.IntDate
            }).ToList()

            result.Success = True
        Catch ex As Exception
            result.Success = False
            result.Errors.Add("Listeleme işlemi sırasında bir hata oluştu.")
        End Try

        Return result
    End Function

    Public Async Function GetBooksByAuthor(authorId As Long) As Task(Of Result(Of List(Of BookDTO))) Implements IBookService.GetBooksByAuthor
        Dim bookRepository = _unitOfWork.GetRepository(Of Book)()
        Dim result As New Result(Of List(Of BookDTO))

        Try
            Dim books = Await bookRepository.GetByFilterAsync(Function(book) book.Author.Id = authorId, "Author", "Category", "Publisher", "Year")

            result.Data = books.Select(Function(book) New BookDTO With {
                .Id = book.Id,
                .ReleaseDateId = book.YearId,
                .CategoryId = book.CategoryId,
                .PublisherId = book.PublisherId,
                .AuthorId = book.AuthorId,
                .CountryId = book.Author.CountryId,
                .BookName = book.BookName,
                .AuthorFirstName = book.Author.FirstName,
                .AuthorLastName = book.Author.LastName,
                .Page = book.Page,
                .Detail = book.Detail,
                .IsRead = book.IsRead,
                .CategoryName = book.Category.Name,
                .PublisherName = book.Publisher.Name,
                .IntDate = book.Year.IntDate
            }).ToList()

            result.Success = True
        Catch ex As Exception
            result.Success = False
            result.Errors.Add("Listeleme işlemi sırasında bir hata oluştu.")
        End Try

        Return result
    End Function

    Public Async Function GetBooksByPublisher(publisherId As Long) As Task(Of Result(Of List(Of BookDTO))) Implements IBookService.GetBooksByPublisher
        Dim bookRepository = _unitOfWork.GetRepository(Of Book)()
        Dim result As New Result(Of List(Of BookDTO))

        Try
            Dim books = Await bookRepository.GetByFilterAsync(Function(book) book.Publisher.Id = publisherId, "Author", "Category", "Publisher", "Year")

            result.Data = books.Select(Function(book) New BookDTO With {
                .Id = book.Id,
                .ReleaseDateId = book.YearId,
                .CategoryId = book.CategoryId,
                .PublisherId = book.PublisherId,
                .AuthorId = book.AuthorId,
                .CountryId = book.Author.CountryId,
                .BookName = book.BookName,
                .AuthorFirstName = book.Author.FirstName,
                .AuthorLastName = book.Author.LastName,
                .Page = book.Page,
                .Detail = book.Detail,
                .IsRead = book.IsRead,
                .CategoryName = book.Category.Name,
                .PublisherName = book.Publisher.Name,
                .IntDate = book.Year.IntDate
            }).ToList()

            result.Success = True
        Catch ex As Exception
            result.Success = False
            result.Errors.Add("Listeleme işlemi sırasında bir hata oluştu.")
        End Try

        Return result
    End Function
End Class
