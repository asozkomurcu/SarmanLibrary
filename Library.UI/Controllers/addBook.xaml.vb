
Imports Library.UI.Models.DTOs
Imports Library.UI.Models.RequestModels.BookVMs
Imports Library.UI.Models.RequestModels.YearVMs
Imports Library.UI.Services.Implementation
Imports Library.UI.Services.Interfaces
Public Class addBook
    Inherits Window

    Dim restService As IRestService

    Public Sub New()
        InitializeComponent()
        Me.DataContext = New AddBookServices()

    End Sub

    Private Sub DraggMove(sender As Object, e As MouseButtonEventArgs) Handles addBookMove.MouseDown
        If (Mouse.LeftButton = MouseButtonState.Pressed) Then
            Me.DragMove()
        End If
    End Sub


    Private Sub TextBox_PreviewTextInput(sender As Object, e As TextCompositionEventArgs)
        e.Handled = Not IsTextAllowed(e.Text)
    End Sub

    Private Function IsTextAllowed(text As String) As Boolean
        Dim result As Integer
        Return Integer.TryParse(text, result)
    End Function

    Private Sub closeWindow_MouseDown(sender As Object, e As MouseButtonEventArgs)
        Me.Close()

    End Sub

    Private Sub RadioButton_Checked(sender As Object, e As RoutedEventArgs)

        Dim selectedRadioButton As RadioButton = TryCast(sender, RadioButton)

        Dim yearService As New YearServices
        Dim elementModels As New List(Of String)
        elementModels = yearService.RadioButton(selectedRadioButton)

        Dim century As Integer = DateTime.Now.Year / 100 + 1

        cbxMOMS.Items.Clear()

        If elementModels.Count < 12 Then
            cbxMOMS.Visibility = Visibility.Visible
            For index = 0 To elementModels.Count - 1
                cbxMOMS.Items.Add(elementModels(index))
            Next
        Else
            cbxMOMS.Visibility = Visibility.Visible
            For index = 0 To century
                cbxMOMS.Items.Add(elementModels(index))
            Next
        End If
        cbxMOMS.SelectedIndex = 0
        lblRadioButtonValue.Content = selectedRadioButton.Name
    End Sub


    Private Async Sub comboBox1_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbxMOMS.SelectionChanged

        If cbxMOMS.SelectedIndex > 0 Then
            cbxMOMS.Visibility = Visibility.Collapsed
            cbxAuthorIntDate.Visibility = Visibility.Visible
            back.Visibility = Visibility.Visible


            Dim yearModel As New YearVM
            yearModel.Century = cbxMOMS.SelectedIndex
            If lblRadioButtonValue.Content = "rdbMO" Then
                yearModel.MOMS = "MO"
            ElseIf rdbMS.Content = "rdbMS" Then
                yearModel.MOMS = "MS"
            End If


            Dim yearService As New YearServices
            Await yearService.AllCenturyAsync(yearModel)

            Dim years = yearService.AllCenturyList.ToList

            If years.Count > 0 Then
                For item = 0 To years.Count - 1
                    cbxAuthorIntDate.Items.Add(years(item))
                Next
            End If


        Else
            cbxMOMS.Visibility = Visibility.Visible
            cbxAuthorIntDate.Visibility = Visibility.Collapsed
            back.Visibility = Visibility.Collapsed
        End If
        cbxAuthorIntDate.SelectedIndex = 0
    End Sub

    Private Sub btn_AddBook_Click(sender As Object, e As MouseButtonEventArgs)

        Dim author As AuthorDTO = cbxAuthor.SelectedItem
        Dim year As YearDTO = cbxAuthorIntDate.SelectedItem
        Dim category As CategoryDTO = cbxCategory.SelectedItem
        Dim publisher As PublisherDTO = cbxPublisher.SelectedItem

        Dim model As New AddBookVM

        If tbxBookName.Text <> "" AndAlso tbxPage.Text <> "" AndAlso year IsNot Nothing AndAlso txbxDetail.Text <> "" Then


            model.BookName = tbxBookName.Text
            model.AuthorId = author.Id
            model.Page = Convert.ToInt32(tbxPage.Text)
            model.YearId = year.Id
            model.IsRead = cbcIsRead.IsChecked
            model.CategoryId = category.Id
            model.PublisherId = publisher.Id
            model.Detail = txbxDetail.Text

            Dim addBookService = New AddBookServices
            addBookService.AddBookAsync(model)

            Dim refresh As New RefreshService
            refresh.Refresh("TransmissionAllBook")

            Me.Close()
        Else
            MessageBox.Show("Lütfen tüm bilgi alanlarını doldurunuz.", "Bilgi Alanı Boş", vbOKOnly, MessageBoxImage.Information)

        End If
    End Sub

    Private Sub back_MouseDown(sender As Object, e As MouseButtonEventArgs)
        rdbMO.IsChecked = False
        rdbMS.IsChecked = False
        cbxAuthorIntDate.Items.Clear()
        cbxAuthorIntDate.Visibility = Visibility.Hidden
        back.Visibility = Visibility.Hidden
        rdbMO.Visibility = Visibility.Visible
        rdbMS.Visibility = Visibility.Visible
    End Sub

End Class
