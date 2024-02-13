Imports System.Collections.ObjectModel
Imports System.Net
Imports Library.UI.Models.DTOs
Imports Library.UI.Models.RequestModels.BookVMs
Imports Library.UI.Models.Wrapper
Imports Library.UI.Services.Interfaces
Imports Newtonsoft.Json
Imports RestSharp

Namespace Services.Implementation
    Public Class FilterbookService
        Inherits ViewModelBase


        Dim restService As IRestService
        Public Property FilterBooksList() As New ObservableCollection(Of BookDTO)
        Public Property FilterAuthorsList() As New ObservableCollection(Of AuthorDTO)
        Public Property FilterPublishersList() As New ObservableCollection(Of PublisherDTO)
        Public Property AllBooksList As New ObservableCollection(Of BookDTO)


        Public Sub New(filter)

            Dim unused = AllAuthorBooksAsync(filter)
            Dim unused1 = AllBooksAsync(filter)

        End Sub


        Private Async Function AllAuthorBooksAsync(filter As Object) As Task
            If filter IsNot Nothing Then
                Dim bookList = Await GetFilterBook(filter)
                FilterBooksList.Clear()
                For Each books In bookList
                    FilterBooksList.Add(books)
                Next


            End If
        End Function
        Private Async Function AllBooksAsync(searchModel As Object) As Task
            If searchModel IsNot Nothing Then

                If searchModel.SearchEntity = 0 Then
                    Dim bookList = Await GetAllBooks(searchModel)
                    FilterBooksList.Clear()
                    For Each books In bookList
                        FilterBooksList.Add(books)
                    Next
                ElseIf searchModel.SearchEntity = 1 Then
                    Dim authorList = Await GetAllAuthors(searchModel)
                    FilterAuthorsList.Clear()
                    For Each authors In authorList
                        FilterAuthorsList.Add(authors)
                    Next
                ElseIf searchModel.SearchEntity = 2 Then
                    Dim publisherList = Await GetAllPublishers(searchModel)
                    FilterAuthorsList.Clear()
                    For Each publishers In publisherList
                        FilterPublishersList.Add(publishers)
                    Next
                End If
            End If
        End Function

        Public Async Function GetFilterBook(filter As FilterBookVM) As Task(Of IEnumerable(Of BookDTO))
            restService = New RestService
            If filter IsNot Nothing Then
                Dim endpoint As String = Nothing
                If filter.FilterDto = "AuthorDto" Then
                    endpoint = $"book/author/{filter.FilterId}"
                ElseIf filter.FilterDto = "CategoryDto" Then
                    endpoint = $"book/category/{filter.FilterId}"
                ElseIf filter.FilterDto = "PublisherDto" Then
                    endpoint = $"book/publisher/{filter.FilterId}"
                End If

                Dim responseMessage = Await restService.GetAsync(Of BookDTO)(endpoint)

                If responseMessage.StatusCode = HttpStatusCode.OK And responseMessage.ErrorMessage = Nothing Then
                    Dim response = JsonConvert.DeserializeObject(Of Result(Of List(Of BookDTO)))(responseMessage.Content)


                    Return response.Data

                Else
                    ' Hata durumunu ele al
                    Return Enumerable.Empty(Of BookDTO)()
                End If
            End If
        End Function

        Public Async Function GetAllBooks(searchModel As SearchVM) As Task(Of IEnumerable(Of BookDTO))
            restService = New RestService
            Dim responseMessage = New RestResponse

            If searchModel.SearchText <> "" Then
                responseMessage = Await restService.PostAsync(Of SearchVM, BookDTO)(searchModel, "book/searchBook")
            Else
                responseMessage = Await restService.GetAsync(Of BookDTO)("book/all")
            End If

            If responseMessage.IsSuccessful Then
                Dim response = JsonConvert.DeserializeObject(Of Result(Of List(Of BookDTO)))(responseMessage.Content)
                Return response.Data
            Else
                ' Hata durumunu ele al
                Return Enumerable.Empty(Of BookDTO)()
            End If

        End Function

        Public Async Function GetAllAuthors(searchModel As SearchVM) As Task(Of IEnumerable(Of AuthorDTO))
            restService = New RestService
            Dim responseMessage = New RestResponse

            If searchModel.SearchText <> "" Then
                responseMessage = Await restService.PostAsync(Of SearchVM, AuthorDTO)(searchModel, "author/searchAuthor")
            Else
                responseMessage = Await restService.GetAsync(Of AuthorDTO)("author/all")
            End If

            If responseMessage.IsSuccessful Then
                Dim response = JsonConvert.DeserializeObject(Of Result(Of List(Of AuthorDTO)))(responseMessage.Content)
                Return response.Data
            Else
                ' Hata durumunu ele al
                Return Enumerable.Empty(Of BookDTO)()
            End If

        End Function

        Public Async Function GetAllPublishers(searchModel As SearchVM) As Task(Of IEnumerable(Of PublisherDTO))
            restService = New RestService
            Dim responseMessage = New RestResponse

            If searchModel.SearchText <> "" Then
                responseMessage = Await restService.PostAsync(Of SearchVM, PublisherDTO)(searchModel, "publisher/searchPublisher")
            Else
                responseMessage = Await restService.GetAsync(Of PublisherDTO)("publisher/all")
            End If

            If responseMessage.IsSuccessful Then
                Dim response = JsonConvert.DeserializeObject(Of Result(Of List(Of PublisherDTO)))(responseMessage.Content)
                Return response.Data
            Else
                ' Hata durumunu ele al
                Return Enumerable.Empty(Of PublisherDTO)()
            End If

        End Function

    End Class
End Namespace