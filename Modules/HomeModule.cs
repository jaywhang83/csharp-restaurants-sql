using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;
using System;

namespace BestRestaurant
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ =>
      {
        List<Cuisine> allCuisines = Cuisine.GetAll();
        return View["index.cshtml", allCuisines];
      };
      Get["/restaurants"] = _ =>
      {
        List<Restaurant> allRestaurants = Restaurant.GetAll();
        return View["restaurants.cshtml", allRestaurants];
      };
      Get["/cuisines/new"] = _ =>
      {
        return View["cuisines_form.cshtml"];
      };
      Post["/cuisines/new"] = _ =>
      {
        Cuisine newCuisine = new Cuisine(Request.Form["cuisine-name"]);
        newCuisine.Save();
        return View["success.cshtml"];
      };
      Get["/restaurants/new"] = _ =>
      {
        List<Cuisine> allCuisines = Cuisine.GetAll();
        return View["restaurants_form.cshtml", allCuisines];
      };
      Post["/restaurants/new"] = _ =>
      {
        Restaurant newRestaurant = new Restaurant(Request.Form["restaurant-name"], Request.Form["restaurant-address"], Request.Form["cuisine-id"]);
        newRestaurant.Save();
        return View["success.cshtml"];
      };
      Post["/restaurants/delete"] = _ =>
      {
        Restaurant.DeleteAll();
        return View["deleted.cshtml"];
      };
      Get["/cuisines/{id}"] = parameters =>
      {
        Dictionary<string, object> model = new Dictionary<string, object>();
        var selectedCuisine = Cuisine.Find(parameters.id);
        var cuisineRestaurants = selectedCuisine.GetRestaurants();
        model.Add("cuisine", selectedCuisine);
        model.Add("restaurants", cuisineRestaurants);
        return View["cuisine.cshtml", model];
      };
      Post["/cuisines/delete"] = _ =>
      {
        Cuisine.DeleteAll();
        return View["deleted.cshtml"];
      };
      Get["/cuisine/edit/{id}"]= parameters =>
      {
        Cuisine selectedCuisine = Cuisine.Find(parameters.id);
        return View["cuisine_edit.cshtml", selectedCuisine];
      };
      Patch["/cuisine/edit/{id}"]= parameters =>
      {
        Cuisine selectedCuisine = Cuisine.Find(parameters.id);
        selectedCuisine.Update(Request.Form["cuisine-name"]);
        return View["success.cshtml"];
      };
      Get["/cuisine/delete/{id}"] = parameters =>
      {
        Cuisine selectedCuisine = Cuisine.Find(parameters.id);
        return View["cuisine_delete.cshtml", selectedCuisine];
      };
      Delete["/cuisine/delete/{id}"] = parameters =>
      {
        Cuisine selectedCuisine = Cuisine.Find(parameters.id);
        selectedCuisine.Delete();
        return View["deleted.cshtml"];
      };
    }
  }
}
