Imports System.Collections.ObjectModel
Imports Library.UI.Models.DTOs
Imports Library.UI.Models.RequestModels.PublisherVMs
Imports Library.UI.Models.Wrapper
Imports Library.UI.Services.Interfaces
Imports Newtonsoft.Json

Namespace Services.Implementation
    Public Class PublisherServices
        Inherits ViewModelBase


        Dim restService As IRestService

        Public Property AllPublisherList As New ObservableCollection(Of PublisherDTO)

        Public ReadOnly Property AddPublisherCommand As RelayCommand
            Get
                Return New RelayCommand(Sub(execute)
                                            Dim unused = AddPublisherAsync()
                                        End Sub,
                                       Function(canExecute)
                                           Return True
                                       End Function)
            End Get
        End Property

        Public Sub New()

            Dim unused1 = AllPublisherAsync()
            Dim unused2 = AddPublisherAsync()
        End Sub

        Private _id As String
        Public Property Id() As String
            Get
                Return _id
            End Get
            Set(ByVal value As String)
                If _id = value Then
                    Return
                End If
                _id = value
                OnPropertyChanged(NameOf(Id))
            End Set
        End Property

        Private _name As String
        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(value As String)
                If _name = value Then
                    Return
                End If
                _name = value
                OnPropertyChanged(NameOf(Name))
            End Set
        End Property

        Private _description As String
        Public Property Description() As String
            Get
                Return _description
            End Get
            Set(value As String)
                If _description = value Then
                    Return
                End If
                _description = value
                OnPropertyChanged(NameOf(Description))
            End Set
        End Property

        Private Async Function AllPublisherAsync() As Task
            Dim publishersList = Await GetAllPublisher()
            AllPublisherList.Clear()
            For Each publishers In publishersList
                AllPublisherList.Add(publishers)

            Next
        End Function

        Private Async Function AddPublisherAsync() As Task
            Dim addPublisherItem = Await AddPublisher()

        End Function

        Public Async Function GetAllPublisher() As Task(Of IEnumerable(Of PublisherDTO))
            restService = New RestService
            Dim responseMessage = Await restService.GetAsync(Of PublisherDTO)("publisher/all")

            If responseMessage.IsSuccessful Then
                Dim response = JsonConvert.DeserializeObject(Of Result(Of List(Of PublisherDTO)))(responseMessage.Content)
                Return response.Data
            Else
                ' Hata durumunu ele al
                Return Enumerable.Empty(Of PublisherDTO)()
            End If
        End Function

        Public Async Function AddPublisher() As Task(Of IEnumerable(Of AddPublisherVM))
            If Name IsNot Nothing Then
                restService = New RestService


                Dim requestModel As AddPublisherVM = New AddPublisherVM With {
                    .Name = Name,
                    .Description = Description
                    }

                Dim responseMessage = Await restService.PostAsync(Of AddPublisherVM, Result(Of Integer))(requestModel, "publisher/add")

                If responseMessage.IsSuccessful Then
                    Dim response = JsonConvert.DeserializeObject(Of Result(Of Integer))(responseMessage.Content)
                    'Return response
                    MessageBox.Show("Kayıt işlemi başarılı.", "Başarılı", vbOKOnly, vbInformation)


                Else
                    ' Hata durumunu ele al
                    Return Enumerable.Empty(Of AddPublisherVM)()
                    MessageBox.Show("Kayıt işlemi başarısız.", "Hata", vbOKOnly, MessageBoxImage.Error)

                End If
                Name = String.Empty
                Description = String.Empty
            End If

        End Function

    End Class
End Namespace