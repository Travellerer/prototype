using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Windows.Forms;
using System.Drawing;



//ToDos: 

// Grafik/Modelle: Plattformen, Umgebung, Wasser, Sterbebild
// Logik: Kollision, Score als Text, Wasseranstieg, Verlieren,Kamera-koordinaten beschränken

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
        bool is_Player_Colliding;
        Model model;

        //Models
        Model doodle;
        Model platform;
        Model sky;
        Model water;

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
            player = new Doodle(new Vector3(0,0,0));
            is_Player_Colliding = false;

            base.Initialize();
            IsMouseVisible = true;

            Font1 = Content.Load<SpriteFont>("SpriteFont/Miramonte");


            //TextRenderer.DrawText(..., score.ToString(), FontFamily.GenericSansSerif, new System.Drawing.Rectangle(350, 30, 100, 30), System.Drawing.Color.Black, System.Drawing.Color.LightGray);
            model = Content.Load<Model>("Doodle/doodle");

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

            triangleVertices[0] = new VertexPositionColor(new Vector3(
                                  0, 20, 0), Microsoft.Xna.Framework.Color.Red);
            triangleVertices[1] = new VertexPositionColor(new Vector3(-
                                  20, -20, 0), Microsoft.Xna.Framework.Color.Green);
            triangleVertices[2] = new VertexPositionColor(new Vector3(
                                  20, -20, 0), Microsoft.Xna.Framework.Color.Blue);

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
            doodle = Content.Load<Model>("Doodle/doodle");
            platform = Content.Load<Model>("Platform/platform");
            sky = Content.Load<Model>("Sky/sky");
            water = Content.Load<Model>("Water/water");
            
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

            // Player update
            player.Update(gameTime, is_Player_Colliding);


            // Score update
            if (player.position.Y > maxheight)
            {
                maxheight = player.position.Y;
                score = (int) (Math.Pow((double)(maxheight/10),3)/4);
            }

            // update last because dependent on Player
            cam.Update(player.position);
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


            //Turn off culling so we see both sides of our rendered          triangle
            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = rasterizerState;

                    Passes)
            foreach (EffectPass pass in basicEffect.CurrentTechnique.
                    Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, 3);


            }

            VertexLoader doodleMesh = new VertexLoader(doodle, cam, ambientLightColor);
            doodleMesh.draw();

            VertexLoader platformMesh = new VertexLoader(platform, cam, ambientLightColor);
            platformMesh.draw();

            base.Draw(gameTime);
            spriteBatch.End();

        }
    }
}
