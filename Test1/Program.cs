using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels.RequestModels;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3;
using System.Text;
using System.Text.RegularExpressions;

var apiKey = "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";
var chatBot = new OpenAIChat(apiKey);

Console.OutputEncoding = Encoding.Unicode;
Console.InputEncoding = Encoding.Unicode;


while (true)
{
    Console.Write("- Hùng:  ");
    string message = Console.ReadLine();
    Console.WriteLine();

    Console.Write("- ChatGPT:  ");
    string response = await chatBot.GetChatResponse(message); 
    Console.WriteLine(Regex.Replace(response, "^\n\n", ""));
    Console.WriteLine();
    Console.WriteLine();
}

public class OpenAIChat
{
    private readonly OpenAIService gpt3;

    public OpenAIChat(string apiKey)
    {
        gpt3 = new OpenAIService(new OpenAiOptions { ApiKey = apiKey });
    }

    public async Task<string> GetChatResponse(string message)
    {
        // Create a chat completion request
        var completionResult = await gpt3.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
        {
            Messages = new List<ChatMessage>(new ChatMessage[] { new ChatMessage("user", message) }),
            Model = Models.ChatGpt3_5Turbo,
            Temperature = 0.5F,
            MaxTokens = 4000,
            N = 1
        });

        // Check if the completion result was successful and handle the response
        if (completionResult.Successful)
        {
            return completionResult.Choices[0].Message.Content;
        }
        else
        {
            if (completionResult.Error == null)
            {
                throw new Exception("Unknown Error");
            }
            throw new Exception($"{completionResult.Error.Code}: {completionResult.Error.Message}");
        }
    }
}
