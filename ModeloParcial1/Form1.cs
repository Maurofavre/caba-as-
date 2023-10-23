using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModeloParcial1
{
    /*
    Datos para el cálculo de la reparación
    Tipos de dispositivo a mostrar en el ComboBox: Smartphone, Tablet y Consola
    costo base: smartphone $5500, tablet $4850 y consola $9000
    los dias de plazo de entrega varian en función del dispositivo seleccionado:
    smartphone: entre 2 y 10 días (mostrar 2, 3, 4, 5, 6, 7, 8, 9 y 10)
    tablet: entre 1 y 7 días (mostrar 1, 2, 3, 4, 5, 6 y 7)
    consola: entre 5 y 12 días (mostrar 5, 6, 7, 8, 9, 10, 11 y 12)
    Opcionales: Actualizar software $1500, Extender la garantía $1000

    Forma de pago: Crédito: se agrega un 5% de recargo al precio total
    si es Contado se descuenta un 10% del precio total.
    El botón calcular se habilita solamente cuando se ingrese el nombre del cliente
    -En el evento del botón se realizará el cálculo y se mostrará su resultado en el TextBox "txtTotal", 
     además se almacenará en un arreglo de estructura REPARACION estos datos:
      - Cliente      string
      - Dispositivo  string
      - Importe      float
     El arreglo para almacenar las reparaciones tiene capacidad para 20 registros

    El botón Reporte mostrará en un cuadro de mensaje (MessageBox) la suma de todos los importes
    registrados en el arreglo.
     */

    public partial class frmService : Form
    {
        const float smartph = 5500;
        const float tablet = 4850;
        const float consola = 9000;
        const float Tcredito = 5;
        const float contado = 10;
        const float actualizar = 1500;
        const float garantia = 100;

        public struct REPORTE
        {
            public string nombre;
            public string aparato;
            public int monto;
        }

        const int Maximo = 10;

        REPORTE[] reporte;

        int Posicion = 0; 



        public frmService()
        {
            InitializeComponent();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            float resultado = 0;
            float opcionales = 0; 
            float recargo = 0;
            float descuento = 0;
            int equipo = 0;


            // Le paso el precio de cada dispositivo a la var resultado 
            if (cmbTipoDispositivo.SelectedIndex == 0)
            {
                resultado = smartph;
            }
            else
            {
                if (cmbTipoDispositivo.SelectedIndex==1)
                {
                    resultado = tablet;
                }
                else
                {
                    if (cmbTipoDispositivo.SelectedIndex ==2) 
                    {
                        resultado = consola;
                    }
                }
            }

            // me fijo si estan en true los chek y le paso el precio a la variables 
            if(chkASoftware.Checked == true)
            {
                opcionales = opcionales + actualizar;
            }
            else
            {
                opcionales = 0;
            }

            if (chkEGarantia.Checked == true) 
            {
                opcionales = opcionales + garantia;
            }
            else
            {
                opcionales = 0;
            }

            // calculo el resultado mas agregados  

            resultado = resultado + opcionales;

            // calculo descuentos o subas 

           if(optContado.Checked == true)
            {
                descuento = resultado * 10 / 100;
                resultado = resultado - descuento; 
            } else
            {
                recargo = resultado * 20 / 100;
                resultado = resultado + recargo; 
            }

            MessageBox.Show("Costo de equipos: " + resultado.ToString());
        }

        private void frmService_Load(object sender, EventArgs e)
        {
            //Crea el arreglo 

            reporte = new REPORTE[Maximo]; 

            chkASoftware.Checked = false;
            chkEGarantia.Checked = false;


            txtNombre.Text = "";
            txtTotal.Text = "";

            cmbTipoDispositivo.Items.Clear();
            cmbTipoDispositivo.Items.Add("Smartphone");
            cmbTipoDispositivo.Items.Add("Tablet");
            cmbTipoDispositivo.Items.Add("Consola");
            cmbTipoDispositivo.SelectedIndex = 0;

           


        }

        private void cmbTipoDispositivo_SelectedIndexChanged(object sender, EventArgs e)
        {

            if(cmbTipoDispositivo.SelectedIndex == 0)
            {
                cmbPlazo.Items.Clear();

                int d;
                
                for( d= 2; d < 11; d++)
                {
                    cmbPlazo.Items.Add(d);
                    
                }

                cmbPlazo.SelectedIndex = 0;

            }else {

                 if(cmbTipoDispositivo.SelectedIndex == 1) 
                {
                    cmbPlazo.Items.Clear();

                    int d; 

                    for(d = 1;  d < 8; d++)
                    {
                        cmbPlazo.Items.Add(d);
                    }
                    cmbPlazo.SelectedIndex = 0;
                }else{

                    if(cmbTipoDispositivo.SelectedIndex == 2)
                    {
                        cmbPlazo.Items.Clear();
                        
                        int d; 
                        
                        for(d =5; d < 13; d++)
                        {
                            cmbPlazo.Items.Add(d);
                        }
                        cmbPlazo.SelectedIndex = 0;
                    }

                }


            }


        }

        private void cmbPlazo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            if(txtNombre.Text != "")
            {
                btnCalcular.Enabled = true;
            }
            else
            {
                btnCalcular.Enabled = false;
            }
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
           
        }
    }
}
