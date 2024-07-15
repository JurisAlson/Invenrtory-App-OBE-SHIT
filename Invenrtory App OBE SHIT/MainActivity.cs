using System;
using System.Net;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.AppCompat.Widget;
using AndroidX.Core.View;
using AndroidX.DrawerLayout.Widget;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Navigation;
using Google.Android.Material.Snackbar;
using Inventory_App_OBE_SHIT;
using SearchView = AndroidX.AppCompat.Widget.SearchView;
using Toolbar = AndroidX.AppCompat.Widget.Toolbar;

namespace Invenrtory_App_OBE_SHIT
{
    [Activity(Label = "MainActivity", Theme = "@style/AppTheme.NoActionBar")]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        private SearchView searchView1;
        TextView txtWelcome;
        HttpWebResponse response;
        HttpWebRequest request;
        String name = "", school = "", country = "", selectedGender = "", res = "", str = "", login_name = "";
        
       
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.activity_main);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout); 
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);


            searchView1 = FindViewById<SearchView>(Resource.Id.searchView1);
            searchView1.QueryHint = "Search";
            searchView1.QueryTextSubmit += SearchView1_QueryTextSubmit;
        }

        private void SearchView1_QueryTextSubmit(object sender, SearchView.QueryTextSubmitEventArgs e)
        {
            Toast.MakeText(this, $"Submitted query: {e}", ToastLength.Short).Show();
            Log.Debug("SearchView", $"Query submitted: {e}");
            e.Handled = true;
        }

        //public override void OnBackPressed()
        //{
        //    DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
        //    if (drawer.IsDrawerOpen(GravityCompat.Start))
        //    {
        //        drawer.CloseDrawer(GravityCompat.Start);
        //    }
        //    else
        //    {
        //        base.OnBackPressed();
        //    }
        //}

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            Intent intent = new Intent(this, typeof(Form));
            StartActivity(intent);
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;

            if (id == Resource.Id.nav_profile)
            {
                Intent intent = new Intent(this, typeof(ProfileActivity));
                StartActivity(intent);
            }
            else if (id == Resource.Id.nav_menu)
            {

            }
            else if (id == Resource.Id.nav_items)
            {

            }
            else if (id == Resource.Id.nav_category)
            {

            }
            else if (id == Resource.Id.nav_settings)
            {
                
            }
            else if (id == Resource.Id.nav_logout)
            {
                LogoutUser();
            }

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }
        

        private void LogoutUser()
        {
            Intent intent = new Intent(this, typeof(Login));
            intent.SetFlags(ActivityFlags.ClearTask | ActivityFlags.NewTask);
            StartActivity(intent);
            Finish(); 
        }

    }
}