Imports System
Imports System.Threading.Tasks
Imports System.Security.Claims
Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure

' You can add profile data for the user by adding more properties to your User class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

Public Class ApplicationDbContext
    Inherits DbContext

    Public Property SuperUsers As DbSet(Of SuperUser)
    Public Property AppFolders As DbSet(Of AppFolder)
    Public Property CronJobs As DbSet(Of CronJob)
    Public Property CronJobPools As DbSet(Of CronJobPool)
    Public Property CurrentCronJobs As DbSet(Of CurrentCronJob)

    Public Sub New()
        MyBase.New("RussBucks-Membership")
    End Sub


End Class


#Region "Helpers"

Public Class AppUser
    Public Property Id As String
    Public Property Email As String
    Public Property EmailConfirmed As Boolean
    Public Property PasswordHash As String
    Public Property SecurityStamp As String
    Public Property PhoneNumberConfirmed As Boolean
    Public Property TwoFactorEnabled As Boolean
    Public Property LockoutEnabled As Boolean
    Public Property AccessFailedCount As Int32
    Public Property UserName As String
End Class

Public Class SuperUser
    <Key>
    Public Property SuperUserId As Int32
    Public Property SuperUserName As String

    Public Shared Sub SeedSuperUser()
        Dim _dbMembership As New ApplicationDbContext

        Try
            Using (_dbMembership)

                Dim queryMembers = (From members1 In _dbMembership.SuperUsers).ToList

                If queryMembers.Count = 0 Then
                    Dim sU1 As New SuperUser
                    sU1.SuperUserName = "lh4uhotmailcom"
                    _dbMembership.SuperUsers.Add(sU1)

                    _dbMembership.SaveChanges()

                End If
            End Using
        Catch ex As Exception

        End Try
    End Sub


End Class

Public Class AppFolder
    <Key>
    Public Property FolderId As Int32
    Public Property rootFolder As String
    Public Property driverRootFolder As String
    Public Property scrapedFilesFolder As String
    Public Property scrapedScheduleFilesFolder As String
    Public Property testCronJobFolder As String
    Public Property scheduleCronJobFolder As String
    Public Property scoreCronJobFolder As String


End Class

Public Class CronJob

    <Key>
    Public Property CronJobId As Int32
    Public Property CronJobName As String
    Public Property SelectedSport As String
    Public Property SelectedPool As String
    Public Property SelectedSeasonStartDate As String
    Public Property SelectedSeasonStartGameDate As String
    Public Property SelectedSeasonEndDate As String
    Public Property UserTestIsSelected As Boolean
    Public Property CustomScheduleIsSelected As Boolean
    Public Property ContinueTestIsSelected As Boolean
    Public Property CronJobType As String
    Public Property CronJobNumber As String
    Public Property CronJobStartDateTime As String
    Public Property CronJobIsPreseason As Boolean

End Class

Public Class CronJobPool
    <Key>
    Public Property CronJobPoolId As Int32
    Public Property CronJobName As String
    Public Property CronJobPoolAlias As String
End Class

Public Class CurrentCronJob

    <Key>
    Public Property CronJobId As Int32
    Public Property CronJobName As String
    Public Property CronJobType As String
    Public Property CronJobNumber As String
    Public Property CronJobStartDateTime As String

End Class


#End Region