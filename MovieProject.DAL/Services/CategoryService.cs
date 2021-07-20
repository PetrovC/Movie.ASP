using MovieProject.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data;
//using System.Data.SqlClient; //use with old one

namespace MovieProject.DAL.Services
{
    #region Old Connection
    //public class CategoryService : ICategoryService
    //{
    //    private readonly SqlConnection _conn;
    //    public CategoryService(SqlConnection conn)
    //    {
    //        _conn = conn;
    //    }
    //    public List<Category> Get()
    //    {
    //        try
    //        {
    //            _conn.Open();
    //            SqlCommand command = _conn.CreateCommand();
    //            command.CommandText = "SELECT * FROM Category";
    //            List<Category> result = new List<Category>();
    //            SqlDataReader reader = command.ExecuteReader();
    //            while (reader.Read())
    //            {
    //                result.Add(new Category
    //                {
    //                    Id = (int)reader["Id"],
    //                    Name = (string)reader["Name"]
    //                });
    //            }
    //            return result;
    //        }
    //        catch (Exception) { throw; }
    //        finally { _conn.Close(); }
    //    }
    //    public Category GetById(int id)
    //    {
    //        try
    //        {
    //            _conn.Open();
    //            SqlCommand command = _conn.CreateCommand();
    //            command.CommandText = "SELECT * FROM Category WHERE Id = @id";
    //            command.Parameters.AddWithValue("@id", id);
    //            SqlDataReader reader = command.ExecuteReader();
    //            Category Cat = new Category();
    //            while (reader.Read())
    //            {

    //                Cat.Id = (int)reader["Id"];
    //                Cat.Name = (string)reader["Name"];

    //            }
    //            return Cat;
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
    //            command.CommandText = @"DELETE FROM Category WHERE Id = @id";
    //            command.Parameters.AddWithValue("@id", id);
    //            command.ExecuteNonQuery();
    //        }
    //        catch (Exception) { throw; }
    //        finally { _conn.Close(); }
    //    }
    //    public int Add(Category c)
    //    {
    //        try
    //        {
    //            _conn.Open();
    //            SqlCommand cmd = _conn.CreateCommand();
    //            cmd.CommandText = "INSERT INTO Category(Name) OUTPUT Inserted.Id VALUES(@p1)";
    //            cmd.Parameters.AddWithValue("@p1", c.Name);
    //            return (int)cmd.ExecuteScalar();
    //        }
    //        catch (Exception) { throw; }
    //        finally { _conn.Close(); }
    //    }
    //    public void Update(Category c)
    //    {
    //        try
    //        {
    //            _conn.Open();
    //            SqlCommand cmd = _conn.CreateCommand();
    //            cmd.CommandText = @"UPDATE Category SET Name = @p1 WHERE Id = @id";
    //            cmd.Parameters.AddWithValue("@p1", c.Name);
    //            cmd.Parameters.AddWithValue("@id", c.Id);
    //            cmd.ExecuteNonQuery();
    //        }
    //        catch (Exception) { throw; }
    //        finally { _conn.Close(); }
    //    }
    //} 
    #endregion
    #region New Connection
    public class CategoryService : ICategoryService
    {
        private readonly IDbConnection _conn;
        public CategoryService(IDbConnection conn)
        {
            _conn = conn;
        }
        public List<Category> Get()
        {
            try
            {
                _conn.Open();
                IDbCommand command = _conn.CreateCommand();
                command.CommandText = "SELECT * FROM Category";
                List<Category> result = new List<Category>(); 
                IDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new Category
                    {
                        Id = (int)reader["Id"],
                        Name = (string)reader["Name"]
                    });
                }
                return result;
            }
            catch (Exception) { throw; }
            finally { _conn.Close(); }
        }
        public Category GetById(int id)
        {
            try
            {
                _conn.Open();
                IDbCommand command = _conn.CreateCommand();
                command.CommandText = "SELECT * FROM Category WHERE Id = @id";
                var parameter1 = command.CreateParameter();
                parameter1.ParameterName = "id";
                parameter1.Value = id;
                command.Parameters.Add(parameter1);
                IDataReader reader = command.ExecuteReader();
                Category Cat = new Category();
                while (reader.Read())
                {

                    Cat.Id = (int)reader["Id"];
                    Cat.Name = (string)reader["Name"];

                }
                return Cat;
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
                command.CommandText = @"DELETE FROM Category WHERE Id = @id";
                var parameter1 = command.CreateParameter();
                parameter1.ParameterName = "id";
                parameter1.Value = id;
                command.Parameters.Add(parameter1);
                command.ExecuteNonQuery();
            }
            catch (Exception) { throw; }
            finally { _conn.Close(); }
        }
        public int Add(Category c)
        {
            try
            {
                _conn.Open();
                IDbCommand cmd = _conn.CreateCommand();
                cmd.CommandText = "INSERT INTO Category(Name) OUTPUT Inserted.Id VALUES(@p1)";
                var parameter1 = cmd.CreateParameter();
                parameter1.ParameterName = "p1";
                parameter1.Value = c.Name;
                cmd.Parameters.Add(parameter1);
                return (int)cmd.ExecuteScalar();
            }
            catch (Exception) { throw; }
            finally { _conn.Close(); }
        }
        public void Update(Category c)
        {
            try
            {
                _conn.Open();
                IDbCommand cmd = _conn.CreateCommand();
                cmd.CommandText = @"UPDATE Category SET Name = @p1 WHERE Id = @id";
                var parameter1 = cmd.CreateParameter();
                parameter1.ParameterName = "p1";
                parameter1.Value = c.Name;
                cmd.Parameters.Add(parameter1);
                var parameter2 = cmd.CreateParameter();
                parameter2.ParameterName = "id";
                parameter2.Value = c.Id;
                cmd.Parameters.Add(parameter2);
                cmd.ExecuteNonQuery();
            }
            catch (Exception) { throw; }
            finally { _conn.Close(); }
        }
        public List<Movie> GetFilmByCategory(int id) // jointure
        {
            try
            {
                _conn.Open();
                IDbCommand command = _conn.CreateCommand();
                command.CommandText = @"
                    SELECT M.*, C.[Name] CategoryName  
                    FROM Movie M
                    JOIN Category C ON M.CategoryId = C.Id
                    WHERE CategoryId = @id
                ";
                var parameter1 = command.CreateParameter();
                parameter1.ParameterName = "id";
                parameter1.Value = id;
                command.Parameters.Add(parameter1);
                IDataReader reader = command.ExecuteReader();
                List<Movie> lm = new List<Movie>();
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
                    lm.Add(movie);
                }
                return lm;
            }
            catch (Exception) { throw; }
            finally { _conn.Close(); }
        }
    } 
    #endregion
}
