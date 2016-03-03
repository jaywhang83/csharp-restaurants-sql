using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BestRestaurant
{
  public class CuisineTest : IDisposable
  {
    public CuisineTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=restaurant_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_CuisinesEmptyAtFirst()
    {
      int result = Cuisine.GetAll().Count;

      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_ReturnsTrueForSameName()
    {
      Cuisine firstCuisine = new Cuisine("Sushi");
      Cuisine secondCuisine = new Cuisine("Sushi");

      Assert.Equal(firstCuisine, secondCuisine);
    }

    [Fact]
    public void Test_Save_SavesCuisineToDatabase()
    {
      Cuisine testCuisine = new Cuisine("Sushi");
      testCuisine.Save();

      List<Cuisine> result = Cuisine.GetAll();
      List<Cuisine> testList = new List<Cuisine> {testCuisine};

      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Save_AssignsIdToCuisineObject()
    {
      Cuisine testCuisine = new Cuisine("Sushi");
      testCuisine.Save();

      Cuisine savedCuisine = Cuisine.GetAll()[0];

      int result = savedCuisine.GetId();
      int testId = testCuisine.GetId();

      Assert.Equal(testId, result);
    }

    [Fact]
    public void Test_Find_FindsCuisineInDatabase()
    {
      Cuisine testCuisine = new Cuisine("Sushi");
      testCuisine.Save();

      Cuisine foundCuisine = Cuisine.Find(testCuisine.GetId());

      Assert.Equal(testCuisine, foundCuisine);
    }

    [Fact]
    public void Test_GetRestaurants_RetrievesAllRestaurantsWithCuisine()
    {
      Cuisine testCuisine = new Cuisine("Sushi");
      testCuisine.Save();

      Restaurant firstRestaurant = new Restaurant("Bamboo", "123 SW 5th ave, Portland Oregon", testCuisine.GetId());
      firstRestaurant.Save();
      Restaurant secondRestaurant = new Restaurant("Mio Sushi", "SW 23rd ave, Portland Oregon", testCuisine.GetId());
      secondRestaurant.Save();

      List<Restaurant> testRestaurantList = new List<Restaurant> {firstRestaurant, secondRestaurant};
      List<Restaurant> resultRestaurantList = testCuisine.GetRestaurants();

      Assert.Equal(testRestaurantList, resultRestaurantList);
    }

    [Fact]
    public void Test_Update_UpdateCuisineInDatebase()
    {
      string name = "Sushi";
      Cuisine testCuisine = new Cuisine(name);
      testCuisine.Save();
      string newCuisine = "Pho";

      testCuisine.Update(newCuisine);

      string result = testCuisine.GetName();

      Assert.Equal(newCuisine, result);
    }
    [Fact]
    public void Test_Delete_DeletesCuisineFromDatabase()
    {
      string name1 = "Sushi";
      Cuisine testCuisine1 = new Cuisine(name1);
      testCuisine1.Save();

      string name2 = "Pho";
      Cuisine testCuisine2 = new Cuisine(name2);
      testCuisine2.Save();

      Restaurant testRestaurant1 = new Restaurant("Bamboo", "123 SW 5th ave, Portland Oregon", testCuisine1.GetId());
      testRestaurant1.Save();
      Restaurant testRestaurant2 = new Restaurant("Pho Zen", "SW 23rd ave, Portland Oregon", testCuisine2.GetId());
      testRestaurant2.Save();

      testCuisine1.Delete();
      List<Cuisine> resultCuisines = Cuisine.GetAll();
      List<Cuisine> testCuisineList = new List<Cuisine> {testCuisine2};

      List<Restaurant> resultRestaurants = Restaurant.GetAll();
      List<Restaurant> testRestaurantList = new List<Restaurant> {testRestaurant2};

      Assert.Equal(testCuisineList, resultCuisines);
      Assert.Equal(testRestaurantList, resultRestaurants);
    }

    public void Dispose()
    {
      Cuisine.DeleteAll();
      Restaurant.DeleteAll();
    }
  }
}
