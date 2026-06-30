using System.Globalization;
using System.Net.Http.Json;
using SmartStay.Application.Dto;
using SmartStay.Application.Interfaces;
using SmartStay.Domain.Interfaces.Repository;

namespace SmartStay.Application.Services;

public class GeocodingService : IGeocodingService
{
   private readonly HttpClient _httpClient;
   private readonly ILocationRepository _repo;

   public GeocodingService(HttpClient client, ILocationRepository repo)
   {
       _httpClient = client;
       _repo = repo;
       
       //M.G: Nominatim requires user-agent header!
       if (!_httpClient.DefaultRequestHeaders.Contains("User-Agent"))
       {
           _httpClient.DefaultRequestHeaders.Add("User-Agent", "SmartStay");
       }
   }
    
    public async Task<(double Latitude, double Longitude)?> GetCoordinatesAsync(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            return null; 
        
        var cached = await CheckExistingLocationInDb(address);
        if (cached != null)
            return cached;

           var url = $"https://nominatim.openstreetmap.org/search?q={Uri.EscapeDataString(address)}&format=json&limit=1";

           var response = await _httpClient.GetFromJsonAsync<List<NominatimResult>>(url);

           if (response == null || response.Count == 0)
               return null;

           var result = response[0];

           return (
               double.Parse(result.Latitude, CultureInfo.InvariantCulture),
               double.Parse(result.Longitude, CultureInfo.InvariantCulture)
           );
    }


    private async Task<(double Latitude, double Longitude)?> CheckExistingLocationInDb(string address)
    {
           var loc= await _repo.GetLocationByAddressAsync(address);
           if (loc == null)
               return null;
           else
           {
               return (loc.Latitude, loc.Longitude);
           }
    }
}