Imports System.Data.SqlClient
Imports BlokDokument.klasi

' NOTE: You can use the "Rename" command on the context menu to change the class name "Service1" in code, svc and config file together.
' NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.vb at the Solution Explorer and start debugging.
Public Class Service1
    Implements IService1

    Public Sub New()
    End Sub


    Public Function ProverkaKorisnik(ByVal Username As String, ByVal Password As String) As String Implements IService1.ProverkaKorisnik

        Dim conn As SqlConnection
        otvoriKonekcija(conn)
        Dim mojCmd As New SqlCommand
        Dim citac As SqlDataReader
        Dim daliPostoiKorisnik As String = "False"

        Try
            With mojCmd
                .Connection = conn
                .CommandType = CommandType.StoredProcedure
                .CommandText = "rk_Proverka_Korisnik_za_Promet"
                .Parameters.Clear()
                .Parameters.Add(New SqlClient.SqlParameter("@Username", Username))
                .Parameters.Add(New SqlClient.SqlParameter("@Password", Password))
                citac = .ExecuteReader()


            End With

            While citac.Read
                With citac
                    daliPostoiKorisnik = "True"
                End With
            End While


        Catch ex As Exception
            daliPostoiKorisnik = ex.Message

        Finally
            conn.Close()
        End Try



        Return daliPostoiKorisnik


    End Function
    Public Function zemiOrgEd() As List(Of OrgEd) Implements IService1.zemiOrgEd

        Dim conn As SqlConnection
        otvoriKonekcija(conn)
        Dim mojCmd As New SqlCommand
        Dim citac As SqlDataReader
        Dim greska As String = "False"
        Dim OrgEd As New OrgEd
        Dim lista As New List(Of OrgEd)

        Try
            With mojCmd
                .Connection = conn
                .CommandType = CommandType.StoredProcedure
                .CommandText = "rk_Zemi_OrgEd"
                .Parameters.Clear()
                citac = .ExecuteReader()

            End With

            While citac.Read
                With citac
                    OrgEd = New klasi.OrgEd
                    OrgEd.Sifra_OE = .Item("Sifra_OE")
                    'OrgEd.ImeOrg = .Item("ImeOrg").Trim()
                    lista.Add(OrgEd)
                    greska = "True"
                End With
            End While

        Catch ex As Exception
            greska = ex.Message

        Finally
            conn.Close()
        End Try

        Return lista

    End Function

    Public Function zemiGrupi() As List(Of Grupa) Implements IService1.zemiGrupi

        Dim conn As SqlConnection
        otvoriKonekcija(conn)
        Dim mojCmd As New SqlCommand
        Dim citac As SqlDataReader
        Dim greska As String = "False"
        Dim Grupa As New Grupa
        Dim lista As New List(Of Grupa)

        Try
            With mojCmd
                .Connection = conn
                .CommandType = CommandType.StoredProcedure
                .CommandText = "rk_Zemi_Grupi"
                .Parameters.Clear()
                citac = .ExecuteReader()

            End With

            While citac.Read
                With citac
                    Grupa = New klasi.Grupa
                    Grupa.Sifra_Gr = .Item("Sifra_Gr").Trim()
                    Grupa.Ime_Gr = .Item("Ime_Gr").Trim()
                    lista.Add(Grupa)
                    greska = "True"
                End With
            End While


        Catch ex As Exception
            greska = ex.Message

        Finally
            conn.Close()
        End Try

        Return lista

    End Function

    Public Function zemiPodgrupi() As List(Of Podgrupa) Implements IService1.zemiPodgrupi

        Dim conn As SqlConnection
        otvoriKonekcija(conn)
        Dim mojCmd As New SqlCommand
        Dim citac As SqlDataReader
        Dim greska As String = "False"
        Dim Podgrupa As New Podgrupa
        Dim lista As New List(Of Podgrupa)

        Try
            With mojCmd
                .Connection = conn
                .CommandType = CommandType.StoredProcedure
                .CommandText = "rk_Zemi_Podgrupi"
                .Parameters.Clear()
                citac = .ExecuteReader()

            End With

            While citac.Read
                With citac
                    Podgrupa = New klasi.Podgrupa
                    Podgrupa.Sifra_Podg = .Item("Sifra_Podg").Trim()
                    Podgrupa.Ime_Podg = .Item("Ime_Podg").Trim()
                    lista.Add(Podgrupa)
                    greska = "True"
                End With
            End While

        Catch ex As Exception
            greska = ex.Message

        Finally
            conn.Close()
        End Try

        Return lista

    End Function

    Public Function IzvestajPrometMobilen(ByVal baranje As klasi.Baranje) As klasi.Odgovor Implements IService1.IzvestajPrometMobilen

        Dim conn As SqlConnection
        otvoriKonekcija(conn)
        Dim mojCmd As New SqlCommand
        Dim citac As SqlDataReader
        Dim greska As String = "False"
        Dim iOperator As New iOperator
        Dim iArtikal As New iArtikal
        Dim iSmetka As New iSmetka
        Dim iNacin As New iNacin
        Dim listaArtikli As New List(Of klasi.iArtikal)
        Dim listaSmetki As New List(Of klasi.iSmetka)
        Dim listaOperatori As New List(Of klasi.iOperator)
        Dim listaNacin As New List(Of klasi.iNacin)
        Dim odgovor As New klasi.Odgovor

        Try
            With mojCmd
                .Connection = conn
                .CommandType = CommandType.StoredProcedure
                .CommandText = "rk_izvestaj_promet_mobilen"
                .Parameters.Clear()
                .Parameters.Add(New SqlClient.SqlParameter("@datumOd", baranje.datumOd))
                .Parameters.Add(New SqlClient.SqlParameter("@datumDo", baranje.datumDo))
                .Parameters.Add(New SqlClient.SqlParameter("@SifraOe", baranje.SifraOe))
                .Parameters.Add(New SqlClient.SqlParameter("@SifraOper", baranje.SifraOper))
                .Parameters.Add(New SqlClient.SqlParameter("@SifraPodgrupa", baranje.SifraPodgrupa))
                .Parameters.Add(New SqlClient.SqlParameter("@SifraGrupa", baranje.SifraGrupa))
                .Parameters.Add(New SqlClient.SqlParameter("@SifraNac", baranje.SifraNac))
                .Parameters.Add(New SqlClient.SqlParameter("@Opcija", baranje.Opcija))
                citac = .ExecuteReader()

            End With

            If baranje.Opcija = Nothing Then
                baranje.Opcija = "A"

            End If

            If baranje.Opcija = "S" Then
                'lista = New List(Of klasi.iSmetka)
                While citac.Read
                    With citac
                        iSmetka = New klasi.iSmetka
                        iSmetka.Sifra_Oe = .Item("Sifra_Oe").ToString()
                        iSmetka.ImeNac = .Item("ImeNac").Trim()
                        iSmetka.Sifra_Oper = .Item("Sifra_Oper")
                        iSmetka.Datum_Evid = .Item("Datum_Evid")
                        iSmetka.Iznos = Math.Round(.Item("Iznos"), 2)
                        listaSmetki.Add(iSmetka)
                        greska = "True"
                    End With
                End While
            End If

            If baranje.Opcija = "A" Then

                While citac.Read
                    With citac
                        iArtikal = New klasi.iArtikal
                        iArtikal.Sifra_Oe = .Item("Sifra_Oe")
                        iArtikal.Sifra_Art = .Item("Sifra_Art")
                        iArtikal.ImeArt = .Item("ImeArt").Trim()
                        iArtikal.Kolicina = Math.Round(.Item("Kolicina"), 2)
                        iArtikal.Cena = Math.Round(.Item("Cena"), 2)
                        iArtikal.Iznos = Math.Round(.Item("Iznos"), 2)
                        listaArtikli.Add(iArtikal)
                        greska = "True"
                    End With
                End While
            End If

            If baranje.Opcija = "O" Then

                While citac.Read
                    With citac
                        iOperator = New klasi.iOperator
                        iOperator.Sifra_Oe = .Item("Sifra_Oe")
                        iOperator.Ime_Oper = .Item("ImeshiO_Oper").Trim()
                        iOperator.Sifra_Oper = .Item("Sifra_Oper")
                        iOperator.ImeNac = .Item("ImeNac").Trim()
                        iOperator.Iznos = Math.Round(.Item("Iznos"), 2)
                        listaOperatori.Add(iOperator)
                        greska = "True"
                    End With
                End While
            End If

            If baranje.Opcija = "P" Then

                While citac.Read
                    With citac
                        iNacin = New klasi.iNacin
                        iNacin.Sifra_Oe = .Item("Sifra_Oe")
                        iNacin.Sifra_Nac = .Item("Sifra_Nac")
                        iNacin.ImeNac = .Item("ImeNac").Trim()
                        iNacin.Iznos = Math.Round(.Item("Iznos"), 2)
                        listaNacin.Add(iNacin)
                        greska = "True"
                    End With
                End While
            End If

        Catch ex As Exception
            greska = ex.Message

        Finally
            conn.Close()
        End Try

        odgovor.listaArtikli = listaArtikli
        odgovor.listaNacin = listaNacin
        odgovor.listaOperatori = listaOperatori
        odgovor.listaSmetki = listaSmetki

        Return odgovor

    End Function

    Public Function otvoriKonekcija(ByRef MojaKonekcija As SqlConnection)

        Dim cnnString As String
        MojaKonekcija = New SqlConnection

        Dim server As String = Konv.ObjVoStr(System.Configuration.ConfigurationManager.AppSettings.Get("ImeServer"))
        Dim database As String = Konv.ObjVoStr(System.Configuration.ConfigurationManager.AppSettings.Get("ImeBaza"))
        Dim userId As String = Konv.ObjVoStr(System.Configuration.ConfigurationManager.AppSettings.Get("UserId"))
        Dim password As String = Konv.ObjVoStr(System.Configuration.ConfigurationManager.AppSettings.Get("Password"))
        Dim ConnectionTimeout As String = Konv.ObjVoStr(System.Configuration.ConfigurationManager.AppSettings.Get("ConnectionTimeout"))

        If ConnectionTimeout = "" Then
            ConnectionTimeout = 150
        End If

        cnnString = "server=" & server & ";" &
                        "database=" & database & ";" &
                          "user ID=" & userId & ";" &
                          "password=" & password &
                          "; Connection Timeout=" & ConnectionTimeout

        MojaKonekcija.ConnectionString = cnnString

        MojaKonekcija.Open()

    End Function

    Public Function zatvoriKonekcija(ByRef MojaKonekcija As SqlConnection)

        MojaKonekcija.Close()

    End Function

End Class
