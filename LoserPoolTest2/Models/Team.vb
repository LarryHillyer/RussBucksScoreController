Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Imports WpfApplication1.JoinPools.Models
Public Class Team

    <Key>
    Public Property TeamNumber As Int32

    Public Property Sport As String

    Public Property TeamId As String

    <Required>
    Public Property TeamName As String

    Public Property NickName As String

    Public Property TeamLabel As String

    Public Property TeamIcon As String

End Class
Public Class SeedTeams
    Public Sub New()

        Dim _dbPools2 As New PoolDbContext

        Try
            Using (_dbPools2)

                Dim queryTeam1 = (From team1 In _dbPools2.Teams).ToList

                If queryTeam1.Count >= 1 Then
                    Exit Sub
                End If

                Dim teams As New List(Of Team)
                teams = GetTeams()
                For Each team1 In teams
                    _dbPools2.Teams.Add(team1)
                Next

                _dbPools2.SaveChanges()

            End Using
        Catch ex As Exception

        End Try
    End Sub

    Public Shared Function GetTeams() As List(Of Team)

        Dim teams As New List(Of Team)
        Dim t As New Team

        t.TeamId = "team1"
        t.TeamName = "Washington"
        t.Sport = "baseball"
        t.TeamLabel = "~/MLB ICONS/Washington Label.png"
        t.TeamIcon = "~/MLB ICONS/Washington.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team2"
        t.TeamName = "Miami"
        t.Sport = "baseball"
        t.TeamLabel = "~/MLB ICONS/Miami Label.png"
        t.TeamIcon = "~/MLB ICONS/Miami.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team3"
        t.TeamName = "Colorado"
        t.Sport = "baseball"
        t.TeamLabel = "~/MLB ICONS/Colorado Label.png"
        t.TeamIcon = "~/MLB ICONS/Colorado.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team4"
        t.TeamName = "Arizona"
        t.Sport = "baseball"
        t.TeamLabel = "~/MLB ICONS/Arizona Label.png"
        t.TeamIcon = "~/MLB ICONS/Arizona.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team5"
        t.TeamName = "San Francisco"
        t.Sport = "baseball"
        t.TeamLabel = "~/MLB ICONS/San Francisco Label.png"
        t.TeamIcon = "~/MLB ICONS/San Francisco.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team6"
        t.TeamName = "San Diego"
        t.Sport = "baseball"
        t.TeamLabel = "~/MLB ICONS/San Diego Label.png"
        t.TeamIcon = "~/MLB ICONS/San Diego.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team7"
        t.TeamName = "Pittsburg"
        t.Sport = "baseball"
        t.TeamLabel = "~/MLB ICONS/Pittsburg Label.png"
        t.TeamIcon = "~/MLB ICONS/Pittsburg.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team8"
        t.TeamName = "Cincinnati"
        t.Sport = "baseball"
        t.TeamLabel = "~/MLB ICONS/Cincinnati Label.png"
        t.TeamIcon = "~/MLB ICONS/Cincinnati.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team9"
        t.TeamName = "Toronto"
        t.Sport = "baseball"
        t.TeamLabel = "~/MLB ICONS/Toronto Label.png"
        t.TeamIcon = "~/MLB ICONS/Toronto.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team10"
        t.TeamName = "NY Yankees"
        t.Sport = "baseball"
        t.TeamLabel = "~/MLB ICONS/NY Yankees Label.png"
        t.TeamIcon = "~/MLB ICONS/NY Yankees.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team11"
        t.TeamName = "Boston"
        t.Sport = "baseball"
        t.TeamLabel = "~/MLB ICONS/Boston Label.png"
        t.TeamIcon = "~/MLB ICONS/Boston.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team12"
        t.TeamName = "Tampa Bay"
        t.Sport = "baseball"
        t.TeamLabel = "~/MLB ICONS/Tampa Bay Label.png"
        t.TeamIcon = "~/MLB ICONS/Tampa Bay.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team13"
        t.TeamName = "Atlanta"
        t.Sport = "baseball"
        t.TeamLabel = "~/MLB ICONS/Atlanta Label.png"
        t.TeamIcon = "~/MLB ICONS/Atlanta.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team14"
        t.TeamName = "Philadelphia"
        t.Sport = "baseball"
        t.TeamLabel = "~/MLB ICONS/Philadelphia Label.png"
        t.TeamIcon = "~/MLB ICONS/Philadelphia.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team15"
        t.TeamName = "Chicago White Sox"
        t.Sport = "baseball"
        t.TeamLabel = "~/MLB ICONS/Chicago White Sox Label.png"
        t.TeamIcon = "~/MLB ICONS/Chicago White Sox.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team16"
        t.TeamName = "Detroit"
        t.Sport = "baseball"
        t.TeamLabel = "~/MLB ICONS/Detroit Label.png"
        t.TeamIcon = "~/MLB ICONS/Detroit.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team17"
        t.TeamName = "Kansas City"
        t.Sport = "baseball"
        t.TeamLabel = "~/MLB ICONS/Kansas City Label.png"
        t.TeamIcon = "~/MLB ICONS/Kansas City.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team18"
        t.TeamName = "Cleveland"
        t.Sport = "baseball"
        t.TeamLabel = "~/MLB ICONS/Cleveland Label.png"
        t.TeamIcon = "~/MLB ICONS/Cleveland.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team19"
        t.TeamName = "Milwaukee"
        t.Sport = "baseball"
        t.TeamLabel = "~/MLB ICONS/Milwaukee Label.png"
        t.TeamIcon = "~/MLB ICONS/Milwaukee.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team20"
        t.TeamName = "LA Dodgers"
        t.Sport = "baseball"
        t.TeamLabel = "~/MLB ICONS/LA Dodgers Label.png"
        t.TeamIcon = "~/MLB ICONS/LA Dodgers.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team21"
        t.TeamName = "Minnesota"
        t.Sport = "baseball"
        t.TeamLabel = "~/MLB ICONS/Minnesota Label.png"
        t.TeamIcon = "~/MLB ICONS/Minnesota.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team22"
        t.TeamName = "Oakland"
        t.Sport = "baseball"
        t.TeamLabel = "~/MLB ICONS/Oakland Label.png"
        t.TeamIcon = "~/MLB ICONS/Oakland.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team23"
        t.TeamName = "Houston"
        t.Sport = "baseball"
        t.TeamLabel = "~/MLB ICONS/Houston Label.png"
        t.TeamIcon = "~/MLB ICONS/Houston.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team24"
        t.TeamName = "Texas"
        t.Sport = "baseball"
        t.TeamLabel = "~/MLB ICONS/Texas Label.png"
        t.TeamIcon = "~/MLB ICONS/Texas.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team25"
        t.TeamName = "St. Louis"
        t.Sport = "baseball"
        t.TeamLabel = "~/MLB ICONS/St. Louis Label.png"
        t.TeamIcon = "~/MLB ICONS/St. Louis.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team26"
        t.TeamName = "Chicago Cubs"
        t.Sport = "baseball"
        t.TeamLabel = "~/MLB ICONS/Chicago Cubs Label.png"
        t.TeamIcon = "~/MLB ICONS/Chicago Cubs.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team27"
        t.TeamName = "LA Angels"
        t.Sport = "baseball"
        t.TeamLabel = "~/MLB ICONS/LA Angels Label.png"
        t.TeamIcon = "~/MLB ICONS/LA Angels.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team28"
        t.TeamName = "Seattle"
        t.Sport = "baseball"
        t.TeamLabel = "~/MLB ICONS/Seattle Label.png"
        t.TeamIcon = "~/MLB ICONS/Seattle.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team29"
        t.TeamName = "NY Mets"
        t.Sport = "baseball"
        t.TeamLabel = "~/MLB ICONS/NY Mets Label.png"
        t.TeamIcon = "~/MLB ICONS/NY Mets.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team30"
        t.TeamName = "Baltimore"
        t.Sport = "baseball"
        t.TeamLabel = "~/MLB ICONS/Baltimore Label.png"
        t.TeamIcon = "~/MLB ICONS/Baltimore.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team31"
        t.TeamName = "dummy"
        t.Sport = "baseball"
        t.TeamLabel = "dummy"
        t.TeamIcon = "dummy"
        teams.Add(t)

        t = New Team
        t.TeamId = "team32"
        t.TeamName = "dummy"
        t.Sport = "baseball"
        t.TeamLabel = "dummy"
        t.TeamIcon = "dummy"
        teams.Add(t)

        t = New Team
        t.TeamId = "team1"
        t.TeamName = "bills"
        t.Sport = "football"
        t.TeamLabel = "~/NFL ICONS/Bills Label.png"
        t.TeamIcon = "~/NFL ICONS/Bills.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team2"
        t.TeamName = "dolphins"
        t.Sport = "football"
        t.TeamLabel = "~/NFL ICONS/Dolphins Label.png"
        t.TeamIcon = "~/NFL ICONS/Dolphins.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team3"
        t.TeamName = "patriots"
        t.Sport = "football"
        t.TeamLabel = "~/NFL ICONS/Patriots Label.png"
        t.TeamIcon = "~/NFL ICONS/Patriots.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team4"
        t.TeamName = "jets"
        t.Sport = "football"
        t.TeamLabel = "~/NFL ICONS/Jets Label.png"
        t.TeamIcon = "~/NFL ICONS/Jets.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team5"
        t.TeamName = "ravens"
        t.Sport = "football"
        t.TeamLabel = "~/NFL ICONS/Ravens Label.png"
        t.TeamIcon = "~/NFL ICONS/Ravens.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team6"
        t.TeamName = "bengals"
        t.Sport = "football"
        t.TeamLabel = "~/NFL ICONS/Bengals Label.png"
        t.TeamIcon = "~/NFL ICONS/Bengals.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team7"
        t.TeamName = "browns"
        t.Sport = "football"
        t.TeamLabel = "~/NFL ICONS/Browns Label.png"
        t.TeamIcon = "~/NFL ICONS/Browns.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team8"
        t.TeamName = "steelers"
        t.Sport = "football"
        t.TeamLabel = "~/NFL ICONS/Steelers Label.png"
        t.TeamIcon = "~/NFL ICONS/Steelers.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team9"
        t.TeamName = "texans"
        t.Sport = "football"
        t.TeamLabel = "~/NFL ICONS/Texans Label.png"
        t.TeamIcon = "~/NFL ICONS/Texans.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team10"
        t.TeamName = "colts"
        t.Sport = "football"
        t.TeamLabel = "~/NFL ICONS/Colts Label.png"
        t.TeamIcon = "~/NFL ICONS/Colts.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team11"
        t.TeamName = "jaguars"
        t.Sport = "football"
        t.TeamLabel = "~/NFL ICONS/Jaguars Label.png"
        t.TeamIcon = "~/NFL ICONS/Jaguars.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team12"
        t.TeamName = "titans"
        t.Sport = "football"
        t.TeamLabel = "~/NFL ICONS/Titans Label.png"
        t.TeamIcon = "~/NFL ICONS/Titans.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team13"
        t.TeamName = "broncos"
        t.Sport = "football"
        t.TeamLabel = "~/NFL ICONS/Broncos Label.png"
        t.TeamIcon = "~/NFL ICONS/Broncos.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team14"
        t.TeamName = "chiefs"
        t.Sport = "football"
        t.TeamLabel = "~/NFL ICONS/Chiefs Label.png"
        t.TeamIcon = "~/NFL ICONS/Chiefs.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team15"
        t.TeamName = "raiders"
        t.Sport = "football"
        t.TeamLabel = "~/NFL ICONS/Raiders Label.png"
        t.TeamIcon = "~/NFL ICONS/Raiders.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team16"
        t.TeamName = "chargers"
        t.Sport = "football"
        t.TeamLabel = "~/NFL ICONS/Chargers Label.png"
        t.TeamIcon = "~/NFL ICONS/Chargers.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team17"
        t.TeamName = "cowboys"
        t.Sport = "football"
        t.TeamLabel = "~/NFL ICONS/Cowboys Label.png"
        t.TeamIcon = "~/NFL ICONS/Cowboys.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team18"
        t.TeamName = "giants"
        t.Sport = "football"
        t.TeamLabel = "~/NFL ICONS/Giants Label.png"
        t.TeamIcon = "~/NFL ICONS/Giants.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team19"
        t.TeamName = "eagles"
        t.Sport = "football"
        t.TeamLabel = "~/NFL ICONS/Eagles Label.png"
        t.TeamIcon = "~/NFL ICONS/Eagles.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team20"
        t.TeamName = "redskins"
        t.Sport = "football"
        t.TeamLabel = "~/NFL ICONS/Redskins Label.png"
        t.TeamIcon = "~/NFL ICONS/Redskins.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team21"
        t.TeamName = "bears"
        t.Sport = "football"
        t.TeamLabel = "~/NFL ICONS/Bears Label.png"
        t.TeamIcon = "~/NFL ICONS/Bears.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team22"
        t.TeamName = "lions"
        t.Sport = "football"
        t.TeamLabel = "~/NFL ICONS/Lions Label.png"
        t.TeamIcon = "~/NFL ICONS/Lions.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team23"
        t.TeamName = "packers"
        t.Sport = "football"
        t.TeamLabel = "~/NFL ICONS/Packers Label.png"
        t.TeamIcon = "~/NFL ICONS/Packers.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team24"
        t.TeamName = "vikings"
        t.Sport = "football"
        t.TeamLabel = "~/NFL ICONS/Vikings Label.png"
        t.TeamIcon = "~/NFL ICONS/Vikings.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team25"
        t.TeamName = "falcons"
        t.Sport = "football"
        t.TeamLabel = "~/NFL ICONS/Falcons Label.png"
        t.TeamIcon = "~/NFL ICONS/Falcons.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team26"
        t.TeamName = "panthers"
        t.Sport = "football"
        t.TeamLabel = "~/NFL ICONS/Panthers Label.png"
        t.TeamIcon = "~/NFL ICONS/Panthers.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team27"
        t.TeamName = "saints"
        t.Sport = "football"
        t.TeamLabel = "~/NFL ICONS/Saints Label.png"
        t.TeamIcon = "~/NFL ICONS/Saints.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team28"
        t.TeamName = "buccaneers"
        t.Sport = "football"
        t.TeamLabel = "~/NFL ICONS/Buccaneers Label.png"
        t.TeamIcon = "~/NFL ICONS/Buccaneers.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team29"
        t.TeamName = "cardinals"
        t.Sport = "football"
        t.TeamLabel = "~/NFL ICONS/Cardinals Label.png"
        t.TeamIcon = "~/NFL ICONS/Cardinals.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team30"
        t.TeamName = "rams"
        t.Sport = "football"
        t.TeamLabel = "~/NFL ICONS/Rams Label.png"
        t.TeamIcon = "~/NFL ICONS/Rams.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team31"
        t.TeamName = "49ners"
        t.Sport = "football"
        t.TeamLabel = "~/NFL ICONS/_49ners Label.png"
        t.TeamIcon = "~/NFL ICONS/_49ners.png"
        teams.Add(t)

        t = New Team
        t.TeamId = "team32"
        t.TeamName = "seahawks"
        t.Sport = "football"
        t.TeamLabel = "~/NFL ICONS/Seahawks Label.png"
        t.TeamIcon = "~/NFL ICONS/Seahawks.png"
        teams.Add(t)

        Return teams

    End Function

End Class
