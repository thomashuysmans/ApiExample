using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;

namespace ApiExample.Infrastructure;

public class ProblemDetailsResponseContentTypeProcessor : IOperationProcessor
{
    public bool Process(OperationProcessorContext context)
    {
        foreach (var response in context.OperationDescription.Operation.Responses)
        {
            // Look for responses documented as "application/json"
            if (response.Value.Content.ContainsKey("application/json"))
            {
                // Check if the response schema reference indicates a ProblemDetails type.
                var schema = response.Value.Content["application/json"].Schema;
                if (schema?.Reference != null && schema.Reference.ToString().Contains("ProblemDetails"))
                {
                    // Remove "application/json" and add "application/problem+json"
                    var content = response.Value.Content["application/json"];
                    response.Value.Content.Remove("application/json");
                    response.Value.Content.Add("application/problem+json", content);
                }
            }
        }
        return true;
    }
}