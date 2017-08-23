Imports System.Collections.Generic
Imports BlokDokument.klasi

<ServiceContract()>
Public Interface INotes



    Sub DoWork()

    <OperationContract()>
    <WebInvoke(BodyStyle:=WebMessageBodyStyle.Bare,
           Method:="POST",
           RequestFormat:=WebMessageFormat.Json,
           ResponseFormat:=WebMessageFormat.Json,
           UriTemplate:="/ProverkaOperatorMob")>
    Function ProverkaOperatorMob(ByVal operatoriMob As klasi.OperatoriMob) As klasi.DaliPostoiKorisnik

    '

    <OperationContract()>
    <WebInvoke(BodyStyle:=WebMessageBodyStyle.Bare,
       Method:="GET",
    RequestFormat:=WebMessageFormat.Json,
           ResponseFormat:=WebMessageFormat.Json,
           UriTemplate:="/zemiZabeleski?DatumOd={DatumOd}&DatumDo={DatumDo}&Sifra_Oper={Sifra_Oper}")>
    Function zemiZabeleski(DatumOd As String, DatumDo As String, Sifra_Oper As String) As IList(Of ZabeleskiMob)


    <OperationContract()>
    <WebInvoke(BodyStyle:=WebMessageBodyStyle.Bare,
       Method:="GET",
       RequestFormat:=WebMessageFormat.Json,
       ResponseFormat:=WebMessageFormat.Json,
       UriTemplate:="/VnesiZabeleski?Zabeleska={Zabeleska}&Sifra_Oper={Sifra_Oper}&Sifra_Zab={Sifra_Zab}")>
    Function VnesiZabeleski(ByVal Zabeleska As String, ByVal Sifra_Oper As Integer, ByVal Sifra_Zab As Integer) As klasi.Podatok

    <OperationContract()>
    <WebInvoke(BodyStyle:=WebMessageBodyStyle.Bare,
       Method:="POST",
       RequestFormat:=WebMessageFormat.Json,
       ResponseFormat:=WebMessageFormat.Json,
       UriTemplate:="/ZacuvajSlika")>
    Function ZacuvajSlika(ByVal ObjSlika As klasi.Slika) As Boolean


    <OperationContract()>
    <WebInvoke(BodyStyle:=WebMessageBodyStyle.Bare,
       Method:="GET",
       RequestFormat:=WebMessageFormat.Json,
       ResponseFormat:=WebMessageFormat.Json,
       UriTemplate:="/ZemiSliki?Sifra_Zab={Sifra_Zab}")>
    Function BGETZemiSlikiZaPoseta(ByVal Sifra_Zab As Integer) As klasi.Slika


End Interface
