using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEMObyMAUI.Models
{
    internal class myMemo
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public DateTime Data { get; set; }
        public string Descrizione { get; set; }
        public bool Eseguito { get; set; }
    }
}
