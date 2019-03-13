using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using System.Collections.ObjectModel;
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
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IStorage<ContactViewModel> storage) : base()
        {
            var contacts = storage.GetList();
            AssignCommands();
            ContactList = new ObservableCollection<ContactViewModel>(contacts);
           
        }

        private int _count = 0;

      

        public string HID { get; set; }

        public ObservableCollection<ContactViewModel> ContactList { get; set; }

        public ICommand ReMoveSelect { get; private set; }

        public ICommand InsertOne { get; private set; }

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

        private void loadAccount()
        {
            BaseRPCService service = new BaseRPCService();
            try
            {
                string version = service.GetVersion();
                Console.WriteLine(" NBS version>>> " + version + ".");
            
            }
            catch
            {
               
            }
        }
    }
}