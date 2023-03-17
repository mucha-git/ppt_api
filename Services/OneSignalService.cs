namespace WebApi.Services;

using System.Net.Http.Headers;
using System.Text.Json;
using AutoMapper;
using WebApi.Models.OneSignal;
using WebApi.Repositories;

public interface IOneSignalService
{
    Task Push(CreatePostMessage message, int pilgrimageId);
}

public class OneSignalService : IOneSignalService
{
    private readonly IMapper _mapper;
    private readonly IPilgrimagesRepository _pilgrimageRepository;
    public OneSignalService(IMapper mapper, IPilgrimagesRepository pilgrimagesRepository)
    {
        _mapper = mapper;
        _pilgrimageRepository = pilgrimagesRepository;
    }

    public async Task Push(CreatePostMessage message, int pilgrimageId)
    {
        var pilgrimage = await _pilgrimageRepository.GetById(pilgrimageId);
        if(pilgrimage.OneSignalApiKey != null && pilgrimage.OneSignalApiKey != ""){
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://onesignal.com/api/v1/notifications");
            //httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", "Basic " + pilgrimage.OneSignalApiKey);
            //client.DefaultRequestHeaders.Add("content-type", "application/json");
            string[] cars = {"Subscribed Users"};
            var content = new StringContent(JsonSerializer.Serialize(new {
                app_id = pilgrimage.OneSignal,
                contents = new {
                    en = message.Content.Trim()
                },
                name = message.Name,
                included_segments = cars
            }));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync("https://onesignal.com/api/v1/notifications", content);
        }
    }

}