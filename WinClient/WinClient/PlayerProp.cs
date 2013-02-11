using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using WinClient.util.units;

namespace WinClient
{
    class PlayerProp
    {
        private const uint ALPHA_MASK = 0xFF000000;
        private const uint COLOR_MASK = ~ALPHA_MASK;
        private Texture2D pixel;

        private Vector2 direction = new Vector2(0, 1);
        private Vector2 position = new Vector2();
        private Degrees m_rotation = 0;
        private uint color = 0;
        public PlayerProp(Armies game1)
        {

            pixel = new Texture2D(game1.GraphicsDevice, 10, 10);
            var data = new uint[100];
            for (int i = 0; i < 100; i++ )
            {
                data[i] = 0xFF000000;
            }
            pixel.SetData(data);
            game1.onLoad += onLoad;
            color = (uint)new Random().Next(int.MinValue, int.MaxValue);
        }

        private void onLoad(Game game)
        {
            Load(game);
        }
        public void Draw(GameTime gameTime, SpriteBatch batch)
        {
            var white = new Color();
            white.PackedValue = 0;
            var bounds = pixel.Bounds;
            batch.Draw(pixel, new Rectangle((int)position.X, (int)position.Y, bounds.Width, bounds.Height), null, Color.White, (Radians)m_rotation, p2v(bounds.Center), SpriteEffects.None, 0);
        }

        public void Move(Vector2 dir)
        {
            position += dir;// Vector2.TransformNormal(dir, Matrix.CreateRotationZ(m_rotation));
        }


        public Degrees rotation
        {
            get {
                return m_rotation;
            }
            set {
                if (value != m_rotation) {
                    m_rotation = value;
                }
            }
        }

        public static Vector2 p2v(Point value)
        {
            return new Vector2(value.X, value.Y);
        }

        internal void setColor(uint p)
        {
            Console.WriteLine(p.ToString("x2"));
            uint[] data = new uint[pixel.Width * pixel.Height];
            pixel.GetData(data);
            for (int i = 0; i < data.Length; i++)
            {
                uint pix = data[i];
                data[i] = (pix | COLOR_MASK) & (ALPHA_MASK | p);
            }
            pixel.SetData(data);
            color = p;
        }

        internal void SetPosition(Vector2 value)
        {
            position = value;
        }

        internal void Load(Game game)
        {
            pixel = game.Content.Load<Texture2D>("player");
            uint[] data = new uint[pixel.Width * pixel.Height];
            pixel.GetData(data);
            pixel = new Texture2D(game.GraphicsDevice, pixel.Width, pixel.Height);
            for (int i = 0; i < data.Length; i++)
            {
                uint pix = data[i];
                data[i] = (pix & COLOR_MASK) == COLOR_MASK ? 0x0 : pix;
            }
            pixel.SetData(data);
            setColor(color);
        }
    }
}
