namespace PeliculasAPI.Modelos
{
    public class PaginacionModel
    {
        public int Pagina { get; set; } = 1;
        public int cantidadRegistrosPorPagina = 10;
        private readonly int cantidaMaximaPorPagina = 50;

        public int CantidadRegistrosPorPagina
        {
            get => cantidadRegistrosPorPagina;  
           
            set
            {
                cantidadRegistrosPorPagina = (value > cantidaMaximaPorPagina) ? cantidaMaximaPorPagina : value;
            }
        }
    }
}
