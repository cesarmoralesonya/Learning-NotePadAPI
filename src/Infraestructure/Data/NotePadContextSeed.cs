using ApplicationCore.Entities;
using System;
using System.Linq;


namespace Infraestructure.Data
{
    public static class NotePadContextSeed
    {
        public static void Initialize(NotePadContext context)
        {
            if(!context.Notes.Any())
            {
                context.Notes.AddRange(
                    new Note
                    (
                        "Recordatorio .NET Core",
                        "Profundizar en la programación con VSCode",
                        DateTime.Now
                    ),
                    new Note
                    (
                        "Recordatorio Angular",
                        "Profundizar en el lenguaje TypeScript",
                        DateTime.Now
                    ));
                context.SaveChanges();
            }
        }
    }
}
