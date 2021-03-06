﻿
using Microsoft.UI.Composition.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using static CoreLib.Effects.CompositionManager;

namespace CoreLib.Effects
{
    public class ShadowEffect : DependencyObject, ICompositionEffect
    {
        //xaml
        private UIElement element;
        private FrameworkElement frameworkElement;
        private bool isInitializedResources;

         
        //source
        public string Source
        {
            get { return (string)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register(nameof(Source), typeof(string), typeof(ShadowEffect), new PropertyMetadata(null, async (o,a)=> {
                
                

                if(a.NewValue!= null)
                {
                    ShadowEffect s = (ShadowEffect)o;
                    await s.InitializeResources();
                    s.Draw();
                }

            }));
        


        //Composition
        private ContainerVisual visual;
        private Compositor compositor;
        private CompositionImageFactory imageFactory;

        //Effect
        private string EffectSource = "EffectSource"; 
        private string EffectName = "ShadowEffect";
        private string BlurEffectPath = "ShadowEffect.BlurAmount";  //default 3
        private Microsoft.Graphics.Canvas.Effects.ShadowEffect effect;
        private CompositionEffectFactory effectFactory;


        //CompositionImage
        private CompositionSurfaceBrush surfaceBrush;
        private CompositionImage imageSource;


        //Brush & Sprite
        private CompositionEffectBrush effectBrush;
        private SpriteVisual spriteVisual;


        public void Initialize(UIElement attachedElement)
        {
            element = attachedElement;
            frameworkElement = element as FrameworkElement;
        }

        public void Uninitialize()
        {
            effectFactory = null;
            compositor = null;
        }

        public async Task<bool> InitializeResources() {
            
            if (isInitializedResources) return isInitializedResources;

            if (string.IsNullOrEmpty(Source)) return false;

            CreateImageFactory(element);

            CreateEffect();

            var isSurfaceCreated = await CreateSurface();
            if (!isSurfaceCreated) return isSurfaceCreated;

            CreateBrush();

            CreateSpriteVisual();

            Insert();
            
            isInitializedResources = true;
            
            return isInitializedResources;
        }

        public async Task<bool> Draw()
        {
            if (!isInitializedResources) return false;
            UpdateSpriteVisual();
            UpdateLevel();
            return true;
        }


        private void CreateImageFactory(UIElement element)
        {
            visual = GetVisual(element);
            compositor = GetCompositor(visual);
            imageFactory = CreateCompositionImageFactory(compositor);
        }

        private void CreateEffect()
        {
            effect = new Microsoft.Graphics.Canvas.Effects.ShadowEffect()
            {
                Name = EffectName,
                BlurAmount = 1.5f,
                ShadowColor = Colors.Black,
                Source = new CompositionEffectSourceParameter(EffectSource)
            };
            //Level = 1.5d;
            //UpdateLevel();
            effectFactory = compositor.CreateEffectFactory(effect, new[] { BlurEffectPath });
            
        }

        private async Task<bool> CreateSurface()
        {
            surfaceBrush = compositor.CreateSurfaceBrush();
            //var uriSource = UriManager.GetFilUriFromString(Source);
            //imageSource = imageFactory.CreateImageFromUri(uriSource);
            try
            {
                var file = await Windows.Storage.StorageFile.GetFileFromPathAsync(Source);
                if (file != null)
                {
                    imageSource = imageFactory.CreateImageFromFile(file);
                    surfaceBrush.Surface = imageSource.Surface;
                    surfaceBrush.Stretch = CompositionStretch.UniformToFill;
                }
            }
            catch //(Exception ex) 
            {
                return false;
            }

            return true;
        }

        private void CreateBrush()
        {
            effectBrush = effectFactory.CreateBrush();
            effectBrush.SetSourceParameter(EffectSource, surfaceBrush);
            effectBrush.Properties.InsertScalar(BlurEffectPath, 1.5f);
        }

        private void CreateSpriteVisual()
        {
            spriteVisual = compositor.CreateSpriteVisual();
            spriteVisual.Brush = effectBrush;
            spriteVisual.Size = new Vector2(GetElementWidth(), GetElementHeight());
        }

        private void UpdateSpriteVisual()
        {
            spriteVisual.Brush = effectBrush;
            spriteVisual.Size = new Vector2(GetElementWidth(), GetElementHeight());
        }

        private void Insert()
        {
            visual.Children.InsertAtBottom(spriteVisual);
        }

        private float GetElementWidth()
        {
            if (frameworkElement.ActualWidth.Equals(double.NaN))
            {
                return 0;
            }
            return (float)frameworkElement.ActualWidth;
        }

        private float GetElementHeight()
        {
            if (frameworkElement.ActualHeight.Equals(double.NaN))
            {
                return 0;
            }
            return (float)frameworkElement.ActualHeight;
        }


        //Level
        public double Level
        {
            get { return (double)GetValue(LevelProperty); }
            set { SetValue(LevelProperty, value); }
        }

        public static readonly DependencyProperty LevelProperty =
            DependencyProperty.Register(nameof(Level), typeof(double), typeof(ShadowEffect), new PropertyMetadata(0.0, LevelChanged));

        private static void LevelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ShadowEffect).UpdateLevel();
        }

        private void UpdateLevel()
        {
            if (effectBrush != null)
            {
                var newvalue = (float)Level;
                effectBrush.Properties.InsertScalar(BlurEffectPath, newvalue);
            }
        }


    }

}
