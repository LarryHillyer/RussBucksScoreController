Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Imports System.Linq
Imports System.Xml.Linq


Public Class ScheduledGame
    <Key>
    Public Property ScheduleId As Int32
    Public Property GameId As String
    Public Property HomeTeam As String
    Public Property AwayTeam As String
    Public Property StartTime As String
    Public Property StartDate As String
    Public Property HomeScore As String
    Public Property AwayScore As String
    Public Property GameTime As String
    Public Property GameDate As String
    Public Property DisplayStatus1 As String
    Public Property DisplayStatus2 As String
    Public Property Status As String
    Public Property Sport As String
    Public Property PoolAlias As String
    Public Property CronJob As String
    Public Property GameCode As String
    Public Property OriginalStartDate As String
    Public Property OriginalStartTime As String
    Public Property StartDateTime As Date
    Public Property RescheduledGame As Boolean
    Public Property MultipleGamesAreScheduled As Boolean
    Public Property MultipleGameNumber As String
    Public Property WinningTeam As String
    Public Property IsHomeTeamWinning As Boolean
    Public Property AreTeamsTied As Boolean
    Public Property SelectedForCustomSchedule As Boolean
    Public Property TimePeriod As String
    Public Property IsPreseason As Boolean

End Class

Public Class CustomScheduledGame
    <Key>
    Public Property ScheduleId As Int32
    Public Property GameId As String
    Public Property HomeTeam As String
    Public Property AwayTeam As String
    Public Property StartTime As String
    Public Property StartDate As String
    Public Property HomeScore As String
    Public Property AwayScore As String
    Public Property GameTime As String
    Public Property GameDate As String
    Public Property DisplayStatus1 As String
    Public Property DisplayStatus2 As String
    Public Property Status As String
    Public Property Sport As String
    Public Property PoolAlias As String
    Public Property CronJob As String
    Public Property GameCode As String
    Public Property OriginalStartDate As String
    Public Property OriginalStartTime As String
    Public Property StartDateTime As Date
    Public Property RescheduledGame As Boolean
    Public Property MultipleGamesAreScheduled As Boolean
    Public Property MultipleGameNumber As String
    Public Property WinningTeam As String
    Public Property IsHomeTeamWinning As Boolean
    Public Property AreTeamsTied As Boolean
    Public Property SelectedForCustomSchedule As Boolean
    Public Property TimePeriod As String
    Public Property IsPreseason As Boolean

End Class
Public Class QueuedScheduleGame

    <Key>
    Public Property ScheduleId As Int32
    Public Property GameId As String
    Public Property HomeTeam As String
    Public Property AwayTeam As String
    Public Property StartTime As String
    Public Property StartDate As String
    Public Property HomeScore As String
    Public Property AwayScore As String
    Public Property GameTime As String
    Public Property GameDate As String
    Public Property DisplayStatus1 As String
    Public Property DisplayStatus2 As String
    Public Property Status As String
    Public Property Sport As String
    Public Property PoolAlias As String
    Public Property CronJob As String
    Public Property GameCode As String
    Public Property OriginalStartDate As String
    Public Property OriginalStartTime As String
    Public Property StartDateTime As Date
    Public Property RescheduledGame As Boolean
    Public Property MultipleGamesAreScheduled As Boolean
    Public Property MultipleGameNumber As String
    Public Property WinningTeam As String
    Public Property IsHomeTeamWinning As Boolean
    Public Property AreTeamsTied As Boolean
    Public Property SelectedForCustomSchedule As Boolean
    Public Property TimePeriod As String
    Public Property IsPreseason As Boolean


End Class