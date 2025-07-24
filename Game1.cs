using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Graphics;

namespace DemoApp;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Sprite _backgroundSprite;
    private AnimatedSprite _capguySprite;
    private int _xPosition = 100;
    private double _speedInPixelsPerSecond = 150;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        _graphics.PreferredBackBufferWidth = 1000;
        _graphics.PreferredBackBufferHeight = 650;
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // Load sprite atlas from Content Pipeline
        Texture2DAtlas spriteAtlas = Content.Load<Texture2DAtlas>("spritesheet");

        // Create sprite from atlas
        _backgroundSprite = spriteAtlas.CreateSprite("Background");
        
        // Create animation
        SpriteSheet animationSheet = new SpriteSheet("capguy", spriteAtlas);
        animationSheet.DefineAnimation("walk", builder =>
        {
            for (int i = 1; i <= 16; i++)
            {
                builder.AddFrame($"walk/{i:D4}", TimeSpan.FromMilliseconds(40));
            }
            builder.IsLooping(true);
        });
        _capguySprite = new AnimatedSprite(animationSheet, "walk");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _capguySprite.Update(gameTime);
        _xPosition = _xPosition + (int)(gameTime.ElapsedGameTime.TotalSeconds * _speedInPixelsPerSecond);
        _xPosition = _xPosition % (_graphics.PreferredBackBufferWidth + 100);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        _spriteBatch.Begin();
        _spriteBatch.Draw(_backgroundSprite, Vector2.Zero);
        _spriteBatch.Draw(_capguySprite, new Vector2(_xPosition, 580));
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
