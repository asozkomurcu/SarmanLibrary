Imports RestSharp

Namespace Services.Interfaces
    Public Interface IRestService
        Function PostAsync(Of TRequest, TResponse)(requestModel As TRequest, endpointUrl As String, Optional tokenRequired As Boolean = True) As Task(Of RestResponse(Of TResponse))

        Function PostAsync(Of TResponse)(endpointUrl As String, Optional tokenRequired As Boolean = True) As Task(Of RestResponse(Of TResponse))

        Function PostFormAsync(Of TResponse)(parameters As Dictionary(Of String, String), endpointUrl As String, Optional tokenRequired As Boolean = True) As Task(Of RestResponse(Of TResponse))

        Function GetAsync(Of TResponse)(endpointUrl As String, Optional tokenRequired As Boolean = True) As Task(Of RestResponse(Of TResponse))

        Function DeleteAsync(Of TResponse)(endpointUrl As String, Optional tokenRequired As Boolean = True) As Task(Of RestResponse(Of TResponse))

        Function PutAsync(Of TRequest, TResponse)(requestModel As TRequest, endpointUrl As String, Optional tokenRequired As Boolean = True) As Task(Of RestResponse(Of TResponse))
    End Interface
End Namespace