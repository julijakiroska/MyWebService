' NOTE: You can use the "Rename" command on the context menu to change the interface name "IService1" in both code and config file together.
Imports System.Collections.Generic

<ServiceContract()>
Public Interface IService1



    ' TODO: Add your service operations here
    <OperationContract()>
    <WebInvoke(BodyStyle:=WebMessageBodyStyle.Bare,
           Method:="GET",
           RequestFormat:=WebMessageFormat.Json,
           ResponseFormat:=WebMessageFormat.Json,
           UriTemplate:="/ProverkaKorisnik?Username={Username}&Password={Password}")>
    Function ProverkaKorisnik(ByVal Username As String, ByVal Password As String) As String

    <OperationContract()>
    <WebInvoke(BodyStyle:=WebMessageBodyStyle.Bare,
           Method:="GET",
           RequestFormat:=WebMessageFormat.Json,
           ResponseFormat:=WebMessageFormat.Json,
           UriTemplate:="/zemiOrgEd")>
    Function zemiOrgEd() As List(Of klasi.OrgEd)

    <OperationContract()>
    <WebInvoke(BodyStyle:=WebMessageBodyStyle.Bare,
           Method:="GET",
           RequestFormat:=WebMessageFormat.Json,
           ResponseFormat:=WebMessageFormat.Json,
           UriTemplate:="/zemiGrupi")>
    Function zemiGrupi() As List(Of klasi.Grupa)

    <OperationContract()>
    <WebInvoke(BodyStyle:=WebMessageBodyStyle.Bare,
           Method:="GET",
           RequestFormat:=WebMessageFormat.Json,
           ResponseFormat:=WebMessageFormat.Json,
           UriTemplate:="/zemiPodgrupi")>
    Function zemiPodgrupi() As List(Of klasi.Podgrupa)

    <OperationContract()>
    <WebInvoke(BodyStyle:=WebMessageBodyStyle.Bare,
           Method:="POST",
           RequestFormat:=WebMessageFormat.Json,
           ResponseFormat:=WebMessageFormat.Json,
           UriTemplate:="/IzvestajPrometMobilen")>
    Function IzvestajPrometMobilen(ByVal baranje As klasi.Baranje) As klasi.Odgovor
   

End Interface

' Use a data contract as illustrated in the sample below to add composite types to service operations.


