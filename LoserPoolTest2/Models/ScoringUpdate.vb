Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace LosersPool.Models



    Public Class ScoringUpdate

        <Key>
        Public Property scoringId As Int32
        Public Property filetime As String
        Public Property filedate As String
        Public Property TimePeriod As String
        Public Property gameId As String
        Public Property hometeam As String
        Public Property homescore As String
        Public Property awayteam As String
        Public Property awayscore As String
        Public Property gametime As String
        Public Property GameDate As String
        Public Property DisplayStatus1 As String
        Public Property DisplayStatus2 As String
        Public Property Status As String
        Public Property Sport As String
        Public Property CronJobName As String
        Public Property GameCode As String
        Public Property OriginalStartDate As String
        Public Property OriginalStartTime As String

    End Class

    Public Class CurrentScoringUpdate
        <Key>
        Public Property scoringId As Int32
        Public Property filetime As String
        Public Property filedate As String
        Public Property TimePeriod As String
        Public Property gameId As String
        Public Property hometeam As String
        Public Property homescore As Int32
        Public Property awayteam As String
        Public Property awayscore As Int32
        Public Property gametime As String
        Public Property GameDate As String
        Public Property DisplayStatus1 As String
        Public Property DisplayStatus2 As String
        Public Property Status As String
        Public Property Sport As String
        Public Property CronJobName As String
        Public Property GameCode As String
        Public Property OriginalStartDate As String
        Public Property OriginalStartTime As String
    End Class

    Public Class PostponedGame

        <Key>
        Public Property scoringId As Int32
        Public Property filetime As String
        Public Property filedate As String
        Public Property TimePeriod As String
        Public Property gameId As String
        Public Property hometeam As String
        Public Property homescore As Int32
        Public Property awayteam As String
        Public Property awayscore As Int32
        Public Property gametime As String
        Public Property GameDate As String
        Public Property DisplayStatus1 As String
        Public Property DisplayStatus2 As String
        Public Property Status As String
        Public Property Sport As String
        Public Property CronJobName As String
        Public Property GameCode As String
        Public Property OriginalStartDate As String
        Public Property OriginalStartTime As String

    End Class

End Namespace
