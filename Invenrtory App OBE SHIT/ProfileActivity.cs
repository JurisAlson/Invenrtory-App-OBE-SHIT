using System;
using System.Net;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;



namespace Invenrtory_App_OBE_SHIT
{
    [Activity(Label = "Profile")]
    public class ProfileActivity : Activity
    {
        TextView txtName, txtGender;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.profile);

            
            txtName = FindViewById<TextView>(Resource.Id.txtName);
            txtGender = FindViewById<TextView>(Resource.Id.txtGender);

            
            var sharedPreferences = GetSharedPreferences("UserProfile", FileCreationMode.Private);
            string name = sharedPreferences.GetString("Name", "");
            string gender = sharedPreferences.GetString("Gender", "");

            
            txtName.Text = $"Name: {name}";
            txtGender.Text = $"Gender: {gender}";
        }
    }
}
