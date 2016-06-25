using MvvmCross.Core.ViewModels;
using XWeather.Entities;

namespace XWeather.Dto
{
    public class CloudsDto : MvxViewModel
    {
        private int _all;

        public CloudsDto()
        {
            
        }

        public CloudsDto(Clouds clouds)
        {
            All = clouds.all;
        }

        public int All
        {
            get { return _all; }
            set { _all = value; RaisePropertyChanged(); }
        }
    }
}