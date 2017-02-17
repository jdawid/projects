using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;



namespace HelpdeskBusinessDataObjects 
{
    public class EmployeeBusinessData : ConfigBusinessData
    {
        ///<summary>
        ///Create- add Employee row for serialized Dictionary
        ///</summary>
        ///<param name="ByteArrayEmp">serialized employee info</param>
        ///<returns>int representing newly created id for employee</returns>
        public int Create(byte[] ByteArrayEmp)
        {
            int newId = -1;
            Employee EmployeeEntity = new Employee();
            Dictionary<string, Object> DictionaryEmployee = (Dictionary<string, Object>)Deserializer(ByteArrayEmp);
            HelpDeskDBEntities dbContext = new HelpDeskDBEntities();
            try
            {
                EmployeeEntity.Title = Convert.ToString(DictionaryEmployee["title"]);
                EmployeeEntity.FirstName = Convert.ToString(DictionaryEmployee["firstname"]);
                EmployeeEntity.LastName = Convert.ToString(DictionaryEmployee["lastname"]);
                EmployeeEntity.PhoneNo = Convert.ToString(DictionaryEmployee["phoneno"]);
                EmployeeEntity.Email = Convert.ToString(DictionaryEmployee["email"]);
                EmployeeEntity.DepartmentID = Convert.ToInt32(DictionaryEmployee["departmentid"]);
                dbContext.Employees.Add(EmployeeEntity);
                dbContext.SaveChanges();
                newId = EmployeeEntity.EmployeeID;
            }
            catch (Exception ex)
            {
                ErrorRoutine(ex, "EmployeeBusinessData", "Create");
            }
            return newId;
        }

        ///<summary>
        ///<param name="empid">int representing employee</param>
        ///<returns>serialized Dictionary containing single employee info</returns>
        ///</summary>
        public byte[] GetByID(int empid)
        {
            Dictionary<string, Object> retDict = new Dictionary<string, Object>();

            try
            {
                HelpDeskDBEntities dbContext = new HelpDeskDBEntities();
                dbContext.Configuration.ProxyCreationEnabled = false;
                Employee EmployeeEntity = dbContext.Employees.FirstOrDefault(emp => emp.EmployeeID == empid);

                if (EmployeeEntity != null)
                {
                    retDict["title"] = EmployeeEntity.Title;
                    retDict["firstname"] = EmployeeEntity.FirstName;
                    retDict["lastname"] = EmployeeEntity.LastName;
                    retDict["phoneno"] = EmployeeEntity.PhoneNo;
                    retDict["email"] = EmployeeEntity.Email;
                    retDict["departmentid"] = EmployeeEntity.DepartmentID;
                    retDict["employeeid"] = EmployeeEntity.EmployeeID;
                    retDict["entity"] = Serializer(EmployeeEntity, true);
                }
                else
                {
                    retDict["Error"] = "Employee Not Found!";
                }
            }
            catch (Exception ex)
            {
                ErrorRoutine(ex, "EmployeeBusinessData", "GetByID");
            }
            return Serializer(retDict);
        }//end GetByID

        ///<summary>
        ///GetAll - returns list containing all Employee rows from db
        ///</summary>
        ///<returns>List of Employee classes defined from EF</returns>
        public List<Employee> GetAll()
        {
        HelpDeskDBEntities dbContext = new HelpDeskDBEntities();
            List<Employee> employees = dbContext.Employees.ToList();
            
               return employees;
        }//end GetAll

        ///<summary>
        ///Update - update Employee row from a serilized Dictionary. Make sure EF has 
        ///         timer field property Concurrency set to Fixed to allow Optimistic 
        ///         Currency Exception processing. Also make sure to deserialize the
        ///         entity prior to attaching back to Context
        ///<param name="bytEmployee">serialized Dictionary</param>
        ///<returns>int indicating # of rows updated</returns>
        ///</summary>
        public int Update(byte[] bytEmployee)
            {
                int rowsUpdated = -1;
                try
                {
                    HelpDeskDBEntities dbContext = new HelpDeskDBEntities();
                    Dictionary<string, Object> DictionaryEmployee = (Dictionary<string, Object>)Deserializer(bytEmployee);
                    byte[] ByteArrayEmployeeEntity = (byte[])DictionaryEmployee["entity"];
                    Employee EmployeeEntity = (Employee)Deserializer(ByteArrayEmployeeEntity, typeof(Employee));

                    EmployeeEntity.Title = Convert.ToString(DictionaryEmployee["title"]);
                    EmployeeEntity.FirstName = Convert.ToString(DictionaryEmployee["firstname"]);
                    EmployeeEntity.LastName = Convert.ToString(DictionaryEmployee["lastname"]);
                    EmployeeEntity.PhoneNo = Convert.ToString(DictionaryEmployee["phoneno"]);
                    EmployeeEntity.Email = Convert.ToString(DictionaryEmployee["email"]);
                    EmployeeEntity.DepartmentID = Convert.ToInt32(DictionaryEmployee["departmentid"]);
                    EmployeeEntity.EmployeeID = Convert.ToInt32(DictionaryEmployee["employeeid"]);
                    rowsUpdated = dbContext.SaveChanges();
                }//end try
                catch (DbUpdateConcurrencyException ex)
                {
                    System.Diagnostics.Debug.Write(ex.Message);
                    throw new Exception("concurrency");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return rowsUpdated;
            }//end Update

            ///<summary>
            ///Delete - delete Employee row from db
            ///<param name="empid">int representing employeeid</param>
            ///<returns>int representing the # of rows deleted</returns>
            ///</summary>
            public int Delete(int empid)
            {
                int rowsDeleted = -1;

            try
            {
                HelpDeskDBEntities dbContext = new HelpDeskDBEntities();
                Employee EmployeeEntity = dbContext.Employees.FirstOrDefault(emp => emp.EmployeeID == empid);
                int rowsBeforeDeleted = dbContext.Employees.Count();
                dbContext.Employees.Remove(EmployeeEntity);
                dbContext.SaveChanges();
                int rowsAfterDeleted = dbContext.Employees.Count();
                rowsDeleted = rowsBeforeDeleted - rowsAfterDeleted; // count should be 1 now
            }
            catch (Exception ex)
            {
                ErrorRoutine(ex, "EmployeeBusinessData", "Delete");
            }
            return rowsDeleted;
        }//end Delete


    } //end class
}//end namespace