Imports System.Collections.ObjectModel
Imports Library.UI.Models.DTOs
Imports Library.UI.Models.Wrapper
Imports Library.UI.Services.Interfaces
Imports Newtonsoft.Json

Namespace Services.Implementation
    Public Class CountryServices
        Inherits ViewModelBase

        Dim restSrevice As IRestService

        Public Property AllCountriesList As New ObservableCollection(Of CountryDTO)

        Public Sub New()

            Dim unused1 = AllCountriesAsync()

        End Sub


        Private Async Function AllCountriesAsync() As Task
            Dim countriesList = Await GetAllCountries()
            AllCountriesList.Clear()
            For Each countries In countriesList
                AllCountriesList.Add(countries)
            Next
        End Function

        Public Async Function GetAllCountries() As Task(Of IEnumerable(Of CountryDTO))
            restSrevice = New RestService
            Dim responseMessage = Await restSrevice.GetAsync(Of CountryDTO)("country/all")

            If responseMessage.IsSuccessful Then
                Dim response = JsonConvert.DeserializeObject(Of Result(Of List(Of CountryDTO)))(responseMessage.Content)
                Return response.Data
            Else
                ' Hata durumunu ele al
                Return Enumerable.Empty(Of CountryDTO)()
            End If
        End Function
    End Class
End Namespace