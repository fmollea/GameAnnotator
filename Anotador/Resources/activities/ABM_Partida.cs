using Android.App;
using Android.OS;
using Android.Widget;
using Android.Support.Design.Widget;
using System;
using Android.Content;
using System.Collections.Generic;

namespace Anotador
{
    [Activity(Label = "@string/ABMgame_Title")]
    public class ABM_Partida : Activity
    {
        //Declaration of the differents components
        protected Android.Support.V7.Widget.Toolbar tToolbar;
        protected FloatingActionButton fabOk;

        protected Spinner sPlayers;
        protected Spinner sHooks;

        protected IList<EditText> listNamesEtx;

        protected int countPlayers;
        protected int countHooks; //the four value (or position) is equivalent to free hooks

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ABM_Partida);
            //This methods initialize the objects
            InitComponents();
            Delegate();
            FillSpinners();
        }

        //Initialize the objects
        protected void InitComponents()
        {
            listNamesEtx = new List<EditText>();
            tToolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            fabOk    = FindViewById<FloatingActionButton>(Resource.Id.fabOk);
            sPlayers = FindViewById<Spinner>(Resource.Id.sPlayersNum);
            sHooks   = FindViewById<Spinner>(Resource.Id.sHooksNum);
            listNamesEtx.Add(FindViewById<EditText>(Resource.Id.ePlayer1));
            listNamesEtx.Add(FindViewById<EditText>(Resource.Id.ePlayer2));
            listNamesEtx.Add(FindViewById<EditText>(Resource.Id.ePlayer3));
            listNamesEtx.Add(FindViewById<EditText>(Resource.Id.ePlayer4));
            listNamesEtx.Add(FindViewById<EditText>(Resource.Id.ePlayer5));
            listNamesEtx.Add(FindViewById<EditText>(Resource.Id.ePlayer6));
            
            //Set a title in toolbar
            tToolbar.Title = GetString(Resource.String.ABMgame_Title);
            countPlayers = -1; //Initialize countPlayers in -1
            countHooks = -1; //Initialize countHooks in -1
        }

        protected void Delegate()
        {
            fabOk.Click += delegate
            {
                StartGame(); //Start the game. Show a scoring user list.
            };

            //Obtain selected item (amount players in the game).
            sPlayers.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(PlayersSelected);
            //Obtain selected item (amount hooks per game).
            sHooks.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(HooksSelected);
        }

        //I add the string arrays a the spinners
        protected void FillSpinners()
        {
            var lAdapterPlayers = ArrayAdapter.CreateFromResource(this, Resource.Array.playersNum, 
                Android.Resource.Layout.SimpleSpinnerItem);
            lAdapterPlayers.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            sPlayers.Adapter = lAdapterPlayers;

            var lAdapterHooks = ArrayAdapter.CreateFromResource(this, Resource.Array.hooksNum, 
                Android.Resource.Layout.SimpleSpinnerItem);
            lAdapterHooks.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            sHooks.Adapter = lAdapterHooks;
        }

        //I obtain amount players selected and hide the editTexts left over
        protected void PlayersSelected(Object sender, AdapterView.ItemSelectedEventArgs e)
        {
            countPlayers = e.Position; //Obtain the item selected position 
            ShowAllPlayers(); //Show all players before hiding them 

            for (int i = 2 + countPlayers; i < 6; i++)
            {
                listNamesEtx[i].Text = string.Empty; //Clean the fields hidden
                listNamesEtx[i].Visibility = Android.Views.ViewStates.Gone;
            }
        }

        //This method does visible all players
        protected void ShowAllPlayers()
        {
            for (int i = 2; i < 6; i++)
                listNamesEtx[i].Visibility = Android.Views.ViewStates.Visible;
        }
        
        protected void HooksSelected(Object sender, AdapterView.ItemSelectedEventArgs e)
        {
            countHooks = e.Position; //the fourth position is equivalent to free hooks 
        }
        //This method init a new activity (the scoring list)
        protected void StartGame()
        {
            if (checkPlayerNames(countPlayers))
            {
                var intent = new Intent(this, typeof(ScoringList));

                intent.PutExtra("PLAYER1", listNamesEtx[0].Text);
                intent.PutExtra("PLAYER2", listNamesEtx[1].Text);
                intent.PutExtra("PLAYER3", listNamesEtx[2].Text);
                intent.PutExtra("PLAYER4", listNamesEtx[3].Text);
                intent.PutExtra("PLAYER5", listNamesEtx[4].Text);
                intent.PutExtra("PLAYER6", listNamesEtx[5].Text);
                intent.PutExtra("COUNT_PLAYERS", countPlayers);
                intent.PutExtra("COUNT_HOOK", countHooks);
                StartActivity(intent);
            }
        }

        //This method check that name fields is not empty
        protected bool checkPlayerNames(int pCountPlayers)
        {
            bool result = true;

            for (int i = 0; i < 2 + countPlayers; i++)
                if (listNamesEtx[i].Text == string.Empty)
                {
                    result = false;
                    listNamesEtx[i].Error = GetString(Resource.String.PlayerEmpty);
                }

            return result;
        }
    }
}