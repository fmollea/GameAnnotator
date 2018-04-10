using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;

namespace Anotador
{
    [Activity(Label = "@string/scoringList_Title")]
    public class ScoringList : Activity
    {
        protected IList<TextView> listPlayerNames;
        protected IList<EditText> listFinalScores;
        protected IList<string> listNames;

        protected FloatingActionButton fabAdd;
        protected FloatingActionButton fabEdit;
        protected FloatingActionButton fabDelete;

        protected Android.Support.V7.Widget.Toolbar tToolbar;
        protected ListView lvScoring;
                
        protected int FCantPlayers;
        protected int FCantHooks;
        protected int FPosScore = -1; 

        protected List<Scoring> listScoring;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ScoringList);
            InitComponents();
            ObtainData();
            PutsPlayers();
            Delegate();
        }

        protected override void OnRestart()
        {
            
            base.OnRestart();
            
        }
        protected void InitComponents()
        {
            listPlayerNames = new List<TextView>();

            listPlayerNames.Add(FindViewById<TextView>(Resource.Id.tPlayer1));
            listPlayerNames.Add(FindViewById<TextView>(Resource.Id.tPlayer2));
            listPlayerNames.Add(FindViewById<TextView>(Resource.Id.tPlayer3));
            listPlayerNames.Add(FindViewById<TextView>(Resource.Id.tPlayer4));
            listPlayerNames.Add(FindViewById<TextView>(Resource.Id.tPlayer5));
            listPlayerNames.Add(FindViewById<TextView>(Resource.Id.tPlayer6));

            listFinalScores = new List<EditText>();

            listFinalScores.Add(FindViewById<EditText>(Resource.Id.ePlayer1));
            listFinalScores.Add(FindViewById<EditText>(Resource.Id.ePlayer2));
            listFinalScores.Add(FindViewById<EditText>(Resource.Id.ePlayer3));
            listFinalScores.Add(FindViewById<EditText>(Resource.Id.ePlayer4));
            listFinalScores.Add(FindViewById<EditText>(Resource.Id.ePlayer5));
            listFinalScores.Add(FindViewById<EditText>(Resource.Id.ePlayer6));

            fabAdd = FindViewById<FloatingActionButton>(Resource.Id.fabAdd);
            fabEdit = FindViewById<FloatingActionButton>(Resource.Id.fabEdit);
            fabDelete = FindViewById<FloatingActionButton>(Resource.Id.fabDelete);

            tToolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            lvScoring = FindViewById<ListView>(Resource.Id.lvScoring);

            tToolbar.Title = GetString(Resource.String.scoringList_Title);
            listScoring = new List<Scoring>();
            listNames = new List<string>();
        }


        protected void Delegate()
        {
            fabAdd.Click += delegate { AddScore(); };
            lvScoring.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs e)
            {
                FPosScore = e.Position;
            };
        }

        protected void ObtainData()
        {
            listNames.Add(Intent.GetStringExtra("PLAYER1"));
            listNames.Add(Intent.GetStringExtra("PLAYER2"));
            listNames.Add(Intent.GetStringExtra("PLAYER3"));
            listNames.Add(Intent.GetStringExtra("PLAYER4"));
            listNames.Add(Intent.GetStringExtra("PLAYER5"));
            listNames.Add(Intent.GetStringExtra("PLAYER6"));
            FCantHooks = Intent.GetIntExtra("COUNT_HOOKS", -1);
            FCantPlayers = Intent.GetIntExtra("COUNT_PLAYERS", -1);
            if (FCantPlayers != -1)
            {
                FCantPlayers = FCantPlayers + 2;
            }
        }

        protected void PutsPlayers()
        {
            for(int i=0;i<6; i++)
                CheckPlayers(listNames[i], listPlayerNames[i], listFinalScores[i]);
        }

        protected void CheckPlayers(String pName, TextView pPlayer, EditText pScore)
        {
            if (pName == string.Empty)
            {
                pPlayer.Visibility = ViewStates.Gone;
                pScore.Visibility = ViewStates.Gone;
            }
            else
                pPlayer.Text = pName;
        }
        
        protected void AddScore()
        {
            Dialog dlg = new Dialog(this);
            dlg.RequestWindowFeature(1); //the number 1 is the ID of WindowFeature.NoTittle
            dlg.SetContentView(Resource.Layout.ABM_Score);
            var lListScore = new List<EditText>();
            var lListNamesTxInLy = new List<TextInputLayout>();

            //Declaration of the differents components
            Android.Support.V7.Widget.Toolbar tToolbar = dlg.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            FloatingActionButton lFabOk = dlg.FindViewById<FloatingActionButton>(Resource.Id.fabOk);
            
            lListScore.Add(dlg.FindViewById<EditText>(Resource.Id.ePlayer1));
            lListScore.Add(dlg.FindViewById<EditText>(Resource.Id.ePlayer2));
            lListScore.Add(dlg.FindViewById<EditText>(Resource.Id.ePlayer3));
            lListScore.Add(dlg.FindViewById<EditText>(Resource.Id.ePlayer4));
            lListScore.Add(dlg.FindViewById<EditText>(Resource.Id.ePlayer5));
            lListScore.Add(dlg.FindViewById<EditText>(Resource.Id.ePlayer6));

            lListNamesTxInLy.Add(dlg.FindViewById<TextInputLayout>(Resource.Id.txtPlayer1));
            lListNamesTxInLy.Add(dlg.FindViewById<TextInputLayout>(Resource.Id.txtPlayer2));
            lListNamesTxInLy.Add(dlg.FindViewById<TextInputLayout>(Resource.Id.txtPlayer3));
            lListNamesTxInLy.Add(dlg.FindViewById<TextInputLayout>(Resource.Id.txtPlayer4));
            lListNamesTxInLy.Add(dlg.FindViewById<TextInputLayout>(Resource.Id.txtPlayer5));
            lListNamesTxInLy.Add(dlg.FindViewById<TextInputLayout>(Resource.Id.txtPlayer6));

            //Set a title in toolbar
            tToolbar.Title = GetString(Resource.String.Dlg_Add);
            //Set a names in textInputLayout
           for(int i=0; i<FCantPlayers; i++)
                lListNamesTxInLy[i].Hint = listNames[i];
           //Hide players
            HidePlayers(lListScore);
            lFabOk.Click += delegate 
            {
                if (ScoreOk(lListScore))
                {
                    SaveScores(lListScore);
                    ShowScore();
                    dlg.Dismiss();
                }
            };

            dlg.Show();

        }

        //I obtain amount players selected and hide the editTexts left over
        protected void HidePlayers(List<EditText> pListScore)
        {
            for (int i = FCantPlayers; i < 6; i++)
                pListScore[i].Visibility = ViewStates.Gone;
        }

        protected void SaveScores(List<EditText> pListScore)
        {
            listScoring.Add(ObtainScore(pListScore));
        }

        protected bool ScoreOk(List<EditText> pListScore)
        {
            bool result = true;

            for (int i=0; i<FCantPlayers; i++)
            {
                if (pListScore[i].Text == string.Empty)
                {
                    pListScore[i].Error = GetString(Resource.String.Error_EmptyField);
                    result = false;
                }
            }
            return result;
        }
        
        protected Scoring ObtainScore(List<EditText> pListScore)
        {
            List<string> lAux = new List<string>();
            for (int i = 0; i < 6; i++)
                if (pListScore[i].Text != string.Empty)
                    lAux.Add(pListScore[i].Text);
                else
                    lAux.Add("#");
            return new Scoring(lAux);
        }

        protected void EditScore()
        {

        }

        protected void DeleteScore()
        {

        }

        protected void PlusScoring(List<Scoring> pListScr)
        {
            Scoring src;
            int acumScore = 0;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < pListScr.Count; j++)
                {
                    src = pListScr[j];
                    if (src.GetScoring(i) != "#")
                        acumScore = acumScore + int.Parse(src.GetScoring(i));
                }
                listFinalScores[i].Text = acumScore.ToString();
                acumScore = 0;
            }
        }

        protected void ShowScore()
        {
            var listAdapter = new ListAdapter(this, listScoring);
            lvScoring.Adapter = listAdapter;
            PlusScoring(listScoring);
        }

        public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.Back)
            {
                AlertDialog.Builder dlgAlert = new AlertDialog.Builder(this);
                dlgAlert.SetMessage("Si sale de la vista actual perderá los cambios realizados ¿Esta seguro" +
                    "qué quiere volver atras?");
                dlgAlert.SetCancelable(true);
                dlgAlert.SetPositiveButton("Si", delegate { Finish(); });
                dlgAlert.SetNegativeButton("No", delegate { dlgAlert.Dispose(); });
                dlgAlert.Create();
                dlgAlert.Show();

            }
            return base.OnKeyDown(keyCode, e);
        }

    }
}