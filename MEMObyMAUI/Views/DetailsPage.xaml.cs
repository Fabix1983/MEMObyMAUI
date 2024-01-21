using MEMObyMAUI.Data;
using MEMObyMAUI.Models;
using MEMObyMAUI.ViewModels;
using Microsoft.Maui.Controls.PlatformConfiguration;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;

namespace MEMObyMAUI
{
    public partial class DetailsPage : ContentPage
    {

        readonly DataBase database;
        public ObservableCollection<MyMemoVM> Memos { get; set; } = new();

        public DetailsPage()
	    {
		    InitializeComponent();
            database = new DataBase();
            LeggiDettMemo();
        }

        private async void SwipeItem_Invoked(object sender, EventArgs e)
        {
            //var item = sender as SwipeItem;
            //await DisplayAlert(item.Text, $"Hai invocato l'azione {item.Text}.", "OK");

            var item = (sender as BindableObject).BindingContext as MyMemoVM;

            try {

                myMemo delMemoItem = new myMemo
                {
                    Id = item.Id,
                    Data = item.Data,
                    Descrizione = item.Descrizione,
                    Eseguito = item.Eseguito
                };

                int esito = await database.EliminaMemoItem(delMemoItem);

                if (esito == 0)
                {
                    await App.Current.MainPage.DisplayAlert("Errore",
                        "...questo memo non vuole sparire!", "OK");
                }
                else
                {
                    Memos.Remove(item);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Errore", $"Attenzione si è verifcato un errore imprevisto.", "KO");
            }
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

        void OnButtonTuttiFattiClicked(object sender, EventArgs args)
        {
            foreach (var item in Memos)
                item.Eseguito = true;
        }

        private async void OnButtonEliminaFattiClicked(object sender, EventArgs args)
        {
            int pos = 0;

            while (pos < Memos.Count)
                if (Memos[pos].Eseguito)
                {                   
                    myMemo delMemoItem = new myMemo
                    {
                        Id = Memos[pos].Id,
                        Data = Memos[pos].Data,
                        Descrizione = Memos[pos].Descrizione,
                        Eseguito = Memos[pos].Eseguito
                    };

                    int esito = await database.EliminaMemoItem(delMemoItem);

                    if (esito == 0)
                    {
                        await App.Current.MainPage.DisplayAlert("Errore Del Memo",
                            "...questo memo non vuole sparire!", "OK");
                    }
                    else
                    {
                        Memos.Remove(Memos[pos]);
                    }
                    
                }
                else
                    pos++;
        }

        private void OnButtonHomeClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PushModalAsync(new HomePage(), true);
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