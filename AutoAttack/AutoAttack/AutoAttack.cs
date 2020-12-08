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
            //helper.Events.Input.ButtonPressed += this.OnButtonPressed;
        }

        private void OnUpdateTicked(object sender, EventArgs e)
        {
            // ignore if player hasn't loaded a save yet
            if (!Context.IsWorldReady)
                return;

            Farmer player = Game1.player;
            Tool playerTool = Game1.player.CurrentTool;     
            GameLocation playerLocation = Game1.player.currentLocation;
            List<Vector2> tileRange = new List<Vector2>();

            switch (player.getFacingDirection())
            {
                // up right down left
                case 0:
                    tileRange.Add(new Vector2(player.getTileX(), player.getTileY() - 1));           // 🟩 🟩 🟩
                    tileRange.Add(new Vector2(player.getTileX(), player.getTileY() - 2));           // 🟩 🟩 🟩
                    tileRange.Add(new Vector2(player.getTileX() - 1, player.getTileY() - 1));       //   O
                    tileRange.Add(new Vector2(player.getTileX() + 1, player.getTileY() - 1));
                    tileRange.Add(new Vector2(player.getTileX() - 1, player.getTileY() - 2));
                    tileRange.Add(new Vector2(player.getTileX() + 1, player.getTileY() - 2));
                    break;
                case 1:
                    tileRange.Add(new Vector2(player.getTileX() + 1, player.getTileY()));
                    tileRange.Add(new Vector2(player.getTileX() + 1, player.getTileY() - 1));       //   🟩 🟩
                    tileRange.Add(new Vector2(player.getTileX() + 1, player.getTileY() + 1));       // O 🟩 🟩
                    tileRange.Add(new Vector2(player.getTileX() + 2, player.getTileY()));           //   🟩 🟩
                    tileRange.Add(new Vector2(player.getTileX() + 2, player.getTileY() - 1));
                    tileRange.Add(new Vector2(player.getTileX() + 2, player.getTileY() + 1));       
                    break;
                case 2:
                    tileRange.Add(new Vector2(player.getTileX(), player.getTileY() + 1));           //   O
                    tileRange.Add(new Vector2(player.getTileX() - 1, player.getTileY() + 1));       // 🟩 🟩 🟩
                    tileRange.Add(new Vector2(player.getTileX() + 1, player.getTileY() + 1));       // 🟩 🟩 🟩
                    tileRange.Add(new Vector2(player.getTileX(), player.getTileY() - 2));
                    tileRange.Add(new Vector2(player.getTileX() - 1, player.getTileY() - 2));
                    tileRange.Add(new Vector2(player.getTileX() + 1, player.getTileY() - 2));
                    break;
                case 3:
                    tileRange.Add(new Vector2(player.getTileX() - 1, player.getTileY()));           // 🟩 🟩
                    tileRange.Add(new Vector2(player.getTileX() - 1, player.getTileY() - 1));       // 🟩 🟩 O
                    tileRange.Add(new Vector2(player.getTileX() - 1, player.getTileY() + 1));       // 🟩 🟩
                    tileRange.Add(new Vector2(player.getTileX() - 2, player.getTileY()));
                    tileRange.Add(new Vector2(player.getTileX() - 2, player.getTileY() - 1));
                    tileRange.Add(new Vector2(player.getTileX() - 2, player.getTileY() + 1));
                    break;
            }

            foreach (Character c in playerLocation.characters)
            {
                if (c.IsMonster == false && c != player && c.name == "Keeper")
                {
                    player.faceDirection(player.getGeneralDirectionTowards(c.getTileLocation()));
                    //player.FacingDirection = player.getGeneralDirectionTowards(c.getTileLocation());
                }
            }
        }

        //private void OnButtonPressed(object sender, ButtonPressedEventArgs e)
        //{
        //    // ignore if player hasn't loaded a save yet
        //    if (!Context.IsWorldReady)
        //        return;

        //    // print button presses to the console window
        //    this.Monitor.Log($"{Game1.player.Name} pressed {e.Button}.", LogLevel.Debug);
            
        //}
    }
}
