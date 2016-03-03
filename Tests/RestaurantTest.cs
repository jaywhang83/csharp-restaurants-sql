using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BestRestaurant
{
  public class RestaurantTest : IDisposable
  {
    public RestaurantTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=restaurant_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      int result = Restaurant.GetAll().Count;

      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_OverrideTrueForSameName()
    {
      Restaurant firstRestaurant = new Restaurant("Bamboo", "123 SW 5th ave, Portland Oregon", 1);
      Restaurant secondRestaurant = new Restaurant("Bamboo", "123 SW 5th ave, Portland Oregon", 1);

      Assert.Equal(firstRestaurant, secondRestaurant);
    }

    [Fact]
    public void Test_Save()
    {
      Restaurant testRestaurant = new Restaurant("Bamboo", "123 SW 5th ave, Portland Oregon", 1);
      testRestaurant.Save();

      List<Restaurant> result = Restaurant.GetAll();
      List<Restaurant> testList = new List<Restaurant>{testRestaurant};

      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Save_AssignsIdToObject()
    {
      Restaurant testRestaurant = new Restaurant("Bamboo", "123 SW 5th ave, Portland Oregon", 1);

      testRestaurant.Save();

      // List<Restaurant> testSavedRestaurant = Restaurant.GetAll();
      Restaurant savedRestaurant = Restaurant.GetAll()[0];

      int result = savedRestaurant.GetId();
      int testId = testRestaurant.GetId();

      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_Find_FindsRestaurantInDatabase()
    {
      Restaurant testRestaurant = new Restaurant("Bamboo", "123 SW 5th ave, Portland Oregon", 1);
      testRestaurant.Save();

      Restaurant foundRestaurant = Restaurant.Find(testRestaurant.GetId());

      Assert.Equal(testRestaurant, foundRestaurant);
    }

    public void Dispose()
    {
      Restaurant.DeleteAll();
    }
  }
}
