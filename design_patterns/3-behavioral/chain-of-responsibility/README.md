# Chain of Responsibility

Pass a request along a chain of handlers. Each handler decides to process the request or forward it to the next handler in the chain.

## When to use it

- More than one object may handle a request, and the handler isn't known a priori.
- You want to issue a request without specifying the receiver explicitly.
- The set of handlers should be specifiable dynamically — handlers can be added or reordered at runtime.

## How it works

1. Define a **Handler** abstract class (or interface) with a `SetNext` method and a `Handle` method.
2. Each **concrete handler** either processes the request or forwards it to `_next`.
3. The **client** builds the chain by linking handlers, then sends the request to the first one.

```csharp
bot.SetNext(junior).SetNext(senior); // chain: bot → junior → senior

bot.Handle("faq");     // [Bot] Handled: FAQ question
bot.Handle("billing"); // [Bot] Can't handle → [Junior] Handled: Billing issue
bot.Handle("outage");  // [Bot] Can't handle → [Junior] Can't handle → [Senior] Handled: outage
```

## Examples

| File | What it shows |
|---|---|
| `support/support.cs` | `BotHandler`, `JuniorAgentHandler`, `SeniorAgentHandler` form a support escalation chain; each handler processes its issue type or passes up |
| `approval/approval.cs` | `TeamLeadHandler`, `ManagerHandler`, `DirectorHandler`, `CFOHandler` approve expenses by amount threshold; `ExpenseApprovalChain.BuildChain` wires the chain |
