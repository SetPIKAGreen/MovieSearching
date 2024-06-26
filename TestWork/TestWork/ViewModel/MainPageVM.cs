using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using TestWork.Model;

namespace TestWork.ViewModel
{

    public class MainPageVM
    {
        private ObservableCollection<Movie> moviesList;
        private readonly List<Movie> originalMoviesList;

        private bool isTitleChecked;
        private bool isActorChecked;
        private string searchText;
        private string selectedGenre;


        public event EventHandler TitleCheckedChanged;
        public event EventHandler ActorCheckedChanged;

        public ObservableCollection<Movie> MoviesList
        {
            get { return moviesList; }
            set
            {
                if (moviesList != value)
                {
                    moviesList = value;
                    OnPropertyChanged(nameof(MoviesList));
                }
            }
        }
        public Dictionary<int, string> genresDicionary = new Dictionary<int, string>()
        {
            { 0, "None" },
            { 1, "Horror" },
            { 2, "Action" },
            { 3, "Fantasy" },
            { 4, "Comedy" }
        };

        public List<string> GenresList { set; get; }
 
        public bool TitleChecked
        {
            get { return isTitleChecked; }
            set
            {
                if (isTitleChecked != value)
                {
                    isTitleChecked = value;
                    TitleCheckedChanged?.Invoke(this, EventArgs.Empty);

                    if (isTitleChecked)
                    {
                        ActorChecked = false;

                    }
                    else
                    {
                        ActorChecked = true;
                    }

                    OnPropertyChanged(nameof(TitleChecked));
                }
            }
        }
        public bool ActorChecked
        {
            get { return isActorChecked; }
            set
            {
                if (isActorChecked != value)
                {
                    isActorChecked = value;

                    ActorCheckedChanged?.Invoke(this, EventArgs.Empty);
                    if (isActorChecked)
                    {
                        TitleChecked = false;
                    }
                    else
                    {
                        TitleChecked = true;
                    }

                    OnPropertyChanged(nameof(ActorChecked));
                }
            }
        } 
        public string SearchText
        {
            get { return searchText; }
            set
            {
                if (searchText != value)
                {
                    searchText = value;
                    OnPropertyChanged(nameof(SearchText));
                    FilterMovies();
                }
            }
        }
        public string SelectedGenre
        {
            get { return selectedGenre; }
            set
            {
                if (selectedGenre != value)
                {
                    selectedGenre = value;
                    OnPropertyChanged(nameof(SelectedGenre));
                 

                }
            }
        }  
       


        


        public MainPageVM()
        {

            SelectedGenre = "None";
            TitleChecked = true;
            ActorChecked = false;
            GenresList = new List<string>();
            GenreListLoad();
            
            MoviesList = new ObservableCollection<Movie>();
            LoadFromDataBase();
            originalMoviesList = new List<Movie>(MoviesList);
            

        }

        public void GenreListLoad()
        {
            for(int i = 0; i < genresDicionary.Count; i++)
            {
                GenresList.Add(genresDicionary[i]);
            }
        }
        public void LoadFromDataBase()
        {

            List<Actor> actors_1 = new List<Actor>();
            List<Actor> actors_2 = new List<Actor>();
            List<Actor> actors_3 = new List<Actor>();
            List<Actor> actors_4 = new List<Actor>();

            actors_1.Add(new Actor() { Id = 1, Name = "Sigourney Weaver" });
            actors_2.Add(new Actor() { Id = 2, Name = "Bill Skarsgård" });
            actors_2.Add(new Actor() { Id = 3, Name = "Jaeden Martell" });
            actors_3.Add(new Actor() { Id = 4, Name = "Elijah Wood" });
            actors_4.Add(new Actor() { Id = 5, Name = "Macaulay Culkin" });

            MoviesList.Add(new Movie() { Title = "Alien", Genre = "Horror", Actors = actors_1 });
            MoviesList.Add(new Movie() { Title = "Avatar", Genre = "Action", Actors = actors_1 });
            MoviesList.Add(new Movie() { Title = "It", Genre = "Horror", Actors = actors_2 });
            MoviesList.Add(new Movie() { Title = "Lord of the Rings: Two towers", Genre = "Fantasy", Actors = actors_3 });
            MoviesList.Add(new Movie() { Title = "Home Alone", Genre = "Comedy", Actors = actors_4 });
        }
        private void FilterMovies()
        {
            string noneGenre = "None";

            if (string.IsNullOrWhiteSpace(SearchText))
            {
                MoviesList.Clear();
                foreach (var movie in originalMoviesList)
                {
                    MoviesList.Add(movie);
                }

            }
            else if (ActorChecked && SelectedGenre != noneGenre)
            {
                var filteredMovies = originalMoviesList.Where(movie => movie.Actors.Any(actor => actor.Name.ToLower().Contains(SearchText.ToLower())) && movie.Genre == SelectedGenre).ToList();
                MoviesList.Clear();
                foreach (var movie in filteredMovies)
                {
                    MoviesList.Add(movie);
                }
            }

            else if (TitleChecked && SelectedGenre != noneGenre)
            {

                var filteredMovies = originalMoviesList.Where(movie => movie.Title.ToLower().Contains(SearchText.ToLower()) && movie.Genre == SelectedGenre).ToList();

                MoviesList.Clear();
                foreach (var movie in filteredMovies)
                {
                    MoviesList.Add(movie);
                }
            }
            else if (TitleChecked)
            {

                var filteredMovies = originalMoviesList.Where(movie => movie.Title.ToLower().Contains(SearchText.ToLower())).ToList();

                MoviesList.Clear();
                foreach (var movie in filteredMovies)
                {
                    MoviesList.Add(movie);
                }
            }
            else if (ActorChecked)
            {
                var filteredMovies = originalMoviesList.Where(movie => movie.Actors.Any(actor => actor.Name.ToLower().Contains(SearchText.ToLower()))).ToList();

                MoviesList.Clear();
                foreach (var movie in filteredMovies)
                {
                    MoviesList.Add(movie);
                }
            }

            else
            {

            }
        }




        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
