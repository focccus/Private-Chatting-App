﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Chats
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.btnAbmelden = New System.Windows.Forms.Button()
        Me.btnNeuerKontakt = New System.Windows.Forms.Button()
        Me.lblKontakte = New System.Windows.Forms.Label()
        Me.ltbKontakte = New System.Windows.Forms.ListBox()
        Me.ChatArea = New Forms.ChatArea()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnAbmelden
        '
        Me.btnAbmelden.Location = New System.Drawing.Point(9, 9)
        Me.btnAbmelden.Name = "btnAbmelden"
        Me.btnAbmelden.Size = New System.Drawing.Size(75, 23)
        Me.btnAbmelden.TabIndex = 13
        Me.btnAbmelden.Text = "Abmelden"
        Me.btnAbmelden.UseVisualStyleBackColor = True
        '
        'btnNeuerKontakt
        '
        Me.btnNeuerKontakt.Location = New System.Drawing.Point(9, 336)
        Me.btnNeuerKontakt.Margin = New System.Windows.Forms.Padding(2)
        Me.btnNeuerKontakt.Name = "btnNeuerKontakt"
        Me.btnNeuerKontakt.Size = New System.Drawing.Size(125, 19)
        Me.btnNeuerKontakt.TabIndex = 12
        Me.btnNeuerKontakt.Text = "+ Hinzufügen"
        Me.btnNeuerKontakt.UseVisualStyleBackColor = True
        '
        'lblKontakte
        '
        Me.lblKontakte.AutoSize = True
        Me.lblKontakte.Location = New System.Drawing.Point(6, 35)
        Me.lblKontakte.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblKontakte.Name = "lblKontakte"
        Me.lblKontakte.Size = New System.Drawing.Size(82, 13)
        Me.lblKontakte.TabIndex = 11
        Me.lblKontakte.Text = "Meine Kontakte"
        '
        'ltbKontakte
        '
        Me.ltbKontakte.FormattingEnabled = True
        Me.ltbKontakte.Location = New System.Drawing.Point(9, 50)
        Me.ltbKontakte.Margin = New System.Windows.Forms.Padding(2)
        Me.ltbKontakte.Name = "ltbKontakte"
        Me.ltbKontakte.Size = New System.Drawing.Size(125, 277)
        Me.ltbKontakte.TabIndex = 10
        '
        'ChatArea
        '
        Me.ChatArea.AutoSize = True
        Me.ChatArea.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ChatArea.Chat = Nothing
        Me.ChatArea.Dock = System.Windows.Forms.DockStyle.Right
        Me.ChatArea.Location = New System.Drawing.Point(263, 0)
        Me.ChatArea.Name = "ChatArea"
        Me.ChatArea.Size = New System.Drawing.Size(427, 450)
        Me.ChatArea.TabIndex = 14
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(102, 9)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(80, 23)
        Me.Button1.TabIndex = 15
        Me.Button1.Text = "Einstellungen"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Chats
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(690, 450)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.ChatArea)
        Me.Controls.Add(Me.btnAbmelden)
        Me.Controls.Add(Me.btnNeuerKontakt)
        Me.Controls.Add(Me.lblKontakte)
        Me.Controls.Add(Me.ltbKontakte)
        Me.Name = "Chats"
        Me.Text = "Chats"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ChatArea As ChatArea
    Friend WithEvents btnAbmelden As Button
    Friend WithEvents btnNeuerKontakt As Button
    Friend WithEvents lblKontakte As Label
    Friend WithEvents ltbKontakte As ListBox
    Friend WithEvents Button1 As Button
End Class