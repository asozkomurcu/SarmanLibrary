Imports System.Collections.ObjectModel
Imports Library.UI.Models.RequestModels
Imports Library.UI.Models.RequestModels.BookVMs
Imports Library.UI.Models.Wrapper
Imports Library.UI.Services.Interfaces
Imports Newtonsoft.Json

Namespace Services.Implementation
    Public Class DeletedService
        Inherits ViewModelBase


        Dim restService As IRestService

        Public Property DeletedReturn() As New ObservableCollection(Of Integer)

        Public Sub New(filter)

            Dim unused = DeletedAsync(filter)


        End Sub

        Private _filterId As Long
        Public Property FilterId() As Long
            Get
                Return _filterId
            End Get
            Set(ByVal value As Long)
                If _filterId = value Then
                    Return
                End If
                _filterId = value
                OnPropertyChanged(NameOf(FilterId))
            End Set
        End Property

        Private _filterDto As String
        Public Property FilterDto() As String
            Get
                Return _filterDto
            End Get
            Set(ByVal value As String)
                If _filterDto = value Then
                    Return
                End If
                _filterDto = value
                OnPropertyChanged(NameOf(FilterDto))
            End Set
        End Property

        Private Async Function DeletedAsync(filter As FilterBookVM) As Task(Of Integer)
            If filter IsNot Nothing Then

                Dim deleteItem = Await Deleted(filter)
                DeletedReturn.Clear()
                DeletedReturn.Add(deleteItem)



            End If
        End Function

        Public Async Function Deleted(filter As FilterBookVM) As Task(Of Integer)

            restService = New RestService
            If filter IsNot Nothing Then
                Dim endpoint As String = Nothing
                Dim requestModel As New DeleteVM
                requestModel.Id = filter.FilterId
                Select Case filter.FilterDto
                    Case "AuthorDTO"
                        endpoint = $"author/delete/{filter.FilterId}"
                    Case "CategoryDTO"
                        endpoint = $"category/delete/{filter.FilterId}"
                    Case "PublisherDTO"
                        endpoint = $"publisher/delete/{filter.FilterId}"
                    Case "BookDTO"
                        endpoint = $"book/delete/{filter.FilterId}"
                End Select
                Dim responseMessage = Await restService.DeleteAsync(Of Result(Of Long))(endpoint)

                Dim response = JsonConvert.DeserializeObject(Of Result(Of Long))(responseMessage.Content)
                If response.Errors.Count = 0 And response.Success = True Then
                    MessageBox.Show("Silme işlemi başarılı.", "Başarılı", vbOKOnly, vbInformation)

                Else
                    MessageBox.Show(response.Errors(0), "Silme işlemi başarısız.", vbOKOnly, MessageBoxImage.Error)
                End If

            End If

        End Function

    End Class
End Namespace