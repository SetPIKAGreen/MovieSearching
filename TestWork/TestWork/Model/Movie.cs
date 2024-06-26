using System.Collections.Generic;
using System.ComponentModel;


namespace TestWork.Model
{
    public class Movie : INotifyPropertyChanged
    {
        private string title;
        private string genre;
        private List<Actor> actors;

        public string Title
        {
            get { return title; }
            set
            {
                if (title != value)
                {
                    title = value;
                    OnPropertyChanged(nameof(Title));
                }
            }
        }

        public string Genre
        {
            get { return genre; }
            set
            {
                if (genre != value)
                {
                    genre = value;
                    OnPropertyChanged(nameof(Genre));
                }
            }
        }

        public List<Actor> Actors
        {
            get
            {
                return actors;
            }
            set
            {
                if(actors != value)
                {
                    actors = value;
                    OnPropertyChanged(nameof(actors));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

