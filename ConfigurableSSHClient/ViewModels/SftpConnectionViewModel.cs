using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ConfigurableSSHClient.Commands;
using ConfigurableSSHClient.Models;
using ConfigurableSSHClient.Wrappers;

namespace ConfigurableSSHClient.ViewModels
{
    /// <summary>
    /// aka Presenter
    /// </summary>
    public class SftpConnectionViewModel : ObservableObject
    {
        private readonly SftpConnectionModel _sftpConnection;
        public SftpConnectionViewModel()
        {
            List<SshAlgorithm> macAlgorithmsList = new List<SshAlgorithm>();
            macAlgorithmsList.Add(new SshAlgorithm { IsEnabled = true, Name = "hmac-sha1" });
            macAlgorithmsList.Add(new SshAlgorithm { IsEnabled = true, Name = "hmac-md5" });
            macAlgorithmsList.Add(new SshAlgorithm { IsEnabled = true, Name = "hmac-sha1-96" });
            macAlgorithmsList.Add(new SshAlgorithm { IsEnabled = true, Name = "hmac-md5-96" });
            macAlgorithmsList.Add(new SshAlgorithm { IsEnabled = false, Name = "none" });

            List<SshAlgorithm> encryptionAlgorithmsList = new List<SshAlgorithm>();
            encryptionAlgorithmsList.Add(new SshAlgorithm { IsEnabled = true, Name = "aes256-ctr" });
            encryptionAlgorithmsList.Add(new SshAlgorithm { IsEnabled = true, Name = "aes256-cbc" });
            encryptionAlgorithmsList.Add(new SshAlgorithm { IsEnabled = true, Name = "aes192-ctr" });
            encryptionAlgorithmsList.Add(new SshAlgorithm { IsEnabled = true, Name = "aes192-cbc" });
            encryptionAlgorithmsList.Add(new SshAlgorithm { IsEnabled = true, Name = "aes128-ctr" });
            encryptionAlgorithmsList.Add(new SshAlgorithm { IsEnabled = true, Name = "aes128-cbc" });
            encryptionAlgorithmsList.Add(new SshAlgorithm { IsEnabled = true, Name = "3des-ctr" });
            encryptionAlgorithmsList.Add(new SshAlgorithm { IsEnabled = true, Name = "3des-cbc" });
            encryptionAlgorithmsList.Add(new SshAlgorithm { IsEnabled = false, Name = "none" });

            List<SshAlgorithm> compressionAlgorithmsList = new List<SshAlgorithm>();
            compressionAlgorithmsList.Add(new SshAlgorithm { IsEnabled = true, Name = "zlib" });
            compressionAlgorithmsList.Add(new SshAlgorithm { IsEnabled = true, Name = "zlib@openssh.com" });
            compressionAlgorithmsList.Add(new SshAlgorithm { IsEnabled = false, Name = "none" });

            List<SshAlgorithm> keyExchangeAlgorithmsList = new List<SshAlgorithm>();
            keyExchangeAlgorithmsList.Add(new SshAlgorithm { IsEnabled = true, Name = "diffie-hellman-group1-sha1" });
            keyExchangeAlgorithmsList.Add(new SshAlgorithm { IsEnabled = true, Name = "diffie-hellman-group14-sha1" });
            keyExchangeAlgorithmsList.Add(new SshAlgorithm { IsEnabled = true, Name = "diffie-hellman-group-exchange-sha256" });
            keyExchangeAlgorithmsList.Add(new SshAlgorithm { IsEnabled = true, Name = "diffie-hellman-group-exchange-sha1" });

            _sftpConnection = new SftpConnectionModel("127.0.0.1", 22, macAlgorithmsList, encryptionAlgorithmsList, compressionAlgorithmsList, keyExchangeAlgorithmsList);
        }

        public string ServerAddressText
        {
            get
            {
                return _sftpConnection.ServerAddress;
            }
            set
            {
                _sftpConnection.ServerAddress = value;
                OnPropertyChangedEvent("ServerAddressText");
            }
        }

        public string ServerPortText
        {
            get
            {
                return _sftpConnection.ServerPort.ToString();
            }
            set
            {
                int valueAsInt;
                if (int.TryParse(value, out valueAsInt))
                {
                    _sftpConnection.ServerPort = valueAsInt;
                    OnPropertyChangedEvent("ServerPortText");
                }
            }
        }

        public ObservableCollection<SshAlgorithm> MacAlgorithms
        {
            get
            {
                return new ObservableCollection<SshAlgorithm>(_sftpConnection.MacAlgorithms);
            }
            set
            {
                _sftpConnection.MacAlgorithms = new List<SshAlgorithm>(value);
                OnPropertyChangedEvent("MacAlgorithmsText");
            }
        }

        public ObservableCollection<SshAlgorithm> EncryptionAlgorithms
        {
            get
            {
                return new ObservableCollection<SshAlgorithm>(_sftpConnection.EncryptionAlgorithms);
            }
            set
            {
                _sftpConnection.EncryptionAlgorithms = new List<SshAlgorithm>(value);
                OnPropertyChangedEvent("EncryptionAlgorithmsText");
            }
        }

        public ObservableCollection<SshAlgorithm> CompressionAlgorithms
        {
            get
            {
                return new ObservableCollection<SshAlgorithm>(_sftpConnection.CompressionAlgorithms);
            }
            set
            {
                _sftpConnection.CompressionAlgorithms = new List<SshAlgorithm>(value);
                OnPropertyChangedEvent("CompressionAlgorithmsText");
            }
        }

        public ObservableCollection<SshAlgorithm> KeyExchangeAlgorithms
        {
            get
            {
                return new ObservableCollection<SshAlgorithm>(_sftpConnection.KeyExchangeAlgorithms);
            }
            set
            {
                _sftpConnection.KeyExchangeAlgorithms = new List<SshAlgorithm>(value);
                OnPropertyChangedEvent("KeyExchangeAlgorithmsText");
            }
        }

        private readonly ObservableCollection<string> _log = new ObservableCollection<string>();
        /// <summary>
        /// Contains a collection of all the logs status thrown by the SFTP client.
        /// </summary>
        public IEnumerable<string> Log
        {
            get
            {
                return _log;
            }
        }

        private string _lastLog = string.Empty;
        /// <summary>
        /// Contains only the last log status thrown by the SFTP client.
        /// </summary>
        public string LastLog
        {
            get
            {
                return _lastLog;
            }
            set
            {
                _lastLog = value;
                OnPropertyChangedEvent("LastLog");
            }
        }

        public ICommand TestConnectionCommand
        {
            get
            {
                return new DelegateCommand(TestConnection);
            }
        }

        private void TestConnection()
        {
            string output = _sftpConnection.TestConnection();
            AddToLog(output);
        }

        private void AddToLog(string item)
        {
            _log.Add(item);
            LastLog = item;
        }
    }
}
