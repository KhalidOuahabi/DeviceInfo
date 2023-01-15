using System.Management;

namespace DeviceInfo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            richTextBox1.Text = "CPU : " + GetCPUSerialNumber() +"\n BIOS : "+ GetBIOSSerialNumber() +"\n Disk : " + GetHardDiskSerialNumber() + "\n MAC : " + GetNetCardMACAddress();
        }

        private string GetCPUSerialNumber()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * From Win32_Processor");
                string sCPUSerialNumber = "";
                foreach (ManagementObject mo in searcher.Get())
                {
                    sCPUSerialNumber = mo["ProcessorId"].ToString().Trim();
                    break;
                }
                return sCPUSerialNumber;
            }
            catch
            {
                return "";
            }
        }


        //Get the motherboard serial number
        private string GetBIOSSerialNumber()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * From Win32_BIOS");
                string sBIOSSerialNumber = "";
                foreach (ManagementObject mo in searcher.Get())
                {
                    sBIOSSerialNumber = mo.GetPropertyValue("SerialNumber").ToString().Trim();
                    break;
                }
                return sBIOSSerialNumber;
            }
            catch
            {
                return "";
            }
        }


        //Get the serial number of the hard disk
        private string GetHardDiskSerialNumber()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
                string sHardDiskSerialNumber = "";
                int i = 0;
                foreach (ManagementObject mo in searcher.Get())
                {

                    if (mo["SerialNumber"] == null)
                    {
                        sHardDiskSerialNumber = null;
                    }
                    else
                    {
                        sHardDiskSerialNumber = mo["SerialNumber"].ToString();
                        break;
                    }

                    ++i;
                }
                return sHardDiskSerialNumber;
            }
            catch
            {
                return "";
            }
        }


        // Get network card address
        private string GetNetCardMACAddress()
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapter WHERE ((MACAddress Is Not NULL) AND (Manufacturer <> 'Microsoft'))");
                string NetCardMACAddress = "";
                foreach (ManagementObject mo in searcher.Get())
                {
                    NetCardMACAddress = mo["MACAddress"].ToString().Trim();
                    break;
                }
                return NetCardMACAddress;
            }
            catch
            {
                return "";
            }
        }
    }
}