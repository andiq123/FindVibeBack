using System.Text.RegularExpressions;
using API.Models;
using API.Songs;
using Microsoft.AspNetCore.SignalR;

namespace API.Hubs;

public class PlayerHub : Hub
{
    private static readonly Dictionary<string, IList<Session>> _sessions = new();
    private static readonly Dictionary<string, Song> _lastSong = new();

    public async Task Connect(string groupName)
    {
        var callerId = Context.ConnectionId;
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

        if (_lastSong.TryGetValue(groupName, out var song))
        {
            await Clients.Caller.SendAsync("SetSong", song);
        }

        if (_sessions.TryGetValue(groupName, out var sessions))
        {
            sessions.Add(new Session(){
                ConnectionId = callerId,
                IsSelected = false
            });
        }
        else
        {
            _sessions.Add(groupName, new List<Session>());
        }
        
        await Clients.Group(groupName).SendAsync("OtherSessionConnected", _sessions[groupName]);
        if (_lastSong.TryGetValue(groupName, out var lastSong))
        {
            await Clients.Caller.SendAsync("SetSong", lastSong);
        }
    }

    public async Task Disconnect(string groupName)
    {
        var callerId = Context.ConnectionId;
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

        _lastSong.Remove(groupName);

        if (_sessions.TryGetValue(groupName, out var sessions))
        {
            var session = sessions.First(x => x.ConnectionId == callerId);
            sessions.Remove(session);
        }
        
        await Clients.GroupExcept(groupName, callerId).SendAsync("OtherSessionDisconnected", _sessions[groupName]);
    }

    // public async Task SelectSession(string groupName)
    // {
    //     _sessionLists[groupName].Sessions.First(x => x.ConnectionId == Context.ConnectionId).IsSelected = true;
    //     await Clients.GroupExcept(groupName, Context.ConnectionId)
    //         .SendAsync("SelectSession", _sessionLists[groupName].Sessions);
    // }

    public async Task UpdateTime(string time, Int64 startTimeInMs, string groupName)
    {
        var callerId = Context.ConnectionId;
        await Clients.GroupExcept(groupName, callerId).SendAsync("UpdateTime", time, startTimeInMs);
    }

    public async Task SetSong(Song song, string groupName)
    {
        var callerId = Context.ConnectionId;
        _lastSong.Add(groupName, song);
        await Clients.GroupExcept(groupName, callerId).SendAsync("SetSong", song);
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