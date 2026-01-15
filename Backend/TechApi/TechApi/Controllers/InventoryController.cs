using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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



  }
}
