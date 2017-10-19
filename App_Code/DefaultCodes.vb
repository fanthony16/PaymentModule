Imports Microsoft.VisualBasic
Imports System.Linq

Public Class DefaultCodes
    
End Class

Public Class AdhocDocuments

     Dim txtPIN As String
     Dim txtDescription As String
     Dim txtDocPath As String
     Dim txtApplicationCode As String
     Dim dteRecieved As Date
     Dim txtRecievedBy As String
     Dim blnDeactivated As Integer
	Dim dteDeactivated As Date
	Dim dteDOR As Date
	Dim intIsVerified As Integer
	Dim intRetiree As Integer
	Dim intDocTypeID As Integer

	Property IsRetiree As Integer
		Get
			Return intRetiree
		End Get
		Set(ByVal value As Integer)
			intRetiree = value
		End Set
	End Property

	Property RetirementDate As Date
		Get
			Return dteDOR
		End Get
		Set(ByVal value As Date)
			dteDOR = value
		End Set
	End Property

	Property DocumentTypeID As Integer
		Get
			Return intDocTypeID
		End Get
		Set(ByVal value As Integer)
			intDocTypeID = value
		End Set
	End Property


     Property IsVerified As Integer
          Get
               Return intIsVerified
          End Get
          Set(ByVal value As Integer)
               intIsVerified = value
          End Set
     End Property


     Property DeactivatedDate As Date

          Get
               Return dteDeactivated
          End Get
          Set(ByVal value As Date)
               dteDeactivated = value
          End Set

     End Property

     Property IsDeactivated As Integer

          Get
               Return blnDeactivated
          End Get
          Set(ByVal value As Integer)
               blnDeactivated = value
          End Set

     End Property

     Property RecievedBy As String

          Get
               Return txtRecievedBy
          End Get
          Set(ByVal value As String)
               txtRecievedBy = value
          End Set

     End Property

     Property PIN As String

          Get
               Return txtPIN
          End Get
          Set(ByVal value As String)
               txtPIN = value
          End Set

     End Property

     Property Description As String

          Get
               Return txtDescription
          End Get
          Set(ByVal value As String)
               txtDescription = value
          End Set

     End Property

     Property DocPath As String

          Get
               Return txtDocPath
          End Get
          Set(ByVal value As String)
               txtDocPath = value
          End Set

     End Property

     Property ApplicationCode As String

          Get
               Return txtApplicationCode
          End Get
          Set(ByVal value As String)
               txtApplicationCode = value
          End Set

     End Property

     Property RecievedDate As Date

          Get
               Return dteRecieved
          End Get
          Set(ByVal value As Date)
               dteRecieved = value
          End Set

     End Property

End Class

Public Class States

     Dim sCode As String
     Dim sName As String
     Dim sID As Integer

     Property StateID As Integer
          Get
               Return sID
          End Get
          Set(ByVal value As Integer)
               sID = value
          End Set
     End Property

     Property StateCode As String
          Get
               Return sCode
          End Get
          Set(ByVal value As String)
               sCode = value
          End Set
     End Property

     Property StateName As String
          Get
               Return sName
          End Get
          Set(ByVal value As String)
               sName = value
          End Set
     End Property

     Public Function PopulateStates() As List(Of States)

          Dim state As New List(Of States)

          state.Add(New States With {.StateCode = "AB", .StateName = "ABIA", .StateID = "1"})
          state.Add(New States With {.StateCode = "AD", .StateName = "ADAMAWA", .StateID = "2"})
          state.Add(New States With {.StateCode = "AK", .StateName = "AKWA-IBOM", .StateID = "3"})
          state.Add(New States With {.StateCode = "AN", .StateName = "ANAMBRA", .StateID = "4"})
          state.Add(New States With {.StateCode = "BA", .StateName = "BAUCHI", .StateID = "5"})
          state.Add(New States With {.StateCode = "BY", .StateName = "BAYELSA", .StateID = "6"})
          state.Add(New States With {.StateCode = "BE", .StateName = "BENUE", .StateID = "7"})
          state.Add(New States With {.StateCode = "BO", .StateName = "BORNO", .StateID = "8"})
          state.Add(New States With {.StateCode = "CR", .StateName = "CROSS RIVER", .StateID = "9"})
          state.Add(New States With {.StateCode = "DT", .StateName = "DELTA", .StateID = "10"})
          state.Add(New States With {.StateCode = "EB", .StateName = "EBONYI", .StateID = "11"})
          state.Add(New States With {.StateCode = "ED", .StateName = "EDO", .StateID = "12"})
          state.Add(New States With {.StateCode = "EK", .StateName = "EKITI", .StateID = "13"})
          state.Add(New States With {.StateCode = "EN", .StateName = "ENUGU", .StateID = "14"})
          state.Add(New States With {.StateCode = "FC", .StateName = "FCT", .StateID = "15"})
          state.Add(New States With {.StateCode = "GB", .StateName = "GOMBE", .StateID = "16"})
          state.Add(New States With {.StateCode = "IM", .StateName = "IMO", .StateID = "17"})
          state.Add(New States With {.StateCode = "JG", .StateName = "JIGAWA", .StateID = "18"})
          state.Add(New States With {.StateCode = "KD", .StateName = "KADUNA", .StateID = "19"})
          state.Add(New States With {.StateCode = "KN", .StateName = "KANO", .StateID = "20"})
          state.Add(New States With {.StateCode = "KT", .StateName = "KATSINA", .StateID = "21"})
          state.Add(New States With {.StateCode = "KB", .StateName = "KEBBI", .StateID = "22"})
          state.Add(New States With {.StateCode = "KG", .StateName = "KOGI", .StateID = "23"})
          state.Add(New States With {.StateCode = "KW", .StateName = "KWARA", .StateID = "24"})
          state.Add(New States With {.StateCode = "LA", .StateName = "LAGOS", .StateID = "25"})
          state.Add(New States With {.StateCode = "NR", .StateName = "NASSARAWA", .StateID = "26"})
          state.Add(New States With {.StateCode = "NG", .StateName = "NIGER", .StateID = "27"})
          state.Add(New States With {.StateCode = "OG", .StateName = "OGUN", .StateID = "28"})
          state.Add(New States With {.StateCode = "OD", .StateName = "ONDO", .StateID = "29"})
          state.Add(New States With {.StateCode = "OS", .StateName = "OSUN", .StateID = "30"})
          state.Add(New States With {.StateCode = "OY", .StateName = "OYO", .StateID = "31"})
          state.Add(New States With {.StateCode = "PL", .StateName = "PLATEAU", .StateID = "32"})
          state.Add(New States With {.StateCode = "RV", .StateName = "RIVERS", .StateID = "33"})
          state.Add(New States With {.StateCode = "SO", .StateName = "SOKOTO", .StateID = "34"})
          state.Add(New States With {.StateCode = "TB", .StateName = "TARABA", .StateID = "35"})
          state.Add(New States With {.StateCode = "YB", .StateName = "YOBE", .StateID = "36"})
          state.Add(New States With {.StateCode = "ZA", .StateName = "ZAMFARA", .StateID = "37"})

          Return state

     End Function

     Public Function getStateID(stateName As String) As String


          Dim mstate = PopulateStates()
          Dim querys = From m In mstate _
                       Where m.StateName = stateName _
                       Select m.StateID
          Return querys(0).ToString

     End Function

     Public Function getStateName(stateID As Integer) As String

          Dim mstate = PopulateStates()
          Dim querys = From m In mstate _
                       Where m.StateID = stateID _
                       Select m.StateName
          If (querys.Count > 0) Then
               Return querys(0).ToString
          Else
               Return Nothing
          End If

     End Function

     Public Function getStates() As List(Of String)

          Dim lstStates As New List(Of String)
          Dim mstate = PopulateStates()

          Dim query = From m In mstate _
                      Select m

          For Each a As States In query
               lstStates.Add(a.StateName)
          Next
          Return lstStates

     End Function

End Class

