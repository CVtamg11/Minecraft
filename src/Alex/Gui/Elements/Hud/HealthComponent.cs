using System;
using Alex.Common.Gui.Graphics;
using Alex.Entities;
using Microsoft.Xna.Framework;
using RocketUI;

namespace Alex.Gui.Elements.Hud
{
    public enum HeartValue
    {
        Full,
        Half,
        None
    }
    
    public class HealthComponent : Container
    {
        private Player Player { get; }
        private HealthBarHeart[] Hearts { get; }
        
        private float Health { get; set; }
        public HealthComponent(Player player)
        {
            Health = player.HealthManager.Health;
            Player = player;

            Height = 10;

            Hearts = new HealthBarHeart[10];
            for (int i = 0; i < 10; i++)
            {
                AddChild(Hearts[i] = new HealthBarHeart()
                {
                    Margin = new Thickness((i * 8), 0, 0, 0),
                    Anchor = Alignment.BottomLeft
                });
            }
        }

        protected override void OnUpdate(GameTime gameTime)
        {
            if (Player.HealthManager.Health != Health)
            {
                var hearts = Player.HealthManager.Health * (10d / Player.HealthManager.MaxHealth);
                var ceil = (int)Math.Ceiling(hearts);
                for (int i = 0; i < Hearts.Length; i++)
                {
                    HeartValue value = HeartValue.Full;
                    if (i < ceil)
                    {
                        value = HeartValue.Full;
                    }
                    else if (i == ceil - 1)
                    {
                        value = HeartValue.Half;
                    }
                    else
                    {
                        value = HeartValue.None;
                    }
                    
                    Hearts[i].Set(value);
                }
            }
            
            base.OnUpdate(gameTime);
        }
        
        public class HealthBarHeart : RocketControl
        {
            private TextureElement Texture { get; set; }

            //private 
            public HealthBarHeart()
            {
                Width = 9;
                Height = 9;
            
                AddChild(Texture = new TextureElement()
                {
                    Anchor = Alignment.TopLeft,

                    Height = 9,
                    Width = 9,
                    //Margin = new Thickness(4, 4)
                });
            }
        
            protected override void OnInit(IGuiRenderer renderer)
            {
                Background = renderer.GetTexture(AlexGuiTextures.HealthPlaceholder);
                Texture.Texture = renderer.GetTexture(AlexGuiTextures.HealthHeart);
            }

            public void Set(HeartValue value)
            {
                Texture.IsVisible = true;
            
                switch (value)
                {
                    case HeartValue.Full:
                        Texture.Texture = GuiRenderer.GetTexture(AlexGuiTextures.HealthHeart);
                        break;
                    case HeartValue.Half:
                        Texture.Texture = GuiRenderer.GetTexture(AlexGuiTextures.HealthHalfHeart);
                        break;
                    case HeartValue.None:
                        Texture.IsVisible = false;
                        break;
                }
            }
        }
    }
}