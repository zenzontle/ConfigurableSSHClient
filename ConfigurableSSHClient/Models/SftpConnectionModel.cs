using System.Collections.Generic;
using System.Linq;
using ConfigurableSSHClient.Wrappers;
using ConfigurableSSHClient.SSH;

namespace ConfigurableSSHClient.Models
{
    public class SftpConnectionModel
    {
        public string ServerAddress { get; set; }

        public int ServerPort { get; set; }

        public List<SshAlgorithm> MacAlgorithms { get; set; }

        public List<SshAlgorithm> EncryptionAlgorithms { get; set; }

        public List<SshAlgorithm> CompressionAlgorithms { get; set; }

        public List<SshAlgorithm> KeyExchangeAlgorithms { get; set; }

        private readonly SftpClient _sftpClient;

        public SftpConnectionModel(string serverAddress, int serverPort, List<SshAlgorithm> macAlgorithms, List<SshAlgorithm> encryptionAlgorithms, List<SshAlgorithm> compressionAlgorithms, List<SshAlgorithm> keyExchangeAlgorithms)
        {
            ServerAddress = serverAddress;
            ServerPort = serverPort;
            MacAlgorithms = macAlgorithms;
            EncryptionAlgorithms = encryptionAlgorithms;
            CompressionAlgorithms = compressionAlgorithms;
            KeyExchangeAlgorithms = keyExchangeAlgorithms;

            _sftpClient = new SftpClient();
        }

        public string TestConnection()
        {
            string macAlgos = string.Join(",", MacAlgorithms.Where(t => t.IsEnabled).Select(t => t.Name));
            string encryptionAlgos = string.Join(",", EncryptionAlgorithms.Where(t => t.IsEnabled).Select(t => t.Name));
            string compressionAlgos = string.Join(",", CompressionAlgorithms.Where(t => t.IsEnabled).Select(t => t.Name));
            string keyExchangeAlgos = string.Join(",", KeyExchangeAlgorithms.Where(t => t.IsEnabled).Select(t => t.Name));

            return _sftpClient.TryConnection(ServerAddress, ServerPort, macAlgos, encryptionAlgos, compressionAlgos, keyExchangeAlgos);
        }
    }
}
