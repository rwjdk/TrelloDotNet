[Back to Card Features](TrelloClient#card-features)

Mirror a Card _(New Trello Feature introduced January 2025)_

## Signature
```cs

/// <summary>
/// Mirror a Card
/// </summary>
/// <param name="options">Parameters for create the mirror</param>
/// <param name="cancellationToken">Cancellation Token</param>
/// <returns>The Mirror Card</returns>
public async Task<Card> MirrorCardAsync(MirrorCardOptions options, CancellationToken cancellationToken = default)

```

### Examples

```cs

var sourceCardId = "67632719612da6c1f0102e79";
var targetListId = "644d07374e0066a7b90eeddb";
var position = NamedPosition.Top;

var mirrorOptions = new MirrorCardOptions
{
    SourceCardId = sourceCardId,
    TargetListId = targetListId,
    NamedPosition = NamedPosition.Bottom
};

Card mirrorCardReference = await client.MirrorCardAsync(mirrorOptions);

```