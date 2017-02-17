using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Media;
using HelpdeskBusinessDataObjects;

namespace HelpdeskBusinessUserObjects
{
    /// <summary>
    /// EmployeeBusinessUser: Business Object that interacts with UI
    ///                      utilizes EmployeeBusinessData, and BrokenRules
    /// </summary>
    public class EmployeeBusinessUser : ConfigBusinessUser
    {
       // public event RuleBrokenEventHandler evntEmployeeIsValid;

        private int _departmentid;
        private string _email;
        private int _employeeID;
        private byte[] _entity;
        private string _firstName;
        private string _lastName;
        private string _phoneNo;
        private BrokenRules _rules;
        private string _title;

        /// <summary>
        /// Collection to look after colors
        /// </summary>
        public Dictionary<string, SolidColorBrush> PropertyColors; //keep track of colors - black ok, red rule broken

        /// <summary>
        /// event to raise based on event coming from Broken Rules class
        /// </summary>
        public event RuleBrokenEventHandler evntEmployeeStillBroken; // event

        /// <summary>
        /// DepartmentID property
        /// </summary>
        public int DepartmentID
        {
            get
            {
                return _departmentid;
            }
            set
            {
                try
                {
                    if (value < 1200 && (value % 100) == 0)
                    {
                        _departmentid = value;
                        _rules.MaintainRule("department", false); // remove from collection
                        PropertyColors["department"] = new SolidColorBrush(Colors.Black);
                    }
                    else
                        throw new System.Exception(); //caught below
                }
                catch (Exception ex)
                {
                    _rules.MaintainRule("department", true); // add or replace in collection
                    string strDbg = ex.Message;
                    PropertyColors["department"] = new SolidColorBrush(Colors.Red);
                }
            }
        }

        ///<summary>
        /// Title Property
        ///</summary>
        public String Title
        {
            get
            {
                return _title;
            }
            set
            {
                try
                {
                    Regex _regex = new Regex("Mr.|Mrs.|Ms.|Dr.");

                    if (!_regex.Match(value).Success)
                        throw new System.Exception(); //this is caught below

                    if (value.Length > 0 && value.Length <= 4) //makes sure that the title is the correct length
                    {
                        _title = value;
                        _rules.MaintainRule("title", false);   // remove from collection
                        PropertyColors["title"] = new SolidColorBrush(Colors.Black);
                    }
                    else
                        throw new System.Exception();//caught below
                }
                catch (Exception ex)
                {
                    _rules.MaintainRule("title", true);    // add or replace in collection
                    String strDbg = ex.Message;
                    PropertyColors["title"] = new SolidColorBrush(Colors.Red);
                }

            }
        }


