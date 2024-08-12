using API.Songs;
using Microsoft.AspNetCore.SignalR;

namespace API.Hubs;

public class PlayerHub : Hub
{
    public async Task Connect(string groupName)
    {
        var callerId = Context.ConnectionId;
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        await Clients.GroupExcept(groupName, callerId).SendAsync("OtherSessionConnected", callerId);
    }

    public async Task Disconnect(string groupName)
    {
        var callerId = Context.ConnectionId;
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        await Clients.GroupExcept(groupName, callerId).SendAsync("OtherSessionDisconnected", callerId);
    }
    
    public async Task UpdateTime(string time, string groupName)
    {
        var callerId = Context.ConnectionId;
        await Clients.GroupExcept(groupName, callerId).SendAsync("UpdateTime", time);
    }

    public async Task SetSong(SongDto songDto, string groupName)
    {
        var callerId = Context.ConnectionId;
        await Clients.GroupExcept(groupName, callerId).SendAsync("SetSong", songDto);
    }

    public async Task Play(string groupName)
    {
        var callerId = Context.ConnectionId;
        await Clients.GroupExcept(groupName, callerId).SendAsync("Play");
    }

    public async Task Pause(string groupName)
    {
        var callerId = Context.ConnectionId;
        await Clients.GroupExcept(groupName, callerId).SendAsync("Pause");
    }

    public async Task Next(string groupName)
    {
        var callerId = Context.ConnectionId;
        await Clients.GroupExcept(groupName, callerId).SendAsync("Next");
    }

    public async Task Previous(string groupName)
    {
        var callerId = Context.ConnectionId;
        await Clients.GroupExcept(groupName, callerId).SendAsync("Previous");
    }
}