﻿Namespace Models
    Public Class ucCall
        Public Shared Sub Uc_Add(ByVal grd As Grid, ByVal uc As UserControl)
            If grd.Children.Count > 0 Then
                grd.Children.Clear()
                grd.Children.Add(uc)
            ElseIf grd.Children.Count = 0 Then
                grd.Children.Add(uc)
            End If
        End Sub

    End Class
End Namespace