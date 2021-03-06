﻿Public Class EventNotifier(Of T)

    Private handlers As New List(Of Action(Of T))

    Sub [addHandler](action As Action(Of T))
        handlers.Add(action)
    End Sub
    Sub [removeHandler](action As Action(Of T))
        handlers.Remove(action)
    End Sub

    Sub Notify(value As T)
        For Each handler As Action(Of T) In handlers
            handler(value)
        Next
    End Sub

End Class
Public Class EventNotifier(Of T1, T2)

    Private handlers As New List(Of Action(Of T1, T2))

    Sub [addHandler](action As Action(Of T1, T2))
        handlers.Add(action)
    End Sub
    Sub [removeHandler](action As Action(Of T1, T2))
        handlers.Remove(action)
    End Sub

    Sub Notify(value1 As T1, value2 As T2)
        For Each handler As Action(Of T1, T2) In handlers
            handler(value1, value2)
        Next
    End Sub

End Class
Public Class EventNotifier(Of T1, T2, T3)

    Private handlers As New List(Of Action(Of T1, T2, T3))

    Sub [addHandler](action As Action(Of T1, T2, T3))
        handlers.Add(action)
    End Sub
    Sub [removeHandler](action As Action(Of T1, T2, T3))
        handlers.Remove(action)
    End Sub

    Sub Notify(value1 As T1, value2 As T2, value3 As T3)
        For Each handler As Action(Of T1, T2, T3) In handlers
            handler(value1, value2, value3)
        Next
    End Sub

End Class

Public Class EventNotifier

    Private handlers As New List(Of Action)

    Sub [addHandler](action As Action)
        handlers.Add(action)
    End Sub
    Sub [removeHandler](action As Action)
        handlers.Remove(action)
    End Sub

    Sub Notify()
        For Each handler As Action In handlers
            handler()
        Next
    End Sub

End Class
