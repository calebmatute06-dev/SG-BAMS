using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_BAMS.Facturas
{
    internal class ClsAgregarFactura:ClsConexion
    {

        public async Task<int> AgregarFacturas(int idusuario,int idcliente, int pago, DateTime fecha, int bateria)
        {
            try
            {
                AbrirConexion();

                using (SqlCommand cmd = new SqlCommand("PA_insertar_factura", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id_usuario", idusuario);
                    cmd.Parameters.AddWithValue("@id cliente", idcliente);
                    cmd.Parameters.AddWithValue("@id_tipo_forma_pago", pago);
                    cmd.Parameters.AddWithValue("@fecha_venta", fecha);
                    cmd.Parameters.AddWithValue("@bateria_vieja", bateria);

                    int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                    return filasAfectadas;

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
            finally
            {
                Cerrar();
            }



        }
    }
}
