﻿using System.Collections.Generic;
using System.Text.Json;

namespace MAUITestAPP
{
    public class PlaceRequestHandler
    {
        public class PlaceDataType
        {
            public string? place_id { get; set; }
            public string? place_name { get; set; }
            public string? image_src { get; set; }
            public string? location { get; set; }
            public float? rating { get; set; }
        }
        public class PlaceDetailsType
        {
            public string? place_name { get; set; }
            public string? image_src { get; set; }
            public float? rating { get; set; }
            public string? description { get; set; }
            public string? location { get; set; }
            public float? lat { get; set; }
            public float? lon { get; set; }
        }
        public class PlaceImageType
        {
            public string? thumbnail { get; set; }
            public string? content { get; set; }
        }

        private static readonly string remote_url = "https://musical-goldfish-75444r96g7qhrrwg-3000.app.github.dev";
        
        private PlaceRequestHandler() { }
        private static List<ReturnT>? makeRequest<ReturnT>(string? path)
        {
            var client = new HttpClient();
            var response = client.GetAsync(remote_url + "/" + path).Result;
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<List<ReturnT>>(response.Content.ReadAsStringAsync().Result);
            }
            return null;
        }
        public static List<PlaceDataType>? GetPlaceList()
        {
            return makeRequest<PlaceDataType>("get_place");
        }
        public static PlaceDetailsType? GetPlaceDetails(string id)
        {
            var data = makeRequest<PlaceDetailsType>("get_place_details/" + id);
            return data != null ? data[0] : null;
        }
        public static List<PlaceDataType>? SearchPlace(string name)
        {
            return makeRequest<PlaceDataType>("search_place/" + name);
        }
        public static List<PlaceImageType>? GetPlaceImages(string name)
        {
            return makeRequest<PlaceImageType>("get_place_images/" + name);
        }
    }

}
