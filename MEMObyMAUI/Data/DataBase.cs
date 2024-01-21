using MEMObyMAUI.Models;
using SQLite;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEMObyMAUI.Data
{
    internal class DataBase
    {
        private readonly SQLiteAsyncConnection connessione;

        public DataBase()
        {
            Batteries.Init();

            var dataDir = FileSystem.AppDataDirectory;        
            var databasePath = Path.Combine(dataDir, "memoDB.db");

            var dbStringaConnessione = new SQLiteConnectionString(databasePath);
            connessione = new SQLiteAsyncConnection(dbStringaConnessione);

            var risposta = InizializzaDB();
        }

        private async Task InizializzaDB()
        {
            await connessione.CreateTableAsync<myMemo>();
        }

        public async Task<int> AggiungiMemoItem(myMemo item)
        {
            return await connessione.InsertAsync(item);
        }

        public async Task<List<myMemo>> LeggiMemoItem()
        {
            return await connessione.Table<myMemo>().OrderBy(t => t.Data).ToListAsync();
        }

        public async Task<List<myMemo>> CercaMemoItem(string cercata)
        {
            return await connessione.Table<myMemo>()
              .Where(t => t.Descrizione.Contains(cercata))
                .ToListAsync();
        }

        public async Task<int> EliminaMemoItem(myMemo daEliminare)
        {
            return await connessione.DeleteAsync(daEliminare);
        }
    }
}
