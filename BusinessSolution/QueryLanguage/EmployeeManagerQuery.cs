namespace BusinessSolution
{
    /// <summary>
    /// Query class for managing employees
    /// </summary>
    class EmployeeManagerQuery
    {
        /// <summary>
        /// Query for Adding Employee
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="phoneNumber"></param>
        /// <param name="email"></param>
        /// <param name="addressLine"></param>
        /// <param name="city"></param>
        /// <param name="country"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string AddEmployeeQuery(string firstName, string lastName, string phoneNumber, string email, string addressLine, string city, string country, string password)
        {            
            return "INSERT INTO [Employee] " +
                "(FirstName, LastName, PhoneNumber, Email, AddressLine, City, Country, EmployeeLoginPassword)" +
                "VALUES ('" + firstName + "','" + lastName + "','" + phoneNumber + "','" + email + "','" + addressLine + "','" + city + "','" + country +"','" + password + "')";
        }

        /// <summary>
        /// Query for Removing an Employee
        /// </summary>
        /// <returns></returns>
        public string RemoveEmployeeQuery(int employeeLoginId)
        {
            return "DELETE FROM [Employee] WHERE EmployeeLoginId = '" + employeeLoginId + "'";
        }

        /// <summary>
        /// Query for Searching to delete an Employee
        /// </summary>
        /// <param name="employeeLoginId"></param>
        /// <returns></returns>
        public string SearchEmployeeForDeletingQuery(int employeeLoginId)
        {
            return "SELECT CONCAT(FirstName, LastName) AS Name, PhoneNumber, Email FROM [Employee] WHERE EmployeeLoginId = '" + employeeLoginId + "'";
        }

        /// <summary>
        /// Query for Searching Employee Information
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
        public string SearchEmployee(string firstName, string lastName)
        {
            if(firstName == "")
            {
                return "SELECT * FROM [Employee] WHERE FirstName = '" + lastName + "' OR LastName = '" + lastName + "'";
            }
            else if(lastName == "")
            {
                return "SELECT * FROM [Employee] WHERE FirstName = '" + firstName + "' OR LastName = '" + firstName + "'";
            }
            else
            {
                return "SELECT * FROM [Employee] WHERE FirstName = '" + firstName + "' AND LastName = '" + lastName + "'";
            }            
        }

        /// <summary>
        /// Query for Showing All Employees
        /// </summary>
        /// <returns></returns>
        public string ShowAllEmployee()
        {
            return "SELECT EmployeeLoginId, (FirstName + ' ' + LastName) AS Name, EmployeeLoginPassword FROM [Employee]";
        }
    }
}
