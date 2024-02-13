Imports Library.UI.Models.RequestModels.AuthorVMs
Imports Library.UI.Models.RequestModels.BookVMs
Imports Library.UI.Models.Wrapper
Imports Library.UI.Services.Interfaces
Imports Newtonsoft.Json

Namespace Services.Implementation
    Public Class AddBookServices
        Inherits ViewModelBase

        Dim restService As IRestService


        Public Sub New()

        End Sub

        Public Async Function AddBookAsync(model As AddBookVM) As Task
            Dim addBookItem = Await AddBook(model)

        End Function


        Public Async Function AddBook(model As AddBookVM) As Task(Of IEnumerable(Of AddBookVM))

            If model IsNot Nothing Then
                restService = New RestService

                Dim responseMessage = Await restService.PostAsync(Of AddBookVM, Integer)(model, "book/add")
                Dim response = JsonConvert.DeserializeObject(Of Result(Of Integer))(responseMessage.Content)
                If response.Errors.Count = 0 And response.Success = True Then

                    MessageBox.Show("Kayıt işlemi başarılı.", "Başarılı", vbOKOnly, vbInformation)

                Else
                    ' Hata durumunu ele al
                    Return Enumerable.Empty(Of AddAuthorVM)()
                    MessageBox.Show("Kayıt işlemi başarısız.", "Hata", vbOKOnly, MessageBoxImage.Error)

                End If

            End If

        End Function

    End Class
End Namespace
