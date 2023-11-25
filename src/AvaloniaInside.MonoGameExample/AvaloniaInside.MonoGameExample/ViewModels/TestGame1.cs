using System.Linq;
using AvaloniaInside.MonoGame;
using MGCore;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AvaloniaInside.MonoGameExample.ViewModels;

public class TestGame1 : CoreGame
{


 

    private ResolutionRenderer? _res;

    private int _lastWidth, _lastHeight;



    public TestGame1():base()
    {
  
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




    protected override void Draw(GameTime gameTime)
    {
        _res.Begin();



        base.Draw(gameTime);   //this is the shared draw code...
        _res.End(); //todo drawing outside this works too.. see why


    }

    private void DrawModel(Model model, Matrix world, Matrix view, Matrix projection)
    {


    }




}
