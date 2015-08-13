Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace LosersPool.Models

    Public Class UserChoices

        <Key>
        Public Property ListId As Int32

        Public Property UserID As String

        Public Property UserName As String
        Public Property TimePeriod As String
        Public Property ConfirmationNumber As Int32
        Public Property UserPick As String
        Public Property Contender As Boolean

        Public Property Team1Available As Boolean
        Public Property Team2Available As Boolean
        Public Property Team3Available As Boolean
        Public Property Team4Available As Boolean
        Public Property Team5Available As Boolean
        Public Property Team6Available As Boolean
        Public Property Team7Available As Boolean
        Public Property Team8Available As Boolean
        Public Property Team9Available As Boolean
        Public Property Team10Available As Boolean
        Public Property Team11Available As Boolean
        Public Property Team12Available As Boolean
        Public Property Team13Available As Boolean
        Public Property Team14Available As Boolean
        Public Property Team15Available As Boolean
        Public Property Team16Available As Boolean
        Public Property Team17Available As Boolean
        Public Property Team18Available As Boolean
        Public Property Team19Available As Boolean
        Public Property Team20Available As Boolean
        Public Property Team21Available As Boolean
        Public Property Team22Available As Boolean
        Public Property Team23Available As Boolean
        Public Property Team24Available As Boolean
        Public Property Team25Available As Boolean
        Public Property Team26Available As Boolean
        Public Property Team27Available As Boolean
        Public Property Team28Available As Boolean
        Public Property Team29Available As Boolean
        Public Property Team30Available As Boolean
        Public Property Team31Available As Boolean
        Public Property Team32Available As Boolean

        Public Property AdministrationPick As Boolean

        Public Property PoolAlias As String
        Public Property Sport As String
        Public Property CronJob As String

        Public Property PickedGameCode As String
        Public Property UserPickPostponed As String
        Public Property UserIsWinning As Boolean
        Public Property UserIsTied As Boolean

        Public Overridable Property PossibleUserPicks As New List(Of String)

    End Class

    Public Class UserPick
        <Key>
        Public Property UserPickId As Int32
        Public Property UserID As String
        Public Property CronJobName As String
        Public Property PoolAlias As String
        Public Property TimePeriod As String
        Public Property UserPick1 As String
        Public Property GameCode As String
        Public Property UserPickPostponed As Boolean
        Public Property PickIsWinning As Boolean
        Public Property PickIsTied As Boolean

    End Class




    Public Class UserChoiceList

        Public Sub New(filepath As String, rootFolder As String, cronJobName As String, poolAlias As String)
            Dim _dbLoserPool1 As New LosersPoolContext
            Dim _dbApp1 As New ApplicationDbContext
            Try
                Using (_dbLoserPool1)
                    Using (_dbApp1)


                        Dim queryUser1 = (From users1 In _dbLoserPool1.UserChoicesList
                                            Where users1.PoolAlias = poolAlias).ToList

                        If queryUser1.Count > 0 Then
                            Exit Sub
                        End If

                        Dim userChoicesXDocument = XDocument.Load(filepath)

                        Dim DailyPossibleChoicesForAllUsers = (From dayElement In userChoicesXDocument.Descendants("UserChoicesList").Descendants("TimeP").Descendants("User")
                                                                Select New UserChoices With {.UserID = dayElement.Attribute("UserId").Value,
                                                                                                .TimePeriod = dayElement.Attribute("TimePeriod").Value,
                                                                                                .UserName = dayElement.Elements("UserName").Value,
                                                                                                .ConfirmationNumber = CInt(dayElement.Elements("ConfirmationNumber").Value),
                                                                                                .UserPick = dayElement.Elements("UserPick").Value,
                                                                                                .Team1Available = CBool(dayElement.Elements("Team1Available").Value),
                                                                                                .Team2Available = CBool(dayElement.Elements("Team2Available").Value),
                                                                                                .Team3Available = CBool(dayElement.Elements("Team3Available").Value),
                                                                                                .Team4Available = CBool(dayElement.Elements("Team4Available").Value),
                                                                                                .Team5Available = CBool(dayElement.Elements("Team5Available").Value),
                                                                                                .Team6Available = CBool(dayElement.Elements("Team6Available").Value),
                                                                                                .Team7Available = CBool(dayElement.Elements("Team7Available").Value),
                                                                                                .Team8Available = CBool(dayElement.Elements("Team8Available").Value),
                                                                                                .Team9Available = CBool(dayElement.Elements("Team9Available").Value),
                                                                                                .Team10Available = CBool(dayElement.Elements("Team10Available").Value),
                                                                                                .Team11Available = CBool(dayElement.Elements("Team11Available").Value),
                                                                                                .Team12Available = CBool(dayElement.Elements("Team12Available").Value),
                                                                                                .Team13Available = CBool(dayElement.Elements("Team13Available").Value),
                                                                                                .Team14Available = CBool(dayElement.Elements("Team14Available").Value),
                                                                                                .Team15Available = CBool(dayElement.Elements("Team15Available").Value),
                                                                                                .Team16Available = CBool(dayElement.Elements("Team16Available").Value),
                                                                                                .Team17Available = CBool(dayElement.Elements("Team17Available").Value),
                                                                                                .Team18Available = CBool(dayElement.Elements("Team18Available").Value),
                                                                                                .Team19Available = CBool(dayElement.Elements("Team19Available").Value),
                                                                                                .Team20Available = CBool(dayElement.Elements("Team20Available").Value),
                                                                                                .Team21Available = CBool(dayElement.Elements("Team21Available").Value),
                                                                                                .Team22Available = CBool(dayElement.Elements("Team22Available").Value),
                                                                                                .Team23Available = CBool(dayElement.Elements("Team23Available").Value),
                                                                                                .Team24Available = CBool(dayElement.Elements("Team24Available").Value),
                                                                                                .Team25Available = CBool(dayElement.Elements("Team25Available").Value),
                                                                                                .Team26Available = CBool(dayElement.Elements("Team26Available").Value),
                                                                                                .Team27Available = CBool(dayElement.Elements("Team27Available").Value),
                                                                                                .Team28Available = CBool(dayElement.Elements("Team28Available").Value),
                                                                                                .Team29Available = CBool(dayElement.Elements("Team29Available").Value),
                                                                                                .Team30Available = CBool(dayElement.Elements("Team30Available").Value),
                                                                                                .Team31Available = CBool(dayElement.Elements("Team31Available").Value),
                                                                                                .Team32Available = CBool(dayElement.Elements("Team32Available").Value),
                                                                                                .Contender = CBool(dayElement.Elements("Contender").Value),
                                                                                                .PoolAlias = dayElement.Elements("PoolAlias").Value,
                                                                                                .Sport = dayElement.Elements("Sport").Value}).ToList

                        For Each user1 In DailyPossibleChoicesForAllUsers
                            Dim user2 = New UserChoices
                            user2.ListId = _dbLoserPool1.UserChoicesList.Count + 1
                            user2.UserID = user1.UserID
                            user2.UserName = user1.UserName
                            user2.TimePeriod = user1.TimePeriod
                            user2.ConfirmationNumber = user1.ConfirmationNumber
                            user2.Contender = user1.Contender
                            user2.UserPick = user1.UserPick
                            user2.Team1Available = user1.Team1Available
                            user2.Team2Available = user1.Team2Available
                            user2.Team3Available = user1.Team3Available
                            user2.Team4Available = user1.Team4Available
                            user2.Team5Available = user1.Team5Available
                            user2.Team6Available = user1.Team6Available
                            user2.Team7Available = user1.Team7Available
                            user2.Team8Available = user1.Team8Available
                            user2.Team9Available = user1.Team9Available
                            user2.Team10Available = user1.Team10Available
                            user2.Team11Available = user1.Team11Available
                            user2.Team12Available = user1.Team12Available
                            user2.Team13Available = user1.Team13Available
                            user2.Team14Available = user1.Team14Available
                            user2.Team15Available = user1.Team15Available
                            user2.Team16Available = user1.Team16Available
                            user2.Team17Available = user1.Team17Available
                            user2.Team18Available = user1.Team18Available
                            user2.Team19Available = user1.Team19Available
                            user2.Team20Available = user1.Team20Available
                            user2.Team21Available = user1.Team21Available
                            user2.Team22Available = user1.Team22Available
                            user2.Team23Available = user1.Team23Available
                            user2.Team24Available = user1.Team24Available
                            user2.Team25Available = user1.Team25Available
                            user2.Team26Available = user1.Team26Available
                            user2.Team27Available = user1.Team27Available
                            user2.Team28Available = user1.Team28Available
                            user2.Team29Available = user1.Team29Available
                            user2.Team30Available = user1.Team30Available
                            user2.Team31Available = user1.Team31Available
                            user2.Team32Available = user1.Team32Available
                            user2.PoolAlias = poolAlias
                            user2.CronJob = cronJobName
                            user2.Sport = user1.Sport
                            user2.UserPickPostponed = False

                            _dbLoserPool1.UserChoicesList.Add(user2)
                        Next

                        _dbLoserPool1.SaveChanges()

                        Dim dummy = "dummy"
                    End Using
                End Using

            Catch ex As Exception

            End Try

        End Sub
    End Class



End Namespace
