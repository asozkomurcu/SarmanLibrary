Imports Library.UI.Models.DTOs
Imports Library.UI.Models.RequestModels.BookVMs
Imports Library.UI.Services.Implementation

Public Class Authors
    Inherits UserControl

    Public Sub New()
        InitializeComponent()

        Me.DataContext = New AuthorServices()

    End Sub

    Private Sub AddAuthor_Click(sender As Object, e As RoutedEventArgs)
        Dim add As New addAuthor()
        add.ShowDialog()
    End Sub

    Private Sub AddAuthor_Click(sender As Object, e As MouseButtonEventArgs)
        Dim add As New addAuthor()
        add.ShowDialog()
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

                Dim refresh As New RefreshService
                refresh.Refresh("TransmissionAuthors")


            End If

        End If


    End Sub


    Private Async Sub lblAuthorBookList_PreviewMouseDown(sender As Object, e As MouseButtonEventArgs)

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

    Private Sub btnUpdateAuthor_PreviewMouseDown(sender As Object, e As MouseButtonEventArgs)

        Dim requestModel = New AuthorDTO()

        Dim selectedItem As AuthorDTO = lstAuthors.SelectedItem

        If selectedItem IsNot Nothing Then
            Dim add As New UpdateAuthor(selectedItem)
            add.ShowDialog()
        End If
    End Sub

End Class