Public Class LGA
     Dim lID As Integer
     Dim lCode As String
     Dim lName As String
     Dim sCode As String
     Property LGAID As Integer

          Get
               Return lID
          End Get
          Set(ByVal value As Integer)
               lID = value
          End Set

     End Property

     Property LGACode As String
          Get
               Return lCode
          End Get
          Set(ByVal value As String)
               lCode = value
          End Set
     End Property

     Property LGAName As String
          Get
               Return lName
          End Get
          Set(ByVal value As String)
               lName = value
          End Set
     End Property

     Property StateCode As String
          Get
               Return sCode
          End Get
          Set(ByVal value As String)
               sCode = value
          End Set
     End Property


     Public Function populateLGA() As List(Of LGA)


          Dim LGA As New List(Of LGA)

          'LGA.Add(New LGA With {.LGAID = "756", .LGAName = "NGURU", .LGACode = "NGU", .StateCode = "36"})
		LGA.Add(New LGA With {.LGAID = "0", .LGAName = "", .LGACode = "", .StateCode = "1"})
          LGA.Add(New LGA With {.LGAID = "1", .LGAName = "ABA NORTH", .LGACode = "EZA", .StateCode = "1"})
          LGA.Add(New LGA With {.LGAID = "2", .LGAName = "ABA SOUTH", .LGACode = "ABA", .StateCode = "1"})
          LGA.Add(New LGA With {.LGAID = "3", .LGAName = "AROCHUKWU", .LGACode = "ACH", .StateCode = "1"})
          LGA.Add(New LGA With {.LGAID = "4", .LGAName = "BENDE", .LGACode = "BND", .StateCode = "1"})
          LGA.Add(New LGA With {.LGAID = "5", .LGAName = "IKWUANO", .LGACode = "KWU", .StateCode = "1"})
          LGA.Add(New LGA With {.LGAID = "6", .LGAName = "ISIALA-NGWA NORTH", .LGACode = "KPU", .StateCode = "1"})
          LGA.Add(New LGA With {.LGAID = "7", .LGAName = "ISIALA-NGWA SOUTH", .LGACode = "MBA", .StateCode = "1"})
          LGA.Add(New LGA With {.LGAID = "8", .LGAName = "ISUIKWUATO", .LGACode = "MBL", .StateCode = "1"})
          LGA.Add(New LGA With {.LGAID = "9", .LGAName = "OBIOMA NGWA", .LGACode = "NGK", .StateCode = "1"})
          LGA.Add(New LGA With {.LGAID = "10", .LGAName = "OHAFIA", .LGACode = "HAF", .StateCode = "1"})
          LGA.Add(New LGA With {.LGAID = "11", .LGAName = "OSISIOMA", .LGACode = "SSM", .StateCode = "1"})
          LGA.Add(New LGA With {.LGAID = "12", .LGAName = "UGWUNAGBO", .LGACode = "GWB", .StateCode = "1"})
          LGA.Add(New LGA With {.LGAID = "13", .LGAName = "UKWA EAST", .LGACode = "KEK", .StateCode = "1"})
          LGA.Add(New LGA With {.LGAID = "14", .LGAName = "UKWA WEST", .LGACode = "KKE", .StateCode = "1"})
          LGA.Add(New LGA With {.LGAID = "15", .LGAName = "UMUAHIA NORTH", .LGACode = "UMA", .StateCode = "1"})
          LGA.Add(New LGA With {.LGAID = "16", .LGAName = "UMUAHIA SOUTH", .LGACode = "APR", .StateCode = "1"})
          LGA.Add(New LGA With {.LGAID = "17", .LGAName = "UMUNNEOCHI", .LGACode = "UNC", .StateCode = "1"})
          LGA.Add(New LGA With {.LGAID = "18", .LGAName = "DEMSA", .LGACode = "DSA", .StateCode = "2"})
          LGA.Add(New LGA With {.LGAID = "19", .LGAName = "FUFORE", .LGACode = "FUR", .StateCode = "2"})
          LGA.Add(New LGA With {.LGAID = "20", .LGAName = "GANYE", .LGACode = "GAN", .StateCode = "2"})
          LGA.Add(New LGA With {.LGAID = "21", .LGAName = "GIREI", .LGACode = "GRE", .StateCode = "2"})
          LGA.Add(New LGA With {.LGAID = "22", .LGAName = "GOMBI", .LGACode = "GMB", .StateCode = "2"})
          LGA.Add(New LGA With {.LGAID = "23", .LGAName = "GUYUK", .LGACode = "GUY", .StateCode = "2"})
          LGA.Add(New LGA With {.LGAID = "24", .LGAName = "HONG", .LGACode = "HNG", .StateCode = "2"})
          LGA.Add(New LGA With {.LGAID = "25", .LGAName = "JADA", .LGACode = "JAD", .StateCode = "2"})
          LGA.Add(New LGA With {.LGAID = "26", .LGAName = "JIMETA", .LGACode = "JMT", .StateCode = "2"})
          LGA.Add(New LGA With {.LGAID = "27", .LGAName = "LAMURDE", .LGACode = "LMR", .StateCode = "2"})
          LGA.Add(New LGA With {.LGAID = "28", .LGAName = "MADAGALI", .LGACode = "MDG", .StateCode = "2"})
          LGA.Add(New LGA With {.LGAID = "29", .LGAName = "MAIHA", .LGACode = "MAH", .StateCode = "2"})
          LGA.Add(New LGA With {.LGAID = "30", .LGAName = "MAYO-BELWA", .LGACode = "MWA", .StateCode = "2"})
          LGA.Add(New LGA With {.LGAID = "31", .LGAName = "MICHIKA", .LGACode = "MCH", .StateCode = "2"})
          LGA.Add(New LGA With {.LGAID = "32", .LGAName = "MUBI-NORTH", .LGACode = "MUB", .StateCode = "2"})
          LGA.Add(New LGA With {.LGAID = "33", .LGAName = "MUBI-SOUTH", .LGACode = "GYL", .StateCode = "2"})
          LGA.Add(New LGA With {.LGAID = "34", .LGAName = "NUMAN", .LGACode = "NUM", .StateCode = "2"})
          LGA.Add(New LGA With {.LGAID = "35", .LGAName = "SHELLENG", .LGACode = "SHG", .StateCode = "2"})
          LGA.Add(New LGA With {.LGAID = "36", .LGAName = "SONG", .LGACode = "SNG", .StateCode = "2"})
          LGA.Add(New LGA With {.LGAID = "37", .LGAName = "TOUNGO", .LGACode = "TNG", .StateCode = "2"})
          LGA.Add(New LGA With {.LGAID = "38", .LGAName = "YOLA", .LGACode = "YLA", .StateCode = "2"})
          LGA.Add(New LGA With {.LGAID = "39", .LGAName = "ABAK", .LGACode = "ABK", .StateCode = "3"})
          LGA.Add(New LGA With {.LGAID = "40", .LGAName = "EASTERN-OBOLO", .LGACode = "KRT", .StateCode = "3"})
          LGA.Add(New LGA With {.LGAID = "41", .LGAName = "EKET", .LGACode = "KET", .StateCode = "3"})
          LGA.Add(New LGA With {.LGAID = "42", .LGAName = "ESIT EKET", .LGACode = "KST", .StateCode = "3"})
          LGA.Add(New LGA With {.LGAID = "43", .LGAName = "ESSIEN-UDIM", .LGACode = "AFH", .StateCode = "3"})
          LGA.Add(New LGA With {.LGAID = "44", .LGAName = "ETIM-EKPO", .LGACode = "AEE", .StateCode = "3"})
          LGA.Add(New LGA With {.LGAID = "45", .LGAName = "ETINAM", .LGACode = "ETN", .StateCode = "3"})
          LGA.Add(New LGA With {.LGAID = "46", .LGAName = "IBENO", .LGACode = "PNG", .StateCode = "3"})
          LGA.Add(New LGA With {.LGAID = "47", .LGAName = "IBESIKPO-ASUTAN", .LGACode = "NGD", .StateCode = "3"})
          LGA.Add(New LGA With {.LGAID = "48", .LGAName = "IBIONO-IBOM", .LGACode = "BMT", .StateCode = "3"})
          LGA.Add(New LGA With {.LGAID = "49", .LGAName = "IKA", .LGACode = "NYA", .StateCode = "3"})
          LGA.Add(New LGA With {.LGAID = "50", .LGAName = "IKONO", .LGACode = "KKN", .StateCode = "3"})
          LGA.Add(New LGA With {.LGAID = "51", .LGAName = "IKOT-ABASI", .LGACode = "KTS", .StateCode = "3"})
          LGA.Add(New LGA With {.LGAID = "52", .LGAName = "IKOT-EKPENE", .LGACode = "KTE", .StateCode = "3"})
          LGA.Add(New LGA With {.LGAID = "53", .LGAName = "INI", .LGACode = "DRK", .StateCode = "3"})
          LGA.Add(New LGA With {.LGAID = "54", .LGAName = "ITU", .LGACode = "TTU", .StateCode = "3"})
          LGA.Add(New LGA With {.LGAID = "55", .LGAName = "MBO", .LGACode = "ENW", .StateCode = "3"})
          LGA.Add(New LGA With {.LGAID = "56", .LGAName = "MKPAT-ENIN", .LGACode = "MKP", .StateCode = "3"})
          LGA.Add(New LGA With {.LGAID = "57", .LGAName = "NSIT-ATAI", .LGACode = "TAI", .StateCode = "3"})
          LGA.Add(New LGA With {.LGAID = "58", .LGAName = "NSIT-IBOM", .LGACode = "AFG", .StateCode = "3"})
          LGA.Add(New LGA With {.LGAID = "59", .LGAName = "NSIT-UBIUM", .LGACode = "KTD", .StateCode = "3"})
          LGA.Add(New LGA With {.LGAID = "60", .LGAName = "OBOT-AKARA", .LGACode = "NTE", .StateCode = "3"})
          LGA.Add(New LGA With {.LGAID = "61", .LGAName = "OKOBO", .LGACode = "KPD", .StateCode = "3"})
          LGA.Add(New LGA With {.LGAID = "62", .LGAName = "ONNA", .LGACode = "ABT", .StateCode = "3"})
          LGA.Add(New LGA With {.LGAID = "63", .LGAName = "ORON", .LGACode = "RNN", .StateCode = "3"})
          LGA.Add(New LGA With {.LGAID = "64", .LGAName = "ORUK ANAM", .LGACode = "KTM", .StateCode = "3"})
          LGA.Add(New LGA With {.LGAID = "65", .LGAName = "UDUNG-UKO", .LGACode = "EYF", .StateCode = "3"})
          LGA.Add(New LGA With {.LGAID = "66", .LGAName = "UKANEFUN", .LGACode = "KPK", .StateCode = "3"})
          LGA.Add(New LGA With {.LGAID = "67", .LGAName = "URU OFFONG ORUKO", .LGACode = "UFG", .StateCode = "3"})
          LGA.Add(New LGA With {.LGAID = "68", .LGAName = "URUAN", .LGACode = "DUU", .StateCode = "3"})
          LGA.Add(New LGA With {.LGAID = "69", .LGAName = "UYO", .LGACode = "UYY", .StateCode = "3"})
          LGA.Add(New LGA With {.LGAID = "70", .LGAName = "AGUATA", .LGACode = "AGU", .StateCode = "4"})
          LGA.Add(New LGA With {.LGAID = "71", .LGAName = "ANAMBRA", .LGACode = "AAH", .StateCode = "4"})
          LGA.Add(New LGA With {.LGAID = "72", .LGAName = "ANAMBRA-WEST", .LGACode = "NZM", .StateCode = "4"})
          LGA.Add(New LGA With {.LGAID = "73", .LGAName = "ANAOCHA", .LGACode = "NEN", .StateCode = "4"})
          LGA.Add(New LGA With {.LGAID = "74", .LGAName = "AWKA-NORTH", .LGACode = "ACA", .StateCode = "4"})
          LGA.Add(New LGA With {.LGAID = "75", .LGAName = "AWKA-SOUTH", .LGACode = "AWK", .StateCode = "4"})
          LGA.Add(New LGA With {.LGAID = "76", .LGAName = "AYAMELUM", .LGACode = "NKK", .StateCode = "4"})
          LGA.Add(New LGA With {.LGAID = "77", .LGAName = "DUNUKOFIA", .LGACode = "KPP", .StateCode = "4"})
          LGA.Add(New LGA With {.LGAID = "78", .LGAName = "EKWUSIGO", .LGACode = "ZBL", .StateCode = "4"})
          LGA.Add(New LGA With {.LGAID = "79", .LGAName = "IDEMILI-NORTH", .LGACode = "GDD", .StateCode = "4"})
          LGA.Add(New LGA With {.LGAID = "80", .LGAName = "IDEMILI-SOUTH", .LGACode = "JJT", .StateCode = "4"})
          LGA.Add(New LGA With {.LGAID = "81", .LGAName = "IHIALA", .LGACode = "HAL", .StateCode = "4"})
          LGA.Add(New LGA With {.LGAID = "82", .LGAName = "NJIKOKA", .LGACode = "ABN", .StateCode = "4"})
          LGA.Add(New LGA With {.LGAID = "83", .LGAName = "NNEWI-NORTH", .LGACode = "NNE", .StateCode = "4"})
          LGA.Add(New LGA With {.LGAID = "84", .LGAName = "NNEWI-SOUTH", .LGACode = "UKP", .StateCode = "4"})
          LGA.Add(New LGA With {.LGAID = "85", .LGAName = "OGBARU", .LGACode = "ATN", .StateCode = "4"})
          LGA.Add(New LGA With {.LGAID = "86", .LGAName = "ONITSHA-NORTH", .LGACode = "NSH", .StateCode = "4"})
          LGA.Add(New LGA With {.LGAID = "87", .LGAName = "ONITSHA-SOUTH", .LGACode = "FGG", .StateCode = "4"})
          LGA.Add(New LGA With {.LGAID = "88", .LGAName = "ORUMBA-NORTH", .LGACode = "AJL", .StateCode = "4"})
          LGA.Add(New LGA With {.LGAID = "89", .LGAName = "ORUMBA-SOUTH", .LGACode = "UMZ", .StateCode = "4"})
          LGA.Add(New LGA With {.LGAID = "90", .LGAName = "OYI", .LGACode = "HTE", .StateCode = "4"})
          LGA.Add(New LGA With {.LGAID = "91", .LGAName = "ALKALERI", .LGACode = "ALK", .StateCode = "5"})
          LGA.Add(New LGA With {.LGAID = "92", .LGAName = "BAUCHI", .LGACode = "BAU", .StateCode = "5"})
          LGA.Add(New LGA With {.LGAID = "93", .LGAName = "BOGORO", .LGACode = "BGR", .StateCode = "5"})
          LGA.Add(New LGA With {.LGAID = "94", .LGAName = "DAMBAN", .LGACode = "DBM", .StateCode = "5"})
          LGA.Add(New LGA With {.LGAID = "95", .LGAName = "DARAZO", .LGACode = "DRZ", .StateCode = "5"})
          LGA.Add(New LGA With {.LGAID = "96", .LGAName = "DASS", .LGACode = "DAS", .StateCode = "5"})
          LGA.Add(New LGA With {.LGAID = "97", .LGAName = "GAMAWA", .LGACode = "GAM", .StateCode = "5"})
          LGA.Add(New LGA With {.LGAID = "98", .LGAName = "GANJUWA", .LGACode = "GJW", .StateCode = "5"})
          LGA.Add(New LGA With {.LGAID = "99", .LGAName = "GIADE", .LGACode = "GYD", .StateCode = "5"})
          LGA.Add(New LGA With {.LGAID = "100", .LGAName = "ITAS/GADAU", .LGACode = "TSG", .StateCode = "5"})
          LGA.Add(New LGA With {.LGAID = "101", .LGAName = "JAMA ARE", .LGACode = "JMA", .StateCode = "5"})
          LGA.Add(New LGA With {.LGAID = "102", .LGAName = "KATAGUM", .LGACode = "KTG", .StateCode = "5"})
          LGA.Add(New LGA With {.LGAID = "103", .LGAName = "KIRFI", .LGACode = "KRF", .StateCode = "5"})
          LGA.Add(New LGA With {.LGAID = "104", .LGAName = "MISAU", .LGACode = "MSA", .StateCode = "5"})
          LGA.Add(New LGA With {.LGAID = "105", .LGAName = "NINGI", .LGACode = "NNG", .StateCode = "5"})
          LGA.Add(New LGA With {.LGAID = "106", .LGAName = "SHIRA", .LGACode = "SHR", .StateCode = "5"})
          LGA.Add(New LGA With {.LGAID = "107", .LGAName = "TAFAWA-BALEWA", .LGACode = "TFB", .StateCode = "5"})
          LGA.Add(New LGA With {.LGAID = "108", .LGAName = "TORO", .LGACode = "TRR", .StateCode = "5"})
          LGA.Add(New LGA With {.LGAID = "109", .LGAName = "WARJI", .LGACode = "WRJ", .StateCode = "5"})
          LGA.Add(New LGA With {.LGAID = "110", .LGAName = "ZAKI", .LGACode = "ZAK", .StateCode = "5"})
          LGA.Add(New LGA With {.LGAID = "111", .LGAName = "BRASS", .LGACode = "BRS", .StateCode = "6"})
          LGA.Add(New LGA With {.LGAID = "112", .LGAName = "EKEREMOR", .LGACode = "KMR", .StateCode = "6"})
          LGA.Add(New LGA With {.LGAID = "113", .LGAName = "KOLOKUMA/OPKUMA", .LGACode = "KMK", .StateCode = "6"})
          LGA.Add(New LGA With {.LGAID = "114", .LGAName = "NEMBE", .LGACode = "NEM", .StateCode = "6"})
          LGA.Add(New LGA With {.LGAID = "115", .LGAName = "OGBIA", .LGACode = "GBB", .StateCode = "6"})
          LGA.Add(New LGA With {.LGAID = "116", .LGAName = "SAGBAMA", .LGACode = "SAG", .StateCode = "6"})
          LGA.Add(New LGA With {.LGAID = "117", .LGAName = "SOUTHERN-IJAW", .LGACode = "SPR", .StateCode = "6"})
          LGA.Add(New LGA With {.LGAID = "118", .LGAName = "YENEGOA", .LGACode = "YEN", .StateCode = "6"})
          LGA.Add(New LGA With {.LGAID = "119", .LGAName = "ADO", .LGACode = "GMU", .StateCode = "7"})
          LGA.Add(New LGA With {.LGAID = "120", .LGAName = "AGATU", .LGACode = "GTU", .StateCode = "7"})
          LGA.Add(New LGA With {.LGAID = "121", .LGAName = "APA", .LGACode = "GKP", .StateCode = "7"})
          LGA.Add(New LGA With {.LGAID = "122", .LGAName = "BURUKU", .LGACode = "BKB", .StateCode = "7"})
          LGA.Add(New LGA With {.LGAID = "123", .LGAName = "GBOKO", .LGACode = "GBK", .StateCode = "7"})
          LGA.Add(New LGA With {.LGAID = "124", .LGAName = "GUMA", .LGACode = "YGJ", .StateCode = "7"})
          LGA.Add(New LGA With {.LGAID = "125", .LGAName = "GWER-EAST", .LGACode = "ALD", .StateCode = "7"})
          LGA.Add(New LGA With {.LGAID = "126", .LGAName = "GWER-WEST", .LGACode = "NAK", .StateCode = "7"})
          LGA.Add(New LGA With {.LGAID = "127", .LGAName = "KATSINA-ALA", .LGACode = "KAL", .StateCode = "7"})
          LGA.Add(New LGA With {.LGAID = "128", .LGAName = "KONSHISHA", .LGACode = "TSE", .StateCode = "7"})
          LGA.Add(New LGA With {.LGAID = "129", .LGAName = "KWANDE", .LGACode = "WDP", .StateCode = "7"})
          LGA.Add(New LGA With {.LGAID = "130", .LGAName = "LOGO", .LGACode = "GBG", .StateCode = "7"})
          LGA.Add(New LGA With {.LGAID = "131", .LGAName = "MAKURDI", .LGACode = "MKD", .StateCode = "7"})
          LGA.Add(New LGA With {.LGAID = "132", .LGAName = "OBI", .LGACode = "BRT", .StateCode = "7"})
          LGA.Add(New LGA With {.LGAID = "133", .LGAName = "OGBADIBO", .LGACode = "BGT", .StateCode = "7"})
          LGA.Add(New LGA With {.LGAID = "134", .LGAName = "OHIMINI", .LGACode = "DKP", .StateCode = "7"})
          LGA.Add(New LGA With {.LGAID = "135", .LGAName = "OJU", .LGACode = "JUX", .StateCode = "7"})
          LGA.Add(New LGA With {.LGAID = "136", .LGAName = "OKPOKWU", .LGACode = "PKG", .StateCode = "7"})
          LGA.Add(New LGA With {.LGAID = "137", .LGAName = "OTUKPO", .LGACode = "TKP", .StateCode = "7"})
          LGA.Add(New LGA With {.LGAID = "138", .LGAName = "TARKA", .LGACode = "WNN", .StateCode = "7"})
          LGA.Add(New LGA With {.LGAID = "139", .LGAName = "UKUM", .LGACode = "UKM", .StateCode = "7"})
          LGA.Add(New LGA With {.LGAID = "140", .LGAName = "USHONGO", .LGACode = "SEL", .StateCode = "7"})
          LGA.Add(New LGA With {.LGAID = "141", .LGAName = "VANDEIKYA", .LGACode = "VDY", .StateCode = "7"})
          LGA.Add(New LGA With {.LGAID = "142", .LGAName = "ABADAM", .LGACode = "ADM", .StateCode = "8"})
          LGA.Add(New LGA With {.LGAID = "143", .LGAName = "ASKIRA-UBA", .LGACode = "ASU", .StateCode = "8"})
          LGA.Add(New LGA With {.LGAID = "144", .LGAName = "BAMA", .LGACode = "BAM", .StateCode = "8"})
          LGA.Add(New LGA With {.LGAID = "145", .LGAName = "BAYO", .LGACode = "BAY", .StateCode = "8"})
          LGA.Add(New LGA With {.LGAID = "146", .LGAName = "BIU", .LGACode = "BBU", .StateCode = "8"})
          LGA.Add(New LGA With {.LGAID = "147", .LGAName = "CHIBOK", .LGACode = "CBK", .StateCode = "8"})
          LGA.Add(New LGA With {.LGAID = "148", .LGAName = "DAMBOA", .LGACode = "DAM", .StateCode = "8"})
          LGA.Add(New LGA With {.LGAID = "149", .LGAName = "DIKWA", .LGACode = "DKW", .StateCode = "8"})
          LGA.Add(New LGA With {.LGAID = "150", .LGAName = "GUBIO", .LGACode = "GUB", .StateCode = "8"})
          LGA.Add(New LGA With {.LGAID = "151", .LGAName = "GUZAMALA", .LGACode = "GZM", .StateCode = "8"})
          LGA.Add(New LGA With {.LGAID = "152", .LGAName = "GWOZA", .LGACode = "GZA", .StateCode = "8"})
          LGA.Add(New LGA With {.LGAID = "153", .LGAName = "HAWUL", .LGACode = "HWL", .StateCode = "8"})
          LGA.Add(New LGA With {.LGAID = "154", .LGAName = "JERE", .LGACode = "JRE", .StateCode = "8"})
          LGA.Add(New LGA With {.LGAID = "155", .LGAName = "KAGA", .LGACode = "KGG", .StateCode = "8"})
          LGA.Add(New LGA With {.LGAID = "156", .LGAName = "KALA/BALGE", .LGACode = "KBG", .StateCode = "8"})
          LGA.Add(New LGA With {.LGAID = "157", .LGAName = "KONDUGA", .LGACode = "KDG", .StateCode = "8"})
          LGA.Add(New LGA With {.LGAID = "158", .LGAName = "KUKAWA", .LGACode = "KWA", .StateCode = "8"})
          LGA.Add(New LGA With {.LGAID = "159", .LGAName = "KWAYA-KUSAR", .LGACode = "KWY", .StateCode = "8"})
          LGA.Add(New LGA With {.LGAID = "160", .LGAName = "MAFA", .LGACode = "MAF", .StateCode = "8"})
          LGA.Add(New LGA With {.LGAID = "161", .LGAName = "MAGUMERI", .LGACode = "MGM", .StateCode = "8"})
          LGA.Add(New LGA With {.LGAID = "162", .LGAName = "MAIDUGURI", .LGACode = "MAG", .StateCode = "8"})
          LGA.Add(New LGA With {.LGAID = "163", .LGAName = "MARTE", .LGACode = "MAR", .StateCode = "8"})
          LGA.Add(New LGA With {.LGAID = "164", .LGAName = "MOBBAR", .LGACode = "MBR", .StateCode = "8"})
          LGA.Add(New LGA With {.LGAID = "165", .LGAName = "MONGUNU", .LGACode = "MNG", .StateCode = "8"})
          LGA.Add(New LGA With {.LGAID = "166", .LGAName = "NGALA", .LGACode = "NGL", .StateCode = "8"})
          LGA.Add(New LGA With {.LGAID = "167", .LGAName = "NGANZAI", .LGACode = "NGZ", .StateCode = "8"})
          LGA.Add(New LGA With {.LGAID = "168", .LGAName = "SHANI", .LGACode = "SHN", .StateCode = "8"})
          LGA.Add(New LGA With {.LGAID = "169", .LGAName = "ABI", .LGACode = "TGD", .StateCode = "9"})
          LGA.Add(New LGA With {.LGAID = "170", .LGAName = "AKAMKPA", .LGACode = "KAM", .StateCode = "9"})
          LGA.Add(New LGA With {.LGAID = "171", .LGAName = "AKPABUYO", .LGACode = "KTA", .StateCode = "9"})
          LGA.Add(New LGA With {.LGAID = "172", .LGAName = "BAKASSI", .LGACode = "BKS", .StateCode = "9"})
          LGA.Add(New LGA With {.LGAID = "173", .LGAName = "BEKWARA", .LGACode = "ABE", .StateCode = "9"})
          LGA.Add(New LGA With {.LGAID = "174", .LGAName = "BIASI", .LGACode = "AKP", .StateCode = "9"})
          LGA.Add(New LGA With {.LGAID = "175", .LGAName = "BOKI", .LGACode = "BJE", .StateCode = "9"})
          LGA.Add(New LGA With {.LGAID = "176", .LGAName = "CALABAR-MUNICIPAL", .LGACode = "CAL", .StateCode = "9"})
          LGA.Add(New LGA With {.LGAID = "177", .LGAName = "CALABAR-SOUTH", .LGACode = "ANA", .StateCode = "9"})
          LGA.Add(New LGA With {.LGAID = "178", .LGAName = "ETUNK", .LGACode = "EFE", .StateCode = "9"})
          LGA.Add(New LGA With {.LGAID = "179", .LGAName = "IKOM", .LGACode = "KMM", .StateCode = "9"})
          LGA.Add(New LGA With {.LGAID = "180", .LGAName = "OBANLIKU", .LGACode = "BNS", .StateCode = "9"})
          LGA.Add(New LGA With {.LGAID = "181", .LGAName = "OBUBRA", .LGACode = "BRA", .StateCode = "9"})
          LGA.Add(New LGA With {.LGAID = "182", .LGAName = "OBUDU", .LGACode = "UDU", .StateCode = "9"})
          LGA.Add(New LGA With {.LGAID = "183", .LGAName = "ODUKPANI", .LGACode = "DUK", .StateCode = "9"})
          LGA.Add(New LGA With {.LGAID = "184", .LGAName = "OGOJA", .LGACode = "GGJ", .StateCode = "9"})
          LGA.Add(New LGA With {.LGAID = "185", .LGAName = "YAKURR", .LGACode = "GEP", .StateCode = "9"})
          LGA.Add(New LGA With {.LGAID = "186", .LGAName = "YALA", .LGACode = "CKK", .StateCode = "9"})
          LGA.Add(New LGA With {.LGAID = "187", .LGAName = "ANIOCHA-NORTH", .LGACode = "SLK", .StateCode = "10"})
          LGA.Add(New LGA With {.LGAID = "188", .LGAName = "ANIOCHA-SOUTH", .LGACode = "GWK", .StateCode = "10"})
          LGA.Add(New LGA With {.LGAID = "189", .LGAName = "BOMADI", .LGACode = "BMA", .StateCode = "10"})
          LGA.Add(New LGA With {.LGAID = "190", .LGAName = "BURUTU", .LGACode = "BUR", .StateCode = "10"})
          LGA.Add(New LGA With {.LGAID = "191", .LGAName = "ETHIOPE-EAST", .LGACode = "SKL", .StateCode = "10"})
          LGA.Add(New LGA With {.LGAID = "192", .LGAName = "ETHIOPE-WEST", .LGACode = "GRA", .StateCode = "10"})
          LGA.Add(New LGA With {.LGAID = "193", .LGAName = "IKA-NORTH", .LGACode = "AGB", .StateCode = "10"})
          LGA.Add(New LGA With {.LGAID = "194", .LGAName = "IKA-SOUTH", .LGACode = "AYB", .StateCode = "10"})
          LGA.Add(New LGA With {.LGAID = "195", .LGAName = "ISOKO-NORTH", .LGACode = "DSZ", .StateCode = "10"})
          LGA.Add(New LGA With {.LGAID = "196", .LGAName = "ISOKO-SOUTH", .LGACode = "LEH", .StateCode = "10"})
          LGA.Add(New LGA With {.LGAID = "197", .LGAName = "NDOKWA-EAST", .LGACode = "ABH", .StateCode = "10"})
          LGA.Add(New LGA With {.LGAID = "198", .LGAName = "NDOKWA-WEST", .LGACode = "KWC", .StateCode = "10"})
          LGA.Add(New LGA With {.LGAID = "199", .LGAName = "OKPE", .LGACode = "KPE", .StateCode = "10"})
          LGA.Add(New LGA With {.LGAID = "200", .LGAName = "OSHIMILI", .LGACode = "ASB", .StateCode = "10"})
          LGA.Add(New LGA With {.LGAID = "201", .LGAName = "OSHIMILI-NORTH", .LGACode = "AKU", .StateCode = "10"})
          LGA.Add(New LGA With {.LGAID = "202", .LGAName = "PATANI", .LGACode = "PTN", .StateCode = "10"})
          LGA.Add(New LGA With {.LGAID = "203", .LGAName = "SAPELE", .LGACode = "SAP", .StateCode = "10"})
          LGA.Add(New LGA With {.LGAID = "204", .LGAName = "UDU", .LGACode = "ALA", .StateCode = "10"})
          LGA.Add(New LGA With {.LGAID = "205", .LGAName = "UGHELLI-NORTH", .LGACode = "UGH", .StateCode = "10"})
          LGA.Add(New LGA With {.LGAID = "206", .LGAName = "UGHELLI-SOUTH", .LGACode = "JRT", .StateCode = "10"})
          LGA.Add(New LGA With {.LGAID = "207", .LGAName = "UKWUANI", .LGACode = "BKW", .StateCode = "10"})
          LGA.Add(New LGA With {.LGAID = "208", .LGAName = "UVWIE", .LGACode = "EFR", .StateCode = "10"})
          LGA.Add(New LGA With {.LGAID = "209", .LGAName = "WARRI-CENTRAL", .LGACode = "GBJ", .StateCode = "10"})
          LGA.Add(New LGA With {.LGAID = "210", .LGAName = "WARRI-NORTH", .LGACode = "KLK", .StateCode = "10"})
          LGA.Add(New LGA With {.LGAID = "211", .LGAName = "WARRI-SOUTH", .LGACode = "WWR", .StateCode = "10"})
          LGA.Add(New LGA With {.LGAID = "212", .LGAName = "ABAKALIKI", .LGACode = "AKL", .StateCode = "11"})
          LGA.Add(New LGA With {.LGAID = "213", .LGAName = "AFIKPO-NORTH", .LGACode = "AFK", .StateCode = "11"})
          LGA.Add(New LGA With {.LGAID = "214", .LGAName = "AFIKPO-SOUTH", .LGACode = "EDA", .StateCode = "11"})
          LGA.Add(New LGA With {.LGAID = "215", .LGAName = "EBONYI", .LGACode = "UGB", .StateCode = "11"})
          LGA.Add(New LGA With {.LGAID = "216", .LGAName = "EZZA-NORTH", .LGACode = "EBJ", .StateCode = "11"})
          LGA.Add(New LGA With {.LGAID = "217", .LGAName = "EZZA-SOUTH", .LGACode = "NKE", .StateCode = "11"})
          LGA.Add(New LGA With {.LGAID = "218", .LGAName = "IKWO", .LGACode = "CHR", .StateCode = "11"})
          LGA.Add(New LGA With {.LGAID = "219", .LGAName = "ISHIELU", .LGACode = "ZLL", .StateCode = "11"})
          LGA.Add(New LGA With {.LGAID = "220", .LGAName = "IVO", .LGACode = "SKA", .StateCode = "11"})
          LGA.Add(New LGA With {.LGAID = "221", .LGAName = "IZZI", .LGACode = "BKL", .StateCode = "11"})
          LGA.Add(New LGA With {.LGAID = "222", .LGAName = "OHAKWU", .LGACode = "HKW", .StateCode = "11"})
          LGA.Add(New LGA With {.LGAID = "223", .LGAName = "OHAOZARA", .LGACode = "BZR", .StateCode = "11"})
          LGA.Add(New LGA With {.LGAID = "224", .LGAName = "ONICHA", .LGACode = "NCA", .StateCode = "11"})
          LGA.Add(New LGA With {.LGAID = "225", .LGAName = "AKOKO-EDO", .LGACode = "GAR", .StateCode = "12"})
          LGA.Add(New LGA With {.LGAID = "226", .LGAName = "EGOR", .LGACode = "USL", .StateCode = "12"})
          LGA.Add(New LGA With {.LGAID = "227", .LGAName = "ESAN-CENTRAL", .LGACode = "RRU", .StateCode = "12"})
          LGA.Add(New LGA With {.LGAID = "228", .LGAName = "ESAN-NORTH-EAST", .LGACode = "URM", .StateCode = "12"})
          LGA.Add(New LGA With {.LGAID = "229", .LGAName = "ESAN-SOUTH-EAST", .LGACode = "UBJ", .StateCode = "12"})
          LGA.Add(New LGA With {.LGAID = "230", .LGAName = "ESAN-WEST", .LGACode = "EKP", .StateCode = "12"})
          LGA.Add(New LGA With {.LGAID = "231", .LGAName = "ETSAKO-CENTRAL", .LGACode = "FUG", .StateCode = "12"})
          LGA.Add(New LGA With {.LGAID = "232", .LGAName = "ETSAKO-EAST", .LGACode = "AGD", .StateCode = "12"})
          LGA.Add(New LGA With {.LGAID = "233", .LGAName = "ETSAKO-WEST", .LGACode = "AUC", .StateCode = "12"})
          LGA.Add(New LGA With {.LGAID = "234", .LGAName = "IGUEBEN", .LGACode = "GUE", .StateCode = "12"})
          LGA.Add(New LGA With {.LGAID = "235", .LGAName = "IKPOBA-OKHA", .LGACode = "DGE", .StateCode = "12"})
          LGA.Add(New LGA With {.LGAID = "236", .LGAName = "OREDO", .LGACode = "BEN", .StateCode = "12"})
          LGA.Add(New LGA With {.LGAID = "237", .LGAName = "ORHIONMWON", .LGACode = "ABD", .StateCode = "12"})
          LGA.Add(New LGA With {.LGAID = "238", .LGAName = "OVIA-NORTH-EAST", .LGACode = "AKA", .StateCode = "12"})
          LGA.Add(New LGA With {.LGAID = "239", .LGAName = "OVIA-SOUTH-WEST", .LGACode = "GBZ", .StateCode = "12"})
          LGA.Add(New LGA With {.LGAID = "240", .LGAName = "OWAN-EAST", .LGACode = "AFZ", .StateCode = "12"})
          LGA.Add(New LGA With {.LGAID = "241", .LGAName = "OWAN-WEST", .LGACode = "SGD", .StateCode = "12"})
          LGA.Add(New LGA With {.LGAID = "242", .LGAName = "UHUNMWONDE", .LGACode = "HER", .StateCode = "12"})
          LGA.Add(New LGA With {.LGAID = "243", .LGAName = "ADO-EKITI", .LGACode = "ADK", .StateCode = "13"})
          LGA.Add(New LGA With {.LGAID = "244", .LGAName = "EFON", .LGACode = "EFY", .StateCode = "13"})
          LGA.Add(New LGA With {.LGAID = "245", .LGAName = "EKITI-EAST", .LGACode = "MUE", .StateCode = "13"})
          LGA.Add(New LGA With {.LGAID = "246", .LGAName = "EKITI-SOUTH-WEST", .LGACode = "LAW", .StateCode = "13"})
          LGA.Add(New LGA With {.LGAID = "247", .LGAName = "EKITI-WEST", .LGACode = "AMK", .StateCode = "13"})
          LGA.Add(New LGA With {.LGAID = "248", .LGAName = "EMURE", .LGACode = "EMR", .StateCode = "13"})
          LGA.Add(New LGA With {.LGAID = "249", .LGAName = "GBONYIN", .LGACode = "DEA", .StateCode = "13"})
          LGA.Add(New LGA With {.LGAID = "250", .LGAName = "IDO-OSI", .LGACode = "DEK", .StateCode = "13"})
          LGA.Add(New LGA With {.LGAID = "251", .LGAName = "IJERO", .LGACode = "JER", .StateCode = "13"})
          LGA.Add(New LGA With {.LGAID = "252", .LGAName = "IKERE", .LGACode = "KER", .StateCode = "13"})
          LGA.Add(New LGA With {.LGAID = "253", .LGAName = "IKOLE", .LGACode = "KLE", .StateCode = "13"})
          LGA.Add(New LGA With {.LGAID = "254", .LGAName = "ILEJEMEJE", .LGACode = "YEK", .StateCode = "13"})
          LGA.Add(New LGA With {.LGAID = "255", .LGAName = "IREPODUN/IFELODUN", .LGACode = "GED", .StateCode = "13"})
          LGA.Add(New LGA With {.LGAID = "256", .LGAName = "ISE-ORUN", .LGACode = "SSE", .StateCode = "13"})
          LGA.Add(New LGA With {.LGAID = "257", .LGAName = "MOBA", .LGACode = "TUN", .StateCode = "13"})
          LGA.Add(New LGA With {.LGAID = "258", .LGAName = "OYE", .LGACode = "YEE", .StateCode = "13"})
          LGA.Add(New LGA With {.LGAID = "259", .LGAName = "ANINRI", .LGACode = "DBR", .StateCode = "14"})
          LGA.Add(New LGA With {.LGAID = "260", .LGAName = "AWGU", .LGACode = "AWG", .StateCode = "14"})
          LGA.Add(New LGA With {.LGAID = "261", .LGAName = "ENUGU-EAST", .LGACode = "NKW", .StateCode = "14"})
          LGA.Add(New LGA With {.LGAID = "262", .LGAName = "ENUGU-NORTH", .LGACode = "ENU", .StateCode = "14"})
          LGA.Add(New LGA With {.LGAID = "263", .LGAName = "ENUGU-SOUTH", .LGACode = "UWN", .StateCode = "14"})
          LGA.Add(New LGA With {.LGAID = "264", .LGAName = "EZEAGU", .LGACode = "AGW", .StateCode = "14"})
          LGA.Add(New LGA With {.LGAID = "265", .LGAName = "IGBO-ETITI", .LGACode = "GBD", .StateCode = "14"})
          LGA.Add(New LGA With {.LGAID = "266", .LGAName = "IGBO-EZE-NORTH", .LGACode = "ENZ", .StateCode = "14"})
          LGA.Add(New LGA With {.LGAID = "267", .LGAName = "IGBO-EZE-SOUTH", .LGACode = "BBG", .StateCode = "14"})
          LGA.Add(New LGA With {.LGAID = "268", .LGAName = "ISI-UZO", .LGACode = "KEM", .StateCode = "14"})
          LGA.Add(New LGA With {.LGAID = "269", .LGAName = "NKANU-EAST", .LGACode = "MGL", .StateCode = "14"})
          LGA.Add(New LGA With {.LGAID = "270", .LGAName = "NKANU-WEST", .LGACode = "AGN", .StateCode = "14"})
          LGA.Add(New LGA With {.LGAID = "271", .LGAName = "NSUKKA", .LGACode = "NSK", .StateCode = "14"})
          LGA.Add(New LGA With {.LGAID = "272", .LGAName = "OJI-RIVER", .LGACode = "JRV", .StateCode = "14"})
          LGA.Add(New LGA With {.LGAID = "273", .LGAName = "UDENU", .LGACode = "BLF", .StateCode = "14"})
          LGA.Add(New LGA With {.LGAID = "274", .LGAName = "UDI", .LGACode = "UDD", .StateCode = "14"})
          LGA.Add(New LGA With {.LGAID = "275", .LGAName = "UZO-UWANI", .LGACode = "UMU", .StateCode = "14"})
          LGA.Add(New LGA With {.LGAID = "276", .LGAName = "ABAJI AREA COUNCIL", .LGACode = "ABJ", .StateCode = "15"})
          LGA.Add(New LGA With {.LGAID = "277", .LGAName = "ABUJA MUNICIPAL AREA COUNCIL", .LGACode = "ABC", .StateCode = "15"})
          LGA.Add(New LGA With {.LGAID = "278", .LGAName = "BWARI", .LGACode = "BWR", .StateCode = "15"})
          LGA.Add(New LGA With {.LGAID = "279", .LGAName = "GWAGWALADA", .LGACode = "GWA", .StateCode = "15"})
          LGA.Add(New LGA With {.LGAID = "280", .LGAName = "KUJE AREA COUNCIL", .LGACode = "KUJ", .StateCode = "15"})
          LGA.Add(New LGA With {.LGAID = "281", .LGAName = "KWALI", .LGACode = "KWL", .StateCode = "15"})
          LGA.Add(New LGA With {.LGAID = "282", .LGAName = "AKKO", .LGACode = "AKK", .StateCode = "16"})
          LGA.Add(New LGA With {.LGAID = "283", .LGAName = "BALANGA", .LGACode = "BLG", .StateCode = "16"})
          LGA.Add(New LGA With {.LGAID = "284", .LGAName = "BILLIRI", .LGACode = "BLR", .StateCode = "16"})
          LGA.Add(New LGA With {.LGAID = "285", .LGAName = "DUKKU", .LGACode = "DKU", .StateCode = "16"})
          LGA.Add(New LGA With {.LGAID = "286", .LGAName = "FUNAKAYE", .LGACode = "FKY", .StateCode = "16"})
          LGA.Add(New LGA With {.LGAID = "287", .LGAName = "GOMBE", .LGACode = "GME", .StateCode = "16"})
          LGA.Add(New LGA With {.LGAID = "288", .LGAName = "KALTUNGO", .LGACode = "KLT", .StateCode = "16"})
          LGA.Add(New LGA With {.LGAID = "289", .LGAName = "KWAMI", .LGACode = "KWM", .StateCode = "16"})
          LGA.Add(New LGA With {.LGAID = "290", .LGAName = "NAFADA/BAJOGA", .LGACode = "NFD", .StateCode = "16"})
          LGA.Add(New LGA With {.LGAID = "291", .LGAName = "SHOMGOM", .LGACode = "SHM", .StateCode = "16"})
          LGA.Add(New LGA With {.LGAID = "292", .LGAName = "YAMALTU/DEBA", .LGACode = "YDB", .StateCode = "16"})
          LGA.Add(New LGA With {.LGAID = "293", .LGAName = "ABOH-MBAISE", .LGACode = "ABB", .StateCode = "17"})
          LGA.Add(New LGA With {.LGAID = "294", .LGAName = "AHIAZU-MBAISE", .LGACode = "AFR", .StateCode = "17"})
          LGA.Add(New LGA With {.LGAID = "295", .LGAName = "EHIME-MBANO", .LGACode = "EHM", .StateCode = "17"})
          LGA.Add(New LGA With {.LGAID = "296", .LGAName = "EZINIHITTE", .LGACode = "ETU", .StateCode = "17"})
          LGA.Add(New LGA With {.LGAID = "297", .LGAName = "IDEATO-NORTH", .LGACode = "URU", .StateCode = "17"})
          LGA.Add(New LGA With {.LGAID = "298", .LGAName = "IDEATO-SOUTH", .LGACode = "DFB", .StateCode = "17"})
          LGA.Add(New LGA With {.LGAID = "299", .LGAName = "IHITTE/UBOMA", .LGACode = "EKE", .StateCode = "17"})
          LGA.Add(New LGA With {.LGAID = "300", .LGAName = "IKEDURU", .LGACode = "KED", .StateCode = "17"})
          LGA.Add(New LGA With {.LGAID = "301", .LGAName = "ISIALA-MBANO", .LGACode = "UML", .StateCode = "17"})
          LGA.Add(New LGA With {.LGAID = "302", .LGAName = "ISU", .LGACode = "UMD", .StateCode = "17"})
          LGA.Add(New LGA With {.LGAID = "303", .LGAName = "MBAITOLI", .LGACode = "NWA", .StateCode = "17"})
          LGA.Add(New LGA With {.LGAID = "304", .LGAName = "NGOR-OKPALA", .LGACode = "NGN", .StateCode = "17"})
          LGA.Add(New LGA With {.LGAID = "305", .LGAName = "NJABA", .LGACode = "UMK", .StateCode = "17"})
          LGA.Add(New LGA With {.LGAID = "306", .LGAName = "NKWERRE", .LGACode = "NKR", .StateCode = "17"})
          LGA.Add(New LGA With {.LGAID = "307", .LGAName = "NWANGELE", .LGACode = "AMG", .StateCode = "17"})
          LGA.Add(New LGA With {.LGAID = "308", .LGAName = "OBOWO", .LGACode = "TTK", .StateCode = "17"})
          LGA.Add(New LGA With {.LGAID = "309", .LGAName = "OGUTA", .LGACode = "GUA", .StateCode = "17"})
          LGA.Add(New LGA With {.LGAID = "310", .LGAName = "OHAJI-EGBEMA", .LGACode = "EBM", .StateCode = "17"})
          LGA.Add(New LGA With {.LGAID = "311", .LGAName = "OKIGWE", .LGACode = "KGE", .StateCode = "17"})
          LGA.Add(New LGA With {.LGAID = "312", .LGAName = "ONUIMO", .LGACode = "KWE", .StateCode = "17"})
          LGA.Add(New LGA With {.LGAID = "313", .LGAName = "ORLU", .LGACode = "RLU", .StateCode = "17"})
          LGA.Add(New LGA With {.LGAID = "314", .LGAName = "ORSU", .LGACode = "AWD", .StateCode = "17"})
          LGA.Add(New LGA With {.LGAID = "315", .LGAName = "ORU-EAST", .LGACode = "MMA", .StateCode = "17"})
          LGA.Add(New LGA With {.LGAID = "316", .LGAName = "ORU-WEST", .LGACode = "NGB", .StateCode = "17"})
          LGA.Add(New LGA With {.LGAID = "317", .LGAName = "OWERRI-MUNICIPAL", .LGACode = "WER", .StateCode = "17"})
          LGA.Add(New LGA With {.LGAID = "318", .LGAName = "OWERRI-NORTH", .LGACode = "RRT", .StateCode = "17"})
          LGA.Add(New LGA With {.LGAID = "319", .LGAName = "OWERRI-WEST", .LGACode = "UMG", .StateCode = "17"})
          LGA.Add(New LGA With {.LGAID = "320", .LGAName = "AUYO", .LGACode = "AUY", .StateCode = "18"})
          LGA.Add(New LGA With {.LGAID = "321", .LGAName = "BABURA", .LGACode = "BBR", .StateCode = "18"})
          LGA.Add(New LGA With {.LGAID = "322", .LGAName = "BIRINIWA", .LGACode = "BNW", .StateCode = "18"})
          LGA.Add(New LGA With {.LGAID = "323", .LGAName = "BIRNIN-KUDU", .LGACode = "BKD", .StateCode = "18"})
          LGA.Add(New LGA With {.LGAID = "324", .LGAName = "BUJI", .LGACode = "BUJ", .StateCode = "18"})
          LGA.Add(New LGA With {.LGAID = "325", .LGAName = "DUTSE", .LGACode = "DUT", .StateCode = "18"})
          LGA.Add(New LGA With {.LGAID = "326", .LGAName = "GAGARAWA", .LGACode = "GGW", .StateCode = "18"})
          LGA.Add(New LGA With {.LGAID = "327", .LGAName = "GARKI", .LGACode = "GRK", .StateCode = "18"})
          LGA.Add(New LGA With {.LGAID = "328", .LGAName = "GUMEL", .LGACode = "GML", .StateCode = "18"})
          LGA.Add(New LGA With {.LGAID = "329", .LGAName = "GURI", .LGACode = "GRR", .StateCode = "18"})
          LGA.Add(New LGA With {.LGAID = "330", .LGAName = "GWARAM", .LGACode = "GRM", .StateCode = "18"})
          LGA.Add(New LGA With {.LGAID = "331", .LGAName = "GWIWA", .LGACode = "GWW", .StateCode = "18"})
          LGA.Add(New LGA With {.LGAID = "332", .LGAName = "HADEJIA", .LGACode = "HJA", .StateCode = "18"})
          LGA.Add(New LGA With {.LGAID = "333", .LGAName = "JAHUN", .LGACode = "JHN", .StateCode = "18"})
          LGA.Add(New LGA With {.LGAID = "334", .LGAName = "KAFIN-HAUSA", .LGACode = "KHS", .StateCode = "18"})
          LGA.Add(New LGA With {.LGAID = "335", .LGAName = "KAUGAMA", .LGACode = "KGM", .StateCode = "18"})
          LGA.Add(New LGA With {.LGAID = "336", .LGAName = "KAZAURE", .LGACode = "KZR", .StateCode = "18"})
          LGA.Add(New LGA With {.LGAID = "337", .LGAName = "KIRKASAMMA", .LGACode = "KKM", .StateCode = "18"})
          LGA.Add(New LGA With {.LGAID = "338", .LGAName = "KIYAWA", .LGACode = "KYW", .StateCode = "18"})
          LGA.Add(New LGA With {.LGAID = "339", .LGAName = "MAIGATARI", .LGACode = "MGR", .StateCode = "18"})
          LGA.Add(New LGA With {.LGAID = "340", .LGAName = "MALAM-MADURI", .LGACode = "MMR", .StateCode = "18"})
          LGA.Add(New LGA With {.LGAID = "341", .LGAName = "MIGA", .LGACode = "MGA", .StateCode = "18"})
          LGA.Add(New LGA With {.LGAID = "342", .LGAName = "RINGIM", .LGACode = "RNG", .StateCode = "18"})
          LGA.Add(New LGA With {.LGAID = "343", .LGAName = "RONI", .LGACode = "RRN", .StateCode = "18"})
          LGA.Add(New LGA With {.LGAID = "344", .LGAName = "SULE-TANKARKAR", .LGACode = "STK", .StateCode = "18"})
          LGA.Add(New LGA With {.LGAID = "345", .LGAName = "TAURA", .LGACode = "TAR", .StateCode = "18"})
          LGA.Add(New LGA With {.LGAID = "346", .LGAName = "YANKWASHI", .LGACode = "YKS", .StateCode = "18"})
          LGA.Add(New LGA With {.LGAID = "347", .LGAName = "BIRNIN-GWARI", .LGACode = "BNG", .StateCode = "19"})
          LGA.Add(New LGA With {.LGAID = "348", .LGAName = "CHIKUN", .LGACode = "KJM", .StateCode = "19"})
          LGA.Add(New LGA With {.LGAID = "349", .LGAName = "GIWA", .LGACode = "GKW", .StateCode = "19"})
          LGA.Add(New LGA With {.LGAID = "350", .LGAName = "IGABI", .LGACode = "TRK", .StateCode = "19"})
          LGA.Add(New LGA With {.LGAID = "351", .LGAName = "IKARA", .LGACode = "KAR", .StateCode = "19"})
          LGA.Add(New LGA With {.LGAID = "352", .LGAName = "JABA", .LGACode = "KWB", .StateCode = "19"})
          LGA.Add(New LGA With {.LGAID = "353", .LGAName = "JEMA A", .LGACode = "KAF", .StateCode = "19"})
          LGA.Add(New LGA With {.LGAID = "354", .LGAName = "KACHIA", .LGACode = "KCH", .StateCode = "19"})
          LGA.Add(New LGA With {.LGAID = "355", .LGAName = "KADUNA-NORTH", .LGACode = "DKA", .StateCode = "19"})
          LGA.Add(New LGA With {.LGAID = "356", .LGAName = "KADUNA-SOUTH", .LGACode = "MKA", .StateCode = "19"})
          LGA.Add(New LGA With {.LGAID = "357", .LGAName = "KAGARKO", .LGACode = "KGK", .StateCode = "19"})
          LGA.Add(New LGA With {.LGAID = "358", .LGAName = "KAJURU", .LGACode = "KJR", .StateCode = "19"})
          LGA.Add(New LGA With {.LGAID = "359", .LGAName = "KAURA", .LGACode = "KRA", .StateCode = "19"})
          LGA.Add(New LGA With {.LGAID = "360", .LGAName = "KAURU", .LGACode = "KRU", .StateCode = "19"})
          LGA.Add(New LGA With {.LGAID = "361", .LGAName = "KUBAH", .LGACode = "ANC", .StateCode = "19"})
          LGA.Add(New LGA With {.LGAID = "362", .LGAName = "KUDAN", .LGACode = "HKY", .StateCode = "19"})
          LGA.Add(New LGA With {.LGAID = "363", .LGAName = "LERE", .LGACode = "SNK", .StateCode = "19"})
          LGA.Add(New LGA With {.LGAID = "364", .LGAName = "MAKARFI", .LGACode = "MKR", .StateCode = "19"})
          LGA.Add(New LGA With {.LGAID = "365", .LGAName = "SABON-GARI", .LGACode = "SBG", .StateCode = "19"})
          LGA.Add(New LGA With {.LGAID = "366", .LGAName = "SANGA", .LGACode = "GWT", .StateCode = "19"})
          LGA.Add(New LGA With {.LGAID = "367", .LGAName = "SOBA", .LGACode = "MGN", .StateCode = "19"})
          LGA.Add(New LGA With {.LGAID = "368", .LGAName = "ZANGON-KATAF", .LGACode = "ZKW", .StateCode = "19"})
          LGA.Add(New LGA With {.LGAID = "369", .LGAName = "ZARIA", .LGACode = "ZAR", .StateCode = "19"})
          LGA.Add(New LGA With {.LGAID = "370", .LGAName = "AJINGI", .LGACode = "AJG", .StateCode = "20"})
          LGA.Add(New LGA With {.LGAID = "371", .LGAName = "ALBASU", .LGACode = "ABS", .StateCode = "20"})
          LGA.Add(New LGA With {.LGAID = "372", .LGAName = "BAGWAI", .LGACode = "BGW", .StateCode = "20"})
          LGA.Add(New LGA With {.LGAID = "373", .LGAName = "BEBEJI", .LGACode = "BBJ", .StateCode = "20"})
          LGA.Add(New LGA With {.LGAID = "374", .LGAName = "BICHI", .LGACode = "BCH", .StateCode = "20"})
          LGA.Add(New LGA With {.LGAID = "375", .LGAName = "BUNKURE", .LGACode = "BNK", .StateCode = "20"})
          LGA.Add(New LGA With {.LGAID = "376", .LGAName = "DALA", .LGACode = "DAL", .StateCode = "20"})
          LGA.Add(New LGA With {.LGAID = "377", .LGAName = "DANBATTA", .LGACode = "DBT", .StateCode = "20"})
          LGA.Add(New LGA With {.LGAID = "378", .LGAName = "DAWAKIN-KUDU", .LGACode = "DKD", .StateCode = "20"})
          LGA.Add(New LGA With {.LGAID = "379", .LGAName = "DAWAKIN-TOFA", .LGACode = "DTF", .StateCode = "20"})
          LGA.Add(New LGA With {.LGAID = "380", .LGAName = "DOGUWA", .LGACode = "DGW", .StateCode = "20"})
          LGA.Add(New LGA With {.LGAID = "381", .LGAName = "FAGGE", .LGACode = "FGE", .StateCode = "20"})
          LGA.Add(New LGA With {.LGAID = "382", .LGAName = "GABASAWA", .LGACode = "DSW", .StateCode = "20"})
          LGA.Add(New LGA With {.LGAID = "383", .LGAName = "GARKO", .LGACode = "GAK", .StateCode = "20"})
          LGA.Add(New LGA With {.LGAID = "384", .LGAName = "GARUN-MALLAM", .LGACode = "GNM", .StateCode = "20"})
          LGA.Add(New LGA With {.LGAID = "385", .LGAName = "GAYA", .LGACode = "GYA", .StateCode = "20"})
          LGA.Add(New LGA With {.LGAID = "386", .LGAName = "GEZAWA", .LGACode = "GZW", .StateCode = "20"})
          LGA.Add(New LGA With {.LGAID = "387", .LGAName = "GWALE", .LGACode = "GWL", .StateCode = "20"})
          LGA.Add(New LGA With {.LGAID = "388", .LGAName = "GWARZO", .LGACode = "GRZ", .StateCode = "20"})
          LGA.Add(New LGA With {.LGAID = "389", .LGAName = "KABO", .LGACode = "KBK", .StateCode = "20"})
          LGA.Add(New LGA With {.LGAID = "390", .LGAName = "KANO-MUNICIPAL", .LGACode = "KMC", .StateCode = "20"})
          LGA.Add(New LGA With {.LGAID = "391", .LGAName = "KARAYE", .LGACode = "KRY", .StateCode = "20"})
          LGA.Add(New LGA With {.LGAID = "392", .LGAName = "KIBIYA", .LGACode = "KBY", .StateCode = "20"})
          LGA.Add(New LGA With {.LGAID = "393", .LGAName = "KIRU", .LGACode = "KKU", .StateCode = "20"})
          LGA.Add(New LGA With {.LGAID = "394", .LGAName = "KUMBOTSO", .LGACode = "KBT", .StateCode = "20"})
          LGA.Add(New LGA With {.LGAID = "395", .LGAName = "KUNCHI", .LGACode = "KNC", .StateCode = "20"})
          LGA.Add(New LGA With {.LGAID = "396", .LGAName = "KURA", .LGACode = "KUR", .StateCode = "20"})
          LGA.Add(New LGA With {.LGAID = "397", .LGAName = "MADOBI", .LGACode = "MDB", .StateCode = "20"})
          LGA.Add(New LGA With {.LGAID = "398", .LGAName = "MAKODA", .LGACode = "MKK", .StateCode = "20"})
          LGA.Add(New LGA With {.LGAID = "399", .LGAName = "MINJIBIR", .LGACode = "MJB", .StateCode = "20"})
          LGA.Add(New LGA With {.LGAID = "400", .LGAName = "NASSARAWA", .LGACode = "NSR", .StateCode = "20"})
          LGA.Add(New LGA With {.LGAID = "401", .LGAName = "RANO", .LGACode = "RAN", .StateCode = "20"})
          LGA.Add(New LGA With {.LGAID = "402", .LGAName = "RIMIN-GADO", .LGACode = "RMG", .StateCode = "20"})
          LGA.Add(New LGA With {.LGAID = "403", .LGAName = "ROGO", .LGACode = "RGG", .StateCode = "20"})
          LGA.Add(New LGA With {.LGAID = "404", .LGAName = "SHANONO", .LGACode = "SNN", .StateCode = "20"})
          LGA.Add(New LGA With {.LGAID = "405", .LGAName = "SUMAILA", .LGACode = "SML", .StateCode = "20"})
          LGA.Add(New LGA With {.LGAID = "406", .LGAName = "TAKAI", .LGACode = "TAK", .StateCode = "20"})
          LGA.Add(New LGA With {.LGAID = "407", .LGAName = "TARAUNI", .LGACode = "TRN", .StateCode = "20"})
          LGA.Add(New LGA With {.LGAID = "408", .LGAName = "TOFA", .LGACode = "TFA", .StateCode = "20"})
          LGA.Add(New LGA With {.LGAID = "409", .LGAName = "TSANYAWA", .LGACode = "TYW", .StateCode = "20"})
          LGA.Add(New LGA With {.LGAID = "410", .LGAName = "TUDUN-WADA", .LGACode = "TWD", .StateCode = "20"})
          LGA.Add(New LGA With {.LGAID = "411", .LGAName = "UNGOGO", .LGACode = "UGG", .StateCode = "20"})
          LGA.Add(New LGA With {.LGAID = "412", .LGAName = "WARAWA", .LGACode = "WRA", .StateCode = "20"})
          LGA.Add(New LGA With {.LGAID = "413", .LGAName = "WUDIL", .LGACode = "WDL", .StateCode = "20"})
          LGA.Add(New LGA With {.LGAID = "414", .LGAName = "BAKORI", .LGACode = "BKR", .StateCode = "21"})
          LGA.Add(New LGA With {.LGAID = "415", .LGAName = "BATAGARAWA", .LGACode = "BAT", .StateCode = "21"})
          LGA.Add(New LGA With {.LGAID = "416", .LGAName = "BATSARI", .LGACode = "BTR", .StateCode = "21"})
          LGA.Add(New LGA With {.LGAID = "417", .LGAName = "BAURE", .LGACode = "BRE", .StateCode = "21"})
          LGA.Add(New LGA With {.LGAID = "418", .LGAName = "BINDAWA", .LGACode = "BDW", .StateCode = "21"})
          LGA.Add(New LGA With {.LGAID = "419", .LGAName = "CHARANCHI", .LGACode = "CRC", .StateCode = "21"})
          LGA.Add(New LGA With {.LGAID = "420", .LGAName = "DANDUME", .LGACode = "DDM", .StateCode = "21"})
          LGA.Add(New LGA With {.LGAID = "421", .LGAName = "DANJA", .LGACode = "DJA", .StateCode = "21"})
          LGA.Add(New LGA With {.LGAID = "422", .LGAName = "DAN-MUSA", .LGACode = "DMS", .StateCode = "21"})
          LGA.Add(New LGA With {.LGAID = "423", .LGAName = "DAURA", .LGACode = "DRA", .StateCode = "21"})
          LGA.Add(New LGA With {.LGAID = "424", .LGAName = "DUTSI", .LGACode = "DTS", .StateCode = "21"})
          LGA.Add(New LGA With {.LGAID = "425", .LGAName = "DUTSINMA", .LGACode = "DTM", .StateCode = "21"})
          LGA.Add(New LGA With {.LGAID = "426", .LGAName = "FASKARI", .LGACode = "FSK", .StateCode = "21"})
          LGA.Add(New LGA With {.LGAID = "427", .LGAName = "FUNTUA", .LGACode = "FTA", .StateCode = "21"})
          LGA.Add(New LGA With {.LGAID = "428", .LGAName = "INGAWA", .LGACode = "NGW", .StateCode = "21"})
          LGA.Add(New LGA With {.LGAID = "429", .LGAName = "JIBIA", .LGACode = "JBY", .StateCode = "21"})
          LGA.Add(New LGA With {.LGAID = "430", .LGAName = "KAFUR", .LGACode = "KFR", .StateCode = "21"})
          LGA.Add(New LGA With {.LGAID = "431", .LGAName = "KAITA", .LGACode = "KAT", .StateCode = "21"})
          LGA.Add(New LGA With {.LGAID = "432", .LGAName = "KANKARA", .LGACode = "KKR", .StateCode = "21"})
          LGA.Add(New LGA With {.LGAID = "433", .LGAName = "KANKIA", .LGACode = "KNK", .StateCode = "21"})
          LGA.Add(New LGA With {.LGAID = "434", .LGAName = "KATSINA", .LGACode = "KTN", .StateCode = "21"})
          LGA.Add(New LGA With {.LGAID = "435", .LGAName = "KURFI", .LGACode = "KUF", .StateCode = "21"})
          LGA.Add(New LGA With {.LGAID = "436", .LGAName = "KUSADA", .LGACode = "KSD", .StateCode = "21"})
          LGA.Add(New LGA With {.LGAID = "437", .LGAName = "MAI-ADUA", .LGACode = "MDW", .StateCode = "21"})
          LGA.Add(New LGA With {.LGAID = "438", .LGAName = "MALUMFASHI", .LGACode = "MNF", .StateCode = "21"})
          LGA.Add(New LGA With {.LGAID = "439", .LGAName = "MANI", .LGACode = "MAN", .StateCode = "21"})
          LGA.Add(New LGA With {.LGAID = "440", .LGAName = "MASHI", .LGACode = "MSH", .StateCode = "21"})
          LGA.Add(New LGA With {.LGAID = "441", .LGAName = "MATAZU", .LGACode = "MTZ", .StateCode = "21"})
          LGA.Add(New LGA With {.LGAID = "442", .LGAName = "MUSAWA", .LGACode = "MSW", .StateCode = "21"})
          LGA.Add(New LGA With {.LGAID = "443", .LGAName = "RIMI", .LGACode = "RMY", .StateCode = "21"})
          LGA.Add(New LGA With {.LGAID = "444", .LGAName = "SABUWA", .LGACode = "SBA", .StateCode = "21"})
          LGA.Add(New LGA With {.LGAID = "445", .LGAName = "SAFANA", .LGACode = "SFN", .StateCode = "21"})
          LGA.Add(New LGA With {.LGAID = "446", .LGAName = "SANDAMU", .LGACode = "SDM", .StateCode = "21"})
          LGA.Add(New LGA With {.LGAID = "447", .LGAName = "ZANGO", .LGACode = "ZNG", .StateCode = "21"})
          LGA.Add(New LGA With {.LGAID = "448", .LGAName = "ALEIRO", .LGACode = "ALR", .StateCode = "22"})
          LGA.Add(New LGA With {.LGAID = "449", .LGAName = "AREWA-DANDI", .LGACode = "KGW", .StateCode = "22"})
          LGA.Add(New LGA With {.LGAID = "450", .LGAName = "ARGUNGU", .LGACode = "ARG", .StateCode = "22"})
          LGA.Add(New LGA With {.LGAID = "451", .LGAName = "AUGIE", .LGACode = "AUG", .StateCode = "22"})
          LGA.Add(New LGA With {.LGAID = "452", .LGAName = "BAGUDO", .LGACode = "BGD", .StateCode = "22"})
          LGA.Add(New LGA With {.LGAID = "453", .LGAName = "BIRNIN-KEBBI", .LGACode = "BRK", .StateCode = "22"})
          LGA.Add(New LGA With {.LGAID = "454", .LGAName = "BUNZA", .LGACode = "BNZ", .StateCode = "22"})
          LGA.Add(New LGA With {.LGAID = "455", .LGAName = "DANDI", .LGACode = "KMB", .StateCode = "22"})
          LGA.Add(New LGA With {.LGAID = "456", .LGAName = "FAKAI", .LGACode = "MHT", .StateCode = "22"})
          LGA.Add(New LGA With {.LGAID = "457", .LGAName = "GWANDU", .LGACode = "GWN", .StateCode = "22"})
          LGA.Add(New LGA With {.LGAID = "458", .LGAName = "JEGA", .LGACode = "JEG", .StateCode = "22"})
          LGA.Add(New LGA With {.LGAID = "459", .LGAName = "KALGO", .LGACode = "KLG", .StateCode = "22"})
          LGA.Add(New LGA With {.LGAID = "460", .LGAName = "KOKO-BESSE", .LGACode = "BES", .StateCode = "22"})
          LGA.Add(New LGA With {.LGAID = "461", .LGAName = "MAIYAMA", .LGACode = "MYM", .StateCode = "22"})
          LGA.Add(New LGA With {.LGAID = "462", .LGAName = "NGASKI", .LGACode = "WRR", .StateCode = "22"})
          LGA.Add(New LGA With {.LGAID = "463", .LGAName = "SAKABA", .LGACode = "DRD", .StateCode = "22"})
          LGA.Add(New LGA With {.LGAID = "464", .LGAName = "SHANGA", .LGACode = "SNA", .StateCode = "22"})
          LGA.Add(New LGA With {.LGAID = "465", .LGAName = "SURU", .LGACode = "DKG", .StateCode = "22"})
          LGA.Add(New LGA With {.LGAID = "466", .LGAName = "WASAGU", .LGACode = "RBH", .StateCode = "22"})
          LGA.Add(New LGA With {.LGAID = "467", .LGAName = "YAURI", .LGACode = "YLW", .StateCode = "22"})
          LGA.Add(New LGA With {.LGAID = "468", .LGAName = "ZURU", .LGACode = "ZUR", .StateCode = "22"})
          LGA.Add(New LGA With {.LGAID = "469", .LGAName = "ADAVI", .LGACode = "DAV", .StateCode = "23"})
          LGA.Add(New LGA With {.LGAID = "470", .LGAName = "AJAOKUTA", .LGACode = "AJA", .StateCode = "23"})
          LGA.Add(New LGA With {.LGAID = "471", .LGAName = "ANKPA", .LGACode = "KPA", .StateCode = "23"})
          LGA.Add(New LGA With {.LGAID = "472", .LGAName = "BASSA", .LGACode = "BAS", .StateCode = "23"})
          LGA.Add(New LGA With {.LGAID = "473", .LGAName = "DEKINA", .LGACode = "KNA", .StateCode = "23"})
          LGA.Add(New LGA With {.LGAID = "474", .LGAName = "IBAJI", .LGACode = "NDG", .StateCode = "23"})
          LGA.Add(New LGA With {.LGAID = "475", .LGAName = "IDAH", .LGACode = "DAH", .StateCode = "23"})
          LGA.Add(New LGA With {.LGAID = "476", .LGAName = "IGALAMELA-ODOLU", .LGACode = "AJK", .StateCode = "23"})
          LGA.Add(New LGA With {.LGAID = "477", .LGAName = "IJUMU", .LGACode = "JMU", .StateCode = "23"})
          LGA.Add(New LGA With {.LGAID = "478", .LGAName = "KABBA/BUNU", .LGACode = "KAB", .StateCode = "23"})
          LGA.Add(New LGA With {.LGAID = "479", .LGAName = "KOGI", .LGACode = "KKF", .StateCode = "23"})
          LGA.Add(New LGA With {.LGAID = "480", .LGAName = "LOKOJA", .LGACode = "LKJ", .StateCode = "23"})
          LGA.Add(New LGA With {.LGAID = "481", .LGAName = "MOPA-MURO-MOPI", .LGACode = "MPA", .StateCode = "23"})
          LGA.Add(New LGA With {.LGAID = "482", .LGAName = "OFU", .LGACode = "KFU", .StateCode = "23"})
          LGA.Add(New LGA With {.LGAID = "483", .LGAName = "OGORI/MAGONGO", .LGACode = "KPF", .StateCode = "23"})
          LGA.Add(New LGA With {.LGAID = "484", .LGAName = "OKEHI", .LGACode = "KKH", .StateCode = "23"})
          LGA.Add(New LGA With {.LGAID = "485", .LGAName = "OKENE", .LGACode = "KNE", .StateCode = "23"})
          LGA.Add(New LGA With {.LGAID = "486", .LGAName = "OLAMABORO", .LGACode = "LAM", .StateCode = "23"})
          LGA.Add(New LGA With {.LGAID = "487", .LGAName = "OMALA", .LGACode = "BJK", .StateCode = "23"})
          LGA.Add(New LGA With {.LGAID = "488", .LGAName = "YAGBA-EAST", .LGACode = "ERE", .StateCode = "23"})
          LGA.Add(New LGA With {.LGAID = "489", .LGAName = "YAGBA-WEST", .LGACode = "SAN", .StateCode = "23"})
          LGA.Add(New LGA With {.LGAID = "490", .LGAName = "ASA", .LGACode = "AFN", .StateCode = "24"})
          LGA.Add(New LGA With {.LGAID = "491", .LGAName = "BARUTEN", .LGACode = "KSB", .StateCode = "24"})
          LGA.Add(New LGA With {.LGAID = "492", .LGAName = "EDU", .LGACode = "LAF", .StateCode = "24"})
          LGA.Add(New LGA With {.LGAID = "493", .LGAName = "EKITI", .LGACode = "ARP", .StateCode = "24"})
          LGA.Add(New LGA With {.LGAID = "494", .LGAName = "IFELODUN", .LGACode = "SHA", .StateCode = "24"})
          LGA.Add(New LGA With {.LGAID = "495", .LGAName = "ILORIN-EAST", .LGACode = "KEY", .StateCode = "24"})
          LGA.Add(New LGA With {.LGAID = "496", .LGAName = "ILORIN-SOUTH", .LGACode = "FUF", .StateCode = "24"})
          LGA.Add(New LGA With {.LGAID = "497", .LGAName = "ILORIN-WEST", .LGACode = "LRN", .StateCode = "24"})
          LGA.Add(New LGA With {.LGAID = "498", .LGAName = "IREPODUN", .LGACode = "MUN", .StateCode = "24"})
          LGA.Add(New LGA With {.LGAID = "499", .LGAName = "ISIN", .LGACode = "WSN", .StateCode = "24"})
          LGA.Add(New LGA With {.LGAID = "500", .LGAName = "KAIAMA", .LGACode = "KMA", .StateCode = "24"})
          LGA.Add(New LGA With {.LGAID = "501", .LGAName = "MORO", .LGACode = "BDU", .StateCode = "24"})
          LGA.Add(New LGA With {.LGAID = "502", .LGAName = "OFFA", .LGACode = "FFA", .StateCode = "24"})
          LGA.Add(New LGA With {.LGAID = "503", .LGAName = "OKE-ERO", .LGACode = "LFF", .StateCode = "24"})
          LGA.Add(New LGA With {.LGAID = "504", .LGAName = "OYUN", .LGACode = "LEM", .StateCode = "24"})
          LGA.Add(New LGA With {.LGAID = "505", .LGAName = "PATEGI", .LGACode = "PTG", .StateCode = "24"})
          LGA.Add(New LGA With {.LGAID = "506", .LGAName = "AGEGE", .LGACode = "GGE", .StateCode = "25"})
          LGA.Add(New LGA With {.LGAID = "507", .LGAName = "AJEROMI-IFELODUN", .LGACode = "AGL", .StateCode = "25"})
          LGA.Add(New LGA With {.LGAID = "508", .LGAName = "ALIMOSHO", .LGACode = "KTU", .StateCode = "25"})
          LGA.Add(New LGA With {.LGAID = "509", .LGAName = "AMUWO-ODOFIN", .LGACode = "FST", .StateCode = "25"})
          LGA.Add(New LGA With {.LGAID = "510", .LGAName = "APAPA", .LGACode = "APP", .StateCode = "25"})
          LGA.Add(New LGA With {.LGAID = "511", .LGAName = "BADAGRY", .LGACode = "BDG", .StateCode = "25"})
          LGA.Add(New LGA With {.LGAID = "512", .LGAName = "EPE", .LGACode = "EPE", .StateCode = "25"})
          LGA.Add(New LGA With {.LGAID = "513", .LGAName = "ETI-OSA", .LGACode = "EKY", .StateCode = "25"})
          LGA.Add(New LGA With {.LGAID = "514", .LGAName = "IBEJU-LEKKI", .LGACode = "AKD", .StateCode = "25"})
          LGA.Add(New LGA With {.LGAID = "515", .LGAName = "IFAKO-IJAIYE", .LGACode = "FKJ", .StateCode = "25"})
          LGA.Add(New LGA With {.LGAID = "516", .LGAName = "IKEJA", .LGACode = "KJA", .StateCode = "25"})
          LGA.Add(New LGA With {.LGAID = "517", .LGAName = "IKORODU", .LGACode = "KRD", .StateCode = "25"})
          LGA.Add(New LGA With {.LGAID = "518", .LGAName = "KOSOFE", .LGACode = "KSF", .StateCode = "25"})
          LGA.Add(New LGA With {.LGAID = "519", .LGAName = "LAGOS-ISLAND", .LGACode = "AAA", .StateCode = "25"})
          LGA.Add(New LGA With {.LGAID = "520", .LGAName = "LAGOS-MAINLAND", .LGACode = "LND", .StateCode = "25"})
          LGA.Add(New LGA With {.LGAID = "521", .LGAName = "MUSHIN", .LGACode = "MUS", .StateCode = "25"})
          LGA.Add(New LGA With {.LGAID = "522", .LGAName = "OJO", .LGACode = "JJJ", .StateCode = "25"})
          LGA.Add(New LGA With {.LGAID = "523", .LGAName = "OSHODI-ISOLO", .LGACode = "LSD", .StateCode = "25"})
          LGA.Add(New LGA With {.LGAID = "524", .LGAName = "SOMOLU", .LGACode = "SMK", .StateCode = "25"})
          LGA.Add(New LGA With {.LGAID = "525", .LGAName = "SURULERE", .LGACode = "LSR", .StateCode = "25"})
          LGA.Add(New LGA With {.LGAID = "526", .LGAName = "AKWANGA", .LGACode = "AKW", .StateCode = "26"})
          LGA.Add(New LGA With {.LGAID = "527", .LGAName = "AWE", .LGACode = "AWE", .StateCode = "26"})
          LGA.Add(New LGA With {.LGAID = "528", .LGAName = "DOMA", .LGACode = "DMA", .StateCode = "26"})
          LGA.Add(New LGA With {.LGAID = "529", .LGAName = "KARU", .LGACode = "KRV", .StateCode = "26"})
          LGA.Add(New LGA With {.LGAID = "530", .LGAName = "KEANA", .LGACode = "KEN", .StateCode = "26"})
          LGA.Add(New LGA With {.LGAID = "531", .LGAName = "KEFFI", .LGACode = "KEF", .StateCode = "26"})
          LGA.Add(New LGA With {.LGAID = "532", .LGAName = "KOKONA", .LGACode = "GRU", .StateCode = "26"})
          LGA.Add(New LGA With {.LGAID = "533", .LGAName = "LAFIA", .LGACode = "LFA", .StateCode = "26"})
          LGA.Add(New LGA With {.LGAID = "534", .LGAName = "NASARAWA", .LGACode = "NSW", .StateCode = "26"})
          LGA.Add(New LGA With {.LGAID = "535", .LGAName = "NASARAWA-EGGON", .LGACode = "NEG", .StateCode = "26"})
          LGA.Add(New LGA With {.LGAID = "536", .LGAName = "OBI", .LGACode = "NBB", .StateCode = "26"})
          LGA.Add(New LGA With {.LGAID = "537", .LGAName = "TOTO", .LGACode = "NTT", .StateCode = "26"})
          LGA.Add(New LGA With {.LGAID = "538", .LGAName = "WAMBA", .LGACode = "WAM", .StateCode = "26"})
          LGA.Add(New LGA With {.LGAID = "539", .LGAName = "AGAIE", .LGACode = "AGA", .StateCode = "27"})
          LGA.Add(New LGA With {.LGAID = "540", .LGAName = "AGWARA", .LGACode = "AGR", .StateCode = "27"})
          LGA.Add(New LGA With {.LGAID = "541", .LGAName = "BIDA", .LGACode = "BDA", .StateCode = "27"})
          LGA.Add(New LGA With {.LGAID = "542", .LGAName = "BORGU", .LGACode = "NBS", .StateCode = "27"})
          LGA.Add(New LGA With {.LGAID = "543", .LGAName = "BOSSO", .LGACode = "MAK", .StateCode = "27"})
          LGA.Add(New LGA With {.LGAID = "544", .LGAName = "CHANCHAGA", .LGACode = "MNA", .StateCode = "27"})
          LGA.Add(New LGA With {.LGAID = "545", .LGAName = "EDATI", .LGACode = "ENG", .StateCode = "27"})
          LGA.Add(New LGA With {.LGAID = "546", .LGAName = "GBAKO", .LGACode = "LMU", .StateCode = "27"})
          LGA.Add(New LGA With {.LGAID = "547", .LGAName = "GURARA", .LGACode = "GWU", .StateCode = "27"})
          LGA.Add(New LGA With {.LGAID = "548", .LGAName = "KATCHA", .LGACode = "KHA", .StateCode = "27"})
          LGA.Add(New LGA With {.LGAID = "549", .LGAName = "KONTAGORA", .LGACode = "KNT", .StateCode = "27"})
          LGA.Add(New LGA With {.LGAID = "550", .LGAName = "LAPAI", .LGACode = "LAP", .StateCode = "27"})
          LGA.Add(New LGA With {.LGAID = "551", .LGAName = "LAVUN", .LGACode = "KUG", .StateCode = "27"})
          LGA.Add(New LGA With {.LGAID = "552", .LGAName = "MAGAMA", .LGACode = "NAS", .StateCode = "27"})
          LGA.Add(New LGA With {.LGAID = "553", .LGAName = "MARIGA", .LGACode = "BMG", .StateCode = "27"})
          LGA.Add(New LGA With {.LGAID = "554", .LGAName = "MASHEGU", .LGACode = "MSG", .StateCode = "27"})
          LGA.Add(New LGA With {.LGAID = "555", .LGAName = "MOKWA", .LGACode = "MKW", .StateCode = "27"})
          LGA.Add(New LGA With {.LGAID = "556", .LGAName = "MUYA", .LGACode = "SRP", .StateCode = "27"})
          LGA.Add(New LGA With {.LGAID = "557", .LGAName = "PAIKORO", .LGACode = "PAK", .StateCode = "27"})
          LGA.Add(New LGA With {.LGAID = "558", .LGAName = "RAFI", .LGACode = "KAG", .StateCode = "27"})
          LGA.Add(New LGA With {.LGAID = "559", .LGAName = "RIJAU", .LGACode = "RJA", .StateCode = "27"})
          LGA.Add(New LGA With {.LGAID = "560", .LGAName = "SHIRORO", .LGACode = "KUT", .StateCode = "27"})
          LGA.Add(New LGA With {.LGAID = "561", .LGAName = "SULEJA", .LGACode = "SUL", .StateCode = "27"})
          LGA.Add(New LGA With {.LGAID = "562", .LGAName = "TAFA", .LGACode = "WSE", .StateCode = "27"})
          LGA.Add(New LGA With {.LGAID = "563", .LGAName = "WUSHISHI", .LGACode = "WSH", .StateCode = "27"})
          LGA.Add(New LGA With {.LGAID = "564", .LGAName = "ABEOKUTA-NORTH", .LGACode = "AKM", .StateCode = "28"})
          LGA.Add(New LGA With {.LGAID = "565", .LGAName = "ABEOKUTA-SOUTH", .LGACode = "AAB", .StateCode = "28"})
          LGA.Add(New LGA With {.LGAID = "566", .LGAName = "ADO-ODO/OTA", .LGACode = "OTA", .StateCode = "28"})
          LGA.Add(New LGA With {.LGAID = "567", .LGAName = "EGBADO-NORTH", .LGACode = "AYE", .StateCode = "28"})
          LGA.Add(New LGA With {.LGAID = "568", .LGAName = "EGBADO-SOUTH", .LGACode = "LAR", .StateCode = "28"})
          LGA.Add(New LGA With {.LGAID = "569", .LGAName = "EWEKORO", .LGACode = "TRE", .StateCode = "28"})
          LGA.Add(New LGA With {.LGAID = "570", .LGAName = "IFO", .LGACode = "FFF", .StateCode = "28"})
          LGA.Add(New LGA With {.LGAID = "571", .LGAName = "IJEBU-EAST", .LGACode = "GBE", .StateCode = "28"})
          LGA.Add(New LGA With {.LGAID = "572", .LGAName = "IJEBU-NORTH", .LGACode = "JGB", .StateCode = "28"})
          LGA.Add(New LGA With {.LGAID = "573", .LGAName = "IJEBU-NORTH-EAST", .LGACode = "JNE", .StateCode = "28"})
          LGA.Add(New LGA With {.LGAID = "574", .LGAName = "IJEBU-ODE", .LGACode = "JBD", .StateCode = "28"})
          LGA.Add(New LGA With {.LGAID = "575", .LGAName = "IKENNE", .LGACode = "KNN", .StateCode = "28"})
          LGA.Add(New LGA With {.LGAID = "576", .LGAName = "IMEKO-AFON", .LGACode = "MEK", .StateCode = "28"})
          LGA.Add(New LGA With {.LGAID = "577", .LGAName = "IPOKIA", .LGACode = "PKA", .StateCode = "28"})
          LGA.Add(New LGA With {.LGAID = "578", .LGAName = "OBAFEMI-OWODE", .LGACode = "WDE", .StateCode = "28"})
          LGA.Add(New LGA With {.LGAID = "579", .LGAName = "ODEDAH", .LGACode = "DED", .StateCode = "28"})
          LGA.Add(New LGA With {.LGAID = "580", .LGAName = "ODOGBOLU", .LGACode = "DGB", .StateCode = "28"})
          LGA.Add(New LGA With {.LGAID = "581", .LGAName = "OGUN-WATERSIDE", .LGACode = "ABG", .StateCode = "28"})
          LGA.Add(New LGA With {.LGAID = "582", .LGAName = "REMO-NORTH", .LGACode = "JRM", .StateCode = "28"})
          LGA.Add(New LGA With {.LGAID = "583", .LGAName = "SHAGAMU", .LGACode = "SMG", .StateCode = "28"})
          LGA.Add(New LGA With {.LGAID = "584", .LGAName = "AKOKO SOUTH", .LGACode = "SUA", .StateCode = "29"})
          LGA.Add(New LGA With {.LGAID = "585", .LGAName = "AKOKO-NORTH", .LGACode = "KAK", .StateCode = "29"})
          LGA.Add(New LGA With {.LGAID = "586", .LGAName = "AKOKO-NORTH-WEST", .LGACode = "ANG", .StateCode = "29"})
          LGA.Add(New LGA With {.LGAID = "587", .LGAName = "AKOKO-SOUTH-EAST", .LGACode = "KAA", .StateCode = "29"})
          LGA.Add(New LGA With {.LGAID = "588", .LGAName = "AKURE-NORTH", .LGACode = "AKR", .StateCode = "29"})
          LGA.Add(New LGA With {.LGAID = "589", .LGAName = "AKURE-SOUTH", .LGACode = "JTA", .StateCode = "29"})
          LGA.Add(New LGA With {.LGAID = "590", .LGAName = "ESE-ODO", .LGACode = "GKB", .StateCode = "29"})
          LGA.Add(New LGA With {.LGAID = "591", .LGAName = "IDANRE", .LGACode = "WEN", .StateCode = "29"})
          LGA.Add(New LGA With {.LGAID = "592", .LGAName = "IFEDORE", .LGACode = "FGB", .StateCode = "29"})
          LGA.Add(New LGA With {.LGAID = "593", .LGAName = "ILAJE", .LGACode = "GBA", .StateCode = "29"})
          LGA.Add(New LGA With {.LGAID = "594", .LGAName = "ILE-OLUJI-OKEIGBO", .LGACode = "LEL", .StateCode = "29"})
          LGA.Add(New LGA With {.LGAID = "595", .LGAName = "IRELE", .LGACode = "REL", .StateCode = "29"})
          LGA.Add(New LGA With {.LGAID = "596", .LGAName = "ODIGBO", .LGACode = "REE", .StateCode = "29"})
          LGA.Add(New LGA With {.LGAID = "597", .LGAName = "OKITI-PUPA", .LGACode = "KTP", .StateCode = "29"})
          LGA.Add(New LGA With {.LGAID = "598", .LGAName = "ONDO WEST", .LGACode = "NND", .StateCode = "29"})
          LGA.Add(New LGA With {.LGAID = "599", .LGAName = "ONDO-EAST", .LGACode = "BDR", .StateCode = "29"})
          LGA.Add(New LGA With {.LGAID = "600", .LGAName = "OSE", .LGACode = "FFN", .StateCode = "29"})
          LGA.Add(New LGA With {.LGAID = "601", .LGAName = "OWO", .LGACode = "WWW", .StateCode = "29"})
          LGA.Add(New LGA With {.LGAID = "602", .LGAName = "ATAKUMOSA", .LGACode = "SSU", .StateCode = "30"})
          LGA.Add(New LGA With {.LGAID = "603", .LGAName = "ATAKUMOSA EAST", .LGACode = "PRN", .StateCode = "30"})
          LGA.Add(New LGA With {.LGAID = "604", .LGAName = "AYEDA-ADE", .LGACode = "GBN", .StateCode = "30"})
          LGA.Add(New LGA With {.LGAID = "605", .LGAName = "AYEDIRE", .LGACode = "LGB", .StateCode = "30"})
          LGA.Add(New LGA With {.LGAID = "606", .LGAName = "BOLUWADURO", .LGACode = "TAN", .StateCode = "30"})
          LGA.Add(New LGA With {.LGAID = "607", .LGAName = "BORIPE", .LGACode = "RGB", .StateCode = "30"})
          LGA.Add(New LGA With {.LGAID = "608", .LGAName = "EDE", .LGACode = "EDE", .StateCode = "30"})
          LGA.Add(New LGA With {.LGAID = "609", .LGAName = "EDE NORTH", .LGACode = "EDT", .StateCode = "30"})
          LGA.Add(New LGA With {.LGAID = "610", .LGAName = "EGBEDORE", .LGACode = "AAW", .StateCode = "30"})
          LGA.Add(New LGA With {.LGAID = "611", .LGAName = "EJIGBO", .LGACode = "EJG", .StateCode = "30"})
          LGA.Add(New LGA With {.LGAID = "612", .LGAName = "IFE NORTH", .LGACode = "PMD", .StateCode = "30"})
          LGA.Add(New LGA With {.LGAID = "613", .LGAName = "IFE SOUTH", .LGACode = "FTD", .StateCode = "30"})
          LGA.Add(New LGA With {.LGAID = "614", .LGAName = "IFE-CENTRAL", .LGACode = "FFE", .StateCode = "30"})
          LGA.Add(New LGA With {.LGAID = "615", .LGAName = "IFEDAYO", .LGACode = "FDY", .StateCode = "30"})
          LGA.Add(New LGA With {.LGAID = "616", .LGAName = "IFE-EAST", .LGACode = "FEE", .StateCode = "30"})
          LGA.Add(New LGA With {.LGAID = "617", .LGAName = "IFELODUN", .LGACode = "KNR", .StateCode = "30"})
          LGA.Add(New LGA With {.LGAID = "618", .LGAName = "ILA", .LGACode = "LRG", .StateCode = "30"})
          LGA.Add(New LGA With {.LGAID = "619", .LGAName = "ILESA-EAST", .LGACode = "LES", .StateCode = "30"})
          LGA.Add(New LGA With {.LGAID = "620", .LGAName = "ILESA-WEST", .LGACode = "LEW", .StateCode = "30"})
          LGA.Add(New LGA With {.LGAID = "621", .LGAName = "IREPODUN", .LGACode = "RLG", .StateCode = "30"})
          LGA.Add(New LGA With {.LGAID = "622", .LGAName = "IREWOLE", .LGACode = "KRE", .StateCode = "30"})
          LGA.Add(New LGA With {.LGAID = "623", .LGAName = "ISOKAN", .LGACode = "APM", .StateCode = "30"})
          LGA.Add(New LGA With {.LGAID = "624", .LGAName = "IWO", .LGACode = "WWD", .StateCode = "30"})
          LGA.Add(New LGA With {.LGAID = "625", .LGAName = "OBOKUN", .LGACode = "BKN", .StateCode = "30"})
          LGA.Add(New LGA With {.LGAID = "626", .LGAName = "ODO-OTIN", .LGACode = "DTN", .StateCode = "30"})
          LGA.Add(New LGA With {.LGAID = "627", .LGAName = "OLA OLUWA", .LGACode = "BDS", .StateCode = "30"})
          LGA.Add(New LGA With {.LGAID = "628", .LGAName = "OLORUNDA", .LGACode = "GNN", .StateCode = "30"})
          LGA.Add(New LGA With {.LGAID = "629", .LGAName = "ORI-ADE", .LGACode = "JJS", .StateCode = "30"})
          LGA.Add(New LGA With {.LGAID = "630", .LGAName = "OROLU", .LGACode = "FNN", .StateCode = "30"})
          LGA.Add(New LGA With {.LGAID = "631", .LGAName = "OSOGBO", .LGACode = "SGB", .StateCode = "30"})
          LGA.Add(New LGA With {.LGAID = "632", .LGAName = "AFIJIO", .LGACode = "JBL", .StateCode = "31"})
          LGA.Add(New LGA With {.LGAID = "633", .LGAName = "AKINYELE", .LGACode = "MNY", .StateCode = "31"})
          LGA.Add(New LGA With {.LGAID = "634", .LGAName = "ATIBA", .LGACode = "FMT", .StateCode = "31"})
          LGA.Add(New LGA With {.LGAID = "635", .LGAName = "ATIGBO", .LGACode = "TDE", .StateCode = "31"})
          LGA.Add(New LGA With {.LGAID = "636", .LGAName = "EGBEDA", .LGACode = "EGB", .StateCode = "31"})
          LGA.Add(New LGA With {.LGAID = "637", .LGAName = "IBADAN-NORTH", .LGACode = "BDJ", .StateCode = "31"})
          LGA.Add(New LGA With {.LGAID = "638", .LGAName = "IBADAN-NORTH-EAST", .LGACode = "AGG", .StateCode = "31"})
          LGA.Add(New LGA With {.LGAID = "639", .LGAName = "IBADAN-NORTH-WEST", .LGACode = "NRK", .StateCode = "31"})
          LGA.Add(New LGA With {.LGAID = "640", .LGAName = "IBADAN-SOUTH-EAST", .LGACode = "MAP", .StateCode = "31"})
          LGA.Add(New LGA With {.LGAID = "641", .LGAName = "IBADAN-SOUTH-WEST", .LGACode = "LUY", .StateCode = "31"})
          LGA.Add(New LGA With {.LGAID = "642", .LGAName = "IBARAPA-CENTRAL", .LGACode = "RUW", .StateCode = "31"})
          LGA.Add(New LGA With {.LGAID = "643", .LGAName = "IBARAPA-EAST", .LGACode = "AYT", .StateCode = "31"})
          LGA.Add(New LGA With {.LGAID = "644", .LGAName = "IBARAPA-NORTH", .LGACode = "IRP", .StateCode = "31"})
          LGA.Add(New LGA With {.LGAID = "645", .LGAName = "IDO", .LGACode = "DDA", .StateCode = "31"})
          LGA.Add(New LGA With {.LGAID = "646", .LGAName = "IREPO", .LGACode = "KSH", .StateCode = "31"})
          LGA.Add(New LGA With {.LGAID = "647", .LGAName = "ISEYIN", .LGACode = "SEY", .StateCode = "31"})
          LGA.Add(New LGA With {.LGAID = "648", .LGAName = "ITESIWAJU", .LGACode = "TUT", .StateCode = "31"})
          LGA.Add(New LGA With {.LGAID = "649", .LGAName = "IWAJOWA", .LGACode = "WEL", .StateCode = "31"})
          LGA.Add(New LGA With {.LGAID = "650", .LGAName = "KAJOLA", .LGACode = "KEH", .StateCode = "31"})
          LGA.Add(New LGA With {.LGAID = "651", .LGAName = "LAGELU", .LGACode = "YNF", .StateCode = "31"})
          LGA.Add(New LGA With {.LGAID = "652", .LGAName = "OGBOMOSO-NORTH", .LGACode = "KNH", .StateCode = "31"})
          LGA.Add(New LGA With {.LGAID = "653", .LGAName = "OGBOMOSO-SOUTH", .LGACode = "AME", .StateCode = "31"})
          LGA.Add(New LGA With {.LGAID = "654", .LGAName = "OGO-OLUWA", .LGACode = "AJW", .StateCode = "31"})
          LGA.Add(New LGA With {.LGAID = "655", .LGAName = "OLORUNSOGO", .LGACode = "GBY", .StateCode = "31"})
          LGA.Add(New LGA With {.LGAID = "656", .LGAName = "OLUYOLE", .LGACode = "YRE", .StateCode = "31"})
          LGA.Add(New LGA With {.LGAID = "657", .LGAName = "ONA-ARA", .LGACode = "AKN", .StateCode = "31"})
          LGA.Add(New LGA With {.LGAID = "658", .LGAName = "ORELOPE", .LGACode = "GBH", .StateCode = "31"})
          LGA.Add(New LGA With {.LGAID = "659", .LGAName = "ORI-IRE", .LGACode = "KKY", .StateCode = "31"})
          LGA.Add(New LGA With {.LGAID = "660", .LGAName = "OYO", .LGACode = "JND", .StateCode = "31"})
          LGA.Add(New LGA With {.LGAID = "661", .LGAName = "OYO-EAST", .LGACode = "YYY", .StateCode = "31"})
          LGA.Add(New LGA With {.LGAID = "662", .LGAName = "SAKI-EAST", .LGACode = "GMD", .StateCode = "31"})
          LGA.Add(New LGA With {.LGAID = "663", .LGAName = "SAKI-WEST", .LGACode = "SHK", .StateCode = "31"})
          LGA.Add(New LGA With {.LGAID = "664", .LGAName = "SURULERE", .LGACode = "RSD", .StateCode = "31"})
          LGA.Add(New LGA With {.LGAID = "665", .LGAName = "BARKIN-LADI", .LGACode = "BLD", .StateCode = "32"})
          LGA.Add(New LGA With {.LGAID = "666", .LGAName = "BASSA", .LGACode = "BSA", .StateCode = "32"})
          LGA.Add(New LGA With {.LGAID = "667", .LGAName = "BOKKOS", .LGACode = "BKK", .StateCode = "32"})
          LGA.Add(New LGA With {.LGAID = "668", .LGAName = "JOS-EAST", .LGACode = "ANW", .StateCode = "32"})
          LGA.Add(New LGA With {.LGAID = "669", .LGAName = "JOS-NORTH", .LGACode = "JJN", .StateCode = "32"})
          LGA.Add(New LGA With {.LGAID = "670", .LGAName = "JOS-SOUTH", .LGACode = "BUU", .StateCode = "32"})
          LGA.Add(New LGA With {.LGAID = "671", .LGAName = "KANAM", .LGACode = "DNG", .StateCode = "32"})
          LGA.Add(New LGA With {.LGAID = "672", .LGAName = "KANKE", .LGACode = "KWK", .StateCode = "32"})
          LGA.Add(New LGA With {.LGAID = "673", .LGAName = "LANGTANG-NORTH", .LGACode = "LGT", .StateCode = "32"})
          LGA.Add(New LGA With {.LGAID = "674", .LGAName = "LANGTANG-SOUTH", .LGACode = "MBD", .StateCode = "32"})
          LGA.Add(New LGA With {.LGAID = "675", .LGAName = "MANGU", .LGACode = "MGU", .StateCode = "32"})
          LGA.Add(New LGA With {.LGAID = "676", .LGAName = "MIKANG", .LGACode = "TNK", .StateCode = "32"})
          LGA.Add(New LGA With {.LGAID = "677", .LGAName = "PANKSHIN", .LGACode = "PKN", .StateCode = "32"})
          LGA.Add(New LGA With {.LGAID = "678", .LGAName = "QUAN ANPAN", .LGACode = "QAP", .StateCode = "32"})
          LGA.Add(New LGA With {.LGAID = "679", .LGAName = "RIYOM", .LGACode = "RYM", .StateCode = "32"})
          LGA.Add(New LGA With {.LGAID = "680", .LGAName = "SHENDAM", .LGACode = "SHD", .StateCode = "32"})
          LGA.Add(New LGA With {.LGAID = "681", .LGAName = "WASE", .LGACode = "WAS", .StateCode = "32"})
          LGA.Add(New LGA With {.LGAID = "682", .LGAName = "ABOA/ODUAL", .LGACode = "ABU", .StateCode = "33"})
          LGA.Add(New LGA With {.LGAID = "683", .LGAName = "AHOADA-EAST", .LGACode = "AHD", .StateCode = "33"})
          LGA.Add(New LGA With {.LGAID = "684", .LGAName = "AHOADA-WEST", .LGACode = "KNM", .StateCode = "33"})
          LGA.Add(New LGA With {.LGAID = "685", .LGAName = "AKUKUTORU", .LGACode = "ABM", .StateCode = "33"})
          LGA.Add(New LGA With {.LGAID = "686", .LGAName = "ANDONI", .LGACode = "NDN", .StateCode = "33"})
          LGA.Add(New LGA With {.LGAID = "687", .LGAName = "ASARI-TORU", .LGACode = "BGM", .StateCode = "33"})
          LGA.Add(New LGA With {.LGAID = "688", .LGAName = "BONNY", .LGACode = "BNY", .StateCode = "33"})
          LGA.Add(New LGA With {.LGAID = "689", .LGAName = "DEGEMA", .LGACode = "DEG", .StateCode = "33"})
          LGA.Add(New LGA With {.LGAID = "690", .LGAName = "ELEME", .LGACode = "NCH", .StateCode = "33"})
          LGA.Add(New LGA With {.LGAID = "691", .LGAName = "EMUOHA", .LGACode = "MHA", .StateCode = "33"})
          LGA.Add(New LGA With {.LGAID = "692", .LGAName = "ETCHE", .LGACode = "KHE", .StateCode = "33"})
          LGA.Add(New LGA With {.LGAID = "693", .LGAName = "GOKANA", .LGACode = "KPR", .StateCode = "33"})
          LGA.Add(New LGA With {.LGAID = "694", .LGAName = "IKWERRE", .LGACode = "SKP", .StateCode = "33"})
          LGA.Add(New LGA With {.LGAID = "695", .LGAName = "KHANA", .LGACode = "BRR", .StateCode = "33"})
          LGA.Add(New LGA With {.LGAID = "696", .LGAName = "OBIO/AKPOR", .LGACode = "RUM", .StateCode = "33"})
          LGA.Add(New LGA With {.LGAID = "697", .LGAName = "OGBA-EGBEMA-NDONI", .LGACode = "RGM", .StateCode = "33"})
          LGA.Add(New LGA With {.LGAID = "698", .LGAName = "OGU/BOLO", .LGACode = "GGU", .StateCode = "33"})
          LGA.Add(New LGA With {.LGAID = "699", .LGAName = "OKIRIKA", .LGACode = "KRK", .StateCode = "33"})
          LGA.Add(New LGA With {.LGAID = "700", .LGAName = "OMUMA", .LGACode = "BER", .StateCode = "33"})
          LGA.Add(New LGA With {.LGAID = "701", .LGAName = "OPOBO/NKORO", .LGACode = "PBT", .StateCode = "33"})
          LGA.Add(New LGA With {.LGAID = "702", .LGAName = "OYIGBO", .LGACode = "AFM", .StateCode = "33"})
          LGA.Add(New LGA With {.LGAID = "703", .LGAName = "PORT-HARCOURT", .LGACode = "PHC", .StateCode = "33"})
          LGA.Add(New LGA With {.LGAID = "704", .LGAName = "TAI", .LGACode = "SKN", .StateCode = "33"})
          LGA.Add(New LGA With {.LGAID = "705", .LGAName = "BINJI", .LGACode = "BJN", .StateCode = "34"})
          LGA.Add(New LGA With {.LGAID = "706", .LGAName = "BODINGA", .LGACode = "DBN", .StateCode = "34"})
          LGA.Add(New LGA With {.LGAID = "707", .LGAName = "DANGE-SHUNI", .LGACode = "DGS", .StateCode = "34"})
          LGA.Add(New LGA With {.LGAID = "708", .LGAName = "GADA", .LGACode = "GAD", .StateCode = "34"})
          LGA.Add(New LGA With {.LGAID = "709", .LGAName = "GORONYO", .LGACode = "GRY", .StateCode = "34"})
          LGA.Add(New LGA With {.LGAID = "710", .LGAName = "GUDU", .LGACode = "BLE", .StateCode = "34"})
          LGA.Add(New LGA With {.LGAID = "711", .LGAName = "GWADABAWA", .LGACode = "GWD", .StateCode = "34"})
          LGA.Add(New LGA With {.LGAID = "712", .LGAName = "ILLELA", .LGACode = "LLA", .StateCode = "34"})
          LGA.Add(New LGA With {.LGAID = "713", .LGAName = "ISA", .LGACode = "SAA", .StateCode = "34"})
          LGA.Add(New LGA With {.LGAID = "714", .LGAName = "KEBBE", .LGACode = "KBE", .StateCode = "34"})
          LGA.Add(New LGA With {.LGAID = "715", .LGAName = "KWARE", .LGACode = "KWR", .StateCode = "34"})
          LGA.Add(New LGA With {.LGAID = "716", .LGAName = "RABAH", .LGACode = "RBA", .StateCode = "34"})
          LGA.Add(New LGA With {.LGAID = "717", .LGAName = "SABON-BIRNI", .LGACode = "SBN", .StateCode = "34"})
          LGA.Add(New LGA With {.LGAID = "718", .LGAName = "SHAGARI", .LGACode = "SGR", .StateCode = "34"})
          LGA.Add(New LGA With {.LGAID = "719", .LGAName = "SILAME", .LGACode = "SLM", .StateCode = "34"})
          LGA.Add(New LGA With {.LGAID = "720", .LGAName = "SOKOTO-NORTH", .LGACode = "SKK", .StateCode = "34"})
          LGA.Add(New LGA With {.LGAID = "721", .LGAName = "SOKOTO-SOUTH", .LGACode = "SRZ", .StateCode = "34"})
          LGA.Add(New LGA With {.LGAID = "722", .LGAName = "TAMBAWAL", .LGACode = "TBW", .StateCode = "34"})
          LGA.Add(New LGA With {.LGAID = "723", .LGAName = "TANGAZA", .LGACode = "TGZ", .StateCode = "34"})
          LGA.Add(New LGA With {.LGAID = "724", .LGAName = "TURETA", .LGACode = "TRT", .StateCode = "34"})
          LGA.Add(New LGA With {.LGAID = "725", .LGAName = "WAMAKKO", .LGACode = "WMK", .StateCode = "34"})
          LGA.Add(New LGA With {.LGAID = "726", .LGAName = "WURNO", .LGACode = "WRN", .StateCode = "34"})
          LGA.Add(New LGA With {.LGAID = "727", .LGAName = "YABO", .LGACode = "YYB", .StateCode = "34"})
          LGA.Add(New LGA With {.LGAID = "728", .LGAName = "ARDO-KOLA", .LGACode = "ARD", .StateCode = "35"})
          LGA.Add(New LGA With {.LGAID = "729", .LGAName = "BALI", .LGACode = "BAL", .StateCode = "35"})
          LGA.Add(New LGA With {.LGAID = "730", .LGAName = "DONGA", .LGACode = "DGA", .StateCode = "35"})
          LGA.Add(New LGA With {.LGAID = "731", .LGAName = "GASHAKA", .LGACode = "GKA", .StateCode = "35"})
          LGA.Add(New LGA With {.LGAID = "732", .LGAName = "GASSOL", .LGACode = "GAS", .StateCode = "35"})
          LGA.Add(New LGA With {.LGAID = "733", .LGAName = "IBI", .LGACode = "BBB", .StateCode = "35"})
          LGA.Add(New LGA With {.LGAID = "734", .LGAName = "JALINGO", .LGACode = "JAL", .StateCode = "35"})
          LGA.Add(New LGA With {.LGAID = "735", .LGAName = "KARIM-LAMIDO", .LGACode = "KLD", .StateCode = "35"})
          LGA.Add(New LGA With {.LGAID = "736", .LGAName = "KURMI", .LGACode = "KRM", .StateCode = "35"})
          LGA.Add(New LGA With {.LGAID = "737", .LGAName = "LAU", .LGACode = "LAU", .StateCode = "35"})
          LGA.Add(New LGA With {.LGAID = "738", .LGAName = "SARDAUNA", .LGACode = "SDA", .StateCode = "35"})
          LGA.Add(New LGA With {.LGAID = "739", .LGAName = "TAKUM", .LGACode = "TTM", .StateCode = "35"})
          LGA.Add(New LGA With {.LGAID = "740", .LGAName = "USSA", .LGACode = "USS", .StateCode = "35"})
          LGA.Add(New LGA With {.LGAID = "741", .LGAName = "WUKARI", .LGACode = "WKR", .StateCode = "35"})
          LGA.Add(New LGA With {.LGAID = "742", .LGAName = "YORRO", .LGACode = "YRR", .StateCode = "35"})
          LGA.Add(New LGA With {.LGAID = "743", .LGAName = "ZING", .LGACode = "TZG", .StateCode = "35"})
          LGA.Add(New LGA With {.LGAID = "744", .LGAName = "BADE", .LGACode = "GSH", .StateCode = "36"})
          LGA.Add(New LGA With {.LGAID = "745", .LGAName = "BOSARI", .LGACode = "DPH", .StateCode = "36"})
          LGA.Add(New LGA With {.LGAID = "746", .LGAName = "DAMATURU", .LGACode = "DTR", .StateCode = "36"})
          LGA.Add(New LGA With {.LGAID = "747", .LGAName = "FIKA", .LGACode = "FKA", .StateCode = "36"})
          LGA.Add(New LGA With {.LGAID = "748", .LGAName = "FUNE", .LGACode = "FUN", .StateCode = "36"})
          LGA.Add(New LGA With {.LGAID = "749", .LGAName = "GEIDAM", .LGACode = "GDM", .StateCode = "36"})
          LGA.Add(New LGA With {.LGAID = "750", .LGAName = "GUJBA", .LGACode = "GJB", .StateCode = "36"})
          LGA.Add(New LGA With {.LGAID = "751", .LGAName = "GULANI", .LGACode = "GLN", .StateCode = "36"})
          LGA.Add(New LGA With {.LGAID = "752", .LGAName = "JAKUSKO", .LGACode = "JAK", .StateCode = "36"})
          LGA.Add(New LGA With {.LGAID = "753", .LGAName = "KARASUWA", .LGACode = "KRS", .StateCode = "36"})
          LGA.Add(New LGA With {.LGAID = "754", .LGAName = "MACHINA", .LGACode = "MCN", .StateCode = "36"})
          LGA.Add(New LGA With {.LGAID = "755", .LGAName = "NANGERE", .LGACode = "NNR", .StateCode = "36"})
          LGA.Add(New LGA With {.LGAID = "756", .LGAName = "NGURU", .LGACode = "NGU", .StateCode = "36"})
          LGA.Add(New LGA With {.LGAID = "757", .LGAName = "POTISKUM", .LGACode = "PKM", .StateCode = "36"})
          LGA.Add(New LGA With {.LGAID = "758", .LGAName = "TARMUA", .LGACode = "TMW", .StateCode = "36"})
          LGA.Add(New LGA With {.LGAID = "759", .LGAName = "YUNUSARI", .LGACode = "YUN", .StateCode = "36"})
          LGA.Add(New LGA With {.LGAID = "760", .LGAName = "YUSUFARI", .LGACode = "YSF", .StateCode = "36"})
          LGA.Add(New LGA With {.LGAID = "761", .LGAName = "ANKA", .LGACode = "ANK", .StateCode = "37"})
          LGA.Add(New LGA With {.LGAID = "762", .LGAName = "BAKURA", .LGACode = "BKA", .StateCode = "37"})
          LGA.Add(New LGA With {.LGAID = "763", .LGAName = "BIRNIN MAGAJI", .LGACode = "BMJ", .StateCode = "37"})
          LGA.Add(New LGA With {.LGAID = "764", .LGAName = "BUKKUYUM", .LGACode = "BKM", .StateCode = "37"})
          LGA.Add(New LGA With {.LGAID = "765", .LGAName = "BUNGUDU", .LGACode = "BUG", .StateCode = "37"})
          LGA.Add(New LGA With {.LGAID = "766", .LGAName = "GUMI", .LGACode = "GMM", .StateCode = "37"})
          LGA.Add(New LGA With {.LGAID = "767", .LGAName = "GUSAU", .LGACode = "GUS", .StateCode = "37"})
          LGA.Add(New LGA With {.LGAID = "768", .LGAName = "KAURA-NAMODA", .LGACode = "KRN", .StateCode = "37"})
          LGA.Add(New LGA With {.LGAID = "769", .LGAName = "MARADUN", .LGACode = "MRD", .StateCode = "37"})
          LGA.Add(New LGA With {.LGAID = "770", .LGAName = "MARU", .LGACode = "MRR", .StateCode = "37"})
          LGA.Add(New LGA With {.LGAID = "771", .LGAName = "SHINKAFI", .LGACode = "SKF", .StateCode = "37"})
          LGA.Add(New LGA With {.LGAID = "772", .LGAName = "TALATA-MAFARA", .LGACode = "TMA", .StateCode = "37"})
          LGA.Add(New LGA With {.LGAID = "773", .LGAName = "TSAFE", .LGACode = "TSF", .StateCode = "37"})
          LGA.Add(New LGA With {.LGAID = "774", .LGAName = "ZURMI", .LGACode = "ZRM", .StateCode = "37"})




         
     
         

         
         



          Return LGA


     End Function

     Public Function getLGA(statecode As String) As List(Of String)

          Dim lstLGA As New List(Of String)
          Dim mLGA = populateLGA()

          Dim query = From m In mLGA _
                      Where m.StateCode = statecode _
                      Select m

          For Each a As LGA In query
               lstLGA.Add(a.LGAName)
          Next
          Return lstLGA

     End Function

     Public Function getLGAID(LGAName As String) As String

          Dim mstate = populateLGA()
          Dim querys = From m In mstate _
                       Where m.LGAName = LGAName _
                       Select m.LGAID
          Return querys(0).ToString

     End Function

     Public Function getLGAName(LGAID As Integer) As String

		Try
			Dim mstate = populateLGA()
			Dim querys = From m In mstate _
					   Where m.LGAID = LGAID _
					   Select m.LGAName

			If Not IsNothing(querys) = True Then
				Return querys(0).ToString()
			Else
				Return ""
			End If


		Catch ex As Exception

		End Try

	End Function


