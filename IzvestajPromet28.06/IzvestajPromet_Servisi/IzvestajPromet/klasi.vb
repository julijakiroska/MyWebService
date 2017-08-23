Public Class klasi

    Public Class TipDok

        <DataMember(Name:="Sifra_Dok")>
        Public Sifra_Dok As String

    End Class

    Public Class OrgEd

        <DataMember(Name:="Sifra_OE")>
        Public Sifra_OE As String

    End Class

    Public Class Grupa

        <DataMember(Name:="Sifra_Gr")>
        Public Sifra_Gr As String

        <DataMember(Name:="Ime_Gr")>
        Public Ime_Gr As String

    End Class

    Public Class Podgrupa

        <DataMember(Name:="Sifra_Podg")>
        Public Sifra_Podg As String

        <DataMember(Name:="Ime_Podg")>
        Public Ime_Podg As String

    End Class

    Public Class iOperator

        <DataMember(Name:="Sifra_Oe")>
        Public Sifra_Oe As String

        <DataMember(Name:="Ime_Oper")>
        Public Ime_Oper As String

        <DataMember(Name:="Sifra_Oper")>
        Public Sifra_Oper As String

        <DataMember(Name:="ImeNac")>
        Public ImeNac As String

        <DataMember(Name:="Iznos")>
        Public Iznos As String

    End Class

    Public Class Baranje

        <DataMember(Name:="datumOd")>
        Public datumOd As String

        <DataMember(Name:="datumDo")>
        Public datumDo As String

        <DataMember(Name:="SifraOe")>
        Public SifraOe As String

        <DataMember(Name:="SifraOper")>
        Public SifraOper As String

        <DataMember(Name:="SifraPodgrupa")>
        Public SifraPodgrupa As String

        <DataMember(Name:="SifraGrupa")>
        Public SifraGrupa As String

        <DataMember(Name:="SifraNac")>
        Public SifraNac As String

        <DataMember(Name:="Opcija")>
        Public Opcija As String

    End Class

    Public Class iArtikal

        <DataMember(Name:="Sifra_Oe")>
        Public Sifra_Oe As String

        <DataMember(Name:="Sifra_Art")>
        Public Sifra_Art As String

        <DataMember(Name:="ImeArt")>
        Public ImeArt As String

        <DataMember(Name:="Kolicina")>
        Public Kolicina As String

        <DataMember(Name:="Cena")>
        Public Cena As String

        <DataMember(Name:="Iznos")>
        Public Iznos As String

    End Class

    Public Class iSmetka

        <DataMember(Name:="Sifra_Oe")>
        Public Sifra_Oe As String

        <DataMember(Name:="ImeNac")>
        Public ImeNac As String

        <DataMember(Name:="Sifra_Oper")>
        Public Sifra_Oper As String

        <DataMember(Name:="Datum_Evid")>
        Public Datum_Evid As String

        <DataMember(Name:="Iznos")>
        Public Iznos As String

    End Class

    Public Class iNacin

        <DataMember(Name:="Sifra_Oe")>
        Public Sifra_Oe As String

        <DataMember(Name:="Sifra_Nac")>
        Public Sifra_Nac As String

        <DataMember(Name:="ImeNac")>
        Public ImeNac As String

        <DataMember(Name:="Iznos")>
        Public Iznos As String

    End Class

    Public Class Odgovor

        <DataMember(Name:="listaArtikli")>
        Public listaArtikli As List(Of klasi.iArtikal)

        <DataMember(Name:="listaSmetki")>
        Public listaSmetki As List(Of klasi.iSmetka)

        <DataMember(Name:="listaOperatori")>
        Public listaOperatori As List(Of klasi.iOperator)

        <DataMember(Name:="listaNacin")>
        Public listaNacin As List(Of klasi.iNacin)


    End Class

    Public Class ProverkaNaDokument

        <DataMember(Name:="Sifra_OE")>
        Public Sifra_OE As String

        <DataMember(Name:="Sifra_Dok")>
        Public Sifra_Dok As String

        <DataMember(Name:="Broj_Dok")>
        Public Broj_Dok As String

        <DataMember(Name:="Broj_proekt")>
        Public Broj_proekt As String

        <DataMember(Name:="GreskaSifra_OE")>
        Public GreskaSifra_OE As String

        <DataMember(Name:="GreskaSifra_Dok")>
        Public GreskaSifra_Dok As String

        <DataMember(Name:="GreskaBroj_Dok")>
        Public GreskaBroj_Dok As String

        <DataMember(Name:="GreskaBroj_proekt")>
        Public GreskaBroj_proekt As String

    End Class
    
    Public Class Podatok
        Public greska As String

        <DataMember(Name:="SifraZab")>
        Public SifraZab As Integer

        Sub New()
            greska = ""
            SifraZab = 0
        End Sub

    End Class

    Public Class IdentifBr

        <DataMember(Name:="Identif_Br")>
        Public Identif_Br As String

    End Class

    Public Class OperatoriMob

        <DataMember(Name:="Login_Ime")>
        Public Login_Ime As String

        <DataMember(Name:="Lozinka")>
        Public Lozinka As String

    End Class

    Public Class ZabeleskiMob

        <DataMember(Name:="Zabeleska")>
        Public Zabeleska As String

        <DataMember(Name:="Data_Vnes")>
        Public Data_Vnes As String

        <DataMember(Name:="Ime_Oper")>
        Public Ime_Oper As String

        <DataMember(Name:="Sifra_Zab")>
        Public Sifra_Zab As String

    End Class

    Public Class DaliPostoiKorisnik

        <DataMember(Name:="Sifra_Oper")>
        Public Sifra_Oper As String

        <DataMember(Name:="Ime_Oper")>
        Public Ime_Oper As String

        <DataMember(Name:="Sifra_TipOper")>
        Public Sifra_TipOper As String

        Sub New()
            Sifra_Oper = ""
            Ime_Oper = ""
            Sifra_TipOper = ""
        End Sub


    End Class

    <DataContract()>
    Public Class Slika

        <DataMember(Name:="SlikaStr")>
        Public SlikaStr As String

        <DataMember(Name:="Ime")>
        Public Ime As String

        <DataMember(Name:="Pateka_Slika")>
        Public Pateka_Slika As String

        <DataMember(Name:="Sifra_Zab")>
        Public Sifra_Zab As Integer
    End Class

End Class
