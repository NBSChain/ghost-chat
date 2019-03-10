/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:TerzoChat"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using Autofac;
using TerzoChat.Data;
using TerzoChat.Peer;
using TerzoChat.ViewModel;


namespace TerzoChat.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            var builder = new ContainerBuilder();
            //×¢²áViewModel
            builder.RegisterType<MainViewModel>();
            
            builder.RegisterType<ContactService>().As<IStorage<ContactViewModel>>();

            builder.RegisterType<RChatViewModel>();
            builder.RegisterType<MessageService>().As<IStorage<MessageViewModel>>();

            ViewModelContainer = builder.Build();
        }

        public IContainer ViewModelContainer;

        public MainViewModel Main
        {
            get
            {
                return ViewModelContainer.Resolve<MainViewModel>();
            }
        }
        
        public RChatViewModel RightChat
        {
            get
            {
                return ViewModelContainer.Resolve<RChatViewModel>();
            }
        }
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}