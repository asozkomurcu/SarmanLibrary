Imports Library.UI.Services.Interfaces
Imports Newtonsoft.Json
Imports RestSharp



Namespace Services.Implementation
    Public Class RestService
        Implements IRestService


        Const apiUrl = "https://localhost:49961/"

        Public Async Function PostAsync(Of TRequest, TResponse)(requestModel As TRequest, endpointUrl As String, Optional tokenRequired As Boolean = True) As Task(Of RestResponse(Of TResponse)) Implements IRestService.PostAsync

            Dim jsonModel As String = JsonConvert.SerializeObject(requestModel)

            Dim restClient As New RestClient(apiUrl)
            Dim restRequest As New RestRequest(endpointUrl, Method.Post)

            restRequest.AddParameter("application/json", jsonModel, ParameterType.RequestBody)
            restRequest.AddHeader("Accept", "application/json")

            Dim response = Await restClient.ExecuteAsync(Of TResponse)(restRequest)
            Return response

        End Function

        Public Async Function PostAsync(Of TResponse)(endpointUrl As String, Optional tokenRequired As Boolean = True) As Task(Of RestResponse(Of TResponse)) Implements IRestService.PostAsync

            Dim restClient As New RestClient(apiUrl)
            Dim restRequest As New RestRequest(endpointUrl, Method.Post)

            restRequest.AddHeader("Accept", "application/json")

            Dim response = Await restClient.ExecuteAsync(Of TResponse)(restRequest)
            Return response

        End Function
        Public Async Function PostFormAsync(Of TResponse)(parameters As Dictionary(Of String, String), endpointUrl As String, Optional tokenRequired As Boolean = True) As Task(Of RestResponse(Of TResponse)) Implements IRestService.PostFormAsync

            Dim restClient As New RestClient(apiUrl)
            Dim restRequest As New RestRequest(endpointUrl, Method.Post)

            restRequest.AddHeader("content-type", "application/x-www-form-urlencoded")
            restRequest.AddHeader("Accept", "application/json")

            'Modelden gelen bilgiler isteğe key value şeklinde aktarılıyor
            AddFormParametersToRequest(restRequest, parameters)

            Dim response = Await restClient.ExecuteAsync(Of TResponse)(restRequest)
            Return response
        End Function

        Public Async Function GetAsync(Of TResponse)(endpointUrl As String, Optional tokenRequired As Boolean = True) As Task(Of RestResponse(Of TResponse)) Implements IRestService.GetAsync

            Dim restClient As New RestClient(apiUrl)
            Dim restRequest As New RestRequest(endpointUrl, Method.Get)

            restRequest.AddHeader("Accept", "application/json")

            Dim response = Await restClient.ExecuteAsync(Of TResponse)(restRequest)
            Return response

        End Function



        Public Async Function DeleteAsync(Of TResponse)(endpointUrl As String, Optional tokenRequired As Boolean = True) As Task(Of RestResponse(Of TResponse)) Implements IRestService.DeleteAsync

            Dim restClient As New RestClient(apiUrl)
            Dim restRequest As New RestRequest(endpointUrl, Method.Delete)

            restRequest.AddHeader("Accept", "application/json")

            Dim response = Await restClient.ExecuteAsync(Of TResponse)(restRequest)
            Return response

        End Function

        Public Async Function PutAsync(Of TRequest, TResponse)(requestModel As TRequest, endpointUrl As String, Optional tokenRequired As Boolean = True) As Task(Of RestResponse(Of TResponse)) Implements IRestService.PutAsync
            Dim jsonModel As String = JsonConvert.SerializeObject(requestModel)

            Dim restClient As New RestClient(apiUrl)
            Dim restRequest As New RestRequest(endpointUrl, Method.Put)

            restRequest.AddParameter("application/json", jsonModel, ParameterType.RequestBody)
            restRequest.AddHeader("Accept", "application/json")

            Dim response = Await restClient.ExecuteAsync(Of TResponse)(restRequest)
            Return response
        End Function


        Private Sub AddFormParametersToRequest(ByVal request As RestRequest, ByVal parameters As Dictionary(Of String, String))
            For Each key In parameters.Keys
                request.AddParameter(key, parameters(key))
            Next
        End Sub
    End Class

End Namespace