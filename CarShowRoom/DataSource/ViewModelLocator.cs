/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:CarShowRoom.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/ 

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using CarShowRoom.Model;
using CarShowRoom.DesignMode;
using CarShowRoom.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using Xceed.Wpf.Toolkit;

namespace CarShowRoom.DataSource
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<DesignMode.MainWindowDesign>();
                SimpleIoc.Default.Register<DesignMode.CarBrandDesign>();
                SimpleIoc.Default.Register<DesignMode.CarDesign>();
                SimpleIoc.Default.Register<DesignMode.ClientDesign>();
            }
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<CarBrandViewModel>();
            SimpleIoc.Default.Register<CarViewModel>();
            SimpleIoc.Default.Register<ClientViewModel>();
            Messenger.Default.Register<NotificationMessage>(this, NotifyUserMethod);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public CarBrandViewModel CarBrandMain
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CarBrandViewModel>();
            }
        } 

        public CarViewModel CarMain
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CarViewModel>();
            }
        }

        public ClientViewModel ClientMain
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ClientViewModel>();
            }
        }

        private void NotifyUserMethod(NotificationMessage message)
        {
            MessageBox.Show(message.Notification);
        } 

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
        }
    }
}