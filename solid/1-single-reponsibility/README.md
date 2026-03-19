# S — Single Responsibility Principle

> "Every software module should have only one reason to change."

## What it means

A class should do one thing and own it completely. If a class handles business logic, persistence, *and* email sending, then a change to any one of those areas forces you to touch — and re-test — the entire class.

## Problem (`user/bad.cs`)

`UserService` does too much: it validates the email format, saves to the database, **and** sends a welcome email. Three responsibilities, three reasons to change.

```
UserService
  ├── ValidateEmail()   ← email concerns
  ├── Save()            ← persistence concerns
  └── SendEmail()       ← notification concerns
```

## Solution (`user/good.cs`)

Split the responsibilities into focused classes. `UserService` only orchestrates; the details live in dedicated collaborators injected via the constructor.

```
UserService         ← orchestration only
EmailService        ← email validation + sending
DbContext           ← persistence
```

Each class now has exactly one reason to change.
