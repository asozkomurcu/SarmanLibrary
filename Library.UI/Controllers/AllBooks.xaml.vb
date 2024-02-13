Imports Library.UI.Models.DTOs
Imports Library.UI.Models.RequestModels.BookVMs
Imports Library.UI.Services.Abstraction
Imports Library.UI.Services.Implementation
Imports Library.UI.Services.Implementation.BooksServices
Imports Library.UI.Services.Interfaces

Partial Public Class AllBooks
    Inherits UserControl


    Dim restService As IRestService


    Public Sub New()
        InitializeComponent()
        DataContext = New BookServices()
    End Sub

    Private Sub btn_AddBook_Click(sender As Object, e As MouseButtonEventArgs)
        Dim add As New addBook()
        add.ShowDialog()
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

                Dim refresh As New RefreshService
                refresh.Refresh("TransmissionAllBook")


            End If
        End If
    End Sub

    Private Sub btnUpdateBook_PreviewMouseDown(sender As Object, e As MouseButtonEventArgs)
        Dim requestModel = New BookDTO()


        Dim selectedItem As BookDTO = dtGridTeacher.SelectedItem


        If selectedItem IsNot Nothing Then
            Dim add As New UpdateBook(selectedItem)
            add.ShowDialog()
        End If
    End Sub
End Class

