Imports Library.UI.Models
Imports Library.UI.Models.RequestModels
Imports Library.UI.Models.RequestModels.BookVMs
Imports System.Security.Policy
Imports System.Windows.Media.Animation

Public Class MainWindow
    Inherits Window

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub btn_minimalize_Click(sender As Object, e As RoutedEventArgs)
        WindowState = WindowState.Minimized
        SearchHidden()
    End Sub

    Private Sub btn_fullScreen_Click(sender As Object, e As RoutedEventArgs)
        If WindowState = WindowState.Maximized Then
            WindowState = WindowState.Normal
        Else
            WindowState = WindowState.Maximized
        End If
        SearchHidden()
    End Sub


    Private Sub btn_kapat_Click(sender As Object, e As RoutedEventArgs)
        Me.Close()
    End Sub

    Private Sub brdTopRight_MouseDown(sender As Object, e As MouseButtonEventArgs) Handles brdTopRight.MouseDown
        Me.DragMove()
        SearchHidden()
    End Sub
    Private Sub sideBar_AllBooks_Click(sender As Object, e As RoutedEventArgs)
        ucCall.Uc_Add(MWContent, New AllBooks())

        SearchHidden()
    End Sub
    Private Sub sideBar_Authors_Click(sender As Object, e As RoutedEventArgs)
        ucCall.Uc_Add(MWContent, New Authors())
        SearchHidden()
    End Sub

    Private Sub sideBar_Categories_Click(sender As Object, e As RoutedEventArgs)
        ucCall.Uc_Add(MWContent, New Categories())
        SearchHidden()
    End Sub

    Private Sub sideBar_Publishers_Click(sender As Object, e As RoutedEventArgs)
        ucCall.Uc_Add(MWContent, New Publisher())
        SearchHidden()
    End Sub



    Public Sub TransmissionFilterBook(filterBook)
        ucCall.Uc_Add(MWContent, New FilterBookUserControl(filterBook))
        SearchHidden()
    End Sub


    Public Sub TransmissionFilterAuthor(filterBook)
        ucCall.Uc_Add(MWContent, New FilterAuthorUserControl(filterBook))
        SearchHidden()
    End Sub
    Public Sub TransmissionFilterPublisher(filterBook)
        ucCall.Uc_Add(MWContent, New FilterPublisherUserControl(filterBook))
        SearchHidden()
    End Sub


    Public Sub TransmissionAuthors()
        ucCall.Uc_Add(MWContent, New Authors())
        SearchHidden()
    End Sub
    Public Sub TransmissionCategory()
        ucCall.Uc_Add(MWContent, New Categories())
        SearchHidden()
    End Sub

    Public Sub TransmissionPublisher()
        ucCall.Uc_Add(MWContent, New Publisher())
        SearchHidden()
    End Sub
    Public Sub TransmissionAllBook()
        ucCall.Uc_Add(MWContent, New AllBooks())
    End Sub

    Private Sub ReloadMainWindow()
        Dim newMainWindow As New MainWindow()
        Application.Current.MainWindow = newMainWindow
        newMainWindow.Show()
        Close()
    End Sub

    Private Sub searchMain_PreviewMouseDown(sender As Object, e As MouseButtonEventArgs)

        Dim elementModels As New List(Of String) From {"Kitap Ara", "Yazar Ara", "Yayınevi Ara"}
        If cbxSearch.Items.Count = 0 Then
            For Each model As String In elementModels
                cbxSearch.Items.Add(model)
            Next
        End If


        searchMain.Visibility = Visibility.Visible
        cbxSearch.Visibility = Visibility.Visible

        Dim animationSearchMain As New DoubleAnimation()
        animationSearchMain.To = 300
        animationSearchMain.Duration = New Duration(TimeSpan.FromSeconds(1.3))
        searchMain.BeginAnimation(WidthProperty, animationSearchMain)

        Dim animationCbxSearch As New DoubleAnimation()
        animationCbxSearch.To = 120
        animationCbxSearch.Duration = New Duration(TimeSpan.FromSeconds(0.6))
        animationCbxSearch.BeginTime = TimeSpan.FromSeconds(1.31)
        cbxSearch.BeginAnimation(WidthProperty, animationCbxSearch)



        Dim opacityAnimation As New DoubleAnimation()
        opacityAnimation.To = 0
        opacityAnimation.Duration = New Duration(TimeSpan.FromSeconds(1.3))
        lblSearchMain.BeginAnimation(OpacityProperty, opacityAnimation)

        If searchMain.Text <> "" AndAlso cbxSearch.SelectedIndex > -1 Then
            Dim search As New SearchVM
            search.SearchText = searchMain.Text
            search.SearchEntity = cbxSearch.SelectedIndex
            Dim mainWindow As MainWindow = TryCast(Window.GetWindow(Me), MainWindow)
            If mainWindow IsNot Nothing Then
                If cbxSearch.SelectedIndex = 0 Then
                    mainWindow.TransmissionFilterBook(search)
                ElseIf cbxSearch.SelectedIndex = 1 Then
                    mainWindow.TransmissionFilterAuthor(search)
                ElseIf cbxSearch.SelectedIndex = 2 Then
                    mainWindow.TransmissionFilterPublisher(search)
                End If


            End If
        End If
    End Sub

    Private Function SearchHidden()
        searchMain.Text = ""

        Dim animationSearchMain As New DoubleAnimation()
        animationSearchMain.To = 0
        animationSearchMain.Duration = New Duration(TimeSpan.FromSeconds(1.3))
        animationSearchMain.BeginTime = TimeSpan.FromSeconds(0.6)
        searchMain.BeginAnimation(WidthProperty, animationSearchMain)

        Dim animationCbxSearch As New DoubleAnimation()
        animationCbxSearch.To = 0
        animationCbxSearch.Duration = New Duration(TimeSpan.FromSeconds(0.5))

        cbxSearch.BeginAnimation(WidthProperty, animationCbxSearch)

        Dim opacityAnimation As New DoubleAnimation()
        opacityAnimation.To = 0.6
        opacityAnimation.Duration = New Duration(TimeSpan.FromSeconds(1.3))
        lblSearchMain.BeginAnimation(OpacityProperty, opacityAnimation)


    End Function

    Private Sub searchMain_PreviewKeyDown(sender As Object, e As KeyEventArgs)
        If e.Key = Key.Enter Then

            PerformSearch()
        End If
    End Sub

    Private Sub PerformSearch()

        Dim search As New SearchVM
        search.SearchText = searchMain.Text
        search.SearchEntity = cbxSearch.SelectedIndex
        Dim mainWindow As MainWindow = TryCast(Window.GetWindow(Me), MainWindow)
        If mainWindow IsNot Nothing Then
            If cbxSearch.SelectedIndex = 0 Then
                mainWindow.TransmissionFilterBook(search)
            ElseIf cbxSearch.SelectedIndex = 1 Then
                mainWindow.TransmissionFilterAuthor(search)
            ElseIf cbxSearch.SelectedIndex = 2 Then
                mainWindow.TransmissionFilterPublisher(search)
            End If
        End If
    End Sub

End Class