Imports System.ComponentModel.DataAnnotations

Public Class DbRecipientResult
    <Key, ScaffoldColumn(False), Display(Name:="Result Id")>
    Public Property ResultId As Integer
    <Required, Display(Name:="Task Id")>
    Public Property TaskId As Integer
    <MaxLength(250)>
    Public Property Recipient As String
    Public Property Status As String
    <Display(Name:="Succeeded")>
    Public Property IsSucceeded As Boolean
    <Display(Name:="Date Time")>
    Public Property CreationTime As DateTime
End Class