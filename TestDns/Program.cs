using System.Net;
using System.Net.NetworkInformation;

namespace TestDns
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            //TestMethod1();
            TestMethod2();

            Console.ReadLine();
        }

        private static void TestMethod1()
        {
            try
            {
                IPAddress ipAddress = null;
                foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                    {
                        //Console.WriteLine(ni.Name);
                        foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                        {
                            if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            {
                                Console.WriteLine(ip.Address.ToString());
                                ipAddress = ip.Address;
                                break;
                            }
                        }
                    }
                }

                Console.WriteLine(ipAddress);
                Console.WriteLine();

                var result5 = Dns.GetHostAddresses("", System.Net.Sockets.AddressFamily.InterNetwork).FirstOrDefault();
                Console.WriteLine(result5);
                Console.WriteLine();

                var result2 = Dns.GetHostAddresses("203.69.51.54", System.Net.Sockets.AddressFamily.InterNetwork).FirstOrDefault();
                Console.WriteLine(result2);
                Console.WriteLine();

                var result1 = Dns.GetHostAddresses("mmchat.yuanta.com.hk").FirstOrDefault();
                Console.WriteLine(result1);
                Console.WriteLine();

                var result6 = Dns.GetHostAddresses("NexusGSYQ-UAT.yuanta.com.tw").FirstOrDefault();
                Console.WriteLine(result6);
                Console.WriteLine();

                var result3 = Dns.GetHostEntry("202.66.68.12");
                Console.WriteLine(result3);
                Console.WriteLine();

                var result4 = Dns.GetHostEntry("mmchat.yuanta.com.hk").AddressList[0];
                Console.WriteLine(result4);
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static void TestMethod2()
        {
            Uri baseUri = new Uri("http://www.contoso.com/");
            Uri myUri = new Uri(baseUri, "catalog/shownew.htm?date=today");

            Console.WriteLine(myUri.Scheme);

            Uri uri = new Uri("test://mmchat.yuanta.com.hk");
            Console.WriteLine(uri.Scheme);
            Console.WriteLine(uri.Host);
        }
    }
}