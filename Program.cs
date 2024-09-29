namespace BingoGame
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("¡Bienvenido al juego de Bingo!");

            // Obtener el número de jugadores
            int numJugadores = ObtenerNumeroJugadores();

            // Crear la lista de jugadores
            List<Jugador> jugadores = CrearJugadores(numJugadores);
            bool juegoTerminado = false;
            int iteracion = 0;

            // Mostrar el tablero inicial
            MostrarTablero(jugadores, iteracion);
            Console.WriteLine("Presione una tecla para iniciar el juego...");
            Console.ReadKey();

            // Bucle principal del juego
            while (!juegoTerminado)
            {
                iteracion++;

                // Seleccionar un número aleatorio
                int numeroSeleccionado = SeleccionarNumero();

                // Marcar los números en las tarjetas de los jugadores
                MarcarNumeros(jugadores, numeroSeleccionado);
                Console.WriteLine($"Número llamado: {numeroSeleccionado}");
                MostrarTablero(jugadores, iteracion);

                Console.WriteLine("Presione cualquier tecla para continuar con la jugada...");
                Console.ReadKey();

                // Verificar si algún jugador ha ganado
                foreach (var jugador in jugadores)
                {
                    if (VerificarGanador(jugador.Tarjeta))
                    {
                        Console.WriteLine($"¡Bingo! El jugador {jugador.Nombre} ha ganado la partida en la iteración {iteracion}.");
                        juegoTerminado = true;
                        break;
                    }
                }
            }

            Console.WriteLine("¡Gracias por jugar al Bingo!");
            Console.ReadLine();
        }

        // Método para obtener el número de jugadores
        static int ObtenerNumeroJugadores()
        {
            while (true)
            {
                Console.Write("Ingrese el número de jugadores (Max. 5): ");
                string entrada = Console.ReadLine() ?? string.Empty;

                try
                {
                    int numJugadores = int.Parse(entrada ?? throw new ArgumentNullException(nameof(entrada)));

                    if (numJugadores < 1 || numJugadores > 5)
                    {
                        Console.WriteLine("Por favor, ingrese un número entre 1 y 5.");
                        continue;
                    }

                    return numJugadores;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ingrese un valor válido (Número entero).");
                }
            }
        }

        // Método para crear la lista de jugadores
        static List<Jugador> CrearJugadores(int numParticipantes)
        {
            List<Jugador> jugadores = new List<Jugador>();

            for (int i = 1; i <= numParticipantes; i++)
            {
                Console.Write($"Ingrese el nombre del jugador {i}: ");
                string nombre = ObtenerNombreJugador();
                var tarjeta = GenerarTarjetaBingo();
                jugadores.Add(new Jugador(nombre, tarjeta));
            }

            return jugadores;
        }

        // Método para obtener el nombre de un jugador
        static string ObtenerNombreJugador()
        {
            while (true)
            {
                string nombre = Console.ReadLine() ?? string.Empty;

                try
                {
                    if (!string.IsNullOrWhiteSpace(nombre) && nombre.All(char.IsLetter))
                    {
                        return nombre;
                    }
                    else
                    {
                        Console.WriteLine("Por favor, ingrese un nombre válido (Solo texto).");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error inesperado: {ex.Message}");
                }
            }
        }

        // Método para generar una tarjeta de Bingo
        static TarjetaBingo GenerarTarjetaBingo()
        {
            var tarjeta = new TarjetaBingo();
            Random random = new Random();

            for (int j = 0; j < 5; j++)
            {
                HashSet<int> numerosDisponibles = new HashSet<int>(Enumerable.Range(1, 75));

                for (int i = 0; i < 5; i++)
                {
                    if (i == 2 && j == 2)
                    {
                        tarjeta.Marcados[i, j] = true;
                    }
                    else
                    {
                        int numero;
                        switch (j)
                        {
                            case 0: numero = ObtenerNumeroNoRepetidoEnColumna(random, numerosDisponibles, tarjeta.Numeros, i, 0, 1, 16); break; // Columna B
                            case 1: numero = ObtenerNumeroNoRepetidoEnColumna(random, numerosDisponibles, tarjeta.Numeros, i, 1, 16, 31); break; // Columna I
                            case 2: numero = ObtenerNumeroNoRepetidoEnColumna(random, numerosDisponibles, tarjeta.Numeros, i, 2, 31, 46); break; // Columna N
                            case 3: numero = ObtenerNumeroNoRepetidoEnColumna(random, numerosDisponibles, tarjeta.Numeros, i, 3, 46, 61); break; // Columna G
                            case 4: numero = ObtenerNumeroNoRepetidoEnColumna(random, numerosDisponibles, tarjeta.Numeros, i, 4, 61, 76); break; // Columna O
                            default: throw new InvalidOperationException("Columna inválida.");
                        }

                        tarjeta.Numeros[i, j] = numero;
                    }
                }
            }

            return tarjeta;
        }

        // Método para obtener un número no repetido en la misma columna
        static int ObtenerNumeroNoRepetidoEnColumna(Random random, HashSet<int> numerosDisponibles, int[,] tarjetaNumeros, int fila, int columna, int min, int max)
        {
            int numero;
            do
            {
                numero = random.Next(min, max);
            } while (!numerosDisponibles.Contains(numero) || YaExisteEnColumna(tarjetaNumeros, fila, columna, numero));

            numerosDisponibles.Remove(numero);
            return numero;
        }

        // Método para verificar si un número ya existe en la misma columna
        static bool YaExisteEnColumna(int[,] tarjetaNumeros, int fila, int columna, int numero)
        {
            for (int i = 0; i < fila; i++)
            {
                if (tarjetaNumeros[i, columna] == numero)
                {
                    return true;
                }
            }
            return false;
        }

        // Método para seleccionar un número aleatorio
        static int SeleccionarNumero()
        {
            Random random = new Random();
            return random.Next(1, 76);
        }

        // Método para marcar los números en las tarjetas de los jugadores
        static void MarcarNumeros(List<Jugador> jugadores, int numero)
        {
            foreach (var jugador in jugadores)
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (jugador.Tarjeta.Numeros[i, j] == numero)
                        {
                            jugador.Tarjeta.Marcados[i, j] = true;
                            break;
                        }
                    }
                }
            }
        }

        // Método para mostrar el tablero de los jugadores
        static void MostrarTablero(List<Jugador> jugadores, int iteracion)
        {
            TableroJugadores tableroJugadores = new TableroJugadores(jugadores, iteracion);
            tableroJugadores.MostrarTablero();
        }


        // Método para verificar si un jugador ha ganado
        static bool VerificarGanador(TarjetaBingo tarjeta)
        {
            // Verificar filas
            for (int i = 0; i < 5; i++)
            {
                bool filaCompleta = true;
                for (int j = 0; j < 5; j++)
                {
                    if (!tarjeta.Marcados[i, j])
                    {
                        filaCompleta = false;
                        break;
                    }
                }

                if (filaCompleta)
                {
                    return true;
                }
            }

            // Verificar columnas
            for (int j = 0; j < 5; j++)
            {
                bool columnaCompleta = true;
                for (int i = 0; i < 5; i++)
                {
                    if (!tarjeta.Marcados[i, j])
                    {
                        columnaCompleta = false;
                        break;
                    }
                }

                if (columnaCompleta)
                {
                    return true;
                }
            }

            // Verificar diagonales
            bool diagonal1Completa = true;
            bool diagonal2Completa = true;
            for (int i = 0; i < 5; i++)
            {
                if (!tarjeta.Marcados[i, i])
                {
                    diagonal1Completa = false;
                }

                if (!tarjeta.Marcados[i, 4 - i])
                {
                    diagonal2Completa = false;
                }
            }

            // Verificar si alguna diagonal es completa
            if (diagonal1Completa || diagonal2Completa)
            {
                return true;
            }

            return false;
        }
    }
}