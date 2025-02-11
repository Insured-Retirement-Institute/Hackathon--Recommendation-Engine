using Client;
using recommendation_service.Models;

namespace recommendation_service.Services;

public interface IConversationService
{
    Task<ConversationResponse> Chat(ConversationRequest request);
}

public class ConversationService(IConversationClient conversation) : IConversationService
{
    public async Task<ConversationResponse> Chat(ConversationRequest request)
    {
        if (request.Message == null)
            throw new Exception($"{nameof(request.Message)} is required.");

        var responseMesssage = await conversation.Chat(request.Message);
        var response = new ConversationResponse
        {
            Message = responseMesssage
        };

        return response;
    }
}
