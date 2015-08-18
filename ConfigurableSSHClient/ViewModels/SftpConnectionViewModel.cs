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
            macAlgorithmsList.Add(new SshAlgorithm { IsEnabled = false, Name = "hmac-sha1" });
            macAlgorithmsList.Add(new SshAlgorithm { IsEnabled = false, Name = "hmac-md5" });
            macAlgorithmsList.Add(new SshAlgorithm { IsEnabled = false, Name = "hmac-sha1-96" });
            macAlgorithmsList.Add(new SshAlgorithm { IsEnabled = false, Name = "hmac-md5-96" });
            macAlgorithmsList.Add(new SshAlgorithm { IsEnabled = false, Name = "none" });

            List<SshAlgorithm> encryptionAlgorithmsList = new List<SshAlgorithm>();
            encryptionAlgorithmsList.Add(new SshAlgorithm { IsEnabled = false, Name = "aes256-ctr" });
            encryptionAlgorithmsList.Add(new SshAlgorithm { IsEnabled = false, Name = "aes256-cbc" });
            encryptionAlgorithmsList.Add(new SshAlgorithm { IsEnabled = false, Name = "aes192-ctr" });
            encryptionAlgorithmsList.Add(new SshAlgorithm { IsEnabled = false, Name = "aes192-cbc" });
            encryptionAlgorithmsList.Add(new SshAlgorithm { IsEnabled = false, Name = "aes128-ctr" });
            encryptionAlgorithmsList.Add(new SshAlgorithm { IsEnabled = false, Name = "aes128-cbc" });
            encryptionAlgorithmsList.Add(new SshAlgorithm { IsEnabled = false, Name = "3des-ctr" });
            encryptionAlgorithmsList.Add(new SshAlgorithm { IsEnabled = false, Name = "3des-cbc" });
            encryptionAlgorithmsList.Add(new SshAlgorithm { IsEnabled = false, Name = "none" });

            _sftpConnection = new SftpConnectionModel("127.0.0.1", 22, macAlgorithmsList, encryptionAlgorithmsList);
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

        private readonly ObservableCollection<string> _log = new ObservableCollection<string>();
        public IEnumerable<string> Log
        {
            get
            {
                return _log;
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
        }
    }
}
