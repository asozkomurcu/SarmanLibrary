Imports Library.UI.Models.DTOs
Imports Library.UI.Models.RequestModels
Imports Library.UI.Models.RequestModels.YearVMs
Imports Library.UI.Services.Implementation

Public Class UpdateBook
    Inherits Window


    Public Sub New(selectedItems)
        InitializeComponent()
        Me.DataContext = selectedItems


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
            ElseIf lblRadioButtonValue.Content = "rdbMS" Then
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

    Private Sub back_MouseDown(sender As Object, e As MouseButtonEventArgs)
        rdbMO.IsChecked = False
        rdbMS.IsChecked = False
        cbxAuthorIntDate.Items.Clear()
        cbxAuthorIntDate.Visibility = Visibility.Hidden
        back.Visibility = Visibility.Hidden
        rdbMO.Visibility = Visibility.Visible
        rdbMS.Visibility = Visibility.Visible

    End Sub


    Private Async Sub btnUpdate_Click(sender As Object, e As RoutedEventArgs)

        Dim data = Me.DataContext
        Dim ReleaseDateItem As YearDTO = cbxAuthorIntDate.SelectedItem
        Dim CategoryItem As CategoryDTO = cbxCategory.SelectedItem
        Dim AuthorItem As AuthorDTO = cbxAuthor.SelectedItem
        Dim PublisherItem As PublisherDTO = cbxPublisher.SelectedItem

        Dim ReleaseDate As Long
        If ReleaseDateItem IsNot Nothing
            ReleaseDate = ReleaseDateItem.Id
        Else
            ReleaseDate = data.ReleaseDateId
        End If

        Dim CategoryId As Long
        If grdCategoryLabel.Visibility = Visibility.Visible
            CategoryId = data.CategoryId
        Else
            CategoryId = CategoryItem.Id
        End If

        Dim AuthorId As Long
        If grdAuthorLabel.Visibility = Visibility.Visible
            AuthorId = data.AuthorId
        Else
            AuthorId = AuthorItem.Id
        End If

        Dim PublisherId As Long
        If grdPublisherLabel.Visibility = Visibility.Visible
            PublisherId = data.PublisherId
        Else
            PublisherId = PublisherItem.Id
        End If

        Dim requestModel = New UpdateVM()
        requestModel.Id = data.Id
        requestModel.AuthorId = AuthorId
        requestModel.BookName = tbxBookName.Text
        requestModel.Page = Convert.ToInt64(tbxPage.Text)
        requestModel.ReleaseDateId = ReleaseDate
        requestModel.IsRead = chcbIsRead.IsChecked
        requestModel.CategoryId = CategoryId
        requestModel.PublisherId = PublisherId
        requestModel.Detail = tbxDetail.Text
        requestModel.FilterDto = "BookDto"

        Dim update = New UpdateService(requestModel)
        Dim refresh As New RefreshService
        refresh.Refresh("TransmissionAllBook")
        Me.Close

    End Sub

    Public Sub closeWindow_MouseDown(sender As Object, e As MouseButtonEventArgs)
        Me.Close()
    End Sub




    Private Sub tbxAuthorIntDate_MouseDown(sender As Object, e As MouseButtonEventArgs)
        grdAuthorIntDate.Visibility = Visibility.Hidden
        grdAuthor.Visibility = Visibility.Visible
        rdbMO.Visibility = Visibility.Visible
        rdbMS.Visibility = Visibility.Visible
    End Sub

    Private Sub cbxCategoryLabel_MouseDown(sender As Object, e As MouseButtonEventArgs)
        grdCategoryLabel.Visibility = Visibility.Hidden
        grdCategory.Visibility = Visibility.Visible
    End Sub

    Private Sub cbxPublisherLabel_MouseDown(sender As Object, e As MouseButtonEventArgs)
        grdPublisherLabel.Visibility = Visibility.Hidden
        grdPublisher.Visibility = Visibility.Visible
    End Sub

    Private Sub cbxAuthorLabel_MouseDown(sender As Object, e As MouseButtonEventArgs)
        grdAuthorLabel.Visibility = Visibility.Hidden
        grdAuthorID.Visibility = Visibility.Visible
    End Sub

    Private Sub authorListBox_MouseDown(sender As Object, e As MouseButtonEventArgs)
        grdAuthorLabel.Visibility = Visibility.Hidden
        grdAuthorID.Visibility = Visibility.Visible
    End Sub

    Private Sub TextBox_PreviewTextInput(sender As Object, e As TextCompositionEventArgs)
        e.Handled = Not IsTextAllowed(e.Text)
    End Sub

    Private Function IsTextAllowed(text As String) As Boolean
        Dim result As Integer
        Return Integer.TryParse(text, result)
    End Function
End Class
