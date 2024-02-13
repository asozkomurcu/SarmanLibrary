Imports [Lib].Application.Models.DTOs
Imports [Lib].Application.Models.RequestModels
Imports [Lib].Application.Models.RequestModels.CategoryVMs
Imports [Lib].Application.Wrapper

Namespace Services.Abstraction
    Public Interface ICategoryService
        Function GetAllCategories() As Task(Of Result(Of List(Of CategoryDTO)))
        Function GetCategoryById(categoryId As Long) As Task(Of Result(Of CategoryDTO))
        Function CreateCategory(createCategoryVM As CreateCategoryVM) As Result(Of Integer)
        Function UpdateCategory(updateCategoryVM As UpdateVM) As Result(Of Integer)
        Function DeleteCategory(categoryId As Long) As Result(Of Boolean)

    End Interface
End Namespace