Imports System.Web
Imports System.Linq
Imports System.Data

Imports WpfApplication1.JoinPools.Models


Public Class PoolList
    Public Sub New(filepath As String)
        Dim _dbPools As New WpfApplication1.JoinPools.Models.PoolDbContext

        Try
            Using (_dbPools)

                Dim queryPool1 = (From pool1 In _dbPools.Pools).ToList

                If queryPool1.Count >= 1 Then
                    Exit Sub
                End If

                Dim poolsXDocument = XDocument.Load(filepath)

                Dim allPools = (From pool1 In poolsXDocument.Descendants("PoolList").Descendants("Pool")
                                Select New PoolXML With {.PoolId = pool1.Attribute("PoolId").Value,
                                                         .PoolName = pool1.Elements("PoolName").Value})

                For Each pool1 In allPools
                    Dim pool2 As New Pool
                    pool2.PoolId = pool1.PoolId
                    pool2.PoolName = pool1.PoolName
                    _dbPools.Pools.Add(pool2)
                Next

                _dbPools.SaveChanges()

            End Using
        Catch ex As Exception

        End Try
    End Sub
End Class
Public Class TeamList
    Public Sub New(filepath As String)
        Dim _dbPools As New PoolDbContext

        Try
            Using (_dbPools)

                Dim queryTeam1 = (From team1 In _dbPools.Teams).ToList

                If queryTeam1.Count >= 1 Then
                    Exit Sub
                End If

                Dim teamsXDocument = XDocument.Load(filepath)

                Dim allteams = (From team1 In teamsXDocument.Descendants("TeamList").Descendants("Team")
                                Select New TeamXML With {.TeamId = team1.Attribute("TeamId").Value,
                                                         .TeamName = team1.Elements("TeamName").Value})

                For Each team1 In allteams
                    Dim team2 As New Team
                    team2.TeamId = team1.TeamId
                    team2.TeamName = team1.TeamName
                    _dbPools.Teams.Add(team2)
                Next

                _dbPools.SaveChanges()

            End Using
        Catch ex As Exception

        End Try
    End Sub

End Class
Public Class MyPoolListAlso
    Public Sub New(filepath As String)
        Dim _dbPools As New PoolDbContext

        Try
            Using (_dbPools)

                Dim queryMypool1 = (From user1 In _dbPools.MyPools).ToList

                If queryMypool1.Count >= 1 Then
                    Exit Sub
                End If

                Dim poolsXDocument = XDocument.Load(filepath)

                Dim queryAllMypools = (From user1 In poolsXDocument.Descendants("MyPoolList").Descendants("User")
                                       Select New MyPoolXML With {.UserId = user1.Attribute("UserId").Value,
                                                                   .EName = user1.Elements("EName").Value,
                                                                   .Loser = user1.Elements("Loser").Value,
                                                                   .Playoff = user1.Elements("Playoff").Value}).ToList

                For Each user1 In queryAllMypools
                    Dim user2 = New MyPool
                    user2.UserId = user1.UserId
                    user2.EName = user1.EName

                    If user1.Loser = "" Then
                        user2.Loser = Nothing
                    Else
                        user2.Loser = user1.Loser
                    End If

                    If user1.Playoff = "" Then
                        user2.Playoff = Nothing
                    Else
                        user2.Playoff = user1.Playoff
                    End If



                    _dbPools.MyPools.Add(user2)
                Next

                _dbPools.SaveChanges()

            End Using
        Catch ex As Exception

        End Try

    End Sub

End Class

