using System;
using System.Diagnostics;
using Alex.Common.Services;
using Alex.Common.Utils;
using Alex.Common.Utils.Vectors;
using Alex.Entities;
using Alex.Gui.Elements;
using Alex.Gui.Elements.Context3D;
using Microsoft.Xna.Framework;
using RocketUI;
using RocketUI.Input;
using Color = Microsoft.Xna.Framework.Color;

namespace Alex.Gamestates.MainMenu.Profile
{
    public class ProfileEntry : SelectionListItem
    {
        public GuiEntityModelView ModelView { get; }
        public PlayerProfile Profile { get; }
        private Action<ProfileEntry> OnDoubleClick { get; }
        public ProfileEntry(PlayerProfile profile, Skin defaultSelection, Action<ProfileEntry> onDoubleClick)
        {
            Profile = profile;
            OnDoubleClick = onDoubleClick;
            
            MinWidth = 92;
            MaxWidth = 92;
            MinHeight = 128;
            MaxHeight = 128;
            
           // AutoSizeMode = AutoSizeMode.GrowOnly;
            
            AddChild(new TextElement()
            {
                Text = profile.Username,
                Margin = Thickness.Zero,
                Anchor = Alignment.TopCenter,
                //Enabled = false
            });

            Margin = new Thickness(0, 8);
            Anchor = Alignment.FillY;
           // AutoSizeMode = AutoSizeMode.GrowAndShrink;
           // BackgroundOverlay = new GuiTexture2D(GuiTextures.OptionsBackground);

            ModelView = new GuiEntityModelView(new RemotePlayer(null, (profile.Skin?.Slim ?? defaultSelection.Slim) ? "geometry.humanoid.customSlim" : "geometry.humanoid.custom")) /*"geometry.humanoid.customSlim"*/
            {
                BackgroundOverlay = new Color(Color.Black, 0.15f),
                Background = null,
             //   Margin = new Thickness(15, 15, 5, 40),

                Width = 92,
                Height = 128,

                Anchor = Alignment.Fill,
                
            };
            
            AddChild(ModelView);

            AddChild(new TextElement()
            {
                Text = profile.Username, //PROFILE TYPE!!!
                Margin = Thickness.Zero,
                Anchor = Alignment.BottomCenter,
                //Enabled = false,
                BackgroundOverlay = new Color(Color.Black, 0.5f),
                Background = null
            });
        }
        
        private readonly float _playerViewDepth = -512.0f;
        protected override void OnUpdate(GameTime gameTime)
        {
            base.OnUpdate(gameTime);
            
            var mousePos = Alex.Instance.GuiManager.FocusManager.CursorPosition;
            mousePos = GuiRenderer.Unproject(mousePos);
            
            var playerPos = ModelView.RenderBounds.Center.ToVector2();

            var mouseDelta = (new Vector3(playerPos.X, playerPos.Y, _playerViewDepth) - new Vector3(mousePos.X, mousePos.Y, 0.0f));
            mouseDelta.Normalize();

            var headYaw = (float)mouseDelta.GetYaw();
            var pitch = (float)mouseDelta.GetPitch();
            var yaw = (float)headYaw;

            ModelView.SetEntityRotation(-yaw, pitch, -headYaw);
        }

        private Stopwatch _previousClick = null;
        private bool FirstClick = true;
        protected override void OnCursorPressed(Point cursorPosition, MouseButton button)
        {
            base.OnCursorPressed(cursorPosition, button);

            if (_previousClick == null)
            {
                _previousClick = Stopwatch.StartNew();
                FirstClick = false;
                return;
            }

            if (FirstClick)
            {
                _previousClick.Restart();
                FirstClick = false;
            }
            else
            {
                if (_previousClick.ElapsedMilliseconds < 150)
                {
                    OnDoubleClick?.Invoke(this);
                }

                FirstClick = true;
            }
        }
    }
}