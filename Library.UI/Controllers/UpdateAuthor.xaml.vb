
Imports System.Runtime.Intrinsics.Arm
Imports Library.UI.Models.DTOs
Imports Library.UI.Models.RequestModels
Imports Library.UI.Models.RequestModels.YearVMs
Imports Library.UI.Services.Implementation

Public Class UpdateAuthor
    Inherits Window


    Public Sub New(selectedItems)
        InitializeComponent()
        Me.DataContext = selectedItems
    End Sub


    Private Sub windowClose_Click(sender As Object, e As MouseButtonEventArgs)
        Me.Close()
    End Sub


    Private Async Sub btnUpdate_Click(sender As Object, e As RoutedEventArgs)

        Dim data = Me.DataContext
        Dim ReleaseDateItem As YearDTO = cbxAuthorIntDate.SelectedItem
        Dim CountryItem As CountryDTO = cbxAuthorCountryId.SelectedItem

        Dim ReleaseDate As Long
        If ReleaseDateItem IsNot Nothing Then
            ReleaseDate = ReleaseDateItem.Id
        Else
            ReleaseDate = data.BirthDateId
        End If

        Dim Country As New CountryDTO
        If grdCountryLabel.Visibility = Visibility.Visible Then
            Country.Name = data.CountryName
        Else
            Country.Name = CountryItem.Name
        End If

        Dim requestModel = New UpdateVM()
        requestModel.Id = data.Id
        requestModel.FirstName = tbxSelectedModelFirstName.Text
        requestModel.LastName = txtSelectedModelLastName.Text
        requestModel.IntDate = ReleaseDate
        requestModel.CountryName = Country.Name
        requestModel.FilterDto = "AuthorDto"

        Dim update = New UpdateService(requestModel)
        Dim refresh As New RefreshService
        refresh.Refresh("TransmissionAuthors")
        Me.Close()

    End Sub

    Private Sub tbxAuthorIntDate_MouseDown(sender As Object, e As MouseButtonEventArgs)
        grdAuthorIntDate.Visibility = Visibility.Hidden
        grdAuthor.Visibility = Visibility.Visible
        rdbMO.Visibility = Visibility.Visible
        rdbMS.Visibility = Visibility.Visible
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

    Private Sub tbxCountry_MouseDown(sender As Object, e As MouseButtonEventArgs)
        grdCountryLabel.Visibility = Visibility.Hidden
        grdCountry.Visibility = Visibility.Visible
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

    Private Sub btnUpdate_Click_1(sender As Object, e As RoutedEventArgs)

    End Sub
End Class
