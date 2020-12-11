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
        const int DETECTIONRANGE = 4;

        Farmer player = Game1.player;
        Tool playerTool = Game1.player.CurrentTool;
        GameLocation playerLocation = Game1.player.currentLocation;
               
        public override void Entry(IModHelper helper)
        {
            helper.Events.GameLoop.UpdateTicked += this.OnUpdateTicked;
        }

        private void OnUpdateTicked(object sender, EventArgs e)
        {
            if (!Context.IsWorldReady)
                return;

            Character c = Utility.isThereAFarmerOrCharacterWithinDistance(player.getTileLocation(), DETECTIONRANGE, playerLocation);

            //  Start swinging at the character's direction if a monster is detected
            //      and a  melee weapon is selected

            if (c.name == "Abigail" && c != player && playerTool is MeleeWeapon)      //debug aim detection using spouse lmao
            //if (c.IsMonster == true && c != player && playerTool is MeleeWeapon)
            {
                int direction = player.getGeneralDirectionTowards(c.Position);

                if (player.FacingDirection == direction) return;
               
                // We moved away from the target; make farmer face the right way again
                player.FacingDirection = direction;

                // Set run animation to be heading towards the target to simulate side-stepping
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

                playerTool.beginUsing(playerLocation, (int)player.GetToolLocation(true).X, (int)player.GetToolLocation(true).Y, player);

                return;
            }           
        }
    }
}
