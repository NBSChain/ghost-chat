using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Data;
using System.Windows.Input;
using System.Collections.ObjectModel;
using UrusTools.Config;
using TerzoChat.Data;
using TerzoChat.Peer;
using Pb;
using grpc2slib.BBL;
using System;
using System.Windows;

namespace TerzoChat.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private string _nick;
        private string _acid;
        private ObservableCollection<ContactViewModel> _contacts = new ObservableCollection<ContactViewModel>();
        private readonly object _lockContacts = new object();
        //»’÷æ
        private readonly string debugPath;

        private readonly GossipServiceClient gossipService;
        
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IStorage<ContactViewModel> storage) :base()
        {
            BindingOperations.EnableCollectionSynchronization(_contacts, _lockContacts);
            debugPath = AppState.Instance.GetLogFilePath("gossip");
            if (!System.IO.Directory.Exists(debugPath)) System.IO.Directory.CreateDirectory(debugPath);
            _nick = AppState.Instance.CID;
            _acid = AppState.Instance.CID;
            AddSelfToContacts(_nick, _acid);
            // 
            if (gossipService == null) gossipService = new GossipServiceClient(GrpcBaseHelper.Instance(), debugPath, "debug-gossip.txt");
            AssignCommands();         
        }

  
        public string ACID { get => _acid; set => Set(ref _acid,value); }

        public string NickName { get => _nick; set => Set(ref _nick, value); }

        public ObservableCollection<ContactViewModel> ContactList
        {
            get => _contacts;
            set => Set(ref _contacts, value);
        }

        public ICommand DebugGossip { get; private set; }

        public ICommand OpenGossipFolder { get; private set; }

        private void AddSelfToContacts(string nick ,string acid)
        {
            ContactViewModel cm = new ContactViewModel
            {
                Nickname = "Me",
                Password = acid,
                AvatarImg = ConfigManager.Instance.AvatarLocal()
            };
            _contacts.Add(cm);   
        }

        private void AssignCommands()
        {
            DebugGossip = new RelayCommand<string>(a =>
            {
                if (a == null) return;
                Console.WriteLine(">>>Parameter>>" + a);
                
                DebugCmd cmd = new DebugCmd { Cmd = a.ToString() };
                try
                {
                    DebugResult result = gossipService.GossipDebug(cmd);
                    string val = gossipService.FormatResult(result);
                    gossipService.WriteLog(result);
                    Console.WriteLine(val);
                    MessageBox.Show(val,"Debug");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Debug");
                }
            });


            OpenGossipFolder = new RelayCommand(() => {
                System.Diagnostics.Process.Start("explorer.exe", debugPath);
            });
        }

    }
}