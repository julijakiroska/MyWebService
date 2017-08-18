Option Strict On
Imports System.Data.SqlClient
Imports BlokDokument.klasi

' NOTE: You can use the "Rename" command on the context menu to change the class name "Proba" in code, svc and config file together.
' NOTE: In order to launch WCF Test Client for testing this service, please selecroba.svc or Proba.svc.vb at the Solution Explorer and start debugging.
Public Class Proba
    Implements IProba

    Public Sub DoWork() Implements IProba.DoWork
    End Sub

    Public Sub New()
    End Sub
    Function zemiOrgEd(ByVal Sifra_OE As String) As Podatok Implements IProba.zemiOrgEd

        Dim podatok As New Podatok()
        Dim status As String = proveriValidnaOe(Sifra_OE).ToString()

        If status <> "" Then
            If proveriValidnaOe(Sifra_OE) Then
                ''postoi
            ElseIf status = "False" Then
                ''ne postoi
                podatok.greska = "False"

            Else
                ''ima nekoja logicka greska
                podatok.greska = status
            End If
        End If
        Return podatok
    End Function

    Function proveriValidnaOe(ByVal Sifra_OE As String) As Boolean
        Dim conn As SqlConnection
        Dim status As String = otvoriKonekcija(conn)

        If status <> "" Then
            Return False
        End If

        Dim mojCmd As New SqlCommand
        Dim citac As SqlDataReader
        Dim daliPostoi As Boolean = False

        Try
            With mojCmd
                .Connection = conn
                .CommandType = CommandType.StoredProcedure
                .CommandText = "sp_zemiOrged"
                .Parameters.Clear()
                .Parameters.Add(New SqlClient.SqlParameter("@Sifra_OE", Sifra_OE))

                citac = .ExecuteReader()
            End With

            If citac.Read Then
                With citac
                    Return True
                End With
            End If

        Catch ex As Exception
            status = ex.Message

        End Try

        Return False
    End Function

    Function proveriValidenDok(ByVal Sifra_Dok As String) As Boolean
        Dim conn As SqlConnection
        Dim status As String = otvoriKonekcija(conn)
        If status <> "" Then
            Return False
        End If

        Dim mojCmd As New SqlCommand
        Dim citac As SqlDataReader
        Dim daliPostoi As Boolean = False

        Try
            With mojCmd
                .Connection = conn
                .CommandType = CommandType.StoredProcedure
                .CommandText = "sp_zemiTipDok"
                .Parameters.Clear()
                .Parameters.Add(New SqlClient.SqlParameter("@Sifra_Dok", Sifra_Dok))

                citac = .ExecuteReader()
            End With
            If citac.Read Then
                With citac
                    Return True

                End With
            End If

        Catch ex As Exception
            status = ex.Message

        End Try

        Return False

    End Function

    Function proveriValidenBrojDok(ByVal Broj_Dok As String) As Boolean
        Dim conn As SqlConnection
        Dim status As String = otvoriKonekcija(conn)
        If status <> "" Then
            Return False
        End If

        Dim mojCmd As New SqlCommand
        Dim citac As SqlDataReader
        Dim daliPostoi As Boolean = False

        Try
            With mojCmd
                .Connection = conn
                .CommandType = CommandType.StoredProcedure
                .CommandText = "sp_zemiBrojDok"
                .Parameters.Clear()
                .Parameters.Add(New SqlClient.SqlParameter("@Broj_Dok", Broj_Dok))

                citac = .ExecuteReader()
            End With
            If citac.Read Then
                With citac
                    Return True
                End With
            End If

        Catch ex As Exception
            status = ex.Message

        End Try

        Return False
    End Function

    Function proveriValidenBrojProekt(ByVal Broj_proekt As String) As Boolean

        Dim conn As SqlConnection
        Dim status As String = otvoriKonekcija(conn)
        If status <> "" Then
            Return False
        End If

        Dim mojCmd As New SqlCommand
        Dim citac As SqlDataReader
        Dim daliPostoi As Boolean = False

        Try
            With mojCmd
                .Connection = conn
                .CommandType = CommandType.StoredProcedure
                .CommandText = "sp_zemiBrojProekt"
                .Parameters.Clear()
                .Parameters.Add(New SqlClient.SqlParameter("@Broj_proekt", Broj_proekt))

                citac = .ExecuteReader()
            End With
            If citac.Read Then
                With citac
                    Return True
                End With
            End If

        Catch ex As Exception
            status = ex.Message

        End Try

        Return False
    End Function

    Function zemiTipDok(ByVal Sifra_Dok As String) As Podatok Implements IProba.zemiTipDok


        Dim podatok As New Podatok()
        Dim status As String = proveriValidenDok(Sifra_Dok).ToString()
        If status <> "" Then
            If status = "True" Then
                ''postoi
            ElseIf status = "False" Then
                ''ne postoi
                podatok.greska = "False"

            Else
                ''ima nekoja logicka greska
                podatok.greska = status
            End If
        End If
        Return podatok


    End Function

    Function zemiBrojDokument(ByVal Broj_Dok As String) As Podatok Implements IProba.zemiBrojDok

        Dim podatok As New Podatok()
        Dim status As String = proveriValidenBrojDok(Broj_Dok).ToString()
        If status <> "" Then
            If status = "True" Then
                ''postoi
            ElseIf status = "False" Then
                ''ne postoi
                podatok.greska = "False"

            Else
                ''ima nekoja logicka greska
                podatok.greska = status
            End If
        End If
        Return podatok
    End Function

    Function zemiBrojProekt(ByVal Broj_proekt As String) As Podatok Implements IProba.zemiBrojproekt

        Dim podatok As New Podatok()
        Dim status As String = proveriValidenBrojDok(Broj_proekt).ToString()
        If status <> "" Then
            If status = "True" Then
                ''postoi
            ElseIf status = "False" Then
                ''ne postoi
                podatok.greska = "False"

            Else
                ''ima nekoja logicka greska
                podatok.greska = status
            End If
        End If
        Return podatok
    End Function

    Public Function ProverkaNaDokument(ByVal Sifra_OE As String, ByVal Sifra_Dok As String, ByVal Broj_Dok As String, ByVal Broj_proekt As String) As ProverkaNaDokument Implements IProba.ProverkaNaDokument

        Dim conn As SqlConnection
        Dim podatok As New Podatok
        Dim provDok As New ProverkaNaDokument
        Dim mojCmd As New SqlCommand
        Dim citac As SqlDataReader
        Dim vrakaDok As Boolean = False

        podatok.greska = otvoriKonekcija(conn)

        If podatok.greska <> "" Then
        End If

        If String.IsNullOrEmpty(conn.ToString()) <> True Then

            Try
                With mojCmd
                    .Connection = conn
                    .CommandType = CommandType.StoredProcedure
                    .CommandText = "sp_ProverkaNaDokument"
                    .Parameters.Clear()
                    .Parameters.Add(New SqlClient.SqlParameter("@Sifra_OE", Sifra_OE))
                    .Parameters.Add(New SqlClient.SqlParameter("@Sifra_dok", Sifra_Dok))
                    .Parameters.Add(New SqlClient.SqlParameter("@Broj_Dok", Broj_Dok))
                    .Parameters.Add(New SqlClient.SqlParameter("@Broj_proekt", Broj_proekt))

                    citac = .ExecuteReader()
                End With

                If citac.Read Then

                    vrakaDok = True ' takov dokument postoi

                    provDok.Sifra_OE = citac("Sifra_OE").ToString()
                    provDok.GreskaSifra_OE = ""
                    provDok.Sifra_Dok = citac("Sifra_Dok").ToString()
                    provDok.GreskaSifra_Dok = ""
                    provDok.Broj_Dok = citac("Broj_Dok").ToString()
                    provDok.GreskaBroj_Dok = ""
                    provDok.Broj_proekt = citac("Broj_proekt").ToString()
                    provDok.GreskaBroj_proekt = ""

                    podatok.greska = ""

                    Return provDok
                End If
            Catch ex As Exception

            Finally
                conn.Close()
            End Try

        End If

        Dim statusOE As Boolean = proveriValidnaOe(Sifra_OE)

        Dim statusDok As Boolean = proveriValidenDok(Sifra_Dok)

        Dim statusBroj As Boolean = proveriValidenBrojDok(Broj_Dok)

        Dim statusPin As Boolean = proveriValidenBrojProekt(Broj_proekt)

        Try
            provDok.Sifra_OE = Sifra_OE
            provDok.GreskaSifra_OE = IIf(statusOE, "", "Sifra_OE не постои").ToString
            provDok.Sifra_Dok = Sifra_Dok
            provDok.GreskaSifra_Dok = IIf(statusDok, "", "Sifra_Dok не постои").ToString
            provDok.Broj_Dok = Broj_Dok
            provDok.GreskaBroj_Dok = IIf(statusBroj, "", "Broj_Dok не постои").ToString
            provDok.Broj_proekt = Broj_proekt
            provDok.GreskaBroj_proekt = IIf(statusPin, "", "Внесениот пин не постои").ToString

        Catch ex As Exception
        Finally
            conn.Close()
        End Try
        Return provDok

    End Function

    Public Function VnesiStatusNaNar(ByVal Identif_Br As String, ByVal Sifra_OE As String, ByVal Sifra_Dok As String, ByVal Broj_Dok As String, ByVal Broj_proekt As String) As klasi.Podatok Implements IProba.VnesiStatusNaNar
        Dim conn As SqlConnection
        Dim mojCmd As New SqlCommand
        Dim vneseno As New Podatok
        Dim brUpdate As Integer

        otvoriKonekcija(conn)

        Try
            With mojCmd
                .Connection = conn
                .CommandType = CommandType.StoredProcedure
                .CommandText = "sp_vnesiStatusNaNar"
                .Parameters.Clear()
                .Parameters.Add(New SqlClient.SqlParameter("@Sifra_OE", Sifra_OE))
                .Parameters.Add(New SqlClient.SqlParameter("@Sifra_dok", Sifra_Dok))
                .Parameters.Add(New SqlClient.SqlParameter("@Broj_Dok", Broj_Dok))
                .Parameters.Add(New SqlClient.SqlParameter("@Broj_proekt", Broj_proekt))
                .Parameters.Add(New SqlClient.SqlParameter("@Identif_Br", Identif_Br))
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
End Class
