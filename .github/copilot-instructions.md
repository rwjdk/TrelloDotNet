#AI Rules for TrelloDotNet

## Rules for Public Methods
- Should always have XML Summaries
- async Methods should always have Suffix 'Async', even if I have specified a name without it in your prompt.

## Rules for XML Summaries
- The cancellationToken parameter should always be documents as '<param name="cancellationToken">Cancellation Token</param>' with no further explanation
- Never call an 'ID' for 'Unique Identifier' 
- Always make 'ID' capital letters in comments