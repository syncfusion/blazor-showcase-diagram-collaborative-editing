using SignalRServer.Models;

namespace SignalRServer.Services
{
    public interface IDiagramService
    {
        Task<DiagramData?> GetDiagramAsync(string diagramId, string roomName);
        Task<bool> SaveDiagramDataAsync(string diagramId, string roomName, string diagramData, string userId);
    }
}