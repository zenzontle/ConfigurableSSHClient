using System;
using System.Text;
using nsoftware.IPWorksSSH;

namespace ConfigurableSSHClient.SSH
{
    public class SftpClient
    {
        private readonly Sftp _sftpClient;
        public SftpClient()
        {
            _sftpClient = new Sftp();
            _sftpClient.OnSSHServerAuthentication += sftp_OnSSHServerAuthentication;
        }

        public string TryConnection(string server, int port, string macAlgorithms, string encryptionAlgorithms, string compressionAlgorithms, string keyExchangeAlgorithms)
        {
            StringBuilder statusMessage = new StringBuilder();
            try
            {
                statusMessage.AppendFormat("Attempting connection to {0}:{1} using the following:", server, port).AppendLine();
                statusMessage.AppendFormat("- MAC Algorithms: {0}", macAlgorithms).AppendLine();
                statusMessage.AppendFormat("- Encryption Algorithms: {0}", encryptionAlgorithms).AppendLine();
                statusMessage.AppendFormat("- Compression Algorithms: {0}", compressionAlgorithms).AppendLine();
                statusMessage.AppendFormat("- KeyExchange Algorithms: {0}", keyExchangeAlgorithms).AppendLine();

                _sftpClient.Config(string.Format("SSHMacAlgorithms={0}", macAlgorithms));
                _sftpClient.SSHEncryptionAlgorithms = encryptionAlgorithms;
                _sftpClient.SSHCompressionAlgorithms = compressionAlgorithms;
                _sftpClient.Config(string.Format("SSHKeyExchangeAlgorithms={0}", keyExchangeAlgorithms));
                
                if (_sftpClient.Connected)
                {
                    _sftpClient.SSHLogoff();
                }
                _sftpClient.SSHLogon(server, port);
                statusMessage.AppendLine("Connection successful.");
                _sftpClient.SSHLogoff();
            }
            catch (Exception exception)
            {
                statusMessage.AppendLine(string.Format("Error: {0}", exception.Message));
            }
            return statusMessage.ToString();
        }

        private static void sftp_OnSSHServerAuthentication(object sender, SftpSSHServerAuthenticationEventArgs e)
        {
            e.Accept = true;
        }
    }
}
