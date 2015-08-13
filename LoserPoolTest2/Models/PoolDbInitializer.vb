Imports System.Collections.Generic
Imports System.Data.Entity

Namespace JoinPools.Models
    Public Class PoolDbInitializer
        Inherits DropCreateDatabaseIfModelChanges(Of PoolDbContext)

        Protected Overrides Sub Seed(_db As PoolDbContext)

            'Dim pools As List(Of Pool)
            'pools = GetPools()
            'For Each pool1 In pools
            '_db.Pools.Add(pool1)
            'Next

            'Dim teams As New List(Of Team)
            'teams = GetTeams()
            'For Each team1 In teams
            '_db.Teams.Add(team1)
            'Next

            'mypool.UserId = "user1"
            'ypool.EName = "larryhillyer@hotmail.com"
            'mypool.Loser = True
            'mypool.Playoff = True

            '_db.MyPools.Add(mypool)

            _db.SaveChanges()

        End Sub

        Public Shared Function GetPools() As List(Of Pool)

            Dim pools As New List(Of Pool)()

            Dim p As New Pool
            p.PoolId = "Pool1"
            p.PoolName = "LoserPool"
            pools.Add(p)

            p = New Pool
            p.PoolId = "Pool2"
            p.PoolName = "PlayoffPool"
            pools.Add(p)

            Return pools

        End Function

        Public Shared Function GetTeams() As List(Of Team)

            Dim teams As New List(Of Team)
            Dim t As New Team

            t.TeamId = "team1"
            t.TeamName = "seahawks"
            teams.Add(t)

            t = New Team
            t.TeamId = "team2"
            t.TeamName = "packers"
            teams.Add(t)

            t = New Team
            t.TeamId = "team3"
            t.TeamName = "falcons"
            teams.Add(t)

            t = New Team
            t.TeamId = "team4"
            t.TeamName = "saints"
            teams.Add(t)

            t = New Team
            t.TeamId = "team5"
            t.TeamName = "chargers"
            teams.Add(t)

            t = New Team
            t.TeamId = "team6"
            t.TeamName = "cardinals"
            teams.Add(t)

            t = New Team
            t.TeamId = "team7"
            t.TeamName = "lions"
            teams.Add(t)

            t = New Team
            t.TeamId = "team8"
            t.TeamName = "giants"
            teams.Add(t)

            Return teams

        End Function

    End Class


End Namespace
