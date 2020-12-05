using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;

namespace AutoAttack
{
    public class AutoAttack : Mod
    {

        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            helper.Events.GameLoop.UpdateTicked += this.OnUpdateTicked;
            
        }

        /*********
        ** Private methods
        *********/
        /// <summary>Raised after the player presses a button on the keyboard, controller, or mouse.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void OnUpdateTicked(object sender, EventArgs e)
        {
            // ignore if player hasn't loaded a save yet
            if (!Context.IsWorldReady)
                return;

            // tile poisition in the current level

            // facing up = 0, right = 1, down = 2, left = 3
            this.Monitor.Log($"X: {Math.Floor(Game1.player.Position.X / Game1.tileSize)} Y: {Math.Floor(Game1.player.Position.Y / Game1.tileSize)}. Facing: {Game1.player.FacingDirection}", LogLevel.Debug);
            //this.Monitor.Log($"Location X: {Game1.player.getTileX() * Game1.tileSize} Y: {Game1.player.getTileY() * Game1.tileSize}.", LogLevel.Debug);

            double PlayerX = Math.Floor(Game1.player.Position.X / Game1.tileSize);
            double PlayerY = Math.Floor(Game1.player.Position.Y / Game1.tileSize);
            double PlayerDirection = Game1.player.FacingDirection;

            GameLocation gameLocation = Game1.player.currentLocation;
            
            double tileInFront = 0;

            switch (PlayerDirection)
            {
                //  Player is facing up
                case 0:
                    //-1y from player pos
                    tileInFront = PlayerY - 1;
                    break;

                //  Player is facing right
                case 1:
                    tileInFront = PlayerX + 1;
                    break;

                //  Player is facing down
                case 2:
                    tileInFront = PlayerY + 1;
                    break;

                //  Player is facing left
                case 3:
                    tileInFront = PlayerX - 1;
                    break;
            }

            Microsoft.Xna.Framework.Point p = new Microsoft.Xna.Framework.Point(Convert.ToInt32(PlayerX), Convert.ToInt32(tileInFront));

            foreach (Character c in gameLocation.characters)
                while ((c.getTileLocationPoint() == p) && c.IsMonster == true)
                {
                    c.jumpWithoutSound();
                    this.Monitor.Log($"Attacking monster {c.name}", LogLevel.Debug);
                }
        }
    }
}
