# Pets
This is a Plugin for the Game SCP:SL based on the Plugin Loader Synapse.<br>
This plugin allows server hoster to create Pets that follows the owner of the pet.

## Create a Pet
In order to create a pet just navigate to `~/Synapse/configs/server-{port}/pets`.<br>
In this Directory should be already a example pet be generated the first time you start the server with the Plugin.This file can you copy and modify
```yml
[Example]
{
petID: 0
role: ClassD
godMode: false
health: 150
name: Example Pet
badge: ''
badgeColor: ''
scale:
  x: 0.300000012
  y: 0.300000012
  z: 0.300000012
itemInHand: GunLogicer
maxAmount: 1
everyoneCanUse: true
permissions:
- pet.example
users:
- 000@steam
}
```
The PetID is the number that is used to spawn the Pet and for other Plugins to give access to this Pet to a different Player, which means it should be different for every pet that you create.

The Role is the skin that the Pet should use [RoleList](https://docs.synapsesl.xyz/resources)

The ItemInHand is like one can assume the Item that the pet should Hold. A list of all items can be found [here](https://docs.synapsesl.xyz/resources#items)

The Max Amount is the amount of this pet that a single Player can spawn.If you want to set a max value for all pets togethter change the value in the [config.syml](https://docs.synapsesl.xyz/setup/configs)

If you disable everyoneCanUse then are there 3 ways of giving some access to the Pet:
* Permissions: You can add a permission to the list and the Plugin will check if the player has this permission
* UserID: You can add a User ID manually to the Users list
* DataBase: A other Plugin can give access to a pet by setting a value in the Database

## Installation
1. Install [Synapse](https://github.com/SynapseSL/Synapse/)
2. Place the Pets.dll in your plugin folder
3. Restart/Start your Server
