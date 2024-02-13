Imports Library.UI.Services.Implementation

Public Class AddCategory
    Inherits Window


    Public Sub New()
        InitializeComponent()
        Me.DataContext = New CategoryServices
    End Sub

    Private Sub windowClose_Click(sender As Object, e As MouseButtonEventArgs)
        Me.Close()
    End Sub

    Private Async Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Await Task.Delay(1000)
        Me.Close()
    End Sub
End Class
