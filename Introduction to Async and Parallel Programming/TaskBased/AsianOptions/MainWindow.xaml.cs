﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;


namespace AsianOptions
{

	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		//
		// Methods:
		//
		public MainWindow()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Exit the app.
		/// </summary>
		private void mnuFileExit_Click(object sender, RoutedEventArgs e)
		{
			this.Close();  // trigger "closed" event as if user had hit "X" on window:
		}

		/// <summary>
		/// Saves the contents of the list box.
		/// </summary>
		private void mnuFileSave_Click(object sender, RoutedEventArgs e)
		{
			using (StreamWriter file = new StreamWriter("results.txt"))
			{
				foreach (string item in this.lstPrices.Items)
					file.WriteLine(item);
			}
		}

	    private int runCounter = 0;
		/// <summary>
		/// Main button to run the simulation.
		/// </summary>
		private void cmdPriceOption_Click(object sender, RoutedEventArgs e)
		{
			//this.cmdPriceOption.IsEnabled = false;
		    runCounter ++;
		    LblCounter.Content = runCounter.ToString(CultureInfo.InvariantCulture);

            if(runCounter > 0)
		    {
		        this.spinnerWait.Visibility = System.Windows.Visibility.Visible;
		        this.spinnerWait.Spin = true;
		    }
		    double initial = Convert.ToDouble(txtInitialPrice.Text);
			double exercise = Convert.ToDouble(txtExercisePrice.Text);
			double up = Convert.ToDouble(txtUpGrowth.Text);
			double down = Convert.ToDouble(txtDownGrowth.Text);
			double interest = Convert.ToDouble(txtInterestRate.Text);
			long periods = Convert.ToInt64(txtPeriods.Text);
			long sims = Convert.ToInt64(txtSimulations.Text);

			//
			// Run simulation to price option:
			//
		    string result = string.Empty;
            Task calcTask = Task.Factory.StartNew(()=>
		    {
		        Random rand = new Random();
		        int start = System.Environment.TickCount;

		        double price = AsianOptionsPricing.Simulation(rand, initial, exercise, up, down, interest, periods, sims);

		        int stop = System.Environment.TickCount;

		        double elapsedTimeInSecs = (stop - start)/1000.0;

		        result = string.Format("{0:C}  [{1:#,##0.00} secs]",
		            price, elapsedTimeInSecs);
		    });
		    //
			// Display the results:
			//
		    Task displayTask = calcTask.ContinueWith((antecedent) =>
		                                             {
		                                                 this.lstPrices.Items.Insert(0, result);

		                                                 this.spinnerWait.Spin = false;
		                                                 this.spinnerWait.Visibility =
		                                                     System.Windows.Visibility.Collapsed;
		                                                 runCounter--;
                                                         LblCounter.Content = runCounter.ToString(CultureInfo.InvariantCulture);
		                                                 //this.cmdPriceOption.IsEnabled = true;
		                                             }, TaskScheduler.FromCurrentSynchronizationContext());
		}

	}//class
}//namespace
