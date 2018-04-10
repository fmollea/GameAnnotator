using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace Anotador
{

    public class ListAdapter : BaseAdapter
    {
        Activity context;
        public List<Scoring> items;

        public ListAdapter(Activity context, List<Scoring> items) : base ()
        {
            this.context = context;
            this.items = items;
        }

        public override int Count
        {
            get { return items.Count; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            List<TextView> listTxt = new List<TextView>();
            var item = items[position];
            var view = (convertView ?? context.LayoutInflater.Inflate(Resource.Layout.ItemList,
                parent, false)) as LinearLayout;


            listTxt.Add(view.FindViewById(Resource.Id.tScore1) as TextView);
            listTxt.Add(view.FindViewById(Resource.Id.tScore2) as TextView);
            listTxt.Add(view.FindViewById(Resource.Id.tScore3) as TextView);
            listTxt.Add(view.FindViewById(Resource.Id.tScore4) as TextView);
            listTxt.Add(view.FindViewById(Resource.Id.tScore5) as TextView);
            listTxt.Add(view.FindViewById(Resource.Id.tScore6) as TextView);

            //if score player is -1 then these player is not gaming
            for (int i=0; i<6; i++)
            {
                SetValue(listTxt[i], item.Score[i]);
            }

            return view;
        }

        public Scoring GetItemAtPosition(int position)
        {
            return items[position];
        }

        //if score player is # then these player is not gaming
        protected void SetValue(TextView pItemScore, System.String pScore)
        {
            if (pScore != "#")
                pItemScore.SetText(pScore, TextView.BufferType.Normal);
            else
            {
                pItemScore.SetText("0", TextView.BufferType.Normal);
                pItemScore.Visibility = ViewStates.Gone;
            }
        }
    }
} 