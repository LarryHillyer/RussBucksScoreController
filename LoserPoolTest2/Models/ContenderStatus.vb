
Namespace LosersPool.Models
    Public Class ContenderStatus

        Public Shared Sub UpdateContenderStatus(TimePeriod As String, queryScoringUpdate As List(Of CurrentScoringUpdate), thisTimePeriod As String, timePeriodIncrement As Int32, poolAlias As String, teams1 As List(Of Team), sport As String, timePeriodName As String, cronJobName As String)
            Dim _dbLoserPool6 As New LosersPoolContext

            Try
                Using (_dbLoserPool6)
                    Dim queryUserChoices1 = (From user3 In _dbLoserPool6.UserChoicesList
                        Where user3.TimePeriod = TimePeriod And user3.Contender = True And user3.PoolAlias = poolAlias And _
                        user3.CronJob = cronJobName).ToList

                    For Each user1 In queryUserChoices1

                        Dim queryUserPicks = (From user2 In _dbLoserPool6.UserPicks
                                         Where user2.CronJobName = cronJobName And user2.UserID = user1.UserID And user2.PoolAlias = poolAlias).ToList

                        Dim userIsLoser = False
                        For Each userPick1 In queryUserPicks

                            For Each game In queryScoringUpdate

                                If userPick1.GameCode <> game.GameCode Then
                                    Continue For
                                Else
                                    If game.hometeam = userPick1.UserPick1 Then
                                        If game.homescore > game.awayscore Then
                                            user1.Contender = False
                                        End If
                                    ElseIf game.awayteam = userPick1.UserPick1 Then
                                        If game.awayscore > game.homescore Then
                                            user1.Contender = False
                                        End If
                                    End If

                                    If user1.Contender = False Then
                                        Dim loser1 = New Loser
                                        loser1.ListId = _dbLoserPool6.LoserList.Count + 1
                                        loser1.UserId = user1.UserID
                                        loser1.UserName = user1.UserName
                                        loser1.TimePeriod = user1.TimePeriod
                                        loser1.TimePeriodInt = CInt(Mid(user1.TimePeriod, Len(user1.TimePeriod)))
                                        loser1.LosingPick = userPick1.UserPick1
                                        loser1.PoolAlias = poolAlias
                                        loser1.Sport = sport
                                        loser1.CronJob = cronJobName

                                        _dbLoserPool6.LoserList.Add(loser1)
                                        _dbLoserPool6.SaveChanges()
                                        userIsLoser = True
                                        Exit For
                                    End If

                                End If
                            Next

                            If userIsLoser = True Then
                                For Each pick1 In queryUserPicks
                                    _dbLoserPool6.UserPicks.Remove(pick1)
                                    _dbLoserPool6.SaveChanges()
                                Next
                                Exit For
                            End If
                        Next
                    Next

                    queryUserChoices1 = (From user3 In _dbLoserPool6.UserChoicesList
                                         Where user3.TimePeriod = TimePeriod And user3.Contender = True And user3.PoolAlias = poolAlias And _
                                         user3.CronJob = cronJobName).ToList

                    For Each user3 In queryUserChoices1
                        Dim user2 = New UserChoices
                        user2.ListId = _dbLoserPool6.UserChoicesList.Count + 1
                        user2.UserID = user3.UserID
                        user2.UserName = user3.UserName
                        user2.TimePeriod = timePeriodName + CStr(CInt(Mid(thisTimePeriod, Len(thisTimePeriod))) + timePeriodIncrement)  'TimePeriod
                        user2.Team1Available = user3.Team1Available
                        user2.Team2Available = user3.Team2Available
                        user2.Team3Available = user3.Team3Available
                        user2.Team4Available = user3.Team4Available
                        user2.Team5Available = user3.Team5Available
                        user2.Team6Available = user3.Team6Available
                        user2.Team7Available = user3.Team7Available
                        user2.Team8Available = user3.Team8Available
                        user2.Team9Available = user3.Team9Available
                        user2.Team10Available = user3.Team10Available
                        user2.Team11Available = user3.Team11Available
                        user2.Team12Available = user3.Team12Available
                        user2.Team13Available = user3.Team13Available
                        user2.Team14Available = user3.Team14Available
                        user2.Team15Available = user3.Team15Available
                        user2.Team16Available = user3.Team16Available
                        user2.Team17Available = user3.Team17Available
                        user2.Team18Available = user3.Team18Available
                        user2.Team19Available = user3.Team19Available
                        user2.Team20Available = user3.Team20Available
                        user2.Team21Available = user3.Team21Available
                        user2.Team22Available = user3.Team22Available
                        user2.Team23Available = user3.Team23Available
                        user2.Team24Available = user3.Team24Available
                        user2.Team25Available = user3.Team25Available
                        user2.Team26Available = user3.Team26Available
                        user2.Team27Available = user3.Team27Available
                        user2.Team28Available = user3.Team28Available
                        user2.Team29Available = user3.Team29Available
                        user2.Team30Available = user3.Team30Available
                        user2.Team31Available = user3.Team31Available
                        user2.Team32Available = user3.Team32Available
                        user2.UserPickPostponed = user3.UserPickPostponed
                        user2.PoolAlias = poolAlias
                        user2.Sport = user3.Sport
                        user2.CronJob = cronJobName
                        user2.Contender = True
                        user2.UserPick = user3.UserPick
                        user2 = SetContendersPickToFalse(user2, teams1)
                        user2.UserPick = ""
                        user2.UserPickPostponed = False
                        user2.UserIsTied = True
                        user2.UserIsWinning = False
                        _dbLoserPool6.UserChoicesList.Add(user2)

                        Dim queryUserPick1 = (From qUP1 In _dbLoserPool6.UserPicks
                                              Where qUP1.PoolAlias = poolAlias And qUP1.UserID = user3.UserID And qUP1.UserPick1 = user3.UserPick And qUP1.UserPickPostponed = False).SingleOrDefault

                        If queryUserPick1 Is Nothing Then
                        Else
                            _dbLoserPool6.UserPicks.Remove(queryUserPick1)
                        End If

                        Dim queryUserPicks2 = (From qUP2 In _dbLoserPool6.UserPicks
                                               Where qUP2.CronJobName = cronJobName And qUP2.UserID = user3.UserID And qUP2.UserPickPostponed = True).ToList

                        For Each pick2 In queryUserPicks2

                            Dim querySchedule1 = (From qS1 In _dbLoserPool6.ScheduleEntities
                                                  Where qS1.GameCode = pick2.GameCode And qS1.CronJob = cronJobName).SingleOrDefault

                            If querySchedule1 Is Nothing Then
                            Else
                                If querySchedule1.Status = "final" Then
                                    _dbLoserPool6.UserPicks.Remove(pick2)
                                End If
                            End If
                        Next

                        _dbLoserPool6.SaveChanges()

                    Next

                End Using
            Catch ex As Exception

            End Try
        End Sub

        Private Shared Function SetContendersPickToFalse(user1 As UserChoices, teams1 As List(Of Team)) As UserChoices


            If user1.UserPick = teams1(0).TeamName Then
                user1.Team1Available = False
            ElseIf user1.UserPick = teams1(1).TeamName Then
                user1.Team2Available = False
            ElseIf user1.UserPick = teams1(2).TeamName Then
                user1.Team3Available = False
            ElseIf user1.UserPick = teams1(3).TeamName Then
                user1.Team4Available = False
            ElseIf user1.UserPick = teams1(4).TeamName Then
                user1.Team5Available = False
            ElseIf user1.UserPick = teams1(5).TeamName Then
                user1.Team6Available = False
            ElseIf user1.UserPick = teams1(6).TeamName Then
                user1.Team7Available = False
            ElseIf user1.UserPick = teams1(7).TeamName Then
                user1.Team8Available = False
            ElseIf user1.UserPick = teams1(8).TeamName Then
                user1.Team9Available = False
            ElseIf user1.UserPick = teams1(9).TeamName Then
                user1.Team10Available = False
            ElseIf user1.UserPick = teams1(10).TeamName Then
                user1.Team11Available = False
            ElseIf user1.UserPick = teams1(11).TeamName Then
                user1.Team12Available = False
            ElseIf user1.UserPick = teams1(12).TeamName Then
                user1.Team13Available = False
            ElseIf user1.UserPick = teams1(13).TeamName Then
                user1.Team14Available = False
            ElseIf user1.UserPick = teams1(14).TeamName Then
                user1.Team15Available = False
            ElseIf user1.UserPick = teams1(15).TeamName Then
                user1.Team16Available = False
            ElseIf user1.UserPick = teams1(16).TeamName Then
                user1.Team17Available = False
            ElseIf user1.UserPick = teams1(17).TeamName Then
                user1.Team18Available = False
            ElseIf user1.UserPick = teams1(18).TeamName Then
                user1.Team19Available = False
            ElseIf user1.UserPick = teams1(19).TeamName Then
                user1.Team20Available = False
            ElseIf user1.UserPick = teams1(20).TeamName Then
                user1.Team21Available = False
            ElseIf user1.UserPick = teams1(21).TeamName Then
                user1.Team22Available = False
            ElseIf user1.UserPick = teams1(22).TeamName Then
                user1.Team23Available = False
            ElseIf user1.UserPick = teams1(23).TeamName Then
                user1.Team24Available = False
            ElseIf user1.UserPick = teams1(24).TeamName Then
                user1.Team25Available = False
            ElseIf user1.UserPick = teams1(25).TeamName Then
                user1.Team26Available = False
            ElseIf user1.UserPick = teams1(26).TeamName Then
                user1.Team27Available = False
            ElseIf user1.UserPick = teams1(27).TeamName Then
                user1.Team28Available = False
            ElseIf user1.UserPick = teams1(28).TeamName Then
                user1.Team29Available = False
            ElseIf user1.UserPick = teams1(29).TeamName Then
                user1.Team30Available = False
            ElseIf user1.UserPick = teams1(30).TeamName Then
                user1.Team31Available = False
            ElseIf user1.UserPick = teams1(31).TeamName Then
                user1.Team32Available = False

            End If
            Return user1
        End Function

        Public Shared Sub RealLosers(thisTimePeriod As String, poolAlias As String, sport As String, cronJobName As String)

            Dim _dbLoserPool7 As New LosersPoolContext

            Try
                Using (_dbLoserPool7)
                    Dim queryUserChoices1 = (From user2 In _dbLoserPool7.UserChoicesList
                                            Where user2.TimePeriod = thisTimePeriod And user2.Contender = True And user2.PoolAlias = poolAlias And (user2.UserPick = "" Or user2.UserPick Is Nothing) And _
                                            user2.CronJob = cronJobName).ToList

                    For Each user1 In queryUserChoices1
                        'user1 is a loser because user did not enter data

                        user1.Contender = False

                        _dbLoserPool7.SaveChanges()

                        'add  user1 to loser list
                        Dim queryLoser = (From loser2 In _dbLoserPool7.LoserList
                                          Where loser2.UserName = user1.UserName And loser2.PoolAlias = poolAlias And loser2.CronJob = cronJobName).ToList

                        If queryLoser.Count = 0 Then
                            Dim loser1 = New Loser
                            loser1.ListId = _dbLoserPool7.LoserList.Count + 1
                            loser1.UserId = user1.UserID
                            loser1.UserName = user1.UserName
                            loser1.TimePeriod = user1.TimePeriod
                            loser1.TimePeriodInt = CInt(Mid(user1.TimePeriod, Len(user1.TimePeriod)))
                            loser1.LosingPick = "Not Made"
                            loser1.PoolAlias = poolAlias
                            loser1.Sport = sport
                            loser1.CronJob = cronJobName
                            _dbLoserPool7.LoserList.Add(loser1)
                            _dbLoserPool7.SaveChanges()
                            Continue For
                        End If
                    Next
                End Using
            Catch ex As Exception

            End Try

        End Sub

    End Class
End Namespace