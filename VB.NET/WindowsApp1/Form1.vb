Imports MySql.Data.MySqlClient

Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim connection As MySqlConnection
        Try
            connection = New MySqlConnection
            connection.ConnectionString = "server='localhost';user='root';password='';database='test'"
            connection.Open()
            MsgBox("Success")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class