End Class

Public Class OfficeLocation

     Dim LocationID As Integer
     Dim sID As Integer
     Dim lName As String

     Property StateID As Integer
          Get
               Return sID
          End Get
          Set(ByVal value As Integer)
               sID = value
          End Set
     End Property

     Property LPPFALocationID As Integer
          Get
               Return LocationID
          End Get
          Set(ByVal value As Integer)
               LocationID = value
          End Set
     End Property

     Property LocationName As String
          Get
               Return lName
          End Get
          Set(ByVal value As String)
               lName = value
          End Set
     End Property

     Public Function PopulateLPPFALocation() As List(Of OfficeLocation)

          Dim state As New List(Of OfficeLocation)


          state.Add(New OfficeLocation With {.LPPFALocationID = "1", .StateID = "19", .LocationName = "Zaria"})
          state.Add(New OfficeLocation With {.LPPFALocationID = "2", .StateID = "19", .LocationName = "Kaduna"})
          state.Add(New OfficeLocation With {.LPPFALocationID = "3", .StateID = "25", .LocationName = "Ikeja"})
          state.Add(New OfficeLocation With {.LPPFALocationID = "4", .StateID = "25", .LocationName = "Head Office"})
          state.Add(New OfficeLocation With {.LPPFALocationID = "5", .StateID = "28", .LocationName = "Abeokuta"})
          state.Add(New OfficeLocation With {.LPPFALocationID = "5", .StateID = "28", .LocationName = "Shagamu"})
          state.Add(New OfficeLocation With {.LPPFALocationID = "5", .StateID = "28", .LocationName = "Ota"})

          Return state

     End Function

     'Public Function getStateID(stateName As String) As String


     '     Dim mstate = PopulateStates()
     '     Dim querys = From m In mstate _
     '                  Where m.StateName = stateName _
     '                  Select m.StateCode

     '     Return querys(0).ToString

     'End Function

     Public Function getStateOfficeLocation(stateID As Integer) As List(Of String)

          Dim lstLocationName As New List(Of String)
          Dim mofficeLocations = PopulateLPPFALocation()

          Dim query = From m In mofficeLocations _
                      Select m Where m.StateID = stateID

          For Each a As OfficeLocation In query
               lstLocationName.Add(a.LocationName)
          Next
          Return lstLocationName

     End Function

