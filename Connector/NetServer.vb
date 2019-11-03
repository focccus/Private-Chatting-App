﻿Imports System.Net.Sockets

Public Class NetServer

    Private connector As ServerConnector

    Sub New()
    End Sub

    ' Erstellt neue Instanz und verbindet sich
    Public Sub connect()
        connector = New ServerConnector()
        ' Einkommende Nachricht handeln
        connector.OnRecieve.addHandler(AddressOf onRequest)
        connector.connect()
    End Sub

    ' Event für Registrierung Neue Methode zuweisen!
    Public OnRegister As Action(Of String, String, String, Action(Of Integer)
    ' Event für login Neue Methode zuweisen!
    Public OnLogin As Action(Of String, String, Action(Of Integer))
    'Event für alle Benutzernamen senden
    Public OnUserlist As Action(Of Action(Of String()))
    'Event für Freunde senden
    Public OnFriends As Action(Of Action(Of String()))
    'Event für Neue Freunde
    Public OnNewFriend As Action(Of String, Action(Of Boolean))


    ' Falls neue Nachricht kommt:
    Private Sub onRequest(req As ConnectionData, client As TcpClient)
        Console.WriteLine("Einkommende Nachricht des Typs " & req.Type)
        ' Welcher Typ ist die Nachricht?
        Select Case req.Type
            Case "register"
                If OnRegister IsNot Nothing Then
                    ' Argumente bekommen
                    Dim name As String = req.Data.Item("name")
                    Dim username As String = req.Data.Item("username")
                    Dim password As String = req.Data.Item("password")

                    ' Methode aufrufen + Callback 
                    OnRegister(
                        name,
                        username,
                        password,
                        Sub(id As Integer)
                            RegisterConfirm(id, client)
                        End Sub
                    )
                End If
            Case "login"
                If OnLogin IsNot Nothing Then
                    ' Argumente bekommen
                    Dim username As String = req.Data.Item("username")
                    Dim password As String = req.Data.Item("password")


                    ' Methode aufrufen + Callback 
                    OnLogin(
                        username,
                        password,
                        Sub(id As Integer)
                            LoginConfirm(id, client)
                        End Sub
                    )
                End If

            Case "all users"
                If OnUserlist IsNot Nothing Then
                    OnUserlist(
                    Sub(val As String())
                        AllUsersSend(val, client)
                    End Sub)

                End If

            Case "Friends"
                If OnFriends IsNot Nothing Then
                    OnFriends(
                        Sub(list As String())
                            FriendsSend(list, client)
                        End Sub)
                End If

            Case "new Friend confirm"
                If OnNewFriend IsNot Nothing Then
                    Dim username As String = req.Data.Item("username")
                    OnNewFriend(username,
                                Sub(val As Boolean)
                                    NewFriendConfirm(val, client)
                                End Sub)


                End If


        End Select

    End Sub

    ' Sende Antwort für Registrieren
    Sub RegisterConfirm(id As Integer, client As TcpClient)
        Dim data As New Dictionary(Of String, Object)
        data.Add("id", id)
        Dim req As New ConnectionData("registerconfirm", data)
        connector.send(client, req)
    End Sub

    ' Sende Antwort für Login
    Sub LoginConfirm(id As Integer, client As TcpClient)
        Dim data As New Dictionary(Of String, Object)
        data.Add("id", id)
        Dim req As New ConnectionData("loginconfirm", data)
        connector.send(client, req)
    End Sub


    Sub AllUsersSend(ans As String(), client As TcpClient)
        Dim data As New Dictionary(Of String, Object)
        data.add("All users", ans)
        connector.send(client, New ConnectionData("users", data))
    End Sub

    Sub FriendsSend(ans As String(), client As TcpClient)
        Dim data As New Dictionary(Of String, Object)
        data.add("Friends", ans)
        connector.send(client, New ConnectionData("friends", data))
    End Sub

    Sub NewFriendConfirm(val As Boolean, client As TcpClient)
        Dim data As New Dictionary(Of String, Object)
        data.Add("success", val)
        connector.send(client, New ConnectionData("friendconfirm", data))
    End Sub
End Class
