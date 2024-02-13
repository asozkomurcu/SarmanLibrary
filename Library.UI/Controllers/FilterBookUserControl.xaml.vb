Imports Library.UI.Models.DTOs
Imports Library.UI.Models.RequestModels.BookVMs
Imports Library.UI.Services.Abstraction
Imports Library.UI.Services.Implementation
Imports Library.UI.Services.Implementation.BooksServices
Imports Library.UI.Services.Interfaces

Public Class FilterBookUserControl
    Inherits UserControl

    Dim restService As IRestService
    Public Sub New(filterBook)
        InitializeComponent()
        DataContext = New FilterbookService(filterBook)

    End Sub


    Private Async Sub DeleteBook_PreviewMouseDown(sender As Object, e As MouseButtonEventArgs)
        If TypeOf sender Is Label Then

            Dim label As Label = DirectCast(sender, Label)
            Dim selectedItem As BookDTO = DirectCast(label.DataContext, BookDTO)

            If selectedItem IsNot Nothing Then
                Dim filter As New FilterBookVM
                filter.FilterId = selectedItem.Id
                filter.FilterDto = "BookDTO"

                Dim delete = New DeletedService(filter)

                Dim filterAuthor As New FilterBookVM
                filterAuthor.FilterId = selectedItem.AuthorId
                filterAuthor.FilterDto = "AuthorDto"

                Dim mainWindow As MainWindow = TryCast(Window.GetWindow(Me), MainWindow)
                If mainWindow IsNot Nothing Then
                    mainWindow.TransmissionFilterBook(filterAuthor)

                End If

            End If
        End If
    End Sub

End Class
