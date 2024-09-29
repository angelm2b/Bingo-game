namespace BingoGame
{
    // Clase que representa a un jugador del Bingo
    class Jugador
    {
        public string Nombre { get; set; }
        public TarjetaBingo Tarjeta { get; set; }

        public Jugador(string nombre, TarjetaBingo tarjeta)
        {
            Nombre = nombre;
            Tarjeta = tarjeta;
        }
    }
}