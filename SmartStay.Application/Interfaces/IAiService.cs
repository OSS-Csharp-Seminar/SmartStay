using SmartStay.Application.Dto.AiDto;

namespace SmartStay.Application.Interfaces;

public interface IAiService
{
    Task<string> ChatAsync(List<OllamaMessageDto> history);
}
