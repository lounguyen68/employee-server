namespace MISA.HUST._21H._2022.API.Enitities
{
    /// <summary>
    /// Thông tin nhân viên
    /// </summary>
    public class Employee
    {
        public Guid EmployeeID { get; set; }

        public string EmployeeCode { get; set; }

        public string EmployeeName { get; set; }

        public int Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string IdentityNumber { get; set; }

        public string IdentityIssuedPlace { get; set; }

        public DateTime IdentityIssuedDate { get; set; }

        public string Email { get; set; }

        public String PhoneNumber { get; set; }

        public int MyProperty { get; set; }

        public Guid PositionID { get; set; }

        public string PositionName { get; set; }

        public Guid DepartmentID { get; set; }

        public string DepartmentName { get; set; }

        public string Salary { get; set; }

        public int WorkStatus { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }

    }
}