End Class

Public Class StandingPaymentOrder

	Dim txtPIN As String
	Dim numPension As Double
	Dim txtRunBy As String
	Dim txtStatus As String
	Dim txtSIType As String
	Dim intMonthFor As Integer
	Dim intYearFor As Integer

	Property PIN As String
		Get
			Return txtPIN
		End Get
		Set(ByVal value As String)
			txtPIN = value
		End Set
	End Property

	Property PensionAmount As Double
		Get
			Return numPension
		End Get
		Set(ByVal value As Double)
			numPension = value
		End Set
	End Property

	Property RunBy As String
		Get
			Return txtRunBy
		End Get
		Set(ByVal value As String)
			txtRunBy = value
		End Set
	End Property

	Property Status As String
		Get
			Return txtStatus
		End Get
		Set(ByVal value As String)
			txtStatus = value
		End Set
	End Property

	Property SIType As String
		Get
			Return txtSIType
		End Get
		Set(ByVal value As String)
			txtSIType = value
		End Set
	End Property

	Property MonthFor As Integer
		Get
			Return intMonthFor
		End Get
		Set(ByVal value As Integer)
			intMonthFor = value
		End Set
	End Property

	Property YearFor As Integer
		Get
			Return intYearFor
		End Get
		Set(ByVal value As Integer)
			intYearFor = value
		End Set
	End Property

