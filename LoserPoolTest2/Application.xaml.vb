Imports System
Imports System.Windows
Imports System.Collections.Generic

Imports System.Threading
Imports System.Threading.Tasks

Imports WpfApplication1.JoinPools.Models
Class Application


    ' Application-level events, such as Startup, Exit, and DispatcherUnhandledException
    ' can be handled in this file.

    Public Property CronJobName1 As String

    Private Sub Application_Startup(sender As Object, e As System.Windows.StartupEventArgs) Handles Me.Startup
        Dim _dbApp As New ApplicationDbContext
        Try
            Using (_dbApp)

                Dim queryCronCurrentCronJob = (From qCCJ1 In _dbApp.CurrentCronJobs).Single
                Me.CronJobName1 = queryCronCurrentCronJob.CronJobName
            End Using
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Application_Exit(ByVal sender As Object, ByVal e As System.Windows.ExitEventArgs) Handles Me.Exit

        My.Application.Shutdown()

    End Sub

End Class