using Core.Infrastructure.ViewModels;
using Core.Resources;
using Xamarin.Essentials;

namespace Core.ViewModels.About
{
    public class AboutViewModel : ViewModel
    {
        public AboutViewModel()
        {
            AppName = AppInfo.Name;
            PackageName = AppInfo.PackageName;
            VersionString = AppInfo.VersionString;
            BuildString = AppInfo.BuildString;
        }
        
        public string AppName { get; }
        
        public string PackageName { get; }
        
        public string VersionString { get; }
        
        public string BuildString { get; }

        public string AboutVersionCompliance => $"App name: {AppName} \nPackage name: {PackageName} \nVersion: {VersionString}\nBuild: {BuildString}";

        public string OtherText => Strings.About;
    }
}
