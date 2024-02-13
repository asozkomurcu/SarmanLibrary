Imports System.ComponentModel
Imports System.Runtime.CompilerServices

Namespace Services
    Public Class ViewModelBase
        Implements INotifyPropertyChanged


        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        'Private Sub OnPropertyChanged(propertyName As String)
        '    PropertyChanged.Invoke(Me, New PropertyChangedEventArgs(propertyName))
        'End Sub

        Protected Sub OnPropertyChanged(<CallerMemberName> Optional propertyName As String = Nothing)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
        End Sub
    End Class
End Namespace