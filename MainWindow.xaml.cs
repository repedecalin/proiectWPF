using System;
using System.Collections.Generic;
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
using Turism_Model;
using System.Data.Entity;
using System.Data;

namespace proiect2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    enum ActionState
    {
        New,
        Edit,
        Delete,
        Nothing
    }
    public partial class MainWindow : Window
    {
        ActionState action = ActionState.Nothing;
        Turism_Entity_Model ctx = new Turism_Entity_Model();
        CollectionViewSource touristVSource;
        CollectionViewSource routeVSource;
        CollectionViewSource appointmentVSource;
        CollectionViewSource guideVSource;
        CollectionViewSource souvenirVSource;
        CollectionViewSource orderVSource;
        public MainWindow()
        {

            InitializeComponent();
            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource touristViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("touristViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // touristViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource routeViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("routeViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // routeViewSource.Source = [generic data source]
            touristVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("touristViewSource")));
            touristVSource.Source = ctx.Tourists.Local;
            ctx.Tourists.Load();

            routeVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("routeViewSource")));
            routeVSource.Source = ctx.Routes.Local;
            ctx.Routes.Load();

            appointmentVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("appointmentViewSource")));
            appointmentVSource.Source = ctx.Appointments.Local;
            ctx.Appointments.Load();

            guideVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("guideViewSource")));
            guideVSource.Source = ctx.Guides.Local;
            ctx.Guides.Load();

            souvenirVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("souvenirViewSource")));
            souvenirVSource.Source = ctx.Souvenirs.Local;
            ctx.Souvenirs.Load();

            orderVSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("orderViewSource")));
            orderVSource.Source = ctx.Orders.Local;
            ctx.Orders.Load();
            System.Windows.Data.CollectionViewSource appointmentViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("appointmentViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // appointmentViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource guideViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("guideViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // guideViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource souvenirViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("souvenirViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // souvenirViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource orderViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("orderViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // orderViewSource.Source = [generic data source]
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.New;
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Edit;
            BindingOperations.ClearBinding(first_nameTextBox, TextBox.TextProperty);
            BindingOperations.ClearBinding(last_nameTextBox, TextBox.TextProperty);
           
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            action = ActionState.Delete;
        }
        private void SaveTourists()
        {
            Tourist tourist = null;
            if (action == ActionState.New)
            {
                try
                {
                    //instantiem Tourist entity
                    tourist = new Tourist()
                    {
                        first_name = first_nameTextBox.Text.Trim(),
                        last_name = last_nameTextBox.Text.Trim(),
                        phone = phoneTextBox.Text.Trim(),
                        email = emailTextBox.Text.Trim()
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Tourists.Add(tourist);
                   touristVSource.View.Refresh();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                //using System.Data;
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
           if (action == ActionState.Edit)
            {
                try
                {
                    tourist = (Tourist)touristDataGrid.SelectedItem;
                    tourist.first_name = first_nameTextBox.Text.Trim();
                    tourist.last_name = last_nameTextBox.Text.Trim();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    tourist = (Tourist)touristDataGrid.SelectedItem;
                    ctx.Tourists.Remove(tourist);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                touristVSource.View.Refresh();
            }

        }
        private void SaveRoutes()
        {
            Route route = null;
            if (action == ActionState.New)
            {
                try
                {
                    //instantiem Route entity
                    route = new Route()
                    {
                        id_guide = int.Parse(id_guideTextBox.Text.Trim()),
                        description = descriptionTextBox.Text.Trim(),
                        duration = durationTextBox.Text.Trim()
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Routes.Add(route);
                    routeVSource.View.Refresh();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                //using System.Data;
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
           if (action == ActionState.Edit)
            {
                try
                {
                    route = (Route)routeDataGrid.SelectedItem;
                    route.description = descriptionTextBox.Text.Trim();
                    route.duration = durationTextBox.Text.Trim();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    route = (Route)routeDataGrid.SelectedItem;
                    ctx.Routes.Remove(route);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                routeVSource.View.Refresh();
            }

        }
        private void SaveAppointments()
        {
            Appointment app = null;
            if (action == ActionState.New)
            {
                try
                {
                    //instantiem Appointment entity
                    app = new Appointment()
                    {
                        id_route = int.Parse(id_routeTextBox1.Text.Trim()),
                        id_tourist = int.Parse(id_touristTextBox1.Text.Trim()),
                        date = Convert.ToDateTime(dateDatePicker.Text.Trim()),
                        hour = TimeSpan.Parse(hourTextBox.Text.Trim())
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Appointments.Add(app);
                    appointmentVSource.View.Refresh();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                //using System.Data;
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
           if (action == ActionState.Edit)
            {
                try
                {
                    app = (Appointment)appointmentDataGrid.SelectedItem;
                    app.id_route = int.Parse(id_routeTextBox1.Text.Trim());
                    app.id_tourist = int.Parse(id_touristTextBox1.Text.Trim());
                    app.hour = TimeSpan.Parse(hourTextBox.Text.Trim());
                    app.date = Convert.ToDateTime(dateDatePicker.Text.Trim());
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    app = (Appointment)appointmentDataGrid.SelectedItem;
                    ctx.Appointments.Remove(app);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                appointmentVSource.View.Refresh();
            }

        }

        private void SaveGuides()
        {
            Guide guide = null;
            if (action == ActionState.New)
            {
                try
                {
                    //instantiem Guide entity
                    guide = new Guide()
                    {
                        first_name = first_nameTextBox1.Text.Trim(),
                        last_name = last_nameTextBox1.Text.Trim(),
                        phone = phoneTextBox1.Text.Trim(),
                        email = emailTextBox1.Text.Trim()
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Guides.Add(guide);
                    guideVSource.View.Refresh();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                //using System.Data;
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
           if (action == ActionState.Edit)
            {
                try
                {
                    guide = (Guide)guideDataGrid.SelectedItem;
                    guide.first_name = first_nameTextBox1.Text.Trim();
                    guide.last_name = last_nameTextBox1.Text.Trim();
                    guide.phone = phoneTextBox1.Text.Trim();
                    guide.email = emailTextBox1.Text.Trim();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    guide = (Guide)guideDataGrid.SelectedItem;
                    ctx.Guides.Remove(guide);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                guideVSource.View.Refresh();
            }

        }

        private void SaveSouvenirs()
        {
            Souvenir souvenir = null;
            if (action == ActionState.New)
            {
                try
                {
                    //instantiem souvenir entity
                    souvenir = new Souvenir()
                    {
                        available_quanity= int.Parse(available_quanityTextBox.Text.Trim()),
                        description = descriptionTextBox1.Text.Trim(),
                        name = nameTextBox.Text.Trim(),
                        price = decimal.Parse(priceTextBox.Text.Trim())
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Souvenirs.Add(souvenir);
                    souvenirVSource.View.Refresh();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                //using System.Data;
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
           if (action == ActionState.Edit)
            {
                try
                {
                    souvenir = (Souvenir)souvenirDataGrid.SelectedItem;
                    souvenir.name = nameTextBox.Text.Trim();
                    souvenir.price = decimal.Parse(priceTextBox.Text.Trim());
                    souvenir.description = descriptionTextBox1.Text.Trim();
                    souvenir.available_quanity = int.Parse(available_quanityTextBox.Text.Trim());
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    souvenir = (Souvenir)souvenirDataGrid.SelectedItem;
                    ctx.Souvenirs.Remove(souvenir);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                souvenirVSource.View.Refresh();
            }

        }

        private void SaveOrders()
        {
            Order order = null;
            if (action == ActionState.New)
            {
                try
                {
                    //instantiem Order entity
                    order = new Order()
                    {
                        id_souvenir = int.Parse(id_souvenirTextBox1.Text.Trim()),
                        id_tourist = int.Parse(id_touristTextBox2.Text.Trim()),
                        quantity = int.Parse(quantityTextBox.Text.Trim())
                        
                    };
                    //adaugam entitatea nou creata in context
                    ctx.Orders.Add(order);
                    orderVSource.View.Refresh();
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                //using System.Data;
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
           if (action == ActionState.Edit)
            {
                try
                {
                    order = (Order)orderDataGrid.SelectedItem;
                    order.id_souvenir = int.Parse(id_souvenirTextBox1.Text.Trim());
                    order.id_tourist = int.Parse(id_touristTextBox2.Text.Trim());
                    order.quantity = int.Parse(quantityTextBox.Text.Trim());
                    //salvam modificarile
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (action == ActionState.Delete)
            {
                try
                {
                    order = (Order)orderDataGrid.SelectedItem;
                    ctx.Orders.Remove(order);
                    ctx.SaveChanges();
                }
                catch (DataException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                orderVSource.View.Refresh();
            }

        }


        private void gbOperations_Click(object sender, RoutedEventArgs e)
        {
            Button SelectedButton = (Button)e.OriginalSource;
            Panel panel = (Panel)SelectedButton.Parent;

            foreach (Button B in panel.Children.OfType<Button>())
            {
                if (B != SelectedButton)
                    B.IsEnabled = false;
            }
            gbActions.IsEnabled = true;
        }
        private void ReInitialize()
        {

            Panel panel = gbOperations.Content as Panel;
            foreach (Button B in panel.Children.OfType<Button>())
            {
                B.IsEnabled = true;
            }
            gbActions.IsEnabled = false;
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ReInitialize();
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            TabItem ti = TabCtrl.SelectedItem as TabItem;

            switch (ti.Header)
            {
                case "Tourists":
                    SaveTourists();
                    break;
                case "Routes":
                    SaveRoutes();
                    break;
                case "Appointments":
                    SaveAppointments();
                    break;
                case "Guides":
                    SaveGuides();
                    break;
                case "Souvenirs":
                    SaveSouvenirs();
                    break;
                case "Orders":
                    SaveOrders();
                    break;
            }
            ReInitialize();
        }
       

    }
    


}
