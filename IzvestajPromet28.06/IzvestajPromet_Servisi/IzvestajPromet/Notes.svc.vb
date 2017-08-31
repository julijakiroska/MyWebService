Imports System.Data.SqlClient
Imports BlokDokument.klasi
Imports System.IO
Imports System.Drawing
Imports System.Web.Script.Serialization
Imports System.Drawing.Imaging


Public Class Notes
    Implements INotes

    Public Sub DoWork() Implements INotes.DoWork
    End Sub

    Public Sub New()
    End Sub

    Public Log As BazaRestLog

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
                vneseno.SifraZab = Sifra_Zab
            End If

        Catch ex As Exception
            vneseno.greska = ex.Message

        Finally
            conn.Close()
        End Try

        Return vneseno

    End Function

    Public Function ZacuvajSlika(ByVal ObjSlika As klasi.Slika) As Boolean Implements INotes.ZacuvajSlika

        Dim selCmd As New System.Data.SqlClient.SqlCommand
        Dim conn As SqlConnection
        otvoriKonekcija(conn)
        Dim sqlTrans As System.Data.SqlClient.SqlTransaction
        sqlTrans = conn.BeginTransaction
        Dim PatekaZacuvaj As String = VarIB.ZemiWRKonfig("PatekaDoSliki")
        Dim uspeh As Boolean = False

        If ObjSlika.SlikaStr <> "" Then

            Dim dataTmp As Byte() = Convert.FromBase64String(ObjSlika.SlikaStr)
            Dim pomosenSlika As Stream = New MemoryStream(dataTmp)

            uspeh = ZacuvajSlikaNaServer(pomosenSlika, ObjSlika.Ime)

            Dim serializer As New JavaScriptSerializer()
            Dim serInputObject As String = serializer.Serialize(ObjSlika)

            UnvFunkcii.zapisiInfoVoFileLog("ZacuvajSlika-Primen Objekt= " + serInputObject)

            If uspeh Then
                Try
                    With selCmd

                        .CommandType = CommandType.StoredProcedure
                        .CommandText = "ZacuvajSlika"
                        .Connection = conn
                        .Transaction = sqlTrans
                        .Parameters.Clear()
                        .Parameters.Add(New SqlClient.SqlParameter("@Sifra_Zab", ObjSlika.Sifra_Zab))
                        .Parameters.Add(New SqlClient.SqlParameter("@Pateka_Slika", PatekaZacuvaj + ObjSlika.Ime))
                        .Parameters.Add(New SqlClient.SqlParameter("@Ime", ObjSlika.Ime))
                        .ExecuteNonQuery()
                        sqlTrans.Commit()
                    End With
                Catch ex As Exception
                    uspeh = False
                    sqlTrans.Rollback()

                    Dim serializedResult As String = serializer.Serialize(ObjSlika)
                    UnvFunkcii.zapisiGreskaVoFileLog("ZacuvajSlika-" + ex.Message)
                    Log.ZapisGreska("ZacuvajSlika -- " + serializedResult, "mc_ZapisMerc_Slika_Stav", ex.ToString)
                End Try
            Else
                UnvFunkcii.zapisiGreskaVoFileLog("ZacuvajSlika -- NE e zacuvana slika na server(na disk)")
                uspeh = False
                sqlTrans.Rollback()
                Log.ZapisGreska("ZacuvajSlikaNaDisk -- " + ObjSlika.Ime, "", "")

            End If

        End If
        Return uspeh
    End Function

    Public Function ZacuvajSlikaNaServer(ByVal SlikaStream As Stream, ByVal ImeFajl As String) As Boolean

        Dim uspeh As Boolean = False
        Try


            Dim PatekaZacuvaj As String = VarIB.ZemiWRKonfig("PatekaDoSliki")
            'Dim PatekaZacuvaj As String = "C:\ib\RestKonfig\"
            ' PatekaDoSliki" value="C:\ib\SlikiZabeleski\

            If Not IO.Directory.Exists(PatekaZacuvaj) Then
                IO.Directory.CreateDirectory(PatekaZacuvaj)
            End If

            Dim pateka As String = PatekaZacuvaj
            Dim FilePath As String = pateka + ImeFajl

            Dim myOutputStream As IO.Stream = System.IO.File.OpenWrite(FilePath)

            Try
                Dim length As Integer = 0


                Dim myBufferSize As Integer = 1024
                Dim buffer(myBufferSize) As Byte
                ReDim buffer(myBufferSize)
                Dim numbytes As Integer
                Dim raboti As Boolean = True

                While raboti
                    numbytes = SlikaStream.Read(buffer, 0, buffer.Length)
                    If numbytes > 0 Then
                        myOutputStream.Write(buffer, 0, buffer.Length)
                        raboti = True
                    Else
                        raboti = False
                    End If
                End While

                myOutputStream.Close()
            Catch ex As Exception
                uspeh = False
                Log.ZapisGreska("ZacuvajSlikaNaServer -- ", "zapis slika file", ex.ToString)
                UnvFunkcii.zapisiGreskaVoFileLog("ZacuvajSlikaNaServer-zapis slika file " + ex.ToString)
            End Try

            uspeh = True
            UnvFunkcii.zapisiInfoVoFileLog("ZacuvajSlikaNaServer-Uspesno zacuvana slika, PATEKA_SLIKA=" + FilePath)
        Catch ex As Exception
            uspeh = False
            Log.ZapisGreska("ZacuvajSlikaNaServer -- ", "zapis slika folder", ex.ToString)
            UnvFunkcii.zapisiGreskaVoFileLog("ZacuvajSlikaNaServer-zapis slika file " + ex.ToString)
        End Try
        Return uspeh
    End Function

    Public Function ZemiSlikiZaPoseta(ByVal Sifra_Zab As Integer) As klasi.Slika
        Dim PatekaZacuvaj As String = VarIB.ZemiWRKonfig("PatekaDoSliki")
        Dim slikiZaPoseta As klasi.Slika = BGETZemiSlikiZaPoseta(Sifra_Zab)

        zemiSlikaZaStav(slikiZaPoseta)

        Return slikiZaPoseta

    End Function

    Private Sub zemiSlikaZaStav(ByRef slika As klasi.Slika)

        Dim image1 As Image
        image1 = Bitmap.FromFile(slika.Pateka_Slika)
        slika.SlikaStr = ImageToBase64(image1, ImageFormat.Jpeg)

    End Sub

    Public Function BGETZemiSlikiZaPoseta(ByVal Sifra_Zab As Integer) As klasi.Slika Implements INotes.BGETZemiSlikiZaPoseta
        Dim conn As SqlConnection
        otvoriKonekcija(conn)
        Dim mojCmd As New SqlCommand
        Dim citac As SqlDataReader
        Dim Slika As New klasi.Slika

        Try
            With mojCmd
                .Connection = conn
                .CommandType = CommandType.StoredProcedure
                .CommandText = "ZemiSliki"
                .Parameters.Clear()
                .Parameters.Add(New SqlClient.SqlParameter("@Sifra_Zab", Sifra_Zab))
                citac = .ExecuteReader()
            End With

            While citac.Read
                With citac
                    Dim PatekaZacuvaj As String = VarIB.ZemiWRKonfig("PatekaDoSliki")
                    Slika.Ime = citac("Ime").ToString()
                    Slika.Pateka_Slika = citac("Pateka_Slika").ToString()
                    Slika.Sifra_Zab = Sifra_Zab
                    zemiSlikaZaStav(Slika)
                End With
            End While

        Catch ex As Exception
            MsgBox(ex.Message)

        Finally
            conn.Close()
        End Try

        Return Slika

    End Function

    Private Function ImageToBase64(ByVal image As Image, ByVal format As System.Drawing.Imaging.ImageFormat) As String
        Using ms As New MemoryStream
            image.Save(ms, format)
            Dim imageBytes As Byte() = ms.ToArray()

            Dim base64String As String = Convert.ToBase64String(imageBytes)

            Return base64String
        End Using
    End Function

End Class
