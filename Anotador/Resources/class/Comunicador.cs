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

namespace Anotador
{

    class Comunicador
    {
        private Object myObject { get; set; } 
        private static Comunicador unique;

        private Comunicador(Object pObj)
        {
            myObject = pObj;
        }

        public static Comunicador GetSingletonInstance(Object pObj)
        {
            if (unique == null)
                unique = new Comunicador(pObj);
            return unique;
        }
    }
}