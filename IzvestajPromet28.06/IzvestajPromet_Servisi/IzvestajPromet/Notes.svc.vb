Imports System.Data.SqlClient
Imports BlokDokument.klasi

Public Class Notes
    Implements INotes

    Public Sub DoWork() Implements INotes.DoWork
    End Sub

    Public Sub New()
    End Sub


    Public Function ProverkaOperatorMob(ByVal operatoriMob As klasi.OperatoriMob) As klasi.DaliPostoiKorisnik Implements INotes.ProverkaOperatorMob


        Dim conn As SqlConnection
        otvoriKonekcija(conn)
        Dim mojCmd As New SqlCommand
        Dim citac As SqlDataReader
        Dim operatori As New OperatoriMob
        Dim greska As String = "False"
        Dim daliPostoiKorisnik As New klasi.DaliPostoiKorisnik

        Try

            With mojCmd
                .Connection = conn
                .CommandType = CommandType.StoredProcedure
                .CommandText = "rk_ProverkaOperatorMob"
                .Parameters.Clear()
                .Parameters.Add(New SqlClient.SqlParameter("@Login_Ime", operatoriMob.Login_Ime))
                .Parameters.Add(New SqlClient.SqlParameter("@Lozinka", operatoriMob.Lozinka))
                citac = .ExecuteReader()


            End With

            If citac.HasRows Then
                If citac.Read Then
                    With citac

                        daliPostoiKorisnik.Sifra_Oper = .Item("Sifra_Oper").ToString
                        daliPostoiKorisnik.Sifra_TipOper = .Item("Sifra_TipOper").ToString
                        daliPostoiKorisnik.Ime_Oper = .Item("Ime_Oper").ToString

                    End With
                End If
            End If
        Catch ex As Exception

            greska = ex.Message

        Finally
            conn.Close()
        End Try



        Return daliPostoiKorisnik


    End Function

    Public Function zemiZabeleski(ByVal DatumOd As String, ByVal DatumDo As String, ByVal Sifra_Oper As String) As IList(Of ZabeleskiMob) Implements INotes.zemiZabeleski

        Dim conn As SqlConnection
        otvoriKonekcija(conn)
        Dim mojCmd As New SqlCommand
        Dim citac As SqlDataReader
        Dim greska As String = "False"

        Dim odgovor As New List(Of ZabeleskiMob)()

        Try
            With mojCmd
                .Connection = conn
                .CommandType = CommandType.StoredProcedure
                .CommandText = "rk_ZemiZabeleski"
                .Parameters.Clear()
                If DatumOd <> "" Then
                    Dim Datum_Od As Date = DateTime.ParseExact(DatumOd, "yyyy-MM-dd", Nothing)
                    .Parameters.Add(New SqlClient.SqlParameter("@DatumOd", Datum_Od))
                End If
                If DatumDo <> "" Then
                    Dim Datum_Do As Date = DateTime.ParseExact(DatumDo, "yyyy-MM-dd", Nothing)
                    .Parameters.Add(New SqlClient.SqlParameter("@DatumDo", Datum_Do))
                End If
                If Sifra_Oper <> "" Then
                    .Parameters.Add(New SqlClient.SqlParameter("@Sifra_Oper", Sifra_Oper))
                End If

                citac = .ExecuteReader()

            End With

            While citac.Read
                With citac
                    Dim ZabeleskiMob As New ZabeleskiMob
                    ZabeleskiMob.Ime_Oper = citac("Ime_Oper").ToString()
                    ZabeleskiMob.Zabeleska = citac("Zabeleska").ToString()
                    ZabeleskiMob.Data_Vnes = CDate(citac("Data_Vnes")).ToString("yyyy-MM-dd")
                    ZabeleskiMob.Sifra_Zab = citac("Sifra_Zab").ToString()
                    odgovor.Add(ZabeleskiMob)

                End With
            End While

        Catch ex As Exception
            greska = ex.Message

        Finally
            conn.Close()
        End Try

        Return odgovor.ToArray()

    End Function

    Public Function otvoriKonekcija(ByRef MojaKonekcija As SqlConnection) As String
        Dim Uspeh As Boolean = False
        Dim Poraka As String = ""
        Try
            Dim cnnString As String
            MojaKonekcija = New SqlConnection


            Dim server As String = Konv.ObjVoStr(System.Configuration.ConfigurationManager.AppSettings.Get("ImeServer"))
            Dim database As String = Konv.ObjVoStr(System.Configuration.ConfigurationManager.AppSettings.Get("ImeBaza"))
            Dim userId As String = Konv.ObjVoStr(System.Configuration.ConfigurationManager.AppSettings.Get("UserId"))
            Dim password As String = Konv.ObjVoStr(System.Configuration.ConfigurationManager.AppSettings.Get("Password"))
            Dim ConnectionTimeout As Integer = Konv.ObjVoInt(System.Configuration.ConfigurationManager.AppSettings.Get("ConnectionTimeout"))

            If ConnectionTimeout = 0 Then
                ConnectionTimeout = 150

            End If

            cnnString = "server=" & server & ";" &
                            "database=" & database & ";" &
                              "user ID=" & userId & ";" &
                              "password=" & password &
                              "; Connection Timeout=" & ConnectionTimeout

            'cnnString = "server=SERVERIB,1223;" +
            '                "database=wtrgPrez;" +
            '                  "user ID=Praktikant;" +
            '                  "password=123321;" +
            '                  "Connection Timeout=1000"
            MojaKonekcija.ConnectionString = cnnString

            MojaKonekcija.Open()

            Uspeh = True
        Catch ex As Exception
            Poraka = ex.Message
        End Try

        Return Poraka
    End Function

    Public Function VnesiZabeleski(ByVal Zabeleska As String, ByVal Sifra_Oper As Integer, ByVal Sifra_Zab As Integer) As klasi.Podatok Implements INotes.VnesiZabeleski
        Dim conn As SqlConnection
        Dim mojCmd As New SqlCommand
        Dim vneseno As New Podatok
        Dim brUpdate As Integer

        otvoriKonekcija(conn)

        Try
            With mojCmd
                .Connection = conn
                .CommandType = CommandType.StoredProcedure
                .CommandText = "rk_VnesiZabeleski"
                .Parameters.Clear()
                .Parameters.Add(New SqlClient.SqlParameter("@Zabeleska", Zabeleska))
                .Parameters.Add(New SqlClient.SqlParameter("@Sifra_Oper", Sifra_Oper))
                .Parameters.Add(New SqlClient.SqlParameter("@Sifra_Zab", Sifra_Zab))
                .Parameters.Add(New SqlClient.SqlParameter("@brUpdate", SqlDbType.Int))
                .Parameters("@brUpdate").Direction = ParameterDirection.Output

                .ExecuteNonQuery()

                brUpdate = Konv.ObjVoInt(.Parameters("@brUpdate").Value())

            End With

            If brUpdate = 0 Then
                vneseno.greska = "Not updated"
            Else
                vneseno.greska = "Updated"
            End If

        Catch ex As Exception
            vneseno.greska = ex.Message

        Finally
            conn.Close()
        End Try

        Return vneseno

    End Function

End Class
