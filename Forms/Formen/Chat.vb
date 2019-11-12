﻿Imports Connector

Public Class ChatForm

    Private chats As New List(Of Chat)


    Private Sub Chat_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ChatArea.Hide()
        NetworkClass.net.getChats(NetworkClass.login.id, AddressOf onGetChats)
    End Sub

    Private Sub onGetChats(FriendChats As Chat())
        For Each chat As Chat In FriendChats
            ltbKontakte.Items.Add(chat.user.benutzername)
            chats.Add(chat)
        Next
        If FriendChats.Length > 0 Then
            ' Ersten als ausgewählten Chat setzen
            ltbKontakte.SelectedIndex = 0
        End If
    End Sub

    Private Sub btnNeuerKontakt_Click(sender As Object, e As EventArgs) Handles btnNeuerKontakt.Click
        AddFriend.Show()

    End Sub

    Private Sub LtbKontakte_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ltbKontakte.SelectedIndexChanged
        ' Chat wechseln

        Dim chat = chats(ltbKontakte.SelectedIndex)
        If chat IsNot Nothing Then
            ChatArea.Chat = chat
            ChatArea.Show()

        End If

    End Sub

    Private Sub btnAbmelden_Click(sender As Object, e As EventArgs) Handles btnAbmelden.Click
        Login.Show()
        NetworkClass.login = Nothing
        Me.Close()
    End Sub

    Public Sub addChatToList(ByVal user As Chat)
        chats.Insert(0, user)
        ltbKontakte.Items.Clear()

        For Each chat As Chat In chats
            ltbKontakte.Items.Add(chat.user.benutzername)
        Next


        ' Als Ausgewählten Chat setzen
        ltbKontakte.SelectedIndex = 0

    End Sub
End Class