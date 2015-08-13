Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Public Class Test
    <Key>
    Public Property TestId As Int32
    Public Property TestRun As Boolean
    Public Property Sport As String
    Public Property PoolAlias As String
    Public Property CronJob As String

End Class
