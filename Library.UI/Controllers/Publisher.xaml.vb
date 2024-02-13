Imports Library.UI.Models.DTOs
Imports Library.UI.Models.RequestModels.BookVMs
Imports Library.UI.Services.Implementation

Public Class Publisher
    Inherits UserControl


    Public Sub New()
        InitializeComponent()
        DataContext = New PublisherServices()
    End Sub

    Private Sub addPublisher_Click(sender As Object, e As RoutedEventArgs)
        Dim add As New AddPublisher()
        add.ShowDialog()
    End Sub


    Private Sub updatePublisher_PreviewMouseDown(sender As Object, e As MouseButtonEventArgs)
        Dim requestModel = New PublisherDTO()

        Dim label As Label = DirectCast(sender, Label)
        Dim selectedItem As PublisherDTO = DirectCast(label.DataContext, PublisherDTO)

        If selectedItem IsNot Nothing Then
            Dim add As New UpdatePublisher(selectedItem)
            add.ShowDialog()
        End If
    End Sub

    Private Async Sub DeletePublisher_PreviewMouseDown(sender As Object, e As MouseButtonEventArgs)

        If TypeOf sender Is Label Then

            Dim label As Label = DirectCast(sender, Label)
            Dim selectedItem As PublisherDTO = DirectCast(label.DataContext, PublisherDTO)
            If selectedItem IsNot Nothing Then

                Dim filter As New FilterBookVM
                filter.FilterId = selectedItem.Id
                filter.FilterDto = "PublisherDTO"
                Dim delete = New DeletedService(filter)
                Dim refresh As New RefreshService
                refresh.Refresh("TransmissionPublisher")
            End If
        End If
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

    Private Sub addPublisher_Click(sender As Object, e As MouseButtonEventArgs)
        Dim add As New AddPublisher()
        add.ShowDialog()
    End Sub
End Class
