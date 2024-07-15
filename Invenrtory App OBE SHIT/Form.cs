using System;
using System.IO;
using System.Net;
using Android.App;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using static Android.Provider.UserDictionary;

namespace Invenrtory_App_OBE_SHIT
{
    [Activity(Label = "Inventory Form")]
    public class Form : AppCompatActivity
    {
        private EditText Item, itemQuantity, Description;
        private Button buttonSubmit;

        HttpWebResponse response;
        HttpWebRequest request;

        string item = "", quantity = "", description = "", res = "";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.Form_main);

            // Bind the widgets
            Item = FindViewById<EditText>(Resource.Id.Item);
            itemQuantity = FindViewById<EditText>(Resource.Id.itemQuantity);
            Description = FindViewById<EditText>(Resource.Id.Description);
            buttonSubmit = FindViewById<Button>(Resource.Id.buttonSubmit);

            buttonSubmit.Click += SubmitClick;
        }

        public void SubmitClick(object sender, EventArgs e)
        {
            item = Item.Text;
            quantity = itemQuantity.Text;
            description = Description.Text;

            // Validate the input data
            if (string.IsNullOrWhiteSpace(item) || string.IsNullOrWhiteSpace(quantity) || string.IsNullOrWhiteSpace(description))
            {
                Toast.MakeText(this, "Please fill in all fields!", ToastLength.Short).Show();
                return;
            }

            if (!int.TryParse(quantity, out int quantityValue))
            {
                Toast.MakeText(this, "Invalid quantity. Please enter a valid number.", ToastLength.Short).Show();
                return;
            }

            try
            {
                request = (HttpWebRequest)WebRequest.Create("http://192.168.1.2/INVENTORY/REST/add_record.php?item=" + item + "&quantity=" + quantity + "&description=" + description);
                response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    res = reader.ReadToEnd();
                }
                Toast.MakeText(this, "Item added successfully!", ToastLength.Short).Show();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "Error occurred: " + ex.Message, ToastLength.Short).Show();
            }
        }
    }
}
