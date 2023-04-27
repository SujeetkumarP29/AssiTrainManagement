using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;

namespace AssiTrain
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SqlConnection trn = new SqlConnection("Server=localhost;Database=train;Integrated Security=True");
            trn.Open();

            SqlCommand cmd = trn.CreateCommand();

            bool A = true;
            while (A)
            {

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("=====================================================================================");
                Console.WriteLine("|               Choose the Below option for What would you like to do?              |");
                Console.WriteLine("|               |1|. Create train                                                   |");
                Console.WriteLine("|               |2|. create train class                                             |");
                Console.WriteLine("|               |3|. create booking ticket                                          |");
                Console.WriteLine("|               |4|. Display train table                                            |");
                Console.WriteLine("|               |5|. Display class table                                            |");
                Console.WriteLine("|               |6|. Display booking table                                          |");
                Console.WriteLine("|               |7|. Display peoples have booked Rani chenamma on april 27 2023     |");
                Console.WriteLine("|               |8|. Displaying peoples who have booked particular class on april 27|");
                Console.WriteLine("=====================================================================================");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Enter train number:");
                        int TNUM = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter TRAIN NAME: ");
                        string TNAME = Console.ReadLine();
                        Console.WriteLine("Enter train departure(source)");
                        string TSOURCE = Console.ReadLine();
                        Console.WriteLine("Enter train destination:");
                        string TDES = Console.ReadLine();

                        cmd.CommandText = "INSERT INTO trainTb VALUES('" + TNUM + "','" + TNAME + "',' " + TSOURCE + "',' " + TDES + "')";
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("DATA Inserted successfully");
                        Console.ReadLine();
                        break;
                    case "2":
                        Console.WriteLine("Enter class ID");
                        int class_id = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter class name ");
                        string class_name = Console.ReadLine();
                        Console.WriteLine("Enter the class price");
                        int class_price = int.Parse(Console.ReadLine());
                        cmd.CommandText = "INSERT INTO ticket_classTb VALUES('" + class_id + "','" + class_name + "',' " + class_price + "')";
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("DATA Inserted successfully");
                        Console.ReadLine();
                        break;
                    case "3":
                        Console.WriteLine("Enter booking ID");
                        int Booking_id = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter Customer name");
                        string Customer_name = Console.ReadLine();
                        Console.WriteLine("Enter train number:");
                        int TNUM1 = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter class ID");
                        int class_id1 = int.Parse(Console.ReadLine());
                        Console.WriteLine("Please enter a date (in the format of yyyy-MM-dd):");
                        string datein = Console.ReadLine();
                        DateTime date;

                        if (DateTime.TryParse(datein, out date))
                        {
                            Console.WriteLine("The converted date is: " + date.ToString());
                        }
                        else
                        {
                            Console.WriteLine("Invalid date format.");
                        }
                        cmd.CommandText = "INSERT INTO ticket_bookingTb VALUES('" + Booking_id + "','" + Customer_name + "',' " + TNUM1 + "',' " + class_id1 + "','" + datein + "')";
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("DATA Inserted successfully");
                        Console.ReadLine();
                        break;
                    case "4":
                        Console.ForegroundColor = ConsoleColor.Green;
                        cmd.CommandText = "SELECT * FROM trainTb";
                        SqlDataReader records = cmd.ExecuteReader();
                        var tb = new ConsoleTable("train_no", "name", "source", "destination");
                        while (records.Read())
                        {
                            tb.AddRow(records["train_no"], records["name"], records["source"], records["destination"]);
                        }
                        records.Close();
                        tb.Write();
                        break;
                    case "5":
                        Console.ForegroundColor = ConsoleColor.Green;
                        cmd.CommandText = "SELECT * FROM ticket_classTb";
                        SqlDataReader records3 = cmd.ExecuteReader();
                        var tb3 = new ConsoleTable("class_id", "class_name", "price");
                        while (records3.Read())
                        {
                            tb3.AddRow(records3["class_id"], records3["class_name"], records3["price"]);
                        }
                        records3.Close();
                        tb3.Write();
                        break;
                    case "6":
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        cmd.CommandText = "SELECT * FROM ticket_bookingTb";
                        SqlDataReader records4 = cmd.ExecuteReader();
                        var tb4 = new ConsoleTable("booking_id", "customer_name", "train_no", "class_id", "Booking_date");
                        while (records4.Read())
                        {
                            tb4.AddRow(records4["booking_id"], records4["customer_name"], records4["train_no"], records4["class_id"], records4["Booking_date"]);
                        }
                        records4.Close();
                        tb4.Write();
                        break;
                    case "7":
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Enter train number:");
                        int TNUM2 = int.Parse(Console.ReadLine());
                        Console.WriteLine("Please enter a date (in the format of yyyy-MM-dd):");
                        string datein1 = Console.ReadLine();
                        DateTime dates;

                        if (DateTime.TryParse(datein1, out dates))
                        {
                            Console.WriteLine("The converted date is: " + dates.ToString());
                            string sqlQuery = $"SELECT booking_id, customer_name, train_no, class_id, Booking_date FROM Ticket_bookingTb WHERE Booking_date = '{datein1}' and train_no ='{TNUM2}'";
                            SqlCommand cmd1 = new SqlCommand(sqlQuery, trn);
                            SqlDataReader records1 = cmd1.ExecuteReader();
                            var tb1 = new ConsoleTable("booking_id", "customer_name", "train_no", "class_id", "Booking_date");
                            while (records1.Read())
                            {
                                tb1.AddRow(records1["booking_id"], records1["customer_name"], records1["train_no"], records1["class_id"], records1["Booking_date"]);
                            }
                            records1.Close();
                            tb1.Write();
                        }
                        else
                        {
                            Console.WriteLine("Invalid date format.");
                        }
                       
                        break;
                    case "8":
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Enter class ID");
                        int class_id2 = int.Parse(Console.ReadLine());
                        Console.WriteLine("Please enter a date (in the format of yyyy-MM-dd):");
                        string datein2 = Console.ReadLine();
                        DateTime dates1;

                        if (DateTime.TryParse(datein2, out dates1))
                        {
                            Console.WriteLine("The converted date is: " + dates1.ToString());
                            string sqlQuery = $"SELECT booking_id, customer_name, train_no, class_id, Booking_date FROM Ticket_bookingTb WHERE Booking_date = '{datein2}' and class_id ='{class_id2}'";
                            SqlCommand cmd1 = new SqlCommand(sqlQuery, trn);
                            SqlDataReader records2 = cmd1.ExecuteReader();
                            var tb2 = new ConsoleTable("booking_id", "customer_name", "train_no", "class_id", "Booking_date");
                            while (records2.Read())
                            {
                                tb2.AddRow(records2["booking_id"], records2["customer_name"], records2["train_no"], records2["class_id"], records2["Booking_date"]);
                            }
                            records2.Close();
                            tb2.Write();
                        }
                        else
                        {
                            Console.WriteLine("Invalid date format.");
                        }
                        break;





                }

            }
            trn.Close();
        }
    }
}
