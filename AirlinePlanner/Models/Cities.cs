using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using AirlinePlanner;

namespace AirlinePlanner.Models
{
  public class City
  {
    private int Id;
    private string CityName;

    public City(string newCityName, int newId =0)
    {
      Id = newId;
      CityName = newCityName;
    }
    public int GetId()
    {
      return Id;
    }
    public string GetCityName()
    {
      return CityName;
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO cities (CityName) VALUES (@inputCityName);";
      MySqlParameter newAircraftId = new MySqlParameter();
      newCityName.ParameterName = "@inputCityName";
      newCityName.Value = this.CityName;
      cmd.Parameters.Add(newCityName);
      cmd.ExecuteNonQuery();
      Id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM cities;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
       {
         conn.Dispose();
       }
    }
  }
}
