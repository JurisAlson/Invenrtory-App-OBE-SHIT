using System;
using System.IO;
using System.Net;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using Inventory_App_OBE_SHIT;


namespace Invenrtory_App_OBE_SHIT
{
    [Activity(Label = "Inventory App", Theme = "@style/AppTheme", MainLauncher = true)]
    public class Login : AppCompatActivity
    {
        TextView textView1;
        EditText txtUsername, txtPassword;
        Button btnLogin, btnCreate;
        ImageView imageView1;
        HttpWebResponse response;
        HttpWebRequest request;
        string res = "", str = "", uname = "", pword = "";



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.login);

            //Binding widgets
            textView1 = FindViewById<TextView>(Resource.Id.textView1);
            txtUsername = FindViewById<EditText>(Resource.Id.txtUsername);
            txtPassword = FindViewById<EditText>(Resource.Id.txtPassword);
            btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
            btnCreate = FindViewById<Button>(Resource.Id.btnCreate);

            imageView1 = FindViewById<ImageView>(Resource.Id.imageView1);
            imageView1.SetImageResource(Resource.Drawable.StashFlowNew);

            btnLogin.Click += this.LoginClick;
            btnCreate.Click += this.CreateAccountClick;

        }

        public void LoginClick(object sender, EventArgs e)
        {

            uname = txtUsername.Text;
            pword = txtPassword.Text;

            //Check if username or password is empty
            if (string.IsNullOrEmpty(uname) || string.IsNullOrEmpty(pword))
            {
                Toast.MakeText(this, "Username and password are required", ToastLength.Short).Show();
                return;
            }

            try
            {
                request = (HttpWebRequest)WebRequest.Create("http://192.168.18.11/INVENTORY/REST/admin_loginMP.php?uname=" + uname + "&pword=" + pword);
                response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    res = reader.ReadToEnd();
                }
                Toast.MakeText(this, res, ToastLength.Long).Show();

                if (res.Contains("Welcome!"))
                {
                    Intent i = new Intent(this, typeof(MainActivity));
                    i.PutExtra("Name", uname);
                    StartActivity(i);
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "Error occurred: " + ex.Message, ToastLength.Short).Show();
            }
        }
        private void CreateAccountClick(object sender, EventArgs eventArgs)
        {
            Intent intent = new Intent(this, typeof(Signup));
            StartActivity(intent);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
