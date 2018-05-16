using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;

namespace Anotador
{
    [Activity(Label = "@string/scoringList_Title")]
    public class ScoringList : Activity
    {
        protected IList<TextView> listPlayerNamesTxt; // list of textview the players names
        protected IList<EditText> listFinalScoresEdt; //List of editext of final score per player
        protected IList<string> listNames; //List of all players

        //FABs
        protected FloatingActionButton fabAdd;
        protected FloatingActionButton fabEdit;
        protected FloatingActionButton fabDelete;

        protected Android.Support.V7.Widget.Toolbar tToolbar;
        protected ListView lvScoring;
                
        protected int FCantPlayers; //players count
        protected int FCantHooks; // hooks count
        protected int FPosItemSelect = -1; // position of item selected  in scoring list
        protected Scoring FScrSelect; //item selected in scoring list
        
        protected List<Scoring> listScoring; //scoring list 

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ScoringList);
            InitComponents(); // binding and initialization of objects
            ObtainData(); // obtainzation the Intent data
            PutsPlayers();//This method puts the players names in your TextView and gone the fields where 
                          //the name is empty or not exist.
            Delegate();
        }

        protected override void OnRestart()
        {    
            base.OnRestart();   
        }

        // binding and initialization of objects
        protected void InitComponents()
        {
            //binding the textview using list
            listPlayerNamesTxt = new List<TextView>();

            listPlayerNamesTxt.Add(FindViewById<TextView>(Resource.Id.tPlayer1));
            listPlayerNamesTxt.Add(FindViewById<TextView>(Resource.Id.tPlayer2));
            listPlayerNamesTxt.Add(FindViewById<TextView>(Resource.Id.tPlayer3));
            listPlayerNamesTxt.Add(FindViewById<TextView>(Resource.Id.tPlayer4));
            listPlayerNamesTxt.Add(FindViewById<TextView>(Resource.Id.tPlayer5));
            listPlayerNamesTxt.Add(FindViewById<TextView>(Resource.Id.tPlayer6));

            //binding the edittext using list
            listFinalScoresEdt = new List<EditText>();

            listFinalScoresEdt.Add(FindViewById<EditText>(Resource.Id.ePlayer1));
            listFinalScoresEdt.Add(FindViewById<EditText>(Resource.Id.ePlayer2));
            listFinalScoresEdt.Add(FindViewById<EditText>(Resource.Id.ePlayer3));
            listFinalScoresEdt.Add(FindViewById<EditText>(Resource.Id.ePlayer4));
            listFinalScoresEdt.Add(FindViewById<EditText>(Resource.Id.ePlayer5));
            listFinalScoresEdt.Add(FindViewById<EditText>(Resource.Id.ePlayer6));

            fabAdd = FindViewById<FloatingActionButton>(Resource.Id.fabAdd);
            fabEdit = FindViewById<FloatingActionButton>(Resource.Id.fabEdit);
            fabDelete = FindViewById<FloatingActionButton>(Resource.Id.fabDelete);

            tToolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            lvScoring = FindViewById<ListView>(Resource.Id.lvScoring);
            
            //Set a title in toolbar
            tToolbar.Title = GetString(Resource.String.scoringList_Title);
            
            //Initialize the objects
            listScoring = new List<Scoring>();
            listNames = new List<string>();
            FScrSelect = new Scoring();
        }


        protected void Delegate()
        {
            fabAdd.Click += delegate { DlgScore(Const.Const_Action_Add); };
            fabEdit.Click += delegate { EditScore(); };
            fabDelete.Click += delegate { DeleteScore(); };

            lvScoring.ItemClick += delegate (object sender, AdapterView.ItemClickEventArgs e)
            {
                FPosItemSelect = e.Position;
            };
        }

        // obtainzation the Intent data
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

        //This method puts the players names in your TextView and gone the fields where the name is empty or not exist.
        protected void PutsPlayers()
        {
            for(int i=0;i<6; i++)
                CheckPlayers(listNames[i], listPlayerNamesTxt[i], listFinalScoresEdt[i]);
        }

        //This method check which fields should disappear. If not gone any field then puts the name in a textview
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
        
        //This method open a dialog window for add/edit score.
        protected void DlgScore(int pAction)
        {
            Dialog dlg = new Dialog(this);
            dlg.RequestWindowFeature(1); //the number 1 is the ID of WindowFeature.NoTittle
            dlg.SetContentView(Resource.Layout.ABM_Score);

            var lListScore = new List<EditText>();
            var lListNamesTxInLy = new List<TextInputLayout>();

            //Declaration of the differents components
            Android.Support.V7.Widget.Toolbar tToolbar = dlg.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            FloatingActionButton lFabOk = dlg.FindViewById<FloatingActionButton>(Resource.Id.fabOk);

            //binding the editText using list
            lListScore.Add(dlg.FindViewById<EditText>(Resource.Id.ePlayer1));
            lListScore.Add(dlg.FindViewById<EditText>(Resource.Id.ePlayer2));
            lListScore.Add(dlg.FindViewById<EditText>(Resource.Id.ePlayer3));
            lListScore.Add(dlg.FindViewById<EditText>(Resource.Id.ePlayer4));
            lListScore.Add(dlg.FindViewById<EditText>(Resource.Id.ePlayer5));
            lListScore.Add(dlg.FindViewById<EditText>(Resource.Id.ePlayer6));

            //binding the textInputLayout using list
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

            //If pAction is edit, set values
            if (pAction == Const.Const_Action_Edit)
            {
                SetEditValues(lListScore);
                lFabOk.Click += delegate
                {
                    for (int i = 0; i < FCantPlayers; i++)
                        listScoring[FPosItemSelect].SetScoring(i, lListScore[i].Text);
                    ShowScore();
                    dlg.Dismiss();
                };
            }
            else //pAction is add a new score
            {
                lFabOk.Click += delegate
                {
                    if (ScoreOk(lListScore))
                    {
                        SaveScores(lListScore); //This method add score in scoring list.
                        ShowScore(); //This method show or refresh the scoring list in the view
                        dlg.Dismiss();
                    }
                };

            }
            dlg.Show();
        }

        //Set values in edit action
        protected void SetEditValues(IList<EditText> pListEd)
        {
            for (int i=0; i<FCantPlayers; i++)
            {
                pListEd[i].Text = FScrSelect.GetScoring(i);
            }
        }
        //I obtain amount players selected and hide the editTexts left over
        protected void HidePlayers(List<EditText> pListScore)
        {
            for (int i = FCantPlayers; i < 6; i++)
                pListScore[i].Visibility = ViewStates.Gone;
        }

        //This method add score in scoring list
        protected void SaveScores(List<EditText> pListScore)
        {
            listScoring.Add(ObtainScore(pListScore)); //Obtain a scoring and add a listScoring.
        }

        //this method check if the score is o not empty.
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
        
        //add the score (object score) in scoring list. 
        protected Scoring ObtainScore(List<EditText> pListScore)
        {
            List<string> lAux = new List<string>(); //create a aux list
            for (int i = 0; i < 6; i++)
                if (pListScore[i].Text != string.Empty) 
                    lAux.Add(pListScore[i].Text); 
                else
                    lAux.Add("#"); // if score is "#" (empty) then not show
            return new Scoring(lAux);
        }

        protected void EditScore()
        {
            try
            {
                if (FPosItemSelect == -1)
                {
                    Toast msg = Toast.MakeText(this, GetString(Resource.String.scoringList_SelectItemEdit), ToastLength.Long);
                    msg.Show();
                    msg = null;
                }
                else
                {
                    FScrSelect = listScoring[FPosItemSelect];
                    DlgScore(Const.Const_Action_Edit);
                }
            }
            catch (Exception e)
            {
                AlertDialog.Builder dlgAlert = new AlertDialog.Builder(this);
                dlgAlert.SetTitle(GetString(Resource.String.titleError));
                dlgAlert.SetMessage(GetString(Resource.String.msgError) + e.Message);
                dlgAlert.SetCancelable(true);
                dlgAlert.SetPositiveButton("Aceptar", delegate { dlgAlert.Dispose(); });
                dlgAlert.Create();
                dlgAlert.Show();
            }
            
        }

        protected void DeleteScore()
        {
            try
            {
                 if (FPosItemSelect == -1)
                 {
                     Toast msg = Toast.MakeText(this, GetString(Resource.String.scoringList_SelectItemDelete), ToastLength.Long);
                     msg.Show();
                     msg = null;
                 }
                 else
                 {
                     listScoring.RemoveAt(FPosItemSelect);
                     ShowScore();
                 } 
                listScoring.RemoveAt(FPosItemSelect);
           }
           catch (Exception e)
            {
                AlertDialog.Builder dlgAlert = new AlertDialog.Builder(this);
                dlgAlert.SetTitle(GetString(Resource.String.titleError));
                dlgAlert.SetMessage(GetString(Resource.String.msgError) + e.Message);
                dlgAlert.SetCancelable(true);
                dlgAlert.SetPositiveButton("Aceptar", delegate { dlgAlert.Dispose(); });
                dlgAlert.Create();
                dlgAlert.Show();
            }
            
        }
        
        //This method add score in EditText.
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
                        //added all score in pos "i", It's added by column
                        acumScore = acumScore + int.Parse(src.GetScoring(i));
                }
                listFinalScoresEdt[i].Text = acumScore.ToString();
                acumScore = 0;
            }
            src = null;
        }

        //This method show scoring list in listView and adds the score in the editText
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

        protected bool CheckMaxScore()
        {
            return true;
        }

    }
}