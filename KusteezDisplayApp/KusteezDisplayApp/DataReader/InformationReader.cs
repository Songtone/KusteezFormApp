﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KusteezDisplayApp.Models;
using MySql.Data.MySqlClient;

namespace KusteezDisplayApp.DataReader
{
    public class InformationReader
    {
        public InformationReader GetInformation(FormInformation fi){
            

            InformationReader info = new InformationReader();
            string sql = "server=localhost;user id=root;password=1234;database=kusteez";
            MySqlConnection conn = new MySqlConnection(sql);
            MySqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = "select gamerTag, clothing from kusteezform";

            conn.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                fi.gamerTag = reader["gamerTag"].ToString();
                fi.clothingType = reader["clothing"].ToString();
            }

            return info;
            }
            
    }

    
}