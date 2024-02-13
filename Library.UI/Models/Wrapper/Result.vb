Namespace Models.Wrapper
    Public Class Result(Of T)
        Public Property Data As T
        Public Property Success As Boolean = True
        Public Property Errors As List(Of String) = New List(Of String)()


    End Class
End Namespace
