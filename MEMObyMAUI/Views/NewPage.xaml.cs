using MEMObyMAUI.Data;
using MEMObyMAUI.Models;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;

namespace MEMObyMAUI;

public partial class NewPage : ContentPage
{
    readonly DataBase database;
    DateTime oggi = DateTime.Now;

    public NewPage()
	{
		InitializeComponent();
        database = new DataBase();

        DatePicker datePicker = new DatePicker
        {
            MinimumDate = new DateTime(2024, 1, 1),
            MaximumDate = new DateTime(2099, 12, 31),
            Date = oggi
        };

        TimePicker timePicker = new TimePicker
        {
            Time = new TimeSpan(7, 0, 0) // Time set to "04:15:26"
        };


    }

    private async void OnInsClicked(object sender, EventArgs e)
    {
        if (DescrizioneTXT.Text is null || DescrizioneTXT.Text == "")
        {
            await App.Current.MainPage.DisplayAlert("Dati mancanti",
                "Ehi non hai inserito la descrizione del Memo", "OK");

            return;
        }
      
        string formatted = "";

        DateTime? selectedDate = DataTXT.Date;
        if (selectedDate.HasValue)
        {
            // Attenzione:
            // su windows machine e pubblicazione su andorid il formato della data deve essere: dd/MM/yyyy
            // mentre per android in locale: MM/dd/yyyy
            formatted = selectedDate.Value.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        }
        else
        {
            await App.Current.MainPage.DisplayAlert("Dati mancanti",
                "Ehi non hai indicato la data del Memo", "OK");

            return;
        }

        TimeSpan? selectedTime = TimeTXT.Time;

        if (selectedTime.HasValue)
        {
            formatted = formatted + " " + selectedTime.Value.ToString();
        }
        else
        {
            await App.Current.MainPage.DisplayAlert("Dati mancanti",
                "Ehi non hai indicato l'ora del Memo", "OK");

            return;
        }

        if (DateTime.Parse(formatted) < DateTime.Now)
        {
            await App.Current.MainPage.DisplayAlert("Data non valida",
                "Ehi la data del memo è passata", "OK");

            return;
        }

        myMemo nuovoMemoItem = new myMemo
        {
            Data = DateTime.Parse(formatted),
            Descrizione = DescrizioneTXT.Text,
        };

        int esito = await database.AggiungiMemoItem(nuovoMemoItem);

        if (esito != 0)
        {
            await App.Current.MainPage.DisplayAlert("Inserimento Memo Eseguito",
                "...ho memorizzato il Tuo nuovo Memo", "OK");

            await Application.Current.MainPage.Navigation.PushModalAsync(new HomePage(), true);
        }
        else
        {
            await App.Current.MainPage.DisplayAlert("Inserimento Memo FALLITO",
                "mmmh qualcosa è andato storto...", "OK");

            await Application.Current.MainPage.Navigation.PushModalAsync(new HomePage(), true);
        }
    }

    private void OnButtonInfoAppClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage.Navigation.PushModalAsync(new InfoAppPage(), true);
    }

    private void OnButtonHomeClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage.Navigation.PushModalAsync(new HomePage(), true);
    }

    private void OnButtonGestisciClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage.Navigation.PushModalAsync(new DetailsPage(), true);
    }
}