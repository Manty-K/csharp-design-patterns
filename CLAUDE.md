# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What This Repository Is

An educational reference repository of C# code examples demonstrating **SOLID design principles** and **Gang of Four design patterns**. Each topic is structured with implementation files and an accompanying `README.md`.

> Note: structural pattern examples use single focused `.cs` files (not a bad/good pair) since the pattern itself is the demonstration.

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
  2-structural/                 ← Adapter, Bridge, Composite, Decorator, Facade, Flyweight, Proxy
  3-behavioral/                 ← Observer
    adapter/
      README.md
      sms/sms.cs                ← SmsAdapter wraps LegacySmsService behind INotification
      weather/weather.cs        ← WeatherAdaptor wraps WeatherAPI behind IWeatherService
    bridge/
      README.md
      tv/tv.cs                  ← BasicRemote/SmartRemote (abstraction) × SamsungTV/SonyTV (implementation) mix freely
      channel/channel.cs        ← AlertNotification/PromoNotification (abstraction) × EmailChannel/SmsChannel (implementation)
    composite/
      README.md
      filesystem/filesystem.cs  ← Folder (composite) and File (leaf) implement IFileSystemItem; Print recurses with indentation
      company/company.cs        ← Manager (composite) and IndividualEmployee (leaf) implement IEmployee; org chart prints recursively
    decorator/
      README.md
      formator/formator.cs      ← BoldDecorator, UpperCaseDecorator, ExclamationDecorator stack on IText
      coffee/coffee.cs          ← MilkDecorator, ChocoDecorator wrap ICoffee; cost and description accumulate
    facade/
      README.md
      order/order.cs            ← OrderFacade.PlaceOrder coordinates Payment, Inventory, Shipping, Notification
      home-theater/home-theater.cs ← HomeTheaterFacade.WatchMovie orchestrates Projector, SoundSystem, StreamingPlayer
    flyweight/
      README.md
      tree/tree.cs              ← TreeType (flyweight) shared by many Tree (context) instances; TreeTypeFactory caches by name+texture
      character/character.cs    ← CharacterType (flyweight) shared by many Character (context) instances; factory caches by symbol+font+size
    proxy/
      README.md
      database/database.cs      ← DatabaseProxy enforces role-based access before delegating to Database
      file-reader/file-reader.cs ← FileReaderCacheProxy serves cached content or falls back to MyFileReader
    observer/
      README.md
      order/order.cs            ← OrderService notifies EmailNotifier/SmsNotifier on PlaceOrder; observers subscribe/unsubscribe at runtime
      stock-market/stock-market.cs ← StockMarket tracks per-stock observer lists; MobileAlert/EmailAlert notified only on actual price changes
```

## Conventions

- SOLID examples: exactly one `bad.cs` (the problem) and one `good.cs` (the solution) per sub-folder.
- Design pattern examples: one or more focused `.cs` files per sub-folder; no bad/good split needed.
- No build system, test framework, or external dependencies — all files are standalone C# examples meant to be read, not compiled.
- Follow existing naming: PascalCase for types/methods, camelCase for fields/locals.

## Adding New Content

**New SOLID example:** create a folder under the relevant principle directory with `bad.cs` and `good.cs`. Update that principle's `README.md` if needed.

**New design pattern:** create a folder under `design_patterns/1-creational/` (or the appropriate category) with the pattern implementation files and a `README.md` following the same structure as existing patterns.

**New principle or pattern category:** add a numbered directory (e.g., `3-behavioral/`) with a `README.md` listing patterns, then add implementations as sub-folders.
