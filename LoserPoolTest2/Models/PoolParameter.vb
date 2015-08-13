Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema


Public Class PoolParameter

    <Key>
    Public Property poolId As Int32
    Public Property poolAlias As String
    Public Property poolName As String
    Public Property poolAdministrator As String
    Public Property poolState As String
    Public Property Sport As String
    Public Property seasonStartDate As String
    Public Property seasonStartTime As String
    Public Property seasonGameStart As String
    Public Property seasonEndDate As String
    Public Property TimePeriod As String
    Public Property timePeriodName As String
    Public Property timePeriodIncrement As String
    Public Property maxTimePeriod As Int32
    Public Property CronJob As String
    Public Property CustomSchedule As Boolean

End Class
