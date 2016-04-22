using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;


namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;

        SpriteFont Font1;
        SpriteBatch spriteBatch;

        Camera cam;
        Doodle player;

        List<Platform> plList;
        bool is_Player_Colliding;

        water water_;
        sky sky_;

        Vector3 ambientLightColor = new Vector3(0,0,0);

        int score;
        float maxheight;
        bool Gamelost;

        //BasicEffect for rendering
        BasicEffect basicEffect;

        //Geometric info
        VertexPositionColor[] triangleVertices;
        VertexBuffer vertexBuffer;

        public Game1()
        {
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

            cam = new Camera(GraphicsDevice);

            sky_ = new sky(new Vector3(0, 100, 0));
            sky_.Initialize(Content);

            water_ = new water(new Vector3(0, -100, 0));
            water_.Initialize(Content);

            player = new Doodle(new Vector3(0,0,0), cam);
            player.Initialize(Content);

            plList = new List<Platform>();

            add(new Vector3(30, 50, 60));
            add(new Vector3(0, -20, 0));
            add(new Vector3(10, 30, 60));
            add(new Vector3(30, 50, 60));
            add(new Vector3(30, 0, 40));
            add(new Vector3(-30, 50, 60));
            add(new Vector3(30, 90, -60));
            add(new Vector3(30, 80, 60));
            add(new Vector3(30, 25, 60));
            add(new Vector3(30, 50, 60));
            add(new Vector3(30, 40, 60));
            add(new Vector3(30, 32, 60));
            add(new Vector3(-30, 20, -60));
            add(new Vector3(30, 53, 60));
            add(new Vector3(30, 70, 60));


            is_Player_Colliding = false;

            base.Initialize();
            IsMouseVisible = true;

            Font1 = Content.Load<SpriteFont>("SpriteFont/Miramonte");

            score = 0;
            Gamelost = false;

            //BasicEffect
            basicEffect = new BasicEffect(GraphicsDevice);
            basicEffect.Alpha = 1f;

            // Want to see the colors of the vertices, this needs to be on
            basicEffect.VertexColorEnabled = true;

            //Lighting requires normal information which       VertexPositionColor does not have
            //If you want to use lighting and VPC you need to create a       custom def
            basicEffect.LightingEnabled = false;

            //Geometry - a simple triangle about the origin
            triangleVertices = new VertexPositionColor[3];

            triangleVertices[0] = new VertexPositionColor(new Vector3(0, 20, 0), Microsoft.Xna.Framework.Color.Red);
            triangleVertices[1] = new VertexPositionColor(new Vector3(-20, -20, 0), Microsoft.Xna.Framework.Color.Green);
            triangleVertices[2] = new VertexPositionColor(new Vector3(20, -20, 0), Microsoft.Xna.Framework.Color.Blue);

            //Vert buffer
            vertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColor), 3, BufferUsage.WriteOnly);
            vertexBuffer.SetData<VertexPositionColor>(triangleVertices);
            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);           
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // End game
            if (Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
                Exit();

            // Game over
            if (player.position.Y < water_.position.Y)
            {
                Gamelost = true;
            }

            if (!Gamelost)
            {
                // Player update
                player.Update(gameTime, plList);
                water_.Update(gameTime);

                // Score update
                if (player.position.Y > maxheight)
                {
                    maxheight = player.position.Y;
                    score = (int)(Math.Pow((double)(maxheight / 10), 3) / 4);
                }

                // update last because dependent on Player
                cam.Update(player.camtg);

            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            spriteBatch.Begin();
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.CornflowerBlue);

            basicEffect.Projection = cam.projectionMatrix;
            basicEffect.View = cam.viewMatrix;
            basicEffect.World = cam.worldMatrix;

            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.CornflowerBlue);
            GraphicsDevice.SetVertexBuffer(vertexBuffer);

            spriteBatch.DrawString(Font1, score.ToString(), new Vector2(375, 30), Microsoft.Xna.Framework.Color.Black);

            //Turn off culling so we see both sides of our rendered triangle
            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = rasterizerState;

            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, 3);

            }
            
            sky_.draw(cam);

            foreach (Platform pf in plList)
            {
                pf.draw(player.cam);
            }

            water_.draw(cam);
            player.draw();

            if (Gamelost)
            {

                spriteBatch.DrawString(Font1, "You have lost", new Vector2(350, 260), Microsoft.Xna.Framework.Color.Black);
            }

            base.Draw(gameTime);
            spriteBatch.End();

        }

        private Platform add(Vector3 pos)
        {
            Platform pf = new Platform(pos);
            pf.Initialize(Content);
            plList.Add(pf);

            return pf;
        }
    }
}
