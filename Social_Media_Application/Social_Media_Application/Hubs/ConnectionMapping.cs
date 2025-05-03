using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Social_Media_Application.Hubs
{
    public class ConnectionMapping
    {
        private readonly Dictionary<string, HashSet<string>> _connections = new();
        public void Add(string userId, string connectionId)
        {
            if (!_connections.TryGetValue(userId, out var connections))
            {
                connections = new HashSet<string>();
                _connections[userId] = connections;
            }

            connections.Add(connectionId);
        }
        public void Remove(string userId, string connectionId)
        {
            if (!_connections.TryGetValue(userId, out var connections)) return;

            connections.Remove(connectionId);
            if (connections.Count == 0)
            {
                _connections.Remove(userId);
            }    
        }
        public List<string> GetConnections(string userId)
        {
            return _connections.TryGetValue(userId, out var connections)
                ? connections.ToList()
                : new List<string>();
        }
        public bool IsConnected(string userId)
        {
            return _connections.ContainsKey(userId) && _connections[userId].Any();
        }
        public List<string> GetAllConnections()
        {
            return _connections.Keys.ToList();          
        }
    }
}
