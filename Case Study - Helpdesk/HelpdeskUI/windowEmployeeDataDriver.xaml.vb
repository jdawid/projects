'Imports HelpdeskBusinessDataObjects
'Public Class windowEmployeeDataDriver

'    Private Sub btnTest_Click(sender As Object, e As RoutedEventArgs) Handles btnTest.Click
'        Dim DictionaryData As New Dictionary(Of String, Object) 'Load an Employee into a Dictionary
'        Dim outStr As String = ""

'        Try
'            Dim EmployeeBD As EmployeeBusinessData = New EmployeeBusinessData
'            DictionaryData("title") = "Mr."
'            DictionaryData("firstname") = "John"
'            DictionaryData("lastname") = "smith"
'            DictionaryData("phoneno") = "555-5555"
'            DictionaryData("email") = "js@abc.com"
'            DictionaryData("departmentid") = 100
'            'add employee, Obtain gernerated PK
'            Dim iEmplId As Integer = EmployeeBD.Create(ConfigBusinessData.Serializer(DictionaryData))
'            outStr += "Employee Added - key = " & iEmplId & vbCrLf
'            'Get employee just added
'            DictionaryData = ConfigBusinessData.Deserializer(EmployeeBD.GetByID(iEmplId))
'            outStr += "Employee " & iEmplId & " Retrieved" & vbCrLf
'            'Update same employee with a small change
'            DictionaryData("email") = "js@abc.ca"
'            Dim rowsUpdated As Integer = EmployeeBD.Update(ConfigBusinessData.Serializer(DictionaryData))
'            outStr += "Employee " & iEmplId & " update - rows updated=" & rowsUpdated & vbCrLf
'            'Delete same employee
'            Dim rowsDeleted As Integer = EmployeeBD.Delete(iEmplId)
'            outStr += "Employee " & iEmplId & " delete - rows deleted=" & rowsDeleted & vbCrLf
'            'Get the remaining Employees and display count
'            Dim AllEmployess As List(Of Employee) = EmployeeBD.GetAll()
'            outStr += "There are " & AllEmployess.Count & " Employees left in the DB" & vbCrLf

'            MessageBox.Show(outStr, "Test EmployeeBusinessData Object", _
'                            MessageBoxButton.OK, MessageBoxImage.Information)
'        Catch ex As Exception
'            MessageBox.Show("Problem doing EmployeeBusinessData routies", _
'                            "check event viewer for details", _
'                            MessageBoxButton.OK, MessageBoxImage.Error)

'        End Try
'    End Sub
'End Class
