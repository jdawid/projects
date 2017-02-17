Imports HelpdeskBusinessUserObjects
Public Class WindowEmployeeUserDriver
    Private Sub btnEnter_Click(sender As Object, e As RoutedEventArgs) Handles btnEnter.Click
        Dim EmployeeBU As EmployeeBusinessUser = New EmployeeBusinessUser
        Dim outStr As String = ""

        Try
            'EmployeeBU.LoadDefaultValues()
            'Load up an Employee for Create
            EmployeeBU.Title = "Mr."
            EmployeeBU.FirstName = "John"
            EmployeeBU.LastName = "Smith"
            EmployeeBU.PhoneNo = "(555)555-5555"
            EmployeeBU.Email = "js@abc.com"
            EmployeeBU.DepartmentID = 100
            Dim iEmplId As Integer = EmployeeBU.Create() 'we'll ask this employee and obtain the new key
            outStr += "Employee Added - key = " & iEmplId & vbCrLf
            EmployeeBU.GetByID(iEmplId) 'retrieve the employee just created
            outStr += "Employee " & iEmplId & " Retrieved" & vbCrLf
            'Update same employee with a small change
            EmployeeBU.Email = "js@abc.ca"
            Dim rowsUpdated As Integer = EmployeeBU.Update()
            outStr += "Employee " & iEmplId & " update - rows updated=" & rowsUpdated & vbCrLf
            Dim rowsDeleted As Integer = EmployeeBU.Delete(iEmplId) 'delete the rows we just added'
            outStr += "Employee " & iEmplId & " delete - rows deleted=" & rowsDeleted & vbCrLf
            'Get the remaining Employees  and display count
            Dim AllEmployees As List(Of EmployeeBusinessUser) = EmployeeBU.GetAll()
            outStr += "There are " & AllEmployees.Count & " Employees left in the DB" & vbCrLf
            MessageBox.Show(outStr, "Test Business User Object", MessageBoxButton.OK, MessageBoxImage.Information)
        Catch Ex As Exception
            MessageBox.Show("Problem, check Error Log", "Error", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try
    End Sub
End Class
