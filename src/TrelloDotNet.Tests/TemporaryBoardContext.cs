using System;
using System.Threading;
using System.Threading.Tasks;
using TrelloDotNet.Model;

namespace TrelloDotNet.Tests;

public sealed class TemporaryBoardContext : IAsyncDisposable
{
    private readonly TrelloClient _trelloClient;
    private readonly Organization _organization;
    private bool _disposed;

    internal TemporaryBoardContext(TrelloClient trelloClient, Board board, Organization organization)
    {
        _trelloClient = trelloClient;
        Board = board;
        _organization = organization;
    }

    public Board Board { get; }

    public Organization Organization => _organization;

    public async ValueTask DisposeAsync()
    {
        if (_disposed)
        {
            return;
        }

        var cancellationToken = TestContext.Current?.CancellationToken ?? CancellationToken.None;

        try
        {
            _trelloClient.Options.AllowDeleteOfBoards = true;
            await _trelloClient.DeleteBoardAsync(Board.Id, cancellationToken: cancellationToken);
        }
        finally
        {
            _trelloClient.Options.AllowDeleteOfBoards = false;
        }

        try
        {
            _trelloClient.Options.AllowDeleteOfOrganizations = true;
            await _trelloClient.DeleteOrganizationAsync(_organization.Id, cancellationToken: cancellationToken);
        }
        finally
        {
            _trelloClient.Options.AllowDeleteOfOrganizations = false;
        }

        _disposed = true;
    }
}
