using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Data.SqlClient;

namespace SG_BAMS
{
    /// <summary>
    /// 
    /// </summary>
    public class ClsRepositorioBaseDatos
    {
        /// <summary>
        /// La cadena de conexión
        /// </summary>
        private String CadenaConexion = ConfigurationManager.ConnectionStrings["AutoBattDB"].ConnectionString;
        /// <summary>
        /// La conexión
        /// </summary>
        public SqlConnection Conectar = new SqlConnection();

        /// <summary>
        /// Semáforo GLOBAL (static): lo comparten TODAS las clases que
        /// heredan de ClsRepositorioBaseDatos (ClsFactura, ClsDetalleFactura,
        /// etc), porque todas compiten por el mismo límite de conexiones
        /// concurrentes que Somee permite. Sin esto, bajo carga alta (ej.
        /// nivel=300) se agota el pool de conexiones y empiezan a fallar.
        ///
        /// AJUSTA este número probando con la prueba de estrés: sube o baja
        /// hasta encontrar el punto donde el % de éxito se estabiliza.
        /// </summary>
        private static readonly SemaphoreSlim semaforo = new SemaphoreSlim(35, 35);

        /// <summary>
        /// Abre la conexión a la base de datos, esperando su turno si ya
        /// hay demasiadas conexiones abiertas al mismo tiempo.
        /// </summary>
        /// <exception cref="System.Exception">Error de conexion a la base de datos: " + ex.Message</exception>
        public void AbrirConexion()
        {
            semaforo.Wait();
            try
            {
                Conectar.ConnectionString = CadenaConexion;
                if (Conectar.State == ConnectionState.Closed)
                {
                    Conectar.Open();
                }
            }
            catch (Exception ex)
            {
                // Si falla al abrir, hay que liberar el permiso aquí mismo,
                // porque Cerrar() nunca se va a llamar si esto revienta.
                semaforo.Release();
                throw new Exception("Error de conexion a la base de datos: " + ex.Message);
            }
        }

        /// <summary>
        /// Versión async de AbrirConexion, para usarla en los métodos que
        /// ya son async (recomendado para no bloquear hilos bajo carga alta).
        /// </summary>
        public async Task AbrirConexionAsync()
        {
            await semaforo.WaitAsync();
            try
            {
                Conectar.ConnectionString = CadenaConexion;
                if (Conectar.State == ConnectionState.Closed)
                {
                    await Conectar.OpenAsync();
                }
            }
            catch (Exception ex)
            {
                semaforo.Release();
                throw new Exception("Error de conexion a la base de datos: " + ex.Message);
            }
        }

        /// <summary>
        /// Cierra la conexión a la base de datos y libera el permiso del
        /// semáforo para que otra petición en espera pueda pasar.
        /// </summary>
        public void Cerrar()
        {
            if (Conectar.State == ConnectionState.Open)
            {
                Conectar.Close();
            }
            semaforo.Release();
        }
    }
}