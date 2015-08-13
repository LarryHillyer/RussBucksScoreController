Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Imports WpfApplication1.LosersPool.Models
Imports WpfApplication1.JoinPools.Models

Namespace LosersPool.Models
    Public Class ScheduleEntity

        <Key>
        Public Property ScheduleId As Int32
        Public Property GameId As String
        Public Property TimePeriod As String
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
        Public Property CronJob As String
        Public Property PoolAlias As String
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
        Public Property seasonPhase As String

    End Class

    Public Class DeletedGame
        <Key>
        Public Property ScheduleId As Int32
        Public Property GameId As String
        Public Property TimePeriod As String
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
        Public Property CronJob As String
        Public Property PoolAlias As String
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
        Public Property seasonPhase As String

    End Class


    Public Class ReadScheduleFile

        Public Sub New(filecontrol As String, rootFolder As String, sport As String, cronJobName As String)

            Dim _dbLoserPool5 As New LosersPoolContext

            Try
                Dim querySchedule = (From schedule1 In _dbLoserPool5.ScheduleEntities
                                     Where schedule1.CronJob = cronJobName).ToList

                If querySchedule.Count > 0 Then
                    For Each game1 In querySchedule
                        _dbLoserPool5.ScheduleEntities.Remove(game1)
                    Next
                End If
                _dbLoserPool5.SaveChanges()

            Catch ex As Exception

            End Try

            System.IO.Directory.SetCurrentDirectory(rootFolder)

            If filecontrol = "onefile" Then
            ElseIf filecontrol = "manyfiles" Then

                Dim schedulefiles = XDocument.Load(".\TestFiles\scheduleDataFileList" + sport + ".xml")


                Dim ScheduleFileList = (From file1 In schedulefiles.Descendants("schedulefiles").Descendants("file")
                                    Select New ScheduleFileXML With {.FilePath = file1.Elements("filepath").Value}).ToList

                For Each schedule1 In ScheduleFileList
                    Dim pathname = schedule1.FilePath
                    ReadScheduleXMLFileAndWriteToScheduleEntities(pathname, cronJobName)
                Next

            End If

        End Sub

        Public Shared Sub ReadScheduleXMLFileAndWriteToScheduleEntities(pathname As String, cronJobName As String)

            Dim _dbLoserPool2 = New LosersPoolContext
            Dim _dbPools2 As New PoolDbContext

            Try
                Using (_dbLoserPool2)
                    Using (_dbPools2)

                        Dim myschedule = XDocument.Load(pathname)

                        Dim queryScheduleFile = (From schedule1 In myschedule.Descendants("schedule").Descendants("TimeP")
                                                 Select New ScheduleEntity With {.TimePeriod = schedule1.Attribute("TimePeriod"),
                                                                                 .Sport = schedule1.Attribute("Sport")}).Single

                        Dim querySchedule = (From schedule1 In _dbLoserPool2.ScheduleEntities
                                             Where schedule1.TimePeriod = queryScheduleFile.TimePeriod And schedule1.CronJob = cronJobName).ToList

                        If querySchedule.Count = 0 Then

                            Dim queryGame = (From gameElement In myschedule.Descendants("schedule").Descendants("TimeP").Descendants("game")
                            Select New ScheduleEntity With {.GameId = gameElement.Attribute("gameNumber").Value,
                                                            .HomeTeam = gameElement.Elements("homeTeam").Value,
                                                            .AwayTeam = gameElement.Elements("awayTeam").Value,
                                                            .GameCode = gameElement.Attribute("gameCode").Value,
                                                            .GameDate = gameElement.Elements("gameDate").Value,
                                                            .GameTime = gameElement.Elements("gameTime").Value,
                                                            .DisplayStatus1 = gameElement.Elements("displayStatus1").Value,
                                                            .DisplayStatus2 = gameElement.Elements("displayStatus2").Value,
                                                            .Status = gameElement.Elements("status").Value}).ToList

                            Dim sport = queryScheduleFile.Sport

                            Dim queryTeams = (From team1 In _dbPools2.Teams
                                              Where team1.Sport = sport).ToList

                            For gameNum = 1 To queryGame.Count



                                Dim gameNumber = New ScheduleEntity

                                gameNumber.GameId = queryGame(gameNum - 1).GameId
                                gameNumber.TimePeriod = queryScheduleFile.TimePeriod
                                gameNumber.Sport = queryScheduleFile.Sport
                                gameNumber.StartDate = queryGame(gameNum - 1).GameDate
                                gameNumber.StartTime = queryGame(gameNum - 1).GameTime
                                gameNumber.HomeTeam = queryGame(gameNum - 1).HomeTeam
                                gameNumber.AwayTeam = queryGame(gameNum - 1).AwayTeam
                                gameNumber.HomeScore = "0"
                                gameNumber.AwayScore = "0"
                                gameNumber.GameCode = queryGame(gameNum - 1).GameCode
                                gameNumber.GameDate = queryGame(gameNum - 1).GameDate
                                gameNumber.GameTime = queryGame(gameNum - 1).GameTime
                                gameNumber.OriginalStartDate = queryGame(gameNum - 1).GameDate
                                gameNumber.OriginalStartTime = queryGame(gameNum - 1).GameTime
                                gameNumber.DisplayStatus1 = queryGame(gameNum - 1).DisplayStatus1
                                gameNumber.DisplayStatus2 = queryGame(gameNum - 1).DisplayStatus2
                                gameNumber.Status = queryGame(gameNum - 1).Status
                                gameNumber.CronJob = cronJobName
                                gameNumber.StartDateTime = DateTime.Parse(gameNumber.GameDate + " " + gameNumber.GameTime)
                                gameNumber.RescheduledGame = False

                                _dbLoserPool2.ScheduleEntities.Add(gameNumber)
                                _dbLoserPool2.SaveChanges()

                            Next

                            Dim thisTimePeriod = queryScheduleFile.TimePeriod
                            Dim querySchedule1 = (From schedule1 In _dbLoserPool2.ScheduleEntities
                                                 Where schedule1.TimePeriod = thisTimePeriod And schedule1.CronJob = cronJobName
                                                 Select schedule1)

                            For Each team1 In queryTeams

                                Dim byeTeam1 = (From schedule1 In querySchedule1
                                                  Where schedule1.HomeTeam = team1.TeamName Or schedule1.AwayTeam = team1.TeamName).ToList

                                If byeTeam1.Count > 0 Then
                                Else
                                    Dim byeTeam2 As New ByeTeam
                                    byeTeam2.TimePeriod = thisTimePeriod
                                    byeTeam2.TeamName = team1.TeamName
                                    byeTeam2.Sport = sport
                                    byeTeam2.CronJob = cronJobName

                                    _dbLoserPool2.ByeTeamsList.Add(byeTeam2)
                                End If
                            Next

                            _dbLoserPool2.SaveChanges()

                        End If
                    End Using
                End Using

            Catch ex As Exception

            End Try

        End Sub

    End Class

    Public Class ScheduleFileXML
        Public Property FilePath As String
        Public Property sport As String
    End Class



End Namespace