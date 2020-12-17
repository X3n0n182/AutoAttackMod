using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley.Tools;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using System.Collections.Generic;

namespace AutoAttack
{
    public class AutoAttack : Mod
    {           
        public override void Entry(IModHelper helper)
        {
            //helper.Events.GameLoop.UpdateTicked += this.TestingMethod;
            helper.Events.GameLoop.UpdateTicked += this.OnUpdateTicked;
            //helper.Events.Input.ButtonPressed += new EventHandler<ButtonPressedEventArgs>(this.ButtonPressed);
        }

        private void TestingMethod(object sender, EventArgs e)
        {
            if (!Context.IsWorldReady)
                return;

            Farmer player = Game1.player;

            //player.CurrentTool.

            if (player.CurrentTool is MeleeWeapon)
            {
                this.Monitor.Log($"{player.CurrentTool.GetType().Name} {player.CurrentTool.GetType().BaseType} {player.CurrentTool.GetType().ToString()}", LogLevel.Debug);
            }
        }

        private void OnUpdateTicked(object sender, EventArgs e)
        {
            //  Don't do stuff if player hasn't loaded a save
            if (!Context.IsWorldReady)
                return;

            //  Tile range at which to start swinging your weapon towards the monster
            const int DETECTIONRANGE = 4;

            //  Get player and current location on the map
            Farmer player = Game1.player;            
            GameLocation playerLocation = Game1.player.currentLocation;

            //  Get the closest NPC in range
            Character c = Utility.isThereAFarmerOrCharacterWithinDistance(player.getTileLocation(), DETECTIONRANGE, playerLocation);

            //debug aim detection using spouse lmao
            //if (c.name == "Abigail" && c != player)
            //if (c.IsMonster == true && c != player)

            //  Start swinging at the character's direction if a monster is detected
            //      and a melee weapon is selected
            if (player.isActive() && player.CurrentTool != null && 
                player.CurrentTool is MeleeWeapon && !player.CurrentTool.Name.ToLower().Contains("scythe"))
            {
                if (c.IsMonster == true && c != player)
                {
                    //  Direction to point farmer towards monster
                    int direction = player.getGeneralDirectionTowards(c.Position);

                    //  Don't do anything if already facing monster
                    if (player.FacingDirection == direction) return;

                    // We moved away from the target; make farmer face the right way again
                    player.FacingDirection = direction;

                    // Set run animation to be pointing towards the target to simulate side-stepping
                    //  depending on set direction
                    switch (direction)
                    {
                        case 0:
                            player.FarmerSprite.animate(48, new GameTime());
                            break;
                        case 1:
                            player.FarmerSprite.animate(40, new GameTime());
                            break;
                        case 2:
                            player.FarmerSprite.animate(32, new GameTime());
                            break;
                        case 3:
                            player.FarmerSprite.animate(56, new GameTime());
                            break;
                    }

                    //  Start swinging the weapon
                    player.CurrentTool.beginUsing(playerLocation, (int)player.GetToolLocation(true).X, (int)player.GetToolLocation(true).Y, player);
                    //player.FireTool();
                    return;
                }                              
            }
        }

        private void ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (!Context.IsWorldReady)
                return;

            if (e.Button.IsUseToolButton())
            {
                SButton useBtn = e.Button;
                Farmer player = Game1.player;
                GameLocation playerLocation = Game1.player.currentLocation;
                Tool playerTool = Game1.player.CurrentTool;

                if (e.IsDown(useBtn))
                {
                    //player.FireTool();
                    //playerTool.beginUsing(playerLocation, (int)player.GetToolLocation(true).X, (int)player.GetToolLocation(true).Y, player);
                    //playerTool.leftClick(player);
                }
            }
        }
    }
}
