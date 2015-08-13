Imports System
Imports System.Math
Imports System.Data
Imports System.Linq
Imports System.Xml.Linq
Imports System.Globalization

Imports System.Collections.Concurrent
Imports System.Collections.Generic
Imports System.IO
Imports System.IO.Path

Imports System.Threading
Imports System.Threading.Tasks

Imports WpfApplication1.JoinPools.Models
Imports WpfApplication1.LosersPool.Models

Class MainWindow

    Private Sports As New Dictionary(Of String, String)

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)

        Dim _dbApp As New ApplicationDbContext
        Dim _dbPools As New PoolDbContext
        Dim _dbLoserPool As New LosersPoolContext

        Try
            Using (_dbApp)
                Using (_dbPools)
                    Using (_dbLoserPool)

                        Dim queryParameters = (From param1 In _dbApp.AppFolders).Single

                        Dim rootFolder1 = queryParameters.scoreCronJobFolder + "\ScrapedFiles"
                        System.IO.Directory.SetCurrentDirectory(rootFolder1)

                        Dim cronJobName = My.Application.CronJobName1

                        Dim queryCronJob = (From cronJob2 In _dbApp.CronJobs
                                            Where cronJob2.CronJobName = cronJobName).Single

                        Dim queryCronJobPools = (From cronJobpool1 In _dbApp.CronJobPools
                                                 Where cronJobpool1.CronJobName = cronJobName).ToList

                        If queryCronJob.ContinueTestIsSelected = False Then
                            DeleteData(cronJobName)
                        End If

                        MW1.Title = "Scoring Update " + cronJobName

                        If queryCronJob.SelectedSport = "baseball" Then
                            Sports.Add("baseball", "baseball")
                        ElseIf queryCronJob.SelectedSport = "football" Then
                            Sports.Add("football", "football")
                        End If

                        For Each sport1a In Sports

                            For Each pool1 In queryCronJobPools

                                Dim queryTests = (From test1 In _dbPools.Tests).ToList

                                If queryTests.Count > 0 Then
                                    For Each test1 In queryTests
                                        _dbPools.Tests.Remove(test1)
                                    Next
                                End If

                                Dim test2 As New Test
                                test2.TestRun = False
                                test2.Sport = sport1a.Value
                                test2.PoolAlias = pool1.CronJobPoolAlias
                                test2.CronJob = cronJobName
                                _dbPools.Tests.Add(test2)
                                _dbPools.SaveChanges()

                            Next
                        Next

                        Dim seasonStartDate = DateTime.Parse(queryCronJob.SelectedSeasonStartDate).ToString("MM/dd/yy")
                        Dim seasonStartTime = "12:01 AM"
                        Dim seasonStartDateTime = DateTime.Parse(CDate(seasonStartDate) + " " + CDate("12:01 AM"))

                        Dim querySchedule4 = (From schedule1 In _dbPools.ScheduledGames
                         Where schedule1.StartDate = queryCronJob.SelectedSeasonStartGameDate And _
                         schedule1.Sport = queryCronJob.SelectedSport).ToList

                        Dim minGameStartDateTime = DateTime.Parse(queryCronJob.SelectedSeasonStartGameDate + " " + "11:59 PM")
                        For Each gamestart1 In querySchedule4
                            Dim gameStartDateTime2 = DateTime.Parse(gamestart1.StartDate + " " + gamestart1.StartTime)
                            If minGameStartDateTime > gameStartDateTime2 Then
                                minGameStartDateTime = gameStartDateTime2
                            End If
                        Next

                        Dim minDateTime = minGameStartDateTime.ToString("g")
                        Dim sp = minDateTime.IndexOf(" ")
                        Dim minDate = minDateTime.Substring(0, sp)
                        Dim minTime = minDateTime.Substring(sp + 1)
                        Dim minDateTime1 = DateTime.Parse(CDate(minDate) + " " + "12:01 AM")

                        Dim startGameDate = DateTime.Parse(queryCronJob.SelectedSeasonStartGameDate).ToString("MM/dd/yy")
                        Dim seasonStartGameDateTime = DateTime.Parse(CDate(startGameDate) + " " + minTime)
                        Dim seasonStartGameDateTime1 = DateTime.Parse(CDate(startGameDate) + " " + CDate("12:01 AM"))

                        Dim seasonEndGameDate = DateTime.Parse(queryCronJob.SelectedSeasonEndDate).ToString("MM/dd/yy")
                        Dim seasonEndGameDateTime = DateTime.Parse(CDate(seasonEndGameDate) + " " + CDate("11:59 PM"))

                        Dim sport = queryCronJob.SelectedSport
                        Dim filename = rootFolder1 + "\PythonScoreScrape.bat"

                        Dim queryPoolParams = (From poolParam1 In _dbPools.PoolParameters
                        Where poolParam1.CronJob = cronJobName).ToList

                        If queryCronJob.ContinueTestIsSelected = False Then

                            If queryCronJob.UserTestIsSelected = True Then

                                For Each pool1 In queryCronJobPools
                                    Dim queryUsersChoices = (From user1 In _dbLoserPool.UserChoicesList
                                                             Where user1.PoolAlias = pool1.CronJobPoolAlias).ToList

                                    If queryUsersChoices.Count > 0 Then
                                        For Each user1 In queryUsersChoices
                                            _dbLoserPool.UserChoicesList.Remove(user1)
                                        Next
                                    End If

                                    _dbLoserPool.SaveChanges()

                                    For Each sport1a In Sports
                                        Dim userChoices1 = New UserChoiceList(".\UserChoicesList" + sport1a.Value + ".xml", rootFolder1, cronJobName, pool1.CronJobPoolAlias)
                                    Next
                                Next
                            End If

                            _dbPools.SaveChanges() 'Needed?

                            Dim querySchedule5 = (From game1 In _dbLoserPool.ScheduleEntities
                                                    Where game1.CronJob = cronJobName).ToList

                            If querySchedule5.Count > 0 Then
                                For Each game1 In querySchedule5
                                    _dbLoserPool.ScheduleEntities.Remove(game1)
                                Next
                            End If

                            Dim queryTimePeriod3 = (From timeP1 In _dbLoserPool.ScheduleTimePeriods
                                                    Where timeP1.CronJob = cronJobName).ToList

                            If queryTimePeriod3.Count >= 0 Then
                                For Each timeP1 In queryTimePeriod3
                                    _dbLoserPool.ScheduleTimePeriods.Remove(timeP1)
                                Next
                            End If

                            _dbLoserPool.SaveChanges()


                            Dim sport1 = ""
                            'Dim timePeriod1 = ""
                            If sport = "baseball" Then
                                sport1 = "mlb"
                                'timePeriod1 = "day"

                                Dim querySchedule As New List(Of QueuedScheduleGame)

                                If queryCronJob.CustomScheduleIsSelected = False Then

                                    Dim querySchedule3 = (From schedule1 In _dbPools.ScheduledGames
                                                        Where schedule1.Sport = "baseball" And CDate(schedule1.StartDate) >= CDate(startGameDate) _
                                                        And CDate(schedule1.StartDate) <= CDate(seasonEndGameDate)
                                                        Order By schedule1.StartDate Ascending).ToList

                                    For Each game In querySchedule3

                                        Dim scheduleGame1 As New QueuedScheduleGame
                                        scheduleGame1.GameId = game.GameId
                                        scheduleGame1.HomeTeam = game.HomeTeam
                                        scheduleGame1.AwayTeam = game.AwayTeam
                                        scheduleGame1.HomeScore = game.HomeScore
                                        scheduleGame1.AwayScore = game.AwayScore
                                        scheduleGame1.Sport = game.Sport
                                        scheduleGame1.StartDate = game.StartDate
                                        scheduleGame1.StartTime = game.StartTime
                                        scheduleGame1.GameDate = game.GameDate
                                        scheduleGame1.GameTime = game.GameTime
                                        scheduleGame1.GameCode = game.GameCode
                                        scheduleGame1.DisplayStatus1 = game.DisplayStatus1
                                        scheduleGame1.DisplayStatus2 = game.DisplayStatus2
                                        scheduleGame1.OriginalStartDate = game.OriginalStartDate
                                        scheduleGame1.OriginalStartTime = game.OriginalStartTime
                                        scheduleGame1.MultipleGamesAreScheduled = game.MultipleGamesAreScheduled
                                        scheduleGame1.MultipleGameNumber = game.MultipleGameNumber
                                        scheduleGame1.RescheduledGame = game.RescheduledGame
                                        scheduleGame1.Status = game.Status
                                        scheduleGame1.StartDateTime = game.StartDateTime
                                        scheduleGame1.CronJob = cronJobName

                                        _dbPools.QueuedScheduledGames.Add(scheduleGame1)
                                    Next

                                    _dbPools.SaveChanges()
                                Else
                                    Dim querySchedule3 = (From schedule1 In _dbPools.CustomScheduledGames
                                                        Where schedule1.Sport = "baseball" And CDate(schedule1.StartDate) >= CDate(startGameDate) _
                                                        And CDate(schedule1.StartDate) <= CDate(seasonEndGameDate) And _
                                                        schedule1.CronJob = cronJobName
                                                        Order By schedule1.StartDate Ascending).ToList

                                    For Each game In querySchedule3

                                        Dim scheduleGame1 As New QueuedScheduleGame
                                        scheduleGame1.GameId = game.GameId
                                        scheduleGame1.HomeTeam = game.HomeTeam
                                        scheduleGame1.AwayTeam = game.AwayTeam
                                        scheduleGame1.HomeScore = game.HomeScore
                                        scheduleGame1.AwayScore = game.AwayScore
                                        scheduleGame1.Sport = game.Sport
                                        scheduleGame1.StartDate = game.StartDate
                                        scheduleGame1.StartTime = game.StartTime
                                        scheduleGame1.GameDate = game.GameDate
                                        scheduleGame1.GameTime = game.GameTime
                                        scheduleGame1.GameCode = game.GameCode
                                        scheduleGame1.DisplayStatus1 = game.DisplayStatus1
                                        scheduleGame1.DisplayStatus2 = game.DisplayStatus2
                                        scheduleGame1.OriginalStartDate = game.OriginalStartDate
                                        scheduleGame1.OriginalStartTime = game.OriginalStartTime
                                        scheduleGame1.MultipleGamesAreScheduled = game.MultipleGamesAreScheduled
                                        scheduleGame1.MultipleGameNumber = game.MultipleGameNumber
                                        scheduleGame1.RescheduledGame = game.RescheduledGame
                                        scheduleGame1.Status = game.Status
                                        scheduleGame1.StartDateTime = game.StartDateTime
                                        scheduleGame1.CronJob = cronJobName

                                        _dbPools.QueuedScheduledGames.Add(scheduleGame1)
                                    Next

                                    _dbPools.SaveChanges()

                                End If

                                querySchedule = (From schedule1 In _dbPools.QueuedScheduledGames
                                                  Where schedule1.Sport = "baseball" And CDate(schedule1.StartDate) >= CDate(startGameDate) _
                                                  And CDate(schedule1.StartDate) <= CDate(seasonEndGameDate) And _
                                                    schedule1.CronJob = cronJobName
                                                  Order By schedule1.StartDate Ascending).ToList

                                Dim cntI = queryPoolParams(0).timePeriodIncrement
                                For Each scheduleGame1 In querySchedule

                                    Dim gameDateTime = DateTime.Parse(CDate(scheduleGame1.StartDate) + " " + CDate("12:01 AM"))

                                    Dim dayDiff As New TimeSpan
                                    dayDiff = gameDateTime.Subtract(seasonStartGameDateTime1)
                                    Dim numDays = Floor(CDbl(dayDiff.TotalDays))
                                    Dim dayMod = numDays Mod CInt(queryPoolParams(0).timePeriodIncrement)

                                    Dim scheduleGame2 As New ScheduleEntity

                                    If dayMod = 0 Then

                                        scheduleGame2.GameId = scheduleGame1.GameId
                                        scheduleGame2.TimePeriod = queryPoolParams(0).timePeriodName + CStr(numDays + 1)
                                        scheduleGame2.HomeTeam = scheduleGame1.HomeTeam
                                        scheduleGame2.AwayTeam = scheduleGame1.AwayTeam
                                        scheduleGame2.HomeScore = scheduleGame1.HomeScore
                                        scheduleGame2.AwayScore = scheduleGame1.AwayScore
                                        scheduleGame2.Sport = scheduleGame1.Sport
                                        scheduleGame2.StartDate = scheduleGame1.StartDate
                                        scheduleGame2.StartTime = scheduleGame1.StartTime
                                        scheduleGame2.GameDate = scheduleGame1.GameDate
                                        scheduleGame2.GameTime = scheduleGame1.GameTime
                                        scheduleGame2.GameCode = scheduleGame1.GameCode
                                        scheduleGame2.DisplayStatus1 = scheduleGame1.DisplayStatus1
                                        scheduleGame2.DisplayStatus2 = scheduleGame2.DisplayStatus2
                                        scheduleGame2.OriginalStartDate = scheduleGame1.OriginalStartDate
                                        scheduleGame2.OriginalStartTime = scheduleGame1.OriginalStartTime
                                        scheduleGame2.MultipleGamesAreScheduled = scheduleGame1.MultipleGamesAreScheduled
                                        scheduleGame2.MultipleGameNumber = scheduleGame1.MultipleGameNumber
                                        scheduleGame2.RescheduledGame = scheduleGame1.RescheduledGame
                                        scheduleGame2.Status = scheduleGame1.Status
                                        scheduleGame2.StartDateTime = scheduleGame1.StartDateTime
                                        scheduleGame2.CronJob = scheduleGame1.CronJob

                                        If queryPoolParams(0).poolName = "LoserPool" Then
                                            _dbLoserPool.ScheduleEntities.Add(scheduleGame2)
                                        Else

                                        End If

                                    End If

                                Next

                                _dbLoserPool.SaveChanges()

                                For Each game In querySchedule
                                    _dbPools.QueuedScheduledGames.Remove(game)
                                Next

                                _dbPools.SaveChanges()

                            ElseIf sport = "football" Then
                                sport1 = "nfl"
                                'timePeriod1 = "week"

                                Dim querySchedule As New List(Of QueuedScheduleGame)

                                If queryCronJob.CustomScheduleIsSelected = False Then

                                    Dim querySchedule3 = (From schedule1 In _dbPools.ScheduledGames
                                                        Where schedule1.Sport = "football" And schedule1.IsPreseason = queryCronJob.CronJobIsPreseason And _
                                                        CDate(schedule1.StartDate) >= CDate(startGameDate) And CDate(schedule1.StartDate) <= CDate(seasonEndGameDate)
                                                        Order By schedule1.StartDateTime Ascending).ToList

                                    For Each game In querySchedule3

                                        Dim gameDate = game.StartDateTime.Date

                                        Dim queryWeekNum = (From date1 In _dbPools.SportDatesOfTheWeeks
                                                            Where date1.Date1 = gameDate And date1.Sport = "NFL").Single

                                        Dim scheduleGame1 As New QueuedScheduleGame
                                        scheduleGame1.GameId = game.GameId
                                        scheduleGame1.HomeTeam = game.HomeTeam
                                        scheduleGame1.AwayTeam = game.AwayTeam
                                        scheduleGame1.HomeScore = game.HomeScore
                                        scheduleGame1.AwayScore = game.AwayScore
                                        scheduleGame1.Sport = game.Sport
                                        scheduleGame1.StartDate = game.StartDate
                                        scheduleGame1.StartTime = game.StartTime
                                        scheduleGame1.GameDate = game.GameDate
                                        scheduleGame1.GameTime = game.GameTime
                                        scheduleGame1.GameCode = game.GameCode
                                        scheduleGame1.DisplayStatus1 = game.DisplayStatus1
                                        scheduleGame1.DisplayStatus2 = game.DisplayStatus2
                                        scheduleGame1.OriginalStartDate = game.OriginalStartDate
                                        scheduleGame1.OriginalStartTime = game.OriginalStartTime
                                        scheduleGame1.MultipleGamesAreScheduled = game.MultipleGamesAreScheduled
                                        scheduleGame1.MultipleGameNumber = game.MultipleGameNumber
                                        scheduleGame1.RescheduledGame = game.RescheduledGame
                                        scheduleGame1.Status = game.Status
                                        scheduleGame1.StartDateTime = game.StartDateTime
                                        scheduleGame1.CronJob = cronJobName
                                        scheduleGame1.TimePeriod = queryWeekNum.WeekNumber
                                        scheduleGame1.IsPreseason = queryCronJob.CronJobIsPreseason

                                        _dbPools.QueuedScheduledGames.Add(scheduleGame1)
                                    Next

                                    _dbPools.SaveChanges()
                                Else
                                    Dim querySchedule3 = (From schedule1 In _dbPools.CustomScheduledGames
                                                        Where schedule1.Sport = "football" And schedule1.IsPreseason = queryCronJob.CronJobIsPreseason And _
                                                        CDate(schedule1.StartDate) >= CDate(startGameDate) And CDate(schedule1.StartDate) <= CDate(seasonEndGameDate) _
                                                        And schedule1.CronJob = cronJobName
                                                        Order By schedule1.StartDateTime Ascending).ToList

                                    For Each game In querySchedule3

                                        Dim gameDate = game.StartDateTime.Date

                                        Dim queryWeekNum = (From date1 In _dbPools.SportDatesOfTheWeeks
                                                            Where date1.Date1 = gameDate And date1.Sport = "NFL").Single


                                        Dim scheduleGame1 As New QueuedScheduleGame
                                        scheduleGame1.GameId = game.GameId
                                        scheduleGame1.HomeTeam = game.HomeTeam
                                        scheduleGame1.AwayTeam = game.AwayTeam
                                        scheduleGame1.HomeScore = game.HomeScore
                                        scheduleGame1.AwayScore = game.AwayScore
                                        scheduleGame1.Sport = game.Sport
                                        scheduleGame1.StartDate = game.StartDate
                                        scheduleGame1.StartTime = game.StartTime
                                        scheduleGame1.GameDate = game.GameDate
                                        scheduleGame1.GameTime = game.GameTime
                                        scheduleGame1.GameCode = game.GameCode
                                        scheduleGame1.DisplayStatus1 = game.DisplayStatus1
                                        scheduleGame1.DisplayStatus2 = game.DisplayStatus2
                                        scheduleGame1.OriginalStartDate = game.OriginalStartDate
                                        scheduleGame1.OriginalStartTime = game.OriginalStartTime
                                        scheduleGame1.MultipleGamesAreScheduled = game.MultipleGamesAreScheduled
                                        scheduleGame1.MultipleGameNumber = game.MultipleGameNumber
                                        scheduleGame1.RescheduledGame = game.RescheduledGame
                                        scheduleGame1.Status = game.Status
                                        scheduleGame1.StartDateTime = game.StartDateTime
                                        scheduleGame1.CronJob = cronJobName
                                        scheduleGame1.TimePeriod = queryWeekNum.WeekNumber
                                        scheduleGame1.IsPreseason = queryCronJob.CronJobIsPreseason

                                        _dbPools.QueuedScheduledGames.Add(scheduleGame1)
                                    Next

                                    _dbPools.SaveChanges()

                                End If

                                querySchedule = (From schedule1 In _dbPools.QueuedScheduledGames
                                                  Where schedule1.Sport = "football" And schedule1.IsPreseason = queryCronJob.CronJobIsPreseason _
                                                  And CDate(schedule1.StartDate) >= CDate(startGameDate) And CDate(schedule1.StartDate) <= CDate(seasonEndGameDate) _
                                                  And schedule1.CronJob = cronJobName
                                                  Order By schedule1.StartDateTime Ascending).ToList

                                Dim cntI = queryPoolParams(0).timePeriodIncrement
                                For Each scheduleGame1 In querySchedule


                                    Dim scheduleGame2 As New ScheduleEntity

                                    scheduleGame2.GameId = scheduleGame1.GameId
                                    scheduleGame2.HomeTeam = scheduleGame1.HomeTeam
                                    scheduleGame2.AwayTeam = scheduleGame1.AwayTeam
                                    scheduleGame2.HomeScore = scheduleGame1.HomeScore
                                    scheduleGame2.AwayScore = scheduleGame1.AwayScore
                                    scheduleGame2.Sport = scheduleGame1.Sport
                                    scheduleGame2.StartDate = scheduleGame1.StartDate
                                    scheduleGame2.StartTime = scheduleGame1.StartTime
                                    scheduleGame2.GameDate = scheduleGame1.GameDate
                                    scheduleGame2.GameTime = scheduleGame1.GameTime
                                    scheduleGame2.GameCode = scheduleGame1.GameCode
                                    scheduleGame2.DisplayStatus1 = scheduleGame1.DisplayStatus1
                                    scheduleGame2.DisplayStatus2 = scheduleGame2.DisplayStatus2
                                    scheduleGame2.OriginalStartDate = scheduleGame1.OriginalStartDate
                                    scheduleGame2.OriginalStartTime = scheduleGame1.OriginalStartTime
                                    scheduleGame2.MultipleGamesAreScheduled = scheduleGame1.MultipleGamesAreScheduled
                                    scheduleGame2.MultipleGameNumber = scheduleGame1.MultipleGameNumber
                                    scheduleGame2.RescheduledGame = scheduleGame1.RescheduledGame
                                    scheduleGame2.Status = scheduleGame1.Status
                                    scheduleGame2.StartDateTime = scheduleGame1.StartDateTime
                                    scheduleGame2.CronJob = scheduleGame1.CronJob
                                    scheduleGame2.TimePeriod = scheduleGame1.TimePeriod

                                    If scheduleGame1.IsPreseason = False Then
                                        scheduleGame2.seasonPhase = "Regular"
                                    Else
                                        scheduleGame2.seasonPhase = "Preseason"
                                    End If

                                    If queryPoolParams(0).poolName = "LoserPool" Then
                                        _dbLoserPool.ScheduleEntities.Add(scheduleGame2)
                                    Else

                                    End If

                                Next

                                _dbLoserPool.SaveChanges()

                                For Each game In querySchedule
                                    _dbPools.QueuedScheduledGames.Remove(game)
                                Next

                                _dbPools.SaveChanges()


                            End If

                            Dim querySchedule1 = (From game1 In _dbLoserPool.ScheduleEntities
                                                 Where game1.CronJob = cronJobName).ToList

                            Dim maxTimePeriod = 1
                            Dim charposition = 4
                            If sport = "baseball" Then
                                charposition = 4
                            ElseIf sport = "football" Then
                                charposition = 5
                            End If

                            For Each game1 In querySchedule1
                                Dim TimePeriod = CInt(Mid(game1.TimePeriod, charposition))
                                If TimePeriod > maxTimePeriod Then
                                    maxTimePeriod = TimePeriod
                                End If
                            Next

                            queryPoolParams(0).maxTimePeriod = maxTimePeriod
                            _dbPools.SaveChanges()

                            ' Will populate schedule time period table if it needs it
                            Dim scheduleTimePeriod1 = New CreateSchedulePeriod(sport, queryPoolParams(0).timePeriodName, _
                                                                               queryPoolParams(0).seasonStartTime, queryPoolParams(0).seasonStartDate, cronJobName)

                            Dim queryTeams = (From team1 In _dbPools.Teams
                                                Where team1.Sport = sport And team1.TeamName <> "dummy").ToList

                            Dim queryScheduleTimePeriod = (From tP In _dbLoserPool.ScheduleTimePeriods
                                                           Where tP.CronJob = cronJobName).ToList

                            For Each timePeriod2 In queryScheduleTimePeriod
                                Dim thisTimePeriod = timePeriod2.TimePeriod

                                Dim querySchedule6 = (From schedule1 In _dbLoserPool.ScheduleEntities
                                                     Where schedule1.TimePeriod = thisTimePeriod And schedule1.CronJob = cronJobName
                                                     Select schedule1)

                                For Each team1 In queryTeams

                                    Dim byeTeam1 = (From schedule1 In querySchedule6
                                                      Where schedule1.HomeTeam = team1.TeamName Or schedule1.AwayTeam = team1.TeamName).ToList

                                    If byeTeam1.Count > 0 Then
                                    Else
                                        Dim byeTeam2 As New ByeTeam
                                        byeTeam2.TimePeriod = thisTimePeriod
                                        byeTeam2.TeamName = team1.TeamName
                                        byeTeam2.Sport = sport
                                        byeTeam2.CronJob = cronJobName

                                        _dbLoserPool.ByeTeamsList.Add(byeTeam2)
                                    End If
                                Next
                            Next

                            _dbLoserPool.SaveChanges()
                        Else


                        End If


                        Dim currentDateTime = DateTime.Now

                        If currentDateTime > seasonStartDateTime Then


                            Dim wUSS As New WaitUntilSeasonStart(seasonStartDateTime, queryCronJob, queryPoolParams(0))

                            Dim wUSSThread As New Thread(AddressOf wUSS.wUSS_Sleep)
                            wUSSThread.IsBackground = True
                            wUSSThread.Start()

                        Else

                        End If

                    End Using
                End Using
            End Using
        Catch ex As Exception

        End Try

    End Sub


    Private Sub DeleteData(cronJobName As String)

        Dim _dbLoserPool4 = New LosersPoolContext
        Dim _dbApp4 = New ApplicationDbContext
        Try
            Using (_dbLoserPool4)
                Using (_dbApp4)

                    Dim queryCronJob2 = (From qCJ2 In _dbApp4.CronJobs
                                         Where qCJ2.CronJobName = cronJobName).Single

                    If queryCronJob2.UserTestIsSelected = True And queryCronJob2.ContinueTestIsSelected = False Then
                        Dim queryUsersChoices = (From user1 In _dbLoserPool4.UserChoicesList
                         Where user1.CronJob = cronJobName).ToList

                        If queryUsersChoices.Count > 0 Then
                            For Each user1 In queryUsersChoices
                                _dbLoserPool4.UserChoicesList.Remove(user1)
                            Next
                        End If
                    End If


                    Dim queryTimePeriods = (From user1 In _dbLoserPool4.ScheduleTimePeriods
                                            Where user1.CronJob = cronJobName).ToList

                    If queryTimePeriods.Count > 0 Then
                        For Each timeperiod1 In queryTimePeriods
                            _dbLoserPool4.ScheduleTimePeriods.Remove(timeperiod1)
                        Next
                    End If

                    Dim querySchedule = (From game1 In _dbLoserPool4.ScheduleEntities
                                         Where game1.CronJob = cronJobName).ToList

                    If querySchedule.Count > 0 Then
                        For Each game1 In querySchedule
                            _dbLoserPool4.ScheduleEntities.Remove(game1)
                        Next
                    End If

                    Dim queryLosers = (From game1 In _dbLoserPool4.LoserList
                                       Where game1.CronJob = cronJobName).ToList

                    If queryLosers.Count > 0 Then
                        For Each loser1 In queryLosers
                            _dbLoserPool4.LoserList.Remove(loser1)
                        Next
                    End If

                    Dim queryByeTeams = (From game1 In _dbLoserPool4.ByeTeamsList
                                         Where game1.CronJob = cronJobName).ToList

                    If queryByeTeams.Count > 0 Then
                        For Each byeteam1 In queryByeTeams
                            _dbLoserPool4.ByeTeamsList.Remove(byeteam1)
                        Next
                    End If

                    Dim queryCurrentScoringUpdate = (From game1 In _dbLoserPool4.CurrentScoringUpdates
                                                     Where game1.CronJobName = cronJobName).ToList

                    If queryCurrentScoringUpdate.Count > 0 Then
                        For Each cSU1 In queryCurrentScoringUpdate
                            _dbLoserPool4.CurrentScoringUpdates.Remove(cSU1)
                        Next
                    End If

                    Dim queryScoringUpdate = (From game1 In _dbLoserPool4.ScoringUpdates
                                              Where game1.CronJobName = cronJobName).ToList

                    If queryScoringUpdate.Count > 0 Then
                        For Each sU1 In queryScoringUpdate
                            _dbLoserPool4.ScoringUpdates.Remove(sU1)
                        Next
                    End If

                    Dim queryPostponedGames = (From game1 In _dbLoserPool4.PostponedGames
                                               Where game1.CronJobName = cronJobName).ToList

                    If queryPostponedGames.Count > 0 Then
                        For Each game1 In queryPostponedGames
                            _dbLoserPool4.PostponedGames.Remove(game1)
                        Next
                    End If

                    Dim queryDeletedGames = (From game1 In _dbLoserPool4.DeletedGames
                               Where game1.CronJob = cronJobName).ToList

                    If queryDeletedGames.Count > 0 Then
                        For Each game1 In queryDeletedGames
                            _dbLoserPool4.DeletedGames.Remove(game1)
                        Next
                    End If

                    Dim queryUserPicks = (From qUP1 In _dbLoserPool4.UserPicks
                                          Where qUP1.CronJobName = cronJobName).ToList

                    If queryUserPicks.Count > 0 Then
                        For Each game1 In queryUserPicks
                            _dbLoserPool4.UserPicks.Remove(game1)
                        Next
                    End If

                    _dbLoserPool4.SaveChanges()

                End Using
            End Using
        Catch ex As Exception

        End Try

    End Sub

