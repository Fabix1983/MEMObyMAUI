using MEMObyMAUI.Data;
using MEMObyMAUI.Models;
using MEMObyMAUI.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;

namespace MEMObyMAUI
{
    public partial class HomePage : ContentPage
    {
        readonly DataBase database;
        public ObservableCollection<MyMemoVM> Memos { get; set; } = new();

        public HomePage()
        {
            InitializeComponent();
            database = new DataBase();
            LeggiDettMemo();
        }

        private async void LeggiDettMemo()
        {
            foreach (var item in await database.LeggiMemoItem())
            {
                Memos.Add(new MyMemoVM
                {
                    Id = item.Id,
                    Descrizione = item.Descrizione,
                    Data = item.Data,
                    Eseguito = item.Eseguito
                });
            }
        }

        private void OnButtonGestisciClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PushModalAsync(new DetailsPage(), true);
        }

        private void OnButtonNuovoClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PushModalAsync(new NewPage(), true);
        }

        private void OnButtonInfoAppClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PushModalAsync(new InfoAppPage(), true);
        }
    }

}
