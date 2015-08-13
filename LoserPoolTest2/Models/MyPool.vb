Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace JoinPools.Models
    Public Class MyPool

        <Key>
        Public Property UserId As String

        Public Property EName As String

        Public Property Loser As String

        Public Property Playoff As String

    End Class

End Namespace