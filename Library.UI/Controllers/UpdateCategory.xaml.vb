

Imports Library.UI.Models.RequestModels
Imports Library.UI.Services.Implementation

Public Class UpdateCategory
    Inherits Window


    Public Sub New(selectedItems)
        InitializeComponent()
        Me.DataContext = selectedItems


    End Sub


    Private Sub CloseWindow_Click(sender As Object, e As MouseButtonEventArgs)
        Me.Close()

    End Sub

    Private Async Sub btnUpdate_Click(sender As Object, e As RoutedEventArgs)

        Dim requestModel = New UpdateVM()
        requestModel.Id = Convert.ToInt64(tbxSelectedModelId.Text)
        requestModel.Name = tbxSelectedModelCategoryName.Text
        requestModel.FilterDto = "CategoryDto"

        Dim update = New UpdateService(requestModel)

        Dim refresh As New RefreshService
        refresh.Refresh("TransmissionCategory")
        Me.Close()
    End Sub

End Class