End Class

Public Class WaitUntilSeasonStart

    Private TeamNameCollection As New Dictionary(Of String, String)
    Private TeamNameCollection2 As New Dictionary(Of String, String)

    Public Property CurrentTime As DateTime
    Public Property SeasonStartTime As DateTime
    Public Property SeasonStartGameTime As DateTime
    Public Property SeasonEndGameTime As DateTime
    Public Property Sport As String
    Public Property PoolName As String
    Public Property PoolAlias As String
    Public Property TimePeriodName As String
    Public Property TimePeriodIncrement As Int32
    Public Property CronJobName As String

    Public Sub New(seasonStartDateTime As DateTime, cronJob1 As CronJob, poolParam1 As PoolParameter)

        Me.SeasonStartTime = seasonStartDateTime
        Me.Sport = cronJob1.SelectedSport
        Me.TimePeriodName = poolParam1.timePeriodName
        Me.PoolName = poolParam1.poolName
        Me.PoolAlias = poolParam1.poolAlias
        Me.TimePeriodIncrement = CInt(poolParam1.timePeriodIncrement)
        Me.CronJobName = cronJob1.CronJobName

    End Sub
    Public Sub wUSS_Sleep()
        Try
            Dim _dbLoserPool2 As LosersPoolContext
            Dim _dbPools2 As PoolDbContext
            Dim _dbApp1 As ApplicationDbContext

            Dim dataLock1 As New Object

            SyncLock dataLock1
                _dbLoserPool2 = New LosersPoolContext
                _dbPools2 = New PoolDbContext
                _dbApp1 = New ApplicationDbContext
            End SyncLock

            Dim filename = "PythonScoreScrape.bat"

            Dim SeasonHasEnded As Boolean = False

            Using (_dbApp1)
                Using (_dbLoserPool2)
                    Using (_dbPools2)

                        Dim queryParameters3 = (From param1 In _dbApp1.AppFolders).Single

                        Dim rootFolder = queryParameters3.scoreCronJobFolder
                        System.IO.Directory.SetCurrentDirectory(rootFolder)
                        Dim scrapedFilesFolder = queryParameters3.scrapedFilesFolder

                        Dim queryTimePeriod = (From timeP1 In _dbLoserPool2.ScheduleTimePeriods
                                               Where timeP1.CronJob = Me.CronJobName
                                               Select timeP1.TimePeriod).ToList

                        Dim teams1 = (From teams2 In _dbPools2.Teams
                                      Where teams2.Sport = Me.Sport And teams2.TeamName <> "dummy").ToList

                        For Each team1 In teams1
                            TeamNameCollection.Add(team1.NickName, team1.TeamName)
                            TeamNameCollection2.Add(team1.TeamName, team1.NickName)
                        Next

                        Dim queryCronJobPools = (From qCJP1 In _dbApp1.CronJobPools
                                                 Where qCJP1.CronJobName = Me.CronJobName).ToList

                        Dim queryCronJobs = (From qCJ1 In _dbApp1.CronJobs
                                             Where qCJ1.CronJobName = Me.CronJobName).Single

                        Dim thisTimePeriod = Me.TimePeriodName + "1"
                        Dim cnt = 1
                        While queryTimePeriod.Contains(thisTimePeriod)

                            Dim timePeriod1 = (From scheduleTimePeriod1 In _dbLoserPool2.ScheduleTimePeriods
                                                    Where scheduleTimePeriod1.CronJob = Me.CronJobName And scheduleTimePeriod1.TimePeriod = thisTimePeriod).Single

                            Dim startGameDateTime = DateTime.Parse(timePeriod1.startGameDate + " " + timePeriod1.startGameTime)
                            Dim startTimePeriodDateTime = DateTime.Parse(timePeriod1.TimePeriodStartDate + " " + timePeriod1.TimePeriodStartTime)
                            Dim startGameDate = startGameDateTime.ToString("MM/dd/yy")

                            Dim currentDateTime = DateTime.Now


                            If currentDateTime < startGameDateTime Then

                                For Each pool1 In queryCronJobPools

                                    Dim queryPoolParam4 = (From poolParam4 In _dbPools2.PoolParameters
                                                             Where poolParam4.poolAlias = pool1.CronJobPoolAlias And poolParam4.CronJob = Me.CronJobName).Single

                                    queryPoolParam4.poolState = "Enter Picks"
                                    queryPoolParam4.TimePeriod = thisTimePeriod

                                    SyncLock dataLock1
                                        _dbPools2.SaveChanges()
                                    End SyncLock
                                Next

                                While currentDateTime < startGameDateTime
                                    Thread.Sleep(TimeSpan.FromMinutes(2))
                                    currentDateTime = DateTime.Now
                                End While

                            ElseIf currentDateTime > startGameDateTime Then

                                For Each pool1 In queryCronJobPools
                                    Dim queryPoolParam4 = (From poolParam4 In _dbPools2.PoolParameters
                                                            Where poolParam4.poolAlias = pool1.CronJobPoolAlias And poolParam4.CronJob = Me.CronJobName).Single

                                    queryPoolParam4.poolState = "Scoring Update"
                                    queryPoolParam4.TimePeriod = thisTimePeriod

                                    _dbPools2.SaveChanges()
                                Next

                                For Each pool1 In queryCronJobPools
                                    Dim queryPoolParam4 = (From qPP4 In _dbPools2.PoolParameters
                                                           Where qPP4.poolAlias = pool1.CronJobPoolAlias And qPP4.CronJob = Me.CronJobName).Single

                                    ContenderStatus.RealLosers(queryPoolParam4.TimePeriod, pool1.CronJobPoolAlias, Me.Sport, Me.CronJobName)
                                Next

                                Dim league = ""
                                If Sport = "baseball" Then
                                    league = "MLB"
                                ElseIf Sport = "football" Then
                                    league = "NFL"
                                End If

                                Dim GamesAreFinished As Boolean = False

                                While GamesAreFinished = False

                                    File.Delete(".\ScrapedFiles\" + filename)
                                    Thread.Sleep(2000)
                                    File.AppendAllText(".\ScrapedFiles\" + filename, "C:\Python27\python " + ".\ScrapedFiles\NBCScoreScrape.py " + " " + league + " " + Me.Sport + " " + startGameDate + " " + scrapedFilesFolder)

                                    Dim cnt1 = 1
RestartScrape:
                                    Dim myUpdate As New XDocument
                                    Try
                                        Dim myPythonScrape = Process.Start(".\ScrapedFiles\" + filename)

                                        While myPythonScrape.HasExited = False
                                            Thread.Sleep(2000)
                                        End While

                                        Thread.Sleep(2000)

                                        Try
                                            myUpdate = XDocument.Load(".\ScrapedFiles\" + "scoringUpdate" + Me.Sport + ".xml")

                                        Catch ex As Exception
                                            If cnt1 > 1 Then
                                                Exit Sub
                                            End If
                                            cnt1 += 2
                                            Thread.Sleep(10000)
                                            GoTo RestartScrape

                                        End Try

                                    Catch ex As Exception

                                        Dim myPythonScrape = Process.Start(".\ScrapedFiles\" + filename)

                                        While myPythonScrape.HasExited = False
                                            Thread.Sleep(2000)
                                        End While

                                        Thread.Sleep(2000)

                                        Try
                                            myUpdate = XDocument.Load(".\ScrapedFiles\" + "scoringUpdate" + Me.Sport + ".xml")

                                        Catch ex1 As Exception
                                            If cnt1 > 2 Then
                                                Exit Sub
                                            End If
                                            cnt1 += 1
                                            Thread.Sleep(10000)
                                            GoTo RestartScrape

                                        End Try

                                    End Try

                                    Dim queryDate = (From date1 In myUpdate.Descendants("score")
                                                     Select New ScheduleEntity With {.StartDate = date1.Attribute("filedate")}).ToList

                                    Dim fileDate = queryDate(0).StartDate

                                    Dim timePeriod = timePeriod1.TimePeriod

                                    Dim xATimePeriod As New XAttribute("TimePeriod", timePeriod)
                                    myUpdate.Element("score").Add(xATimePeriod)

                                    Dim score1 = (From s1 In myUpdate.Elements
                                                  Select s1).Single

                                    Dim games = (From g1 In score1.Elements
                                                 Select g1).ToList


                                    Dim queryGame = (From game In myUpdate.Descendants("score").Descendants("game")
                                                    Select New ScoringUpdate With {.hometeam = game.Attribute("hometeam").Value,
                                                    .homescore = game.Element("homescore").Value,
                                                    .awayteam = game.Attribute("awayteam").Value,
                                                    .awayscore = game.Element("awayscore").Value,
                                                    .GameCode = game.Attribute("gamecode").Value,
                                                    .GameDate = game.Element("gamedate").Value,
                                                    .gametime = game.Element("gametime").Value,
                                                    .DisplayStatus1 = game.Element("display_status1").Value,
                                                    .DisplayStatus2 = game.Element("display_status2").Value,
                                                    .Status = game.Element("status").Value}).ToList

                                    For Each game In queryGame

                                        Dim gameDate1 = Date.Parse(game.GameDate + "/15")
                                        Dim gameDate = gameDate1.Date.ToString("MM/dd/yyyy")

                                        Dim querySchedule3 = (From game1 In _dbLoserPool2.ScheduleEntities
                                                                Where game1.CronJob = CronJobName And game1.GameCode = game.GameCode _
                                                                And game1.GameDate = gameDate).SingleOrDefault

                                        If querySchedule3 Is Nothing Then
                                        Else
                                            querySchedule3.HomeTeam = TeamNameCollection(game.hometeam)
                                            querySchedule3.AwayTeam = TeamNameCollection(game.awayteam)

                                            If querySchedule3 Is Nothing And game.Status = "Pre-Game" Then

                                            ElseIf Not querySchedule3 Is Nothing And game.Status = "Pre-Game" Then

                                                querySchedule3.StartDate = gameDate
                                                querySchedule3.StartTime = game.gametime
                                                querySchedule3.GameDate = gameDate
                                                querySchedule3.GameTime = game.gametime
                                                querySchedule3.DisplayStatus1 = game.DisplayStatus1
                                                querySchedule3.DisplayStatus2 = game.DisplayStatus2
                                                querySchedule3.Status = game.Status
                                                querySchedule3.StartDateTime = DateTime.Parse(gameDate + " " + game.gametime)

                                                _dbLoserPool2.SaveChanges()

                                            ElseIf Not querySchedule3 Is Nothing And game.Status <> "Pre-Game" Then

                                                If game.homescore Is Nothing Or game.homescore = "" Then
                                                    querySchedule3.HomeScore = "0"
                                                Else
                                                    querySchedule3.HomeScore = game.homescore
                                                End If

                                                If game.awayscore Is Nothing Or game.awayscore = "" Then
                                                    querySchedule3.AwayScore = "0"
                                                Else
                                                    querySchedule3.AwayScore = game.awayscore
                                                End If

                                                querySchedule3.GameDate = gameDate
                                                querySchedule3.GameTime = game.gametime
                                                querySchedule3.DisplayStatus1 = game.DisplayStatus1
                                                querySchedule3.DisplayStatus2 = game.DisplayStatus2
                                                querySchedule3.Status = game.Status

                                                Dim homescore = CInt(game.homescore)
                                                Dim awayscore = CInt(game.awayscore)

                                                If homescore = awayscore Then
                                                    querySchedule3.WinningTeam = "tied"
                                                    querySchedule3.IsHomeTeamWinning = False
                                                    querySchedule3.AreTeamsTied = True
                                                ElseIf homescore > awayscore Then
                                                    querySchedule3.WinningTeam = game.hometeam
                                                    querySchedule3.IsHomeTeamWinning = True
                                                    querySchedule3.AreTeamsTied = False
                                                ElseIf homescore < awayscore Then
                                                    querySchedule3.WinningTeam = game.awayteam
                                                    querySchedule3.IsHomeTeamWinning = False
                                                    querySchedule3.AreTeamsTied = False
                                                End If

                                                _dbLoserPool2.SaveChanges()

                                                Try
                                                    Dim queryUserPicks = (From pick1 In _dbLoserPool2.UserPicks
                                                                        Where pick1.CronJobName = CronJobName And pick1.GameCode = game.GameCode).ToList

                                                    For Each user1 In queryUserPicks
                                                        If user1.GameCode = game.GameCode Then
                                                            If homescore = awayscore Then
                                                                user1.PickIsTied = True
                                                                user1.PickIsWinning = False
                                                                _dbLoserPool2.SaveChanges()
                                                            ElseIf homescore > awayscore Then
                                                                If user1.UserPick1 = game.hometeam Then
                                                                    user1.PickIsTied = False
                                                                    user1.PickIsWinning = False
                                                                    _dbLoserPool2.SaveChanges()
                                                                ElseIf user1.UserPick1 = game.awayteam Then
                                                                    user1.PickIsTied = False
                                                                    user1.PickIsWinning = True
                                                                    _dbLoserPool2.SaveChanges()
                                                                End If
                                                            ElseIf homescore < awayscore Then
                                                                If user1.UserPick1 = game.hometeam Then
                                                                    user1.PickIsTied = False
                                                                    user1.PickIsWinning = True
                                                                    _dbLoserPool2.SaveChanges()
                                                                ElseIf user1.UserPick1 = game.awayteam Then
                                                                    user1.PickIsTied = False
                                                                    user1.PickIsWinning = False
                                                                    _dbLoserPool2.SaveChanges()
                                                                End If
                                                            End If
                                                        End If
                                                    Next
                                                Catch ex As Exception

                                                End Try
                                            End If
                                        End If
                                    Next

                                    Dim queryCronJobPools1 = (From qCJP1 In _dbApp1.CronJobPools
                                                              Where qCJP1.CronJobName = CronJobName).ToList

                                    For Each pool1 In queryCronJobPools1

                                        Dim queryUserChoices1 = (From qUC1 In _dbLoserPool2.UserChoicesList
                                                                    Where qUC1.CronJob = CronJobName And qUC1.TimePeriod = thisTimePeriod And _
                                                                    qUC1.PoolAlias = pool1.CronJobPoolAlias And qUC1.Contender = True).ToList

                                        For Each user1 In queryUserChoices1
                                            Dim queryUserPicks1 = (From qUP1 In _dbLoserPool2.UserPicks
                                                                     Where qUP1.PoolAlias = pool1.CronJobPoolAlias And qUP1.UserID = user1.UserID).ToList

                                            Dim userIsWinning = False
                                            Dim userIsTied = True

                                            For Each pick1 In queryUserPicks1
                                                If pick1.PickIsTied = True Then
                                                ElseIf pick1.PickIsWinning = True Then
                                                    userIsTied = False
                                                    userIsWinning = True
                                                ElseIf pick1.PickIsWinning = False Then
                                                    userIsTied = False
                                                    userIsWinning = False
                                                    Exit For
                                                End If
                                            Next

                                            user1.UserIsWinning = userIsWinning
                                            user1.UserIsTied = userIsTied

                                            _dbLoserPool2.SaveChanges()
                                        Next
                                    Next

                                    FinalizeUpdateToDatabase(queryGame, myUpdate, Me.Sport, Me.CronJobName, TeamNameCollection)

                                    'For Each game1 In games
                                    'If game1.Elements("status").Value = "Final" Then
                                    'game1.Elements("status").Value = "final"
                                    'End If
                                    'Next

                                    'Dim queryStatus = (From game1 In myUpdate.Descendants("score").Descendants("game")
                                    'Select New ScheduleEntity With {.Status = game1.Elements("status").Value}).ToList

                                    Dim queryStatus = (From game1 In _dbLoserPool2.ScheduleEntities
                                                       Where game1.TimePeriod = thisTimePeriod And game1.CronJob = CronJobName).ToList

                                    GamesAreFinished = True

                                    For Each status1 In queryStatus
                                        If status1.Status <> "Final" And status1.Status <> "Postponed" Then
                                            GamesAreFinished = False
                                            Exit For
                                        End If
                                    Next

                                    Dim TimePeriodNum As String = Mid(timePeriod, Len(Me.TimePeriodName) + 1)

                                    If GamesAreFinished = True Then

                                        Dim querySchedule = (From schedule1 In _dbLoserPool2.ScheduleEntities
                                                             Where schedule1.Sport = Me.Sport).ToList

                                        Dim queryScoringUpdate = (From sU1 In _dbLoserPool2.CurrentScoringUpdates).ToList

                                        Dim queryTimePeriod1 = (From timePeriod2 In _dbLoserPool2.ScheduleTimePeriods
                                                        Where (timePeriod2.TimePeriod = Me.TimePeriodName + CStr(cnt) Or timePeriod2.TimePeriod = Me.TimePeriodName + CStr(cnt + Me.TimePeriodIncrement)) And timePeriod2.Sport = Me.Sport
                                                        Order By timePeriod2.TimePeriod).ToList

                                        queryTimePeriod1(0).TimePeriodEndDate = queryScoringUpdate(0).filedate
                                        queryTimePeriod1(0).TimePeriodEndTime = queryScoringUpdate(0).filetime

                                        If queryTimePeriod1.Count > 1 Then
                                            queryTimePeriod1(1).TimePeriodStartDate = queryScoringUpdate(0).filedate
                                            queryTimePeriod1(1).TimePeriodStartTime = queryScoringUpdate(0).filetime
                                        End If

                                        _dbLoserPool2.SaveChanges()

                                        For Each pool1 In queryCronJobPools
                                            ContenderStatus.UpdateContenderStatus(thisTimePeriod, queryScoringUpdate, thisTimePeriod, Me.TimePeriodIncrement, Me.PoolAlias, teams1, Me.Sport, Me.TimePeriodName, Me.CronJobName)
                                        Next

                                        cnt = cnt + Me.TimePeriodIncrement
                                        thisTimePeriod = Me.TimePeriodName + CStr(cnt)

                                    Else
                                        File.Delete(".\ScrapedFiles\" + "scoringUpdate" + Me.Sport + ".xml")
                                        Thread.Sleep(TimeSpan.FromMinutes(1))
                                    End If

                                End While

                                Dim queryPoolParam7 = (From poolParam7 In _dbPools2.PoolParameters
                                                Where poolParam7.poolAlias = Me.PoolAlias).Single

                                Dim dayNum = CInt(Mid(queryPoolParam7.TimePeriod, Len(queryPoolParam7.timePeriodName) + 1))
                                If dayNum > queryPoolParam7.maxTimePeriod Then
                                    SeasonHasEnded = True
                                    queryPoolParam7.TimePeriod = queryPoolParam7.timePeriodName + CStr(queryPoolParam7.maxTimePeriod)
                                    _dbPools2.SaveChanges()
                                End If

                                If SeasonHasEnded = True Then
                                    Exit While
                                End If

                            End If

                        End While

                        Dim queryPoolParam6 = (From poolParam6 In _dbPools2.PoolParameters
                                               Where poolParam6.poolAlias = Me.PoolAlias).Single

                        queryPoolParam6.poolState = "Season End"

                        _dbPools2.SaveChanges()


                    End Using
                End Using
            End Using
        Catch ex As Exception

        End Try
    End Sub


    Private Sub FinalizeUpdateToDatabase(queryGame As List(Of ScoringUpdate), myUpdate As XDocument, sport As String, cronJobName As String, TeamNameCollection As Dictionary(Of String, String))

        Dim _dbLoserPool8 As New LosersPoolContext
        Try
            Using (_dbLoserPool8)

                Dim queryCurrentScoringUpdates = (From update1 In _dbLoserPool8.CurrentScoringUpdates
                                                  Where update1.CronJobName = cronJobName).ToList

                If queryCurrentScoringUpdates.Count > 0 Then
                    For Each update1 In queryCurrentScoringUpdates
                        _dbLoserPool8.CurrentScoringUpdates.Remove(update1)
                    Next
                End If

                Dim queryTime1 = (From score In myUpdate.Descendants("score")
                Select New ScoringUpdate With {.filetime = score.Attribute("filetime"),
                                                .filedate = score.Attribute("filedate"),
                                                .TimePeriod = score.Attribute("TimePeriod")}).Single

                Dim cnt As Int16 = 1
                Dim ReorderGames As Boolean = False
                For Each game1 In queryGame

                    Dim gameDate1 = Date.Parse(game1.GameDate + "/15")
                    Dim gameDate = gameDate1.Date.ToString("MM/dd/yyyy")



                    Dim querySchedule1 = (From qS1 In _dbLoserPool8.ScheduleEntities
                                          Where qS1.CronJob = cronJobName And qS1.GameCode = game1.GameCode).SingleOrDefault



                    If game1.Status = "Postponed" Then

                        Dim queryPostponedGames = (From qPG1 In _dbLoserPool8.PostponedGames
                                                   Where qPG1.CronJobName = cronJobName And qPG1.GameCode = game1.GameCode And qPG1.TimePeriod = queryTime1.TimePeriod).SingleOrDefault

                        If queryPostponedGames Is Nothing Then

                            Dim queryUserChoices = (From qUC1 In _dbLoserPool8.UserChoicesList
                                                    Where qUC1.Contender = True And qUC1.PickedGameCode = game1.GameCode And qUC1.TimePeriod = queryTime1.TimePeriod).ToList

                            For Each user1 In queryUserChoices
                                user1.UserPickPostponed = True

                                Dim queryUserPick = (From qUP1 In _dbLoserPool8.UserPicks
                                                     Where qUP1.PoolAlias = user1.PoolAlias And qUP1.TimePeriod = queryTime1.TimePeriod And qUP1.GameCode = game1.GameCode).ToList

                                For Each pick1 In queryUserPick
                                    pick1.UserPickPostponed = True
                                    _dbLoserPool8.SaveChanges()
                                Next
                            Next

                            Dim pG1 As New PostponedGame

                            pG1.filedate = queryTime1.filedate
                            pG1.filetime = queryTime1.filetime
                            pG1.TimePeriod = queryTime1.TimePeriod
                            pG1.gameId = "game" + CStr(cnt)
                            pG1.GameCode = game1.GameCode
                            pG1.GameDate = gameDate
                            pG1.gametime = game1.gametime
                            pG1.hometeam = game1.hometeam
                            pG1.awayteam = game1.awayteam
                            pG1.homescore = game1.homescore
                            pG1.awayscore = game1.awayscore

                            If querySchedule1 Is Nothing Then
                            Else

                                Dim dG1 As New DeletedGame

                                dG1.TimePeriod = queryTime1.TimePeriod
                                dG1.GameId = "game" + CStr(cnt)
                                dG1.GameCode = game1.GameCode
                                dG1.GameDate = gameDate
                                dG1.GameTime = game1.gametime
                                dG1.HomeTeam = game1.hometeam
                                dG1.AwayTeam = game1.awayteam
                                dG1.HomeScore = game1.homescore
                                dG1.AwayScore = game1.awayscore
                                dG1.OriginalStartDate = querySchedule1.OriginalStartDate
                                dG1.OriginalStartTime = querySchedule1.OriginalStartTime
                                dG1.DisplayStatus1 = game1.DisplayStatus1
                                dG1.DisplayStatus2 = game1.DisplayStatus2
                                dG1.StartDateTime = DateTime.Parse(game1.GameDate + " " + game1.gametime)
                                dG1.RescheduledGame = querySchedule1.RescheduledGame
                                dG1.Sport = game1.Sport
                                dG1.Status = game1.Status
                                dG1.CronJob = cronJobName
                                dG1.WinningTeam = querySchedule1.WinningTeam
                                dG1.IsHomeTeamWinning = querySchedule1.IsHomeTeamWinning
                                dG1.AreTeamsTied = querySchedule1.AreTeamsTied

                                _dbLoserPool8.DeletedGames.Add(dG1)

                            End If

                            pG1.DisplayStatus1 = game1.DisplayStatus1
                            pG1.DisplayStatus2 = game1.DisplayStatus2

                            pG1.Sport = game1.Sport
                            pG1.Status = game1.Status
                            pG1.CronJobName = cronJobName

                            cnt = cnt + 1

                            _dbLoserPool8.PostponedGames.Add(pG1)
                            _dbLoserPool8.ScheduleEntities.Remove(querySchedule1)
                            _dbLoserPool8.SaveChanges()
                        End If

                    ElseIf game1.Status <> "Postponed" Then

                        Dim cSU1 As New CurrentScoringUpdate
                        Dim cSU2 As New ScoringUpdate

                        cSU1.filedate = queryTime1.filedate
                        cSU2.filedate = queryTime1.filedate
                        cSU1.filetime = queryTime1.filetime
                        cSU2.filetime = queryTime1.filetime
                        cSU1.TimePeriod = queryTime1.TimePeriod
                        cSU2.TimePeriod = queryTime1.TimePeriod

                        cSU1.gameId = "game" + CStr(cnt)
                        cSU2.gameId = "game" + CStr(cnt)

                        cSU1.hometeam = game1.hometeam
                        cSU2.hometeam = game1.hometeam
                        cSU1.awayteam = game1.awayteam
                        cSU2.awayteam = game1.awayteam
                        cSU1.homescore = game1.homescore
                        cSU2.homescore = game1.homescore
                        cSU1.awayscore = game1.awayscore
                        cSU2.awayscore = game1.awayscore
                        cSU1.gametime = game1.gametime
                        cSU2.gametime = game1.gametime
                        cSU1.GameDate = gameDate
                        cSU2.GameDate = gameDate
                        cSU1.GameCode = game1.GameCode
                        cSU2.GameCode = game1.GameCode

                        If querySchedule1 Is Nothing Then

                            Dim queryDeletedGame = (From qDG1 In _dbLoserPool8.DeletedGames
                                                    Where qDG1.CronJob = cronJobName And qDG1.GameCode = game1.GameCode).SingleOrDefault


                            If queryDeletedGame Is Nothing And game1.Status = "Pre-Game" Then

                            ElseIf Not queryDeletedGame Is Nothing And game1.Status = "Pre-Game" Then

                                Dim team1 = queryDeletedGame.HomeTeam
                                Dim team2 = queryDeletedGame.AwayTeam

                                Dim queryGameSchedule = (From qGS1 In _dbLoserPool8.ScheduleEntities
                                                         Where qGS1.CronJob = cronJobName And ((qGS1.HomeTeam = team1 And qGS1.AwayTeam = team2) Or _
                                                                                               (qGS1.HomeTeam = team2 And qGS1.AwayTeam = team1))).SingleOrDefault

                                ReorderGames = True
                                Dim scheduleGame1 As New ScheduleEntity

                                scheduleGame1.CronJob = queryDeletedGame.CronJob
                                scheduleGame1.GameCode = queryDeletedGame.GameCode
                                scheduleGame1.OriginalStartDate = queryDeletedGame.OriginalStartDate
                                scheduleGame1.OriginalStartTime = queryDeletedGame.OriginalStartTime
                                scheduleGame1.HomeTeam = queryDeletedGame.HomeTeam
                                scheduleGame1.HomeScore = queryDeletedGame.HomeScore
                                scheduleGame1.AwayTeam = queryDeletedGame.AwayTeam
                                scheduleGame1.AwayScore = queryDeletedGame.AwayScore
                                scheduleGame1.Sport = queryDeletedGame.Sport
                                scheduleGame1.GameDate = gameDate
                                scheduleGame1.GameTime = game1.gametime
                                scheduleGame1.StartDate = gameDate
                                scheduleGame1.StartTime = game1.gametime
                                scheduleGame1.DisplayStatus1 = game1.DisplayStatus1
                                scheduleGame1.DisplayStatus2 = game1.DisplayStatus2
                                scheduleGame1.Status = game1.Status
                                scheduleGame1.TimePeriod = queryTime1.TimePeriod
                                scheduleGame1.StartDateTime = DateTime.Parse(scheduleGame1.StartDate + " " + scheduleGame1.StartTime)
                                scheduleGame1.RescheduledGame = True
                                scheduleGame1.WinningTeam = queryDeletedGame.WinningTeam
                                scheduleGame1.IsHomeTeamWinning = queryDeletedGame.IsHomeTeamWinning
                                scheduleGame1.AreTeamsTied = queryDeletedGame.AreTeamsTied

                                If queryGameSchedule Is Nothing Then
                                    scheduleGame1.MultipleGamesAreScheduled = False
                                Else
                                    scheduleGame1.MultipleGamesAreScheduled = True
                                    If scheduleGame1.StartDateTime < queryGameSchedule.StartDateTime Then
                                        scheduleGame1.MultipleGameNumber = "1"
                                        queryGameSchedule.MultipleGamesAreScheduled = True
                                        queryGameSchedule.MultipleGameNumber = "2"
                                    Else
                                        queryGameSchedule.MultipleGamesAreScheduled = True
                                        queryGameSchedule.MultipleGameNumber = "1"
                                        scheduleGame1.MultipleGameNumber = "2"
                                    End If
                                End If

                                _dbLoserPool8.ScheduleEntities.Add(scheduleGame1)


                            End If

                        Else

                        End If

                        cSU1.DisplayStatus1 = game1.DisplayStatus1
                        cSU2.DisplayStatus1 = game1.DisplayStatus1
                        cSU1.DisplayStatus2 = game1.DisplayStatus2
                        cSU2.DisplayStatus2 = game1.DisplayStatus2
                        cSU1.Status = game1.Status
                        cSU2.Status = game1.Status
                        cSU1.Sport = sport
                        cSU2.Sport = sport
                        cSU1.CronJobName = cronJobName
                        cSU2.CronJobName = cronJobName

                        cnt = cnt + 1

                        _dbLoserPool8.CurrentScoringUpdates.Add(cSU1)
                        _dbLoserPool8.ScoringUpdates.Add(cSU2)
                        _dbLoserPool8.SaveChanges()

                    End If


                Next

                If ReorderGames = True Then
                    Dim querySchedule2 = (From game1 In _dbLoserPool8.ScheduleEntities
                      Where game1.CronJob = cronJobName And game1.TimePeriod = queryTime1.TimePeriod
                      Order By game1.StartDateTime Ascending).ToList

                    cnt = 1
                    For Each game1 In querySchedule2
                        game1.GameId = "game" + CStr(cnt)
                        cnt = cnt + 1
                        _dbLoserPool8.SaveChanges()
                    Next

                    For Each game1 In querySchedule2
                        _dbLoserPool8.ScheduleEntities.Remove(game1)
                    Next
                    _dbLoserPool8.SaveChanges()

                    Thread.Sleep(2500)

                    For Each game1 In querySchedule2
                        _dbLoserPool8.ScheduleEntities.Add(game1)
                    Next
                    _dbLoserPool8.SaveChanges()

                End If

            End Using

        Catch ex As Exception

        End Try
    End Sub

End Class