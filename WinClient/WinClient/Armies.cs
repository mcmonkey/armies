using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using Messaging;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Threading;
using WinClient.util.units;
using CommonUtil;
using Common;

namespace WinClient
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Armies : Microsoft.Xna.Framework.Game
    {
        public static Armies instance
        {
            get;
            private set;
        }
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        public delegate void MainGameEvent(Game game);
        public event MainGameEvent onInitialize;

        public event MainGameEvent onLoad;

        private Dictionary<int, PlayerProp> players = new Dictionary<int, PlayerProp>();

        public Armies()
        {
            if (instance != null) throw new Exception("Don't instaniate twice.");
            instance = this;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            if(onInitialize != null)
                onInitialize(this);
            base.Initialize();

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            if (onLoad != null)
                onLoad(this);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (IsActive)
            {
                // Allows the game to exit
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                    this.Exit();
                var dir = GamePad.GetState(PlayerIndex.One).ThumbSticks.Left;

                float rotation = (GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X * 2f * (float)gameTime.ElapsedGameTime.Milliseconds / 1000f);
                if (dir.LengthSquared() != 0 || rotation != 0)
                {
                    var message = new MoveBitch();
                    dir.Y *= -1;
                    message.direction = dir;
                    message.rotation = rotation;
                    SendMessage(message);
                }
                if (GamePad.GetState(PlayerIndex.One).IsButtonDown(Buttons.Start))
                {
                    TryConnect();
                }

            }
            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        public void SendMessage<T>(T message)
        {
            if (connected)
            {
                formatter.Serialize(client.GetStream(), message);
            }
        }
        private bool connected = false;

        private TcpClient client;
        private IFormatter formatter = new BinaryFormatter();

        public PlayerInfo Me { get; private set; }
        public void TryConnect()
        {
            if (!connected)
            {
                client = new TcpClient("127.0.0.1", 765);
                formatter.Serialize(client.GetStream(), new HandShake() {});
                var ack = formatter.Deserialize(client.GetStream()) as HandShakeAck;
                Me = ack.info;

                Thread thread = new Thread(Recieve);
                thread.Start();
                connected = true;
            }
        }

        private void Recieve()
        {
            infinity:

            var message = formatter.Deserialize(client.GetStream());


            PlayerProp player;
            int id = (message is PositionalBitch) ? (message as PositionalBitch).id : 0;
            if(players.TryGetValue(id, out player)) {
            
                if (message is ImHereBitch) {
                    ImHereBitch comeAtMe = message as ImHereBitch;
                    player.SetPosition(comeAtMe.direction);
                    player.rotation = comeAtMe.rotation;
                }
            } 

            if (message is PlayerConnected)  {
                var swap = message as PlayerConnected;
                MakeProp(swap.info);
            }
            else if (message is Match) {
                var match = message as Match;
                players.Clear();
                foreach (var info in match.players.Values)
                {
                    MakeProp(info);
                }
            }
            goto infinity;
        }

        private void MakeProp(PlayerInfo info) {
        
            PlayerProp prop;
            if(!players.TryGetValue(info.id, out prop)) {
                prop = new PlayerProp(this);
                prop.Load(this);
                players[info.id] = prop;
            }
            prop.setColor(info.color);
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            var blend = BlendState.NonPremultiplied;
            spriteBatch.Begin(SpriteSortMode.Deferred, blend);
            foreach (var player in players.Values) {
                player.Draw(gameTime, spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);

        }
    }
}
