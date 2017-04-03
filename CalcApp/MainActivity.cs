using System;
using System.Collections.Generic;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using Java.Lang;
using Java.Security;
using RPN;

namespace CalcApp
{
    [Activity(MainLauncher = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class MainActivity : Activity
    {
        private HorizontalScrollView _hsv;
        private TextView _primaryDisplay;
        private TextView _secondaryDisplay;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            _hsv = FindViewById<HorizontalScrollView>(Resource.Id.display_hsv);
            _primaryDisplay = FindViewById<TextView>(Resource.Id.display_primary);
            _secondaryDisplay = FindViewById<TextView>(Resource.Id.display_secondary);

            GetDigits().ForEach(x => x.Click += (sender, args) =>
            {
                _primaryDisplay.Text += x.Text;
            });
        }


        private List<TextView> GetDigits()
        {
            return new List<TextView>
            {
                FindViewById<TextView>(Resource.Id.button_0),
                FindViewById<TextView>(Resource.Id.button_1),
                FindViewById<TextView>(Resource.Id.button_2),
                FindViewById<TextView>(Resource.Id.button_3),
                FindViewById<TextView>(Resource.Id.button_4),
                FindViewById<TextView>(Resource.Id.button_5),
                FindViewById<TextView>(Resource.Id.button_6),
                FindViewById<TextView>(Resource.Id.button_7),
                FindViewById<TextView>(Resource.Id.button_8),
                FindViewById<TextView>(Resource.Id.button_9)
            };
        }


    }

     
}

