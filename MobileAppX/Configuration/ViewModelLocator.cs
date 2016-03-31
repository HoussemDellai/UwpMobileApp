using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using MobileAppX.ViewModels;

namespace MobileAppX.Configuration
{
    public class ViewModelLocator
    {

        private readonly UnityContainer _unityContainer = new UnityContainer();

        public MainViewModel MainViewModel
        {
            get
            {
                return _unityContainer.Resolve<MainViewModel>();
            }
        }

        public ViewModelLocator()
        {

            var mainViewModel = new MainViewModel();

            //SetForDesignModeAsync();
           
            //_unityContainer.RegisterInstance(typeof(MainViewModel), mainViewModel, new ContainerControlledLifetimeManager());
            _unityContainer.RegisterType<MainViewModel>(new ContainerControlledLifetimeManager());
            SetForDesignModeAsync();

            var unityServiceLocator = new UnityServiceLocator(_unityContainer);

            ServiceLocator.SetLocatorProvider(() => unityServiceLocator);
        }

        private async Task SetForDesignModeAsync()
        {

            if (DesignMode.DesignModeEnabled)
            {
                await MainViewModel.DownloadDataAsync();
                MainViewModel.SelectedYoutubeVideo = MainViewModel.FilteredVideos[5];
            }
        }
    }
}
