Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure

Namespace LosersPool.Models
    Public Class LosersPoolContext
        Inherits DbContext

        Public Property ScheduleEntities As DbSet(Of ScheduleEntity)
        Public Property ScheduleTimePeriods As DbSet(Of ScheduleTimePeriod)
        Public Property CurrentScoringUpdates As DbSet(Of CurrentScoringUpdate)
        Public Property ScoringUpdates As DbSet(Of ScoringUpdate)
        Public Property UserChoicesList As DbSet(Of UserChoices)
        Public Property LoserList As DbSet(Of Loser)
        Public Property MyPicks As DbSet(Of MyPick)
        Public Property ByeTeamsList As DbSet(Of ByeTeam)
        Public Property PostponedGames As DbSet(Of PostponedGame)
        Public Property DeletedGames As DbSet(Of DeletedGame)
        Public Property UserPicks As DbSet(Of UserPick)



        Public Sub New()
            MyBase.New("RussBucks-LoserPool")
        End Sub


    End Class
End Namespace
