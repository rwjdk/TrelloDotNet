# Inbox access investigation

Observed on 2026-10-13. These results describe the tested accounts and clients, not a confirmed global Trello policy.

## Live REST checks

Five configured API-key/token pairs produced the same results:

- `GET https://api.trello.com/1/members/me?fields=inbox`: HTTP 200; an Inbox object contains `idBoard`, `idList`, and `idOrganization`.
- Reads of that Inbox's Board, Labels, and List Cards: HTTP 401.
- Control reads of ordinary Boards and Labels: HTTP 200.

The user then ran the OAuth tester with `read:board:trello` and `read:member:trello`. Their supplied console screenshot showed successful ordinary Board access (59 Boards) and Inbox discovery, followed by HTTP 403 for `GetTokenMemberInboxAsync` and both `GetCardsInInboxAsync` overloads. This establishes successful OAuth authentication without restored Inbox method access. Creation was not tested.

`GetTokenMemberInboxAsync` loads Labels after discovering IDs, so its failure alone does not isolate the Cards endpoint. The updated probe has a direct List Cards read to separate that dependency; a result from that newer probe has not yet been observed.

## OAuth client settings inspected in Chrome

The `oAuth2-sample-app` has the same client ID used by the tester and registers `https://localhost:7248/oauth/callback`. It is a public client. Board and Member read scopes are enabled. All ten scope choices match the public REST documentation; there are no Inbox or Planner scope controls. No settings were changed.

The experimental `--request-inbox-scope` flag requests `read:member.inbox:trello` in the tester without adding it to the library enum. Availability to an ordinary Trello OAuth app remains unconfirmed. Do not treat URL generation or reaching login as evidence that a scope is grantable.

## Official MCP findings

- Protected-resource metadata at `https://mcp.trello.com/.well-known/oauth-protected-resource/v1` advertises `read:member.inbox:trello` and `write:member.inbox:trello`, alongside Board/Member and Planner scopes.
- Its authorization server has a separate registration path, exposed through OAuth authorization-server metadata. Dynamic registration of a diagnostic client succeeded.
- A read-only authorization request reached an MCP-specific consent screen. Selecting either the personal Workspace or Relewise Development showed “Permissions are blocked for these workspaces”; Accept remained unavailable. No MCP access token or Inbox tool response was obtained.
- The public repository contains documentation, connection configuration, and an agent skill, but no server backend implementation. Its guidance directs clients to `trelloReadInbox` / `trelloWriteInbox` rather than Board tools. This does not reveal which private backend endpoints those tools call.

## Website network observation

In the user's signed-in real Chrome session, opening Inbox from the sandbox Board produced:

- `POST https://trello.com/gateway/api/graphql`, operation `TrelloInboxFetch`: HTTP 200, no GraphQL errors. The query traverses `trello.member(id: $memberId).inbox`, opting into `TrelloMember` and `TrelloInbox`, and requests Inbox, List, and Workspace IDs. The returned type is `TrelloInbox`.
- `GET https://trello.com/1/board/{inboxBoardId}` with Board metadata/Labels parameters and with Cards/Lists parameters: HTTP 200. The Cards response contained two Cards and one List.

Browser-session access is established. This is not evidence that API-key tokens or third-party OAuth bearer tokens can call these website routes. No browser credentials were extracted or replayed, and no Cards were created or modified.

## Implications and remaining checks

The experimental 3.x migration, including obsolete Inbox API removal, was subsequently reverted at the user's request. The existing Inbox REST APIs, models/options, and tests remain on `main`; the temporary diagnostic probe was removed. The probe instructions above record historical investigation steps rather than currently available commands. Future Inbox support requires a supported authentication/API surface; do not claim that all Inbox access has ceased.

Remaining checks: whether Atlassian supports granting Inbox scopes to ordinary REST clients; whether an admin-permitted MCP connection can read this account's Inbox; and whether its access has any supported equivalent for TrelloDotNet. Do not infer causation from the MCP launch date alone.

## Sources

- [REST OAuth client scope configuration](https://developer.atlassian.com/cloud/trello/guides/rest-api/oauth-2-client-configuration/)
- [MCP protected-resource metadata](https://mcp.trello.com/.well-known/oauth-protected-resource/v1)
- [Official MCP repository](https://github.com/atlassian/trello-mcp-server)
- [Official tool guidance](https://github.com/atlassian/trello-mcp-server/blob/main/skills/trello-use/SKILL.md)
- [MCP troubleshooting and organization restrictions](https://support.atlassian.com/trello/docs/troubleshoot-trello-mcp-connection-errors/)
