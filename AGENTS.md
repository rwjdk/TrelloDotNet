#AI Rules for TrelloDotNet

## Rules for Public Methods
- Should always have XML Summaries
- async Methods should always have Suffix 'Async', even if I have specified a name without it in your prompt.

## Rules for XML Summaries
- The cancellationToken parameter should always be documents as '<param name="cancellationToken">Cancellation Token</param>' with no further explanation
- Never call an 'ID' for 'Unique Identifier' 
- Always make 'ID' capital letters in comments
- Concepts like 'Board', 'List', 'Card' and 'Member' are very known entities and do not need further descriptions
- Only add XML Summaries on public things. Not internal/private/protected

## Rules for changelog
- Each release should have a `<hr>` between them
- Each release should have a version number and a date (expect if it is marked as #Unreleased)
- Each entry should be in a section called ####Special, ####General, ####TrelloClient, ####Webhook Receiver or #### Automation Engine
- 