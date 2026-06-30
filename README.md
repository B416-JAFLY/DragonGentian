# Dragon Gentian (龙胆) — Slay the Spire 2 Mod

[![Build and Release](https://github.com/B416-JAFLY/DragonGentian/actions/workflows/build.yml/badge.svg)](https://github.com/B416-JAFLY/DragonGentian/actions/workflows/build.yml)

[中文文档](README_CN.md)

---

### Overview

**Dragon Gentian** is a custom card mod for **Slay the Spire 2** that adds a unique Power card for the **Regent** character. The card grants the ability to freely interchange **Starbright** and **Energy** at a 1:1 ratio — any resource can be spent as the other.

This mod was inspired by the Regent's dual-resource system and aims to provide more flexibility in deck-building and combat decision-making.

![Preview](screenshots/preview.png)

---

### The Card

| Property | Base | Upgraded |
|----------|------|----------|
| **Name** | Dragon Gentian (龙胆) | Dragon Gentian+ |
| **Cost** | 2 Energy | 1 Energy |
| **Type** | Power | Power |
| **Rarity** | Uncommon | Uncommon |
| **Pool** | Regent | Regent |
| **Upgrade** | — | Cost -1, gains **Innate** |

**Effect (Power — Dragon Gentian):**
> You can spend Starbright as Energy, and Energy as Starbright.

When played, applies the **Dragon Gentian** buff to your character. While active:
- If you lack Energy but have excess Starbright → Starbright covers the Energy gap
- If you lack Starbright but have excess Energy → Energy covers the Starbright gap
- The conversion is 1:1 and automatic, calculated at the moment you play each card

---

### How It Works (Technical)

The mod uses **Harmony** patches to intercept three key game methods:

1. `CardEnergyCost.GetWithModifiers` — determines if a card can be played (energy check)
2. `CardEnergyCost.GetAmountToSpend` — determines how much energy to actually deduct
3. `CardModel.GetStarCostWithModifiers` — determines the Starbright cost

The `SplitComputer` algorithm checks the player's current energy and star reserves, then recalculates the split cost to maximize playability using the interchangeable resource pool.

---

### Installation

1. **Requirements:**
   - Slay the Spire 2 **v0.107.0** or later
   - [BaseLib](https://github.com/Alchyr/StS2-BaseLib) **v3.3.0** or later

2. **Install:**
   - Download the latest release from [Releases](https://github.com/B416-JAFLY/DragonGentian/releases)
   - Extract to `<STS2>/mods/DragonGentian/`
   - The final structure should look like:
     ```
     mods/DragonGentian/
       DragonGentian.dll
       DragonGentian.json
       DragonGentian.pck
     ```

3. **Launch the game** — the mod loads automatically.

---

### How to Use

1. Start a new run as **Regent** (储君 / Regent)
2. The **Dragon Gentian** card is automatically added to your starting deck (for testing)
3. Play the card to activate the buff
4. Once active, you'll see a "龙胆" icon in your buff bar
5. Enjoy freely mixing Starbright and Energy for the rest of combat!

> **Note:** The auto-add to starting deck is a development convenience. In a production release, the card would appear normally through card rewards, shops, etc. as part of the Regent card pool.

---

### Building from Source

```bash
# Prerequisites: .NET 9 SDK, Godot 4.5.1 Mono

# 1. Import assets (generates .import files)
godot --headless --path . --import

# 2. Build the DLL
dotnet build -c Release

# 3. Export the PCK
godot --headless --path . --export-pack "BasicExport" DragonGentian.pck

# 4. Deploy to mods folder (handled by build target automatically)
```

---

### Credits

- **Author:** oran
- **Card Art:** oran
- **Powered by:** BaseLib by Alchyr, Harmony

---

### License

MIT
