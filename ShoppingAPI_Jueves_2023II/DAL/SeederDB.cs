using ShoppingAPI_Jueves_2023II.DAL.Entities;

namespace ShoppingAPI_Jueves_2023II.DAL
{
    public class SeederDB
    {
        private readonly DataBaseContext _context;

        public SeederDB(DataBaseContext context)
        {
            _context = context;
        }

        //Crearemos un método llamado SeederAsync()
        //Este método es una especie de MAIN()
        //Este método tendrá la responsabilidad de prepoblar mis diferentes tablas de la BD.

        public async Task SeederAsync()
        {
            //Primero: agregaré un método propio de EF que hace las veces del comando 'update-database'
            //En otras palabras: un método que me creará la BD inmediatamente ponga en ejecución mi API
            await _context.Database.EnsureCreatedAsync();

            //A partir de aquí vamos a ir creando métodos que me sirvan para prepoblar mi BD
            await PopulateCountriesAsync();

            await _context.SaveChangesAsync(); //Esta línea me guarda ls datos en BD
        }

        #region Private Methos
        private async Task PopulateCountriesAsync()
        {
            //El método Any() me indica si la tabla Countries tiene al menos un registro
            //El método Any negado (!) me indica que no hay absolutamente nada en la tabla Countries.

            if (!_context.Countries.Any()) 
            {
                //Así creo yo un objeto país con sus respectivos estados
                _context.Countries.Add(new Country
                {
                    CreatedDate = DateTime.Now,
                    Name = "Colombia",
                    States = new List<State>()
                    {
                        new State
                        {
                            CreatedDate = DateTime.Now,
                            Name = "Antioquia"
                        },

                        new State
                        {
                            CreatedDate = DateTime.Now,
                            Name = "Cundinamarca"
                        }
                    }
                });

                //Aquí creo otro nuevo país
                _context.Countries.Add(new Country
                {
                    CreatedDate = DateTime.Now,
                    Name = "Argentina",
                    States = new List<State>()
                    {
                        new State
                        {
                            CreatedDate = DateTime.Now,
                            Name = "Buenos Aires"
                        }
                    }
                });
            }
        }
    }

    #endregion
}
