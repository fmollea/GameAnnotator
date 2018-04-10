using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Widget;
using System;

namespace Anotador
{
    //Main activity. In this activity the user start the game
    [Activity(MainLauncher = true, Label = "@string/mainTitle")]
    public class MainActivity : Activity 
    {
        //Declaration of the diferent components
        protected Button bStart;
        protected Android.Support.V7.Widget.Toolbar tToolbar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            //This method initialize the objects
            InitComponents();
            Delegate();
        }

        //Initialize the objects
        protected void InitComponents()
        {
            bStart   = FindViewById<Button>(Resource.Id.bAceptar);
            tToolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);

            //Set a title in toolbar
            var res = Calcular();
            tToolbar.Title = Calcular().ToString();
        }

        protected void Delegate()
        {
            bStart.Click += delegate
            {
                //start ABM game
                StartActivity(typeof(ABM_Partida));
            };
        }

        protected double Calcular()
        {
            double res = 0;
            double aux1 = Math.Pow((-33.127677) - (-33.127873), 2);
            double aux2 = Math.Pow((-64.364024) - (-64.362967), 2);
            res = Math.Sqrt(aux1 + aux2);

            return res;
        }
    }
}

