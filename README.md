# AdvanceMyShop

Customize every aspect of your pricing system: discounts, markups, and multipliers — all fully configurable to align with your game view. Your store, your rules: take control and optimize for comfortable experience.

## Configuration

Use mod manager of your preference or [REPOConfig](https://thunderstore.io/c/repo/p/nickklmao/REPOConfig/) to edit mod settings.

I strongly suggest to tailor those settings to your liking, because everyone see game different.

### Available options

| Setting                             | Default                                   | Description                                                                                      |
|-------------------------------------|-----------------------------------------|--------------------------------------------------------------------------------------------------|
| General.DiscountChance              | `50`                                      | Chance of a discount to occur. Works shop wide. Set to 0 to disable.                           |
| General.OverpriceChance             | `50`                                      | Chance of an overprice to occur. Works shop wide. Set to 0 to disable.                         |
| General.OverpriceMultiplier         | `1`                                       | This multiplier will be applied to the base generated percentage from 'Percentages'. Set to 1 to disable. |
| General.IndividualApply             | `false`                                   | If enabled, then each item will have a chance of being overpriced/discounted separately, rather than the whole shop. |
| General.Percentages                 | `0.5, 1, 2.5, 5, 10, 15, 20, 25, 30, 50` | List of percentages that will be used in the overpricing/discounts event.                       |
| General.FreeItemChance              | `0.5`                                     | Chance of a free item to occur. Works per item. Set to 0 to disable.                           |
| General.OverpricedItemChance        | `0.5`                                     | Chance of an REALLY overpriced item to occur. Works per item. Set to 0 to disable.             |
| General.OverpricedItemMultiplier     | `5`                                       | Multiplier applied to the overpriced item.                                                      |
| General.DisableVanillaPriceMultiplier     | `false`                                       | If enabled, mod will ignore vanilla game item value multiplier. This might be useful for easier playthrough.                                                      |
| Multipliers.BasePriceMultiplier      | `1`                                       | Base price multiplier applied to all items (e.g. price * multiplier). Set to 1 to disable.    |
| Multipliers.MultiplierFor_[itemCategory] | `1`                                   | Multiplier applied to `[itemCategory]` items. Set to 1 to disable.                             |

## Compatibility

Mod was made to be compatible with other mods that change shop pricings, however I can't guarantee it because of my "skill issue". If you encounter strange behaviour AFTER this mod have been installed, please report it.

Mod was tested with `37` mods installed, including ones that change multipliers.

Currently, it is known that AdvanceMyShop is incompatible with other mods that have similar or same functionality. You will have to choose who to keep ;)

### Compatible with

- [MoreUpgrades](https://thunderstore.io/c/repo/p/BULLETBOT/MoreUpgrades/) from BULLETBOT (since v1.3.0)

## Bugs and suggestions

Feel free to open issue at [GitHub](https://github.com/DjHaski/AdvanceMyShop)!

## Credits

- R.E.P.O. Community
- semiwork (for creating such an amazing game)
