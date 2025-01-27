﻿//#define RENDERTARGETTEST

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace MGCore
{


    /// <summary>
    /// Basic UI and core app level functions for YOUR  game .  for mobile and desktop platforms, built over the general purpose   XNA Game based code
    /// The platfrom dependent parts over this are designed to be as thin as possible, with all shared functionality and game asssets in here
    /// </summary>
    public class CoreGame : MGGameCore
    {

        //global settings

        internal const int MsaaSampleLimit = 32;


        public static new CoreGame Instance;

        public CoreGame()
        {
            Instance=this;
        }

        private bool useEffect = false;



        /// <summary>
        /// Toggles the use of custom shader(s)
        /// </summary>
        public bool UseEffect { get => useEffect; set => useEffect=value; }

        static public bool IsDirectX = false;



        protected override void Initialize()
        {        
            
            
            Window.Title="MG Cross Platform Shaders "+(IsDirectX ? "DirectX" : "OpenGL");


            Window.AllowUserResizing=true;
            base.Initialize();
        }


        Texture2D spritetoClip;






        Texture2D striteClipMask;
        protected override void LoadContent()
        {


            base.LoadContent();


            //we always have a Device by here
            GraphicsDevice.PresentationParameters.MultiSampleCount=0;     // set to windows limit, if gpu doesn't support it, monogame will autom. scale it down to the next supported level
                                                                          //    =GraphicsDeviceManager.PreferMultiSampling ? MsaaSampleLimit : 0;


            //TODo consder mg 3.8.1 fixes to windowing.. some are unmerged and recent..




#if RENDERTARGETTEST
            Window.Title+=" Render Target";
            
       //     clip=Content.Load<Effect>("ClipShader");
             clip=Content.Load<Effect>("ClipShaderNew");

            striteClipMask=Content.Load<Texture2D>("surgeclip");
#else
            Window.Title += " Sprite Test";
            clip=Content.Load<Effect>("ClipShaderNew");

            striteClipMask=Content.Load<Texture2D>("surgeclip"); 

            clip.Parameters["Mask"].SetValue(striteClipMask);
            
#endif

            _spriteBatch =new SpriteBatch(GraphicsDevice);






            //       shader = Content.Load<Effect>("Invert");
            
            spritetoClip=Content.Load<Texture2D>("surge");

     
        }


        Effect clip;


        protected override void OnActivated(object sender, EventArgs args)
        {
            base.OnActivated(sender, args);
        }



        protected override void BeginRun()
        {

        }



        protected override void Update(GameTime gt)
        {

            //TODO put this on the callback from the GameLoop, it call poll faster on the bk thread that can be faster than 60 hhz , works ok . occasiona touch exceptio colection modified but doesnt seem an issue
            base.Update(gt); //updates the Input keys

        }




        Texture2D clippedTex = null;
        protected override void Draw(GameTime gameTime)
        {



#if RENDERTARGETTEST
             GraphicsDevice.Clear(Color.Gainsboro);
            if (clippedTex==null)
            {
                clippedTex=Rasterizer.GetClippedTexture(GraphicsDevice, spritetoClip, striteClipMask, clip);

            }

     
    
            BasicEffect var = new BasicEffect(GraphicsDevice);
       

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null);


            Rectangle rct = GraphicsDevice.Viewport.Bounds;
         
            _spriteBatch.Draw(clippedTex,   rct, null,  Color.White);
            //no because we really wann just draw whats in the mask , it will skip alpha so it wond work the other way...   
            //sending blend mode sourcealpha might work but this is fine
            _spriteBatch.End();


#else
            GraphicsDevice.Clear(Color.Yellow);


      //          BasicEffect var = new BasicEffect(GraphicsDevice);
       

            //     clip.Parameters[0].SetValue(catClipMask);
            ////       clip.Parameters[1].SetValue(spriteCat); ;

            //   spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend,null,null,null,null);
            //
               _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, effect:clip);

            //    spriteBatch.Draw(catClipMask, new Vector2(100,100), Color.White); ;
            Rectangle rct = GraphicsDevice.Viewport.Bounds;
            _spriteBatch.Draw(spritetoClip,   rct,null,  Color.White);
           //no because we really wann just draw whats in the mask , it will skip alpha so it wond work the other way...   
           //sending blend mode sourcealpha might work but this is fine
               _spriteBatch.End();

#endif


        }


    }

}
