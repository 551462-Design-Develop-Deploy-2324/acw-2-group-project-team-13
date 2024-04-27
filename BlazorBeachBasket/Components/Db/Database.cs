using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Routing;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Data.SQLite;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;
namespace BlazorBeachBasket.Components.Db
{
    class Database
    {
        public List<User> Users;
        public List<Restaurant> Restaurants;
        public List<Customer> Customers;
        public List<Driver> Drivers;
        public List<MenuItem> MenuItems;
        public List<Order> Orders;
        public List<PaymentCard> PaymentCards;
        public List<Image> Images;
        public Database()
        {
            Users = new List<User>();
            Restaurants = new List<Restaurant>();
            Customers = new List<Customer>();
            Drivers = new List<Driver>();
            MenuItems = new List<MenuItem>();
            Orders = new List<Order>();
            PaymentCards = new List<PaymentCard>();
            Images = new List<Image>();
        }

        public void ImportData()
        {
            string dbfile_path = "URI=file:BeachBasket.db";
            SQLiteConnection connection = new SQLiteConnection(dbfile_path);
            connection.Open();

            //Read Users
            string sql = "SELECT * FROM Users";
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            SQLiteDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    User user = new User(int.Parse(reader[0].ToString()), int.Parse(reader[1].ToString()), reader[2].ToString(), reader[3].ToString());
                    Users.Add(user);
                }
            }
            reader.Close();

