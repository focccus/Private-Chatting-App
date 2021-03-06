﻿Imports System.Net.Sockets
Imports System.Net
Imports System.Text.Encoding
Imports System.ComponentModel

Friend Class ServerConnector
    Private serverSocket As TcpListener
    ' Liste aller verbundener Clients
    Private ReadOnly _sockets As New List(Of TcpClient)

    ' Getter für Client Array
    Public ReadOnly Property Sockets() As TcpClient()
        Get
            Return _sockets.ToArray()
        End Get
    End Property

    ' recieve Event
    Public ReadOnly OnRecieve As EventNotifier(Of ConnectionData, TcpClient) = New EventNotifier(Of ConnectionData, TcpClient)

    ' connect Event
    Public ReadOnly OnConnection As EventNotifier(Of TcpClient) = New EventNotifier(Of TcpClient)
    ' disconnect Event
    Public ReadOnly OnClose As EventNotifier(Of TcpClient) = New EventNotifier(Of TcpClient)

    Private Ip As String = "0.0.0.0"
    Private Port As Integer = 8080

    ' Konstruktoren
    Public Sub New()

    End Sub
    Public Sub New(_port As Integer)
        Port = _port
    End Sub
    Public Sub New(_Ip As String, _Port As Integer)
        Ip = _Ip
        Port = _Port
    End Sub

    ' Verbinden(mit IP und Port)
    Public Sub connect()

        ' Neuer Listener
        serverSocket = New TcpListener(IPAddress.Parse(Ip), Port)
        ' Startet Server
        serverSocket.Start()
        Console.WriteLine("Server läuft auf " & Ip & ":" & Port)
        While True
            ' sucht nach neuem client
            Dim clientSocket As TcpClient = serverSocket.AcceptTcpClient()
            ' fügt client zu der Liste hinzu
            OnConnection.Notify(clientSocket)

            _sockets.Add(clientSocket)

            ' startet neuen Threat, der auf Nachricht wartet
            RecieveThread(clientSocket)

        End While

    End Sub

    Private Sub RecieveThread(client As TcpClient)
        Dim worker As New BackgroundWorker
        AddHandler worker.DoWork, AddressOf RecieveThreadWork
        AddHandler worker.RunWorkerCompleted, Sub(sender As BackgroundWorker, e As RunWorkerCompletedEventArgs)
                                                  RecieveThreadCompleted(sender, e, client)
                                              End Sub

        worker.RunWorkerAsync(client)
    End Sub
    Private Sub RecieveThreadWork(sender As BackgroundWorker, e As DoWorkEventArgs)
        Try
            e.Result = recieve(e.Argument)
        Catch ex As Exception
            e.Result = Nothing
        End Try

    End Sub

    Private Sub RecieveThreadCompleted(sender As BackgroundWorker, e As RunWorkerCompletedEventArgs, client As TcpClient)

        If (e.Result Is Nothing) Then
            ' Verbindung abbrechen
            closeConnection(client)
        Else
            ' Ergebnis weitergeben
            OnRecieve.Notify(e.Result, client)
            ' Nächsten Thread starten
            RecieveThread(client)

        End If

    End Sub

    ' sendet eine Nachricht an einen Client
    Public Async Sub send(reciever As TcpClient, msg As ConnectionData)
        Dim networkStream As NetworkStream = reciever.GetStream()
        Dim data As Byte() = msg.Serialize()
        networkStream.Write(data, 0, data.Length)
        Await networkStream.FlushAsync()
    End Sub


    ' sendet eine Nachricht an mehrere Clients
    Public Sub send(recievers As TcpClient(), msg As ConnectionData)
        For Each client As TcpClient In recievers
            send(client, msg)
        Next
    End Sub

    ' sendet eine Nachricht an alle Clients
    Public Sub sendAll(msg As ConnectionData)
        send(Sockets.ToArray(), msg)
    End Sub

    ' sendet eine Nachricht an mehrere Clients
    Public Sub sendAndRecieve(recievers As TcpClient(), msg As ConnectionData, recieveHandler As Action(Of ConnectionData, TcpClient))
        OnRecieve.addHandler(
            Sub(str As ConnectionData, client As TcpClient)
                recieveHandler(str, client)
                OnRecieve.removeHandler(recieveHandler)
            End Sub
            )
        For Each client As TcpClient In recievers
            send(client, msg)
        Next
    End Sub

    ' empfängt eine Nachricht asynchron
    Private Function recieve(sender As TcpClient) As ConnectionData
        Dim serverStream As NetworkStream = sender.GetStream()
        Dim inStream(sender.ReceiveBufferSize) As Byte
        serverStream.Read(inStream, 0, sender.ReceiveBufferSize)
        ' Nachrichten einlesen
        Return ConnectionData.Serialized(inStream)
    End Function

    ' beendet eine Verbindung
    Public Sub closeConnection(client As TcpClient)
        OnClose.Notify(client)
        _sockets.Remove(client)
        client.Close()
        client.Dispose()
    End Sub

    ' beendet alle Verbindungen
    Public Sub closeConnections()
        For Each client As TcpClient In Sockets
            closeConnection(client)
        Next
    End Sub

    ' beendet alle Verbindungen und stoppt Server
    Public Sub disconnect()
        closeConnections()
        serverSocket.Stop()
    End Sub

End Class
