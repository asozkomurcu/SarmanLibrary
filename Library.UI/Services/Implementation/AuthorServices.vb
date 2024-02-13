Imports System.Collections.ObjectModel
Imports Library.UI.Models.DTOs
Imports Library.UI.Models.RequestModels.AuthorVMs
Imports Library.UI.Models.Wrapper
Imports Library.UI.Services.Interfaces
Imports Newtonsoft.Json

Namespace Services.Implementation
    Public Class AuthorServices
        Inherits ViewModelBase


        Dim restService As IRestService


        Public Property AllAuthorsList As New ObservableCollection(Of AuthorDTO)


        Public Sub New()

            Dim unused1 = AllAuthorsAsync()

        End Sub

        Private Async Function AllAuthorsAsync() As Task
            Dim authorsList = Await GetAllAuthors()
            AllAuthorsList.Clear()
            For Each authors In authorsList
                AllAuthorsList.Add(authors)
            Next
        End Function



        Public Async Function AddAuthorAsync(model) As Task
            Dim addAuthorItem = Await AddAuthor(model)

        End Function

        Public Async Function GetAllAuthors() As Task(Of IEnumerable(Of AuthorDTO))
            restService = New RestService
            Dim responseMessage = Await restService.GetAsync(Of AuthorDTO)("author/all")

            If responseMessage.IsSuccessful Then
                Dim response = JsonConvert.DeserializeObject(Of Result(Of List(Of AuthorDTO)))(responseMessage.Content)
                Return response.Data
            Else
                ' Hata durumunu ele al
                Return Enumerable.Empty(Of AuthorDTO)()
            End If
        End Function



        Public Async Function AddAuthor(model) As Task(Of IEnumerable(Of AddAuthorVM))

            If model IsNot Nothing Then
                Dim responseMessage = Await restService.PostAsync(Of AddAuthorVM, Result(Of Integer))(model, "author/add")
                Dim response = JsonConvert.DeserializeObject(Of Result(Of Integer))(responseMessage.Content)
                If response.Errors.Count = 0 And response.Success = True Then


                    MessageBox.Show("Kayıt işlemi başarılı.", "Başarılı", vbOKOnly, vbInformation)


                Else
                    ' Hata durumunu ele al
                    Return Enumerable.Empty(Of AddAuthorVM)()
                    MessageBox.Show("Kayıt işlemi başarısız.", "Hata", vbOKOnly, MessageBoxImage.Error)

                End If
            End If

        End Function


    End Class


End Namespace