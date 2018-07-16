using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using AirlinePlanner;

namespace AirlinePlanner.Models
{
  public class FlightDetail
  {
    private int Id;
    private string FlightId;
    private int DepartureCityId;
    private int DestinationCityId;
    private string DepartureTime;
    private string ArrivalTime;
    private string FlightStatus;

    public FlightDetail(string newFlightId, int newDepartureCityId, int newDestinationCityId, string newDepartureTime, string newArrivalTime, string newFlightStatus, int newId = 0)
    {
      Id = newId;
      FlightId = newFlightId;
      DepartureCityId = newDepartureCityId;
      DestinationCityId = newDestinationCityId;
      DepartureTime = newDepartureTime;
      ArrivalTime = newArrivalTime;
      FlightStatus = newFlightStatus;
    }
    public int GetId()
    {
      return Id;
    }
    public string GetFlightId()
    {
      return FlightId;
    }
    public int GetDepartureCityId()
    {
      return DepartureCityId;
    }
    public int GetDestinationCityId()
    {
      return ArrivalCityId;
    }
    public string GetDepartureTime()
    {
      return DepartureTime;
    }
    public string GetArrivalTime()
    {
      return ArrivalTime;
    }
    public string GetFlightStatus()
    {
      return FlightStatus;
    }

    public void SetFlightStatus(string newFlightStatus)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE flight_details SET flight_status = @editFlightStatus WHERE id = @id;";
      MySqlParameter editFlightStatus = new MySqlParameter();
      editFlightStatus.ParameterName = "@editFlightStatus";
      editFlightStatus.Value = newFlightStatus;
      cmd.Parameters.Add(editFlightStatus);
      MySqlParameter myId = new MySqlParameter();
      myId.ParameterName = "@id";
      myId.Value = this.Id;
      cmd.Parameters.Add(myId);
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public City GetDestinationCity()
    {
      int id = 0;
      string city = "";
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM cities JOIN flight_details ON (cities.id = flight_details.destination_city_id) WHERE flight_details.destination_city_id = @DestinationCity;";
      MySqlParameter FoundDestinationCity = new MySqlParameter();
      FoundDestinationCity.ParameterName = "@DestinationCity";
      FoundDestinationCity.Value = this.DestinationCityId;
      cmd.Parameters.Add(FoundDestinationCity);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        id = rdr.GetInt32(0);
        city = rdr.GetString(1);
      }
      City foundDestinationCity = new City(city, id);
      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
      return foundDestinationCity;
    }

    public City GetDepartureCity()
    {
      int id = 0;
      string city = "";
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM cities JOIN flight_details ON (cities.id = flight_details.departure_city_id) WHERE flight_details.departure_city_id = @DepartureCity;";
      MySqlParameter FoundDepartureCity = new MySqlParameter();
      FoundDepartureCity.ParameterName = "@DepartureCity";
      FoundDepartureCity.Value = this.DepartureCityId;
      cmd.Parameters.Add(FoundDepartureCity);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        id = rdr.GetInt32(0);
        city = rdr.GetString(1);
      }
      City foundDepartureCity = new City(city, id);
      conn.Close();
      if (conn !=null)
      {
        conn.Dispose();
      }
      return foundDepartureCity;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO flight_details (flight_id, departure_city_id, destination_city_id, departure_time, arrival_time, flight_status) VALUES (@inputFlightId, @inputDepartureCityId, @inputDestinationCityId, @inputDepartureTime, @inputArrivalTime, @inputFlightStatus);";
      MySqlParameter newFlightId = new MySqlParameter();
      newFlightId.ParameterName = "@inputFlightId";
      newFlightId.Value = this.FlightId;
      cmd.Parameters.Add(newFlightId);
      MySqlParameter newDepartureCityId = new MySqlParameter();
      newDepartureCityId.ParameterName = "@inputDepartureCityId";
      newDepartureCityId.Value = this.DepartureCityId;
      cmd.Parameters.Add(newDepartureCityId);
      MySqlParameter newDestinationCityId = new MySqlParameter();
      newDestinationCityId.ParameterName = "@inputDestinationCityId";
      newDestinationCityId.Value = this.DestinationCityId;
      cmd.Parameters.Add(newDestinationCityId);
      MySqlParameter newDepartureTime = new MySqlParameter();
      newDepartureTime.ParameterName = "@inputDepartureTime";
      newDepartureTime.Value = this.DepartureTime;
      cmd.Parameters.Add(newDepartureTime);
      MySqlParameter newArrivalTime = new MySqlParameter();
      newArrivalTime.ParameterName = "@inputArrivalTime";
      newArrivalTime.Value = this.ArrivalTime;
      cmd.Parameters.Add(newArrivalTime);
      MySqlParameter newFlightStatus = new MySqlParameter();
      newFlightStatus.ParameterName = "@inputFlightStatus";
      newFlightStatus.Value = this.FlightStatus;
      cmd.Parameters.Add(newFlightStatus);
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
      cmd.CommandText = @"DELETE FROM flight_details;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
       {
         conn.Dispose();
       }
    }
  }
}
