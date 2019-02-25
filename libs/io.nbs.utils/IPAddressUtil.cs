using System;
using System.Diagnostics;
using System.Text;

namespace io.nbs.utils
{
     
public class IPAddressUtil
    {
        const string defConnDomain = "www.baidu.com";
        const string IPV4_REGX_EXPRESSION = @"^()(\d|[1-9]\d|1\d\d|2([0-4]\d|5[0-5]))\.){4}$";
            // @"(?<= (\\b |\\D))(((\\d{1,2})|(1\\d{2})|(2[0-4]\\d)|(25[0-5]))\\.){3}((\\d{1,2})|(1\\d{2})|(2[0-4]\\d)|(25[0-5]))(?=(\\b|\\D))";

        private static bool isTracing = false;

        public static string GetLocalIPv4()
        {
            string result = RunApp("route", "print", isTracing);
            System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(result, IPV4_REGX_EXPRESSION);

            if (match.Success)
            {
                return match.Groups[2].Value;
            }
            else
            {
                try
                {
                    System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient();
                    client.Connect(defConnDomain, 80);
                    string ip = ((System.Net.IPEndPoint)client.Client.LocalEndPoint).Address.ToString();
                    client.Close();
                    return ip;
                }catch(Exception e)
                {
                    throw new NbsCommException(e.Message);
                }
            }

        }

        public static string GetPrimaryDNS()
        {
            string result = RunApp("nslookup", "", isTracing);
            System.Text.RegularExpressions.Match match = System.Text.RegularExpressions.Regex.Match(result, @"\d+\.\d+\.\d+\.\d+");
            if (match.Success)
            {
                return match.Value;
            }
            else
            {
                throw new NbsCommException("not get dns.");
            }
        }

        public static void setTracing(bool status)
        {
            isTracing = status;
        }

        public static string RunApp(string filename,string arg,bool logged)
        {
            try
            {
                if (logged)
                {
                    Trace.Write(filename + " " + arg);
                }
                Process proc = new Process();
                proc.StartInfo.FileName = filename;
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.Arguments = arg;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.UseShellExecute = false;
                proc.Start();

                using(System.IO.StreamReader reader = new System.IO.StreamReader(proc.StandardOutput.BaseStream, Encoding.Default))
                {
                    System.Threading.Thread.Sleep(100);

                    if (!proc.HasExited)
                    {
                        proc.Kill();
                    }
                    string txt = reader.ReadToEnd();
                    reader.Close();

                    if (logged) Trace.WriteLine(txt);
                    return txt;
                }
            }catch(Exception e)
            {
                if(logged)Trace.WriteLine(e);
                throw new NbsCommException(e.Message);
            }
        }
    }

   
}
