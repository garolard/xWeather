using MvvmCross.Core.ViewModels;
using XWeather.Entities;

namespace XWeather.Dto
{
    public class CityDto : MvxViewModel
    {
        private int _id;
        private string _name;
        private CoordDto _coord;
        private string _country;
        


        public CityDto()
        {
            
        }

        public CityDto(City city)
        {
            Id = city.id;
            Name = city.name;
            Coordinates = new CoordDto(city.coord);
            Country = city.country;
            
        }


        public int Id
        {
            get { return _id; }
            set { _id = value; RaisePropertyChanged(); }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged(); }
        }

        public CoordDto Coordinates
        {
            get { return _coord; }
            set { _coord = value; RaisePropertyChanged(); }
        }

        public string Country
        {
            get { return _country; }
            set { _country = value; RaisePropertyChanged(); }
        }
    }
}