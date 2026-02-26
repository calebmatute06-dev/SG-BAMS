using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data.SqlClient;
using System.Data;

namespace SG_BAMS.Cliente
{
    internal class ClsAgregarClientes:ClsConexion
    {

        public async Task<int> AgregarClientes(string nombre, string apellido, string telefono, string RTN)
        {
            try
            {
                AbrirConexion();

                using (SqlCommand cmd = new SqlCommand("PA_insertar_cliente",Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@nombre_cliente", nombre);
                    cmd.Parameters.AddWithValue("@apellido_cliente", apellido);
                    cmd.Parameters.AddWithValue("@telefono_cliente", telefono);
                    cmd.Parameters.AddWithValue("@rtn_cliente", RTN);

                    object result = await cmd.ExecuteScalarAsync();

                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }

                    return 0;

                }
            
            
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar cliente: " + ex.Message);
                return 0;
            }
            finally
            {
                Cerrar();
            }
        
        
        
        }


    }
}
