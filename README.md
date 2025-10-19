# Gadgeteers

## Polite request for visitors
This repo is a code showcase - as a developer I ask to not put cloned code into commercial use as this is the game that in far future is supposed to be released.

## The Idea
A MOBA game where two teams fight each other trying to conduct a cyberattack on enemy's base. During a match players build _gadgets_ accordingly to their needs to defeat enemy team.
Each gadget has unique abilities and stats and can be augmented in different ways. Each player has a set of stats that impact the game and can be adjusted by user themselves.
Main target of the game is to give players a competitive field while enabling them to fully customize their play-style.

## Concepts
### Statistics
Game contains a versatile statistic system that can be applied both to players and to the items. Stats are accessed via keys and (apart from each having its base value) can depend on each other, allowing to create complex formulas for desired values.
Those can also be modified using modifiers, making the stat system flexible and adjustable. Before the match player can allocate a fixed number of points to chosen stats to define their strengths and weaknesses.

### Gadgets
The very essence of the game. Built during the match, gadgets provide players with unique abilities that scale from their stats. One can have up to 4 active gadgets and can use only active ones.

### Components
Each gadget can be augmented with up to 3 components. When placed in gadget's component slot, they grant bonus stats and (some) passive abilities

### Kits and perks
Second step after stat allocation is the profession. Player selects one kit and 2 perks, each granting passive effects and possibly some bonus stats. Effects of kits are stronger and have greater impact on the gameplay.
There are 6 kits in total, each having 4 perks assigned to it. This division, however, is just a suggestion of which perks go best with the kit they were assigned to.

### Damage types
Damage is split into 8 different types, each not excluding one another. Some actions consider the type of the damage.

---

> #### ⚠️ **DISCLAIMER** ⚠️
> This repo is just a way to share the code rather than version control.
> Due to the amount of changes made every day, frequent commits would be just inconveniant considering it is a solo project (as for now).
