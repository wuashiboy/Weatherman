using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Data.SqlClient;


namespace Weatherman
{
    class Program
    {

        static void AddWeather(string InputName, Weatherdata MyWeather)
        {
            var connectionStrings = @"Server=localhost\SQLEXPRESS;Database=RecordedTemp;Trusted_Connection=True;";
            using (var connection = new SqlConnection(connectionStrings))
           {
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    connection.Open();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = @"INSERT INTO Temp (Name, Temp, Pressure, Humidity, Time)" +
                                        "Values (@Name, @Temp, @Pressure, @Humidity, @Time)";

                    cmd.Parameters.AddWithValue("@Name", InputName);
                    cmd.Parameters.AddWithValue("@Temp", MyWeather.main.temp);
                    cmd.Parameters.AddWithValue("@Pressure", MyWeather.main.pressure);
                    cmd.Parameters.AddWithValue("@Humidity", MyWeather.main.humidity);
                    cmd.Parameters.AddWithValue("@Time", DateTime.Now);

                    cmd.ExecuteNonQuery();
                }
            }
            
        }

        
        static void DownloadWeather(string InputName, int zipcode)
        {
            var token = "beb11bacb7aa189b2ddbb3f0f37f8c3f";
            var url = ($"http://api.openweathermap.org/data/2.5/weather?zip={zipcode},us&appid=" + token);
            var request = WebRequest.Create(url);
            request.ContentType = "application/json; charset=uft-8";

            var rawResponse = string.Empty;
            var response = request.GetResponse();
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                rawResponse = reader.ReadToEnd();
            }

            var MyWeather = JsonConvert.DeserializeObject<Weatherdata>(rawResponse);
            AddWeather(InputName, MyWeather);
            //Console.WriteLine(rawResponse);
            //Console.WriteLine("--------------------------");
            Console.WriteLine($"City Name: {MyWeather.name}");
            Console.WriteLine($"The Temperature is: {MyWeather.main.temp}");
            Console.WriteLine($"Current conditions: Pressure = {MyWeather.main.pressure}" + $" Humidity = {MyWeather.main.humidity}");
        }
        

        static void Main(string[] args)
        {
            
                Console.WriteLine("Please enter your name");
                var InputName = Console.ReadLine();
                Console.WriteLine("please enter your zip");
                var InputZip = Console.ReadLine();
                //Console.ReadLine();
                DownloadWeather(InputName, int.Parse(InputZip));
                Console.ReadLine();
                      
        }
        
                          
        

    }   
       
}
