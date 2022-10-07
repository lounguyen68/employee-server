using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.HUST._21H._2022.API.Enitities;
using MySqlConnector;

namespace MISA.HUST._21H._2022.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        /// <summary>
        /// API Lấy danh sách tất cả vị trí
        /// </summary>
        /// <returns>Danh sách vị trí</returns>
        [HttpGet]
        public IActionResult GetAllPositions()
        {
            try
            {
                //Khởi tạo kêt nối tới DB
                string connectionString = "Server=3.0.89.182;Port=3306;Database=DAOTAO.AI.2022.NPLONG;Uid=dev;Pwd=12345678;";
                var mySqlConnection = new MySqlConnection(connectionString);

                //Chuẩn bị câu lệnh truy vấn
                string getAllPositionsCommand = "SELECT * FROM positions;";

                //Thực hiện gọi vào DB chạy command
                var positions = mySqlConnection.Query<Position>(getAllPositionsCommand);

                //Xử lý dữ liệu trả về
                if (positions != null)
                {
                    return StatusCode(StatusCodes.Status200OK, positions);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, null);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return StatusCode(StatusCodes.Status400BadRequest, null);
            }
        }
    }
}
