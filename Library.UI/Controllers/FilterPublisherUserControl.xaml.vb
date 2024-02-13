Imports Library.UI.Models.DTOs
Imports Library.UI.Models.RequestModels.BookVMs
Imports Library.UI.Services.Abstraction
Imports Library.UI.Services.Implementation
Imports Library.UI.Services.Implementation.BooksServices
Imports Library.UI.Services.Interfaces

Public Class FilterPublisherUserControl
    Inherits UserControl

    Dim restService As IRestService
    Public Sub New(filterBook)
        InitializeComponent()
        Me.DataContext = New FilterbookService(filterBook)
    End Sub


    Private Sub lblPublisherBookList_PreviewMouseDown(sender As Object, e As MouseButtonEventArgs)
        If TypeOf sender Is Label Then

            Dim label As Label = DirectCast(sender, Label)
            Dim selectedItem As PublisherDTO = DirectCast(label.DataContext, PublisherDTO)

            If selectedItem IsNot Nothing Then
                Dim filter As New FilterBookVM
                filter.FilterId = selectedItem.Id
                filter.FilterDto = "PublisherDto"

                Dim mainWindow As MainWindow = TryCast(Window.GetWindow(Me), MainWindow)
                If mainWindow IsNot Nothing Then
                    mainWindow.TransmissionFilterBook(filter)

                End If
            End If
        End If
    End Sub


    Private Sub DeletePublisher_PreviewMouseDown(sender As Object, e As MouseButtonEventArgs)
        If TypeOf sender Is Label Then

            Dim label As Label = DirectCast(sender, Label)
            Dim selectedItem As PublisherDTO = DirectCast(label.DataContext, PublisherDTO)

            If selectedItem IsNot Nothing Then
                Dim filter As New FilterBookVM
                filter.FilterId = selectedItem.Id
                filter.FilterDto = "PublisherDTO"

                Dim delete = New DeletedService(filter)

                Dim filterAuthor As New FilterBookVM
                filterAuthor.FilterId = selectedItem.Id
                filterAuthor.FilterDto = "PublisherDto"

                Dim mainWindow As MainWindow = TryCast(Window.GetWindow(Me), MainWindow)
                If mainWindow IsNot Nothing Then
                    mainWindow.TransmissionFilterBook(filterAuthor)

                End If

            End If
        End If
    End Sub

    Private Sub updatePublisher_PreviewMouseDown(sender As Object, e As MouseButtonEventArgs)

    End Sub
End Class
