﻿using Bib.Klassen;
using Biblio_test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bib
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupViewBorrow 
    {
        private Student studenttemp;

        public PopupViewBorrow(Student student)
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.Android) { 
                this.BindingContext = new BookListViewModel();
                upbutton.IsVisible = false;
            }
            studenttemp = student;
        }

        private void SearchBar_Book(object sender, TextChangedEventArgs e)
        {
            var _container = BindingContext as BookListViewModel;
            BookListView.BeginRefresh();

            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                BookListView.ItemsSource = _container.Books;
            else
                BookListView.ItemsSource = _container.Books.Where(i => i.Titel.ToLower().Contains(e.NewTextValue.ToLower()));

            BookListView.EndRefresh();
        }

        private void registrieren_Clicked(object sender, EventArgs e)
        {
            Buch buch = BookListView.SelectedItem as Buch;

            Button b = (Button)sender;

            switch (b.Text)
            {
                case("Bücher anzeigen"):
                    this.BindingContext = new BookListViewModel();
                    // hier sollte andere Konstruktur aufgerufen werden 
                    // this.BindingContext = new BookListViewModel(studenttemp);
                    break;
                case ("Ausleihen"):
                    if(studenttemp != null && buch != null)
                        DisplayAlert("Alert","Dieses Buch\n("+buch.ToString()+")\nmuss zu diesem Student\n("+studenttemp.ToString()+")\nhinzugefügt werden", "OK");
                    break;
            }
        }
    }
}