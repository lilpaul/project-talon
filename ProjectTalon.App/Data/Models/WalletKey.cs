﻿using SQLite;

namespace ProjectTalon.App.Data.Models
{
    public class WalletKey
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int WalletId { get; set; }
        public int KeyType { get; set; }
        public string Skey { get; set; }
        public string Vkey { get; set; }
        public string Chaincode { get; set; }
        public int KeyIndex { get; set; }
        public int AccountIndex { get; set; }
    }
}
