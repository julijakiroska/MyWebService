Imports System.Collections.Generic

' NOTE: You can use the "Rename" command on the context menu to change the interface name "IProba" in both code and config file together.
<ServiceContract()>
Public Interface IProba

    Sub DoWork()

    <OperationContract()>
    <WebInvoke(BodyStyle:=WebMessageBodyStyle.Bare,
           Method:="GET",
           RequestFormat:=WebMessageFormat.Json,
           ResponseFormat:=WebMessageFormat.Json,
           UriTemplate:="/zemiOrgEd?Sifra_OE={Sifra_OE}")>
    Function zemiOrgEd(ByVal Sifra_OE As String) As klasi.Podatok

    <OperationContract()>
    <WebInvoke(BodyStyle:=WebMessageBodyStyle.Bare,
          Method:="GET",
          RequestFormat:=WebMessageFormat.Json,
          ResponseFormat:=WebMessageFormat.Json,
          UriTemplate:="/zemiTipDok?Sifra_Dok={Sifra_Dok}")>
    Function zemiTipDok(ByVal Sifra_Dok As String) As klasi.Podatok

    <OperationContract()>
    <WebInvoke(BodyStyle:=WebMessageBodyStyle.Bare,
         Method:="GET",
         RequestFormat:=WebMessageFormat.Json,
         ResponseFormat:=WebMessageFormat.Json,
         UriTemplate:="/zemiBrojDok?Broj_Dok={Broj_Dok}")>
    Function zemiBrojDok(ByVal Broj_Dok As String) As klasi.Podatok

    <OperationContract()>
    <WebInvoke(BodyStyle:=WebMessageBodyStyle.Bare,
         Method:="GET",
         RequestFormat:=WebMessageFormat.Json,
         ResponseFormat:=WebMessageFormat.Json,
         UriTemplate:="/zemiBrojproekt?Broj_proekt={Broj_proekt}")>
    Function zemiBrojproekt(ByVal Broj_proekt As String) As klasi.Podatok

    <OperationContract()>
    <WebInvoke(BodyStyle:=WebMessageBodyStyle.Bare,
         Method:="GET",
         RequestFormat:=WebMessageFormat.Json,
         ResponseFormat:=WebMessageFormat.Json,
         UriTemplate:="/ProverkaNaDokument?Sifra_OE={Sifra_OE}&Sifra_Dok={Sifra_Dok}&Broj_Dok={Broj_Dok}&Broj_proekt={Broj_proekt}")>
    Function ProverkaNaDokument(ByVal Sifra_OE As String, ByVal Sifra_Dok As String, ByVal Broj_Dok As String, ByVal Broj_proekt As String
                                 ) As klasi.ProverkaNaDokument

    <OperationContract()>
    <WebInvoke(BodyStyle:=WebMessageBodyStyle.Bare,
          Method:="POST",
          RequestFormat:=WebMessageFormat.Json,
          ResponseFormat:=WebMessageFormat.Json,
          UriTemplate:="/IdentifBr?Identif_Br={Identif_Br}&Sifra_OE={Sifra_OE}&Sifra_Dok={Sifra_Dok}&Broj_Dok={Broj_Dok}&Broj_proekt={Broj_proekt}")>
    Function VnesiStatusNaNar(ByVal Identif_Br As String, ByVal Sifra_OE As String, ByVal Sifra_Dok As String, ByVal Broj_Dok As String, ByVal Broj_proekt As String) As klasi.Podatok

End Interface

