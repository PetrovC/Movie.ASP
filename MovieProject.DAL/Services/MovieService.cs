using MovieProject.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data;
//using System.Data.SqlClient; //use with old one

namespace MovieProject.DAL.Services
{
    #region Old Connection
    //public class MovieService : IMovieService
    //{
    //    private readonly SqlConnection _conn;
    //    public MovieService(SqlConnection conn)
    //    {
    //        _conn = conn;
    //    }
    //    public Movie GetOneById(int id)
    //    {
    //        try
    //        {
    //            _conn.Open();
    //            SqlCommand command = _conn.CreateCommand();
    //            command.CommandText = @"
    //                SELECT M.*, C.[Name] CategoryName 
    //                FROM Movie M
    //                LEFT JOIN Category C ON M.CategoryId = C.Id
    //                WHERE M.Id = @id
    //            ";
    //            command.Parameters.AddWithValue("@id", id);
    //            SqlDataReader reader = command.ExecuteReader();
    //            Movie movie = new Movie();
    //            while (reader.Read())
    //            {
    //                movie.Id = (int)reader["Id"];
    //                movie.Title = reader["Title"].ToString();
    //                movie.Poster = reader["Poster"] as string;
    //                movie.Duration = (TimeSpan?)reader["Duration"];
    //                movie.CategoryId = reader["CategoryId"] as int?;
    //                movie.BoxOffice = (decimal)reader["BoxOffice"];
    //                movie.Director = reader["Director"].ToString();
    //                if (movie.CategoryId != null)
    //                {
    //                    movie.Category = new Category
    //                    {
    //                        Id = (int)reader["CategoryId"],
    //                        Name = (string)reader["CategoryName"]
    //                    };
    //                }
    //            }
    //            return movie;
    //        }
    //        catch (Exception) { throw; }
    //        finally { _conn.Close(); }
    //    }
    //    public List<Movie> GetWithCategory() // jointure
    //    {
    //        try
    //        {
    //            _conn.Open();
    //            SqlCommand command = _conn.CreateCommand();
    //            command.CommandText = @"
    //                SELECT M.*, C.[Name] CategoryName  
    //                FROM Movie M
    //                LEFT JOIN Category C ON M.CategoryId = C.Id
    //            ";
    //            SqlDataReader reader = command.ExecuteReader();
    //            List<Movie> movieList = new List<Movie>();
    //            while (reader.Read())
    //            {
    //                Movie movie = new Movie();
    //                movie.Id = (int)reader["Id"];
    //                movie.Title = (string)reader["Title"];
    //                movie.Poster = reader["Poster"] as string;
    //                movie.Duration = reader["Duration"] as TimeSpan?;
    //                movie.CategoryId = reader["CategoryId"] as int?;
    //                movie.BoxOffice = (decimal)reader["BoxOffice"];
    //                if (movie.CategoryId != null)
    //                {
    //                    movie.Category = new Category
    //                    {
    //                        Id = (int)reader["CategoryId"],
    //                        Name = (string)reader["CategoryName"]
    //                    };
    //                }
    //                movieList.Add(movie);
    //            }
    //            return movieList;
    //        }
    //        catch (Exception) { throw; }
    //        finally { _conn.Close(); }
    //    }
    //    public int Add(Movie m)
    //    {
    //        try
    //        {
    //            _conn.Open();
    //            SqlCommand cmd = _conn.CreateCommand();
    //            cmd.CommandText = "INSERT INTO Movie(Title, Director, CategoryId, Poster, Duration, BoxOffice) ";
    //            cmd.CommandText += "OUTPUT Inserted.Id VALUES(@p1, @p2, @p3, @p4, @p5, @p6)";
    //            cmd.Parameters.AddWithValue("p1", m.Title);
    //            cmd.Parameters.AddWithValue("p2", m.Director);
    //            cmd.Parameters.AddWithValue("p3", (object)m.CategoryId ?? DBNull.Value);
    //            cmd.Parameters.AddWithValue("p4", (object)m.Poster ?? DBNull.Value);
    //            cmd.Parameters.AddWithValue("p5", (object)m.Duration ?? DBNull.Value);
    //            cmd.Parameters.AddWithValue("p6", m.BoxOffice);
    //            return (int)cmd.ExecuteScalar();
    //        }
    //        catch (Exception) { throw; }
    //        finally { _conn.Close(); }
    //    }
    //    public void Update(Movie m)
    //    {
    //        try
    //        {
    //            _conn.Open();
    //            SqlCommand cmd = _conn.CreateCommand();
    //            cmd.CommandText = @"UPDATE Movie SET Title = @p1, Duration = @p5, Director = @p2, BoxOffice = @p6";
    //            cmd.CommandText += ", Poster = @p4, CategoryId = @p3 WHERE Id = @id";
    //            cmd.Parameters.AddWithValue("p1", m.Title);
    //            cmd.Parameters.AddWithValue("p2", m.Director);
    //            cmd.Parameters.AddWithValue("p3", (object)m.CategoryId ?? DBNull.Value);
    //            cmd.Parameters.AddWithValue("p4", (object)m.Poster ?? DBNull.Value);
    //            cmd.Parameters.AddWithValue("p5", (object)m.Duration ?? DBNull.Value);
    //            cmd.Parameters.AddWithValue("p6", m.BoxOffice);
    //            cmd.Parameters.AddWithValue("id", m.Id);
    //            cmd.ExecuteNonQuery();
    //        }
    //        catch (Exception) { throw; }
    //        finally { _conn.Close(); }
    //    }
    //    public void Delete(int id)
    //    {
    //        try
    //        {
    //            _conn.Open();
    //            SqlCommand command = _conn.CreateCommand();
    //            command.CommandText = @"DELETE FROM Movie WHERE Id = @id";
    //            command.Parameters.AddWithValue("@id", id);
    //            command.ExecuteNonQuery();
    //        }
    //        catch (Exception) { throw; }
    //        finally { _conn.Close(); }
    //    }
    //} 
    #endregion
    #region New Connection
    public class MovieService : IMovieService
    {
        private readonly IDbConnection _conn;
        public MovieService(IDbConnection conn)
        {
            _conn = conn;
        }
        public Movie GetOneById(int id)
        {
            try
            {
                _conn.Open();
                IDbCommand command = _conn.CreateCommand();
                command.CommandText = @"
                    SELECT M.*, C.[Name] CategoryName 
                    FROM Movie M
                    LEFT JOIN Category C ON M.CategoryId = C.Id
                    WHERE M.Id = @id
                ";
                var parameter1 = command.CreateParameter();
                parameter1.ParameterName = "id";
                parameter1.Value = id;
                command.Parameters.Add(parameter1);
                IDataReader reader = command.ExecuteReader();
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
                IDbCommand command = _conn.CreateCommand();
                command.CommandText = @"
                    SELECT M.*, C.[Name] CategoryName  
                    FROM Movie M
                    LEFT JOIN Category C ON M.CategoryId = C.Id
                ";
                IDataReader reader = command.ExecuteReader();
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
                IDbCommand cmd = _conn.CreateCommand();
                cmd.CommandText = "INSERT INTO Movie(Title, Director, CategoryId, Poster, Duration, BoxOffice) ";
                cmd.CommandText += "OUTPUT Inserted.Id VALUES(@p1, @p2, @p3, @p4, @p5, @p6)";
                var parameter1 = cmd.CreateParameter();
                parameter1.ParameterName = "p1";
                parameter1.Value = m.Title;
                cmd.Parameters.Add(parameter1);
                var parameter2 = cmd.CreateParameter();
                parameter2.ParameterName = "p2";
                parameter2.Value = m.Director;
                cmd.Parameters.Add(parameter2);
                var parameter3 = cmd.CreateParameter();
                parameter3.ParameterName = "p5";
                parameter3.Value = (object)m.Duration ?? DBNull.Value;
                cmd.Parameters.Add(parameter3);
                var parameter4 = cmd.CreateParameter();
                parameter4.ParameterName = "p6";
                parameter4.Value = m.BoxOffice;
                cmd.Parameters.Add(parameter4);
                var parameter5 = cmd.CreateParameter();
                parameter5.ParameterName = "p4";
                parameter5.Value = m.Poster == null ? DBNull.Value : m.Poster;
                cmd.Parameters.Add(parameter5);
                var parameter6 = cmd.CreateParameter();
                parameter6.ParameterName = "p3";
                parameter6.Value = (object)m.CategoryId ?? DBNull.Value;
                cmd.Parameters.Add(parameter6);
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
                IDbCommand cmd = _conn.CreateCommand();
                cmd.CommandText = @"UPDATE Movie SET Title = @p1, Duration = @p5, Director = @p2, BoxOffice = @p6";
                cmd.CommandText += ", Poster = @p4, CategoryId = @p3 WHERE Id = @id";
                var parameter1 = cmd.CreateParameter();
                parameter1.ParameterName = "p1";
                parameter1.Value = m.Title;
                cmd.Parameters.Add(parameter1);
                var parameter2 = cmd.CreateParameter();
                parameter2.ParameterName = "p2";
                parameter2.Value = m.Director;
                cmd.Parameters.Add(parameter2);
                var parameter3 = cmd.CreateParameter();
                parameter3.ParameterName = "p5";
                parameter3.Value = (object)m.Duration ?? DBNull.Value;
                cmd.Parameters.Add(parameter3);
                var parameter4 = cmd.CreateParameter();
                parameter4.ParameterName = "p6";
                parameter4.Value = m.BoxOffice;
                cmd.Parameters.Add(parameter4);
                var parameter5 = cmd.CreateParameter();
                parameter5.ParameterName = "p4";
                parameter5.Value = m.Poster == null ? DBNull.Value : m.Poster;
                cmd.Parameters.Add(parameter5);
                var parameter6 = cmd.CreateParameter();
                parameter6.ParameterName = "p3";
                parameter6.Value = (object)m.CategoryId ?? DBNull.Value;
                cmd.Parameters.Add(parameter6);
                var parameter7 = cmd.CreateParameter();
                parameter7.ParameterName = "id";
                parameter7.Value = m.Id;
                cmd.Parameters.Add(parameter7);
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
                IDbCommand command = _conn.CreateCommand();
                command.CommandText = @"DELETE FROM Movie WHERE Id = @id";
                var parameter1 = command.CreateParameter();
                parameter1.ParameterName = "id";
                parameter1.Value = id;
                command.Parameters.Add(parameter1);
                command.ExecuteNonQuery();
            }
            catch (Exception) { throw; }
            finally { _conn.Close(); }
        }
    } 
    #endregion
}
