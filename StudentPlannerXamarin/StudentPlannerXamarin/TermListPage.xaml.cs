using StudentPlannerXamarin.DataModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StudentPlannerXamarin
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermListPage : ContentPage
    {
        public TermListPage()
        {
            InitializeComponent();

            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ormdemo.db3");
            SQLite.SQLiteConnection db = new SQLite.SQLiteConnection(dbPath);
            db.CreateTable<Term>();
            var termTable = db.Table<Term>();

            this.BindingContext = termTable;
        }

        void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e == null) return; // has been set to null, do not 'process' tapped event
            var selectedTerm = (Term)((ListView)sender).SelectedItem;
            Navigation.PushAsync(new TermDetailPage(selectedTerm));
        }

        public ICommand MyCommand { private set; get; }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddTermPage());
        }
    }
}