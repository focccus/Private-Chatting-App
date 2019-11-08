﻿Imports System.Data.OleDb
Imports System.Net.Sockets
Imports Connector
Imports Server.dbTableAdapters

Module Module1

    Private ReadOnly ConnectionStr = "provider= microsoft.jet.oledb.4.0;data source=db.mdb;"

    Public Sub Main()
        ' Verbindungsobjekt erstellen und EventHandler hinzufügen
        Dim connect As NetServer = New NetServer With {
            .OnRegister = AddressOf Register,
            .OnLogin = AddressOf CheckLogin,
            .OnNewChat = AddressOf AddFriend,
            .OnUserlist = AddressOf GetAllUsers,
            .OnChats = AddressOf GetFriends
        }

        connect.connect()

    End Sub

    ' Benutzerverwaltung

    Public Sub Register(name As String, username As String, password As String, done As Action(Of User))

        Dim conn As New OleDbConnection(ConnectionStr)
        conn.Open()

        Dim checkCommand As New OleDbCommand("select * from users where Username = '" & username & "'")
        checkCommand.Connection = conn
        Dim reader = checkCommand.ExecuteReader
        If reader.HasRows Then
            done(Nothing)
        Else
            Dim insertCommand As New OleDbCommand("INSERT INTO Users ([Name],Username,[Password]) VALUES (@displayname,@username,@password); ")
            Dim command As New OleDbCommand("SELECT @@IDENTITY")
            insertCommand.Connection = conn
            insertCommand.Parameters.Add("@displayname", OleDbType.Char).Value = name
            insertCommand.Parameters.Add("@username", OleDbType.Char).Value = username
            insertCommand.Parameters.Add("@password", OleDbType.Char).Value = password
            insertCommand.CommandType = CommandType.Text
            insertCommand.ExecuteNonQuery()
            command.Connection = conn
            done(New User(username, name, command.ExecuteScalar()))

        End If
    End Sub


    Public Sub CheckLogin(username As String, password As String, done As Action(Of User))
        Dim reader = ReaderQuery("SELECT ID, [name] FROM Users WHERE Username = '" & username & "'And [Password] = '" & password & "'")
        If reader.HasRows Then
            reader.Read()
            done(New User(username, reader.GetString(1), reader.GetInt32(0)))
        Else
            done(Nothing)
        End If
    End Sub

    ' Freunde/Chatverwaltung

    Public Sub GetAllUsers(id As Integer, done As Action(Of User()))
        Dim ignore As Integer() = getFriendIDs(id).Concat({id}).ToArray()
        done(getAll(ignore))
    End Sub
    Public Sub AddFriend(ID As Integer, ID2 As Integer, done As Action(Of User))

        If areFriends(ID, ID2) Then
            done(Nothing)
        Else
            Dim conn As New OleDbConnection(ConnectionStr)
            conn.Open()

            Dim insertCommand As New OleDbCommand("INSERT INTO Chats (UserID1, UserID2, Datum) VALUES (@UserID1,@UserID2,@Date);")
            insertCommand.Connection = conn
            insertCommand.Parameters.Add("@UserID1", OleDbType.Char).Value = ID
            insertCommand.Parameters.Add("@UserID2", OleDbType.Char).Value = ID2
            insertCommand.Parameters.Add("@Date", OleDbType.Date).Value = DateTime.Now
            insertCommand.CommandType = CommandType.Text
            Try
                insertCommand.ExecuteNonQuery()
            Catch ex As Exception
                Console.WriteLine(ex.Message)
                done(Nothing)
            Finally
                done(getUser(ID2))
            End Try
        End If
    End Sub

    Public Sub GetFriends(ID As Integer, done As Action(Of Chat()))
        done(getChats(ID))
    End Sub

    Public Sub SafeMessages(ID As Integer, Messages As String, done As Action(Of Boolean))

        Dim conn As New OleDbConnection(ConnectionStr)
        conn.Open()

        Dim insertcommand As New OleDbCommand("INSERT INTO Messages (UserID, Messages, Datum) VALUES @UserID, @Messages, @Datum);")
        insertcommand.Connection = conn
        insertcommand.Parameters.Add("@UserID", OleDbType.Char).Value = ID
        insertcommand.Parameters.Add("@Messages", OleDbType.Char).Value = Messages
        insertcommand.Parameters.Add("@Datum", OleDbType.Date).Value = DateTime.Now
        insertcommand.CommandType = CommandType.Text

        Try
            insertcommand.ExecuteNonQuery()
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            done(False)
        Finally
            done(True)
        End Try

    End Sub

    Public Sub getMessages(ChatID As Integer, done As Action(Of Message()))

        Dim conn As New OleDbConnection(ConnectionStr)
        conn.Open()
        Dim reader = ReaderQuery("SELECT Message, Datum, UserID FROM Messages WHERE ChatID = '" & ChatID & "'")
        Dim messages As New List(Of Message)

        Do While reader.Read

            Dim msg As New Message(getUser(reader.GetInt32(2)), reader.GetString(1), reader.GetString(0))
            messages.Add(msg)

        Loop
        done(messages.ToArray)

    End Sub
End Module