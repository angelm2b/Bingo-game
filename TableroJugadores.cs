namespace BingoGame
{
    // Clase para mostrar el tablero de los jugadores
    class TableroJugadores
    {
        private List<Jugador> _jugadores;
        private int _iteracion;

        public TableroJugadores(List<Jugador> jugadores, int iteracion)
        {
            _jugadores = jugadores ?? throw new ArgumentNullException(nameof(jugadores));
            _iteracion = iteracion;
        }

        public void MostrarTablero()
        {
            Console.WriteLine($"Iteración: {_iteracion}");

            int alturaTarjeta = _jugadores.First().Tarjeta.Numeros.GetLength(0);

            foreach (var jugador in _jugadores)
            {
                Console.Write($"Jugador: {jugador.Nombre,-15}");
            }

            Console.WriteLine();

            for (int i = 0; i < alturaTarjeta; i++)
            {
                if (i == 0)
                {
                    foreach (var jugador in _jugadores)
                    {
                        Console.Write("B  I  N  G  O\t\t");
                    }
                    Console.WriteLine();
                }

                foreach (var jugador in _jugadores)
                {
                    MostrarFila(jugador.Tarjeta, i);
                    Console.Write("\t\t");
                }

                Console.WriteLine();
            }
        }

        private static void MostrarFila(TarjetaBingo tarjeta, int fila)
        {
            for (int j = 0; j < tarjeta.Numeros.GetLength(1); j++)
            {
                if (tarjeta.Marcados[fila, j])
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write($"{tarjeta.Numeros[fila, j],-3}");
                    Console.ResetColor();
                }
                else
                {
                    Console.Write($"{tarjeta.Numeros[fila, j],-3}");
                }
            }
        }
    }
}