namespace maui_db
{

    
    public partial class MainPage : ContentPage
    {
        string username = "";

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
             
            username = NameEntry.Text;

            if (username == "admin")
            {
                db_class.create_db();
                //db_class.add_data();
                List<String> entries = db_class.get_data();
                foreach (var entry in entries)
                {
                    DisplayAlert("Alert", entry, "OK");
                }

                DisplayAlert("Alert", "No more data in table", "OK");

                db_class.delete_data("Original data .. ");
            }
            else
            {
                DisplayAlert("Alert", "You are not admin", "OK");
            }
            
        }
    }

}
