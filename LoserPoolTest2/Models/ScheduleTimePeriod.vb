Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Imports WpfApplication1.LosersPool.Models

Public Class ScheduleTimePeriod

    <Key>
    Public Property TimePeriodId As Int32
    Public Property TimePeriod As String
    Public Property TimePeriodStartTime As String
    Public Property TimePeriodStartDate As String
    Public Property startGameTime As String
    Public Property startGameDate As String
    Public Property TimePeriodEndTime As String
    Public Property TimePeriodEndDate As String
    Public Property Sport As String
    Public Property PoolAlias As String
    Public Property CronJob As String

End Class

Public Class CreateSchedulePeriod


    Public Sub New(sport As String, timePeriodName As String, seasonStartTime As String, seasonStartDate As String, cronJobName As String)
        Dim _dbLoserPool3 As New LosersPoolContext

        Try
            Using (_dbLoserPool3)

                Dim queryScheduleTimePeriod = (From timePeriod1 In _dbLoserPool3.ScheduleTimePeriods
                                               Where timePeriod1.CronJob = cronJobName).ToList

                If queryScheduleTimePeriod.Count > 0 Then
                    For Each timePeriod1 In queryScheduleTimePeriod
                        _dbLoserPool3.ScheduleTimePeriods.Remove(timePeriod1)
                    Next
                End If
                _dbLoserPool3.SaveChanges()

                Dim games = (From schedule1 In _dbLoserPool3.ScheduleEntities
                             Where schedule1.CronJob = cronJobName).ToList

                Dim TimePeriods = New Dictionary(Of String, String)
                For Each game In games

                    Dim timePeriod1 = game.TimePeriod
                    If Not (TimePeriods.ContainsKey(timePeriod1)) Then
                        TimePeriods.Add(timePeriod1, timePeriod1)
                    End If
                Next

                For Each timePeriod2 In TimePeriods

                    Dim TimePeriodGames = _dbLoserPool3.ScheduleEntities.Where(Function(dg) dg.TimePeriod = timePeriod2.Key And dg.CronJob = cronJobName).ToList

                    Dim minStartDate As String
                    minStartDate = TimePeriodGames(0).StartDate
                    For gamenum = 1 To TimePeriodGames.Count - 1
                        If TimePeriodGames(gamenum).StartDate < minStartDate Then
                            minStartDate = TimePeriodGames(gamenum).StartDate
                        End If
                    Next

                    Dim TimePeriodGames1 = _dbLoserPool3.ScheduleEntities.Where(Function(dg) dg.TimePeriod = timePeriod2.Key And dg.StartDate = minStartDate And dg.CronJob = cronJobName).ToList

                    Dim minStartTime As String
                    minStartTime = TimePeriodGames1(0).StartTime
                    For gamenum = 1 To TimePeriodGames1.Count - 1
                        If TimePeriodGames1(gamenum).StartTime < minStartTime Then
                            minStartTime = TimePeriodGames1(gamenum).StartTime
                        End If
                    Next

                    Dim scheduleTimePeriod = New ScheduleTimePeriod
                    scheduleTimePeriod.TimePeriod = timePeriod2.Key

                    If timePeriod2.Key = timePeriodName + "1" Then
                        scheduleTimePeriod.TimePeriodStartDate = seasonStartDate
                        scheduleTimePeriod.TimePeriodStartTime = seasonStartTime
                    End If

                    scheduleTimePeriod.startGameDate = minStartDate
                    scheduleTimePeriod.startGameTime = minStartTime
                    scheduleTimePeriod.Sport = sport
                    'scheduleTimePeriod.PoolAlias = poolAlias
                    scheduleTimePeriod.CronJob = cronJobName

                    _dbLoserPool3.ScheduleTimePeriods.Add(scheduleTimePeriod)
                    _dbLoserPool3.SaveChanges()

                    Dim dummy = "dummy"

                Next

            End Using

        Catch ex As Exception

        End Try


    End Sub


End Class

