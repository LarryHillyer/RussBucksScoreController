Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Public Class PoolAdministrator1
    <Key>
    Public Property PoolAdminId As Int32
    Public Property LicenseeUserId As String
    Public Property PoolAdministrator As String
    Public Property PoolAdministratorAlias As String

End Class
