Imports System.Collections.ObjectModel
Imports Library.UI.Models.DTOs
Imports Library.UI.Models.Wrapper
Imports Library.UI.Services.Interfaces
Imports Newtonsoft.Json

Namespace Services.Implementation
    Public Class BookServices
        Inherits ViewModelBase


        Dim restService As IRestService


        Public Property AllBooksList As New ObservableCollection(Of BookDTO)
        Public Property AllBooksCount As New ObservableCollection(Of Integer)

        Public Sub New()

            Dim unused1 = AllBooksAsync()
            Dim unused2 = AllBooksCountAsync()
        End Sub




        Private Async Function AllBooksAsync() As Task
            If SelectedItem Is Nothing Then
                Dim bookList = Await GetAllBooks()
                AllBooksList.Clear()
                For Each books In bookList
                    AllBooksList.Add(books)
                Next
            End If
        End Function

        Private Async Function AllBooksCountAsync() As Task
            Dim bookList = Await GetAllBooks()
            Dim bookCount = bookList.Count
            AllBooksCount.Clear()
            AllBooksCount.Add(bookCount)
        End Function

        Public Property SelectedItem() As AuthorDTO



        Public Async Function GetAllBooks() As Task(Of IEnumerable(Of BookDTO))
            restService = New RestService

            Dim responseMessage = Await restService.GetAsync(Of BookDTO)("book/all")

            If responseMessage.IsSuccessful Then
                Dim response = JsonConvert.DeserializeObject(Of Result(Of List(Of BookDTO)))(responseMessage.Content)
                Return response.Data
            Else
                ' Hata durumunu ele al
                Return Enumerable.Empty(Of BookDTO)()
            End If

        End Function


    End Class
End Namespace