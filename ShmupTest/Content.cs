using System;
using System.IO;
using System.Reflection;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace ShmupTest.Content;
public static class Textures
{
    public static Texture2D Sprites { get; private set; }
    public static readonly Rectangle Bullet = new Rectangle(13, 16, 4, 8);
    public static readonly Rectangle Enemy = new Rectangle(0, 0, 16, 16);
    public static readonly Rectangle Player = new Rectangle(0, 16, 13, 15);
    public static void Initialize(ContentManager content)
    {
        Sprites = content.Load<Texture2D>("sprites");
    }
}
public static class Fonts
{
    public static FontSystem Opensans { get; private set; }
    public static void Initialize(ContentManager content)
    {
        Opensans = new FontSystem();
        Opensans.AddFont(File.ReadAllBytes(
            System.IO.Path.Combine(
                System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), content.RootDirectory, @"Fonts/opensans.ttf"
            )
        ));
    }
}
public static class SFX
{
    public static SoundEffect Death { get; private set; }
    public static SoundEffect Enemydeath { get; private set; }
    public static SoundEffect Hit { get; private set; }
    public static SoundEffect Shoot { get; private set; }
    public static void Initialize(ContentManager content)
    {
        Death = content.Load<SoundEffect>("death");
        Enemydeath = content.Load<SoundEffect>("enemydeath");
        Hit = content.Load<SoundEffect>("hit");
        Shoot = content.Load<SoundEffect>("shoot");
    }
}
public static class Songs
{
    public static void Initialize(ContentManager content)
    {
    }
}

public static class AllContent
{
    public static void Initialize(ContentManager content)
    {
        Textures.Initialize(content);
        Fonts.Initialize(content);
        SFX.Initialize(content);
        Songs.Initialize(content);
    }
}
