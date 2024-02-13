Imports Library.UI.Models.DTOs
Imports Library.UI.Models.RequestModels.BookVMs
Imports Library.UI.Services.Implementation
Imports Library.UI.Services.Implementation.BooksServices

Public Class FilterAuthorUserControl
    Inherits UserControl

    Public Sub New(filterBook)
        InitializeComponent()
        Me.DataContext = New FilterbookService(filterBook)
    End Sub


    Private Sub lblAuthorBookList_PreviewMouseDown(sender As Object, e As MouseButtonEventArgs)
        If TypeOf sender Is Label Then

            Dim label As Label = DirectCast(sender, Label)
            Dim selectedItem As AuthorDTO = DirectCast(label.DataContext, AuthorDTO)

            If selectedItem IsNot Nothing Then
                Dim filter As New FilterBookVM
                filter.FilterId = selectedItem.Id
                filter.FilterDto = "AuthorDto"

                Dim mainWindow As MainWindow = TryCast(Window.GetWindow(Me), MainWindow)
                If mainWindow IsNot Nothing Then
                    mainWindow.TransmissionFilterBook(filter)

                End If
            End If
        End If
    End Sub

    Private Async Sub DeleteAuthor_PreviewMouseDown(sender As Object, e As MouseButtonEventArgs)
        If TypeOf sender Is Label Then

            Dim label As Label = DirectCast(sender, Label)
            Dim selectedItem As AuthorDTO = DirectCast(label.DataContext, AuthorDTO)

            If selectedItem IsNot Nothing Then
                Dim filter As New FilterBookVM
                filter.FilterId = selectedItem.Id
                filter.FilterDto = "AuthorDTO"

                Dim delete = New DeletedService(filter)

                Dim filterAuthor As New FilterBookVM
                filterAuthor.FilterId = selectedItem.Id
                filterAuthor.FilterDto = "AuthorDto"

                Dim mainWindow As MainWindow = TryCast(Window.GetWindow(Me), MainWindow)
                If mainWindow IsNot Nothing Then
                    mainWindow.TransmissionFilterBook(filterAuthor)

                End If

            End If
        End If
    End Sub
End Class
