using Android.App;
using Android.OS;
using Android.Widget;
using AndroidX.AppCompat.App;
using Invenrtory_App_OBE_SHIT;
using System;

namespace Inventory_App_OBE_SHIT
{
    [Activity(Label = "Inventory Form")]
    public class Form : AppCompatActivity
    {
        private EditText Item, itemQuantity, Description;
        private Button buttonSubmit;
        private TextView txtItemName, txtQuantity, txtDescription;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.Form_main);

            // Bind the EditText and Button widgets
            Item = FindViewById<EditText>(Resource.Id.Item);
            itemQuantity = FindViewById<EditText>(Resource.Id.itemQuantity);
            Description = FindViewById<EditText>(Resource.Id.Description);
            buttonSubmit = FindViewById<Button>(Resource.Id.buttonSubmit);

            // Check if buttonSubmit is not null before assigning the Click event
            if (buttonSubmit != null)
            {
                buttonSubmit.Click += SubmitClick;
            }
            else
            {
                // Log or handle the error appropriately
                Toast.MakeText(this, "Button not found in layout", ToastLength.Short).Show();
            }

            // Bind the TextViews
            txtItemName = FindViewById<TextView>(Resource.Id.txtItemName);
            txtQuantity = FindViewById<TextView>(Resource.Id.txtQuantity);
            txtDescription = FindViewById<TextView>(Resource.Id.txtDescription);
        }

        public void SubmitClick(object sender, EventArgs e)
        {
            string item = Item.Text;
            string quantity = itemQuantity.Text;
            string description = Description.Text;

            // Validate the input data
            if (string.IsNullOrWhiteSpace(item) || string.IsNullOrWhiteSpace(quantity) || string.IsNullOrWhiteSpace(description))
            {
                Toast.MakeText(this, "Please fill in all fields!", ToastLength.Short).Show();
                return;
            }

            if (!int.TryParse(quantity, out _))
            {
                Toast.MakeText(this, "Invalid quantity. Please enter a valid number.", ToastLength.Short).Show();
                return;
            }

            // Display the submitted data
            txtItemName.Text = $"Item Name: {item}";
            txtQuantity.Text = $"Price: {quantity}";
            txtDescription.Text = $"Description: {description}";

            // Optionally clear the EditText fields after submission
            Item.Text = "";
            itemQuantity.Text = "";
            Description.Text = "";

            Toast.MakeText(this, "Form submitted successfully!", ToastLength.Short).Show();
        }
    }
}
