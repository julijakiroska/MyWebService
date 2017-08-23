Option Strict On

Public Class Konv
    Public Shared DefDatum As Date = Date.MinValue

    Public Shared Function TxtVoDec(ByVal Txt As String) As Decimal
        Dim Rez As Decimal
        Try
            If Txt = "" Then        'Zaradi efikasnost (mnogu e spor exception handlingot)
                Return 0
            End If
            Rez = Convert.ToDecimal(Txt)
        Catch
            Return 0
        End Try
        Return Rez
    End Function

    Public Shared Function TxtVoSingle(ByVal Txt As String) As Single
        Dim Rez As Single
        Try
            If Txt = "" Then
                Return 0
            End If
            Rez = Convert.ToSingle(Txt)
        Catch
            Return 0
        End Try
        Return Rez
    End Function

    Public Shared Function TxtVoDouble(ByVal Txt As String) As Double
        Dim Rez As Double
        Try
            If Txt = "" Then
                Return 0
            End If
            Rez = Convert.ToDouble(Txt)
        Catch
            Return 0
        End Try
        Return Rez
    End Function

    Public Shared Function TxtVoInt(ByVal Txt As String) As Integer
        Dim RezDec As Decimal
        Dim Rez As Integer
        Try
            If Txt = "" Then
                Return 0
            End If
            Rez = Convert.ToInt32(Txt)
        Catch
            Try
                RezDec = Math.Round(Convert.ToDecimal(Txt), 0)
                Rez = Convert.ToInt32(RezDec)
            Catch
                Return 0
            End Try
        End Try
        Return Rez
    End Function

    Public Shared Function TxtVoInt16(ByVal Txt As String) As Int16
        Dim RezDec As Decimal
        Dim Rez As Int16
        Try
            If Txt = "" Then
                Return 0
            End If
            Rez = Convert.ToInt16(Txt)
        Catch
            Try
                RezDec = Math.Round(Convert.ToDecimal(Txt), 0)
                Rez = Convert.ToInt16(RezDec)
            Catch
                Return 0
            End Try
        End Try
        Return Rez
    End Function

    Public Shared Function DateTimeVoDat(ByVal txt As String) As String
        Dim i As Integer
        i = InStr(txt, " ")
        If i > 0 Then
            Return Left(txt, i - 1)
        Else
            Return txt
        End If
    End Function

    Public Shared Function IntVoStr(ByVal txt As Integer) As String
        Dim Rez As String = ""

        Try
            Rez = Convert.ToString(txt)
        Catch

        End Try
        If Rez.Trim = "0" Then Rez = " "
        Return Rez
    End Function

    Public Shared Function ObjVoInt(ByVal Sto As Object) As Integer
        Dim Rez As Integer
        If Sto Is DBNull.Value Then
            Return 0
        Else
            Try
                Rez = Convert.ToInt32(Sto)
                Return Rez
            Catch
                Return 0
            End Try
        End If
    End Function

    Public Shared Function ObjVoInt64(ByVal Sto As Object) As Int64
        Dim Rez As Int64
        If Sto Is DBNull.Value Then
            Return 0
        Else
            Try
                Rez = Convert.ToInt64(Sto)
                Return Rez
            Catch
                Return 0
            End Try
        End If
    End Function

    Public Shared Function ObjVoByte(ByVal Sto As Object) As Byte
        Dim Rez As Byte
        If Sto Is DBNull.Value Then
            Return 0
        Else
            Try
                Rez = Convert.ToByte(Sto)
                Return Rez
            Catch
                Return 0
            End Try
        End If
    End Function

    Public Shared Function ObjVoDec(ByVal Sto As Object) As Decimal
        Dim Rez As Decimal
        If Sto Is DBNull.Value Then
            Return 0
        Else
            Try
                Rez = Convert.ToDecimal(Sto)
                Return Rez
            Catch
                Return 0
            End Try
        End If
    End Function

    Public Shared Function ObjVoDouble(ByVal Sto As Object) As Double
        Dim Rez As Double
        If Sto Is DBNull.Value Then
            Return 0
        Else
            Try
                Rez = Convert.ToDouble(Sto)
                Return Rez
            Catch
                Return 0
            End Try
        End If
    End Function

    Public Shared Function ObjVoStr(ByVal Sto As Object) As String
        If Sto Is DBNull.Value Then
            Return ""
        Else
            Return Convert.ToString(Sto)
        End If
    End Function

    Public Shared Function ObjVoStrSoTocka(ByVal Sto As Object) As String
        If Sto Is DBNull.Value Then
            Return ""
        Else
            Return Replace(Convert.ToString(Sto), ",", ".")
        End If
    End Function

    Public Shared Function ObjVoDate(ByVal Sto As Object) As Date
        If Sto Is DBNull.Value Then
            Return DefDatum
        Else
            Try
                Return Convert.ToDateTime(Sto)
            Catch
                Return DefDatum
            End Try
        End If
    End Function

    Public Shared Function TxtVoDate(ByVal Sto As String) As Date
        If Sto Is Nothing OrElse Sto = "" Then
            Return DefDatum
        Else
            Try
                Return Convert.ToDateTime(Sto)
            Catch
                Return DefDatum
            End Try
        End If
    End Function

    Public Shared Function ObjVoSingle(ByVal Sto As Object) As Single
        Dim Rez As Single
        If Sto Is DBNull.Value Then
            Return 0
        Else
            Try
                Rez = Convert.ToSingle(Sto)
                Return Rez
            Catch
                Return 0
            End Try
        End If
    End Function

    Public Shared Function TxtVoLong(ByVal Txt As String) As Long
        Dim RezDec As Decimal
        Dim Rez As Long
        Try
            If Txt = "" Then
                Return 0
            End If
            Rez = Convert.ToInt64(Txt)
        Catch
            Try
                RezDec = Math.Round(Convert.ToDecimal(Txt), 0)
                Rez = Convert.ToInt64(RezDec)
            Catch
                Return 0
            End Try
        End Try
        Return Rez
    End Function

    Public Shared Function LongVoStr(ByVal txt As Long) As String
        Dim Rez As String = ""

        Try
            Rez = Convert.ToString(txt)
        Catch

        End Try
        If Rez.Trim = "0" Then Rez = " "
        Return Rez
    End Function
	
	Public Shared Function DatVoDatW3(ByVal txt As String) As String
        Dim str As String 
        str = txt.Substring(6, 4)
        str += "-"
        str += txt.Substring(3, 2)
        str += "-"
        str += txt.Substring(0, 2)
        Return str
    End Function

    Public Shared Function KlonirajObjekt(ByVal obj1 As Object) As Object
        Try
            If TypeOf obj1 Is SqlClient.SqlCommand Then
                Dim sqlCmd1 As SqlClient.SqlCommand = CType(obj1, SqlClient.SqlCommand)
                Dim sqlCmd2 As New SqlClient.SqlCommand()
                sqlCmd2.CommandText = sqlCmd1.CommandText
                sqlCmd2.Connection = sqlCmd1.Connection
                sqlCmd2.CommandTimeout = sqlCmd1.CommandTimeout
                sqlCmd2.Parameters.Clear()
                Dim pomSqlcmd As SqlClient.SqlParameter
                For Each pomSqlcmd In sqlCmd1.Parameters
                    Dim sqlParam As New SqlClient.SqlParameter()
                    sqlParam.Value = pomSqlcmd.Value
                    sqlParam.DbType = pomSqlcmd.DbType
                    sqlParam.ParameterName = pomSqlcmd.ParameterName
                    sqlParam.SqlDbType = pomSqlcmd.SqlDbType
                    sqlParam.Direction = pomSqlcmd.Direction
                    sqlCmd2.Parameters.Add(sqlParam)
                Next
                sqlCmd2.CommandType = sqlCmd1.CommandType
                Return sqlCmd2
            End If
            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function ObjVoBoolean(ByVal Sto As Object) As Boolean
        Dim Rez As Boolean
        If Sto Is DBNull.Value Then
            Return False
        Else
            Try
                Rez = Convert.ToBoolean(Sto)
                Return Rez
            Catch
                Return False
            End Try
        End If
    End Function

End Class