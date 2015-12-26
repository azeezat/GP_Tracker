using GP_Tracker.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace GP_Tracker
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GPCalculator : Page
    {
        Student mystudent = new Student();
        public TextBlock no;
        public TextBox cs;
        public ComboBox gr;
        public TextBox un;
        int rows = 0;
        int unt, pts = 0;
        int i;
        int allcrses;


        MainPage mymainpage = new MainPage();

        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public GPCalculator()
        {
            this.InitializeComponent();

            mystudent.units = 0;
            mystudent.weights = 0;
            mystudent.gp = 0;
            
            //Bind Enum to combo box
            this.DataContext = new Student.Grade();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Gets the view model for this <see cref="Page"/>.
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
     
        #endregion

        public void doCalculations()
        {
            ++rows;
                unt = Convert.ToInt32(un.Text);
                pts = Convert.ToInt32(gr.SelectedIndex);

                mystudent.units = mystudent.units + unt;
                mystudent.weights = mystudent.weights + (unt * pts);

                mystudent.gp = mystudent.weights / mystudent.units;
            
            
                if (mystudent.units < 1)
                {
                    //("Enter a Value greater than 1");
                }
        }


        public void AddNewTextBox()
        {
            ++rows;


            no = new TextBlock();
            cs = new TextBox();
            gr = new ComboBox();
            un = new TextBox();

            no.Width = 30;
            //no.Name = "no"+ rows.ToString();
            no.FontSize = 20;
            no.Text = rows.ToString();
            no.VerticalAlignment = VerticalAlignment.Center;
            no.TextAlignment = TextAlignment.Center;
            Grid.SetRow(no, rows);
            Grid.SetColumn(no, 0);


            cs.Width = 130;
            //cs.Name = "cc" + rows.ToString();
            cs.FontSize = 20;
            cs.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetRow(cs, rows);
            Grid.SetColumn(cs, 1);



            gr.ItemsSource = Enum.GetValues(typeof(Student.Grade)).Cast<Student.Grade>();
            //gr.Name = "gr"+ rows.ToString();
            gr.Width = 70;
            gr.FontSize = 20;
            gr.PlaceholderText = "Grade";
            gr.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetRow(gr, rows);
            Grid.SetColumn(gr, 2);


            InputScope scope = new InputScope();
            InputScopeName name = new InputScopeName();
            name.NameValue = InputScopeNameValue.Number;
            scope.Names.Add(name);


            un.Width = 70;
            //un.Name = "un" + rows.ToString();
            un.FontSize = 20;
            un.TextAlignment = TextAlignment.Center;
            un.VerticalAlignment = VerticalAlignment.Center;
            Grid.SetRow(un, rows);
            Grid.SetColumn(un, 3);
            un.InputScope = scope;



            ContentPanel.Children.Add(no);
            ContentPanel.Children.Add(cs);
            ContentPanel.Children.Add(gr);
            ContentPanel.Children.Add(un);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var u = e.Parameter.ToString();
            allcrses = Convert.ToInt32(u);

            for (i = 0; i < allcrses; i++)
            {
                AddNewTextBox();
            }
        }


        private async void calculate_Click(object sender, RoutedEventArgs e)
        {

            if (un.Text == "" || gr.SelectedIndex == -1)
            {
                MessageDialog msgbox = new MessageDialog("Fill the empty grade and units!");
                await msgbox.ShowAsync();
                //Ask the user to fill the empty grade and units
            }
            else
            {
                doCalculations();
                this.Frame.Navigate(typeof(Result), mystudent);
            }
        }


    }
}
