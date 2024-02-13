Imports Library.UI.Models.DTOs
Imports Library.UI.Models.RequestModels.AuthorVMs
Imports Library.UI.Models.RequestModels.YearVMs
Imports Library.UI.Services.Implementation
Imports Library.UI.Services.Interfaces

Public Class addAuthor
    Inherits Window

    Public Sub New()
        InitializeComponent()
        Me.DataContext = New AuthorServices()
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

    Dim restService As IRestService

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

    Private Sub Button_Click(sender As Object, e As MouseButtonEventArgs)



        Dim year As YearDTO = cbxAuthorIntDate.SelectedItem
        Dim country As CountryDTO = cbxAuthorCountryId.SelectedItem

        Dim model As New AddAuthorVM

        If tbxFirstName.Text <> "" AndAlso tbxLastName.Text <> "" AndAlso year IsNot Nothing AndAlso cbxAuthorIntDate.SelectedIndex > -1 Then


            model.FirstName = tbxFirstName.Text
            model.LastName = tbxLastName.Text
            model.BirthDateId = year.Id
            model.CountryId = country.Id

            Dim authorService As New AuthorServices
            authorService.AddAuthorAsync(model)

            Dim refresh As New RefreshService
            refresh.Refresh("TransmissionAuthors")
            Me.Close()
        Else
            MessageBox.Show("Lütfen tüm bilgi alanlarını doldurunuz.", "Bilgi Alanı Boş", vbOKOnly, MessageBoxImage.Information)

        End If
    End Sub

    Private Sub CloseWindow_PreviewMouseDown(sender As Object, e As MouseButtonEventArgs)

        Dim refresh As New RefreshService
        refresh.Refresh("TransmissionAuthors")

        Me.Close()
    End Sub

End Class
