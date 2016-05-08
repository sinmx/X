﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using WeakEvent;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace X.Viewer
{
    
    public sealed partial class ContentView : UserControl
    {
        
        private readonly WeakEventSource<ContentViewEventArgs> _SendMessageSource = new WeakEventSource<ContentViewEventArgs>();
        public event EventHandler<ContentViewEventArgs> SendMessage
        {
            add { _SendMessageSource.Subscribe(value); }
            remove { _SendMessageSource.Unsubscribe(value); }
        }




        public IContentRenderer Renderer;

        public ContentView()
        {
            this.InitializeComponent();

            layoutRoot.DataContext = this;

         
            

        }

        private static ContentView _cv;

        public string Uri
        {
            get { return (string)GetValue(UriProperty); }
            set { SetValue(UriProperty, value); }
        }

        public static readonly DependencyProperty UriProperty =
            DependencyProperty.Register("Uri", typeof(string), typeof(ContentView), new PropertyMetadata(string.Empty, UriChanged));

        private static void UriChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

            if (!e.NewValue.Equals(e.OldValue)) {

                if (_cv != null) {
                    _cv.layoutRoot.Children.Remove(_cv.Renderer.RenderElement);
                    _cv.layoutRoot.Children.Clear();
                    _cv.Renderer.Unload();
                    _cv.Renderer.SendMessage -= Renderer_SendMessage;
                    _cv.Renderer = null;
                }

                 ContentView cv = (ContentView)d;
                
                if (cv.Renderer != null ) {
                    cv.layoutRoot.Children.Remove(cv.Renderer.RenderElement);
                    cv.layoutRoot.Children.Clear();
                    cv.Renderer.Unload();
                    cv.Renderer.SendMessage -= Renderer_SendMessage;
                    cv.Renderer = null;
                }

                if (string.IsNullOrEmpty((string)e.NewValue)) return;

                var uriNew = (string)e.NewValue;


                if (uriNew.Contains(".mp4") || uriNew.Contains(".mpeg") || uriNew.Contains(".avi") || uriNew.Contains(".webm") || uriNew.Contains(".ogv") || uriNew.Contains(".3gp") || uriNew.Contains(".mkv"))
                {
                    cv.Renderer = new FFmpeg.FFmpegRenderer();
                    cv.Renderer.SendMessage += Renderer_SendMessage;
                    cv.Renderer.Load();
                    cv.layoutRoot.Children.Add(cv.Renderer.RenderElement);
                    //cv.Renderer.UpdateSource(new Uri("ms-appx:///Assets/Videos/sample01.mp4"));
                    cv.Renderer.UpdateSource(uriNew);
                }
                else if (uriNew.Contains(".tile")){
                    cv.Renderer = new Tiles.TileRenderer();
                    cv.Renderer.SendMessage += Renderer_SendMessage;
                    cv.Renderer.Load();
                    cv.layoutRoot.Children.Add(cv.Renderer.RenderElement);

                    cv.Renderer.UpdateSource((string)e.NewValue);
                }
                else if (uriNew.Contains(".dotnet"))
                {
                    cv.Renderer = new DotnetCLI.DotnetCLIRenderer();
                    cv.Renderer.SendMessage += Renderer_SendMessage;
                    cv.Renderer.Load();
                    cv.layoutRoot.Children.Add(cv.Renderer.RenderElement);

                    cv.Renderer.UpdateSource((string)e.NewValue);
                }
                else if (uriNew.Contains(".map"))
                {
                    cv.Renderer = new MapView.MapViewRenderer();
                    cv.Renderer.SendMessage += Renderer_SendMessage;
                    cv.Renderer.Load();
                    cv.layoutRoot.Children.Add(cv.Renderer.RenderElement);

                    cv.Renderer.UpdateSource((string)e.NewValue);
                }
                else if (uriNew.Contains(".sketch"))
                {
                    cv.Renderer = new SketchFlow.SketchFlowRenderer();
                    cv.Renderer.SendMessage += Renderer_SendMessage;
                    cv.Renderer.Load();
                    cv.layoutRoot.Children.Add(cv.Renderer.RenderElement);
                    cv.Renderer.UpdateSource((string)e.NewValue);
                }
                else if (uriNew.Contains(".urho"))
                {
                    cv.Renderer = new UrhoSharp.UrhoRenderer();
                    cv.Renderer.SendMessage += Renderer_SendMessage;
                    cv.Renderer.Load();
                    cv.layoutRoot.Children.Add(cv.Renderer.RenderElement);

                    cv.Renderer.UpdateSource((string)e.NewValue);
                }
                else
                {
                    cv.Renderer = new WebViewRenderer();
                    cv.Renderer.SendMessage += Renderer_SendMessage;
                    cv.Renderer.Load();
                    cv.layoutRoot.Children.Add(cv.Renderer.RenderElement);

                    cv.Renderer.UpdateSource((string)e.NewValue);
                }

                _cv = cv;

            }
            
        }

        private static void Renderer_SendMessage(object sender, ContentViewEventArgs e)
        {
            _cv?._SendMessageSource.Raise(sender, e);
        }
    }



}
