using ApplicationCore.Entities;
using System;
using System.Linq;


namespace Infraestructure.Data
{
    public static class SeedData
    {
        public static void Initialize(NotePadContext context)
        {
            if(!context.Notes.Any())
            {
                context.Notes.AddRange(
                    new Note
                    (
                        1,
                        "Recordatorio .NET Core",
                        "Profundizar en la programación con VSCode",
                        DateTime.Now
                    ),
                    new Note
                    (
                        2,
                        "Recordatorio Angular",
                        "Profundizar en el lenguaje TypeScript",
                        DateTime.Now
                    ));
                context.SaveChanges();
            }
        }
    }
}
