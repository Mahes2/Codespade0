Imports MySql.Data.MySqlClient

Public Class Form1

    Dim connection As MySqlConnection

    Private Function Connect(connection As MySqlConnection) As Boolean
        Try
            connection.ConnectionString = "server='localhost';user='root';password='';database='test'"
            connection.Open()
            MsgBox("Success", vbInformation, "CodeSpade")
            Return True
        Catch ex As Exception
            MsgBox(ex.Message, vbObjectError, "CodeSpade")
            Return False
        End Try
    End Function

    Private Sub Disconnect()
        Try
            connection.Close()
            MsgBox("Success", vbInformation, "CodeSpade")
        Catch ex As Exception
            MsgBox(ex.Message, vbObjectError, "CodeSpade")
        End Try
    End Sub

    Private Sub GetData(query As String)
        Dim da As New MySqlDataAdapter(query, connection)
        Dim dt As New DataTable
        da.Fill(dt)

        BindingSource1.DataSource = dt
        DataGridView1.DataSource = BindingSource1
    End Sub

    Private Sub PutData(query As String)
        Dim command As MySqlCommand = New MySqlCommand(query, connection)
        command.ExecuteNonQuery()
        MsgBox("[" + TextBox1.Text + "] has been inserted!", vbInformation, "CodeSpade")
    End Sub

    Private Sub DeleteData(query As String)
        Dim command As MySqlCommand = New MySqlCommand(query, connection)
        command.ExecuteNonQuery()
        MsgBox("[" + TextBox1.Text + "] has been deleted!", vbInformation, "CodeSpade")
    End Sub

    Private Sub BtnConnect_Click(sender As Object, e As EventArgs) Handles BtnConnect.Click
        Select Case BtnConnect.Text
            Case "Connect"
                connection = New MySqlConnection
                If (Connect(connection)) Then
                    BtnConnect.Text = "Disconnect"
                    BtnSelect.Enabled = True
                    BtnInsert.Enabled = True
                    BtnDelete.Enabled = True
                End If
            Case "Disconnect"
                Disconnect()
                BtnConnect.Text = "Connect"
                TextBox1.Text = ""
                BtnSelect.Enabled = False
                BtnInsert.Enabled = False
                BtnDelete.Enabled = False
                DataGridView1.DataSource = ""
        End Select
    End Sub

    Private Sub BtnSelect_Click(sender As Object, e As EventArgs) Handles BtnSelect.Click
        Dim query As String = "SELECT * FROM notification"
        GetData(query)
    End Sub

    Private Sub BtnInsert_Click(sender As Object, e As EventArgs) Handles BtnInsert.Click
        Dim query As String = "INSERT INTO notification(data) VALUES('" + TextBox1.Text + "')"
        PutData(query)

        Dim query2 As String = "SELECT * FROM notification"
        GetData(query2)
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click
        DataGridView1_SelectionChanged(sender, e)

        Dim query As String = "DELETE FROM notification WHERE id = '" + DataGridView1.Item(0, DataGridView1.CurrentRow.Index).Value.ToString + "'"
        DeleteData(query)

        Dim query2 As String = "SELECT * FROM notification"
        GetData(query2)
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        TextBox1.Text = DataGridView1.Item(1, DataGridView1.CurrentRow.Index).Value
    End Sub
End Class