End Class

Public Class AVCDetails

	Dim txtApplicationCode As String
	Dim txtAVCProcessed As Double
	Dim txtNoTaxAVCProcessed As Double
	Dim txtAVCProcessedUnit As Decimal
	Dim txtNoTaxAVCProcessedUnit As Decimal
	Dim txtTotalAVCProcessed As Double
	Dim txtAvgPrice As Decimal
	Dim txtPaymentDate As Date
	Dim txtPaymentPrice As Decimal
	Dim txtCurrentValue As Double
	Dim txtTaxDeduction As Double
	Dim txtNetPayable As Double

	Property ApplicationCode As String
		Get
			Return txtApplicationCode
		End Get
		Set(ByVal value As String)
			txtApplicationCode = value
		End Set
	End Property

	Property NonTaxableAVCProcessedUnit As Decimal
		Get
			Return txtNoTaxAVCProcessedUnit
		End Get
		Set(ByVal value As Decimal)
			txtNoTaxAVCProcessedUnit = value
		End Set
	End Property

	Property TaxableAVCProcessedUnit As Decimal
		Get
			Return txtAVCProcessedUnit
		End Get
		Set(ByVal value As Decimal)
			txtAVCProcessedUnit = value
		End Set
	End Property

	Property TaxableProcessedAVC As Double
		Get
			Return txtAVCProcessed
		End Get
		Set(ByVal value As Double)
			txtAVCProcessed = value
		End Set
	End Property

	Property NonTaxableProcessedAVC As Double
		Get
			Return txtNoTaxAVCProcessed
		End Get
		Set(ByVal value As Double)
			txtNoTaxAVCProcessed = value
		End Set
	End Property

	Property TotalProcessedAVC As Double
		Get
			Return txtTotalAVCProcessed
		End Get
		Set(ByVal value As Double)
			txtTotalAVCProcessed = value
		End Set
	End Property

	Property AveragAVCPrice As Decimal
		Get
			Return txtAvgPrice
		End Get
		Set(ByVal value As Decimal)
			txtAvgPrice = value
		End Set
	End Property

	Property AVCPaymentDate As Date
		Get
			Return txtPaymentDate
		End Get
		Set(ByVal value As Date)
			txtPaymentDate = value
		End Set
	End Property

	Property AVCPaymentUnitPrice As Decimal
		Get
			Return txtPaymentPrice
		End Get
		Set(ByVal value As Decimal)
			txtPaymentPrice = value
		End Set
	End Property

	Property AVCCurrentValue As Double

		Get
			Return txtCurrentValue
		End Get
		Set(ByVal value As Double)
			txtCurrentValue = value
		End Set

	End Property

	Property AVCTaxDeduction As Double

		Get
			Return txtTaxDeduction
		End Get
		Set(ByVal value As Double)
			txtTaxDeduction = value
		End Set

	End Property

	Property AVCNetPayable As Double

		Get
			Return txtNetPayable
		End Get
		Set(ByVal value As Double)
			txtNetPayable = value
		End Set

	End Property


