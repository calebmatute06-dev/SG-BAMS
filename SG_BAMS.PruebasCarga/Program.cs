using NBomber.CSharp;
using SG_BAMS.Administracion_de_BAMS.Usuarios;

// "Calienta" la conexión antes de la carga concurrente (mismo fix que en Catálogos)
var repoCalentamiento = new clsUsuario();
await repoCalentamiento.LeerUsuariosAsync();
Console.WriteLine("Conexión calentada correctamente. Iniciando prueba...");

// ==========================================================
// CAMBIA ESTA LÍNEA para alternar entre las dos pruebas:
// true  = Prueba de CARGA (rampa hasta 100/seg)
// false = Prueba de LÍMITE (rampa hasta 200/seg)
// ==========================================================
bool esPruebaDeCarga = true;

const int ID_ROL_PRUEBA = 1; // <-- confirma con SELECT TOP 1 id_rol_usuario FROM Rol;

// ---------- Escenario: Insertar Usuarios ----------
var escenarioUsuarios = Scenario.Create("Usuarios_Insertar", async context =>
{
    try
    {
        long idUnico = DateTime.Now.Ticks + context.InvocationNumber;
        var repo = new clsUsuario();

        bool ok = await repo.InsertarUsuarioAsync(
            nombre: $"Usuario_Prueba_{idUnico}",
            password: "Test123",
            idRol: ID_ROL_PRUEBA,
            imagen: null,
            correo: $"prueba_{idUnico}@test.com"
        );

        return ok ? Response.Ok() : Response.Fail(message: "InsertarUsuarioAsync devolvió false");
    }
    catch (Exception ex)
    {
        return Response.Fail(message: ex.Message);
    }
});

// ---------- Selección de simulación según el interruptor ----------
if (esPruebaDeCarga)
{
    var escenarioCarga = escenarioUsuarios.WithLoadSimulations(
        Simulation.RampingInject(rate: 100, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromSeconds(30))
    );

    NBomberRunner
        .RegisterScenarios(escenarioCarga)
        .WithReportFolder("reports/usuarios_carga")
        .Run();
}
else
{
    var escenarioLimite = escenarioUsuarios.WithLoadSimulations(
        Simulation.RampingInject(rate: 200, interval: TimeSpan.FromSeconds(1), during: TimeSpan.FromSeconds(30))
    );

    NBomberRunner
        .RegisterScenarios(escenarioLimite)
        .WithReportFolder("reports/usuarios_limite")
        .Run();
}