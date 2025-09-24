## 1.2.0
- Updated price calculation to match current game one.
- Added config entry `DisableVanillaPriceMultiplier`, which disables vanilla item value multiplier. Might be useful for someone.

## 1.1.2
- Fixed bug with upgrades being priced at single rate, rather than scale with purchased amount. Thanks to @Ty7aN
- Optimized base price calculation to match current game one.

## 1.1.1
- Fixed critical error due to culture info when parcing floats from config. Sorry but that.

## v1.1.0

- Improved compatibility with other mods changing various multipliers? Idk
- `Percentages` now supports `float` type (e.g. `1.5` or `0.25`).
  - `Percentages` now populated with smaller numbers by default to decrease chance of "buy all shop for nothing" thing to occur.
- Added `OverpriceMultiplier` to increase problems when overpricing occurs. Have fun with that lol.
- Increased `FreeItemChance` from `0.3%` to `0.5%` by default.
- Added `OverpricedItemChance` (default is `0.5%`) which will multiply item cost by `OverpricedItemMultiplier` (default is `5`).
- Boring code chore. I want it to look pretty, what!?

## v1.0.1

- Updated README due to another skill issue.

## v1.0.0

- Initial release, have fun!
