Imports System.Collections.ObjectModel
Imports Library.UI.Models.DTOs
Imports Library.UI.Models.RequestModels.CategoryVMs
Imports Library.UI.Models.Wrapper
Imports Library.UI.Services.Interfaces
Imports Newtonsoft.Json

Namespace Services.Implementation
    Public Class CategoryServices
        Inherits ViewModelBase


        Dim restService As IRestService

        Public Property AllCategoryList As New ObservableCollection(Of CategoryDTO)

        Public ReadOnly Property AddCategoryCommand As RelayCommand
            Get
                Return New RelayCommand(Sub(execute)
                                            Dim unused = AddCategoryAsync()
                                        End Sub,
                                       Function(canExecute)
                                           Return True
                                       End Function)
            End Get
        End Property

        Public Sub New()

            Dim unused1 = AllCategoryAsync()
            Dim unused2 = AddCategoryAsync()
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

        Private Async Function AllCategoryAsync() As Task
            Dim categoriesList = Await GetAllCategory()
            AllCategoryList.Clear()
            For Each categories In categoriesList
                AllCategoryList.Add(categories)

            Next
        End Function

        Private Async Function AddCategoryAsync() As Task
            Dim addCategoryItem = Await AddCategory()
            
        End Function

        Public Async Function GetAllCategory() As Task(Of IEnumerable(Of CategoryDTO))
            restService = New RestService
            Dim responseMessage = Await restService.GetAsync(Of CategoryDTO)("category/all")

            If responseMessage.IsSuccessful Then
                Dim response = JsonConvert.DeserializeObject(Of Result(Of List(Of CategoryDTO)))(responseMessage.Content)
                Return response.Data
            Else
                ' Hata durumunu ele al
                Return Enumerable.Empty(Of CategoryDTO)()
            End If
        End Function

        Public Async Function AddCategory() As Task(Of IEnumerable(Of CreateCategoryVM))
            If Name IsNot Nothing Then
                restService = New RestService


                Dim requestModel As CreateCategoryVM = New CreateCategoryVM With {
                    .Name = Name
                    }

                Dim responseMessage = Await restService.PostAsync(Of CreateCategoryVM, Result(Of Integer))(requestModel, "category/create")

                If responseMessage.IsSuccessful Then
                    Dim response = JsonConvert.DeserializeObject(Of Result(Of Integer))(responseMessage.Content)

                    MessageBox.Show("Kayıt işlemi başarılı.", "Başarılı", vbOKOnly, vbInformation)


                Else
                    ' Hata durumunu ele al
                    Return Enumerable.Empty(Of CreateCategoryVM)()
                    MessageBox.Show("Kayıt işlemi başarısız.", "Hata", vbOKOnly, MessageBoxImage.Error)

                End If
                Name = String.Empty
                Dim refresh As New RefreshService
                refresh.Refresh("TransmissionCategory")
                
            End If

        End Function

    End Class
End Namespace