using Dapper;
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

        public async Task<Object2D?> InsertAsync(Guid environmentId ,Object2D object2D, string userId)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                // Check if the environment exists and belongs to the user
                var environment = await sqlConnection.QuerySingleOrDefaultAsync<Environment2D>("SELECT * FROM [Environment2D] " +
                    "WHERE Id = @Id AND UserId = @UserId",
                    new { id = environmentId, UserId = userId });

                if (environment == null)
                    return null;
                
                // Insert the object
             await sqlConnection.ExecuteAsync(
                    "INSERT INTO [Object2D] (Id, PrefabId, PositionX, PositionY, ScaleX, ScaleY, RotationZ, SortingLayer, EnvironmentId) " +
                    "VALUES (@Id, @PrefabId, @PositionX, @PositionY, @ScaleX, @ScaleY, @RotationZ, @SortingLayer, @EnvironmentId)",
                    new { object2D.Id, object2D.PrefabId, object2D.PositionX, object2D.PositionY, object2D.ScaleX, object2D.ScaleY, object2D.RotationZ, object2D.SortingLayer, EnvironmentId = environmentId });

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