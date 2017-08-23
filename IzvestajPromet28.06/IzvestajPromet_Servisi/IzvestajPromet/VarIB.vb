Imports System.Data.SqlClient
Imports System.Xml
Imports System.IO
'Imports WcfService1.TekPromenlivi

Public Class VarIB

    Private _cnnSrv As System.Data.SqlClient.SqlConnection
    Private _cnnString As String = ""
    Public MaxCasoviDocni As Integer = 25
    Public DaliEdnoMasiSoSvojOper As Boolean = False
    Public ZapisiStavkiVoLog As String = "D"
    Public fixHappyProc As Decimal = 0.0
    Public DozvStorno_Stavka As String = "D"
    Public Touch_SifOperSiteMasi As String = ""

    Public ReadOnly Property CnSrv As System.Data.SqlClient.SqlConnection
        Get
            If _cnnSrv Is Nothing Then
                _cnnSrv = New SqlConnection(CnString)
            End If

            Try
                If _cnnSrv.State = ConnectionState.Closed Then
                    _cnnSrv.ConnectionString = CnString
                    _cnnSrv.Open()
                End If
            Catch ex As Exception
            End Try
            Return _cnnSrv
        End Get
    End Property
    Public ReadOnly Property CnString As String
        Get
            If _cnnString = "" Then
                _cnnString = "server=" & ZemiWRKonfig("Server") & ";" & _
                            "database=" & ZemiWRKonfig("Baza") & ";" & _
                              "user ID=" & ZemiWRKonfig("User") & ";" & _
                              "password=" & ZemiWRKonfig("Pass") & _
                              "; Connection Timeout=" & ZemiWRKonfig("TimeOut")
            End If
            Return _cnnString
        End Get
    End Property

    Public Sub CloseConnection()


        Try
            If Not (_cnnSrv Is Nothing) AndAlso _cnnSrv.State = ConnectionState.Open Then
                _cnnSrv.Close()
            End If
        Catch eX As Exception
        End Try

    End Sub


    Public Shared Function ZemiWRKonfig(ByVal sPole As String) As String
        'Dim XMLCitac As New XmlTextReader("c:\ib\RestKonfig\Merc_Konfig.xml")
        Dim XMLCitac As XmlTextReader = dajXmlCitac()
        Dim element As String = ""
        While XMLCitac.Read
            XMLCitac.MoveToContent()
            If XMLCitac.NodeType = XmlNodeType.Element And XMLCitac.Name = sPole Then
                element = XMLCitac.ReadInnerXml.Trim
            End If
        End While
        XMLCitac.Close()
        Return element
    End Function
    Private Shared Function dajXmlCitac() As XmlTextReader
        Dim basePath As String = AppDomain.CurrentDomain.BaseDirectory

        Try
            Dim lokalenKonfig As String = Konv.ObjVoStr(System.Configuration.ConfigurationManager.AppSettings.Get("lokalenKonfig"))
            If lokalenKonfig <> "" AndAlso fileExists(lokalenKonfig) Then
                Return New XmlTextReader(lokalenKonfig)
            End If

            Dim globalenKonfig As String = Konv.ObjVoStr(System.Configuration.ConfigurationManager.AppSettings.Get("globalenKonfig"))
            If globalenKonfig <> "" AndAlso fileExists(globalenKonfig) Then
                Return New XmlTextReader(globalenKonfig)

            End If
        Catch ex As Exception

        End Try

        Return Nothing

    End Function
    Public Shared Function fileExists(pateka As String) As Boolean
        Static fso As Object
        fso = CreateObject("Scripting.FileSystemObject")
        fileExists = fso.FileExists(pateka)
        Return fileExists
    End Function
End Class
