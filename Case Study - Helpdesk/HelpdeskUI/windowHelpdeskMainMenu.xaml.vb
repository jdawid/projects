
Public Class windowHelpdeskMainMenu

    Private Sub menuItemFileClose_Click(sender As Object, e As RoutedEventArgs) Handles menuItemFileClose.Click
        Me.Close()
        Application.Current.Shutdown()
    End Sub

    Private Sub menuItemEmployeeView_Click(sender As Object, e As RoutedEventArgs) Handles menuItemEmployeeView.Click
        Try
            Dim editor As windowEmployeeEditor = New windowEmployeeEditor
            editor.Title = "View - Employee Editor"
            editor.Show()
        Catch ex As Exception
            MessageBox.Show("Problem loading editor ", ex.Message, MessageBoxButton.OK, MessageBoxImage.Error)
        End Try
    End Sub

    Private Sub MenuItemDriversEmployeeBusinessData_Click(sender As Object, e As RoutedEventArgs) Handles MenuItemDriversEmployeeBusinessData.Click
        Dim driverWindow As windowEmployeeDataDriver = New windowEmployeeDataDriver()
        driverWindow.Show()
    End Sub

    Private Sub MenuItemDriversEmployeeBusinessUser_Click(sender As Object, e As RoutedEventArgs) Handles MenuItemDriversEmployeeBusinessUser.Click
        Dim driverWindow As WindowEmployeeUserDriver = New WindowEmployeeUserDriver()
        driverWindow.Show()
    End Sub
End Class
