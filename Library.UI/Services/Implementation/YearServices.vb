Imports System.Collections.ObjectModel
Imports Library.UI.Models.DTOs
Imports Library.UI.Models.RequestModels.YearVMs
Imports Library.UI.Models.Wrapper
Imports Library.UI.Services.Interfaces
Imports Newtonsoft.Json

Namespace Services.Implementation
    Public Class YearServices
        Inherits ViewModelBase

        Dim restService As IRestService
        Public Property AllYearsList As New ObservableCollection(Of YearDTO)
        Public Property AllCenturyList As New ObservableCollection(Of YearDTO)

        Public Sub New()


        End Sub

        Public Function RadioButton(selectedRadioButton)
            If selectedRadioButton IsNot Nothing AndAlso selectedRadioButton.IsChecked = True Then

                Dim elementModels As New List(Of String)
                Dim century As Integer = DateTime.Now.Year / 100 + 1
                elementModels.Add("Yüzyıl seçimi yapınız.")

                If selectedRadioButton.Name = "rdbMO" Then

                    For index = 1 To 10
                        elementModels.Add(index.ToString() + ". Yüzyıl")
                    Next
                ElseIf selectedRadioButton.Name = "rdbMS" Then
                    For item = 1 To century
                        elementModels.Add(item.ToString() + ". Yüzyıl")
                    Next
                End If
                Return elementModels
            End If
        End Function

        Private Async Function AllYearsAsync() As Task
            Dim yearsList = Await GetAllYears()
            AllYearsList.Clear()
            For Each years In yearsList
                AllYearsList.Add(years)
            Next
        End Function

        Public Async Function AllCenturyAsync(yearModel) As Task(Of List(Of YearDTO))
            Dim yearsList = Await GetAllCentury(yearModel)
            AllCenturyList.Clear()
            For Each years In yearsList
                AllCenturyList.Add(years)
            Next
        End Function

        Public Async Function GetAllYears() As Task(Of IEnumerable(Of YearDTO))
            restService = New RestService
            Dim responseMessage = Await restService.GetAsync(Of YearDTO)("year/all")

            If responseMessage.IsSuccessful Then
                Dim response = JsonConvert.DeserializeObject(Of Result(Of List(Of YearDTO)))(responseMessage.Content)
                Return response.Data
            Else
                ' Hata durumunu ele al
                Return Enumerable.Empty(Of YearDTO)()
            End If
        End Function

        Public Async Function GetAllCentury(yearModel As YearVM) As Task(Of IEnumerable(Of YearDTO))
            restService = New RestService

            Dim responseMessage = Await restService.PostAsync(Of YearVM, YearDTO)(yearModel, "year/allYY")

            Dim response = JsonConvert.DeserializeObject(Of Result(Of List(Of YearDTO)))(responseMessage.Content)

            If response.Errors.Count = 0 AndAlso response.Success = True Then
                Return response.Data

            Else
                ' Hata durumunu ele al
                Return Enumerable.Empty(Of YearDTO)()
            End If

        End Function
    End Class
End Namespace