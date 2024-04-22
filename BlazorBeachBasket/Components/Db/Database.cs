namespace BlazorBeachBasket.Components.Db
{
    public class Database
    {
    }
    class User
    {
        public int UserId { get; set; }
        public int UserType {  get; set; }
        public string Username { get; set; }
        public string UserPassword { get; set; }

        public User(int userId, int userType, string username, string userPassword)
        {
            UserId = userId;
            UserType = userType;
            Username = username;
            UserPassword = userPassword;
        }
        public User() { }
    }
    class Restaurant
    {
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public string RestaurantAddress { get; set; }
        public string RestaurantPhone { get; set; }
        public int Restaurant_UserId { get; set; }

        public Restaurant(int restaurantId, string restaurantName, string restaurantAddress, string restaurantPhone, int restaurant_UserId)
        {
            RestaurantId = restaurantId;
            RestaurantName = restaurantName;
            RestaurantAddress = restaurantAddress;
            RestaurantPhone = restaurantPhone;
            Restaurant_UserId = restaurant_UserId;
        }
        public Restaurant() { }
    }
    class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhone { get; set; }
        public int Customer_UserId { get; set;}

        public Customer(int customerId, string customerName, string customerAddress, string customerPhone, int customer_UserId)
        {
            CustomerId = customerId;
            CustomerName = customerName;
            CustomerAddress = customerAddress;
            CustomerPhone = customerPhone;
            Customer_UserId = customer_UserId;
        }
        public Customer() { }
    }
    class Driver
    {
        public int DriverId { get; set; }
        public string DriverName { get; set; }
        public string DriverPhone { get; set; }
        public int Driver_UserId { get; set;}

        public Driver(int driverId, string driverName, string driverPhone, int driver_UserId)
        {
            DriverId = driverId;
            DriverName = driverName;
            DriverPhone = driverPhone;
            Driver_UserId = driver_UserId;
        }
    }
    class MenuItem
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public double ItemPrice { get; set; }

        public MenuItem(int pitemId, string pitemName, double pitemPrice)
        {
            ItemId = pitemId;
            ItemName = pitemName;
            ItemPrice = pitemPrice;
        }
        public MenuItem()
        {

        }
    }
    class Order
    {
        public int orderId;
        public double orderCost;
        public string order_ItemIds;
        public enum OrderStatus { Preparing, Enroute, Delivered}
        public OrderStatus orderStatus;
        public int order_customerId;
        public int order_driverId;

        public Order(int porderId, double porderCost, string porder_ItemIds, OrderStatus porderStatus, int porder_customerId, int porder_driverId)
        {
            orderId = porderId;
            orderCost = porderCost;
            order_ItemIds = porder_ItemIds;
            orderStatus = porderStatus;
            order_customerId = porder_customerId;
            order_driverId = porder_driverId;
        }
        public Order()
        {

        }
    }
    class PaymentCard
    {
        public int CardId;
        public string CardName;
        public int Card16digit;
        public DateOnly CardValid;
        public DateOnly CardExpiry;
        public int CardCVC;
        public int Card_UserId;
        public PaymentCard(int cardId, string cardName, int card16digit, DateOnly cardValid, DateOnly cardExpiry, int cardCVC, int card_UserId)
        {
            CardId = cardId;
            CardName = cardName;
            Card16digit = card16digit;
            CardValid = cardValid;
            CardExpiry = cardExpiry;
            CardCVC = cardCVC;
            Card_UserId = card_UserId;
        }
        public PaymentCard()
        {

        }
    }
}


/* Yoink example
    string dbfile_path = "URI=file:SPSST_SQLiteDB.db";
    SQLiteConnection connection = new SQLiteConnection(dbfile_path);
    connection.Open();

    //Read Senior Tutors
    string sql = "SELECT * FROM SeniorTutors";
    SQLiteCommand cmd = new SQLiteCommand(sql, connection);
    SQLiteDataReader reader = cmd.ExecuteReader();

    if (reader.HasRows)
    {
        while (reader.Read())
        {
            SeniorTutor seniorTutor = new SeniorTutor(int.Parse(reader[0].ToString()), reader[1].ToString(), reader[2].ToString(), reader[3].ToString()); // ID Password Firstname Surname 
            this.AddSTtoSPSST(seniorTutor);
        }
    }
    reader.Close(); 
*/