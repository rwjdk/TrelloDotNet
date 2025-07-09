using Backend.Models;

namespace Backend.Constants;

internal static class VectorStoreIds
{
    internal const string VectorStoreName = "trello_dot_net_vector_sources";
    internal const int MaxRecords = 50;
    public const string SourceIdCode = "Code";
    public const string SourceIdMarkdownInCode = "MarkdownInCode";
    public const string CollectionId = "TrelloDotNet";
    public static VectorStoreToUse VectorStoreToUse = VectorStoreToUse.AzureAiSearch;
}