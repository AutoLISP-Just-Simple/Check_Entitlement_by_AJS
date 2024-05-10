Imports RestSharp
Imports System
Imports System.Windows
Imports System.Windows.Forms

Namespace Check_Entitlement_by_AJS
    Friend Module AJS_Lics
        Private _appID As String = "4688092142369552289"
        Private mCheckOnline As Boolean

        Function CheckOnline(ByVal Optional msg As Boolean = True) As Boolean
            If mCheckOnline Then Return True
            Dim _userID As String = ""

            Try
                _userID = TryCast(Autodesk.AutoCAD.ApplicationServices.Application.GetSystemVariable("ONLINEUSERID"), String)
            Catch
                If msg Then MessageBox.Show("Work on AutoCAD > 2014 Only")
                Return False
            End Try

            If _userID.Equals("") Then
                If msg Then MessageBox.Show("Please log-in to Autodesk 360")
                Return False
            End If

            Try
                Dim client As RestClient = New RestClient("https://apps.autodesk.com")
                Dim req As RestRequest = New RestRequest("webservices/checkentitlement")
                req.Method = Method.[GET]
                req.AddParameter("userid", _userID)
                req.AddParameter("appid", _appID)

                System.Net.ServicePointManager.ServerCertificateValidationCallback = AddressOf AcceptAllCertifications
                Dim resp As IRestResponse(Of EntitlementResult) = client.Execute(Of EntitlementResult)(req)
                mCheckOnline = resp.Data IsNot Nothing AndAlso resp.Data.IsValid
            Catch
                If msg Then MessageBox.Show("Cannot connect to the https://apps.autodesk.com")
            End Try

            If mCheckOnline <> True AndAlso msg Then MessageBox.Show("You are not entitled to use this plug-in!")
            Return mCheckOnline
        End Function

        Public Function AcceptAllCertifications(ByVal sender As Object, ByVal certification As System.Security.Cryptography.X509Certificates.X509Certificate, ByVal chain As System.Security.Cryptography.X509Certificates.X509Chain, ByVal sslPolicyErrors As System.Net.Security.SslPolicyErrors) As Boolean
            Return True
        End Function
    End Module
End Namespace
