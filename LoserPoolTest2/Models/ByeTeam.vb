Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Public Class ByeTeam

    <Key>
    Public Property ByeId As Int32
    Public Property TimePeriod As String
    Public Property TeamName As String
    Public Property PoolAlias As String
    Public Property Sport As String
    Public Property CronJob As String

End Class
