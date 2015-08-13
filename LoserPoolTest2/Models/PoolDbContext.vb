Imports System.Data.Entity

Namespace JoinPools.Models

    Public Class PoolDbContext
        Inherits DbContext

        Public Property Pools As DbSet(Of Pool)
        Public Property Teams As DbSet(Of Team)
        Public Property BarPoolList As DbSet(Of PoolAlias1)
        Public Property PoolAdministrators As DbSet(Of PoolAdministrator1)
        Public Property PoolParameters As DbSet(Of PoolParameter)
        Public Property Sports As DbSet(Of Sport)
        Public Property ScheduledGames As DbSet(Of ScheduledGame)
        Public Property CustomScheduledGames As DbSet(Of CustomScheduledGame)
        Public Property QueuedScheduledGames As DbSet(Of QueuedScheduleGame)
        Public Property Tests As DbSet(Of Test)
        Public Property Licensees As DbSet(Of licensee)
        Public Property SportDatesOfTheWeeks As DbSet(Of SportDatesOfTheWeek)

        Public Sub New()

            MyBase.New("RussBucks-Pools")

        End Sub

    End Class
    Public Class ScheduleFileXML
        Public Property filepath As String
    End Class



End Namespace