            //Read Restaurants
            sql = "SELECT * FROM Restaurants";
            command = new SQLiteCommand(sql, connection);
            reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Restaurant restaurant = new Restaurant(int.Parse(reader[0].ToString()), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), int.Parse(reader[4].ToString()));
                    Restaurants.Add(restaurant);
                }
            }
            reader.Close();

            //Read Customers
            sql = "SELECT * FROM Customers";
            command = new SQLiteCommand(sql, connection);
            reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Customer customer = new Customer(int.Parse(reader[0].ToString()), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), int.Parse(reader[4].ToString()));
                    Customers.Add(customer);
                }
            }
            reader.Close();

            //Read Drivers
            sql = "SELECT * FROM Drivers";
            command = new SQLiteCommand(sql, connection);
            reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Driver driver = new Driver(int.Parse(reader[0].ToString()), reader[1].ToString(), reader[2].ToString(), int.Parse(reader[3].ToString()));
                    Drivers.Add(driver);
                }
            }
            reader.Close();

            //Read MenuItems
            sql = "SELECT * FROM MenuItems";
            command = new SQLiteCommand(sql, connection);
            reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    MenuItem menuItem = new MenuItem(int.Parse(reader[0].ToString()), reader[1].ToString(), double.Parse(reader[2].ToString()), int.Parse(reader[3].ToString()));
                    MenuItems.Add(menuItem);
                }
            }
            reader.Close();

            //Read Orders
            sql = "SELECT * FROM Orders";
            command = new SQLiteCommand(sql, connection);
            reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Order order = new Order(int.Parse(reader[0].ToString()), double.Parse(reader[2].ToString()), reader[1].ToString(), reader[3].ToString(), int.Parse(reader[4].ToString()),int.Parse(reader[5].ToString()), int.Parse(reader[6].ToString()));
                    Orders.Add(order);
                }
            }
            reader.Close();

            //Read PaymentCards
            sql = "SELECT * FROM PaymentCards";
            command = new SQLiteCommand(sql, connection);
            reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    PaymentCard card = new PaymentCard(int.Parse(reader[0].ToString()), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), int.Parse(reader[5].ToString()), int.Parse(reader[6].ToString()));
                    PaymentCards.Add(card);
                }
            }
            reader.Close();

            //Read Images
            sql = "SELECT * FROM Images";
            command = new SQLiteCommand(sql, connection);
            reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Image image = new Image(int.Parse(reader[0].ToString()), reader[1].ToString(), int.Parse(reader[2].ToString()), int.Parse(reader[3].ToString()));
                    Images.Add(image);
                }
            }
            reader.Close();
            connection.Close();

        }
        public void AssingData()
        {
            //Assigning the menu and orders for the restaurant.
            foreach(Restaurant restaurant in Restaurants)
            {
                foreach(MenuItem menuItem in MenuItems)
                {
                    if(menuItem.Item_RestId == restaurant.RestaurantId)
                    {
                        restaurant.Rest_Menu.Add(menuItem);
                    }
                }
                foreach(Order order in Orders)
                {
                    if(order.order_restaurantId == restaurant.RestaurantId)
                    {
                        restaurant.Rest_Orders.Add(order);
                    }
                }
                foreach(Image img in Images)
                {
                    if(img.Img_ItemType == 0 && restaurant.RestaurantId == img.Img_ItemId)
                    {
                        restaurant.Restaurant_img_link = img.ImgLink;
                    }
                }
            }

            foreach(Order order in Orders)
            {
                string[] itemIds = order.order_ItemIds.Split();
                foreach(string itemId in itemIds)
                {
                    if (itemId == "") { continue; }
                    MenuItem menuItem = MenuItems.FirstOrDefault(o => o.ItemId == int.Parse(itemId));
                    order.Order_items.Add(menuItem);
                }
            }

            foreach (MenuItem menuItem in MenuItems)
            {
                menuItem.Item_Image = Images.FirstOrDefault(o => o.Img_ItemId == menuItem.ItemId && o.Img_ItemType == 1);
            }
        }
        public void ExportData()
        {
            string dbfile_path = "URI=file:BeachBasket.db";
            SQLiteConnection connection = new SQLiteConnection(dbfile_path);
            connection.Open();
            string sql;
            SQLiteCommand cmd;

            //Write Users
            sql = "DELETE FROM Users";
            cmd = new SQLiteCommand(sql, connection);
            cmd.ExecuteNonQuery();
            foreach(User user in Users)
            {
                sql = $"Insert into Users values ({user.UserId},{user.UserType},'{user.Username}','{user.UserPassword}');";
                cmd = new SQLiteCommand(sql, connection);
                cmd.ExecuteNonQuery();
            }

            //Write Restaurants
            sql = "DELETE FROM Restaurants";
            cmd = new SQLiteCommand(sql, connection);
            cmd.ExecuteNonQuery();
            foreach (Restaurant restaurant in Restaurants)
            {
                sql = $"Insert into Restaurants values ({restaurant.RestaurantId},'{restaurant.RestaurantName}','{restaurant.RestaurantAddress}','{restaurant.RestaurantPhone}',{restaurant.Restaurant_UserId});";
                cmd = new SQLiteCommand(sql, connection);
                cmd.ExecuteNonQuery();
            }

            //Write Customers
            sql = "DELETE FROM Customers";
            cmd = new SQLiteCommand(sql, connection);
            cmd.ExecuteNonQuery();
            foreach(Customer customer in Customers)
            {
                sql = $"Insert into Customers values ({customer.CustomerId},'{customer.CustomerName}','{customer.CustomerAddress}','{customer.CustomerPhone}',{customer.Customer_UserId});";
                cmd = new SQLiteCommand(sql, connection);
                cmd.ExecuteNonQuery();
            }

            //Write Drivers
            sql = "DELETE FROM Drivers";
            cmd = new SQLiteCommand (sql, connection);
            cmd.ExecuteNonQuery();
            foreach (Driver driver in Drivers)
            {
                sql = $"Insert into Drivers values ({driver.DriverId},'{driver.DriverName}','{driver.DriverPhone}',{driver.Driver_UserId});";
                cmd = new SQLiteCommand(sql, connection);
                cmd.ExecuteNonQuery();
            }

            //Write MenuItems
            sql = "DELETE FROM MenuItems";
            cmd = new SQLiteCommand(sql, connection);
            cmd.ExecuteNonQuery();
            foreach (MenuItem menuItem in MenuItems)
            {
                sql = $"Insert into MenuItems values ({menuItem.ItemId},'{menuItem.ItemName}',{menuItem.ItemPrice},{menuItem.Item_RestId});";
                cmd = new SQLiteCommand(sql, connection);
                cmd.ExecuteNonQuery();
            }

            //Write Orders
            sql = "DELETE FROM Orders";
            cmd = new SQLiteCommand(sql, connection);
            cmd.ExecuteNonQuery();
            foreach(Order order in Orders)
            {
                sql = $"Insert into Orders values ({order.orderId},'{order.order_ItemIds}',{order.orderCost},'{order.orderStatus_str}',{order.order_restaurantId},{order.order_customerId},{order.order_driverId});";
                cmd = new SQLiteCommand(sql, connection);
                cmd.ExecuteNonQuery();
            }

            //Write PaymentCards
            sql = "DELETE FROM PaymentCards";
            cmd = new SQLiteCommand(sql, connection);
            cmd.ExecuteNonQuery();
            foreach(PaymentCard card in PaymentCards)
            {
                sql = $"Insert into PaymentCards values ({card.CardId},'{card.CardName}',{card.Card16digit},'{card.CardValid}','{card.CardExpiry}',{card.CardCVC},{card.Card_UserId});";
                cmd = new SQLiteCommand(sql, connection);
                cmd.ExecuteNonQuery();
            }

            //Write Images
            sql = "DELETE FROM Images";
            cmd = new SQLiteCommand(sql, connection);
            cmd.ExecuteNonQuery();
            foreach (Image img in Images)
            {
                sql = $"Insert into ImgLinks values ({img.ImgId},'{img.ImgLink}',{img.Img_ItemType},{img.Img_ItemId});";
                cmd = new SQLiteCommand(sql, connection);
                cmd.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    


    //Classes
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
        public string Restaurant_img_link { get; set; }
        public List<MenuItem> Rest_Menu { get; set; }
        public List<Order> Rest_Orders { get; set; }
        public Restaurant(int restaurantId, string restaurantName, string restaurantAddress, string restaurantPhone, int restaurant_UserId, List<MenuItem> rest_Menu, List<Order> rest_Orders)
        {
            RestaurantId = restaurantId;
            RestaurantName = restaurantName;
            RestaurantAddress = restaurantAddress;
            RestaurantPhone = restaurantPhone;
            Restaurant_UserId = restaurant_UserId;
            Rest_Menu = rest_Menu;
            Rest_Orders = rest_Orders;
        }
        public Restaurant(int restaurantId, string restaurantName, string restaurantAddress, string restaurantPhone, int restaurant_UserId)
        {
            RestaurantId = restaurantId;
            RestaurantName = restaurantName;
            RestaurantAddress = restaurantAddress;
            RestaurantPhone = restaurantPhone;
            Restaurant_UserId = restaurant_UserId;
            Rest_Menu = new List<MenuItem>();
            Rest_Orders = new List<Order>();
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
        public Driver()
        {

        }
    }
    class MenuItem
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public double ItemPrice { get; set; }
        public int Item_RestId { get; set; }
        public Image Item_Image { get; set; }

        public MenuItem(int pitemId, string pitemName, double pitemPrice, int item_RestId)
        {
            ItemId = pitemId;
            ItemName = pitemName;
            ItemPrice = pitemPrice;
            Item_RestId = item_RestId;
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
        public List<MenuItem> Order_items = new List<MenuItem>();
        public enum OrderStatus { Preparing, Enroute, Delivered}
        public OrderStatus orderStatus = OrderStatus.Preparing;
        public string orderStatus_str;
        public int order_restaurantId;
        public int order_customerId;
        public int order_driverId;

        public Order(int porderId, double porderCost, string porder_ItemIds, string porderStatus_str, int porder_restaurantId ,int porder_customerId, int porder_driverId)
        {
            orderId = porderId;
            orderCost = porderCost;
            order_ItemIds = porder_ItemIds;
            orderStatus_str = porderStatus_str;
            order_restaurantId = porder_restaurantId;
            order_customerId = porder_customerId;
            order_driverId = porder_driverId;

            if(orderStatus_str == "Preparing")
            {
                orderStatus = OrderStatus.Preparing;
            }
            else if(orderStatus_str == "Enroute")
            {
                orderStatus=OrderStatus.Enroute;
            }
            else if(orderStatus_str == "Delivered")
            {
                orderStatus = OrderStatus.Delivered;
            }
        }
        public Order()
        {

        }
    }
    class PaymentCard
    {
        public int CardId;
        public string CardName;
        public string Card16digit;
        public string CardValid;
        public string CardExpiry;
        public int CardCVC;
        public int Card_UserId;
        public PaymentCard(int cardId, string cardName, string card16digit, string cardValid, string cardExpiry, int cardCVC, int card_UserId)
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
    class Image
    {
        public int ImgId;
        public string ImgLink;
        public int Img_ItemType;
        public int Img_ItemId;

        public Image(int pImgId, string pImgLink,int pImg_ItemType, int pImg_ItemId)
        {
            ImgId = pImgId;
            ImgLink = pImgLink;
            Img_ItemType = pImg_ItemType;
            Img_ItemId = pImg_ItemId;
        }

        public Image()
        {

        }
    }
}