End Class

Public Class RetirementDetails

     Dim txtApplicationCode As String
     Dim numBasicSalary As Double
     Dim numHouseRent As Double
     Dim numTransport As Double
     Dim numUtility As Double
     Dim numConsolidatedAallowance As Double
     Dim numConsolidatedSalary As Double
     Dim numMonthlyTotal As Double
     Dim numAnnualTotalEmolumentAdj As Decimal
     Dim numAccruedRight As Double
     Dim numRSABalance As Double
     Dim numMonthlyProgramedDrawndown As Double
     Dim dtePriceDate As Date
     Dim dteAnnuityCommencement As Date
     Dim txtInsuranceCoy As String
     Dim numPremium As Double
     Dim numAnnuityLumpSum As Double
     Dim numMonthlyAnnuity As Double
     Dim numRecommendedLumpSum As Double
     Dim boolPW As Boolean = False
     Dim boolAnnuity As Boolean = False
     Dim boolDb As Boolean = False
     Dim dteRetirement As Date
     Dim dteDeath As Date
     Dim txtAdminIssuingAuthority As String
     Dim txtAdminIssuingDate As Date
     Dim txtAdminNOK As String
     Dim numInsuranceProceed As Double
     Dim numContribution As Double
     Dim numInvestmentIncome As Double
     Dim txtRemarks As String
     Property IsDeathBenefit As Boolean
          Get
               Return boolDb
          End Get
          Set(ByVal value As Boolean)
               boolDb = value
          End Set
     End Property
     Property RetirementDate As Date
          Get
               Return dteRetirement
          End Get
          Set(ByVal value As Date)
               dteRetirement = value
          End Set
     End Property

     Property DeathDate As Date
          Get
               Return dteDeath
          End Get
          Set(ByVal value As Date)
               dteDeath = value
          End Set
     End Property

     Property AdminIssuingAuthority As String
          Get
               Return txtAdminIssuingAuthority
          End Get
          Set(ByVal value As String)
               txtAdminIssuingAuthority = value
          End Set
     End Property

     Property AdminIssuingDate As Date
          Get
               Return txtAdminIssuingDate
          End Get
          Set(ByVal value As Date)
               txtAdminIssuingDate = value
          End Set
     End Property

     Property AdminNOK As String
          Get
               Return txtAdminNOK
          End Get
          Set(ByVal value As String)
               txtAdminNOK = value
          End Set
     End Property

     Property InsuranceProceed As Double
          Get
               Return numInsuranceProceed
          End Get
          Set(ByVal value As Double)
               numInsuranceProceed = value
          End Set
     End Property

     Property Contribution As Double
          Get
               Return numContribution
          End Get
          Set(ByVal value As Double)
               numContribution = value
          End Set
     End Property

     Property InvestmentIncome As Double
          Get
               Return numInvestmentIncome
          End Get
          Set(ByVal value As Double)
               numInvestmentIncome = value
          End Set
     End Property

     Property Remarks As String
          Get
               Return txtRemarks
          End Get
          Set(ByVal value As String)
               txtRemarks = value
          End Set
     End Property

     Property ApplicationCode As String
          Get
               Return txtApplicationCode
          End Get
          Set(ByVal value As String)
               txtApplicationCode = value
          End Set
     End Property
     Property BasicSalary As Double
          Get
               Return numBasicSalary
          End Get
          Set(ByVal value As Double)
               numBasicSalary = value
          End Set
     End Property
     Property HouseRent As Double
          Get
               Return numHouseRent
          End Get
          Set(ByVal value As Double)
               numHouseRent = value
          End Set
     End Property
     Property Transport As Double
          Get
               Return numTransport
          End Get
          Set(ByVal value As Double)
               numTransport = value
          End Set
     End Property
     Property Utility As Double
          Get
               Return numUtility
          End Get
          Set(ByVal value As Double)
               numUtility = value
          End Set
     End Property
     Property ConsolidatedAallowance As Double
          Get
               Return numConsolidatedAallowance
          End Get
          Set(ByVal value As Double)
               numConsolidatedAallowance = value
          End Set
     End Property
     Property ConsolidatedSalary As Double
          Get
               Return numConsolidatedSalary
          End Get
          Set(ByVal value As Double)
               numConsolidatedSalary = value
          End Set
     End Property
     Property MonthlyTotal As Double
          Get
               Return numMonthlyTotal
          End Get
          Set(ByVal value As Double)
               numMonthlyTotal = value
          End Set
     End Property
     Property AnnualTotalEmolumentAdj As Double
          Get
               Return numAnnualTotalEmolumentAdj
          End Get
          Set(ByVal value As Double)
               numAnnualTotalEmolumentAdj = value
          End Set
     End Property
     Property AccruedRight As Double

          Get
               Return numAccruedRight
          End Get
          Set(ByVal value As Double)
               numAccruedRight = value
          End Set

     End Property
     Property RSABalance As Double

          Get
               Return numRSABalance
          End Get
          Set(ByVal value As Double)
               numRSABalance = value
          End Set

     End Property
     Property MonthlyProgramedDrawndown As Double
          Get
               Return numMonthlyProgramedDrawndown
          End Get
          Set(ByVal value As Double)
               numMonthlyProgramedDrawndown = value
          End Set

     End Property
     Property PriceDate As Date
          Get
               Return dtePriceDate
          End Get
          Set(ByVal value As Date)
               dtePriceDate = value
          End Set

     End Property
     Property AnnuityCommencement As Date
          Get
               Return dteAnnuityCommencement
          End Get
          Set(ByVal value As Date)
               dteAnnuityCommencement = value
          End Set

     End Property
     Property InsuranceCoy As String
          Get
               Return txtInsuranceCoy
          End Get
          Set(ByVal value As String)
               txtInsuranceCoy = value
          End Set

     End Property
     Property Premium As Double

          Get
               Return numPremium
          End Get
          Set(ByVal value As Double)
               numPremium = value
          End Set

     End Property
     Property AnnuityLumpSum As Double

          Get
               Return numAnnuityLumpSum
          End Get
          Set(ByVal value As Double)
               numAnnuityLumpSum = value
          End Set

     End Property
     Property MonthlyAnnuity As Double

          Get
               Return numMonthlyAnnuity
          End Get
          Set(ByVal value As Double)
               numMonthlyAnnuity = value
          End Set

     End Property
     Property RecommendedLumpSum As Double

          Get
               Return numRecommendedLumpSum
          End Get
          Set(ByVal value As Double)
               numRecommendedLumpSum = value
          End Set

     End Property
     Property isProgramWithdrawal As Boolean

          Get
               Return boolPW
          End Get
          Set(ByVal value As Boolean)
               boolPW = value
          End Set

     End Property
     Property isAnnuity As Boolean

          Get
               Return boolAnnuity
          End Get
          Set(ByVal value As Boolean)
               boolAnnuity = value
          End Set

     End Property


