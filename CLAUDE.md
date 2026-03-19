# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What This Repository Is

An educational reference repository of C# code examples demonstrating **SOLID design principles** and **Gang of Four design patterns**. Each topic is structured as a pair of anti-pattern (`bad.cs`) and refactored solution (`good.cs`) with an accompanying `README.md`.

## Repository Structure

```
solid/                          ← 5 SOLID principles
  <n>-<principle-name>/
    README.md                   ← Principle explanation
    <example>/
      bad.cs                    ← Anti-pattern
      good.cs                   ← Correct approach

design_patterns/
  1-creational/                 ← Singleton, Builder, Factory, Abstract Factory, Prototype
  2-structural/                 ← Planned; not yet implemented
```

## Conventions

- Every topic has exactly one `bad.cs` (the problem) and one `good.cs` (the solution).
- `bad.cs` files demonstrate a concrete violation of the principle/pattern; `good.cs` refactors it.
- No build system, test framework, or external dependencies — all files are standalone C# examples meant to be read, not compiled.
- Follow existing naming: PascalCase for types/methods, camelCase for fields/locals.

## Adding New Content

**New SOLID example:** create a folder under the relevant principle directory with `bad.cs` and `good.cs`. Update that principle's `README.md` if needed.

**New design pattern:** create a folder under `design_patterns/1-creational/` (or the appropriate category) with the pattern implementation files and a `README.md` following the same structure as existing patterns.

**New principle or pattern category:** add a numbered directory (e.g., `3-behavioral/`) with a `README.md` listing patterns, then add implementations as sub-folders.
