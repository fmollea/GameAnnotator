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
        protected FloatingActionButton bStart;
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
            bStart   = FindViewById<FloatingActionButton>(Resource.Id.fabStart);
            tToolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);

            //Set a title in toolbar
            tToolbar.Title = GetString(Resource.String.mainTitle);
        }

        protected void Delegate()
        {
            bStart.Click += delegate
            {
                //start ABM game
                StartActivity(typeof(ABM_Partida));
            };
        }
    }
}