        /// <summary>
        /// Email Property
        /// </summary>
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                try
                {
                    Regex _regex = new Regex("\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*");

                    if (!_regex.Match(value).Success)
                        throw new System.Exception(); //this is caught below

                    _email = value;
                    _rules.MaintainRule("email", false);   // remove from collection
                    PropertyColors["email"] = new SolidColorBrush(Colors.Black);
                }
                catch (Exception ex)
                {
                    _rules.MaintainRule("email", true);    // add or replace in collection
                    String strDbg = ex.Message;
                    PropertyColors["email"] = new SolidColorBrush(Colors.Red);
                }
            }
        }


        ///<summary>
        ///PhoneNo
        ///</summary>
        public string PhoneNo
        {
            get
            {
                return _phoneNo;
            }
            set
            {
                try
                {
                    Regex _regex = new Regex(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$");

                    if (!_regex.Match(value).Success)
                        throw new System.Exception(); //this is caught below

                    _phoneNo = value;
                    _rules.MaintainRule("phoneno", false);   // remove from collection
                    PropertyColors["phoneno"] = new SolidColorBrush(Colors.Black);
                }
                catch (Exception ex)
                {
                    _rules.MaintainRule("phoneno", true);    // add or replace in collection
                    String strDbg = ex.Message;
                    PropertyColors["phoneno"] = new SolidColorBrush(Colors.Red);
                }
            }
        }

        ///<summary>
        ///FirstName
        ///</summary>
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                try
                {
                    if (value.Length > 0 && value.Length < 26)
                    {
                        _firstName = value;
                        _rules.MaintainRule("firstname", false); // remove from collection
                        PropertyColors["firstname"] = new SolidColorBrush(Colors.Black);
                    }
                    else
                        throw new System.Exception(); //caught below
                }
                catch (Exception ex)
                {
                    _rules.MaintainRule("firstname", true); // add or replace in collection
                    string strDbg = ex.Message;
                    PropertyColors["firstname"] = new SolidColorBrush(Colors.Red);
                }
            }
        }

        ///<summary>
        ///LastName
        ///</summary>
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                try
                {
                    if (value.Length > 0 && value.Length < 26)
                    {
                        _lastName = value;
                        _rules.MaintainRule("lastname", false); // remove from collection
                        PropertyColors["lastname"] = new SolidColorBrush(Colors.Black);
                    }
                    else
                        throw new System.Exception(); //caught below
                }
                catch (Exception ex)
                {
                    _rules.MaintainRule("lastname", true); // add or replace in collection
                    string strDbg = ex.Message;
                    PropertyColors["lastname"] = new SolidColorBrush(Colors.Red);
                }
            }
        }

        //Handler for the EmployeeBusinessUser() constructor
        public void EmployeeRulesBroken(bool blnIsBroken)
        {
            if (evntEmployeeStillBroken != null)
                evntEmployeeStillBroken(blnIsBroken);  // raise event for presentation layer
        }

        //constructor
        public EmployeeBusinessUser()
        {
            _rules = new BrokenRules();
            _rules.evntStillBroken += new RuleBrokenEventHandler(EmployeeRulesBroken); // register eventhandler for BR
            // break all the rules
            _rules.MaintainRule("deptartmentid", true);
            _rules.MaintainRule("email", true);
            _rules.MaintainRule("firstname", true);
            _rules.MaintainRule("lastname", true);
            _rules.MaintainRule("phoneno", true);
            _rules.MaintainRule("title", true);
            PropertyColors = new Dictionary<string, SolidColorBrush>();
            PropertyColors["deptartmentid"] = new SolidColorBrush(Colors.Red);
            PropertyColors["email"] = new SolidColorBrush(Colors.Red);
            PropertyColors["firstname"] = new SolidColorBrush(Colors.Red);
            PropertyColors["lastname"] = new SolidColorBrush(Colors.Red);
            PropertyColors["phoneno"] = new SolidColorBrush(Colors.Red);
            PropertyColors["title"] = new SolidColorBrush(Colors.Red);
        }



        public List<EmployeeBusinessUser> GetAll()
        {
            List<EmployeeBusinessUser> empList = new List<EmployeeBusinessUser>();
            try
            {
                EmployeeBusinessData empData = new EmployeeBusinessData();
                List<Employee> rawList = empData.GetAll();
                foreach (Employee emp in rawList)
                {
                    EmployeeBusinessUser fullEmployee = new EmployeeBusinessUser();
                    fullEmployee._departmentid = emp.DepartmentID;
                    fullEmployee._email = emp.Email;
                    fullEmployee._employeeID = emp.EmployeeID;
                    fullEmployee._firstName = emp.FirstName;
                    fullEmployee._lastName = emp.LastName;
                    fullEmployee._phoneNo = emp.PhoneNo;
                    fullEmployee._title = emp.Title;
                    empList.Add(fullEmployee);
                }
            }
            catch (Exception e)
            {
                ErrorRoutine(e, "EmployeeBusinessUser", "GetAll");
            }
            return empList;
        }//end GetAll




        public EmployeeBusinessUser GetByID(int employeeid)
        {
            Dictionary<string, Object> dataLayerDict;
            try
            {
            EmployeeBusinessData dataLayer = new EmployeeBusinessData();
             dataLayerDict = (Dictionary<string, Object>)Deserializer(dataLayer.GetByID(employeeid));
            //EmployeeBusinessData empBusData = new EmployeeBusinessData();
            //Dictionary<string, Object> empBusData.GetByID(employeeid);
            _title =  Convert.ToString(dataLayerDict["title"]);
            _firstName =  Convert.ToString(dataLayerDict["firstname"]);
            _lastName =  Convert.ToString(dataLayerDict["lastname"]);
            _departmentid = Convert.ToInt32(dataLayerDict["departmentid"]);
            _employeeID = Convert.ToInt32(dataLayerDict["employeeid"]);
            _email = Convert.ToString(dataLayerDict["email"]);
            _phoneNo = Convert.ToString(dataLayerDict["phoneno"]);
            }
            catch (Exception e)
            {
                ErrorRoutine(e, "EmployeeBusinessUser", "GetByID");
            }
            return this;
        }



        public int Create()
        {
           
            int primaryKey = 0;
            try
            {
                Dictionary<string, Object> empBusinessData = new Dictionary<string, object>();
                EmployeeBusinessData dataLayer = new EmployeeBusinessData();
                empBusinessData.Add("title", _title);
                empBusinessData.Add("firstname", _firstName);
                empBusinessData.Add("lastname", _lastName);
                empBusinessData.Add("phoneno", _phoneNo);
                empBusinessData.Add("email", _email);
                empBusinessData.Add("departmentid", _departmentid);
                _entity = Serializer(empBusinessData);
                primaryKey = dataLayer.Create(_entity);

            }
            catch (Exception e)
            {
                ErrorRoutine(e, "EmployeeBusinessUser", "Create");
            }
            return primaryKey;
        }

        public int Update()
        {
            int rowsUpdated = 0;
            try
            {
                EmployeeBusinessData dataLayer = new EmployeeBusinessData();
                Dictionary<string, Object> empBusinessData = new Dictionary<string, Object>(); 
                Dictionary<string, Object> empDict2 = (Dictionary<string, Object>)Deserializer(dataLayer.GetByID(_employeeID));
                empBusinessData["departmentid"] = _departmentid;
                empBusinessData["email"] = _email;
                empBusinessData["employeeid"] = _employeeID;
                empBusinessData["firstname"] = _firstName;
                empBusinessData["lastname"] = _lastName;
                empBusinessData["phoneno"] = _phoneNo;
                empBusinessData["title"] = _title;
                empBusinessData["entity"] = empDict2["entity"];
                _entity = Serializer(empBusinessData);
                rowsUpdated = dataLayer.Update(_entity);
            }
            catch (Exception e)
            {
                ErrorRoutine(e, "EmployeeBusinessUser", "Update");
            }
            return rowsUpdated;
        }//end Update

        public int Delete(int employeeid)
        {
            int rowsDeleted = 0;
            try
            {
                EmployeeBusinessData empBusinessData = new EmployeeBusinessData();

                rowsDeleted = empBusinessData.Delete(employeeid);
            }
            catch (Exception e)
            {
                ErrorRoutine(e, "EmployeeBusinessUser", "Delete");
            }
            return rowsDeleted;
        }

        public void LoadDefaultValues()
        {
            _employeeID = 0;
            _departmentid = 0;
            _firstName = "";
            _lastName = "";
            _phoneNo = "";
            _email = "";
            _title = "";
        }

    }
}

