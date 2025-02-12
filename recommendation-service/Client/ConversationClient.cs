using Amazon.BedrockRuntime;
using Amazon.BedrockRuntime.Model;
using Client._Configuration;
using System.Text.Json;

namespace Client;

public interface IConversationClient
{
    Task<string> Chat(string message, string? system = null);
    Task<string> GetWhy(string recommendation);
}

public class ConversationClient(IAmazonBedrockRuntime ai, ConversationOptions options) : IConversationClient
{
    public async Task<string> Chat(string message, string? system = null)
    {
        var request = new ConverseRequest
        {
            ModelId = options.AiModelId,
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

        if (system != null)
        {
            request.System = [ new() { Text = system } ];
        }

        var response = await ai.ConverseAsync(request);
        var responseMessage = response.Output.Message.Content[0].Text
            ?? throw new Exception("AI response was not expected.");

        return responseMessage;
    }

    public async Task<string> GetWhy(string recommendation)
    {
        var question = new ReasonModel
        {
            Input = recommendation,
            Ask = "In 2 or 3 sentences, and less than 75 words, why?"
        };
        var questionJson = JsonSerializer.Serialize(question);
        var request = new ConverseRequest
        {
            ModelId = options.AiModelId,
            Messages =
            [
                new()
                {
                    Role = ConversationRole.User,
                    Content =
                    [
                        new() { Text = questionJson }
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

        var reasonResponse = await ai.ConverseAsync(request);
        var reasonResponseMessage = reasonResponse.Output.Message.Content[0].Text
            ?? throw new Exception("AI response was not expected.");

        return reasonResponseMessage;
    }
}
