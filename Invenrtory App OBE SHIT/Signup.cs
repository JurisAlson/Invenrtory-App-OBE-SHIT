using System;
using System.IO;
using System.Net;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using Invenrtory_App_OBE_SHIT;

namespace Inventory_App_OBE_SHIT
{
    [Activity(Label = "Signup", Theme = "@style/AppTheme.NoActionBar")]
    public class Signup : AppCompatActivity
    {
        EditText txtUsername, txtPassword, txtConfirmPassword;
        Button btnSignup;
        ImageView imageView1;

        HttpWebResponse response;
        HttpWebRequest request;


        String uname = "", pword = "", res = "", cpword = "";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.signUp);
           
           
            txtUsername = FindViewById<EditText>(Resource.Id.txtUsername);
            txtPassword = FindViewById<EditText>(Resource.Id.txtPassword);
            txtConfirmPassword = FindViewById<EditText>(Resource.Id.txtConfirmPassword);
            btnSignup = FindViewById<Button>(Resource.Id.btnSignup);

            imageView1 = FindViewById<ImageView>(Resource.Id.imageView1);
            imageView1.SetImageResource(Resource.Drawable.StashFlowNew);

            btnSignup.Click += BtnSignup_Click;
        }

        private void BtnSignup_Click(object sender, EventArgs e)
        {
            uname = txtUsername.Text;
            pword = txtPassword.Text;
            cpword = txtConfirmPassword.Text;
            

            if (string.IsNullOrEmpty(uname) || string.IsNullOrEmpty(pword) || string.IsNullOrEmpty(cpword))
            {
                Toast.MakeText(this, "Username, password, and confirm password are required", ToastLength.Short).Show();
                return; 
            }

            if (pword != cpword)
            {
                Toast.MakeText(this, "Passwords do not match", ToastLength.Short).Show();
                return; 
            }

            try
            {
                request = (HttpWebRequest)WebRequest.Create("http://192.168.18.11/INVENTORY/REST/add_accountMP.php?uname=" + uname + "&pword=" + pword);
                response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    res = reader.ReadToEnd();
                }
                Toast.MakeText(this, res, ToastLength.Long).Show();
                if (res.Contains("Sign up successful! Please log in."))
                {
                    Intent i = new Intent(this, typeof(Login));
                    StartActivity(i);
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "Error occurred: " + ex.Message, ToastLength.Short).Show();
            }

        }

    }
}
