using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;
using System;

namespace BestRestaurant
{
  public class Review
  {
    private int Id;
    private string UserName;
    private DateTime PostDate;
    private string Comment;
    private int Rating;
    private int RestaurantId;


    public Review(string userName, DateTime postDate, string comment, int rating, int restaurantId, int id = 0)
    {
      Id = id;
      UserName = userName;
      PostDate = postDate;
      Comment = comment;
      Rating = rating;
      RestaurantId = restaurantId;
    }

    public override bool Equals(System.Object otherReview)
    {
      if(!(otherReview is Revew))
      {
        return false;
      }
      else
      {
        Review newReview = (Review) otherReview;
        bool idEquality = this.GetId() == newReview.GetId();
        bool userNameEquality = this.GetUserName() == newReview.GetUserName();
        bool dateEquality = this.GetDate() == newReview.GetDate();
        bool commentEquality = this.GetComment() == newReview.GetComment();
        bool ratingEquality = this.GetRating()== newReview.GetRating();
        bool restaurantIdEquality = this.GetRestaurantId() == newReview.GetRestaurantId();

        return (idEquality && userNameEquality && dateEquality && commentEquality && ratingEquality && ratingEquality && restaurantIdEquality);
      }
    }

    public int GetId()
    {
      return Id;
    }
    public string GetUserName()
    {
      return UserName;
    }
    public DateTime GetDate()
    {
      return PostDate;
    }
    public string GetComment()
    {
      return Comment;
    }
    public int GetRating()
    {
      return Rating;
    }
    public int GetRestaurantId()
    {
      return RestaurantId;
    }

    public void SetId(int id)
    {
      Id = id;
    }
    public void SetUserName(string userName)
    {
      UserName = userName;
    }
    public void SetDate(DateTime postDate)
    {
      PostDate = postDate;
    }
    public void SetComment(string comment)
    {
      Comment = comment;
    }
    public void SetRating(int rating)
    {
      Rating = rating;
    }
    public void SetRestaurantId(int restaurantId)
    {
      RestaurantId = restaurantId;
    }

    public static List<Review> GetAll()
    {
      List<Review> allReviews = new List<Review>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM reviews ORDER BY postDate ASC;", conn);
      rdr = md.ExecuteReader();

      while(rdr.Read())
      {
        int reviewId = rdr.GetInt32(0);
        string reviewUserName = rdr.GetString(1);
        DateTime reviewDate = rdr.GetDateTime(2);
        string reviewComment = rdr.GetString(3);
        int reviewRating = rdr.GetInt32(4);
        int reviewRestaurantId = rdr.GetInt32(5);
        Review newReview = new Review(reviewUserName, reviewDate, reviewComment, reviewRating, reviewRestaurantId, reviewId);
        allReviews.Add(newReview);
      }

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
      return allReviews;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO reviews (user_name, post_date, comment, rating, restaurant_id) OUTPUT INSERTED.id VALUES(@ReviewUserName, @ReviewPostDate, @ReviewComment, @ReviewRating, @ReviewRestaurantId);", conn);

      SqlParameter userNameParameter = new SqlParameter();
      userNameParameter.ParameterName = "@ReviewUserName";
      userNameParameter.Value = this.GetUserName();

      SqlParameter postDateParameter = new SqlParameter();
      postDateParameter.ParameterName = "@ReviewPostDate";
      postDateParameter.Value = this.GetDate();

      SqlParameter commentParameter = new SqlParameter();
      commentParameter.ParameterName = "@ReviewComment";
      commentParameter.Value = this.GetComment();

      SqlParameter ratingParameter = new SqlParameter();
      ratingParameter.ParameterName = "@ReviewRating";
      ratingParameter.Value = this.GetRating();

      SqlParameter restaurantIdParameter = new SqlParameter();
      restaurantIdParameter.ParameterName = "@ReviewRestaurantId";
      restaurantIdParameter.Value = this.GetRestaurantId();

      cmd.Parameters.Add(userNameParameter);
      cmd.Parameters.Add(postDateParameter);
      cmd.Parameters.Add(commentParameter);
      cmd.Parameters.Add(ratingParameter);
      cmd.Parameters.Add(restaurantIdParameter);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this.Id = rdr.GetInt32(0);
      }
      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        con.Close();
      }
    }

    public static Review Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM reviews WHERE id = @ReviewId;", conn);
      SqlParameter reviewIdParameter = new SqlParameter();
      reviewIdParameter.ParameterName = "@ReviewId";
      reviewIdParameter.Value = id.ToString();
      cmd.Parameters.Add(reviewIdParameter);
      rdr = cmd.ExecuteReader();

      int foundReviewId = 0;
      string foundReviewUserName = null;
      DateTime foundReviewPostDate = new DateTime();
      string foundReviewComment = null;
      int foundReviewRating = 0;
      int foundReviewRestaurantId = 0;

      while(rdr.Read())
      {
        foundReviewId = rdr.GetInt32(0);
        foundReviewUserName = rdr.GetString(1);
        foundReviewPostDate = rdr.GetDateTime(2);
        foundReviewComment = rdr.GetString(3);
        foundReviewRating = rdr.GetInt32(4);
        foundReviewRestaurantId = rdr.GetInt32(5);
      }
      Review foundReview = new Review(foundReviewUserName, foundReviewPostDate, foundReviewComment, foundReviewRating, foundReviewRestaurantId, foundReviewId);

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn !=null)
      {
        conne.Close();
      }

      return foundReview;
    }

    public static void DeleteAll()
    {
      SqlConnection = DB.Connection();
      conn.Open();
      SqlCommand cmd = newSqlCommand("DELETE FROM reviews;", conn);
      cmd.ExecuteNonQuery(); 
    }
  }
}
