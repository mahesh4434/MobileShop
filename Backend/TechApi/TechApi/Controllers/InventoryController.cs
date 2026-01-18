using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text.Json.Serialization;
using TechApi.Models;


    namespace TechApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
    [HttpPost]
        public ActionResult SaveInventoryData(Inventory inventoryDto) {
      SqlConnection connection = new SqlConnection
      {
        ConnectionString = "Server=(localdb)\\MSSQLLocalDB;Database=techDb;Trusted_Connection=True;MultipleActiveResultSets=true;"
      };

      SqlCommand command = new SqlCommand
      {
        CommandText = "sp_SaveInventoryData",
        CommandType = System.Data.CommandType.StoredProcedure,
        Connection = connection

      };
      command.Parameters.AddWithValue("@ProductId", inventoryDto.ProductId);
      command.Parameters.AddWithValue("@ProductName", inventoryDto.ProductName);
      command.Parameters.AddWithValue("@StockAvailable", inventoryDto.StockAvailable);
      command.Parameters.AddWithValue("@ReorderStock", inventoryDto.ReorderStock);
      connection.Open();
      command.ExecuteNonQuery();
      connection.Close();
      return Ok("Inventory data saved successfully.");
        }

      [HttpGet]
      public ActionResult GetInventoryData()
      {
          SqlConnection connection = new SqlConnection
          {
              ConnectionString = "Server=(localdb)\\MSSQLLocalDB;Database=techDb;Trusted_Connection=True;MultipleActiveResultSets=true;"
          };

          SqlCommand command = new SqlCommand
          {
              CommandText = "sp_GetInventoryData",
              CommandType = System.Data.CommandType.StoredProcedure,
              Connection = connection

          };
       
          connection.Open();
          List<InventoryDto> reponse = new List<InventoryDto>();
          using (SqlDataReader sqlDataReader = command.ExecuteReader())
          {
              while (sqlDataReader.Read())
              {
                  InventoryDto inventoryDto1 = new InventoryDto();
                  inventoryDto1.ProductId = Convert.ToInt32(sqlDataReader["ProductId"]);
                  inventoryDto1.ProductName = sqlDataReader["ProductName"].ToString();
                  inventoryDto1.StockAvailable = Convert.ToInt32(sqlDataReader["StockAvailable"]);
                  inventoryDto1.ReorderStock = Convert.ToInt32(sqlDataReader["ReorderStock"]);
                  reponse.Add(inventoryDto1);
              }
          }
          connection.Close();
          return Ok(reponse);
      }

        [HttpDelete]
        public ActionResult DeleteInventoryData(int ProductId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(
                    "Server=(localdb)\\MSSQLLocalDB;Database=techDb;Trusted_Connection=True;MultipleActiveResultSets=true;"))
                {
                    using (SqlCommand command = new SqlCommand("sp_DeleteInventoryDetails", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ProductId", ProductId);

                        connection.Open();

                        int rowsAffected = command.ExecuteNonQuery();

                        // If no rows were deleted
                        if (rowsAffected == 0)
                        {
                            return NotFound($"No inventory record found with ProductId = {ProductId}");
                        }
                    }
                }

                return Ok("Inventory data deleted successfully.");
            }
            catch (SqlException ex)
            {
                // SQL related errors (DB down, SP error, constraint issues)
                return StatusCode(500, $"Database error occurred: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Any other unexpected error
                return StatusCode(500, $"Unexpected error occurred: {ex.Message}");
            }
        }



    }
}
