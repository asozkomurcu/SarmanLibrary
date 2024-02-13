Imports System.Windows.Input


Public Class RelayCommand
    Implements ICommand

    Private ReadOnly _execute As Action(Of Object)
    Private ReadOnly _canExecute As Predicate(Of Object)

    Public Sub New(ByVal execute As Action(Of Object))
        Me.New(execute, Nothing)
    End Sub

    Public Sub New(ByVal execute As Action(Of Object), ByVal canExecute As Predicate(Of Object))
        _execute = execute
        _canExecute = canExecute
    End Sub

    Public Function CanExecute(ByVal parameter As Object) As Boolean Implements ICommand.CanExecute
        If _canExecute Is Nothing Then
            Return True
        End If
        Return _canExecute(parameter)
    End Function

    Public Custom Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged
        AddHandler(ByVal value As EventHandler)
            AddHandler CommandManager.RequerySuggested, value
        End AddHandler
        RemoveHandler(ByVal value As EventHandler)
            RemoveHandler CommandManager.RequerySuggested, value
        End RemoveHandler
        RaiseEvent(ByVal sender As Object, ByVal e As EventArgs)
        End RaiseEvent
    End Event

    Public Sub Execute(ByVal parameter As Object) Implements ICommand.Execute
        _execute(parameter)
    End Sub
End Class
