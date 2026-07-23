using System;
using System.Collections.Generic;

namespace SG_BAMS.Bitacora;

/// <summary>
/// Registro inmutable que representa una entrada de la bitácora del sistema.
/// Se modela como <c>record</c> porque es un objeto de solo lectura una vez creado
/// (Value Object), no una entidad con identidad mutable.
/// </summary>
/// <param name="Nombre">Nombre del usuario que realizó la acción.</param>
/// <param name="Accion">Acción ejecutada.</param>
/// <param name="Modulo">Módulo del sistema donde ocurrió la acción.</param>
/// <param name="Fecha">Fecha y hora del registro.</param>
public sealed record BitacoraDTO(string Nombre, string Accion, string Modulo, DateTime Fecha);