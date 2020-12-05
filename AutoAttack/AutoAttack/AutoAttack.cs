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

            double PlayerX = Math.Floor(Game1.player.Position.X / Game1.tileSize);
            double PlayerY = Math.Floor(Game1.player.Position.Y / Game1.tileSize);

            double PlayerDirection = Game1.player.FacingDirection;

            GameLocation gameLocation = Game1.player.currentLocation;
            Tool tool;

            double tileInFront = 0;

            // facing up = 0, right = 1, down = 2, left = 3

            this.Monitor.Log($"X: {PlayerX} Y: {PlayerY}, Facing: {PlayerDirection}", LogLevel.Debug);

            //this.Monitor.Log($"X: {Math.Floor(Game1.player.Position.X / Game1.tileSize)} Y: {Math.Floor(Game1.player.Position.Y / Game1.tileSize)}. Facing: {Game1.player.FacingDirection}", LogLevel.Debug);
            //this.Monitor.Log($"Location X: {Game1.player.getTileX() * Game1.tileSize} Y: {Game1.player.getTileY() * Game1.tileSize}.", LogLevel.Debug);

            switch (PlayerDirection)
            {
                //  Player is facing up
                case 0:
                    //-1y from player pos
                    PlayerY = PlayerY - 1;
                    break;

                //  Player is facing right
                case 1:
                    PlayerX = PlayerX + 1;
                    break;

                //  Player is facing down
                case 2:
                    PlayerY = PlayerY + 1;
                    break;

                //  Player is facing left
                case 3:
                    PlayerX = PlayerX - 1;
                    break;
            }
           
            foreach (Character c in gameLocation.characters)
            {
                int npcX = Convert.ToInt32(Math.Floor(c.Position.X / Game1.tileSize));
                int npcY = Convert.ToInt32(Math.Floor(c.Position.Y / Game1.tileSize));

                //  Farmer position +1 tile whatever direction they're facing
                Point p = new Point(Convert.ToInt32(PlayerX), Convert.ToInt32(PlayerY));

                //  NPC position
                Point p2 = new Point(npcX, npcY);

                if (p == p2 && Game1.player.IsEquippedItem(tool. ))
                {
                    Game1.player.
                    this.Monitor.Log($"{c.name} is in front of you.", LogLevel.Debug);
                }
            }                
        }
    }
}
