﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microcharts;
using System.Threading.Tasks;
using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppRun.services;
using AppRun.Model;
using AppRun.clases;
using Xamarin.Essentials;
using System.Threading;

namespace AppRun
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Progreso : ContentPage
    {
        
        List<CarrerasModelApi> service;
        RestApiCarreras restService;
        DateTime fecha;
        public double distancia;
        public double Distancia
        {
            get { return this.distancia; }
            set { distancia= value; }
        }

        private readonly List<ChartEntry> lista=new List<ChartEntry>();

        private readonly List<ChartEntry> tiempo=new List<ChartEntry>();
        public async void Data()
        {

            try
            {
                service = await restService.GetRepositoriesAsync(Constantes.urlGetCarreras);
               var datos = service.Where(c => c.idUser.ToString().Contains(Preferences.Get("id", "").ToString()));

                if (datos != null)
                {
                    var random = new Random();


                    var entries = new List<Entry>();
                    foreach (var data in datos)
                    {
                        var color = String.Format("#{0:X6}", random.Next(0x1000000));

                        lista.Add(new ChartEntry((float)(data.distancia))
                        {
                            Label = data.fecha.ToString(),
                            ValueLabel = data.distancia.ToString(),
                            Color = SKColor.Parse(color)

                        });

                        tiempo.Add(new ChartEntry((float)(data.tiempo))
                        {
                            Label = (data.carrera.ToString()==null ||data.carrera.ToString()=="") ? "carrera"+data.fecha.ToString():data.carrera.ToString(),
                            ValueLabel = data.tiempo.ToString(),
                            Color = SKColor.Parse(color)

                        });
                    }
                }
            }
            catch (Exception e)

            {
                return;
            }
         
           



        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            tiempo.Clear();
            lista.Clear();
            Data();
            
            
            MyLineChart.Chart = new LineChart { Entries = lista, AnimationProgress = 4, LabelColor = SKColors.Black, EnableYFadeOutGradient= true, LineSize= 3, LabelOrientation= Orientation.Horizontal, LineMode = LineMode.Spline, LabelTextSize = 28, IsAnimated = true };
            MyDonutChart.Chart = new DonutChart { Entries = lista, IsAnimated = true, LabelColor = SKColors.Black, LabelMode = LabelMode.LeftAndRight,  LabelTextSize= 28 };
            MyBarChart.Chart = new BarChart { Entries = tiempo, IsAnimated = true, LabelColor = SKColors.Black, BarAreaAlpha = 29, LabelTextSize = 28, PointMode= PointMode.Square, LabelOrientation= Orientation.Horizontal};
            MyPointChart.Chart = new PointChart { Entries = lista, IsAnimated = true, LabelColor = SKColors.Black, LabelOrientation = Orientation.Horizontal, LabelTextSize = 28, };
           // MyRadialGaugeChart.Chart = new RadialGaugeChart { Entries = lista, IsAnimated = true };
        }
        public Progreso()
        {
            InitializeComponent();
            restService = new RestApiCarreras();
            BindingContext = new MainPageModel();

        }
        protected override void OnParentSet()
        {
            base.OnParentSet();
            
            
        }

      
    }
}