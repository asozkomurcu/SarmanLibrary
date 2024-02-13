Imports Library.UI.Models.DTOs
Imports Library.UI.Models.RequestModels.BookVMs
Imports Library.UI.Services.Implementation

Public Class Categories
    Inherits UserControl

    Public Sub New()
        InitializeComponent()
        DataContext = New CategoryServices
    End Sub

    Private Sub lblCategoryBookList_PreviewMouseDown(sender As Object, e As MouseButtonEventArgs)

        If TypeOf sender Is Label Then

            Dim label As Label = DirectCast(sender, Label)
            Dim selectedItem As CategoryDTO = DirectCast(label.DataContext, CategoryDTO)

            If selectedItem IsNot Nothing Then
                Dim filter As New FilterBookVM
                filter.FilterId = selectedItem.Id
                filter.FilterDto = "CategoryDto"

                Dim mainWindow As MainWindow = TryCast(Window.GetWindow(Me), MainWindow)
                If mainWindow IsNot Nothing Then
                    mainWindow.TransmissionFilterBook(filter)

                End If
            End If
        End If
    End Sub

    Private Async Sub DeleteCategory_PreviewMouseDown(sender As Object, e As MouseButtonEventArgs)

        If TypeOf sender Is Label Then

            Dim label As Label = DirectCast(sender, Label)
            Dim selectedItem As CategoryDTO = DirectCast(label.DataContext, CategoryDTO)
            If selectedItem IsNot Nothing Then
                Dim filter As New FilterBookVM
                filter.FilterId = selectedItem.Id
                filter.FilterDto = "CategoryDTO"
                Dim delete = New DeletedService(filter)
                Dim refresh As New RefreshService
                refresh.Refresh("TransmissionCategory")
            End If
        End If
    End Sub

    Private Sub UpdateCategory_Click(sender As Object, e As MouseButtonEventArgs)
        Dim requestModel = New CategoryDTO()

        Dim selectedItems As CategoryDTO = lstCategory.SelectedItem

        If selectedItems IsNot Nothing Then
            Dim add As New UpdateCategory(selectedItems)
            add.ShowDialog()
        End If

    End Sub

    Private Sub addCategory_Click(sender As Object, e As MouseButtonEventArgs)
        Dim add As New AddCategory()
        add.ShowDialog()
    End Sub

    Private Sub lblUpdateCategory_PreviewMouseDown(sender As Object, e As MouseButtonEventArgs)
        Dim requestModel = New CategoryDTO()

        Dim label As Label = DirectCast(sender, Label)
        Dim selectedItem As CategoryDTO = DirectCast(label.DataContext, CategoryDTO)

        If selectedItem IsNot Nothing Then
            Dim add As New UpdateCategory(selectedItem)
            add.ShowDialog()
        End If
    End Sub
End Class
