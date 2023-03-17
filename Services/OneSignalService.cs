namespace WebApi.Services;

using AutoMapper;
using RestSharp;
using WebApi.Entities;
using WebApi.Factories;
using WebApi.Models.Maps;
using WebApi.Models.OneSignal;
using WebApi.Models.Views;
using WebApi.Repositories;

public interface IOneSignalService
{
    Task Push(CreatePostMessage message, string oneSignalApiKey);
}

public class OneSignalService : IOneSignalService
{
    private readonly IMapper _mapper;
    public OneSignalService(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task Push(CreatePostMessage message, string oneSignalApiKey)
    {
        if(oneSignalApiKey != null && oneSignalApiKey != ""){
            var client = new RestClient("https://onesignal.com/api/v1/notifications");
            var request = new RestRequest("/api/v1/notifications");
            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", "Basic " + oneSignalApiKey);
            request.AddHeader("content-type", "application/json");
            var body = "{\"included_segments\":[\"Subscribed Users\"],\"contents\":{\"en\":\""+ message.Content +"\"},\"name\":\""+ message.Name +"\"}";
        
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            var response = await client.PostAsync(request);
        }
    }

}