using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.IO;


namespace Weatherman
{
    class Program
    {
        static void DownloadWeather(int zipcode)
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
            //Console.WriteLine(rawResponse);
            //Console.WriteLine("--------------------------");
            Console.WriteLine($"City is: {MyWeather.name}");
            Console.WriteLine($"The Temperature is: {MyWeather.main.temp}");
            Console.WriteLine($"Current conditions: Pressure = {MyWeather.main.pressure}" + $" Humidity = {MyWeather.main.humidity}");
        }

        static void Main(string[] args)

            
        {
            Console.WriteLine("Please enter your name");
            var InputName = Console.ReadLine();
            Console.WriteLine("please enter your zip");
            var InputZip = Console.ReadLine();
            Console.ReadLine();
            DownloadWeather(int.Parse(InputZip));
            Console.WriteLine();
            Console.ReadLine();
        }
        
                          
        

}
       
}
