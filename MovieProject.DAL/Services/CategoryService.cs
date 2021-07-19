﻿using MovieProject.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MovieProject.DAL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly SqlConnection _conn;
        public CategoryService(SqlConnection conn)
        {
            _conn = conn;
        }
        public List<Category> Get()
        {
            try
            {
                _conn.Open();
                SqlCommand command = _conn.CreateCommand();
                command.CommandText = "SELECT * FROM Category";
                List<Category> result = new List<Category>();
                SqlDataReader reader = command.ExecuteReader();
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
                SqlCommand command = _conn.CreateCommand();
                command.CommandText = "SELECT * FROM Category WHERE Id = @id";
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader();
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
                SqlCommand command = _conn.CreateCommand();
                command.CommandText = @"DELETE FROM Category WHERE Id = @id";
                command.Parameters.AddWithValue("@id", id);
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
                SqlCommand cmd = _conn.CreateCommand();
                cmd.CommandText = "INSERT INTO Category(Name) OUTPUT Inserted.Id VALUES(@p1)";
                cmd.Parameters.AddWithValue("@p1", c.Name);
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
                SqlCommand cmd = _conn.CreateCommand();
                cmd.CommandText = @"UPDATE Category SET Name = @p1 WHERE Id = @id";
                cmd.Parameters.AddWithValue("@p1", c.Name);
                cmd.Parameters.AddWithValue("@id", c.Id);
                cmd.ExecuteNonQuery();
            }
            catch (Exception) { throw; }
            finally { _conn.Close(); }
        }
    }
}