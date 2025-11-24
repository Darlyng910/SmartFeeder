using System.Net.Http.Json;

Console.WriteLine("SmartFeeder Scheduler iniciado...");

// Cliente HTTP apuntando al microservicio
var http = new HttpClient();
http.BaseAddress = new Uri("http://smartfeeder-api:8080");

var ultimoRegistro = new Dictionary<int, DateTime>();

while (true)
{
    Console.WriteLine($"[{DateTime.Now}] Ciclo del Scheduler...");

    try
    {
        var mascotas = await http.GetFromJsonAsync<List<MascotaDto>>("/api/mascotas");

        if (mascotas == null || mascotas.Count == 0)
        {
            Console.WriteLine("No hay mascotas registradas.");
            await Task.Delay(30000);
            continue;
        }

        foreach (var mascota in mascotas)
        {
            var horarios = await http.GetFromJsonAsync<List<HorarioDto>>($"/api/horarios/mascota/{mascota.Id}");

            if (horarios == null || horarios.Count == 0)
            {
                Console.WriteLine($"No hay horarios para la mascota {mascota.Id}.");
                continue;
            }

            foreach (var h in horarios)
            {
                var ahora = DateTime.Now;

                if (h.Hora.Hour == ahora.Hour && h.Hora.Minute == ahora.Minute)
                {
                    if (ultimoRegistro.ContainsKey(h.Id))
                    {
                        var ultimaVez = ultimoRegistro[h.Id];

                        if (ultimaVez.Date == ahora.Date)
                        {
                            Console.WriteLine($"⏳ Ya se alimentó hoy según el horario {h.Id}. Evitando duplicado.");
                            continue;
                        }
                    }

                    Console.WriteLine($"⚡ Alimentando mascota {h.MascotaId} ({h.CantidadGramos}g)");

                    // 4️⃣ Registrar evento
                    await http.PostAsJsonAsync("/api/alimentacion/registrar", new
                    {
                        mascotaId = mascota.Id,
                        cantidadGramos = h.CantidadGramos
                    });

                    Console.WriteLine("✔ Evento de alimentación registrado.");

                    // Guardamos la fecha del último evento
                    ultimoRegistro[h.Id] = ahora;
                }
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Error llamando a la API: {ex.Message}");
    }

    await Task.Delay(30000);
}

// DTOs
public class MascotaDto
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
}

public class HorarioDto
{
    public int Id { get; set; }
    public int MascotaId { get; set; }
    public TimeOnly Hora { get; set; }
    public int CantidadGramos { get; set; }
}