using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace BestRestaurant
{
  public class Restaurant
  {
    private int Id;
    private string Name;
    private string Address;
    private int CuisineId;

    public Restaurant(string name, string address, int cuisineId, int id = 0)
    {
      Id = id;
      Name = name;
      Address = address;
      CuisineId = cuisineId;
    }

    public override bool Equals(System.Object otherRestauarant)
    {
      if(!(otherRestauarant is Restaurant))
      {
        return false;
      }
      else
      {
        Restaurant newRestaurant = (Restaurant) otherRestauarant;
        bool idEquality = this.GetId() == newRestaurant.GetId();
        bool nameEquality = this.GetName() == newRestaurant.GetName();
        bool addressEquality = this.GetAddress() == newRestaurant.GetAddress();
        bool cuisineIdEquality = this.GetCuisineId() == newRestaurant.GetCuisineId();

        return (idEquality && nameEquality && addressEquality && cuisineIdEquality);
      }
    }

    public int GetId()
    {
      return Id;
    }
    public string GetName()
    {
      return Name;
    }
    public string GetAddress()
    {
      return Address;
    }
    public int GetCuisineId()
    {
      return CuisineId;
    }

    public void SetId(int id)
    {
      Id= id;
    }
    public void SetName(string name)
    {
      Name = name;
    }
    public void SetAddress(string address)
    {
      Address = address;
    }
    public void SetCuisineId(int cuisineId)
    {
      CuisineId = cuisineId;
    }

    public static List<Restaurant> GetAll()
    {
      List<Restaurant> allRestaurants = new List<Restaurant> {};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM restaurants;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int restaurantId = rdr.GetInt32(0);
        string restaurantName = rdr.GetString(1);
        string restaurantAddress = rdr.GetString(2);
        int restaurantCuisineId = rdr.GetInt32(3);

        Restaurant newRestaurant = new Restaurant(restaurantName, restaurantAddress, restaurantCuisineId, restaurantId);
        allRestaurants.Add(newRestaurant);
      }

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn !=null)
      {
        conn.Close();
      }

      return allRestaurants;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO restaurants (name, address, cuisine_id) OUTPUT INSERTED.id VALUES(@RestaurantName, @RestaurantAddress, @RestaurantCuisineId);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@RestaurantName";
      nameParameter.Value = this.GetName();

      SqlParameter addressParameter = new SqlParameter();
      addressParameter.ParameterName = "@RestaurantAddress";
      addressParameter.Value = this.GetAddress();

      SqlParameter cuisineIdParameter = new SqlParameter();
      cuisineIdParameter.ParameterName = "@RestaurantCuisineId";
      cuisineIdParameter.Value = this.GetCuisineId();

      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(addressParameter);
      cmd.Parameters.Add(cuisineIdParameter); 
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this.Id = rdr.GetInt32(0);
      }

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn !=null)
      {
        conn.Close();
      }
    }

    public static Restaurant Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd= new SqlCommand("SELECT * FROM restaurants WHERE id = @RestaurantId;", conn);
      SqlParameter restaurantIdParameter = new SqlParameter();
      restaurantIdParameter.ParameterName = "@RestaurantId";
      restaurantIdParameter.Value = id.ToString();
      cmd.Parameters.Add(restaurantIdParameter);
      rdr = cmd.ExecuteReader();

      int foundRestaurantId = 0;
      string foundRestaurantName = null;
      string foundRestaurantAddress = null;
      int foundRestaurantCuisineId = 0;

      while(rdr.Read())
      {
        foundRestaurantId = rdr.GetInt32(0);
        foundRestaurantName = rdr.GetString(1);
        foundRestaurantAddress = rdr.GetString(2);
        foundRestaurantCuisineId = rdr.GetInt32(3);
      }
      Restaurant foundRestaurant = new Restaurant(foundRestaurantName, foundRestaurantAddress, foundRestaurantCuisineId, foundRestaurantId);

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return foundRestaurant;
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM restaurants;", conn);
      cmd.ExecuteNonQuery();
    }
  }
}
