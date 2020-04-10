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
                    {
                        Id = 1,
                        Title = "Recordatorio .NET Core",
                        Body = "Profundizar en la programación con VSCode",
                        DateTime = DateTime.Now,
                    },
                    new Note
                    {
                        Id = 2,
                        Title = "Recordatorio Angular",
                        Body = "Profundizar en el lenguaje TypeScript",
                        DateTime = DateTime.Now,
                    });
                context.SaveChanges();
            }
        }
    }
}