End Class

Public Class ApprovalType
     Dim apptypeID As Integer
     Dim apptypeName As String



     Property ApprovalID As Integer
          Get
               Return apptypeID
          End Get
          Set(ByVal value As Integer)
               apptypeID = value
          End Set
     End Property

     Property ApprovalName As String
          Get
               Return apptypeName
          End Get
          Set(ByVal value As String)
               apptypeName = value
          End Set
     End Property

     Public Function populateApprovalType() As List(Of ApprovalType)


          Dim aType As New List(Of ApprovalType)

          aType.Add(New ApprovalType With {.ApprovalID = "1", .ApprovalName = "Enbloc"})
          aType.Add(New ApprovalType With {.ApprovalID = "2", .ApprovalName = "25% Lumpsum Withdrawal"})
          aType.Add(New ApprovalType With {.ApprovalID = "3", .ApprovalName = "Lump Sum/Program Withdrawal"})
          aType.Add(New ApprovalType With {.ApprovalID = "4", .ApprovalName = "Annuity"})
          aType.Add(New ApprovalType With {.ApprovalID = "5", .ApprovalName = "Death Benefit Payment"})
          aType.Add(New ApprovalType With {.ApprovalID = "6", .ApprovalName = "NSITF Payment"})
          aType.Add(New ApprovalType With {.ApprovalID = "7", .ApprovalName = "AVC"})
          aType.Add(New ApprovalType With {.ApprovalID = "8", .ApprovalName = "Legacy"})
          aType.Add(New ApprovalType With {.ApprovalID = "9", .ApprovalName = "Exit"})
          aType.Add(New ApprovalType With {.ApprovalID = "10", .ApprovalName = "Gratuity"})
          aType.Add(New ApprovalType With {.ApprovalID = "11", .ApprovalName = "Employee Portion"})
          aType.Add(New ApprovalType With {.ApprovalID = "12", .ApprovalName = "Accrued Rights"})
          aType.Add(New ApprovalType With {.ApprovalID = "13", .ApprovalName = "Grouplife"})
          aType.Add(New ApprovalType With {.ApprovalID = "14", .ApprovalName = "Add. Lump Sum Payment"})
          aType.Add(New ApprovalType With {.ApprovalID = "15", .ApprovalName = "Additional Annuity"})

          Return aType

     End Function

     Public Function getAppTypeID(AppTypeName As String) As String

          Dim lst As New List(Of String)
          Dim mApprovalType = populateApprovalType()

          Dim query = From m In populateApprovalType() _
                      Where m.ApprovalName = AppTypeName _
                      Select m.ApprovalID

          Return query(0).ToString

     End Function

     Public Function getApprovalTypes() As List(Of String)

          Dim lstAppTypes As New List(Of String)
          Dim mAppTypes = populateApprovalType()

          Dim query = From m In mAppTypes _
                      Select m

          For Each a As ApprovalType In query
               lstAppTypes.Add(a.ApprovalName)
          Next
          Return lstAppTypes

     End Function

End Class

Public Class ExitReasons
     Dim ReasontypeID As Integer
     Dim ReasontypeName As String
     Dim PaymentID As Integer

     Property PaymentTypeID As Integer
          Get
               Return PaymentID
          End Get
          Set(ByVal value As Integer)
               PaymentID = value
          End Set
     End Property

     Property ReasonID As Integer
          Get
               Return ReasontypeID
          End Get
          Set(ByVal value As Integer)
               ReasontypeID = value
          End Set
     End Property


     Property ReasonName As String
          Get
               Return ReasontypeName
          End Get
          Set(ByVal value As String)
               ReasontypeName = value
          End Set
     End Property
	'creating list of payment exit reason for different types of payments
     Public Function populateExitReasonType() As List(Of ExitReasons)


          Dim rType As New List(Of ExitReasons)

          rType.Add(New ExitReasons With {.ReasonID = "1", .ReasonName = "Mandatory Retirement", .PaymentTypeID = "2"})
          rType.Add(New ExitReasons With {.ReasonID = "2", .ReasonName = "Voluntary Retirement", .PaymentTypeID = "2"})
          rType.Add(New ExitReasons With {.ReasonID = "3", .ReasonName = "Resignation", .PaymentTypeID = "2"})
          rType.Add(New ExitReasons With {.ReasonID = "4", .ReasonName = "Job Change", .PaymentTypeID = "2"})
          rType.Add(New ExitReasons With {.ReasonID = "5", .ReasonName = "Missing Employee", .PaymentTypeID = "2"})
          rType.Add(New ExitReasons With {.ReasonID = "6", .ReasonName = "Death", .PaymentTypeID = "2"})

          rType.Add(New ExitReasons With {.ReasonID = "1", .ReasonName = "Retirement", .PaymentTypeID = "1"})
		rType.Add(New ExitReasons With {.ReasonID = "2", .ReasonName = "Relocation", .PaymentTypeID = "1"})

		rType.Add(New ExitReasons With {.ReasonID = "1", .ReasonName = "Retirement", .PaymentTypeID = "16"})
		rType.Add(New ExitReasons With {.ReasonID = "2", .ReasonName = "Relocation", .PaymentTypeID = "16"})

          rType.Add(New ExitReasons With {.ReasonID = "1", .ReasonName = "Retirement", .PaymentTypeID = "3"})
		rType.Add(New ExitReasons With {.ReasonID = "2", .ReasonName = "Relocation", .PaymentTypeID = "3"})

		rType.Add(New ExitReasons With {.ReasonID = "1", .ReasonName = "Retirement", .PaymentTypeID = "14"})
		rType.Add(New ExitReasons With {.ReasonID = "2", .ReasonName = "Relocation", .PaymentTypeID = "14"})

          rType.Add(New ExitReasons With {.ReasonID = "1", .ReasonName = "Retirement", .PaymentTypeID = "4"})
		rType.Add(New ExitReasons With {.ReasonID = "2", .ReasonName = "Relocation", .PaymentTypeID = "4"})

		rType.Add(New ExitReasons With {.ReasonID = "1", .ReasonName = "Retirement", .PaymentTypeID = "15"})
		rType.Add(New ExitReasons With {.ReasonID = "2", .ReasonName = "Relocation", .PaymentTypeID = "15"})

          Return rType

     End Function

     Public Function getExitReasonTypeID(rTypeName As String) As String

          Dim lst As New List(Of String)
          Dim mExitReasonsTypes = populateExitReasonType()

          Dim query = From m In mExitReasonsTypes _
                      Where m.ReasonName = rTypeName _
                      Select m.ReasonID

          Return query(0).ToString

     End Function

     Public Function getExitReasonsTypes(paymentType As Integer) As List(Of String)

          Dim lstExitReasonTypes As New List(Of String)
          Dim mExitReasonsTypes = populateExitReasonType()

          Dim query = From m In mExitReasonsTypes Where m.PaymentTypeID = paymentType _
                      Select m

          For Each a As ExitReasons In query
               lstExitReasonTypes.Add(a.ReasonName)
          Next
          Return lstExitReasonTypes

     End Function

End Class

Public Class ApplicationDetail

     Dim ObjRetirementDetails As RetirementDetails
     Dim intApprovalOrderID As Integer
     Dim numCurrentValue As Decimal
     Dim numApproved As Decimal
     Dim txtTitle As String
	Dim isSchGenerated As Integer
	Dim isAgeBarrierOverwitten As Integer
     Dim dteDOB As Date
     Dim txtEmployerCode As String
     Dim blnIsretirement As Boolean
     Dim txtApplicationID As String
     Dim fkiMemberID As Integer
     Dim fkiAppTypeId As Integer
     Dim txtSector As String
     Dim dteApplicationDate As Date
     Dim txtStatus As String
     Dim txtCreatedBy As String
     Dim dteDOR As Date
     Dim dteDisengagement As Date
     Dim txtSex As String
     Dim txtComment As String
     Dim txtApplicationState As String
     Dim txtApplicationOffice As String
     Dim numFinalSalary As Decimal
     Dim dteStatusChange As Date
     Dim IsRejected As Int32
     Dim IsDocCompleted As Int32
     Dim IsSentToPencom As Int32
     Dim dteSentToPencom As DateTime
     Dim txtPencomBatch As String
     Dim dteDocumentCompleted As DateTime
     Dim dteProcessing As DateTime
     Dim dteConfirmed As DateTime
     Dim dtePaid As DateTime
     Dim numRSABalance As Decimal
     Dim numLumpSum As Decimal
     Dim numMonthlyPW As Decimal
     Dim numPremium As Decimal
     Dim numAnnuity As Decimal
     Dim dteConfirmPriceDate As DateTime
     Dim txtReason As String
     Dim fkiEmployerID As Integer
     Dim txtDepartment As String
     Dim txtDesignation As String
     Dim txtInsuranceCompany As String
     Dim txtLastChangedPerson As String
     Dim txtAccountName As String
     Dim txtAccountNo As String
     Dim txtBVN As String
     Dim intBankID As Integer
     Dim intBranchID As Integer
     Dim txtCommentGroup As String
     Dim txtPIN As String
     Dim txtFullName As String
     Dim txtEmployerName As String
     Dim txtFundStatus As String
     Dim isPhototConfirmed As Integer
     Dim isSignConfirmed As Integer
     Dim txtApplicationTypeName As String
     Dim txtEmail As String
     Dim txtFileNumber As String
	Dim txtTIN As String
	Dim txtReferenceApplicationCode As String

     Dim txtPayingLumpSum As Double
     Dim txtPayingPension As Double
     Dim txtPayingArrears As Double
	Dim txtPayingAnnuity As Double
	Dim txtInterest As Double

	Dim dteARLAcknowledgment As Date
	Dim blnIsARLActRecieved As Boolean

	Property IsARLActRecieved As Boolean

		Get
			Return blnIsARLActRecieved
		End Get
		Set(ByVal value As Boolean)
			blnIsARLActRecieved = value
		End Set

	End Property

	Property ARLAcknowledgmentDate As Date
		Get
			Return dteARLAcknowledgment
		End Get
		Set(ByVal value As Date)
			dteARLAcknowledgment = value
		End Set
	End Property


	Property ReferenceApplicationCode As String
		Get
			Return txtReferenceApplicationCode
		End Get
		Set(ByVal value As String)
			txtReferenceApplicationCode = value
		End Set
	End Property

	Property InterestAmount As Double
		Get
			Return txtInterest
		End Get
		Set(ByVal value As Double)
			txtInterest = value
		End Set
	End Property

     Property PayingAnnuity As Double
          Get
               Return txtPayingAnnuity
          End Get
          Set(ByVal value As Double)
               txtPayingAnnuity = value
          End Set
     End Property

     Property PayingLumpSum As Double
          Get
               Return txtPayingLumpSum
          End Get
          Set(ByVal value As Double)
               txtPayingLumpSum = value
          End Set
     End Property

     Property PayingPension As Double
          Get
               Return txtPayingPension
          End Get
          Set(ByVal value As Double)
               txtPayingPension = value
          End Set
     End Property

     Property PayingArrears As Double
          Get
               Return txtPayingArrears
          End Get
          Set(ByVal value As Double)
               txtPayingArrears = value
          End Set
     End Property


     Property TIN As String
          Get
               Return txtTIN
          End Get
          Set(ByVal value As String)
               txtTIN = value
          End Set
     End Property

     Property FileNumber As String
          Get
               Return txtFileNumber
          End Get
          Set(ByVal value As String)
               txtFileNumber = value
          End Set
     End Property

     Property ApprovalOrderID As Integer
          Get
               Return intApprovalOrderID
          End Get
          Set(ByVal value As Integer)
               intApprovalOrderID = value
          End Set
     End Property

     Property RetirementDetails As RetirementDetails
          Get
               Return ObjRetirementDetails
          End Get
          Set(ByVal value As RetirementDetails)
               ObjRetirementDetails = value
          End Set
     End Property

     Property ApprovedAmount As Decimal
          Get
               Return numApproved
          End Get
          Set(ByVal value As Decimal)
               numApproved = value
          End Set
     End Property

     Property AmountToPay As Decimal
          Get
               Return numCurrentValue
          End Get
          Set(ByVal value As Decimal)
               numCurrentValue = value
          End Set
     End Property



     Property Email As String
          Get
               Return txtEmail
          End Get
          Set(ByVal value As String)
               txtEmail = value
          End Set
     End Property

     Property ApplicationTypeName As String
          Get
               Return txtApplicationTypeName
          End Get
          Set(ByVal value As String)
               txtApplicationTypeName = value
          End Set
     End Property


     Property Title As String
          Get
               Return txtTitle
          End Get
          Set(ByVal value As String)
               txtTitle = value
          End Set
     End Property

     Property isSignatureConfirmed As Integer
          Get
               Return isSignConfirmed
          End Get
          Set(ByVal value As Integer)
               isSignConfirmed = value
          End Set
     End Property


     Property IsPassportConfirmed As Integer
          Get
               Return isPhototConfirmed
          End Get
          Set(ByVal value As Integer)
               isPhototConfirmed = value
          End Set
     End Property

     Property IsScheduleGenerated As Integer
          Get
               Return isSchGenerated
          End Get
          Set(ByVal value As Integer)
               isSchGenerated = value
          End Set
	End Property
	Property AgeConstrainstOverwitten As Integer
		Get
			Return isAgeBarrierOverwitten
		End Get
		Set(ByVal value As Integer)
			isAgeBarrierOverwitten = value
		End Set
	End Property
	'


     Property FundStatus As String
          Get
               Return txtFundStatus
          End Get
          Set(ByVal value As String)
               txtFundStatus = value
          End Set
     End Property

     Property EmployerName As String
          Get
               Return txtEmployerName
          End Get
          Set(ByVal value As String)
               txtEmployerName = value
          End Set
     End Property

     Property DOB As Date
          Get
               Return dteDOB
          End Get
          Set(ByVal value As Date)
               dteDOB = value
          End Set
     End Property
     Property EmployerCode As String
          Get
               Return txtEmployerCode
          End Get
          Set(ByVal value As String)
               txtEmployerCode = value
          End Set
     End Property

     Property FullName As String
          Get
               Return txtFullName
          End Get
          Set(ByVal value As String)
               txtFullName = value
          End Set
     End Property

     Property PIN As String
          Get
               Return txtPIN
          End Get
          Set(ByVal value As String)
               txtPIN = value
          End Set
     End Property


     Property IsRetirement As Boolean
          Get
               Return blnIsretirement
          End Get
          Set(ByVal value As Boolean)
               blnIsretirement = value
          End Set
     End Property

     Property MemberID As Integer
          Get
               Return fkiMemberID
          End Get
          Set(ByVal value As Integer)
               fkiMemberID = value
          End Set
     End Property

     Property AppTypeId As Integer
          Get
               Return fkiAppTypeId
          End Get
          Set(ByVal value As Integer)
               fkiAppTypeId = value
          End Set
     End Property
     Property Sector As String
          Get
               Return txtSector
          End Get
          Set(ByVal value As String)
               txtSector = value
          End Set
     End Property
     Property ApplicationDate As Date
          Get
               Return dteApplicationDate
          End Get
          Set(ByVal value As Date)
               dteApplicationDate = value
          End Set
     End Property

     Property Status As String
          Get
               Return txtStatus
          End Get
          Set(ByVal value As String)
               txtStatus = value
          End Set
     End Property

     Property CreatedBy As String
          Get
               Return txtCreatedBy
          End Get
          Set(ByVal value As String)
               txtCreatedBy = value
          End Set
     End Property

     Property Sex As Char
          Get
               Return txtSex
          End Get
          Set(ByVal value As Char)
               txtSex = value
          End Set
     End Property

     Property DOR As Date
          Get
               Return dteDOR
          End Get
          Set(ByVal value As Date)
               dteDOR = value
          End Set
     End Property

     Property DateDisengagement As Date
          Get
               Return dteDisengagement
          End Get
          Set(ByVal value As Date)
               dteDisengagement = value
          End Set
     End Property
     Property Comment As String
          Get
               Return txtComment
          End Get
          Set(ByVal value As String)
               txtComment = value
          End Set
     End Property
     Property ApplicationState As String
          Get
               Return txtApplicationState
          End Get
          Set(ByVal value As String)
               txtApplicationState = value
          End Set
     End Property
     Property ApplicationOffice As String
          Get
               Return txtApplicationOffice
          End Get
          Set(ByVal value As String)
               txtApplicationOffice = value
          End Set
     End Property
     Property FinalSalary As Decimal
          Get
               Return numFinalSalary
          End Get
          Set(ByVal value As Decimal)
               numFinalSalary = value
          End Set
     End Property
     Property StatusChange As Date
          Get
               Return dteStatusChange
          End Get
          Set(ByVal value As Date)
               dteStatusChange = value
          End Set
     End Property
     Property Rejected As Int32
          Get
               Return IsRejected
          End Get
          Set(ByVal value As Int32)
               IsRejected = value
          End Set
     End Property

     Property DocCompleted As Int32
          Get
               Return IsDocCompleted
          End Get
          Set(ByVal value As Int32)
               IsDocCompleted = value
          End Set
     End Property

     Property SentToPencom As Int32
          Get
               Return IsSentToPencom
          End Get
          Set(ByVal value As Int32)
               IsSentToPencom = value
          End Set
     End Property
     Property DateSentToPencom As DateTime
          Get
               Return dteSentToPencom
          End Get
          Set(ByVal value As DateTime)
               dteSentToPencom = value
          End Set
     End Property
     Property PencomBatch As String
          Get
               Return txtPencomBatch
          End Get
          Set(ByVal value As String)
               txtPencomBatch = value
          End Set
     End Property

     Property DateDocumentCompleted As DateTime
          Get
               Return dteDocumentCompleted
          End Get
          Set(ByVal value As DateTime)
               dteDocumentCompleted = value
          End Set
     End Property

     Property DateProcessing As DateTime
          Get
               Return dteProcessing
          End Get
          Set(ByVal value As DateTime)
               dteProcessing = value
          End Set
     End Property
     Property DateConfirmed As DateTime
          Get
               Return dteConfirmed
          End Get
          Set(ByVal value As DateTime)
               dteConfirmed = value
          End Set
     End Property

     Property RSABalance As Decimal
          Get
               Return numRSABalance
          End Get
          Set(ByVal value As Decimal)
               numRSABalance = value
          End Set
     End Property
     Property NSITFInitialAmountPaid As Decimal
          Get
               Return numLumpSum
          End Get
          Set(ByVal value As Decimal)
               numLumpSum = value
          End Set
     End Property
     Property NSITFRecievedToRSA As Decimal
          Get
               Return numMonthlyPW
          End Get
          Set(ByVal value As Decimal)
               numMonthlyPW = value
          End Set
     End Property
     Property NSITFRequestedToRSA As Decimal
          Get
               Return numPremium
          End Get
          Set(ByVal value As Decimal)
               numPremium = value
          End Set
     End Property
     Property Annuity As Decimal
          Get
               Return numAnnuity
          End Get
          Set(ByVal value As Decimal)
               numAnnuity = value
          End Set
     End Property

     Property DateConfirmPriceDate As DateTime
          Get
               Return dteConfirmPriceDate
          End Get
          Set(ByVal value As DateTime)
               dteConfirmPriceDate = value
          End Set
     End Property

     Property Reason As String
          Get
               Return txtReason
          End Get
          Set(ByVal value As String)
               txtReason = value
          End Set
     End Property

     Property EmployerID As Integer
          Get
               Return fkiEmployerID
          End Get
          Set(ByVal value As Integer)
               fkiEmployerID = value
          End Set
     End Property

     Property Department As String
          Get
               Return txtDepartment
          End Get
          Set(ByVal value As String)
               txtDepartment = value
          End Set
     End Property

     Property Designation As String
          Get
               Return txtDesignation
          End Get
          Set(ByVal value As String)
               txtDesignation = value
          End Set
     End Property

     Property InsuranceCompany As String
          Get
               Return txtInsuranceCompany
          End Get
          Set(ByVal value As String)
               txtInsuranceCompany = value
          End Set
     End Property

     Property LastChangedPerson As String
          Get
               Return txtLastChangedPerson
          End Get
          Set(ByVal value As String)
               txtLastChangedPerson = value
          End Set
     End Property
     Property ApplicationID As String
          Get
               Return txtApplicationID
          End Get
          Set(ByVal value As String)
               txtApplicationID = value
          End Set
     End Property

     Property AccountName As String
          Get
               Return txtAccountName
          End Get
          Set(ByVal value As String)
               txtAccountName = value
          End Set
     End Property

     Property AccountNo As String
          Get
               Return txtAccountNo
          End Get
          Set(ByVal value As String)
               txtAccountNo = value
          End Set
     End Property

     Property BVN As String
          Get
               Return txtBVN
          End Get
          Set(ByVal value As String)
               txtBVN = value
          End Set
     End Property

     Property BankID As Integer
          Get
               Return intBankID
          End Get
          Set(ByVal value As Integer)
               intBankID = value
          End Set
     End Property

     Property BranchID As Integer
          Get
               Return intBranchID
          End Get
          Set(ByVal value As Integer)
               intBranchID = value
          End Set
     End Property

     Property CommentGroup As String
          Get
               Return txtCommentGroup
          End Get
          Set(ByVal value As String)
               txtCommentGroup = value
          End Set
     End Property

End Class

Public Class ApplicationCheckList
	Dim txtApplicationCode As String
	Dim isFundingStatusChecked As Integer
	Dim isLegAVCChecked As Integer
	Dim isDOBChecked As Integer

	Dim isNamesChecked As Integer
	Dim isExitDocChecked As Integer
	Dim isDataEntryChecked As Integer

	Dim isValidDocChecked As Integer

	Property DOBChecked As Integer
		Get
			Return isDOBChecked
		End Get
		Set(ByVal value As Integer)
			isDOBChecked = value
		End Set
	End Property

	Property ApplicationCode As String
		Get
			Return txtApplicationCode
		End Get
		Set(ByVal value As String)
			txtApplicationCode = value
		End Set
	End Property

	Property FundingStatusChecked As Integer
		Get
			Return isFundingStatusChecked
		End Get
		Set(ByVal value As Integer)
			isFundingStatusChecked = value
		End Set
	End Property

	Property LegAVCChecked As Integer
		Get
			Return isLegAVCChecked
		End Get
		Set(ByVal value As Integer)
			isLegAVCChecked = value
		End Set
	End Property

	Property NamesChecked As Integer
		Get
			Return isNamesChecked
		End Get
		Set(ByVal value As Integer)
			isNamesChecked = value
		End Set
	End Property

	Property ExitDocChecked As Integer
		Get
			Return isExitDocChecked
		End Get
		Set(ByVal value As Integer)
			isExitDocChecked = value
		End Set
	End Property

	Property ValidDocChecked As Integer
		Get
			Return isValidDocChecked
		End Get
		Set(ByVal value As Integer)
			isValidDocChecked = value
		End Set
	End Property

	Property DataEntryChecked As Integer
		Get
			Return isDataEntryChecked
		End Get
		Set(ByVal value As Integer)
			isDataEntryChecked = value
		End Set
	End Property

