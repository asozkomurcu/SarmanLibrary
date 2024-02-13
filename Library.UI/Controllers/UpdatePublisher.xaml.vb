
Imports Library.UI.Models.RequestModels
Imports Library.UI.Services.Implementation

Public Class UpdatePublisher
    Inherits Window

    Public Sub New(selectedItems)
        InitializeComponent()
        Me.DataContext = selectedItems


    End Sub


    Private Sub CloseWindow_Click(sender As Object, e As MouseButtonEventArgs)
        Me.Close()
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As MouseButtonEventArgs)

        Dim requestModel = New UpdateVM()
        If tbxSelectedModelPublisherName.Text = "" OrElse tbxSelectedModelPublisherDescription.Text = "" Then
            MessageBox.Show("Yayınevi adı ve açıklaması boş bırakılamaz.", "Hata.", vbOKOnly, MessageBoxImage.Error)
        Else

            requestModel.Id = Convert.ToInt64(lblSelectedModelId.Content)
            requestModel.Name = tbxSelectedModelPublisherName.Text
            requestModel.Description = tbxSelectedModelPublisherDescription.Text
            requestModel.FilterDto = "PublisherDto"

            Dim update = New UpdateService(requestModel)
        End If
        Dim refresh As New RefreshService
        refresh.Refresh("TransmissionPublisher")
        Me.Close()
    End Sub

End Class
