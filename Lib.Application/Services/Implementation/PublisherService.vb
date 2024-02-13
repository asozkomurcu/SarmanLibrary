Imports [Lib].Application.Models.DTOs
Imports [Lib].Application.Models.RequestModels
Imports [Lib].Application.Models.RequestModels.PublisherVMs
Imports [Lib].Application.Services.Abstraction
Imports [Lib].Application.Wrapper
Imports [Lib].Domain
Imports [Lib].Persistence.Context

Namespace Services.Implementation
    Public Class PublisherService
        Implements IPublisherService

        Private ReadOnly _unitOfWork As IUnitofWork
        Private ReadOnly _context As LibContext



        Public Sub New(unitOfWork As IUnitofWork, context As LibContext)
            _unitOfWork = unitOfWork
            _context = context
        End Sub


        Public Async Function GetAllPublishers() As Task(Of Result(Of List(Of PublisherDTO))) Implements IPublisherService.GetAllPublishers
            Dim publisherRepository = _unitOfWork.GetRepository(Of Publisher)()
            Dim result As New Result(Of List(Of PublisherDTO))

            Try
                Dim publishers = Await publisherRepository.GetAllAsync()

                result.Data = publishers.Select(Function(publisher) New PublisherDTO With {
                    .Id = publisher.Id,
                    .Name = publisher.Name,
                    .Description = publisher.Description
                }).ToList()

                result.Success = True
            Catch ex As Exception
                result.Success = False
                result.Errors.Add("Listeleme işlemi sırasında bir hata oluştu.")
            End Try

            Return result
        End Function

        Public Async Function GetSearchAuthor(search As SearchVM) As Task(Of Result(Of List(Of PublisherDTO))) Implements IPublisherService.GetSearchPublisher
            Dim result = New Result(Of List(Of PublisherDTO))

            Try
                Dim publisherEntities As IQueryable(Of Publisher)
                publisherEntities = _context.Publishers.Where(Function(x) x.Name.ToUpper.Trim.Contains(search.SearchText.ToUpper.Trim))


                result.Data = publisherEntities.Select(Function(publisher) New PublisherDTO With {
                    .Id = publisher.Id,
                    .Name = publisher.Name,
                    .Description = publisher.Description
                }).ToList()


                result.Success = True
            Catch ex As Exception
                result.Success = False
                result.Errors.Add("Arama işlemi sırasında bir hata oluştu.")
            End Try
            _unitOfWork.Dispose()
            Return result
        End Function


        Public Async Function GetPublisherById(publisherId As Long) As Task(Of Result(Of PublisherDTO)) Implements IPublisherService.GetPublisherById
            Dim publisherRepository = _unitOfWork.GetRepository(Of Publisher)()
            Dim result As New Result(Of PublisherDTO)

            Try
                Dim publisher = Await publisherRepository.GetById(publisherId)

                If publisher IsNot Nothing Then
                    result.Data = New PublisherDTO With {
                        .Id = publisher.Id,
                        .Name = publisher.Name,
                        .Description = publisher.Description
                    }
                    result.Success = True
                Else
                    result.Errors.Add("Yayınevi bulunamadı.")
                    result.Success = False
                End If
            Catch ex As Exception
                result.Success = False
                result.Errors.Add("Arama işlemi sırasında bir hata oluştu.")
            End Try

            Return result
        End Function

        Public Function AddPublisher(addPublisherVM As AddPublisherVM) As Result(Of Integer) Implements IPublisherService.AddPublisher
            Dim publisherRepository = _unitOfWork.GetRepository(Of Publisher)()
            Dim result As New Result(Of Integer)

            Try
                Dim newPublisher As New Publisher With {
                    .Name = addPublisherVM.Name,
                    .Description = addPublisherVM.Description
                }

                publisherRepository.Add(newPublisher)
                _unitOfWork.CommitAsync().Wait()
                result.Success = True
            Catch ex As Exception
                result.Success = False
                result.Errors.Add("Ekleme işlemi sırasında bir hata oluştu.")
            End Try

            Return result
        End Function

        Public Function UpdatePublisher(updatePublisherVM As UpdateVM) As Result(Of Integer) Implements IPublisherService.UpdatePublisher
            Dim publisherRepository = _unitOfWork.GetRepository(Of Publisher)()
            Dim result As New Result(Of Integer)

            Try
                Dim existingPublisher = publisherRepository.GetById(updatePublisherVM.Id).Result

                If existingPublisher IsNot Nothing Then
                    existingPublisher.Name = updatePublisherVM.Name
                    existingPublisher.Description = updatePublisherVM.Description

                    publisherRepository.Update(existingPublisher)
                    _unitOfWork.CommitAsync().Wait()
                    result.Success = True
                Else
                    result.Errors.Add("Yayınevi bulunamadı.")
                    result.Success = False
                End If
            Catch ex As Exception
                result.Success = False
                result.Errors.Add("Güncelleme işlemi sırasında bir hata oluştu.")
            End Try

            Return result
        End Function
        Private Function DeletePublisher(deleteVM As Long) As Result(Of Boolean) Implements IPublisherService.DeletePublisher
            Dim publisherRepository = _unitOfWork.GetRepository(Of Publisher)()
            Dim result As New Result(Of Boolean)
            Try
                Dim publisherToDelete = publisherRepository.AnyAsync(Function(x) x.Id = deleteVM).Result

                If Not publisherToDelete Then
                    result.Errors.Add("Yayınevi bulunamadı.")
                    result.Success = False

                End If

                publisherRepository.Delete(deleteVM)
                _unitOfWork.CommitAsync().Wait
                result.Data = deleteVM
                _unitOfWork.Dispose()

            Catch ex As Exception
                result.Success = False
                result.Errors.Add("Silme işlemi sırasında bir hata oluştu.")
            End Try
            Return result
        End Function
    End Class
End Namespace