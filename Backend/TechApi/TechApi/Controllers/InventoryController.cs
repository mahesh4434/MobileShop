  using Microsoft.AspNetCore.Mvc;
  using Microsoft.Data.SqlClient;
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
      }
  }
