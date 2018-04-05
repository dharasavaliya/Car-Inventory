Option Strict On
''' 
''' Name:           CarInventory
''' Student Name:   Dhara Savaliya
''' Student ID:     100648840
''' Date:           20-March-2018
''' Site:           CustomerList-Master 
''' Description:    This project is used to create car inventory using Car class. The instatited data displays in List view. 
'''                 We can update the data by selecting it. This also verifies the user input.  
Public Class frmCarInventory

    Private carList As New SortedList 'collection of all instantiated car list from the list
    Private carIdentificationNumber As String = String.Empty ' used to select the current car identification number
    Private editMode As Boolean = False ' editmode is used to set the flag

    Private Sub btnEnter_Click(sender As Object, e As EventArgs) Handles btnEnter.Click
        Dim carData As Car 'declares an object for Car class
        Dim carItem As ListViewItem 'declares a ListViewItem class 

        If ValidInput() = True Then ' Calls the function ValidInput for validation 
            editMode = True 'if valid input is given then it sets the flag to true


            ' if the current car identification number has a no value
            ' then this is not an existing item from the listview
            If carIdentificationNumber.Trim.Length = 0 Then

                ' This creates a new object by passing parameters 
                carData = New Car(cmbMake.Text, txtModel.Text, cmbYear.Text, txtPrice.Text, chkNew.Checked)

                'add the car to the colection of car list using identification number to find it easily
                carList.Add(carData.IdentificationNumber.ToString(), carData)
            Else
                ' If the identification number has a value that means a user has selecte an object from the list. 
                carData = CType(carList.Item(carIdentificationNumber), Car)

                'This will upate the data with the old data from the controls 
                carData.Make = cmbMake.Text
                carData.Model = txtModel.Text
                carData.Year = cmbYear.Text
                carData.Price = txtPrice.Text
                carData.NewStatus = chkNew.Checked
            End If

            ' clears the items from the listview control 
            lvwCars.Items.Clear()

            ' This will loop carList collection and populates the list view
            For index As Integer = 0 To carList.Count - 1

                'instantiates a new ListViewItem
                carItem = New ListViewItem()

                'This will get the car from the list
                carData = CType(carList(carList.GetKey(index)), Car)

                'assign a value to the checked control and the subitems
                carItem.Checked = carData.NewStatus
                carItem.SubItems.Add(carData.IdentificationNumber.ToString)
                carItem.SubItems.Add(carData.Make)
                carItem.SubItems.Add(carData.Model)
                carItem.SubItems.Add(carData.Year)
                carItem.SubItems.Add(Format(carData.Price, "Currency")) 'Format: formats the user entered price to the currency and display it with two rounded decimal points

                '  add the new instantiated and populated ListViewItem to the listview control
                lvwCars.Items.Add(carItem)
            Next index ' increments index with 1

            ' Clears the form
            ResetForm() ' To display it worked! it is commented for not using it
            ' Set the flag to false
            editMode = False
        End If

    End Sub

    ' This function is used to verify the user input is not empty string, or character or less than zero (0) 
    Private Function ValidInput() As Boolean
        Dim carPrice As Decimal = 0.00D ' declares a decimal variable to store price of the car  
        Dim isValidInput As Boolean = True ' sets the boolean according to user input
        Dim displayMessage As String = String.Empty ' used to display error messages

        If cmbMake.SelectedIndex = -1 Then 'if combobox for make is not selected then it will display a message
            displayMessage += "Please select the car make from the list" & vbCrLf 'This will display error message for car make + new line 
            isValidInput = False 'sets the isValidINput to false to and return it to reprompt the valid data
        End If
        If txtModel.Text.Trim.Length = 0 Then 'If model text is an empty string it returns false and prints error message 
            displayMessage += "Please enter the car model" & vbCrLf
            isValidInput = False
        End If
        If cmbYear.SelectedIndex = -1 Then 'if combobox for year is not selected then it will display a message
            displayMessage += "Please select the car year from the list" & vbCrLf
            isValidInput = False
        End If
        If txtPrice.Text.Trim.Length = 0 Then ' If price text is an empty string it returns false and prints error message
            displayMessage += "Please enter the car price" & vbCrLf
            isValidInput = False
        Else 'If it is not empty then it chekcs for valid double input
            If Decimal.TryParse(txtPrice.Text, carPrice) Then 'Try to pare the carPrice with user input  if it is true then 
                If carPrice < 0 Then ' Checks user input is not less than 0
                    isValidInput = False 'sets isValidInput to false
                    displayMessage += "Please enter the valid car price. It must be greater than 0." & vbCrLf 'Prints error message for valid input
                Else
                    If displayMessage = String.Empty Then
                        isValidInput = True 'if user input is greater than 0 then it sets isValidInput to true
                    Else
                        isValidInput = False 'sets isValidInput to false
                    End If
                End If
            Else
                displayMessage += "Please enter the car price in numbers only" & vbCrLf ' If user input is character then prints a message
                isValidInput = False ' sets isValidInput to false
            End If

        End If

        If isValidInput = False Then ' Prints all error messages if any
            lblErrorOrResultList.Text = "ERROR(S):" & vbCrLf & displayMessage
        Else ' If user enters valid input it displays the message showing "it worked"
            lblErrorOrResultList.Text = "It worked!" ' displays a mesage when proper user input is given and isValidInput is true
        End If

        Return isValidInput 'Returns the boolean value. if it is true then valid user input else false for invalid input

    End Function
    ' This will ensure by preventing user to check the chekc box in the list view 
    Private Sub lvwCars_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles lvwCars.ItemCheck

        If editMode = False Then ' if it is not in editmode then
            e.NewValue = e.CurrentValue ' it can not be edit in the list view by the user
        End If
    End Sub
    ' This will allow a user to edit the selected row by populating the fiels for editing
    Private Sub lvwCars_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvwCars.SelectedIndexChanged

        ' constant that represents the index of the subitem in the list that
        ' holds the car identification number 
        Const identificationSubItemIndex As Integer = 1

        ' Get the car identification number 
        carIdentificationNumber = lvwCars.Items(lvwCars.FocusedItem.Index).SubItems(identificationSubItemIndex).Text

        ' Use the car identification number to get the car from the collection object
        Dim cardata As Car = CType(carList.Item(carIdentificationNumber), Car)
        cmbMake.Text = cardata.Make ' get the make and set the text box
        txtModel.Text = cardata.Model 'get the model and set the text box
        cmbYear.Text = cardata.Year 'get the year and set the text box
        txtPrice.Text = cardata.Price ' get the price and set the text box
        chkNew.Checked = cardata.NewStatus 'get the new car status and set the combo box

        'This calls the function GetCarData() and displays the string in lblErrorOrResultList result label
        lblErrorOrResultList.Text = cardata.GetCarData()


    End Sub
    Private Sub ResetForm()

        cmbMake.SelectedIndex = -1 ' This is used to set the checkbox to -1 index for not showing any option by default for the car Make
        txtModel.Text = String.Empty ' This will clear the textbox of car Model
        cmbYear.SelectedIndex = -1 ' This is used to set the checkbox to -1 index for not showing any option by default for the car Year
        txtPrice.Text = String.Empty ' This will clear the textbox of car Price
        chkNew.Checked = False ' Sets the checkbox unselected by default
        'lblErrorOrResultList.Text = String.Empty 'Clears the lblErrorOrResultList text which displays errors or success messages
        carIdentificationNumber = String.Empty 'Sets the carIdentificationNumber variable value to empty
        editMode = False
        cmbMake.Focus() ' Sets the focus to the combo box for car make 

    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click

        ' This reset button click event is used to reset the form
        ResetForm()
        lblErrorOrResultList.Text = String.Empty 'Clears the lblErrorOrResultList text which displays errors or success messages


    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        ' This button click event will clost the form 
        Me.Close()
    End Sub

End Class
