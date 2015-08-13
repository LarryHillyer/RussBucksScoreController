Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace JoinPools.Models
    Public Class PoolAlias1

        <Key>
        Public Property PoolAliasId As Int32
        Public Property UserId As String
        Public Property UserName As String
        Public Property PoolName As String
        Public Property PoolAlias As String
        Public Property Sport As String
        Public Property CronJob As String

    End Class
End Namespace