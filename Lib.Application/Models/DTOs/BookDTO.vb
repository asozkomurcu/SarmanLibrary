Namespace Models.DTOs
    Public Class BookDTO
        Public Property Id As Long

        Public Property ReleaseDateId As Long
        Public Property CategoryId As Long
        Public Property PublisherId As Long
        Public Property AuthorId As Long
        Public Property CountryId As Long

        Public Property BookName As String
        Public Property Page As Integer
        Public Property Detail As String
        Public Property IsRead As Boolean
        Public Property CategoryName As String
        Public Property IntDate As Integer
        Public Property PublisherName As String
        Public Property AuthorFirstName As String
        Public Property AuthorLastName As String
    End Class
End Namespace