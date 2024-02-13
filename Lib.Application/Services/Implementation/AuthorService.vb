Imports [Lib].Application.Models.DTOs
Imports [Lib].Application.Models.RequestModels
Imports [Lib].Application.Models.RequestModels.AuthorVMs
Imports [Lib].Application.Services.Abstraction
Imports [Lib].Application.Wrapper
Imports [Lib].Domain
Imports [Lib].Persistence.Context

Public Class AuthorService
    Implements IAuthorService

    Private ReadOnly _unitOfWork As IUnitofWork
    Private ReadOnly _context As LibContext

    Public Sub New(unitOfWork As IUnitofWork, context As LibContext)

        _unitOfWork = unitOfWork
        _context = context
    End Sub

    Public Async Function GetAllAuthors() As Task(Of Result(Of List(Of AuthorDTO))) Implements IAuthorService.GetAllAuthors
        Dim authorRepository = _unitOfWork.GetRepository(Of Author)()
        Dim result As New Result(Of List(Of AuthorDTO))

        Try
            Dim authors = Await authorRepository.GetAllAsync()

            Dim r = authors.Select(Function(author) New AuthorDTO With {
                .Id = author.Id,
                .FirstName = author.FirstName,
                .LastName = author.LastName,
                .BirthDateId = author.Year.Id,
                .CountryId = author.Country.Id,
                .IntDate = author.Year.IntDate,
                .CountryName = author.Country.Name
            }).ToList()

            result.Data = r
            result.Success = True
        Catch ex As Exception
            result.Success = False
            result.Errors.Add("Listeleme işlemi sırasında bir hata oluştu.")
        End Try

        Return result
    End Function

    Public Async Function GetAuthorById(authorId As Long) As Task(Of Result(Of AuthorDTO)) Implements IAuthorService.GetAuthorById
        Dim authorRepository = _unitOfWork.GetRepository(Of Author)()
        Dim result As New Result(Of AuthorDTO)
        Try
            Dim author = Await authorRepository.GetSingleByFilterAsync(Function(e) e.Id = authorId, {"Year", "Country"})

            If author IsNot Nothing Then
                result.Data = New AuthorDTO With {
                    .Id = author.Id,
                    .FirstName = author.FirstName,
                    .LastName = author.LastName,
                    .IntDate = author.Year.IntDate,
                    .CountryName = author.Country.Name
                }
                result.Success = True
            Else
                result.Errors.Add("Yazar bulunamadı.")
            End If
        Catch ex As Exception
            result.Success = False
            result.Errors.Add("Arama işlemi sırasında bir hata oluştu.")
        End Try

        Return result
    End Function

    Public Async Function GetSearchAuthor(search As SearchVM) As Task(Of Result(Of List(Of AuthorDTO))) Implements IAuthorService.GetSearchAuthor
        Dim result = New Result(Of List(Of AuthorDTO))

        Try
            Dim authorEntities As IQueryable(Of Author)
            authorEntities = _context.Authors.Where(Function(x) (x.FirstName.ToUpper.Trim + " " + x.LastName.ToUpper.Trim).Contains(search.SearchText.ToUpper.Trim))


            result.Data = authorEntities.Select(Function(author) New AuthorDTO With {
                .Id = author.Id,
                .FirstName = author.FirstName,
                .LastName = author.LastName,
                .IntDate = author.Year.IntDate,
                .CountryName = author.Country.Name,
                .BirthDateId = author.YearId,
                .CountryId = author.CountryId
            }).ToList()


            result.Success = True
        Catch ex As Exception
            result.Success = False
            result.Errors.Add("Arama işlemi sırasında bir hata oluştu.")
        End Try
        _unitOfWork.Dispose()
        Return result
    End Function


    Public Function AddAuthor(addAuthorVM As AddAuthorVM) As Result(Of Integer) Implements IAuthorService.AddAuthor
        Dim authorRepository = _unitOfWork.GetRepository(Of Author)()
        Dim result As New Result(Of Integer)

        Try
            Dim newAuthor As New Author With {
                .FirstName = addAuthorVM.FirstName,
                .LastName = addAuthorVM.LastName,
                .YearId = addAuthorVM.BirthDateId,
                .CountryId = addAuthorVM.CountryId
            }

            authorRepository.Add(newAuthor)
            _unitOfWork.CommitAsync().Wait()
            result.Success = True
        Catch ex As Exception
            result.Success = False
            result.Errors.Add("Ekleme işlemi sırasında bir hata oluştu.")
        End Try

        Return result
    End Function

    Public Function UpdateAuthor(updateAuthorVM As UpdateVM) As Result(Of Integer) Implements IAuthorService.UpdateAuthor
        Dim authorRepository = _unitOfWork.GetRepository(Of Author)()

        Dim yearRepository = _unitOfWork.GetRepository(Of Year)()
        Dim countryRepository = _unitOfWork.GetRepository(Of Country)()
        Dim result As New Result(Of Integer)

        Try
            Dim existingAuthor = authorRepository.GetById(updateAuthorVM.Id).Result
            Dim existingYear = yearRepository.GetSingleByFilterAsync(Function(x) x.Id = updateAuthorVM.IntDate).Result
            Dim existingCountry = countryRepository.GetSingleByFilterAsync(Function(x) x.Name = updateAuthorVM.CountryName).Result

            If existingAuthor IsNot Nothing Then
                existingAuthor.FirstName = updateAuthorVM.FirstName
                existingAuthor.LastName = updateAuthorVM.LastName
                existingAuthor.YearId = updateAuthorVM.IntDate
                existingAuthor.CountryId = existingCountry.Id

                authorRepository.Update(existingAuthor)
                _unitOfWork.CommitAsync().Wait()
                result.Success = True
            Else
                result.Errors.Add("Yazar bulunamadı.")
                result.Success = False
            End If
        Catch ex As Exception
            result.Success = False
            result.Errors.Add("Güncelleme işlemi sırasında bir hata oluştu.")
        End Try

        Return result
    End Function

    Public Function DeleteAuthor(deleteAuthorByIdVM As Long) As Result(Of Boolean) Implements IAuthorService.DeleteAuthor
        Dim authorRepository = _unitOfWork.GetRepository(Of Author)()

        Dim result As New Result(Of Boolean)


        Try
            Dim authorToDelete = authorRepository.GetById(deleteAuthorByIdVM).Result

            If authorToDelete IsNot Nothing Then
                authorRepository.Delete(authorToDelete)
                _unitOfWork.CommitAsync().Wait()
                result.Success = True
            Else
                result.Errors.Add("Yazar bulunamadı.")
            End If
        Catch ex As Exception
            result.Success = False
            result.Errors.Add("Silme işlemi sırasında bir hata oluştu.")
        End Try

        Return result
    End Function
End Class