End Class

Public Class ApplicationDocumentDetail

     Dim fkiDocumentTypeID As Integer
	Dim dteReceived As Date
     Dim txtReceivedBy As String
     Dim fkiMemberApplicationID As String
     Dim txtdocumentSource As String
     Dim txtDocumentTypeName As String
     Dim txtDocumentLocation As String
	Dim intIsVerified As String
	Dim txtDMSDocumentID As String
	Dim txtDMSDocumentExt As String

	Property DMSDocumentExt As String
		Get
			Return txtDMSDocumentExt
		End Get
		Set(ByVal value As String)
			txtDMSDocumentExt = value
		End Set
	End Property

	Property DMSDocumentID As String
		Get
			Return txtDMSDocumentID
		End Get
		Set(ByVal value As String)
			txtDMSDocumentID = value
		End Set
	End Property


     Property IsVerified As String
          Get
               Return intIsVerified
          End Get
          Set(ByVal value As String)
               intIsVerified = value
          End Set
     End Property

     Property DocumentLocation As String
          Get
               Return txtDocumentLocation
          End Get
          Set(ByVal value As String)
               txtDocumentLocation = value
          End Set
     End Property

     Property DocumentTypeName As String
          Get
               Return txtDocumentTypeName
          End Get
          Set(ByVal value As String)
               txtDocumentTypeName = value
          End Set
     End Property

     Property DocumentTypeID As Integer
          Get
               Return fkiDocumentTypeID
          End Get
          Set(ByVal value As Integer)
               fkiDocumentTypeID = value
          End Set
     End Property
     Property DateReceived As Date
          Get
               Return dteReceived
          End Get
          Set(ByVal value As Date)
               dteReceived = value
          End Set
     End Property
     Property ReceivedBy As String
          Get
               Return txtReceivedBy
          End Get
          Set(ByVal value As String)
               txtReceivedBy = value
          End Set
     End Property

     Property MemberApplicationID As String
          Get
               Return fkiMemberApplicationID
          End Get
          Set(ByVal value As String)
               fkiMemberApplicationID = value
          End Set
     End Property

     Property DocumentSource As String
          Get
               Return txtdocumentSource
          End Get
          Set(ByVal value As String)
               txtdocumentSource = value
          End Set
     End Property

End Class

Public Class ApplicationProperties

     Dim fName As String
     Dim fValue As String

     Property FieldName As String
          Get
               Return fName
          End Get
          Set(ByVal value As String)
               fName = value
          End Set
     End Property

     Property FieldValue As String
          Get
               Return fValue
          End Get
          Set(ByVal value As String)
               fValue = value
          End Set
     End Property

End Class

Public Class RMASSchedule
     Dim ObjRetirementDetails As RetirementDetails
     Dim txtApplicationCode As String
     Dim txtPIN As String
     Dim txtName As String
     Dim txtEmployercode As String
     Dim intGender As Integer
     Dim dteDOB As Date
     Dim dteDisengagement As Date
     Dim numRSABalance As Decimal
     Dim num25Percent As Decimal
     Dim dteSent As Date
     Dim blnConfirm As Integer
     Dim dteConfirm As Date
     Dim txtConfirmedBy As String
     Dim numEnblocPayment As Decimal
     Dim reasonforPayment As String
     Dim country As String
     Dim dteRetirement As Date
     Dim numTotalAVC As Decimal
     Dim numTotalAmount As Decimal
     Dim numAVCTax As Decimal
     Dim numNetAVC As Decimal
     Dim intAge As Integer
     Dim insuranceCoy As String
     Dim dtAnnuityCommencement As Date
     Dim numAnnualTotal As Decimal
     Dim numPremium As Decimal
     Dim numLumpSum As Decimal
     Dim numMonthlyAnnuity As Decimal
	Dim txtEmployerName As String
	Dim txtSector As String

	Property Sector As String
		Get
			Return txtSector
		End Get
		Set(ByVal value As String)
			txtSector = value
		End Set
	End Property

     Property Name As String
          Get
               Return txtName
          End Get
          Set(ByVal value As String)
               txtName = value
          End Set
     End Property

     Property EmployerName As String
          Get
               Return txtEmployerName
          End Get
          Set(ByVal value As String)
               txtEmployerName = value
          End Set
     End Property

     Property Age As Integer
          Get
               Return intAge
          End Get
          Set(ByVal value As Integer)
               intAge = value
          End Set
     End Property

     Property RetirementDetails As RetirementDetails
          Get
               Return ObjRetirementDetails
          End Get
          Set(ByVal value As RetirementDetails)
               ObjRetirementDetails = value
          End Set
     End Property

     Property InsuranceCompany As String
          Get
               Return insuranceCoy
          End Get
          Set(ByVal value As String)
               insuranceCoy = value
          End Set
     End Property

     Property AnnuityCommencementDate As Date
          Get
               Return dtAnnuityCommencement
          End Get
          Set(ByVal value As Date)
               dtAnnuityCommencement = value
          End Set
     End Property

     Property AnnualTotal As Decimal
          Get
               Return numAnnualTotal
          End Get
          Set(ByVal value As Decimal)
               numAnnualTotal = value
          End Set
     End Property

     Property NSITFInitialAmountPaid As Decimal
          Get
               Return numPremium
          End Get
          Set(ByVal value As Decimal)
               numPremium = value
          End Set
     End Property

     Property NSITFRecievedToRSA As Decimal
          Get
               Return numLumpSum
          End Get
          Set(ByVal value As Decimal)
               numLumpSum = value
          End Set
     End Property

     Property NSITFRequestedToRSA As Decimal
          Get
               Return numMonthlyAnnuity
          End Get
          Set(ByVal value As Decimal)
               numMonthlyAnnuity = value
          End Set
     End Property



     Property TotalAVC As Decimal
          Get
               Return numTotalAVC
          End Get
          Set(ByVal value As Decimal)
               numTotalAVC = value
          End Set
     End Property

     Property TotalAVCAmount As Decimal
          Get
               Return numTotalAmount
          End Get
          Set(ByVal value As Decimal)
               numTotalAmount = value
          End Set
     End Property

     Property AVCTax As Decimal
          Get
               Return numAVCTax
          End Get
          Set(ByVal value As Decimal)
               numAVCTax = value
          End Set
     End Property

     Property NetAVCPayable As Decimal
          Get
               Return numNetAVC
          End Get
          Set(ByVal value As Decimal)
               numNetAVC = value
          End Set
     End Property

     Property RetirementDate As Date
          Get
               Return dteRetirement
          End Get
          Set(ByVal value As Date)
               dteRetirement = value
          End Set
     End Property


     Property Nationality As String
          Get
               Return country
          End Get
          Set(ByVal value As String)
               country = value
          End Set
     End Property

     Property PaymentReason As String
          Get
               Return reasonforPayment
          End Get
          Set(ByVal value As String)
               reasonforPayment = value
          End Set
     End Property

     Property EnblocAmount As Decimal
          Get
               Return numEnblocPayment
          End Get
          Set(ByVal value As Decimal)
               numEnblocPayment = value
          End Set
     End Property


     Property ApplicationCode As String
          Get
               Return txtApplicationCode
          End Get
          Set(ByVal value As String)
               txtApplicationCode = value
          End Set
     End Property

     Property PIN As String
          Get
               Return txtPIN
          End Get
          Set(ByVal value As String)
               txtPIN = value
          End Set
     End Property

     Property Employercode As String
          Get
               Return txtEmployercode
          End Get
          Set(ByVal value As String)
               txtEmployercode = value
          End Set
     End Property

     Property Gender As Integer
          Get
               Return intGender
          End Get
          Set(ByVal value As Integer)
               intGender = value
          End Set
     End Property

     Property DOB As Date
          Get
               Return dteDOB
          End Get
          Set(ByVal value As Date)
               dteDOB = value
          End Set
     End Property

     Property DateDisengagement As Date
          Get
               Return dteDisengagement
          End Get
          Set(ByVal value As Date)
               dteDisengagement = value
          End Set
     End Property
     Property RSABalance As Decimal
          Get
               Return numRSABalance
          End Get
          Set(ByVal value As Decimal)
               numRSABalance = value
          End Set
     End Property
     Property Twenty5Percent As Decimal
          Get
               Return num25Percent
          End Get
          Set(ByVal value As Decimal)
               num25Percent = value
          End Set
     End Property
     Property DateSent As Date
          Get
               Return dteSent
          End Get
          Set(ByVal value As Date)
               dteSent = value
          End Set
     End Property
     Property IsConfirm As Integer
          Get
               Return blnConfirm
          End Get
          Set(ByVal value As Integer)
               blnConfirm = value
          End Set
     End Property
     Property DateConfirm As Date
          Get
               Return dteConfirm
          End Get
          Set(ByVal value As Date)
               dteConfirm = value
          End Set
     End Property
     Property ConfirmedBy As String
          Get
               Return txtConfirmedBy
          End Get
          Set(ByVal value As String)
               txtConfirmedBy = value
          End Set
     End Property


End Class

Public Class EmpployerDetails

     Dim intEmployerID As Integer
     Dim txtEmployercode As String
     Dim txtEmployerName As String
     

     Property EmployerID As Integer
          Get
               Return intEmployerID
          End Get
          Set(ByVal value As Integer)
               intEmployerID = value
          End Set
     End Property

     Property Employercode As String
          Get
               Return txtEmployercode
          End Get
          Set(ByVal value As String)
               txtEmployercode = value
          End Set
     End Property

     Property EmployerName As String
          Get
               Return txtEmployerName
          End Get
          Set(ByVal value As String)
               txtEmployerName = value
          End Set
     End Property

End Class

Public Class PencomApprovalDetails

	Dim txtRefNo As String
	Dim dteApproval As Date
	Dim dteAcknowledgment As Date
	Dim numApprovalAmount As Decimal
	Dim numTotalLumpSumAmount As Double
	Dim numTotalPensionAmount As Double
	Dim numTotalAnnuityAmount As Double
	Dim txtCreatedBy As String
	Dim dteConfirmed As Date
	Dim txtConfirmedBy As String
	Dim intApptype As Integer
	Dim txtApplicationCode As String
	Dim txtStatus As Char

	Property TotalAnnuityAmount As Double
		Get
			Return numTotalAnnuityAmount
		End Get
		Set(ByVal value As Double)
			numTotalAnnuityAmount = value
		End Set
	End Property

	Property TotalPensionAmount As Double
		Get
			Return numTotalPensionAmount
		End Get
		Set(ByVal value As Double)
			numTotalPensionAmount = value
		End Set
	End Property

	Property TotalLumpSumAmount As Double
		Get
			Return numTotalLumpSumAmount
		End Get
		Set(ByVal value As Double)
			numTotalLumpSumAmount = value
		End Set
	End Property

	Property Status As Char
		Get
			Return txtStatus
		End Get
		Set(ByVal value As Char)
			txtStatus = value
		End Set
	End Property

	Property ApplicationCode As String
		Get
			Return txtApplicationCode
		End Get
		Set(ByVal value As String)
			txtApplicationCode = value
		End Set
	End Property

	Property AppType As Integer
		Get
			Return intApptype
		End Get
		Set(ByVal value As Integer)
			intApptype = value
		End Set
	End Property

	Property ConfirmedDate As Date
		Get
			Return dteConfirmed
		End Get
		Set(ByVal value As Date)
			dteConfirmed = value
		End Set
	End Property

	Property ConfirmedBy As String
		Get
			Return txtConfirmedBy
		End Get
		Set(ByVal value As String)
			txtConfirmedBy = value
		End Set
	End Property

	Property PencomBatch As String
		Get
			Return txtRefNo
		End Get
		Set(ByVal value As String)
			txtRefNo = value
		End Set
	End Property

	Property ApprovalDate As Date
		Get
			Return dteApproval
		End Get
		Set(ByVal value As Date)
			dteApproval = value
		End Set
	End Property

	Property AcknowledgmentDate As Date
		Get
			Return dteAcknowledgment
		End Get
		Set(ByVal value As Date)
			dteAcknowledgment = value
		End Set
	End Property

	Property TotalApprovalAmount As Decimal
		Get
			Return numApprovalAmount
		End Get
		Set(ByVal value As Decimal)
			numApprovalAmount = value
		End Set
	End Property

	Property CreatedBy As String
		Get
			Return txtCreatedBy
		End Get
		Set(ByVal value As String)
			txtCreatedBy = value
		End Set
	End Property


End Class

Public Class PencomApprovalExport
     Dim txtApplicationCode As String
     Dim txtFundID As String
     Dim txtRSAPIN As String
     Dim intApptype As Integer
     Dim dteApproval As Date
     Dim dteValueDate As Date
     Dim dtePeriodStart As Date
     Dim dtePeriodEnd As Date
     Dim numApprovalAmount As Decimal
     Dim txtEnpowerExportBatch As String
     Dim isPending As Integer

     Property ApplicationCode As String
          Get
               Return txtApplicationCode
          End Get
          Set(ByVal value As String)
               txtApplicationCode = value
          End Set
     End Property

     Property FundID As String
          Get
               Return txtFundID
          End Get
          Set(ByVal value As String)
               txtFundID = value
          End Set
     End Property

     Property RSAPIN As String
          Get
               Return txtRSAPIN
          End Get
          Set(ByVal value As String)
               txtRSAPIN = value
          End Set
     End Property

     Property ApplicationType As Integer
          Get
               Return intApptype
          End Get
          Set(ByVal value As Integer)
               intApptype = value
          End Set
     End Property

     Property ApprovalDate As Date
          Get
               Return dteApproval
          End Get
          Set(ByVal value As Date)
               dteApproval = value
          End Set
     End Property

     Property ValueDate As Date
          Get
               Return dteValueDate
          End Get
          Set(ByVal value As Date)
               dteValueDate = value
          End Set
     End Property

     Property StartPeriod As Date
          Get
               Return dtePeriodStart
          End Get
          Set(ByVal value As Date)
               dtePeriodStart = value
          End Set
     End Property

     Property EndPeriod As Date
          Get
               Return dtePeriodEnd
          End Get
          Set(ByVal value As Date)
               dtePeriodEnd = value
          End Set
     End Property

     Property ApprovalAmount As Decimal
          Get
               Return numApprovalAmount
          End Get
          Set(ByVal value As Decimal)
               numApprovalAmount = value
          End Set
     End Property

     Property EnpowerExportBatch As String
          Get
               Return txtEnpowerExportBatch
          End Get
          Set(ByVal value As String)
               txtEnpowerExportBatch = value
          End Set
     End Property


End Class

Public Class ChangeRequest
	Dim FieldN As String
	Dim FieldV As String


	Property FieldName As String
		Get
			Return FieldN

		End Get
		Set(ByVal value As String)
			FieldN = value
		End Set
	End Property

	Property FieldValue As String
		Get
			Return FieldV
		End Get
		Set(ByVal value As String)
			FieldV = value
		End Set
	End Property

End Class

Public Class PencomApprovalPeople
	Dim intAppType As Integer
     Dim txtApplicationCode As String
     Dim txtPIN As String
     Dim txtName As String
     Dim dteDisengagement As Date
     Dim dteDOR As Date
     Dim numApprovedAmount As Decimal
     Dim numAmountToPay As Double
     Dim dteValueDate As String
     Dim txtAccountName As String
     Dim txtAccountNo As String
     Dim txtBankName As String
     Dim txtBankBranch As String
     Dim txtPencomBatch As String
	Dim dteApprovalConfirmed As String
     Dim numLumpSum As Double
     Dim numMonthlyDrawndown As Double
     Dim numArears As Double
     Dim numMonthlyAnnuity As Double
     Dim insuranceCoy As String
     Dim numLumpSumToPay As Double
     Dim numMonthlyDrawndownToPay As Double
     Dim numArearsToPay As Double
	Dim numAnnuityToPay As Double
	Dim numInterestAmount As Double

	Property AppTypeID As Integer
		Get
			Return intAppType
		End Get
		Set(ByVal value As Integer)
			intAppType = value
		End Set
	End Property

	Property InterestAmount As Double
		Get
			Return numInterestAmount
		End Get
		Set(ByVal value As Double)
			numInterestAmount = value
		End Set
	End Property

     Property AmountToPay As Double
          Get
               Return numAmountToPay
          End Get
          Set(ByVal value As Double)
               numAmountToPay = value
          End Set
     End Property

     Property AnnuityToPay As Double
          Get
               Return numAnnuityToPay
          End Get
          Set(ByVal value As Double)
               numAnnuityToPay = value
          End Set
     End Property

     Property LumpSumToPay As Double
          Get
               Return numLumpSumToPay
          End Get
          Set(ByVal value As Double)
               numLumpSumToPay = value
          End Set
     End Property

     Property MonthlyDrawndownToPay As Double
          Get
               Return numMonthlyDrawndownToPay
          End Get
          Set(ByVal value As Double)
               numMonthlyDrawndownToPay = value
          End Set
     End Property

     Property ArearsToPay As Double
          Get
               Return numArearsToPay
          End Get
          Set(ByVal value As Double)
               numArearsToPay = value
          End Set
     End Property

     Property Arears As Double
          Get
               Return numArears
          End Get
          Set(ByVal value As Double)
               numArears = value
          End Set
     End Property

     Property LumpSum As Double
          Get
               Return numLumpSum
          End Get
          Set(ByVal value As Double)
               numLumpSum = value
          End Set
     End Property

     Property MonthlyDrawndown As Double
          Get
               Return numMonthlyDrawndown
          End Get
          Set(ByVal value As Double)
               numMonthlyDrawndown = value
          End Set
     End Property

     Property MonthlyAnnuity As Double
          Get
               Return numMonthlyAnnuity
          End Get
          Set(ByVal value As Double)
               numMonthlyAnnuity = value
          End Set
     End Property

     Property InsuranceCompanyName As String
          Get
               Return insuranceCoy
          End Get
          Set(ByVal value As String)
               insuranceCoy = value
          End Set
     End Property

     Property DateApprovalConfirmed As String
          Get
               Return dteApprovalConfirmed
          End Get
          Set(ByVal value As String)
               dteApprovalConfirmed = value
          End Set
     End Property
     Property PencomBatch As String
          Get
               Return txtPencomBatch
          End Get
          Set(ByVal value As String)
               txtPencomBatch = value
          End Set
     End Property

     Property ApplicationCode As String
          Get
               Return txtApplicationCode
          End Get
          Set(ByVal value As String)
               txtApplicationCode = value
          End Set
     End Property

     Property PIN As String
          Get
               Return txtPIN
          End Get
          Set(ByVal value As String)
               txtPIN = value
          End Set
     End Property

     Property Name As String
          Get
               Return txtName
          End Get
          Set(ByVal value As String)
               txtName = value
          End Set
     End Property

     Property Disengagement As Date
          Get
               Return dteDisengagement
          End Get
          Set(ByVal value As Date)
               dteDisengagement = value
          End Set
     End Property

     Property RetirementDate As Date
          Get
               Return dteDOR
          End Get
          Set(ByVal value As Date)
               dteDOR = value
          End Set
     End Property

     Property ApprovedAmount As Decimal
          Get
               Return numApprovedAmount
          End Get
          Set(ByVal value As Decimal)
               numApprovedAmount = value
          End Set
     End Property

     Property ValueDate As String
          Get
               Return dteValueDate
          End Get
          Set(ByVal value As String)
               dteValueDate = value
          End Set
     End Property

     Property BankBranch As String
          Get
               Return txtBankBranch
          End Get
          Set(ByVal value As String)
               txtBankBranch = value
          End Set
     End Property

     Property BankName As String
          Get
               Return txtBankName
          End Get
          Set(ByVal value As String)
               txtBankName = value
          End Set
     End Property

     Property AccountNo As String
          Get
               Return txtAccountNo
          End Get
          Set(ByVal value As String)
               txtAccountNo = value
          End Set
     End Property

     Property AccountName As String
          Get
               Return txtAccountName
          End Get
          Set(ByVal value As String)
               txtAccountName = value
          End Set
     End Property

   



End Class
Public Class EncryptAES
	Dim encoder As New UTF8Encoding()
End Class

Public Class Pensioner
	Dim txtPIN As String
	Dim txtFullname As String
	Dim numPWAmount As Double
	Dim numPensionAmount As Double
	Dim txtAccountNo As String
	Dim txtBank As String
	Dim txtBranch As String
	Dim intFrequency As Integer
	Dim dteAnniversaryDate As Date

	Property AnniversaryDate As Date
		Get
			Return dteAnniversaryDate
		End Get
		Set(ByVal value As Date)
			dteAnniversaryDate = value
		End Set
	End Property

	Property Frequency As Integer
		Get
			Return intFrequency
		End Get
		Set(ByVal value As Integer)
			intFrequency = value
		End Set
	End Property
	Property Branch As String
		Get
			Return txtBranch
		End Get
		Set(ByVal value As String)
			txtBranch = value
		End Set
	End Property

	Property Bank As String
		Get
			Return txtBank
		End Get
		Set(ByVal value As String)
			txtBank = value
		End Set
	End Property

	Property AccountNo As String
		Get
			Return txtAccountNo
		End Get
		Set(ByVal value As String)
			txtAccountNo = value
		End Set
	End Property

	Property PensionAmount As Double
		Get
			Return numPWAmount
		End Get
		Set(ByVal value As Double)
			numPWAmount = value
		End Set
	End Property


	Property PWAmount As Double
		Get
			Return numPWAmount
		End Get
		Set(ByVal value As Double)
			numPWAmount = value
		End Set
	End Property

	Property Fullname As String
		Get
			Return txtFullname
		End Get
		Set(ByVal value As String)
			txtFullname = value
		End Set
	End Property

	Property PIN As String
		Get
			Return txtPIN
		End Get
		Set(ByVal value As String)
			txtPIN = value
		End Set
	End Property

End Class