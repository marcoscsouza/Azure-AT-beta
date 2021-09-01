using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace ContadorFunction
{
    public static class FunctionUpdateLastDataView
    {
        [FunctionName("UpdateLastDataView")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("function funcionando");
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            log.LogInformation($"Json body: {requestBody}");

            dynamic data = JsonConvert.DeserializeObject(requestBody);
            int malucoId =  data?.malucoId;

            var connectionString = Environment.GetEnvironmentVariable("SqlConnectionString");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var textSql = $@"UPDATE [dbo].[Maluco] SET [UltimaVisualizacao] = GETDATE() WHERE [Id] = {malucoId};";
                var totalSql = $@"UPDATE [dbo].[Maluco] SET [TotalVisualizacao] = [TotalVisualizacao] + 1 WHERE [Id] = {malucoId};";

                using (SqlCommand cmd = new SqlCommand(textSql, conn))
                {
                    var rowsAffected = cmd.ExecuteNonQuery();
                    log.LogInformation($"rowsAffected: {rowsAffected}");
                }
                
                using (SqlCommand cmd = new SqlCommand(totalSql, conn))
                {
                    var rowsAffected = cmd.ExecuteNonQuery();
                    log.LogInformation($"rowsAffected: {rowsAffected}");
                }
            }

            return new OkResult();
        }
    }
}
