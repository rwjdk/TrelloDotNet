﻿using System.Security;
using TrelloDotNet.Model;

namespace TrelloDotNet.Tests.IntegrationTests;

[Collection("Organization Management")] //In own collection to not overlap other tests
public class OrganizationTests : TestBase
{
    [Fact]
    public async Task OrganizationCrud()
    {
        string? id = null;
        try
        {
            var displayName = "UnitTestOrganization-"+Guid.NewGuid();
            var input = new Organization(displayName);
            const string description = "Some Description";
            input.Description = description;
            input.Website = "https://www.rwj.dk";

            Organization added = await TrelloClient.AddOrganizationAsync(input);
            id = added.Id;
            Assert.Equal(displayName, added.DisplayName);
            Assert.Equal(description, added.Description);
            Assert.NotNull(added.Id);
            Assert.NotEmpty(added.Url);

            Organization get = await TrelloClient.GetOrganizationAsync(id);
            Assert.Equal(added.Id, get.Id);
            Assert.Equal(added.Name, get.Name);

            get.Description = "Some other description";
            Organization updated = await TrelloClient.UpdateOrganizationAsync(added);
            Assert.Equal(added.Description, updated.Description);

            var members = await TrelloClient.GetMembersOfOrganizationAsync(updated.Id);
            Assert.Single(members);
        }
        finally
        {
            if (id != null)
            {
                await Assert.ThrowsAsync<SecurityException>(async () =>
                {
                    await TrelloClient.DeleteOrganizationAsync(id);
                });

                TrelloClient.Options.AllowDeleteOfOrganizations = true;
                await TrelloClient.DeleteOrganizationAsync(id);
                TrelloClient.Options.AllowDeleteOfOrganizations = false;
            }
        }
    }

}