Imports AutoMapper
Imports [Lib].Application.Models.RequestModels
Imports [Lib].Application.Models.RequestModels.AuthorVMs
Imports [Lib].Application.Models.RequestModels.BookVMs
Imports [Lib].Application.Models.RequestModels.CategoryVMs
Imports [Lib].Application.Models.RequestModels.PublisherVMs
Imports [Lib].Domain
Imports [Lib].Domain.Entities

Namespace AutoMapper
    Public Class ViewModelToDomain
        Inherits Profile
        Public Sub New()
            'Author
            CreateMap(Of AddAuthorVM, Author)()
            CreateMap(Of UpdateVM, Author)()


            'Book
            CreateMap(Of AddBookVM, Book)()
            CreateMap(Of UpdateVM, Book)()


            'Category
            CreateMap(Of CreateCategoryVM, Category)()
            CreateMap(Of UpdateVM, Category)()


            'Publisher
            CreateMap(Of AddPublisherVM, Publisher)()
            CreateMap(Of UpdateVM, Publisher)()
        End Sub
    End Class
End Namespace