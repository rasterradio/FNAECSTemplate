using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MoonTools.ECS;
using ShmupTest.Systems;
using ShmupTest.Components;
using ShmupTest.Renderers;
using FontStashSharp;
using ShmupTest.Content;

namespace ShmupTest;


public class ShmupTest : Game
{
    GraphicsDeviceManager GraphicsDeviceManager { get; }

    /*
    the World is the place where all our entities go.5
    */
    World World { get; } = new World();

    Input Input;
    //ExampleSystem ExampleSystem;
    SpriteBatch SpriteBatch;
    SpriteRenderer SpriteRenderer;

    public static readonly int RenderWidth = 320;
public static readonly int RenderHeight = 240;
public static RenderTarget2D RenderTarget;
public static readonly float AspectRatio = (float)RenderWidth / RenderHeight;

    [STAThread]
    internal static void Main()
    {
        using (ShmupTest game = new ShmupTest())
        {
            game.Run();
        }
    }
    private ShmupTest()
    {
        //setup our graphics device, default window size, etc
        //here is where i will make a plea to you, intrepid game developer:
        //please default your game to windowed mode.
        GraphicsDeviceManager = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";

        GraphicsDeviceManager.PreferredBackBufferWidth = 1024;
        GraphicsDeviceManager.PreferredBackBufferHeight = 768;
        GraphicsDeviceManager.SynchronizeWithVerticalRetrace = true;

        IsFixedTimeStep = false;
        IsMouseVisible = true;
    }

    //you'll want to do most setup in LoadContent() rather than your constructor.
    protected override void LoadContent()
    {
        /*
        CONTENT
        */

        /*
        SpriteBatch is FNA/XNA's abstraction for drawing sprites on the screen.
        you want to do is send all the sprites to the GPU at once, 
        it's much more efficient to send one huge batch than to send sprites piecemeal. 
        See more in the Renderers/ExampleRenderer.cs. 
        */
        RenderTarget = new RenderTarget2D(GraphicsDevice, RenderWidth, RenderHeight);

        SpriteBatch = new(GraphicsDevice);

        AllContent.Initialize(Content);
        /*
        SYSTEMS
        */

        /*
        here we set up all our systems. 
        you can pass in information that these systems might need to their constructors.
        it doesn't matter what order you create the systems in, we'll specify in what order they run later.
        */
        // = new(World);
        Input = new(World);

        /*
        RENDERERS
        */

        SpriteRenderer = new SpriteRenderer(World, SpriteBatch);

        /*
        ENTITIES
        */

            var player = World.CreateEntity();
            World.Set(player, new Sprite(Textures.Player, 0.0f));
            World.Set(player, new Position(new Vector2(RenderWidth * 0.5f, RenderHeight * 0.5f)));
        base.LoadContent();
    }

    //sometimes content needs to be unloaded, but it usually doesn't.
    protected override void UnloadContent()
    {
        base.UnloadContent();
    }


    protected override void Update(GameTime gameTime)
    {
        /*
        here we call all our system update functions. 
        call them in the order you want them to run. 
        other ECS libraries have a master "update" function that does this for you,
        but moontools.ecs does not. this lets you have more control
        over the order systems run in, and whether they run at all.
        */
        Input.Update(gameTime.ElapsedGameTime); //always update this before anything that takes inputs
        //ExampleSystem.Update(gameTime.ElapsedGameTime);
        World.FinishUpdate(); //always call this at the end of your update function.
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.SetRenderTarget(RenderTarget);

        GraphicsDevice.Clear(Color.Black);
        SpriteRenderer.Draw();

        GraphicsDevice.SetRenderTarget(null);
        GraphicsDevice.Clear(Color.Black);

        SpriteBatch.Begin(
                SpriteSortMode.Deferred,
                BlendState.Opaque,
                SamplerState.PointClamp,
                DepthStencilState.None,
                RasterizerState.CullCounterClockwise
            );

        var height = Window.ClientBounds.Height;
        height -= (height % RenderHeight);
        var width = (int)MathF.Floor(height * AspectRatio);
        var wDiff = Window.ClientBounds.Width - width;
        var hDiff = Window.ClientBounds.Height - height;

        SpriteBatch.Draw(
            RenderTarget,
            new Rectangle(
                (int)MathF.Floor(wDiff * 0.5f),
                (int)MathF.Floor(hDiff * 0.5f),
                width,
                height),
            null,
            Color.White
        );

        SpriteBatch.End();
    }
}