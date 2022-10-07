using Dapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.HUST._21H._2022.API.Enitities;
using MySqlConnector;

namespace MISA.HUST._21H._2022.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class EmployeesController : ControllerBase
    {
        /// <summary>
        /// API Lấy danh sách tất cả nhân viên
        /// </summary>
        /// <returns>Danh sách nhân viên</returns>
        /// 
        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            try
            {
                //Khởi tạo kêt nối tới DB
                string connectionString = "Server=3.0.89.182;Port=3306;Database=DAOTAO.AI.2022.NPLONG;Uid=dev;Pwd=12345678;";
                var mySqlConnection = new MySqlConnection(connectionString);

                //Chuẩn bị câu lệnh truy vấn
                string getAllEmployeesCommand = "SELECT * FROM employee;";

                //Thực hiện gọi vào DB chạy command
                var employees = mySqlConnection.Query<Employee>(getAllEmployeesCommand);

                //Xử lý dữ liệu trả về
                if (employees != null)
                {
                    return StatusCode(StatusCodes.Status200OK, employees);
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

        /// <summary>
        /// API Lấy thông tin một nhân viên
        /// </summary>
        /// <param name="employeeID">ID của nhân viên</param>
        /// <returns>Thông tin chi tiết của 1 nhân viên</returns>
        [HttpGet]
        [Route("{employeeID}")]
        public IActionResult GetEmployeeByID([FromRoute] Guid employeeID)
        {
            try
            {
                //Khởi tạo kêt nối tới DB
                string connectionString = "Server=3.0.89.182;Port=3306;Database=DAOTAO.AI.2022.NPLONG;Uid=dev;Pwd=12345678;";
                var mySqlConnection = new MySqlConnection(connectionString);

                //Chuẩn bị câu lệnh truy vấn
                string getEmployeeByIDCommand = "SELECT * FROM employee e WHERE e.EmployeeID ='" + employeeID + "'";

                //Thực hiện gọi vào DB chạy command
                var employee = mySqlConnection.QueryFirstOrDefault<Employee>(getEmployeeByIDCommand);

                if (employee != null)
                {
                    return StatusCode(StatusCodes.Status200OK, employee);
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
        /// <summary>
        /// API Tạo mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        [HttpGet]
        [Route("/LatestEmployeeCode")]
        public IActionResult GetLatestEmployeeCode()
        {
            try
            {
                //Khởi tạo kêt nối tới DB
                string connectionString = "Server=3.0.89.182;Port=3306;Database=DAOTAO.AI.2022.NPLONG;Uid=dev;Pwd=12345678;";
                var mySqlConnection = new MySqlConnection(connectionString);

                //Chuẩn bị câu lệnh truy vấn
                string getLatestEmployeeCodeCommand = "SELECT MAX(EmployeeCode) AS LatestEmployeeCode FROM employee e";

                //Thực hiện gọi vào DB chạy command
                var latestEmployeeCode = mySqlConnection.QueryFirst(getLatestEmployeeCodeCommand);

            
                //Xu li du lieu
                return StatusCode(StatusCodes.Status200OK, latestEmployeeCode);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return StatusCode(StatusCodes.Status400BadRequest, null);
            }
        }

        /// <summary>
        /// API Lọc danh sách nhân viên có điều kiện tìm kiếm, phân trang
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="positionID"></param>
        /// <param name="departmentID"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns>Danh sách nhân viên đã lọc</returns>
        [HttpGet]
        [Route("filter")]
        public IActionResult FilterEmployees([FromQuery] string keyword,
                                             [FromQuery] Guid positionID,
                                             [FromQuery] Guid departmentID,
                                             [FromQuery] int limit,
                                             [FromQuery] int offset)
        {
            try
            {
                //Khởi tạo kêt nối tới DB
                string connectionString = "Server=3.0.89.182;Port=3306;Database=DAOTAO.AI.2022.NPLONG;Uid=dev;Pwd=12345678;";
                var mySqlConnection = new MySqlConnection(connectionString);

                //Chuẩn bị câu lệnh truy vấn
                string FilterEmployees = "SELECT * FROM employee e WHERE (e.EmployeeCode LIKE '%" + keyword + "%' OR e.EmployeeName LIKE '%" + keyword + "%')";

                FilterEmployees += " AND  e.PositionID = '" + positionID + "'";
              
                FilterEmployees += " AND  e.PositionID = '" + departmentID + "'";
                
                FilterEmployees += " LIMIT " + offset + "," + limit + ";";

                //Thực hiện gọi vào DB chạy command
                var employees = mySqlConnection.Query<Employee>(FilterEmployees);

                if (employees != null)
                {
                    return StatusCode(StatusCodes.Status200OK, employees);
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
        /// <summary>
        /// API Them moi 1 nhan vien
        /// </summary>
        /// <param name="employee"> Object nhân viên mới</param>
        /// <returns>Thêm mới 1 nhân viên vào CSDL</returns>
        [HttpPost]
        public IActionResult InsertEmployee([FromBody] Employee employee)
        {
            try
            {
                //Khởi tạo kêt nối tới DB
                string connectionString = "Server=3.0.89.182;Port=3306;Database=DAOTAO.AI.2022.NPLONG;Uid=dev;Pwd=12345678;";
                var mySqlConnection = new MySqlConnection(connectionString);

                //Chuẩn bị câu lệnh truy vấn
                string insertEmployeeCommand = "INSERT INTO employee (EmployeeID, EmployeeCode, EmployeeName, Gender, DateOfBirth, IdentityNumber, IdentityIssuedPlace, IdentityIssuedDate, Email, PhoneNumber, PositionName, PositionID, DepartmentID, DepartmentName, Salary, WorkStatus, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) "
                    + "VALUES ( @EmployeeID, @EmployeeCode, @EmployeeName, @Gender, @DateOfBirth, @IdentityNumber, @IdentityIssuedPlace, @IdentityIssuedDate, @Email, @PhoneNumber, @PositionName, @PositionID, @DepartmentID, @DepartmentName, @Salary, @WorkStatus, @CreatedBy, @CreatedDate, @ModifiedBy, @ModifiedDate);";
                //Chuẩn bị tham số cho câu lệnh truy vấn
                var employeeID = Guid.NewGuid();
                var parameters = new DynamicParameters();
                parameters.Add("@EmployeeID", employeeID);
                parameters.Add("@EmployeeCode", employee.EmployeeCode);
                parameters.Add("@EmployeeName", employee.EmployeeName);
                parameters.Add("@Gender", employee.Gender);
                parameters.Add("@DateOfBirth", employee.DateOfBirth);
                parameters.Add("@IdentityNumber", employee.IdentityNumber);
                parameters.Add("@IdentityIssuedPlace", employee.IdentityIssuedPlace);
                parameters.Add("@IdentityIssuedDate", employee.IdentityIssuedDate);
                parameters.Add("@Email", employee.Email);
                parameters.Add("@PhoneNumber", employee.PhoneNumber);
                parameters.Add("@PositionName", employee.PositionName);
                parameters.Add("@PositionID", employee.PositionID);
                parameters.Add("@DepartmentID", employee.DepartmentID);
                parameters.Add("@Salary", employee.Salary);
                parameters.Add("@WorkStatus", employee.WorkStatus);
                parameters.Add("@CreatedBy", employee.CreatedBy);
                parameters.Add("@CreatedDate", employee.CreatedDate);
                parameters.Add("@ModifiedBy", employee.ModifiedBy);
                parameters.Add("@ModifiedDate", employee.ModifiedDate);


                //Thực hiện gọi vào DB chạy command
                int numberOfAffectedRows = mySqlConnection.Execute(insertEmployeeCommand, parameters);

                //Xử lí kết quả
                if (numberOfAffectedRows > 0)
                {
                    return StatusCode(StatusCodes.Status201Created, employeeID);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, null);
                };
            }
            catch (MySqlException mySqlException)
            {
                if (mySqlException.ErrorCode != MySqlErrorCode.DuplicateKeyEntry)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, null);
                }
                return StatusCode(StatusCodes.Status400BadRequest, mySqlException.Message);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return StatusCode(StatusCodes.Status400BadRequest, null);
            }

        }
        /// <summary>
        /// API Sua 1 nhan vien
        /// </summary>
        /// <param name="employee">Object nhân viên cần sửa</param>
        /// <param name="employeeID">ID nhân viên cần sửa</param>
        /// <returns>Sửa 1 nhân viên trong CSDL</returns>
        [HttpPut]
        [Route("{employeeID}")]
        public IActionResult UpdateEmployee(
            [FromBody] Employee employee,
            [FromRoute] Guid employeeID)
        {
            try
            {
                //Khởi tạo kêt nối tới DB
                string connectionString = "Server=3.0.89.182;Port=3306;Database=DAOTAO.AI.2022.NPLONG;Uid=dev;Pwd=12345678;";
                var mySqlConnection = new MySqlConnection(connectionString);

                //Chuẩn bị câu lệnh truy vấn
                string updateEmployeeCommand = "UPDATE employee e SET EmployeeCode = @EmployeeCode, EmployeeName = @EmployeeName, Gender = @Gender, DateOfBirth = @DateOfBirth, IdentityNumber = @IdentityNumber, IdentityIssuedPlace = @IdentityIssuedPlace, IdentityIssuedDate = @IdentityIssuedDate, Email = @Email, PhoneNumber = @PhoneNumber, PositionName = @PositionName, PositionID = @PositionID, DepartmentID = @DepartmentID, DepartmentName = @DepartmentName, Salary = @Salary, WorkStatus = @WorkStatus, CreatedBy = @CreatedBy, CreatedDate = @CreatedDate, ModifiedBy = @ModifiedBy, ModifiedDate = @ModifiedDate WHERE EmployeeID = '" + employeeID + "';";
                //Chuẩn bị tham số cho câu lệnh truy vấn           
                var parameters = new DynamicParameters();
                parameters.Add("@EmployeeCode", employee.EmployeeCode);
                parameters.Add("@EmployeeName", employee.EmployeeName);
                parameters.Add("@Gender", employee.Gender);
                parameters.Add("@DateOfBirth", employee.DateOfBirth);
                parameters.Add("@IdentityNumber", employee.IdentityNumber);
                parameters.Add("@IdentityIssuedPlace", employee.IdentityIssuedPlace);
                parameters.Add("@IdentityIssuedDate", employee.IdentityIssuedDate);
                parameters.Add("@Email", employee.Email);
                parameters.Add("@PhoneNumber", employee.PhoneNumber);
                parameters.Add("@PositionName", employee.PositionName);
                parameters.Add("@PositionID", employee.PositionID);
                parameters.Add("@DepartmentID", employee.DepartmentID);
                parameters.Add("@Salary", employee.Salary);
                parameters.Add("@WorkStatus", employee.WorkStatus);
                parameters.Add("@CreatedBy", employee.CreatedBy);
                parameters.Add("@CreatedDate", employee.CreatedDate);
                parameters.Add("@ModifiedBy", employee.ModifiedBy);
                parameters.Add("@ModifiedDate", employee.ModifiedDate);


                //Thực hiện gọi vào DB chạy command
                int numberOfAffectedRows = mySqlConnection.Execute(updateEmployeeCommand, parameters);

                //Xử lí kết quả
                if (numberOfAffectedRows > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, employeeID);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, null);
                };
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return StatusCode(StatusCodes.Status400BadRequest, null);
            }
        }
        /// <summary>
        /// API Xóa 1 nhân viên
        /// </summary>
        /// <param name="employeeID"> ID của nhân viên</param>
        /// <returns>Xóa nhân viên có ID trên trong CSDL</returns>
        [HttpDelete]
        [Route("{employeeID}")]
        public IActionResult DeleteEmployee([FromRoute] Guid employeeID)
        {
            try
            {
                //Khởi tạo kêt nối tới DB
                string connectionString = "Server=3.0.89.182;Port=3306;Database=DAOTAO.AI.2022.NPLONG;Uid=dev;Pwd=12345678;";
                var mySqlConnection = new MySqlConnection(connectionString);

                //Chuẩn bị câu lệnh truy vấn
                string deleteEmployeeCommand = "DELETE FROM employee WHERE EmployeeID = '" + employeeID + "';";

                //Thực hiện gọi vào DB chạy command
                int numberOfAffectedRows = mySqlConnection.Execute(deleteEmployeeCommand);

                //Xử lí kết quả
                if (numberOfAffectedRows > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, employeeID);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, null);
                };
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return StatusCode(StatusCodes.Status400BadRequest, null);
            }
        }
    }
}
