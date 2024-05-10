using RestSharp;
using System;
using System.Windows;

namespace Check_Entitlement_by_AJS
{
    internal static class AJS_Lics
    {
        //Install-Package RestSharp -Version 105.2.3
        //Example by www.lisp.vn

        private static String _appID = "4688092142369552289";
        private static bool mCheckOnline;

        public static bool CheckOnline(bool msg = true)
        {
            if (mCheckOnline) return true;
            String _userID = "";
            try
            {
                _userID = Autodesk.AutoCAD.ApplicationServices.Application.GetSystemVariable("ONLINEUSERID") as String;
            }
            catch
            {
                if (msg)
                    MessageBox.Show("Work on AutoCAD > 2014 Only");
                return false;
            }

            if (_userID.Equals(""))
            {
                if (msg)
                    MessageBox.Show("Please log-in to Autodesk 360");
                return false;
            }

            try
            {
                //check for online entitlement
                RestClient client = new RestClient("https://apps.autodesk.com");
                RestRequest req = new RestRequest("webservices/checkentitlement");
                req.Method = Method.GET;
                req.AddParameter("userid", _userID);
                req.AddParameter("appid", _appID);
                System.Net.ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                IRestResponse<EntitlementResult> resp = client.Execute<EntitlementResult>(req);

                mCheckOnline = resp.Data != null && resp.Data.IsValid;
            }
            catch
            {
                if (msg)
                    MessageBox.Show("Cannot connect to the https://apps.autodesk.com");
            }

            if (mCheckOnline != true && msg)
                MessageBox.Show("You are not entitled to use this plug-in!");

            //Example by www.lisp.vn
            return mCheckOnline;
        }
    }
}