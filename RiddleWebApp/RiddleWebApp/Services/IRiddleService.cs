using RiddleWebApp.Dtos;

namespace RiddleWebApp.Services
{
    public interface IRiddleService
    {
        List<RiddleDto> GetAllRiddles();
        void AddRiddle(RiddleDto riddleDto);
    }
}
