Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Imports WpfApplication1.JoinPools.Models

Public Class Pool

    <Key>
    Public Property PoolId As Int32

    <Required>
    Public Property PoolName As String

    Public Property Sport As String
    Public Property timePeriodName As String
    Public Property timePeriodIncrement As String

End Class



