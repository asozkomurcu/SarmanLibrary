﻿Imports AutoMapper
Imports [Lib].Application.Models.DTOs
Imports [Lib].Domain

Public Class DomainToDTO
    Inherits Profile
    Public Sub New()
        CreateMap(Of Author, AuthorDTO)()
        CreateMap(Of Book, BookDTO)()
        CreateMap(Of Category, CategoryDTO)()
        CreateMap(Of Publisher, PublisherDTO)()
        CreateMap(Of Year, YearDTO)()
        CreateMap(Of Country, CountryDTO)()
    End Sub
End Class
