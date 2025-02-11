namespace Client._Configuration;

public class ConversationOptions
{
    public string AiModelId { get; set; }
    public int MaxTokens { get; set; } = 512;
    public float Temperature { get; set; } = 0F;
    public float TopP { get; set; } = 0.9F;
}