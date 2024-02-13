Imports [Lib].Application.Models.DTOs
Imports [Lib].Application.Models.RequestModels
Imports [Lib].Application.Models.RequestModels.CategoryVMs
Imports [Lib].Application.Services.Abstraction
Imports Microsoft.AspNetCore.Mvc

Namespace Controllers
    <Route("category")>
    <ApiController>
    Public Class CategoryController
        Inherits ControllerBase

        Private ReadOnly _categoryService As ICategoryService

        Public Sub New(categoryService As ICategoryService)
            _categoryService = categoryService
        End Sub

        <HttpGet("all")>
        Public Async Function GetAllCategories() As Task(Of ActionResult(Of List(Of CategoryDTO)))
            Dim categories = Await _categoryService.GetAllCategories()
            Return Ok(categories)
        End Function

        <HttpGet("{categoryId}")>
        Public Async Function GetCategoryById(categoryId As Long) As Task(Of ActionResult(Of CategoryDTO))
            Dim category = Await _categoryService.GetCategoryById(categoryId)
            If category IsNot Nothing Then
                Return Ok(category)
            End If
            Return NotFound()
        End Function

        <HttpPost("create")>
        Public Function CreateCategory(createCategoryVM As CreateCategoryVM)
            Dim categories = _categoryService.CreateCategory(createCategoryVM)
            Return Ok(categories)
        End Function

        <HttpPut("update")>
        Public Function UpdateCategory(updateVM As UpdateVM)
            Dim categories = _categoryService.UpdateCategory(updateVM)
            Return Ok(categories)
        End Function

        <HttpDelete("delete/{categoryId}")>
        Public Function DeleteCategory(categoryId As Long)
            Dim categories = _categoryService.DeleteCategory(categoryId)
            Return Ok(categories)
        End Function
    End Class
End Namespace