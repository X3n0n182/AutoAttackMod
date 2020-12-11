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
            helper.Events.GameLoop.UpdateTicked += this.OnUpdateTicked;
        }

        private void OnUpdateTicked(object sender, EventArgs e)
        {
            if (!Context.IsWorldReady)
                return;

            //  Tile range at which to start swinging your weapon towards the monster
            const int DETECTIONRANGE = 4;

            //  Get player, their equipped tool and current location on the map
            Farmer player = Game1.player;
            Tool playerTool = Game1.player.CurrentTool;
            GameLocation playerLocation = Game1.player.currentLocation;

            //  Get the closest NPC in range
            Character c = Utility.isThereAFarmerOrCharacterWithinDistance(player.getTileLocation(), DETECTIONRANGE, playerLocation);

            //  Start swinging at the character's direction if a monster is detected
            //      and a  melee weapon is selected

            //debug aim detection using spouse lmao
            //if (c.name == "Penny" && c != player && playerTool is MeleeWeapon) 
            
            //  Point farmer towards farmer if it's a monster and farmer has equipped a melee weapon
            if (c.IsMonster == true && c != player && playerTool is MeleeWeapon)
            {
                //  Direction to point farmer towards monster
                int direction = player.getGeneralDirectionTowards(c.Position);

                //  Don't do anything if already facing monster
                if (player.FacingDirection == direction) return;
               
                // We moved away from the target; make farmer face the right way again
                player.FacingDirection = direction;

                // Set run animation to be pointing towards the target to simulate side-stepping
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
                playerTool.beginUsing(playerLocation, (int)player.GetToolLocation(true).X, (int)player.GetToolLocation(true).Y, player);

                return;
            }           
        }
    }
}
