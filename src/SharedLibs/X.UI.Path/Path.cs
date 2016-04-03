﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace X.UI.Path
{
    public sealed class Path : Control
    {
        Windows.UI.Xaml.Shapes.Path _path;




        public PathType PathType
        {
            get { return (PathType)GetValue(PathTypeProperty); }
            set { SetValue(PathTypeProperty, value); }
        }

        public double PathWidth
        {
            get { return (double)GetValue(PathWidthProperty); }
            set { SetValue(PathWidthProperty, value); }
        }
        
        public double PathHeight
        {
            get { return (double)GetValue(PathHeightProperty); }
            set { SetValue(PathHeightProperty, value); }
        }
        
        public double Rotation
        {
            get { return (double)GetValue(RotationProperty); }
            set { SetValue(RotationProperty, value); }
        }







        public static readonly DependencyProperty RotationProperty =
            DependencyProperty.Register("Rotation", typeof(double), typeof(Path), new PropertyMetadata(0));

        public static readonly DependencyProperty PathHeightProperty =
            DependencyProperty.Register("PathHeight", typeof(double), typeof(Path), new PropertyMetadata(20));
        
        public static readonly DependencyProperty PathTypeProperty =
            DependencyProperty.Register("PathType", typeof(PathType), typeof(Path), new PropertyMetadata(0, OnPathTypePropertyChanged));
        
        public static readonly DependencyProperty PathWidthProperty =
            DependencyProperty.Register("PathWidth", typeof(double), typeof(Path), new PropertyMetadata(20));





        private static void OnPathTypePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instanceOfPath = d as Path;
            if(instanceOfPath._path!=null)
                instanceOfPath.FillPathData(instanceOfPath._path, (PathType)e.NewValue);
        }

 


        public Path()
        {
            this.DefaultStyleKey = typeof(Path);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (_path == null)
            {
                _path = GetTemplateChild("pth") as Windows.UI.Xaml.Shapes.Path;
                FillPathData(_path, PathType);
            }

        }

        private void FillPathData(Windows.UI.Xaml.Shapes.Path pathInstance, PathType typeOfPath){
            
            var dataPath = string.Empty ;

            if (typeOfPath == PathType.Book)
            {
                dataPath = "M8.15192985534668,0L8.16493034362793,0 8.16493034362793,39.189998626709C8.16493034362793,39.6419982910156 8.31793022155762,40.0549983978271 8.55592918395996,40.2599983215332 8.79993057250977,40.4699993133545 9.08692932128906,40.4329986572266 9.30992889404297,40.173999786377L15.2389297485352,33.1699991226196 20.8559303283691,40.1579990386963C20.9839305877686,40.3139991760254,21.1359310150146,40.3959999084473,21.2879314422607,40.3959999084473L21.6139316558838,40.2689990997314C21.8609313964844,40.0629997253418,22.0179309844971,39.6469993591309,22.0179309844971,39.189998626709L22.0179309844971,0 52.1599340438843,0C53.090934753418,0,53.8439350128174,0.757999420166016,53.8439350128174,1.6879997253418L53.8439350128174,49.3569984436035C53.8439350128174,50.2879981994629,53.090934753418,51.0459976196289,52.1599340438843,51.0459976196289L52.1399345397949,51.0410003662109C52.0039348602295,51.0699996948242,51.8759346008301,51.0909996032715,51.7449340820313,51.0909996032715L8.14793014526367,51.0909996032715C5.61592864990234,51.0909996032715 3.5399284362793,53.0789985656738 3.39693069458008,55.5789985656738 3.39693069458008,55.7309989929199 3.40092849731445,55.8460006713867 3.40092849731445,55.9209976196289L3.39292907714844,56.0529975891113C3.49493026733398,58.5929985046387,5.58692932128906,60.6279983520508,8.14793014526367,60.6279983520508L50.4719343185425,60.6279983520508 50.4719343185425,55.9669990539551C50.4719343185425,55.0359992980957 51.2299346923828,54.2779998779297 52.1599340438843,54.2779998779297 53.090934753418,54.2779998779297 53.8439350128174,55.0359992980957 53.8439350128174,55.9669990539551L53.8439350128174,62.3120002746582C53.8439350128174,63.246997833252,53.090934753418,64,52.1599340438843,64L7.89292907714844,64 7.63792991638184,63.9749984741211C3.3879280090332,63.7070007324219,0.00792694091796875,60.1749992370605,0.00792694091796875,55.8589973449707L0.0229301452636719,55.5669975280762C-0.0290718078613281,50.5599994659424,0.0229301452636719,12.4609990119934,0.0279273986816406,8.3179988861084L0.0119285583496094,8.14099884033203C0.0119285583496094,3.65299892425537,3.6649284362793,0,8.15192985534668,0z";

            }
            else if (typeOfPath == PathType.Key)
            {
                dataPath = "M16.547848,26.872497C14.451092,26.916562 12.365034,27.710413 10.729302,29.266098 7.2240894,32.589393 7.0834706,38.118687 10.403706,41.615683 13.72137,45.118677 19.252512,45.263676 22.752474,41.941881 26.247238,38.621584 26.401036,33.097193 23.078072,29.594298 21.314234,27.73567 18.92417,26.822555 16.547848,26.872497z M47.555126,0.0002117157C47.726013,0.0044841766,47.895291,0.073574066,48.021641,0.20638657L52.778168,5.1985388C53.03077,5.4641666,53.020371,5.888484,52.754769,6.1409225L52.232945,6.6370945 58.379608,13.115402C58.632122,13.382402,58.621121,13.806803,58.354809,14.058203L56.356011,15.956708C56.089798,16.207609,55.665879,16.197409,55.413365,15.930308L49.269745,9.4546347 48.00716,10.655153 52.407509,15.294587C52.660187,15.560289,52.649086,15.984593,52.382813,16.237495L50.384396,18.133013C50.118122,18.386015,49.694359,18.375215,49.441685,18.109612L45.04353,13.473104 30.99349,26.832494 31.170538,27.124847C35.031944,33.685067 34.017586,42.279621 28.253817,47.748075 21.549486,54.107365 10.954499,53.828564 4.5965569,47.125475 -1.7705653,40.417484 -1.4855343,29.826198 5.21898,23.462906 10.672487,18.294763 18.680851,17.507213 24.908787,20.994482L25.088602,21.09812 47.078934,0.18294907C47.211735,0.056484222,47.384236,-0.0040607452,47.555126,0.0002117157z";
            }
            else if (typeOfPath == PathType.Pin)
            {
                dataPath = "M537.76,0L767,237.883 699.552,307.874 654.177,260.828 449.89,472.781 460.135,483.414 447.983,639.291 336.562,523.67 0,767 265.122,449.537 154.528,334.849 304.815,322.276 311.431,329.066 336,303.562 336,300 339.432,300 515.648,117.076 470.311,69.9921z";
            }
            else if (typeOfPath == PathType.Camera)
            {
                dataPath = "M32.433098,16.311C39.318604,16.311 44.900002,21.891891 44.900002,28.777302 44.900002,35.662912 39.318604,41.245001 32.433098,41.245001 25.5485,41.245001 19.966999,35.662912 19.966999,28.777302 19.966999,27.701456 20.103268,26.657459 20.35948,25.661623L20.420795,25.435036 20.493568,25.619604C21.510777,28.024502 23.892122,29.712002 26.667551,29.712002 30.368025,29.712002 33.368,26.712002 33.368,23.011502 33.368,20.236127 31.680515,17.854783 29.275633,16.837572L29.091026,16.764781 29.317705,16.703444C30.313477,16.447252,31.35738,16.311,32.433098,16.311z M32.433102,11.324C22.793745,11.324 14.98,19.137912 14.98,28.777349 14.98,38.416887 22.793745,46.232 32.433102,46.232 42.072556,46.232 49.887001,38.416887 49.887001,28.777349 49.887001,19.137912 42.072556,11.324 32.433102,11.324z M6.3339348,10.896001C5.0713553,10.896001 4.0480003,11.919366 4.0480003,13.181001 4.0480003,14.443735 5.0713553,15.467001 6.3339348,15.467001 7.5964546,15.467001 8.6199999,14.443735 8.6199999,13.181001 8.6199999,11.919366 7.5964546,10.896001 6.3339348,10.896001z M21.6329,0L42.929802,0C44.881001,0,46.462402,1.582015,46.462402,3.5326004L47.086002,7.0652599C47.086002,7.126215,47.083851,7.1868101,47.079617,7.2470083L47.078251,7.2600002 64,7.2600002 64,50.897001 0,50.897001 0,7.2600002 17.277473,7.2600002 17.275982,7.2470083C17.271357,7.1868101,17.269001,7.126215,17.269001,7.0652599L18.100401,3.5326004C18.100401,1.582015,19.6819,0,21.6329,0z";
            }
            else if (typeOfPath == PathType.BatteryLow)
            {
                dataPath = "M8,8.0000002L18.666872,8.0000002 26.667,16 8,16z M5.3334079,5.3333104L5.3334079,18.666701 34.666943,18.666701 34.666943,5.3333104z M0,0L40.000282,0 40.000282,8.0000002 42.667,8.0000002 42.667,18.666701 40.000282,18.666701 40.000282,24 0,24z";
            }
            else if (typeOfPath == PathType.BatteryCharging)
            {
                dataPath = "M37.144343,6.3500005L22.345,23.449005 32.70436,18.68071 32.70436,28.712 48.16,11.775815 37.472542,16.378611z M7.0289994,0L64,0 64,35.514 7.0289994,35.514 7.0289994,26.636999 0,26.636999 0,8.509 7.0289994,8.509z";
            }
            else if (typeOfPath == PathType.Wifi1)
            {
                dataPath = "M32.207049,31.555002C34.397277,31.555002 36.171999,33.330445 36.171999,35.5207 36.171999,37.711457 34.397277,39.487003 32.207049,39.487002 30.017022,39.487003 28.240999,37.711457 28.240998,35.5207 28.240999,33.330445 30.017022,31.555002 32.207049,31.555002z M32.207049,21.118999C38.654932,21.118999,44.113132,25.358,45.948999,31.20124L42.235567,33.513794C41.303384,28.826783 37.167959,25.293499 32.207049,25.293499 27.16034,25.293499 22.971616,28.949085 22.134431,33.756L18.386999,31.459046C20.140868,25.483003,25.662967,21.118999,32.207049,21.118999z M32.056239,10.577002C42.306176,10.577002,51.110823,16.757293,54.952,25.593639L51.541821,27.718102C48.441538,19.996137 40.886885,14.543232 32.056239,14.543232 23.172193,14.543232 15.57844,20.062535 12.514758,27.859999L9.0889988,25.760336C12.890956,16.834692,21.742502,10.577002,32.056239,10.577002z M32.056,0C46.0938,0,58.2279,8.1408958,63.999998,19.958156L60.256598,22.290197C55.2787,11.709309 44.524799,4.3836174 32.056,4.3836174 19.509199,4.3836174 8.6953087,11.804411 3.7617188,22.496L0,20.190559C5.7253399,8.2477074,17.927099,0,32.056,0z";
            }
            else if (typeOfPath == PathType.Wifi2)
            {
                dataPath = "M0,42.393001L10.830997,42.393001 10.830997,55.657001 0,55.657001z M13.374001,31.669L24.205999,31.669 24.205999,55.658001 13.374001,55.658001z M26.639,21.056999L37.471,21.056999 37.471,55.658001 26.639,55.658001z M39.903999,10.446999L50.734999,10.446999 50.734999,55.657001 39.903999,55.657001z M53.167999,0L63.999,0 63.999,55.658001 53.167999,55.658001z";
            }
            else if (typeOfPath == PathType.More)
            {
                dataPath = "M2.8150252,17.479C4.3696795,17.479 5.6300002,18.739332 5.6300002,20.29395 5.6300002,21.848667 4.3696795,23.109001 2.8150252,23.109001 1.2603809,23.109001 6.2182664E-08,21.848667 0,20.29395 6.2182664E-08,18.739332 1.2603809,17.479 2.8150252,17.479z M2.8150252,8.7399998C4.3696795,8.7399998 5.6300002,10.000317 5.6300002,11.55504 5.6300002,13.109663 4.3696795,14.37 2.8150252,14.37 1.260381,14.37 6.2182664E-08,13.109663 0,11.55504 6.2182664E-08,10.000317 1.260381,8.7399998 2.8150252,8.7399998z M2.8150252,0C4.3696795,0 5.6300002,1.2605648 5.6300002,2.8150654 5.6300002,4.3696756 4.3696795,5.6300001 2.8150252,5.6300001 1.260381,5.6300001 6.2182664E-08,4.3696756 0,2.8150654 6.2182664E-08,1.2605648 1.260381,0 2.8150252,0z";
            }

            if (!string.IsNullOrEmpty(dataPath))
            {
                var b = new Binding { Source = dataPath };

                BindingOperations.SetBinding(pathInstance, Windows.UI.Xaml.Shapes.Path.DataProperty, b);
            }
        


        }

    }

    public enum PathType {
        None,
        Book,
        Key,
        Pin,
        Camera,
        BatteryLow,
        BatteryCharging,
        Wifi1,
        Wifi2,
        More
    }
}
