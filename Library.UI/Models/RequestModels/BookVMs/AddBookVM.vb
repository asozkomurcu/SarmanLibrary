﻿Namespace Models.RequestModels.BookVMs
    Public Class AddBookVM
        Public Property BookName As String
        Public Property Page As Integer
        Public Property YearId As Long
        Public Property CategoryId As Long
        Public Property Detail As String
        Public Property IsRead As Boolean
        Public Property PublisherId As Long
        Public Property AuthorId As Long
    End Class
End Namespace