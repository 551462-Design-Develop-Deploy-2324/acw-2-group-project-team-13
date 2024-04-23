﻿using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Routing;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Data.SQLite;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;
namespace BlazorBeachBasket.Components.Db
{
    public class Database
    {
        List<User> Users = new List<User>();
        List<Restaurant> Restaurants = new List<Restaurant>();
        List<Customer> Customers = new List<Customer>();
        List<Driver> Drivers = new List<Driver>();
        List<MenuItem> MenuItems = new List<MenuItem>();
        List<Order> Orders = new List<Order>();
        List<PaymentCard> PaymentCards = new List<PaymentCard>();

        public Database()
        {

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
                    Order order = new Order(int.Parse(reader[0].ToString()), double.Parse(reader[1].ToString()), reader[2].ToString(), reader[3].ToString(), int.Parse(reader[4].ToString()),int.Parse(reader[5].ToString()), int.Parse(reader[6].ToString()));
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
                    PaymentCard card = new PaymentCard(int.Parse(reader[0].ToString()), reader[1].ToString(), int.Parse(reader[2].ToString()), reader[3].ToString(), reader[4].ToString(), int.Parse(reader[5].ToString()), int.Parse(reader[6].ToString()));
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
            }

            foreach(Order order in Orders)
            {
                string[] itemIds = order.order_ItemIds.Split();
                foreach(string itemId in itemIds)
                {
                    MenuItem menuItem = MenuItems.FirstOrDefault(o => o.ItemId == int.Parse(itemId));
                    order.Order_items.Add(menuItem);
                }
            }
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
    }
    class MenuItem
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public double ItemPrice { get; set; }
        public int Item_RestId { get; set; }
        public string Item_Imglink { get; set; }

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
        public int Card16digit;
        public string CardValid;
        public string CardExpiry;
        public int CardCVC;
        public int Card_UserId;
        public PaymentCard(int cardId, string cardName, int card16digit, string cardValid, string cardExpiry, int cardCVC, int card_UserId)
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

    class ImageClass()
    {
        public int ImgId { get; set; }
        public string ImgLink { get; set; }
        public int Img_ItemId { get; set; }

        public ImageClass(int pImgId, string pImgLink, int pImg_ItemId)
        {
            ImgId = pImgId;
            ImgLink = pImgLink;
            Img_ItemId = pImg_ItemId;
        }
        public ImageClass()
        {

        }
    }
}
