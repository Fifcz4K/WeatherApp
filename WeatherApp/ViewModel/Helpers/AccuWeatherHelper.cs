﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Model;

namespace WeatherApp.ViewModel.Helpers
{
    public class AccuWeatherHelper
    {
        public const string BASE_URL = "http://dataservice.accuweather.com";
        public const string AUTOCOMPLETE_ENPOINT = "locations/v1/cities/autocomplete?apikey={0}&q={1}";
        public const string CURRENTCONDITION_ENDPOINT = "currentconditions/v1/{0}?apikey={1}";
        static string API_KEY;

        public AccuWeatherHelper()
        {
            API_KEY = File.ReadAllText("apikey.txt");
        }

        public static async Task<List<City>> GetCities(string query)
        {
            List<City> cities = new List<City>();

            string url = BASE_URL + string.Format(AUTOCOMPLETE_ENPOINT, API_KEY, query);

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();
                cities = JsonConvert.DeserializeObject<List<City>>(json);
            }

            return cities;
        }

        public static async Task<CurrentCondtions> GetCurrentConditions(string cityKey)
        {
            CurrentCondtions currentConditions = new CurrentCondtions();

            string url = BASE_URL + string.Format(CURRENTCONDITION_ENDPOINT, cityKey, API_KEY);

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();
                currentConditions = JsonConvert.DeserializeObject<List<CurrentCondtions>>(json).FirstOrDefault();
            }

            return currentConditions;
        }
    }
}
