Imports System.ComponentModel.DataAnnotations

Public Class DbRecipient
    <Key, ScaffoldColumn(False)>
    Public Property RecipientId As Integer
    <MaxLength(128)>
    Public Property Name As String
    <Required, MaxLength(250)>
    Public Property Address As String
End Class