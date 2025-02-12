using Amazon.BedrockRuntime;
using Amazon.BedrockRuntime.Model;
using Client._Configuration;

namespace Client;

public interface IConversationClient
{
    Task<string> Chat(string message, string? system = null);
}

public class ConversationClient(IAmazonBedrockRuntime ai, ConversationOptions options) : IConversationClient
{
    public async Task<string> Chat(string message, string? system = null)
    {
        var request = new ConverseRequest
        {
            ModelId = options.AiModelId,
            System = [
                new()
                {
                    Text = system
                }
            ],
            Messages =
            [
                new() 
                {
                    Role = ConversationRole.User,
                    Content =
                    [
                        new() { Text = message }
                    ]
                }
            ],
            InferenceConfig = new InferenceConfiguration
            {
                MaxTokens = options.MaxTokens,
                Temperature = options.Temperature,
                TopP = options.TopP
            }
        };

        var response = await ai.ConverseAsync(request);
        var responseMessage = response.Output.Message.Content[0].Text
            ?? throw new Exception("AI response was not expected.");

        return responseMessage;
    }
}