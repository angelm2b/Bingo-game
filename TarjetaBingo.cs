namespace BingoGame
{
    // Clase que representa una tarjeta de Bingo
    class TarjetaBingo
    {
        public int[,] Numeros { get; set; }
        public bool[,] Marcados { get; set; }

        public TarjetaBingo()
        {
            Numeros = new int[5, 5];
            Marcados = new bool[5, 5];
        }
    }
}