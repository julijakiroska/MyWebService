Public Class BazaRestLog
    Public varibBaza As New VarIB

    Public Sub ZapisLog(ByVal Funkcija As String, ByVal Procedura As String, ByVal exception As String)

        Dim selCmd As New System.Data.SqlClient.SqlCommand


        Try
            With selCmd
                .CommandType = CommandType.StoredProcedure
                .CommandText = "mc_ZapisRestLog"
                .Connection = varibBaza.CnSrv
                .Parameters.Clear()
                .Parameters.Add(New SqlClient.SqlParameter("@RestSvr", "IBRestMerc"))
                .Parameters.Add(New SqlClient.SqlParameter("@Datum", DateTime.Now))
                .Parameters.Add(New SqlClient.SqlParameter("@TokenStamp", DajTokenStamp))
                .Parameters.Add(New SqlClient.SqlParameter("@Funkcija", Funkcija))
                .Parameters.Add(New SqlClient.SqlParameter("@Procedura", Procedura))
                .Parameters.Add(New SqlClient.SqlParameter("@Exception", exception))
                .Parameters.Add(New SqlClient.SqlParameter("@Flag", "L"))
                .ExecuteNonQuery()

            End With

        Catch eX As Exception
            ''MsgBox(eX.Message)

        End Try
        varibBaza.CloseConnection()
    End Sub


    Public Sub ZapisGreska(ByVal Funkcija As String, ByVal Procedura As String, ByVal exception As String)
        Dim selCmd As New System.Data.SqlClient.SqlCommand


        Try
            With selCmd
                .CommandType = CommandType.StoredProcedure
                .CommandText = "mc_ZapisRestLog"
                .Connection = varibBaza.CnSrv
                .Parameters.Clear()
                .Parameters.Add(New SqlClient.SqlParameter("@RestSvr", "IBRestMerc"))
                .Parameters.Add(New SqlClient.SqlParameter("@Datum", DateTime.Now))
                .Parameters.Add(New SqlClient.SqlParameter("@TokenStamp", DajTokenStamp))
                .Parameters.Add(New SqlClient.SqlParameter("@Funkcija", Funkcija))
                .Parameters.Add(New SqlClient.SqlParameter("@Procedura", Procedura))
                .Parameters.Add(New SqlClient.SqlParameter("@Exception", exception))
                .Parameters.Add(New SqlClient.SqlParameter("@Flag", "G"))
                .ExecuteNonQuery()

            End With


        Catch eX As Exception
            ''MsgBox(eX.Message)

        End Try
        varibBaza.CloseConnection()

    End Sub
    Private Function DajTokenStamp() As String
        Try
            Return DateTime.Now.ToString("ddMMyyHHmmssfff")
        Catch eX As Exception
        End Try
        Return ""
    End Function
End Class
