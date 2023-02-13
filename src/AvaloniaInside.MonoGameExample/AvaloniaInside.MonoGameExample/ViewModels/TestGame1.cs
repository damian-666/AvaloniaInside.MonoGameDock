using System.Linq;
using AvaloniaInside.MonoGame;
using MGCore;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AvaloniaInside.MonoGameExample.ViewModels;

public class TestGame1 : CoreGame
{
    private Matrix _world = Matrix.CreateTranslation(new Vector3(0, 0, 0));
    private readonly Matrix _view = Matrix.CreateLookAt(new Vector3(0, 0, 10), new Vector3(0, 0, 0), Vector3.UnitY);
    private readonly Matrix _projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 800f/480f, 0.1f, 100f);

    /// <summary>
    /// Gets the graphics device manager.
    /// </summary>
    private GraphicsDeviceManager GraphicsDeviceManager { get; }


    private SpriteBatch _spriteBatch;
    private ResolutionRenderer _res;

    private int _lastWidth, _lastHeight;

    public Vector3 DiffuseColor { get; set; } = new(1f, 0.2f, 0.2f);
    public Vector3 SpecularColor { get; set; } = new(0, 1, 0);
    public Vector3 AmbientLightColor { get; set; } = new(0.2f, 0.2f, 0.2f);
    public Vector3 EmissiveColor { get; set; } = new(1, 0, 0);


    Texture2D sprite1;
    public TestGame1()
    {
        //	GraphicsDeviceManager = new GraphicsDeviceManager(this);
        //	Content.RootDirectory = "Content";
    }

    protected override void Initialize()
    {
        _lastWidth=GraphicsDevice.Viewport.Width;
        _lastHeight=GraphicsDevice.Viewport.Height;

        _res=new ResolutionRenderer(new Point(
            GraphicsDevice.Adapter.CurrentDisplayMode.Width,
            GraphicsDevice.Adapter.CurrentDisplayMode.Height), GraphicsDevice)
        {
            ScreenResolution=new Point(_lastWidth, _lastHeight),
            Method=ResizeMethod.Fill
        };

        base.Initialize();
    }

    protected override void LoadContent()
    {
        // Create a new SpriteBatch, which can be used to draw textures.
        _spriteBatch=new SpriteBatch(GraphicsDevice);

        sprite1=Content.Load<Texture2D>("surge");
        //		_monkeyModel = Content.Load<Model>("monkey/monkey");


        _clip=Content.Load<Effect>("ClipShaderNew");
        _clipMask=Content.Load<Texture2D>("surgeclip");
        _clip.Parameters[0].SetValue(_clipMask);

        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {

        if (_lastWidth!=GraphicsDevice.Viewport.Width||
            _lastHeight!=GraphicsDevice.Viewport.Height)
        {
            _lastWidth=GraphicsDevice.Viewport.Width;
            _lastHeight=GraphicsDevice.Viewport.Height;

            _res.ScreenResolution=new Point(_lastWidth, _lastHeight);
        }

        base.Update(gameTime);
    }

    Texture2D _clipMask;
    Effect _clip;

    private bool useEffect= false;


    //todo shoul we use reactive or nofity, how to do it the neweset way.
    //this cna be boud t the apps propert sheet that sets is properties at runtime 
    //or to a settings view

    /// <summary>
    /// Toggles the use of custom shader(s)
    /// </summary>
    public bool UseEffect { get => useEffect; set => useEffect=value; } 

    protected override void Draw(GameTime gameTime)
    {
        _res.Begin();

        GraphicsDevice.Clear(Color.Brown);


        //_spriteBatch.Begin(SpriteSortMode.Immediate,
        //                   blendState: BlendState.AlphaBlend,
        //                   samplerState: SamplerState.PointWrap,
        //                   effect: UseEffect ? _clip : null);

        //_spriteBatch.Draw(sprite1, destinationRectangle: new Rectangle(Point.Zero, new Point(_lastWidth, _lastHeight)), Color.White);

        //_spriteBatch.End();


         base.Draw(gameTime);   //this is the shared draw code...
        _res.End(); //todo drawing outside this works too.. see why

     //   base.Draw(gameTime); //todo drawing outside this works too.. see why
    }

    private void DrawModel(Model model, Matrix world, Matrix view, Matrix projection)
    {


    }




}
