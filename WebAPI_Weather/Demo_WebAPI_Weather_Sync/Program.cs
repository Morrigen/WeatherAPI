using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

namespace Demo_WebAPI_Weather
{
    class Program
    {
        static void Main(string[] args)
        {
            DisplayOpeningScreen();
            DisplayMenu();
            DisplayClosingScreen();
        }

        static void DisplayMenu()
        {
            bool quit = false;
            LocationZipcode zipcode = new LocationZipcode(0);

            while (!quit)
            {
                DisplayHeader("Main Menu");

                Console.WriteLine("Enter the number of your menu choice.");
                Console.WriteLine();
                Console.WriteLine("1) Set the Location");
                Console.WriteLine("2) Display the Current Weather");
                Console.WriteLine("3) Exit");
                Console.WriteLine();
                Console.Write("Enter Choice:");
                string userMenuChoice = Console.ReadLine();

                switch (userMenuChoice)
                {
                    case "1":
                        zipcode = DisplayGetLocation();
                        break;

                    case "2":
                        DisplayCurrentWeather(zipcode);
                        break;

                    case "3":
                        quit = true;
                        break;

                    default:
                        Console.WriteLine("You must enter a number from the menu.");
                        break;
                }
            }
        }

        static void DisplayOpeningScreen()
        {
            //
            // display an opening screen
            //
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Weather Reporter");
            Console.WriteLine();
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        static void DisplayClosingScreen()
        {
            //
            // display an closing screen
            //
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Thank you for using my application!");
            Console.WriteLine();
            Console.WriteLine();

            //
            // display continue prompt
            //
            Console.WriteLine();
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
            Console.WriteLine();
        }

        static void DisplayContinuePrompt()
        {
            //
            // display continue prompt
            //
            Console.WriteLine();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            Console.WriteLine();
        }

        static void DisplayHeader(string headerText)
        {
            //
            // display header
            //
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine(headerText);
            Console.WriteLine();
        }

        static LocationZipcode DisplayGetLocation()
        {
            DisplayHeader("Set Location by Zipcode");

            LocationZipcode zipcode = new LocationZipcode();

            //Console.Write("Enter Latitude: ");
            //coordinates.Latitude = double.Parse(Console.ReadLine());

            //Console.Write("Enter longitude: ");
            //coordinates.Longitude = double.Parse(Console.ReadLine());

            Console.Write("Enter Zipcode: ");
            zipcode.Zipcode = int.Parse(Console.ReadLine());

            Console.WriteLine();
            Console.WriteLine($"Location Zipcode: ({zipcode.Zipcode})");
            Console.WriteLine();

            DisplayContinuePrompt();

            return zipcode;
        }

        static WeatherData GetCurrentWeatherData(LocationZipcode zipcode)
        {
            string url;

            StringBuilder sb = new StringBuilder();
            sb.Clear();
            sb.Append("http://api.openweathermap.org/data/2.5/weather?");
            sb.Append("&zip=" + zipcode.Zipcode.ToString());            
            //sb.Append("&lat=" + coordinates.Latitude.ToString());
            //sb.Append("&lon=" + coordinates.Longitude.ToString());
            sb.Append("&appid=3c0e99c015621b7e9a8a1bc42240704a");

            url = sb.ToString();

            WeatherData currentWeather = new WeatherData();
 
            currentWeather = HttpGetCurrentWeatherByLocation(url);

            return currentWeather;
        }

        static WeatherData HttpGetCurrentWeatherByLocation(string url)
        {
            string result = null;

            using (WebClient syncClient = new WebClient())
            {
                result = syncClient.DownloadString(url);
            }

            //Console.WriteLine(result);

            WeatherData currentWeather = JsonConvert.DeserializeObject<WeatherData>(result);

            return currentWeather;
        }

        static void DisplayCurrentWeather(LocationZipcode zipcode)
        {
            DisplayHeader("Current Weather");

            WeatherData currentWeatherData = GetCurrentWeatherData(zipcode);
            
            Console.WriteLine(String.Format("Temperature (Fahrenheit): {0:0.0}", ConvertToFahrenheit(currentWeatherData.Main.Temp)));

            Console.WriteLine(String.Format("Humidity: {0:0.0}", (currentWeatherData.Main.Humidity) + "%"));

            DisplayContinuePrompt();
        }

        static double ConvertToFahrenheit(double degreesKalvin)
        {
            return (degreesKalvin - 273.15) * 1.8 + 32;
        }
    }
}
