//using Fiddler;
//using System;
//using System.Security.Cryptography.X509Certificates;

//namespace WebHelper.Network.Fiddler
//{
//    // This class is for handling certs for Fiddler.
//    public static class FiddlerCertManager
//    {
//        // Handle the trust for machine certs.
//        private static bool HandleMachineCert(X509Certificate2 oRootCert, bool trust)
//        {
//            try
//            {
//                X509Store certStore = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
//                certStore.Open(OpenFlags.ReadWrite);
//                try
//                {
//                    if(trust)
//                        certStore.Add(oRootCert);
//                    else
//                        certStore.Remove(oRootCert);
//                }
//                finally
//                {
//                    certStore.Close();
//                }
//                return true;
//            }
//            catch (Exception)
//            {
//                return false;
//            }
//        }

//        // Install a cert for Fiddler.
//        public static bool InstallCertificate(bool machine)
//        {
//            if (!CertMaker.rootCertExists())
//            {
//                if (!CertMaker.createRootCert())
//                    return false;

//                if (machine)
//                {
//                    X509Certificate2 oRootCert = CertMaker.GetRootCertificate();
//                    if (!HandleMachineCert(oRootCert, true))
//                        return false;
//                }
//                else
//                {
//                    if (!CertMaker.trustRootCert())
//                        return false;
//                }
//            }

//            return true;
//        }

//        // Uninstall fiddler certificates.
//        public static bool UninstallCertificate()
//        {
//            if (CertMaker.rootCertExists())
//            {
//                X509Certificate2 oRootCert = CertMaker.GetRootCertificate();
//                if (!HandleMachineCert(oRootCert, false))
//                    return false;
//                if (!CertMaker.removeFiddlerGeneratedCerts(true))
//                    return false;
//            }

//            return true;
//        }
//    }
//}
