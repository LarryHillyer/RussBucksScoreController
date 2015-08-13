Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace LosersPool.Models
    Public Class Loser

        <Key>
        Public Property ListId As Int32
        Public Property UserId As String
        Public Property UserName As String
        Public Property TimePeriod As String
        Public Property TimePeriodInt As Int32
        Public Property LosingPick As String
        Public Property PoolAlias As String
        Public Property Sport As String
        Public Property CronJob As String

    End Class
End Namespace