Public Class RefreshService
    Public Sub Refresh(methodName As String)
        Dim newMainWindow As New MainWindow()
        If Application.Current.MainWindow IsNot Nothing Then
            AddHandler Application.Current.MainWindow.Closed, Sub(sender, e) MainWindow_Closed(sender, e, methodName)
            Application.Current.MainWindow.Close()
        Else
            ShowNewMainWindow(newMainWindow, methodName)
        End If
    End Sub
    Sub MainWindow_Closed(sender As Object, e As EventArgs, Transmission As Object)
        Dim newMainWindow As New MainWindow()
        ShowNewMainWindow(newMainWindow, Transmission)
    End Sub

    Sub ShowNewMainWindow(newMainWindow As MainWindow, methodName As String)

        Application.Current.MainWindow = newMainWindow
        newMainWindow.Show()

        ' Metod adını belirtilen isimle çağır
        Dim method = newMainWindow.GetType().GetMethod(methodName)
        If method IsNot Nothing Then
            method.Invoke(newMainWindow, Nothing)
        Else
            MessageBox.Show("Metod bulunamadı: " & methodName)
        End If
    End Sub



End Class
