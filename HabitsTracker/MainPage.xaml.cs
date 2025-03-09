using HabitsTracker.Persistence;
using HabitsTracker.Persistence.Entities;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace HabitsTracker
{
    public partial class MainPage : ContentPage
    {
        HabitDatabase _habitDatabase;
        int count = 0;

        public ObservableCollection<HabitEntity> Habits { get; set; }

        public MainPage(HabitDatabase habitDatabase)
        {
            InitializeComponent();
            _habitDatabase = habitDatabase;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await LoadHabitsAsync();
        }

        private async Task LoadHabitsAsync()
        {
            var habits = await _habitDatabase.GetAllAsync();

            Habits = new ObservableCollection<HabitEntity>(habits);

            BindingContext = this;
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            var habit = new HabitEntity
            {
                Name = HabitNameEntry.Text
            };

            habit.Id = await _habitDatabase.CreateAsync(habit);

            Habits.Add(habit);
        }

        private async void OnClearClicked(object sender, EventArgs e)
        {
            await _habitDatabase.ClearAll();
            Habits.Clear();
        }
    }

}
