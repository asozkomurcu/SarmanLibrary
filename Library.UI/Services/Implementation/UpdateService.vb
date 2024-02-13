Imports Library.UI.Models.RequestModels
Imports Library.UI.Models.Wrapper
Imports Library.UI.Services
Imports Library.UI.Services.Implementation
Imports Library.UI.Services.Interfaces
Imports Newtonsoft.Json

Public Class UpdateService
    Inherits ViewModelBase

    Dim restService As IRestService


    Public Sub New(model)

        Dim unused = UpdatedAsync(model)

    End Sub

    Public Async Function UpdatedAsync(model As UpdateVM) As Task(Of Integer)
        restService = New RestService
        Dim endpoint As String = Nothing

        Select Case model.FilterDto
            Case "AuthorDto"
                endpoint = $"author/update"
            Case "CategoryDto"
                endpoint = $"category/update"
            Case "PublisherDto"
                endpoint = $"publisher/update"
            Case "BookDto"
                endpoint = $"book/update"
        End Select

        Dim responseMessage = Await restService.PutAsync(Of UpdateVM, Result(Of Integer))(model, endpoint)

        Dim response = JsonConvert.DeserializeObject(Of Result(Of Long))(responseMessage.Content)
        If response.Errors.Count = 0 And response.Success = True Then
            MessageBox.Show("Güncelleme işlemi başarılı.", "Başarılı", vbOKOnly, vbInformation)
        Else
            MessageBox.Show(response.Errors(0), "Güncelleme işlemi başarısız.", vbOKOnly, MessageBoxImage.Error)
        End If
    End Function



End Class
