using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SG_BAMS.Cliente
{
    internal class ClsModificarCliente:ClsConexion
    {

        public async Task<int> ModificarClientes(int idCliente,string nombre, string apellido, string telefono, string RTN, int idEstado)
        {
            
            try 
            {

                AbrirConexion();

                using (SqlCommand cmd = new SqlCommand("PA_insertar_cliente", Conectar))
                {
                    


                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id_cliente", idCliente);
                    cmd.Parameters.AddWithValue("@nombre_cliente", nombre);
                    cmd.Parameters.AddWithValue("@apellido_cliente", apellido);
                    cmd.Parameters.AddWithValue("@telefono_cliente", telefono);
                    cmd.Parameters.AddWithValue("@rtn_cliente", RTN);
                    cmd.Parameters.AddWithValue("@id_estado", idEstado);

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
