Public Class Car

    Private Shared carCounter As Integer ' static or shared private variable to hold the number of cars
    Private carIdentificationNumber As Integer = 0 ' private variable to hold the car's identification number
    Private carMake As String = String.Empty ' private variable to hold the car's make
    Private carModel As String = String.Empty ' private variable to hold the car's model
    Private carYear As String = String.Empty ' private variable to hold the car's year
    Private carPrice As Decimal = 0.00D ' private variable to hold the car's price as an decimal datatype
    Private carIsNew As Boolean = False ' private variable to hold the car's status new or used
    ' Constructor - this creates a new car object
    Public Sub New()
        carCounter += 1 'incrementing the shared / static variable counter by 1
        carIdentificationNumber = carCounter 'assigns the car identification number
    End Sub
    'Constructor: Creates a new car object using parameters 
    Public Sub New(makeOfCar As String, modelOfCar As String, yearOfCar As String, priceOfCar As String, isNewCar As Boolean)
        'calling another constructor to car cound and set the car identificatinon number
        Me.New()
        carMake = makeOfCar ' set the car make
        carModel = modelOfCar ' set the car model
        carYear = yearOfCar ' set the car manufacturing year
        carPrice = priceOfCar ' set the car price
        carIsNew = isNewCar ' set to check the car status

    End Sub
    ' Count ReadOnly Property - Gets the number of cars that have been instantiated/created
    Public ReadOnly Property Count() As Integer
        Get ' This will get the selected car ID number 
            Return carCounter
        End Get
    End Property
    'IdentificationNumber ReadOnly Property - Gets a specific cars identification number
    Public ReadOnly Property IdentificationNumber() As Integer
        Get ' This will get the selected car ID number 
            Return carIdentificationNumber
        End Get
    End Property
    'Make property - Gets and Sets the make of the car
    Public Property Make() As String
        Get ' This will get the car make
            Return carMake
        End Get
        Set(ByVal value As String) 'This will set the car make
            carMake = value
        End Set
    End Property
    'Model property - Gets and Sets the model of the car
    Public Property Model() As String
        Get ' This will get the car model
            Return carModel
        End Get
        Set(ByVal value As String) 'This will set the car make
            carModel = value
        End Set
    End Property
    'Year property - Gets and Sets the year of the car
    Public Property Year() As String
        Get ' This will get the car model
            Return carYear
        End Get
        Set(ByVal value As String) 'This will set the car make
            carYear = value
        End Set
    End Property
    'Price property - Gets and Sets the price of the car
    Public Property Price() As String

        Get ' This will get the car model
            Return carPrice
        End Get
        Set(ByVal value As String) 'This will set the car make
            carPrice = value
        End Set
    End Property
    'NewStatus Property - Gets and Sets the status for used or new car
    Public Property NewStatus() As Boolean
        Get ' This will get the car's status
            Return carIsNew
        End Get
        Set(ByVal value As Boolean) 'This will set the car status
            carIsNew = value
        End Set
    End Property
    'GetCarData is a function that displays a string based on the private properties within the class scope
    Public Function GetCarData() As String

        Return "The model " & carModel & " made by " & carMake & " was manufactured in the year of " & carYear &
            " and it's current price is " & Format(carPrice, "Currency") & ". " & IIf(carIsNew = True, "It is a new car.", "It is a used car.").ToString()

    End Function

End Class