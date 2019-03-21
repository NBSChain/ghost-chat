using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Data;
using System.Windows.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UrusTools.Config;
using TerzoChat.Data;
using TerzoChat.Peer;
using Pb;
using grpc2slib.BBL;
using System;

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
        

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IStorage<ContactViewModel> storage) :base()
        {
            BindingOperations.EnableCollectionSynchronization(_contacts, _lockContacts);

            _nick = AppState.Instance.CID;
            _acid = AppState.Instance.CID;
            AddSelfToContacts(_nick, _acid);
            AssignCommands();
                   
        }

        private int _count = 0;


  
        public string ACID { get => _acid; set => Set(ref _acid,value); }

        public string NickName { get => _nick; set => Set(ref _nick, value); }

        public ObservableCollection<ContactViewModel> ContactList
        {
            get => _contacts;
            set => Set(ref _contacts, value);
        }

        public ICommand ReMoveSelect { get; private set; }

        public ICommand InsertOne { get; private set; }

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
            ReMoveSelect = new RelayCommand<ContactViewModel>(a =>
            {
                if (a == null) return;
                ContactViewModel c = a as ContactViewModel;
                Console.WriteLine("rev"+ a.Nickname);
                ContactList.Remove(a);
            });
            InsertOne = new RelayCommand(() => {
                _count++;
                Console.WriteLine("add"+_count.ToString());
                ContactList.Add(new ContactViewModel { Nickname = "Add"+_count.ToString(), Password = "AAsss11" });
                });
        }

    }
}