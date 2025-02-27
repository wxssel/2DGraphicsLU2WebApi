﻿using Dapper;
using Microsoft.Data.SqlClient;
using _2DGraphicsLU2.WebApi.Models;


namespace _2DGraphicsLU2.WebApi.Repositories
{
    public class Object2DRepository
    {
        private readonly string sqlConnectionString;

        public Object2DRepository(string sqlConnectionString)
        {
            this.sqlConnectionString = sqlConnectionString;
        }

        public async Task<Object2D> InsertAsync(Object2D object2D, string userId)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                var environmentId = await sqlConnection.ExecuteAsync("INSERT INTO [Object2D] (Id, PrefabId, PositionX, PositionY, ScaleX, ScaleY, RotationZ, SortingLayer, EnviromentId) VALUES (@Id, @PrefabId, @PositionX, @PositionY, @ScaleX, @ScaleY, @RotationZ, @SortingLayer, @EnviromentId) WHERE userId = @userId", new { object2D, userId });
                return object2D;
            }
        }

        public async Task<Object2D?> ReadAsync(Guid id, string userId)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QuerySingleOrDefaultAsync<Object2D>("SELECT * FROM [Object2D] WHERE Id = @Id && userId = @userId", new { id, userId });
            }
        }

        public async Task<IEnumerable<Object2D>> ReadAsync(string userId)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QueryAsync<Object2D>("SELECT * FROM [Object2D]WHERE userId = @userId", userId);
            }
        }

        public async Task UpdateAsync(Object2D environment, string userId)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("UPDATE [Object2D] SET " +
                                                 "PrefabId = @PrefabId" +
                                                 "PositionX = @PositionX, " +
                                                 "PostitionY = @PositionY" +
                                                 "ScaleX = @ScaleX" +
                                                 "ScaleY = @ScaleY" +
                                                 "RotationZ + @RotationZ" +
                                                 "SortingLayer = @SortingLayer" +
                                                 "EnvironmentId = @EnvironmentId" +
                                                 "WHERE Id = @Id && userID = @userId"
                                                 , new { environment, userId });

            }
        }

        public async Task DeleteAsync(Guid id, string userId)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("DELETE FROM [Object2D] WHERE Id = @Id && userId = @userId", new { id, userId });
            }
        }

    }
}