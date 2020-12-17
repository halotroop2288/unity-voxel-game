# A Minecraft clone made with Unity

This is a fork of [this repo, by Jin Yuhan](https://github.com/Jin-Yuhan/MinecraftClone-Unity).
I plan to replace Minecraft copyrighted assets, and add my own twist to it.
For now, it remains a pretty great copy of Minecraft!

(Replacement screenshots coming soon!)

## Plans:
- [x] Remove LUA scripting
- [ ] Design a working inventory and items system
  - [x] Items
  - [ ] Item stacks
  - [ ] Containers
  - [ ] Player inventory
  - [ ] Hotbar
- [ ] Rewrite the game in my own style
  - [ ] Remove all the Chinese comments and console logs from the code
  - [ ] Avoid creating artificial limitations (like mapping blocks and items to a byte)
- [ ] Make hotbar scroll through the entire inventory, like the Half-Life 2 weapon wheel
- [ ] Add survival aspects
  - [ ] Health
  - [ ] Stamina
  - [ ] Hunger and thirst
  - [ ] Limited inventory
- [ ] Add a small list of animals and monsters
- [ ] Implement extra HUD elements
  - [ ] Minimap
  - [ ] Target tooltip (like the WAILA/HWYLA mod)
- [ ] Add work stations
  - [ ] Tool station
  - [ ] Furnace
  - [ ] ???
- [ ] ???
- [ ] Add Portal Guns!
- [ ] ???
- [ ] Profit!

## Features

* Infinite chunk generation
* Caves and Ores
* Lighting And Physics
* TNT Explosion
* Flowing water
* Sand affected by gravity
* Saves world to files
* Audios and Particle effects
* Custom resource packages
* Some bugs (〃'▽'〃)

## Coming soon
* A functional inventory system

## Block Editor and Item Editor in Unity

![Screenshot](Screenshots/3.png)

![Screenshot](Screenshots/4.png)

![Screenshot](Screenshots/5.png)

You can create a new block or item without writing any code! Some complex block logic only needs to be written using C# scripts.

(Sorry, Jin Yuhan. The LUA caused more problems than it solved for me.)

## References

Starting in the next few commits, I will be rewriting the whole game while heavily referencing Jin Yuhan's code,
which has its own set of references, but I will not be looking at them.

1. [Jin-Yuhan/MinecraftClone-Unity](https://github.com/Jin-Yuhan/MinecraftClone-Unity)
2. [Brackey's RPG in Unity tutorial series](https://www.youtube.com/playlist?list=PLPV2KyIb3jR4KLGCCAciWQ5qHudKtYeP7)
3. [B3agz's Minecraft in Unity tutorial series[https://www.youtube.com/playlist?list=PLVsTSlfj0qsWEJ-5eMtXsYp03Y9yF1dEn]
4. [B3agz's Discord server members](https://discord.gg/aZgBgC2)