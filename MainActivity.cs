using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;
using System.Xml.Serialization;
using System.IO;
using System.Data;

namespace AppBusquedaTel
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            var btnBuscar = FindViewById<Button>(Resource.Id.btnBuscar);
            var txtTelefono = FindViewById<EditText>(Resource.Id.txtBuscarTelefono);
            var lblNombre = FindViewById<TextView>(Resource.Id.lblNombre);
            var lblApellido = FindViewById<TextView>(Resource.Id.lblApellido);
            var lblSaldo = FindViewById<TextView>(Resource.Id.lblSaldo);

            btnBuscar.Click += delegate
            {
                lblNombre.Text = "";
                lblApellido.Text = "";
                lblSaldo.Text = "";
                var conjunto = new DataSet();
                DataRow Renglon;
                try
                {
                    var WS = new ServicioWeb.ServicioWeb();
                    conjunto = WS.Buscar(long.Parse(txtTelefono.Text));
                    Renglon = conjunto.Tables["Telefonia"].Rows[0];
                    lblNombre.Text = Renglon["Nombre"].ToString();
                    lblApellido.Text = Renglon["Apellido"].ToString();
                    lblSaldo.Text = Renglon["Saldo"].ToString();
                }
                catch (System.Exception ex)
                {
                    Toast.MakeText(this, ex.Message,
                     ToastLength.Long).Show();
                }
            };
        }

    }
}