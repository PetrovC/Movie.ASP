using MovieProject.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MovieProject.DAL.Services
{
    public class MovieService : IMovieService
    {
        private readonly SqlConnection _conn;
        public MovieService(SqlConnection conn)
        {
            _conn = conn;
        }
        public Movie GetOneById(int id)
        {
            try
            {
                _conn.Open();
                SqlCommand command = _conn.CreateCommand();
                command.CommandText = @"
                    SELECT M.*, C.[Name] CategoryName 
                    FROM Movie M
                    LEFT JOIN Category C ON M.CategoryId = C.Id
                    WHERE M.Id = @id
                ";
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader();
                Movie movie = new Movie();
                while (reader.Read())
                {
                    movie.Id = (int)reader["Id"];
                    movie.Title = reader["Title"].ToString();
                    movie.Poster = reader["Poster"] as string;
                    movie.Duration = (TimeSpan?)reader["Duration"];
                    movie.CategoryId = reader["CategoryId"] as int?;
                    movie.BoxOffice = (decimal)reader["BoxOffice"];
                    movie.Director = reader["Director"].ToString();
                    if (movie.CategoryId != null)
                    {
                        movie.Category = new Category
                        {
                            Id = (int)reader["CategoryId"],
                            Name = (string)reader["CategoryName"]
                        };
                    }
                }
                return movie;
            }
            catch (Exception) { throw; }
            finally { _conn.Close(); }
        }
        public List<Movie> GetWithCategory() // jointure
        {
            try
            {
                _conn.Open();
                SqlCommand command = _conn.CreateCommand();
                command.CommandText = @"
                    SELECT M.*, C.[Name] CategoryName  
                    FROM Movie M
                    LEFT JOIN Category C ON M.CategoryId = C.Id
                ";
                SqlDataReader reader = command.ExecuteReader();
                List<Movie> movieList = new List<Movie>();
                while (reader.Read())
                {
                    Movie movie = new Movie();
                    movie.Id = (int)reader["Id"];
                    movie.Title = (string)reader["Title"];
                    movie.Poster = reader["Poster"] as string;
                    movie.Duration = reader["Duration"] as TimeSpan?;
                    movie.CategoryId = reader["CategoryId"] as int?;
                    movie.BoxOffice = (decimal)reader["BoxOffice"];
                    if (movie.CategoryId != null)
                    {
                        movie.Category = new Category
                        {
                            Id = (int)reader["CategoryId"],
                            Name = (string)reader["CategoryName"]
                        };
                    }
                    movieList.Add(movie);
                }
                return movieList;
            }
            catch (Exception) { throw; }
            finally { _conn.Close(); }
        }
        public int Add(Movie m)
        {
            try
            {
                _conn.Open();
                SqlCommand cmd = _conn.CreateCommand();
                cmd.CommandText = "INSERT INTO Movie(Title, Director, CategoryId, Poster, Duration, BoxOffice) ";
                cmd.CommandText += "OUTPUT Inserted.Id VALUES(@p1, @p2, @p3, @p4, @p5, @p6)";
                cmd.Parameters.AddWithValue("p1", m.Title);
                cmd.Parameters.AddWithValue("p2", m.Director);
                cmd.Parameters.AddWithValue("p3", (object)m.CategoryId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("p4", (object)m.Poster ?? DBNull.Value);
                cmd.Parameters.AddWithValue("p5", (object)m.Duration ?? DBNull.Value);
                cmd.Parameters.AddWithValue("p6", m.BoxOffice);
                return (int)cmd.ExecuteScalar();
            }
            catch (Exception) { throw; }
            finally { _conn.Close(); }
        }
        public void Update(Movie m)
        {
            try
            {
                _conn.Open();
                SqlCommand cmd = _conn.CreateCommand();
                cmd.CommandText = @"UPDATE Movie SET Title = @p1, Duration = @p5, Director = @p2, BoxOffice = @p6";
                cmd.CommandText += ", Poster = @p4, CategoryId = @p3 WHERE Id = @id";
                cmd.Parameters.AddWithValue("p1", m.Title);
                cmd.Parameters.AddWithValue("p2", m.Director);
                cmd.Parameters.AddWithValue("p3", (object)m.CategoryId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("p4", (object)m.Poster ?? DBNull.Value);
                cmd.Parameters.AddWithValue("p5", (object)m.Duration ?? DBNull.Value);
                cmd.Parameters.AddWithValue("p6", m.BoxOffice);
                cmd.Parameters.AddWithValue("id", m.Id);
                cmd.ExecuteNonQuery();
            }
            catch (Exception) { throw; }
            finally { _conn.Close(); }
        }
        public void Delete(int id)
        {
            try
            {
                _conn.Open();
                SqlCommand command = _conn.CreateCommand();
                command.CommandText = @"DELETE FROM Movie WHERE Id = @id";
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
            catch (Exception) { throw; }
            finally { _conn.Close(); }
        }
    }
}
