using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// VPNSharp
/// Credits: Student Stoner
/// Created: 3/26/2018
/// Updated: 3/26/2018
/// 
/// IF YOU PLAN ON USING THIS, YOU MUST GIVE CREDITS!
/// </summary>

namespace VPNSharp
{
    public class PPTP
    {
        private bool Connect(string host, string user, string pass)
        {
            try
            {
                Disconnect();
                var proc = new Process();
                var sinfo = proc.StartInfo;
                sinfo.FileName = "rasdial";
                if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.History), "VPNSharp")))
                {
                    Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.History), "VPNSharp"));
                }
                File.WriteAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.History), "VPNSharp\\VPNSharp.pbk"), "[VPNSharp]\r\nMEDIA=rastapi\r\nPort=VPN2-0\r\nDevice=WAN Miniport (IKEv2)\r\nDEVICE=vpn\r\nPhoneNumber=" + host);
                sinfo.Arguments = $"\"VPNSharp\" { user } { pass }  /phonebook:\"{ Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.History), "VPNSharp\\VPNSharp.pbk") }\"";
                sinfo.WindowStyle = ProcessWindowStyle.Hidden;
                sinfo.CreateNoWindow = true;
                sinfo.UseShellExecute = false;
                sinfo.RedirectStandardOutput = true;
                proc.Start();
                proc.WaitForExit();
                if (!proc.StandardOutput.ReadToEnd().Contains("error"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
            }
            return false;
        }

        private void Disconnect()
        {
            var proc = new Process();
            var sinfo = proc.StartInfo;
            sinfo.FileName = @"rasdial";
            sinfo.Arguments = "/disconnect";
            sinfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.Start();
            proc.WaitForExit();
        }
    }
}
