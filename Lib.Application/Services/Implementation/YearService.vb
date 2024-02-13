Imports [Lib].Application.Models.DTOs
Imports [Lib].Application.Models.RequestModels.AuthorVMs
Imports [Lib].Application.Services.Abstraction
Imports [Lib].Application.Wrapper
Imports [Lib].Domain
Imports [Lib].Persistence.Context

Public Class YearService
    Implements IYearService

    Private ReadOnly _unitOfWork As IUnitofWork
    Private ReadOnly _context As LibContext
    Public Sub New(unitOfWork As IUnitofWork, context As LibContext)
        _unitOfWork = unitOfWork
        _context = context
    End Sub

    Public Async Function GetAllYear() As Task(Of Result(Of List(Of YearDTO))) Implements IYearService.GetAllYear
        Dim yearRepository = _unitOfWork.GetRepository(Of Year)()
        Dim result As New Result(Of List(Of YearDTO))

        Try
            Dim years = Await yearRepository.GetAllAsync()


            result.Data = years.Select(Function(year) New YearDTO With {
                .Id = year.Id,
                .IntDate = year.IntDate
            }).ToList()

            result.Success = True
        Catch ex As Exception
            result.Success = False
            result.Errors.Add("Listeleme işlemi sırasında bir hata oluştu.")
        End Try

        Return result
    End Function

    Public Async Function GetAllYY(yearVM As YearVM) As Task(Of Result(Of List(Of YearDTO))) Implements IYearService.GetAllYY
        Dim yearRepository = _unitOfWork.GetRepository(Of Year)()
        Dim result As New Result(Of List(Of YearDTO))

        Dim years As IQueryable(Of Year)
        Dim CenturyIndex As Integer
        Dim CenturyIndex1 As Integer
        Try
            If yearVM.MOMS = "MO" Then
                CenturyIndex = yearVM.Century * -100
                CenturyIndex1 = CenturyIndex + 99
                years = _context.Year.OrderByDescending(Function(x) x.Id).Where(Function(x) CenturyIndex <= x.IntDate And x.IntDate <= CenturyIndex1)
            ElseIf yearVM.Century = 21 Then
                CenturyIndex = yearVM.Century * 100
                CenturyIndex1 = CenturyIndex - 99
                Dim CenturyIndex2 As Integer = DateTime.Now.Year
                years = _context.Year.OrderBy(Function(x) x.Id).Where(Function(x) CenturyIndex1 <= x.IntDate And x.IntDate <= CenturyIndex2)
            Else
                CenturyIndex = yearVM.Century * 100
                CenturyIndex1 = CenturyIndex - 99
                years = _context.Year.OrderBy(Function(x) x.Id).Where(Function(x) CenturyIndex1 <= x.IntDate And x.IntDate <= CenturyIndex)
            End If

            result.Data = years.Select(Function(year) New YearDTO With {
                    .Id = year.Id,
                    .IntDate = year.IntDate
                }).ToList()

            result.Success = True
        Catch ex As Exception
            result.Success = False
            result.Errors.Add("Listeleme işlemi sırasında bir hata oluştu.")
        End Try

        Return result
    End Function
End Class
