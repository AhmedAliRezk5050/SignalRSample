using Microsoft.AspNetCore.SignalR;

namespace SignalRSample.Hubs;

public class SubscriptionHub : Hub
{
    public List<string> GroupsJoined { get; set; } = new ();

    public async Task JoinGroup(string groupName)
    {
        var groupIdentifier = GetGroupIdentifier(groupName);
        
        if (!GroupsJoined.Contains(groupIdentifier))
        {
            GroupsJoined.Add(groupIdentifier);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }
    }
    
    public async Task LeaveGroup(string groupName)
    {
        var groupIdentifier = GetGroupIdentifier(groupName);
        
        if (GroupsJoined.Contains(groupIdentifier))
        {
            GroupsJoined.Remove(groupIdentifier);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
    }

    private string GetGroupIdentifier(string groupName)
    {
        return Context.ConnectionId + ":" + groupName;
    }
}