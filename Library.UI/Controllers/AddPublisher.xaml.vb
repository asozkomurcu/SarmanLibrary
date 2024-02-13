Imports Library.UI.Services.Implementation

Public Class AddPublisher
    Inherits Window


    Public Sub New()
        InitializeComponent()
        Me.DataContext = New PublisherServices
    End Sub

    Private Sub windowClose_Click(sender As Object, e As MouseButtonEventArgs)
        Me.Close()

    End Sub

    Private Async Sub addPublisher_Click(sender As Object, e As MouseButtonEventArgs)
        Await Task.Delay(1000)
        Me.Close()
    End Sub
End Class
