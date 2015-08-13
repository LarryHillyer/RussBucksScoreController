Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Imports WpfApplication1

Public Class Sport

    <Key>
    Public Property SportId As Int32
    Public Property SportName As String
    Public Property PoolName As String
    Public Property PageUrl As String
    Public Property LeagueSize As Int32

End Class

Public Class SeedSports

    Public Sub New()

        Dim _dbPools As New JoinPools.Models.PoolDbContext

        Try
            Using (_dbPools)

                Dim querySports = (From sport1 In _dbPools.Sports).ToList

                If querySports.Count > 0 Then
                    Exit Sub
                End If

                Dim sport2 As New Sport
                sport2.PoolName = "LoserPool"
                sport2.SportName = "baseball"
                sport2.PageUrl = "../LosersPool/LoserPoolHome.aspx"
                sport2.LeagueSize = 30
                _dbPools.Sports.Add(sport2)

                sport2 = New Sport
                sport2.PoolName = "LoserPool"
                sport2.SportName = "football"
                sport2.PageUrl = "../LosersPool/LoserPoolHome.aspx"
                sport2.LeagueSize = 32
                _dbPools.Sports.Add(sport2)

                sport2 = New Sport
                sport2.PoolName = "PlayoffPool"
                sport2.SportName = "baseball"
                sport2.PageUrl = "../PlayoffPool/Default.aspx"
                sport2.LeagueSize = 30
                _dbPools.Sports.Add(sport2)

                sport2 = New Sport
                sport2.PoolName = "PlayoffPool"
                sport2.SportName = "football"
                sport2.PageUrl = "../PlayoffPool/Default.aspx"
                sport2.LeagueSize = 32
                _dbPools.Sports.Add(sport2)

                _dbPools.SaveChanges()


            End Using
        Catch ex As Exception

        End Try
    End Sub




End Class

Public Class SportDatesOfTheWeek
    <Key>
    Public Property DateId As Int32
    Public Property Date1 As Date
    Public Property WeekNumber As String
    Public Property Sport As String
    Public Property Phase As String

End Class