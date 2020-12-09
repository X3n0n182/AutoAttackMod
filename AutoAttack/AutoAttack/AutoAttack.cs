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
            //helper.Events.Input.ButtonPressed += new EventHandler<ButtonPressedEventArgs>(this.ButtonPressed);
            helper.Events.GameLoop.UpdateTicked += this.OnUpdateTicked;
        }

        private void ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            SButton[] walkBtns = new SButton[4];
            
            if (walkBtns is null)
            {
                walkBtns[0] = SButton.W;
                walkBtns[1] = SButton.A;
                walkBtns[2] = SButton.S;
                walkBtns[3] = SButton.D;
            }

            foreach (SButton sb in walkBtns)
            {
                if (sb != e.Button)
                {
                    Helper.Events.GameLoop.UpdateTicked += this.OnUpdateTicked;
                }
            }
        }

        private void OnUpdateTicked(object sender, EventArgs e)
        {
            if (!Context.IsWorldReady)
                return;

            Farmer player = Game1.player;
            Tool playerTool = Game1.player.CurrentTool;     
            GameLocation playerLocation = Game1.player.currentLocation;

            const int TILERANGE = 3;
                  
            Character c = Utility.isThereAFarmerOrCharacterWithinDistance(player.getTileLocation(), TILERANGE, playerLocation);

            //if (c.IsMonster == true && c != player && playerTool is MeleeWeapon)
            //if (c.name == "Abigail" &&c != player && playerTool is MeleeWeapon)
            if (c.IsMonster == true && c != player && playerTool is MeleeWeapon)
            {
                int direction = player.getGeneralDirectionTowards(c.Position);

                // We moved towards the target; we don't need to do anything
                if (player.FacingDirection == direction) return;
               
                // We moved away from the target; make farmer face the right way again
                player.FacingDirection = direction;

                // Set run animation to be heading towards the target; does not support walking yet...
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

                //MeleeWeapon mw = (MeleeWeapon)playerTool;
                //mw.doSwipe(Convert.ToInt32(mw.GetType()), player.GetToolLocation(true), direction, mw.speed, player);
                //playerTool.leftClick(player);
                return;
            }           
        }

        private void unusedCode()
        {
            //if (c.IsMonster == true && c != player && playerTool is MeleeWeapon)
            //{
            //    if (c.getTileLocation() == player.getTileLocation())
            //    {
            //        MeleeWeapon mw = new MeleeWeapon();
            //        mw.triggerDefenseSwordFunction(player);

            //    }
            //    else
            //    {
            //        player.FacingDirection = player.getGeneralDirectionTowards(c.Position);
            //        playerTool.leftClick(player);
            //    }

            //}

            //c = null;

            //List<Vector2> tileRange = new List<Vector2>();
            //
            //switch (player.getFacingDirection())
            //{
            //    // up right down left
            //    case 0:
            //        tileRange.Add(new Vector2(player.getTileX(), player.getTileY() - 1));           // 🟩 🟩 🟩
            //        tileRange.Add(new Vector2(player.getTileX(), player.getTileY() - 2));           // 🟩 🟩 🟩
            //        tileRange.Add(new Vector2(player.getTileX() - 1, player.getTileY() - 1));       //   O
            //        tileRange.Add(new Vector2(player.getTileX() + 1, player.getTileY() - 1));
            //        tileRange.Add(new Vector2(player.getTileX() - 1, player.getTileY() - 2));
            //        tileRange.Add(new Vector2(player.getTileX() + 1, player.getTileY() - 2));
            //        break;
            //    case 1:
            //        tileRange.Add(new Vector2(player.getTileX() + 1, player.getTileY()));
            //        tileRange.Add(new Vector2(player.getTileX() + 1, player.getTileY() - 1));       //   🟩 🟩
            //        tileRange.Add(new Vector2(player.getTileX() + 1, player.getTileY() + 1));       // O 🟩 🟩
            //        tileRange.Add(new Vector2(player.getTileX() + 2, player.getTileY()));           //   🟩 🟩
            //        tileRange.Add(new Vector2(player.getTileX() + 2, player.getTileY() - 1));
            //        tileRange.Add(new Vector2(player.getTileX() + 2, player.getTileY() + 1));
            //        break;
            //    case 2:
            //        tileRange.Add(new Vector2(player.getTileX(), player.getTileY() + 1));           //   O
            //        tileRange.Add(new Vector2(player.getTileX() - 1, player.getTileY() + 1));       // 🟩 🟩 🟩
            //        tileRange.Add(new Vector2(player.getTileX() + 1, player.getTileY() + 1));       // 🟩 🟩 🟩
            //        tileRange.Add(new Vector2(player.getTileX(), player.getTileY() - 2));
            //        tileRange.Add(new Vector2(player.getTileX() - 1, player.getTileY() - 2));
            //        tileRange.Add(new Vector2(player.getTileX() + 1, player.getTileY() - 2));
            //        break;
            //    case 3:
            //        tileRange.Add(new Vector2(player.getTileX() - 1, player.getTileY()));           // 🟩 🟩
            //        tileRange.Add(new Vector2(player.getTileX() - 1, player.getTileY() - 1));       // 🟩 🟩 O
            //        tileRange.Add(new Vector2(player.getTileX() - 1, player.getTileY() + 1));       // 🟩 🟩
            //        tileRange.Add(new Vector2(player.getTileX() - 2, player.getTileY()));
            //        tileRange.Add(new Vector2(player.getTileX() - 2, player.getTileY() - 1));
            //        tileRange.Add(new Vector2(player.getTileX() - 2, player.getTileY() + 1));
            //        break;
            //}
        }
    }
}
