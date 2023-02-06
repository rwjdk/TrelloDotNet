using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TrelloDotNet.Model;

namespace TrelloDotNet.Tests
{
    public class TestSandbox
    {
        [RunnableInDebugOnly]
        public async Task AddBoardWithOptions()
        {
            await Task.CompletedTask;
            var client = TestHelper.GetClient();
            var addBoardOptions = new AddBoardOptions
            {
                DefaultLabels = false,
                DefaultLists = false,
                WorkspaceId = "other60483948"
            };
            //await client.AddBoardAsync(new Board("Test"), addBoardOptions);
        }
        
        [RunnableInDebugOnly]
        public async Task CustomDelete()
        {
            await Task.CompletedTask;
            var client = TestHelper.GetClient();
            await client.DeleteAsync("cards/63de84fd6ec529e05977a5ab");
        }
        
        [RunnableInDebugOnly]
        public async Task CloseCard()
        {
            await Task.CompletedTask;
            var client = TestHelper.GetClient();
            await client.ArchiveCardAsync("63de89f086c564af5be296ba");
        }
        
        [RunnableInDebugOnly]
        public async Task ReOpenCard()
        {
            await Task.CompletedTask;
            var client = TestHelper.GetClient();
            await client.ReOpenCardAsync("63de89f086c564af5be296ba");
        }
        
        [RunnableInDebugOnly]
        public async Task CloseBoard()
        {
            await Task.CompletedTask;
            var client = TestHelper.GetClient();
            await client.CloseBoardAsync("SCPjg8ON");
        }
        
        [RunnableInDebugOnly]
        public async Task ReOpenBoard()
        {
            await Task.CompletedTask;
            var client = TestHelper.GetClient();
            await client.ReOpenBoardAsync("SCPjg8ON");
        }
        
        [RunnableInDebugOnly]
        public async Task CloseList()
        {
            await Task.CompletedTask;
            var client = TestHelper.GetClient();
            await client.ArchiveListAsync("63de3eb58d0d357abc562b02");
        }
        
        [RunnableInDebugOnly]
        public async Task ReOpenList()
        {
            await Task.CompletedTask;
            var client = TestHelper.GetClient();
            await client.ReOpenListAsync("63de3eb58d0d357abc562b02");
        }
        
        [RunnableInDebugOnly]
        public async Task ArchiveAllCardsOnList()
        {
            await Task.CompletedTask;
            var client = TestHelper.GetClient();
            await client.ArchiveAllCardsInList("63de3eb58d0d357abc562b02");
        }

        [RunnableInDebugOnly]
        public async Task MoveList()
        {
            await Task.CompletedTask;
            var client = TestHelper.GetClient();
            var boardAsync = await client.GetBoardAsync("uPB5YWto");
            await client.MoveListToBoardAsync("63dbef756685986201365c13", boardAsync.Id);
        }

        [RunnableInDebugOnly]
        public async Task MoveAllCardsOnList()
        {
            await Task.CompletedTask;
            var client = TestHelper.GetClient();
            var boardAsync = await client.GetBoardAsync("SCPjg8ON");
            await client.MoveAllCardsInList("63de3eb58d0d357abc562b02", "63dbef756685986201365c13");
        }
    }
}
