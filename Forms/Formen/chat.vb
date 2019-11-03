﻿Public Class Chat

    Private users As String() = {"marten1", "laura1", "till1"}

    Private Sub Chat_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        For Each user As String In users
            ltbKontakte.Items.Add(user)
        Next
    End Sub

    Private Sub btnNeuerKontakt_Click(sender As Object, e As EventArgs) Handles btnNeuerKontakt.Click
        AddFriend.Show()

    End Sub

    Private Sub LtbKontakte_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ltbKontakte.SelectedIndexChanged
        ' Chat wechseln
    End Sub

    Private Sub btnAbmelden_Click(sender As Object, e As EventArgs) Handles btnAbmelden.Click
        Login.Show()
        NetworkClass.loginID = Nothing
        Me.Hide()
    End Sub
End Class