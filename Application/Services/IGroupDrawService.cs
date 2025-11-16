using Application.DTOs;

namespace Application.Services
{ 
    public interface IGroupDrawService
    {
        Task<DrawResultDto> RunDrawAsync(DrawRequestDto request);
    }

}
